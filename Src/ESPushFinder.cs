using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using RT.Util;

namespace ExpertSokoban
{
    public class ESPushFinder
    {
        // FPushLength and FMoveLength are layed out like this:
        // element #0 = virtual start node
        // element #4*(y*sizex+x)+1 = node representing piece at (x,y) with Sokoban above
        //                       +2 = on the left
        //                       +3 = on the right
        //                       +4 = below
        // The value "0" means "infinity" (so we don't need to initialise the arrays).
        // Values above zero mean "one less than this".
        private int[] FPushLength;
        private int[] FMoveLength;

        // Maps from any node to the node that comes before it in any path
        private int[] FPredecessor;

        // For node a, path[a][b] is the sequence of moves to go from
        //  a = 4*(y*Width+x)+d to 4*(y*Width+x)+b+1.
        private int[][][] FPath;

        // FPredefinedPaths[d1][d2] contains several suggestions on how to get from dir d1 to dir d2
        private int[][][][] FPredefinedPaths;

        #region SpecialHeap class
        private class SpecialHeap
        {
            private class ThreeIntegers
            {
                private int FIndex, FPushLen, FMoveLen;
                public ThreeIntegers(int i, int p, int m)
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

            private int Compare(int POne, int PTwo)
            {
                ThreeIntegers One = FElement[POne];
                ThreeIntegers Two = FElement[PTwo];
                if (One.PushLen < Two.PushLen) return -1;
                if (One.PushLen > Two.PushLen) return 1;
                if (One.MoveLen < Two.MoveLen) return -1;
                if (One.MoveLen > Two.MoveLen) return 1;
                return 0;
            }

            private void Swap(int One, int Two)
            {
                ThreeIntegers tmp = FElement[One];
                FElement[One] = FElement[Two];
                FElement[Two] = tmp;
            }
            private void ReheapifyUp(int Index)
            {
                int Parent = (Index-1)/2;
                while (Index > 0 && Compare(Index, Parent) < 0)
                {
                    Swap(Index, Parent);
                    Index = Parent;
                    Parent = (Index-1)/2;
                }
            }
            private void ReheapifyDown(int Index)
            {
                while (Index < FNumElements/2)
                {
                    // special case: only one child
                    if (FNumElements % 2 == 0 && Index == FNumElements/2-1)
                    {
                        if (Compare(Index, FNumElements-1) > 0)
                            Swap(Index, FNumElements-1);
                        return;
                    }
                    else
                    {
                        int Child1 = 2*Index+1;
                        int Child2 = Child1+1;
                        int ChildCompare = Compare(Child1, Child2);
                        if (Compare(Child1, Index) < 0 && ChildCompare <= 0)
                        {
                            Swap(Child1, Index);
                            Index = Child1;
                        }
                        else if (Compare(Child2, Index) < 0 && ChildCompare >= 0)
                        {
                            Swap(Child2, Index);
                            Index = Child2;
                        }
                        else return;
                    }
                }
            }

            // This means: Add the node with the index i in the arrays FPushLength and FMoveLength.
            public void Add(int i)
            {
                if (FNumElements == FElement.Length)
                {
                    ThreeIntegers[] NewArray = new ThreeIntegers[2*FElement.Length];
                    for (int j = 0; j < FElement.Length; j++)
                        NewArray[j] = FElement[j];
                    FElement = NewArray;
                }
                FElement[FNumElements] = new ThreeIntegers(i, FParent.FPushLength[i], FParent.FMoveLength[i]);
                FNumElements++;
                ReheapifyUp(FNumElements-1);
            }
            public int Extract()
            {
                if (FNumElements > 0)
                {
                    int Result = FElement[0].Index;
                    FNumElements--;
                    FElement[0] = FElement[FNumElements];
                    ReheapifyDown(0);
                    return Result;
                }
                else return -1;
            }
        };
        #endregion

