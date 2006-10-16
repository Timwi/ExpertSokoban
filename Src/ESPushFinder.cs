using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ExpertSokoban
{
    public class ESPushFinder 
    {
        private SokobanLevel level;

        // These arrays are layed out like this:
        // element #0 = virtual start node
        // element #4*(y*sizex+x)+1 = node representing piece at (x,y) with Sokoban above
        //                       +2 = on the left
        //                       +3 = on the right
        //                       +4 = below
        // The value "0" means "infinity" (so we don't need to initialise the arrays).
        // Values above zero mean "one less than this".
        private int[] pushLength;
        private int[] moveLength;
        private int[] predecessor;
        private ESPackedBooleans extracted;
        private Control callbackOwner;
        private Delegate callbackFound, callbackDone;

        // For node a, path[a][b] is the sequence of moves to go from
        //  a = 4*(y*sizex+x)+d to 4*(y*sizex+x)+b+1.
        private int[][][] path;

        // For each node, the walking distance (plus one) to another side (above/right/below/left).
        // Not currently used. Should be used for performance optimisation later.
        // private int[][] edgeWeights;

        private bool done, running;
        private int selX, selY, selPos, // the original position of the piece we are actually pushing.
                    sokPos, numPossibilities;
        private ESMoveFinder initialMoveFinder;

        // predefinedPaths[d1][d2] contains several suggestions on how to get from dir d1 to dir d2
        private int[][][][] predefinedPaths;

        class SpecialHeap
        {
            class ThreeIntegers
            {
                private int index, pushLen, moveLen;
                public ThreeIntegers (int i, int p, int m)
                { index = i; pushLen = p; moveLen = m; }
                public int getIndex() { return index; }
                public int getPushLen() { return pushLen; }
                public int getMoveLen() { return moveLen; }
            }
            private ESPushFinder parent;
            private ThreeIntegers[] element;
            private int numElements;

            public SpecialHeap(ESPushFinder par)
            {
                parent = par;
                element = new ThreeIntegers[64];
                numElements = 0;
            }

            private String printify (ThreeIntegers[] arr, int num)
            {
                if (arr == null) return "null";
                if (arr.Length < 1 || num < 1) return "[]";
                String result = "[(" + arr[0].getIndex() + "/" + arr[0].getPushLen() + ")";
                for (int i = 1; i < num; i++)
                    result += ", (" + arr[i].getIndex() + "/" + arr[i].getPushLen() + ")";
                return result + "]";
            }
            /*
            private String printify2 (int[][] arr, int num)
            {
                if (arr == null) return "null";
                if (arr.length < 1 || num < 1) return "[]";
                String result = "[" + printify (arr[0]);
                for (int i = 1; i < num; i++)
                    result += ", " + printify (arr[i]);
                return result + "]";
            }
            */

            private int compare (int pOne, int pTwo)
            {
                ThreeIntegers one = element[pOne];
                ThreeIntegers two = element[pTwo];
                if (one.getPushLen() < two.getPushLen()) return -1;
                if (one.getPushLen() > two.getPushLen()) return 1;
                if (one.getMoveLen() < two.getMoveLen()) return -1;
                if (one.getMoveLen() > two.getMoveLen()) return 1;
                return 0;
            }

            private void swap (int one, int two)
            {
                ThreeIntegers tmp = element[one];
                element[one] = element[two];
                element[two] = tmp;
            }
            private void reheapifyUp (int index)
            {
                int parent = (index-1)/2;
                while (index > 0 && compare (index, parent) < 0)
                {
                    swap (index, parent);
                    index = parent;
                    parent = (index-1)/2;
                }
            }
            private void reheapifyDown (int index)
            {
                while (index < numElements/2)
                {
                    // special case: only one child
                    if (numElements % 2 == 0 && index == numElements/2-1)
                    {
                        if (compare (index, numElements-1) > 0)
                            swap (index, numElements-1);
                        return;
                    }
                    else
                    {
                        int child1 = 2*index+1;
                        int child2 = child1+1;
                        int ccc = compare (child1, child2);
                        if (compare (child1, index) < 0 && ccc <= 0)
                        {
                            swap (child1, index);
                            index = child1;
                        }
                        else if (compare (child2, index) < 0 && ccc >= 0)
                        {
                            swap (child2, index);
                            index = child2;
                        }
                        else return;
                    }
                }
            }

            // This means: Add the node with the index i in the arrays "pushLength"/"moveLength".
            public void add (int i)
            {
                if (numElements == element.Length)
                {
                    ThreeIntegers[] newArray = new ThreeIntegers [ 2*element.Length ];
                    for (int j = 0; j < element.Length; j++)
                        newArray[j] = element[j];
                    element = newArray;
                }
                element [ numElements ] = new ThreeIntegers (i, parent.pushLength[i], parent.moveLength[i]);
                numElements++;
                reheapifyUp (numElements-1);
            }
            public int extract()
            {
                if (numElements > 0)
                {
                    int ret = element[0].getIndex();
                    numElements--;
                    element[0] = element [ numElements ];
                    reheapifyDown (0);
                    return ret;
                }
                else return -1;
            }
            public bool isEmpty() { return numElements == 0; }
        };

        private SpecialHeap priorityQueue;

        public ESPushFinder(SokobanLevel l, int x, int y, Control Owner, Delegate FoundCallback, 
            Delegate DoneCallback, ESMoveFinder curMF)
        {
            callbackOwner = Owner;
            callbackFound = FoundCallback;
            callbackDone = DoneCallback;
            level = l;
            selX = x;
            selY = y;
            int sx = l.getSizeX();
            selPos = y*sx + x;
            sokPos = l.getSokobanPos();
            done = false;
            priorityQueue = new SpecialHeap(this);
            int array_size = 4 * sx * l.getSizeY();
            pushLength = new int[array_size];
            moveLength = new int[array_size];
            predecessor = new int[array_size];
            extracted = new ESPackedBooleans (array_size);
            path = new int[array_size][][];
            numPossibilities = 0;

            predefinedPaths = new int[][][][]
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

            if (curMF.moveValid (j))
            {
                pushLength[i] = 1;
                moveLength[i] = curMF.getPathLength (j)+1;
                priorityQueue.add (i);
            }
            i++;
            j += sx-1;
            if (curMF.moveValid (j))
            {
                pushLength[i] = 1;
                moveLength[i] = curMF.getPathLength (j)+1;
                priorityQueue.add (i);
            }
            i++;
            j += 2;
            if (curMF.moveValid (j))
            {
                pushLength[i] = 1;
                moveLength[i] = curMF.getPathLength (j)+1;
                priorityQueue.add (i);
            }
            i++;
            j += sx-1;
            if (curMF.moveValid (j))
            {
                pushLength[i] = 1;
                moveLength[i] = curMF.getPathLength (j)+1;
                priorityQueue.add (i);
            }

            pushLength[0] = 1;
            if (priorityQueue.isEmpty())
            {
                callbackOwner.Invoke(callbackDone, new object[] { false });
                done = true;
            }

            initialMoveFinder = curMF;
        }

        public void SingleStep()
        {
            if (done) return;

            // extract the next item, filter out duplicates, quit if priority queue is empty
            int item;
            do
            {
                if (priorityQueue.isEmpty())
                {
                    done = true;
                    callbackOwner.Invoke(callbackDone, new object[] { numPossibilities > 0 });
                    return;
                }
                item = priorityQueue.extract();
            }
            while (extracted.get (item));

            extracted.set (item, true);

            // Dijkstra's algorithm: We extract a node from our priority queue and relax
            // all its outgoing edges. However, we might not yet know the length of those
            // edges; might have to invoke MoveFinder to determine them. We also don't have
            // all nodes in the priority queue; we are adding them as we discover them.

            int direction = (item-1) % 4;
                // 0 = Sokoban above piece, 1 = left, 2 = right, 3 = below
            int position = (item-1) / 4;
            level.movePiece (selPos, position);
            int sx = level.getSizeX();
            int nSokPos = position +
                ((direction == 0) ? -sx :
                 (direction == 1) ? -1:
                 (direction == 2) ? 1 : sx);
            level.setSokobanPos (nSokPos);

            // Now try to find out which of the other squares adjacent to the piece
            // the Sokoban can move to.  For optimisation, we first check if there
            // is a trivial path (one of the paths predefined in predefinedPaths).
            // If not, we will run a BSMoveFinder.

            path[item] = new int[4][];

            bool stop = false;
            for (int consider_direction = 0; consider_direction < 4 && !stop; consider_direction++)
            {
                if (consider_direction != direction)
                {
                    int consider_spot = position + ((consider_direction == 0) ? -sx :
                                                    (consider_direction == 1) ? -1:
                                                    (consider_direction == 2) ? 1 : sx);
                    if (level.isFree (consider_spot))
                    {
                        bool found = false;

                        // now give each of the predefined paths a try.
                        for (int predef = 0; predef < predefinedPaths[direction][consider_direction].Length && !found; predef++)
                        {
                            int curPos = nSokPos;

                            // This might seem a bit weird, but it works as follows: for as long as
                            // we can walk along the path, "found" will be true.  Hence, if we can
                            // walk the entire path, "found" will still be true at the end, which is
                            // what we want, because it means we have found a path.
                            found = true;
                            for (int i = 0; i < predefinedPaths[direction][consider_direction][predef].Length && found; i++)
                            {
                                curPos += predefinedPaths[direction][consider_direction][predef][i];
                                found = level.isFree (curPos);
                            }
                            if (found)  // We have found a path, so let's remember it.
                                path[item][consider_direction] = predefinedPaths[direction][consider_direction][predef];
                        }
                        if (!found)  // There is a free space, but we couldn't find a path to it.
                        // Hence, no matter what we find for the other directions, we will have to
                        // run the BSMoveFinder anyway.
                        {
                            ESMoveFinder mf = new ESMoveFinder(level, true, null, null, null, position);
                            mf.SingleStep();

                            // mf.getPath will return null if you can't walk to any of these
                            if (direction != 0) path[item][0] = mf.getPath (position - sx);
                            if (direction != 1) path[item][1] = mf.getPath (position - 1);
                            if (direction != 2) path[item][2] = mf.getPath (position + 1);
                            if (direction != 3) path[item][3] = mf.getPath (position + sx);
                            stop = true;
                        }
                    }
                }
            }

            int index = 4*position + 1;

            // If we can move above the piece (this is automatically null if we are already there)
            if (path[item][0] != null)
            {
                // ... then consider the edge leading to that position.
                if (pushLength[index] == 0
                    || pushLength[index] > pushLength[item]
                    || (pushLength[index] == pushLength[item]
                        && moveLength[index] > moveLength[item] + path[item][0].Length))
                {
                    pushLength[index] = pushLength[item];
                    moveLength[index] = moveLength[item] + path[item][0].Length;
                    predecessor[index] = item;
                    priorityQueue.add (index);
                }
            }
            // Left...
            index++;
            if (path[item][1] != null)
            {
                if (pushLength[index] == 0
                    || pushLength[index] > pushLength[item]
                    || (pushLength[index] == pushLength[item]
                        && moveLength[index] > moveLength[item] + path[item][1].Length))
                {
                    pushLength[index] = pushLength[item];
                    moveLength[index] = moveLength[item] + path[item][1].Length;
                    predecessor[index] = item;
                    priorityQueue.add (index);
                }
            }
            // Right...
            index++;
            if (path[item][2] != null)
            {
                if (pushLength[index] == 0
                    || pushLength[index] > pushLength[item]
                    || (pushLength[index] == pushLength[item]
                        && moveLength[index] > moveLength[item] + path[item][2].Length))
                {
                    pushLength[index] = pushLength[item];
                    moveLength[index] = moveLength[item] + path[item][2].Length;
                    predecessor[index] = item;
                    priorityQueue.add (index);
                }
            }
            // Below...
            index++;
            if (path[item][3] != null)
            {
                if (pushLength[index] == 0
                    || pushLength[index] > pushLength[item]
                    || (pushLength[index] == pushLength[item]
                        && moveLength[index] > moveLength[item] + path[item][3].Length))
                {
                    pushLength[index] = pushLength[item];
                    moveLength[index] = moveLength[item] + path[item][3].Length;
                    predecessor[index] = item;
                    priorityQueue.add (index);
                }
            }

            // Finally, also consider actually pushing the piece.
            bool aFound = false;
            int nPushTo = position +
                ((direction == 0) ? sx :
                 (direction == 1) ? 1:
                 (direction == 2) ? -1 : -sx);
            if (level.isFree (nPushTo))
            {
                index = 4*nPushTo + direction + 1;
                if (pushLength[index] == 0
                    || pushLength[index] > pushLength[item]+1
                    || (pushLength[index] == pushLength[item]+1
                        && moveLength[index] > moveLength[item]+1))
                {
                    pushLength[index] = pushLength[item]+1;
                    moveLength[index] = moveLength[item]+1;
                    predecessor[index] = item;
                    priorityQueue.add (index);

                    // We have to defer calling pushThreadFound because we
                    // have made changes to the level...
                    aFound = true;
                }
            }

            // restore the level as it was
            level.movePiece (position, selPos);
            level.setSokobanPos (sokPos);
            // now we can call pushThreadFound
            if (aFound)
            {
                callbackOwner.Invoke(callbackFound, new object[] { nPushTo });
                numPossibilities++;
            }

            if (priorityQueue.isEmpty())
            {
                done = true;
                callbackOwner.Invoke(callbackDone, new object[] { numPossibilities >0 });
            }
        }
        public bool pushValid (int pos, int posInDir) {
            int index = 4*pos+posInDir;
            return index <= 0 || index >= pushLength.Length ? false : pushLength[4*pos+posInDir] > 0; 
        }
        public bool pushValid (int pos)
        {
            return  pushValid (pos, 1) || pushValid (pos, 2) ||
                    pushValid (pos, 3) || pushValid (pos, 4);
        }
        public int getMoveLength (int pos) { return pushLength[pos]; }
        public int getPushLength (int pos) { return moveLength[pos]; }
        public void started() { running = true; }
        public void stopped() { running = false; }
        public bool isRunning() { return running; }
        public bool isDone() { return done; }

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
            if (pos < 0 || 4*pos+4 > pushLength.Length)
                return null;
            int dir = 0;

            if ((posInDir > 0 && posInDir < 5) && pushLength[4*pos+posInDir] > 0)
                dir = posInDir;
            else
            {
                // "pos" is the position where we want to move the piece.
                // There are four nodes in the graph that can represent
                // this position. If the user didn't specify one (or it's
                // invalid), let's find the one that has the shortest path.
                if (pushLength[4*pos+1] > 0) dir = 1; else
                if (pushLength[4*pos+2] > 0) dir = 2; else
                if (pushLength[4*pos+3] > 0) dir = 3; else
                if (pushLength[4*pos+4] > 0) dir = 4; else return null;

                if (dir == 1 && pushLength[4*pos+2] > 0 &&
                    (pushLength[4*pos+2] < pushLength[4*pos+1] ||
                     (pushLength[4*pos+2] == pushLength[4*pos+1] &&
                      moveLength[4*pos+2] < moveLength[4*pos+1])))
                    dir = 2;
                if (dir < 3 && pushLength[4*pos+3] > 0 &&
                    (pushLength[4*pos+3] < pushLength[4*pos+dir] ||
                     (pushLength[4*pos+3] == pushLength[4*pos+dir] &&
                      moveLength[4*pos+3] < moveLength[4*pos+dir])))
                    dir = 3;
                if (dir < 4 && pushLength[4*pos+4] > 0 &&
                    (pushLength[4*pos+4] < pushLength[4*pos+dir] ||
                     (pushLength[4*pos+4] == pushLength[4*pos+dir] &&
                      moveLength[4*pos+4] < moveLength[4*pos+dir])))
                    dir = 4;
            }

            int item = 4*pos + dir;

            // It is probably more efficient to just allocate an array that
            // will be a little bit too large in most cases, than to glue
            // several arrays into one just to have the right size.
            int[][] ret = new int [ moveLength[item] ][];
            int index = moveLength[item]-1;
            int pred = predecessor[item];
            int sx = level.getSizeX();
            while (pred != 0)
            {
                if ((pred-1) / 4 == (item-1) / 4) // moving the Sokoban around
                    ret[index] = path[pred][ (item-1) % 4 ];
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
                pred = predecessor[item];
            }
            int idir = (item-1) % 4;
            int rpos = (item-1) / 4 + ((idir == 0) ? -sx :
                                       (idir == 1) ? -1 :
                                       (idir == 2) ? 1 : sx);
            ret[index] = initialMoveFinder.getPath (rpos);
            return ret;
        }
    }
}
