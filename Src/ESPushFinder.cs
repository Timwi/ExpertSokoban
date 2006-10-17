using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using RT.Util;

namespace ExpertSokoban
{
    public class ESPushFinder 
    {
        private SokobanLevel FLevel;

        // These arrays are layed out like this:
        // element #0 = virtual start node
        // element #4*(y*sizex+x)+1 = node representing piece at (x,y) with Sokoban above
        //                       +2 = on the left
        //                       +3 = on the right
        //                       +4 = below
        // The value "0" means "infinity" (so we don't need to initialise the arrays).
        // Values above zero mean "one less than this".
        private int[] FPushLength;
        private int[] FMoveLength;
        private int[] FPredecessor;
        private PackedBooleans FExtracted;
        private Control FCallbackOwner;
        private Delegate FCallbackFound, FCallbackDone;

        // For node a, path[a][b] is the sequence of moves to go from
        //  a = 4*(y*sizex+x)+d to 4*(y*sizex+x)+b+1.
        private int[][][] FPath;

        // For each node, the walking distance (plus one) to another side (above/right/below/left).
        // Not currently used. Should be used for performance optimisation later.
        // private int[][] edgeWeights;

        private bool FDone;
        private int FSelX, FSelY, FSelPos, // the original position of the piece we are actually pushing.
                    FSokPos, FNumPossibilities;
        private ESMoveFinder FMoveFinder;

        // predefinedPaths[d1][d2] contains several suggestions on how to get from dir d1 to dir d2
        private int[][][][] FPredefinedPaths;

        public bool Done { get { return FDone; } }

        class SpecialHeap
        {
            class ThreeIntegers
            {
                private int FIndex, FPushLen, FMoveLen;
                public ThreeIntegers (int i, int p, int m)
                { FIndex = i; FPushLen = p; FMoveLen = m; }
                public int Index { get { return FIndex; } }
                public int PushLen { get { return FPushLen; } }
                public int MoveLen { get { return FMoveLen; } }
            }
            private ESPushFinder FParent;
            private ThreeIntegers[] FElement;
            private int FNumElements;

            public bool Empty { get { return FNumElements == 0; } }

            public SpecialHeap(ESPushFinder Parent)
            {
                FParent = Parent;
                FElement = new ThreeIntegers[64];
                FNumElements = 0;
            }

            private int Compare (int POne, int PTwo)
            {
                ThreeIntegers One = FElement[POne];
                ThreeIntegers Two = FElement[PTwo];
                if (One.PushLen < Two.PushLen) return -1;
                if (One.PushLen > Two.PushLen) return 1;
                if (One.MoveLen < Two.MoveLen) return -1;
                if (One.MoveLen > Two.MoveLen) return 1;
                return 0;
            }

            private void Swap (int One, int Two)
            {
                ThreeIntegers tmp = FElement[One];
                FElement[One] = FElement[Two];
                FElement[Two] = tmp;
            }
            private void ReheapifyUp (int Index)
            {
                int Parent = (Index-1)/2;
                while (Index > 0 && Compare (Index, Parent) < 0)
                {
                    Swap (Index, Parent);
                    Index = Parent;
                    Parent = (Index-1)/2;
                }
            }
            private void ReheapifyDown (int Index)
            {
                while (Index < FNumElements/2)
                {
                    // special case: only one child
                    if (FNumElements % 2 == 0 && Index == FNumElements/2-1)
                    {
                        if (Compare (Index, FNumElements-1) > 0)
                            Swap (Index, FNumElements-1);
                        return;
                    }
                    else
                    {
                        int Child1 = 2*Index+1;
                        int Child2 = Child1+1;
                        int ChildCompare = Compare (Child1, Child2);
                        if (Compare (Child1, Index) < 0 && ChildCompare <= 0)
                        {
                            Swap (Child1, Index);
                            Index = Child1;
                        }
                        else if (Compare (Child2, Index) < 0 && ChildCompare >= 0)
                        {
                            Swap (Child2, Index);
                            Index = Child2;
                        }
                        else return;
                    }
                }
            }