        // The priority queue that is central to Dijkstra's algorithm
        private SpecialHeap FPriorityQueue;
        // Remembers which nodes have already been extracted from the priority queue.
        // Since reheapification is only possible when adding or extracting items,
        // we simply add nodes to the priority queue every time a node changes its value.
        // This means some nodes will have several copies in the priority queue, and we
        // are only interested in each node the first time it is extracted.
        private PackedBooleans FExtracted;

        // This is the MoveFinder passed in the constructor, which is for the initial
        // level situation. We need it to construct the beginning of the path in Path().
        private ESMoveFinder FMoveFinder;

        // The actual level we're working with. While Dijkstra's algorithm is running,
        // the level will be modified temporarily (we might want to run an ESMoveFinder
        // on a modified version of the level), but it will be returned to its original
        // state before the constructor returns.
        private SokobanLevel FLevel;

        public ESPushFinder(SokobanLevel Level, int SelX, int SelY, ESMoveFinder MoveFinder)
        {
            FLevel = Level;
            FPriorityQueue = new SpecialHeap(this);
            int OrigPiecePos = SelY*FLevel.Width + SelX;
            int OrigSokPos = FLevel.SokobanPos;
            int ArraySize = 4 * Level.Width * Level.Height;
            FPushLength = new int[ArraySize];
            FMoveLength = new int[ArraySize];
            FPredecessor = new int[ArraySize];
            FExtracted = new PackedBooleans(ArraySize);
            FPath = new int[ArraySize][][];
            FMoveFinder = MoveFinder;

            FPredefinedPaths = new int[][][][]
            {
                // from above ...
                new int[][][] {
                    // to above...
                    null,
                    // to the left...
                    new int[][] { new int[] { -1, FLevel.Width }, new int[] { 1, FLevel.Width, FLevel.Width, -1, -1, -FLevel.Width }, new int[] { -FLevel.Width, -1, -1, FLevel.Width, FLevel.Width, 1 } },
                    // to the right...
                    new int[][] { new int[] { 1, FLevel.Width }, new int[] { -1, FLevel.Width, FLevel.Width, 1, 1, -FLevel.Width }, new int[] { -FLevel.Width, 1, 1, FLevel.Width, FLevel.Width, -1 } },
                    // to below...
                    new int[][] { new int[] { -1, FLevel.Width, FLevel.Width, 1 }, new int[] { 1, FLevel.Width, FLevel.Width, -1 } }
                },
                // from the left ...
                new int[][][] {
                    // to above...
                    new int[][] { new int[] { -FLevel.Width, 1 }, new int[] { FLevel.Width, 1, 1, -FLevel.Width, -FLevel.Width, -1 }, new int[] { -1, -FLevel.Width, -FLevel.Width, 1, 1, FLevel.Width } },
                    // to the left...
                    null,
                    // to the right...
                    new int[][] { new int[] { -FLevel.Width, 1, 1, FLevel.Width }, new int[] { FLevel.Width, 1, 1, -FLevel.Width } },
                    // to below...
                    new int[][] { new int[] { FLevel.Width, 1 }, new int[] { -FLevel.Width, 1, 1, FLevel.Width, FLevel.Width, -1 }, new int[] { -1, FLevel.Width, FLevel.Width, 1, 1, -FLevel.Width } }
                },
                // from the right ...
                new int[][][] {
                    // to above...
                    new int[][] { new int[] { -FLevel.Width, -1 }, new int[] { FLevel.Width, -1, -1, -FLevel.Width, -FLevel.Width, 1 }, new int[] { 1, -FLevel.Width, -FLevel.Width, -1, -1, FLevel.Width } },
                    // to the left...
                    new int[][] { new int[] { FLevel.Width, -1, -1, -FLevel.Width }, new int[] { -FLevel.Width, -1, -1, FLevel.Width } },
                    // to the right...
                    null,
                    // to below...
                    new int[][] { new int[] { FLevel.Width, -1 }, new int[] { -FLevel.Width, -1, -1, FLevel.Width, FLevel.Width, 1 }, new int[] { 1, FLevel.Width, FLevel.Width, -1, -1, -FLevel.Width } }
                },
                // from below ...
                new int[][][] {
                    // to above...
                    new int[][] { new int[] { -1, -FLevel.Width, -FLevel.Width, 1 }, new int[] { 1, -FLevel.Width, -FLevel.Width, -1 } },
                    // to the left...
                    new int[][] { new int[] { -1, -FLevel.Width }, new int[] { 1, -FLevel.Width, -FLevel.Width, -1, -1, FLevel.Width }, new int[] { FLevel.Width, -1, -1, -FLevel.Width, -FLevel.Width, 1 } },
                    // to the right...
                    new int[][] { new int[] { 1, -FLevel.Width }, new int[] { -1, -FLevel.Width, -FLevel.Width, 1, 1, FLevel.Width }, new int[] { FLevel.Width, 1, 1, -FLevel.Width, -FLevel.Width, -1 } },
                    // to below...
                    null
                },
            };

            AddIfValid(4*OrigPiecePos+1, OrigPiecePos-FLevel.Width);
            AddIfValid(4*OrigPiecePos+2, OrigPiecePos-1);
            AddIfValid(4*OrigPiecePos+3, OrigPiecePos+1);
            AddIfValid(4*OrigPiecePos+4, OrigPiecePos+FLevel.Width);

            // Dijkstra's algorithm: We extract a node from our priority queue and relax
            // all its outgoing edges. However, we might not yet know the length of those
            // edges; might have to invoke MoveFinder to determine them. We also don't have
            // all nodes in the priority queue; we are adding them as we discover them.

            while (!FPriorityQueue.Empty)
            {
                // extract the next item and filter out duplicates
                int Node = FPriorityQueue.Extract();
                if (FExtracted.Get(Node)) continue;
                FExtracted.Set(Node, true);

                // The item we have extracted represents a node in our tree
                // that we want to run Dijkstra's algorithm on. That node, in
                // turn, represents a situation in which:
                //  (1) the piece we are moving is at the position given by Position;
                //  (2) the Sokoban is located:
                //          0 = above the piece, 1 = left, 2 = right, 3 = below
                //      as given by DirFrom.
                int Position = (Node-1) / 4;
                int DirFrom = (Node-1) % 4;

                // For the duration of this one iteration of the algorithm, we will
                // manipulate the level by placing the piece and the Sokoban in the
                // right place. We will undo this change at the end of the iteration.
                FLevel.MovePiece(OrigPiecePos, Position);
                int NSokPos = PosDirToPos(Position, DirFrom);
                FLevel.SetSokobanPos(NSokPos);

                // The node we're at has up to four possible outgoing edges.
                // These are:
                //  (1) up to three of the following four:
                //      (a) moving the Sokoban to above the piece
                //      (b) moving the Sokoban to left of the piece
                //      (c) moving the Sokoban to right of the piece
                //      (d) moving the Sokoban to below the piece
                //  (2) pushing the piece along one cell.

                // We will first examine (1)(a-d). To do that, we need to find out
                // which of the other cells adjacent to the piece the Sokoban can move
                // to. For optimisation, we first check if there is a trivial path
                // (one of the paths predefined in FPredefinedPaths). If not, we will
                // run an ESMoveFinder.

                FPath[Node] = new int[4][];
                bool Stop = false;
                for (int DirTo = 0; DirTo < 4 && !Stop; DirTo++)
                {
                    if (DirTo != DirFrom)
                    {
                        int Cell = PosDirToPos(Position, DirTo);
                        if (FLevel.IsFree(Cell))
                        {
                            bool Found = false;

                            // now give each of the predefined paths a try.
                            for (int Predef = 0; Predef < FPredefinedPaths[DirFrom][DirTo].Length && !Found; Predef++)
                            {
                                int Pos = NSokPos;

                                // For as long as we can walk along the path, Found will remain true.
                                // If we can walk the entire path, Found will still be true at the end.
                                // If Found ever becomes false, we stop and run an ESMoveFinder.
                                Found = true;
                                for (int i = 0; i < FPredefinedPaths[DirFrom][DirTo][Predef].Length && Found; i++)
                                {
                                    Pos += FPredefinedPaths[DirFrom][DirTo][Predef][i];
                                    Found = FLevel.IsFree(Pos);
                                }
                                if (Found)  // We have found a path, so let's remember it.
                                    FPath[Node][DirTo] = FPredefinedPaths[DirFrom][DirTo][Predef];
                            }
                            // There is a free cell, but we couldn't find a path to it.
                            // Hence, no matter what we find for the other directions, we will have to
                            // run the ESMoveFinder anyway.
                            if (!Found)
                            {
                                ESMoveFinder mf = new ESMoveFinder(FLevel, Position);
                                // mf.Path() will return null if you can't walk to any of these
                                if (DirFrom != 0) FPath[Node][0] = mf.Path(Position - FLevel.Width);
                                if (DirFrom != 1) FPath[Node][1] = mf.Path(Position - 1);
                                if (DirFrom != 2) FPath[Node][2] = mf.Path(Position + 1);
                                if (DirFrom != 3) FPath[Node][3] = mf.Path(Position + FLevel.Width);
                                Stop = true;
                            }
                        }
                    }
                }

                if (FPath[Node][0] != null) RelaxEdge(Node, 4*Position+1, 0, FPath[Node][0].Length);
                if (FPath[Node][1] != null) RelaxEdge(Node, 4*Position+2, 0, FPath[Node][1].Length);
                if (FPath[Node][2] != null) RelaxEdge(Node, 4*Position+3, 0, FPath[Node][2].Length);
                if (FPath[Node][3] != null) RelaxEdge(Node, 4*Position+4, 0, FPath[Node][3].Length);

                // Finally, consider possibility (2): pushing the piece.
                int PosPushTo = PosDirToPos(Position, 3-DirFrom);
                if (FLevel.IsFree(PosPushTo))
                    RelaxEdge(Node, 4*PosPushTo + DirFrom + 1, 1, 1);

                // Before moving on to the next iteration, restore the level as it was.
                FLevel.MovePiece(Position, OrigPiecePos);
                FLevel.SetSokobanPos(OrigSokPos);
            }
        }

        public bool PushValid(int Pos, int Direction)
        {
            int Index = 4*Pos+Direction;
            return Index <= 0 || Index >= FPushLength.Length ? false : FPushLength[Index] > 0;
        }

        public bool PushValid(int Pos)
        {
            return PushValid(Pos, 1) || PushValid(Pos, 2) ||
                   PushValid(Pos, 3) || PushValid(Pos, 4);
        }

        // Returns the path (sequence of Sokoban movements) that lead from
        // the current situation (Level in the constructor) to one where
        // the selected piece (SelX and SelY in the constructor) is moved
        // to Pos. If PreferDir is between 1 and 4, and it is possible to
        // push the piece in such a way that the Sokoban will end up above,
        // left of, right of, or below the piece (1, 2, 3, 4, respectively),
        // that sequence is returned, otherwise the most push-efficient one.
        public List<int> Path(int Pos, int PreferDir)
        {
            if (Pos < 0 || 4*Pos+4 > FPushLength.Length || !PushValid(Pos))
                return null;

            int Dir = 0;
            if ((PreferDir > 0 && PreferDir < 5) && FPushLength[4*Pos+PreferDir] > 0)
                Dir = PreferDir;
            else
            {
                // Find which of the four nodes has the shortest path
                Dir =
                    FPushLength[4*Pos+1] > 0 ? 1 :
                    FPushLength[4*Pos+2] > 0 ? 2 :
                    FPushLength[4*Pos+3] > 0 ? 3 : 4;

                if (Dir == 1 && FPushLength[4*Pos+2] > 0 &&
                    (FPushLength[4*Pos+2] < FPushLength[4*Pos+1] ||
                     (FPushLength[4*Pos+2] == FPushLength[4*Pos+1] &&
                      FMoveLength[4*Pos+2] < FMoveLength[4*Pos+1])))
                    Dir = 2;
                if (Dir < 3 && FPushLength[4*Pos+3] > 0 &&
                    (FPushLength[4*Pos+3] < FPushLength[4*Pos+Dir] ||
                     (FPushLength[4*Pos+3] == FPushLength[4*Pos+Dir] &&
                      FMoveLength[4*Pos+3] < FMoveLength[4*Pos+Dir])))
                    Dir = 3;
                if (Dir < 4 && FPushLength[4*Pos+4] > 0 &&
                    (FPushLength[4*Pos+4] < FPushLength[4*Pos+Dir] ||
                     (FPushLength[4*Pos+4] == FPushLength[4*Pos+Dir] &&
                      FMoveLength[4*Pos+4] < FMoveLength[4*Pos+Dir])))
                    Dir = 4;
            }

            // The list will be created backwards, i.e. we start at the
            // node that represents the end-result and work backwards
            // (via FPredecessor) to Node 0, the source node.
            List<int> Result = new List<int>();
            int Node = 4*Pos + Dir;
            int Predecessor = FPredecessor[Node];
            while (Predecessor != 0)
            {
                if ((Predecessor-1) / 4 == (Node-1) / 4)
                    // moving the Sokoban around
                    for (int i = 0; i < FPath[Predecessor][(Node-1) % 4].Length; i++)
                        Result.Insert(i, FPath[Predecessor][(Node-1) % 4][i]);
                else
                    // pushing the piece
                    Result.Insert(0, PosDirToPos(0, 3 - (Node-1) % 4));

                Node = Predecessor;
                Predecessor = FPredecessor[Node];
            }
            
            // moving the Sokoban from Node 0 to the piece
            int[] FirstPath = FMoveFinder.Path(PosDirToPos((Node-1)/4, (Node-1)%4));
            for (int i = 0; i < FirstPath.Length; i++)
                Result.Insert(i, FirstPath[i]);
            
            return Result;
        }