            // This means: Add the node with the index i in the arrays "pushLength"/"moveLength".
            public void Add (int i)
            {
                if (FNumElements == FElement.Length)
                {
                    ThreeIntegers[] NewArray = new ThreeIntegers [ 2*FElement.Length ];
                    for (int j = 0; j < FElement.Length; j++)
                        NewArray[j] = FElement[j];
                    FElement = NewArray;
                }
                FElement [ FNumElements ] = new ThreeIntegers (i, FParent.FPushLength[i], FParent.FMoveLength[i]);
                FNumElements++;
                ReheapifyUp (FNumElements-1);
            }
            public int Extract()
            {
                if (FNumElements > 0)
                {
                    int Result = FElement[0].Index;
                    FNumElements--;
                    FElement[0] = FElement [ FNumElements ];
                    ReheapifyDown (0);
                    return Result;
                }
                else return -1;
            }
        };

        private SpecialHeap PriorityQueue;

        public ESPushFinder(SokobanLevel Level, int x, int y, Control Owner, Delegate FoundCallback, 
            Delegate DoneCallback, ESMoveFinder MoveFinder)
        {
            FCallbackOwner = Owner;
            FCallbackFound = FoundCallback;
            FCallbackDone = DoneCallback;
            FLevel = Level;
            FSelX = x;
            FSelY = y;
            int sx = FLevel.Width;
            FSelPos = y*sx + x;
            FSokPos = FLevel.SokobanPos;
            FDone = false;
            PriorityQueue = new SpecialHeap(this);
            int ArraySize = 4 * sx * Level.Height;
            FPushLength = new int[ArraySize];
            FMoveLength = new int[ArraySize];
            FPredecessor = new int[ArraySize];
            FExtracted = new PackedBooleans (ArraySize);
            FPath = new int[ArraySize][][];
            FNumPossibilities = 0;

            FPredefinedPaths = new int[][][][]
            {
                // from direction 0 ...
                new int[][][]{
                    // to direction 0...
                    null,
                    // to direction 1...
                    new int[][]{ new int[]{ -1, sx }, new int[]{ 1, sx, sx, -1, -1, -sx }, new int[]{ -sx, -1, -1, sx, sx, 1 } },
                    // to direction 2...
                    new int[][]{ new int[]{ 1, sx }, new int[]{ -1, sx, sx, 1, 1, -sx }, new int[]{ -sx, 1, 1, sx, sx, -1 } },
                    // to direction 3...
                    new int[][]{ new int[]{ -1, sx, sx, 1 }, new int[]{ 1, sx, sx, -1} }
                },
                // from direction 1 ...
                new int[][][]{
                    // to direction 0...
                    new int[][]{ new int[]{ -sx, 1 }, new int[]{ sx, 1, 1, -sx, -sx, -1 }, new int[]{ -1, -sx, -sx, 1, 1, sx } },
                    // to direction 1...
                    null,
                    // to direction 2...
                    new int[][]{ new int[]{ -sx, 1, 1, sx }, new int[]{ sx, 1, 1, -sx } },
                    // to direction 3...
                    new int[][]{ new int[]{ sx, 1 }, new int[]{ -sx, 1, 1, sx, sx, -1 }, new int[]{ -1, sx, sx, 1, 1, -sx } }
                },
                // from direction 2 ...
                new int[][][]{
                    // to direction 0...
                    new int[][]{ new int[]{ -sx, -1 }, new int[]{ sx, -1, -1, -sx, -sx, 1 }, new int[]{ 1, -sx, -sx, -1, -1, sx } },
                    // to direction 1...
                    new int[][]{ new int[]{ sx, -1, -1, -sx }, new int[]{ -sx, -1, -1, sx } },
                    // to direction 2...
                    null,
                    // to direction 3...
                    new int[][]{ new int[]{ sx, -1 }, new int[]{ -sx, -1, -1, sx, sx, 1 }, new int[]{ 1, sx, sx, -1, -1, -sx } }
                },
                // from direction 3 ...
                new int[][][]{
                    // to direction 0...
                    new int[][]{ new int[]{ -1, -sx, -sx, 1 }, new int[]{ 1, -sx, -sx, -1} },
                    // to direction 1...
                    new int[][]{ new int[]{ -1, -sx }, new int[]{ 1, -sx, -sx, -1, -1, sx }, new int[]{ sx, -1, -1, -sx, -sx, 1 } },
                    // to direction 2...
                    new int[][]{ new int[]{ 1, -sx }, new int[]{ -1, -sx, -sx, 1, 1, sx }, new int[]{ sx, 1, 1, -sx, -sx, -1 } },
                    // to direction 3...
                    null
                },
            };

            int i = 4*(y*sx + x) + 1;
            int j = (y-1)*sx + x;

            // predecessor will already automatically be 0

            if (MoveFinder.MoveValid (j))
            {
                FPushLength[i] = 1;
                FMoveLength[i] = MoveFinder.PathLength (j)+1;
                PriorityQueue.Add (i);
            }
            i++;
            j += sx-1;
            if (MoveFinder.MoveValid (j))
            {
                FPushLength[i] = 1;
                FMoveLength[i] = MoveFinder.PathLength (j)+1;
                PriorityQueue.Add (i);
            }
            i++;
            j += 2;
            if (MoveFinder.MoveValid (j))
            {
                FPushLength[i] = 1;
                FMoveLength[i] = MoveFinder.PathLength (j)+1;
                PriorityQueue.Add (i);
            }
            i++;
            j += sx-1;
            if (MoveFinder.MoveValid (j))
            {
                FPushLength[i] = 1;
                FMoveLength[i] = MoveFinder.PathLength (j)+1;
                PriorityQueue.Add (i);
            }

            FPushLength[0] = 1;
            if (PriorityQueue.Empty)
            {
                FCallbackOwner.Invoke(FCallbackDone, new object[] { false });
                FDone = true;
            }

            FMoveFinder = MoveFinder;
        }