        // Used to add the first four items into the priority queue at the 
        // beginning of the algorithm.
        private void AddIfValid(int ArrIndex, int Pos)
        {
            if (FMoveFinder.MoveValid(Pos))
            {
                FPushLength[ArrIndex] = 1;
                FMoveLength[ArrIndex] = FMoveFinder.PathLength(Pos)+1;
                FPriorityQueue.Add(ArrIndex);
            }
        }

        // Relax an edge, which is Dijkstrian for: check if we have found a
        // shorter way to reach ToNode, and if so, update the node with
        // the new path lengths and the new predecessor (FromNode), and
        // insert it into the priority queue.
        private void RelaxEdge(int FromNode, int ToNode, int PushLength, int MoveLength)
        {
            if (
                // Either we haven't discovered this node yet...
                FPushLength[ToNode] == 0 ||
                // ...or we can reduce its push length...
                FPushLength[ToNode] > FPushLength[FromNode] + PushLength ||
                // ...or its push length is the same, but we can reduce the move length
                (FPushLength[ToNode] == FPushLength[FromNode] + PushLength && FMoveLength[ToNode] > FMoveLength[FromNode] + MoveLength)
            )
            {
                FPushLength[ToNode] = FPushLength[FromNode] + PushLength;
                FMoveLength[ToNode] = FMoveLength[FromNode] + MoveLength;
                FPredecessor[ToNode] = FromNode;
                FPriorityQueue.Add(ToNode);
            }
        }

        private int PosDirToPos(int Pos, int Dir)
        {
            if (Dir == 0) return Pos - FLevel.Width;
            if (Dir == 1) return Pos - 1;
            if (Dir == 2) return Pos + 1;
            return Pos + FLevel.Width;
        }
    }
}