        public void SingleStep()
        {
            if (FDone) return;

            // extract the next item, filter out duplicates, quit if priority queue is empty
            int item;
            do
            {
                if (PriorityQueue.Empty)
                {
                    FDone = true;
                    FCallbackOwner.Invoke(FCallbackDone, new object[] { FNumPossibilities > 0 });
                    return;
                }
                item = PriorityQueue.Extract();
            }
            while (FExtracted.Get (item));

            FExtracted.Set (item, true);

            // Dijkstra's algorithm: We extract a node from our priority queue and relax
            // all its outgoing edges. However, we might not yet know the length of those
            // edges; might have to invoke MoveFinder to determine them. We also don't have
            // all nodes in the priority queue; we are adding them as we discover them.

            int direction = (item-1) % 4;
                // 0 = Sokoban above piece, 1 = left, 2 = right, 3 = below
            int position = (item-1) / 4;
            FLevel.MovePiece (FSelPos, position);
            int sx = FLevel.Width;
            int nSokPos = position +
                ((direction == 0) ? -sx :
                 (direction == 1) ? -1:
                 (direction == 2) ? 1 : sx);
            FLevel.SetSokobanPos (nSokPos);

            // Now try to find out which of the other squares adjacent to the piece
            // the Sokoban can move to.  For optimisation, we first check if there
            // is a trivial path (one of the paths predefined in predefinedPaths).
            // If not, we will run a BSMoveFinder.

            FPath[item] = new int[4][];

            bool stop = false;
            for (int consider_direction = 0; consider_direction < 4 && !stop; consider_direction++)
            {
                if (consider_direction != direction)
                {
                    int consider_spot = position + ((consider_direction == 0) ? -sx :
                                                    (consider_direction == 1) ? -1:
                                                    (consider_direction == 2) ? 1 : sx);
                    if (FLevel.IsFree (consider_spot))
                    {
                        bool found = false;

                        // now give each of the predefined paths a try.
                        for (int predef = 0; predef < FPredefinedPaths[direction][consider_direction].Length && !found; predef++)
                        {
                            int curPos = nSokPos;

                            // This might seem a bit weird, but it works as follows: for as long as
                            // we can walk along the path, "found" will be true.  Hence, if we can
                            // walk the entire path, "found" will still be true at the end, which is
                            // what we want, because it means we have found a path.
                            found = true;
                            for (int i = 0; i < FPredefinedPaths[direction][consider_direction][predef].Length && found; i++)
                            {
                                curPos += FPredefinedPaths[direction][consider_direction][predef][i];
                                found = FLevel.IsFree (curPos);
                            }
                            if (found)  // We have found a path, so let's remember it.
                                FPath[item][consider_direction] = FPredefinedPaths[direction][consider_direction][predef];
                        }
                        if (!found)  // There is a free space, but we couldn't find a path to it.
                        // Hence, no matter what we find for the other directions, we will have to
                        // run the BSMoveFinder anyway.
                        {
                            ESMoveFinder mf = new ESMoveFinder(FLevel, true, null, null, null, position);
                            mf.SingleStep();

                            // mf.getPath will return null if you can't walk to any of these
                            if (direction != 0) FPath[item][0] = mf.Path (position - sx);
                            if (direction != 1) FPath[item][1] = mf.Path (position - 1);
                            if (direction != 2) FPath[item][2] = mf.Path (position + 1);
                            if (direction != 3) FPath[item][3] = mf.Path (position + sx);
                            stop = true;
                        }
                    }
                }
            }

            int index = 4*position + 1;

            // If we can move above the piece (this is automatically null if we are already there)
            if (FPath[item][0] != null)
            {
                // ... then consider the edge leading to that position.
                if (FPushLength[index] == 0
                    || FPushLength[index] > FPushLength[item]
                    || (FPushLength[index] == FPushLength[item]
                        && FMoveLength[index] > FMoveLength[item] + FPath[item][0].Length))
                {
                    FPushLength[index] = FPushLength[item];
                    FMoveLength[index] = FMoveLength[item] + FPath[item][0].Length;
                    FPredecessor[index] = item;
                    PriorityQueue.Add (index);
                }
            }
            // Left...
            index++;
            if (FPath[item][1] != null)
            {
                if (FPushLength[index] == 0
                    || FPushLength[index] > FPushLength[item]
                    || (FPushLength[index] == FPushLength[item]
                        && FMoveLength[index] > FMoveLength[item] + FPath[item][1].Length))
                {
                    FPushLength[index] = FPushLength[item];
                    FMoveLength[index] = FMoveLength[item] + FPath[item][1].Length;
                    FPredecessor[index] = item;
                    PriorityQueue.Add (index);
                }
            }
            // Right...
            index++;
            if (FPath[item][2] != null)
            {
                if (FPushLength[index] == 0
                    || FPushLength[index] > FPushLength[item]
                    || (FPushLength[index] == FPushLength[item]
                        && FMoveLength[index] > FMoveLength[item] + FPath[item][2].Length))
                {
                    FPushLength[index] = FPushLength[item];
                    FMoveLength[index] = FMoveLength[item] + FPath[item][2].Length;
                    FPredecessor[index] = item;
                    PriorityQueue.Add (index);
                }
            }
            // Below...
            index++;
            if (FPath[item][3] != null)
            {
                if (FPushLength[index] == 0
                    || FPushLength[index] > FPushLength[item]
                    || (FPushLength[index] == FPushLength[item]
                        && FMoveLength[index] > FMoveLength[item] + FPath[item][3].Length))
                {
                    FPushLength[index] = FPushLength[item];
                    FMoveLength[index] = FMoveLength[item] + FPath[item][3].Length;
                    FPredecessor[index] = item;
                    PriorityQueue.Add (index);
                }
            }

            // Finally, also consider actually pushing the piece.
            bool aFound = false;
            int nPushTo = position +
                ((direction == 0) ? sx :
                 (direction == 1) ? 1:
                 (direction == 2) ? -1 : -sx);
            if (FLevel.IsFree (nPushTo))
            {
                index = 4*nPushTo + direction + 1;
                if (FPushLength[index] == 0
                    || FPushLength[index] > FPushLength[item]+1
                    || (FPushLength[index] == FPushLength[item]+1
                        && FMoveLength[index] > FMoveLength[item]+1))
                {
                    FPushLength[index] = FPushLength[item]+1;
                    FMoveLength[index] = FMoveLength[item]+1;
                    FPredecessor[index] = item;
                    PriorityQueue.Add (index);

                    // We have to defer calling pushThreadFound because we
                    // have made changes to the level...
                    aFound = true;
                }
            }

            // restore the level as it was
            FLevel.MovePiece (position, FSelPos);
            FLevel.SetSokobanPos (FSokPos);
            // now we can call pushThreadFound
            if (aFound)
            {
                FCallbackOwner.Invoke(FCallbackFound, new object[] { nPushTo });
                FNumPossibilities++;
            }

            if (PriorityQueue.Empty)
            {
                FDone = true;
                FCallbackOwner.Invoke(FCallbackDone, new object[] { FNumPossibilities >0 });
            }
        }
        public bool PushValid (int Pos, int Direction) {
            int index = 4*Pos+Direction;
            return index <= 0 || index >= FPushLength.Length ? false : FPushLength[4*Pos+Direction] > 0; 
        }
        public bool PushValid (int Pos)
        {
            return  PushValid (Pos, 1) || PushValid (Pos, 2) ||
                    PushValid (Pos, 3) || PushValid (Pos, 4);
        }
        public int MoveLength (int Pos) { return FPushLength[Pos]; }
        public int PushLength (int Pos) { return FMoveLength[Pos]; }

        /* - It's a bit weird to return an array of arrays here, but I think
             this is more efficient, because it means we don't have to glue
             the arrays together to form a bigger array.
           - The array returned may have some extra "null"s at the *front*.
           - pos = y*sizex + x.
           - posInDir means: Try to place the Sokoban above (1), to the
             left (2), right (3), or below (4) the piece. If the specified
             placement is not possible, or any other number is specified,
             we will automatically find the one with the smallest number
             of pushes.
        */
        public int[][] getMoves (int pos, int posInDir)
        {
            if (pos < 0 || 4*pos+4 > FPushLength.Length || !PushValid(pos))
                return null;
            int dir = 0;

            if ((posInDir > 0 && posInDir < 5) && FPushLength[4*pos+posInDir] > 0)
                dir = posInDir;
            else
            {
                // "pos" is the position where we want to move the piece.
                // There are four nodes in the graph that can represent
                // this position. If the user didn't specify one (or it's
                // invalid), let's find the one that has the shortest path.
                if (FPushLength[4*pos+1] > 0) dir = 1; else
                if (FPushLength[4*pos+2] > 0) dir = 2; else
                if (FPushLength[4*pos+3] > 0) dir = 3; else
                if (FPushLength[4*pos+4] > 0) dir = 4; else return null;

                if (dir == 1 && FPushLength[4*pos+2] > 0 &&
                    (FPushLength[4*pos+2] < FPushLength[4*pos+1] ||
                     (FPushLength[4*pos+2] == FPushLength[4*pos+1] &&
                      FMoveLength[4*pos+2] < FMoveLength[4*pos+1])))
                    dir = 2;
                if (dir < 3 && FPushLength[4*pos+3] > 0 &&
                    (FPushLength[4*pos+3] < FPushLength[4*pos+dir] ||
                     (FPushLength[4*pos+3] == FPushLength[4*pos+dir] &&
                      FMoveLength[4*pos+3] < FMoveLength[4*pos+dir])))
                    dir = 3;
                if (dir < 4 && FPushLength[4*pos+4] > 0 &&
                    (FPushLength[4*pos+4] < FPushLength[4*pos+dir] ||
                     (FPushLength[4*pos+4] == FPushLength[4*pos+dir] &&
                      FMoveLength[4*pos+4] < FMoveLength[4*pos+dir])))
                    dir = 4;
            }

            int item = 4*pos + dir;

            // It is probably more efficient to just allocate an array that
            // will be a little bit too large in most cases, than to glue
            // several arrays into one just to have the right size.
            int[][] ret = new int [ FMoveLength[item] ][];
            int index = FMoveLength[item]-1;
            int pred = FPredecessor[item];
            int sx = FLevel.Width;
            while (pred != 0)
            {
                if ((pred-1) / 4 == (item-1) / 4) // moving the Sokoban around
                    ret[index] = FPath[pred][ (item-1) % 4 ];
                else    // pushing the piece
                {
                    ret[index] = new int[1];
                    int ndir = (item-1) % 4;
                    ret[index][0] = (ndir == 0) ? sx :
                                    (ndir == 1) ? 1 :
                                    (ndir == 2) ? -1 : -sx;
                }
                index--;
                item = pred;
                pred = FPredecessor[item];
            }
            int idir = (item-1) % 4;
            int rpos = (item-1) / 4 + ((idir == 0) ? -sx :
                                       (idir == 1) ? -1 :
                                       (idir == 2) ? 1 : sx);
            ret[index] = FMoveFinder.Path (rpos);
            return ret;
        }
    }
}
