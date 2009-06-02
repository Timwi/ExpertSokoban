using System.Collections.Generic;
using System.Drawing;
using RT.Util.Collections;
using System;

namespace ExpertSokoban
{
    /// <summary>
    /// Given a <see cref="SokobanLevel"/> and a selected piece, determines which empty cells
    /// the piece can be pushed to, and for each such cell, the optimal path (where "optimal"
    /// means "smallest number of pushes").
    /// </summary>
    public class PushFinder : Virtual2DArray<bool>
    {
        /// <summary>
        /// Stores the number of pushes required to push the piece to a specific
        /// destination cell with the Sokoban ending up on a specific cell adjacent
        /// to the destination cell of the piece. The array is layed out like this:
        /// element #0 = virtual start node;
        /// element #4*(y*sizex+x)+1 = node representing piece at (x,y) with Sokoban above;
        ///                       +2 = with Sokoban on the left;
        ///                       +3 = with Sokoban on the right;
        ///                       +4 = with Sokoban below.
        /// The value "0" means "infinity" (so we don't need to initialise the arrays).
        /// Values above zero mean "one less than this".
        /// </summary>
        private int[] _pushLength;

        /// <summary>
        /// Stores the number of moves required to push the piece to a specific
        /// destination cell with the Sokoban ending up on a specific cell adjacent
        /// to the destination cell of the piece. The array is layed out as explained
        /// in the documentation for <see cref="_pushLength"/>.
        /// </summary>
        private int[] _moveLength;

        /// <summary>
        /// For each node, stores the predecessor node, which is the node that "comes
        /// before it" in the path leading up to that node. A complete path can be
        /// extracted by following the predecessor from the desired destination node to
        /// the source node (node #0).
        /// </summary>
        private int[] _predecessor;

        /// <summary>
        /// Given a node (first index), which represents both a position for the piece
        /// and for the Sokoban, and given a direction from the piece (second index,
        /// values are 1 to 4), stores the move path necessary to get from the former
        /// node to the one where the piece is in the same place, but the Sokoban is in
        /// the cell that is adjacent to the piece in the specified direction.
        /// </summary>
        private Point[][][] _path;

        #region SpecialHeap class
        private class SpecialHeap
        {
            private class ThreeIntegers
            {
                private int _index, _pushLen, _moveLen;
                public ThreeIntegers(int index, int pushLen, int moveLen)
                { _index = index; _pushLen = pushLen; _moveLen = moveLen; }
                public int Index { get { return _index; } }
                public int PushLen { get { return _pushLen; } }
                public int MoveLen { get { return _moveLen; } }
            }
            private PushFinder _pushFinder;
            private ThreeIntegers[] _element;
            private int _size;

            public bool Empty { get { return _size == 0; } }

            public SpecialHeap(PushFinder pushFinder)
            {
                _pushFinder = pushFinder;
                _element = new ThreeIntegers[64];
                _size = 0;
            }

            private int compare(int xIndex, int yIndex)
            {
                ThreeIntegers x = _element[xIndex];
                ThreeIntegers y = _element[yIndex];
                if (x.PushLen < y.PushLen) return -1;
                if (x.PushLen > y.PushLen) return 1;
                if (x.MoveLen < y.MoveLen) return -1;
                if (x.MoveLen > y.MoveLen) return 1;
                return 0;
            }

            private void swap(int xIndex, int yIndex)
            {
                ThreeIntegers tmp = _element[xIndex];
                _element[xIndex] = _element[yIndex];
                _element[yIndex] = tmp;
            }

            private void reheapifyUp(int index)
            {
                int parent = (index - 1) / 2;
                while (index > 0 && compare(index, parent) < 0)
                {
                    swap(index, parent);
                    index = parent;
                    parent = (index - 1) / 2;
                }
            }
            private void reheapifyDown(int index)
            {
                while (index < _size / 2)
                {
                    // special case: only one child
                    if (_size % 2 == 0 && index == _size / 2 - 1)
                    {
                        if (compare(index, _size - 1) > 0)
                            swap(index, _size - 1);
                        return;
                    }
                    else
                    {
                        int child1 = 2 * index + 1;
                        int child2 = child1 + 1;
                        int childCompare = compare(child1, child2);
                        if (compare(child1, index) < 0 && childCompare <= 0)
                        {
                            swap(child1, index);
                            index = child1;
                        }
                        else if (compare(child2, index) < 0 && childCompare >= 0)
                        {
                            swap(child2, index);
                            index = child2;
                        }
                        else
                            return;
                    }
                }
            }

            // This means: Add the node with the index i in the arrays _pushLength and _moveLength.
            public void Add(int i)
            {
                if (_size == _element.Length)
                {
                    ThreeIntegers[] newArray = new ThreeIntegers[2 * _element.Length];
                    Array.Copy(_element, newArray, _element.Length);
                    _element = newArray;
                }
                _element[_size] = new ThreeIntegers(i, _pushFinder._pushLength[i], _pushFinder._moveLength[i]);
                _size++;
                reheapifyUp(_size - 1);
            }

            public int Extract()
            {
                if (_size == 0)
                    return -1;

                int result = _element[0].Index;
                _size--;
                _element[0] = _element[_size];
                reheapifyDown(0);
                return result;
            }
        };
        #endregion

        /// <summary>
        /// Remembers the <see cref="MoveFinder"/> passed in the constructor, which is for the initial
        /// level situation. It is required in order to construct the beginning of the path in <see cref="Path()"/>.
        /// </summary>
        private MoveFinder _moveFinder;

        /// <summary>
        /// The actual level we're working with. While Dijkstra's algorithm is running,
        /// the level will be modified temporarily (we might want to run a MoveFinder
        /// on a modified version of the level), but it will be returned to its original
        /// state before the constructor returns.
        /// </summary>
        private SokobanLevel _level;

        /// <summary>(read-only) Returns the width of the level.</summary>
        public int Width { get { return _level.Width; } }

        /// <summary>(read-only) Returns the height of the level.</summary>
        public int Height { get { return _level.Height; } }

        /// <summary>Main constructor. Runs the push finder.</summary>
        /// <param name="level">The Sokoban level under consideration.</param>
        /// <param name="selectedPiece">The co-ordinates of the currently selected piece
        /// (which is the source node for our algorithm).</param>
        /// <param name="moveFinder">The MoveFinder for the initial level situation.</param>
        public PushFinder(SokobanLevel level, Point selectedPiece, MoveFinder moveFinder)
        {
            _level = level;
            SpecialHeap priorityQueue = new SpecialHeap(this);
            Point origPiecePos = selectedPiece;
            Point origSokPos = _level.SokobanPos;
            int arraySize = 4 * level.Width * level.Height;
            _pushLength = new int[arraySize];
            _moveLength = new int[arraySize];
            _predecessor = new int[arraySize];
            PackedBooleans extracted = new PackedBooleans(arraySize);
            _path = new Point[arraySize][][];
            _moveFinder = moveFinder;

            // In Dijkstra's algorithm, you usually start with a priority queue containing only
            // the source node. Then the first iteration extracts that source node from the
            // priority queue again, relaxes all its edges, and inserts the newly-discovered
            // nodes. We will run this "first iteration" manually because our source node is
            // special and not like all the others. We will end up inserting up to four nodes
            // into the priority queue, and then we will proceed with the iterative algorithm.
            addIfValid(nodeIndex(origPiecePos, 1), new Point(origPiecePos.X, origPiecePos.Y - 1), priorityQueue);
            addIfValid(nodeIndex(origPiecePos, 2), new Point(origPiecePos.X - 1, origPiecePos.Y), priorityQueue);
            addIfValid(nodeIndex(origPiecePos, 3), new Point(origPiecePos.X + 1, origPiecePos.Y), priorityQueue);
            addIfValid(nodeIndex(origPiecePos, 4), new Point(origPiecePos.X, origPiecePos.Y + 1), priorityQueue);

            // Dijkstra's algorithm: We extract a node from our priority queue and relax
            // all its outgoing edges. However, we might not yet know the length of those
            // edges; we might have to invoke MoveFinder to determine them. We also don't
            // have all nodes in the priority queue; we are adding them as we discover them.
            while (!priorityQueue.Empty)
            {
                // Extract the next item and filter out duplicates.
                // Since reheapification is only possible when adding or extracting items,
                // not when items change their value, we simply add nodes to the priority
                // queue every time a node changes its value. This means some nodes will
                // have several copies in the priority queue, and we are only interested
                // in each node the first time it is extracted.
                int node = priorityQueue.Extract();
                if (extracted.Get(node)) continue;
                extracted.Set(node, true);

                // The item we have extracted represents a node in our graph
                // that we want to run Dijkstra's algorithm on. That node, in
                // turn, represents a situation in which:
                //  (1) the piece we are moving is at the position given by NodeToPos(node);
                Point position = nodeToPos(node);
                //  (2) the Sokoban is located:
                //          1 = above the piece, 2 = left, 3 = right, 4 = below
                //      as given by NodeToDir(node).
                int dirFrom = nodeToDir(node);

                // For the duration of this one iteration of the algorithm, we will
                // manipulate the level by placing the piece and the Sokoban in the
                // right place. We will undo this change at the end of the iteration.
                _level.MovePiece(origPiecePos, position);
                Point newSokPos = posDirToPos(position, dirFrom);
                _level.SetSokobanPos(newSokPos);

                // The node we're at has up to four possible outgoing edges.
                // These are:
                //  (1) up to three of the following four:
                //      (a) moving the Sokoban to above the piece
                //      (b) moving the Sokoban to left of the piece
                //      (c) moving the Sokoban to right of the piece
                //      (d) moving the Sokoban to below the piece
                //  (2) pushing the piece forward one cell.

                // We will first examine (1)(a-d). To do that, we need to find out
                // which of the other cells adjacent to the piece the Sokoban can move
                // to. We do this by running a MoveFinder.

                MoveFinder mf = new MoveFinder(_level, position);
                _path[node] = new Point[5][];

                // For any of these cells, mf.Path() will return null if you can't walk to it
                if (dirFrom != 1) _path[node][1] = mf.Path(new Point(position.X, position.Y - 1));
                if (dirFrom != 2) _path[node][2] = mf.Path(new Point(position.X - 1, position.Y));
                if (dirFrom != 3) _path[node][3] = mf.Path(new Point(position.X + 1, position.Y));
                if (dirFrom != 4) _path[node][4] = mf.Path(new Point(position.X, position.Y + 1));

                if (_path[node][1] != null) relaxEdge(node, nodeIndex(position, 1), 0, _path[node][1].Length, priorityQueue);
                if (_path[node][2] != null) relaxEdge(node, nodeIndex(position, 2), 0, _path[node][2].Length, priorityQueue);
                if (_path[node][3] != null) relaxEdge(node, nodeIndex(position, 3), 0, _path[node][3].Length, priorityQueue);
                if (_path[node][4] != null) relaxEdge(node, nodeIndex(position, 4), 0, _path[node][4].Length, priorityQueue);

                // Finally, consider possibility (2): pushing the piece.
                Point pushTo = posDirToPos(position, oppositeDir(dirFrom));
                if (_level.IsFree(pushTo))
                    relaxEdge(node, nodeIndex(pushTo, dirFrom), 1, 1, priorityQueue);

                // Before moving on to the next iteration, restore the level as it was.
                _level.MovePiece(position, origPiecePos);
                _level.SetSokobanPos(origSokPos);
            }
        }

        /// <summary>
        /// Determines whether the Sokoban can move from the source node to <paramref name="pos"/>, and if
        /// so, adds this as a newly-discovered node to the <paramref name="priorityQueue"/>. This is used
        /// only to add the first four items into the priority queue at the beginning of the algorithm.
        /// </summary>
        /// <param name="arrIndex">Index of the node the Sokoban is moving to.</param>
        /// <param name="pos">Co-ordinates of the cell the Sokoban is moving to.</param>
        /// <param name="priorityQueue">The priority queue.</param>
        private void addIfValid(int arrIndex, Point pos, SpecialHeap priorityQueue)
        {
            if (_moveFinder.Get(pos))
            {
                _pushLength[arrIndex] = 1;
                _moveLength[arrIndex] = _moveFinder.PathLength(pos) + 1;
                priorityQueue.Add(arrIndex);
            }
        }

        /// <summary>
        /// Relaxes an edge, which is Dijkstrian for: Given that going from <paramref name="fromNode"/> to <paramref name="toNode"/>
        /// requires <paramref name="pushLength"/> pushes and <paramref name="moveLength"/> moves, check if this gives rise to a
        /// shorter way to reach <paramref name="toNode"/> from the source node, and if so, update the node with the new path
        /// lengths and the new predecessor, and insert it into the priority queue.
        /// </summary>
        private void relaxEdge(int fromNode, int toNode, int pushLength, int moveLength, SpecialHeap priorityQueue)
        {
            if (
                // Either we haven't discovered this node yet...
                _pushLength[toNode] == 0 ||
                // ...or we can reduce its push length...
                _pushLength[toNode] > _pushLength[fromNode] + pushLength ||
                // ...or its push length is the same, but we can reduce the move length
                (_pushLength[toNode] == _pushLength[fromNode] + pushLength && _moveLength[toNode] > _moveLength[fromNode] + moveLength)
            )
            {
                _pushLength[toNode] = _pushLength[fromNode] + pushLength;
                _moveLength[toNode] = _moveLength[fromNode] + moveLength;
                _predecessor[toNode] = fromNode;
                priorityQueue.Add(toNode);
            }
        }

        /// <summary>
        /// Returns true if it is possible to push the selected piece to <paramref name="pos"/> in such a
        /// way that the Sokoban will end up adjacent to it in the direction given by <paramref name="direction"/>.
        /// </summary>
        public bool GetDir(Point pos, int direction)
        {
            return GetDir(pos.X, pos.Y, direction);
        }

        /// <summary>
        /// Returns true if it is possible to push the selected piece to the cell given by <paramref name="x"/>
        /// and <paramref name="y"/> in such a way that the Sokoban will end up adjacent to it in the
        /// direction given by <paramref name="direction"/>.
        /// </summary>
        public bool GetDir(int x, int y, int direction)
        {
            int index = 4 * (y * _level.Width + x) + direction;
            return index <= 0 || index >= _pushLength.Length ? false : _pushLength[index] > 0;
        }

        /// <summary>Returns true if it is possible to push the selected piece to <paramref name="pos"/>,
        /// irrespective of where the Sokoban will end up.</summary>
        public bool Get(Point pos)
        {
            return Get(pos.X, pos.Y);
        }

        /// <summary>
        /// Returns true if it is possible to push the selected piece to the cell given by <paramref name="x"/>
        /// and <paramref name="y"/>, irrespective of where the Sokoban will end up.</summary>
        public bool Get(int x, int y)
        {
            return GetDir(x, y, 1) || GetDir(x, y, 2) ||
                   GetDir(x, y, 3) || GetDir(x, y, 4);
        }

        /// <summary>
        /// Returns the path (sequence of Sokoban movements) that leads from the current situation (level in
        /// the constructor) to one where the selected piece (selectedPiece in the constructor) is moved to
        /// <paramref name="pos"/>. If <paramref name="preferDir"/> is between 1 and 4, and it is possible to
        /// push the piece in such a way that the Sokoban will end up above, left of, right of, or below the
        /// piece (1, 2, 3, 4, respectively), that sequence is returned, otherwise the most push-efficient one.
        /// </summary>
        public Point[] Path(Point pos, int preferDir)
        {
            if (pos.X < 0 || pos.Y < 0 || pos.X >= _level.Width || pos.Y >= _level.Height || !Get(pos))
                return null;

            int dir = 0;
            if ((preferDir > 0 && preferDir < 5) && _pushLength[nodeIndex(pos, preferDir)] > 0)
                dir = preferDir;
            else
            {
                // Find which of the four nodes has the shortest path
                dir =
                    _pushLength[nodeIndex(pos, 1)] > 0 ? 1 :
                    _pushLength[nodeIndex(pos, 2)] > 0 ? 2 :
                    _pushLength[nodeIndex(pos, 3)] > 0 ? 3 : 4;

                if (dir == 1 && _pushLength[nodeIndex(pos, 2)] > 0 &&
                    (_pushLength[nodeIndex(pos, 2)] < _pushLength[nodeIndex(pos, 1)] ||
                     (_pushLength[nodeIndex(pos, 2)] == _pushLength[nodeIndex(pos, 1)] &&
                      _moveLength[nodeIndex(pos, 2)] < _moveLength[nodeIndex(pos, 1)])))
                    dir = 2;
                if (dir < 3 && _pushLength[nodeIndex(pos, 3)] > 0 &&
                    (_pushLength[nodeIndex(pos, 3)] < _pushLength[nodeIndex(pos, dir)] ||
                     (_pushLength[nodeIndex(pos, 3)] == _pushLength[nodeIndex(pos, dir)] &&
                      _moveLength[nodeIndex(pos, 3)] < _moveLength[nodeIndex(pos, dir)])))
                    dir = 3;
                if (dir < 4 && _pushLength[nodeIndex(pos, 4)] > 0 &&
                    (_pushLength[nodeIndex(pos, 4)] < _pushLength[nodeIndex(pos, dir)] ||
                     (_pushLength[nodeIndex(pos, 4)] == _pushLength[nodeIndex(pos, dir)] &&
                      _moveLength[nodeIndex(pos, 4)] < _moveLength[nodeIndex(pos, dir)])))
                    dir = 4;
            }

            // The list will be created backwards, i.e. we start at the
            // node that represents the end-result and work backwards
            // (via _predecessor) to Node 0, the source node.
            List<Point> result = new List<Point>();
            int node = nodeIndex(pos, dir);
            int predecessor = _predecessor[node];
            while (predecessor != 0)
            {
                if (nodeToPos(predecessor) == nodeToPos(node))
                    // moving the Sokoban around
                    result.InsertRange(0, _path[predecessor][nodeToDir(node)]);
                else
                    // pushing the piece
                    result.Insert(0, posDirToPos(nodeToPos(node), nodeToDir(node)));

                node = predecessor;
                predecessor = _predecessor[node];
            }

            // moving the Sokoban to the piece at the beginning
            result.InsertRange(0, _moveFinder.Path(posDirToPos(nodeToPos(node), nodeToDir(node))));

            return result.ToArray();
        }

        /// <summary>
        /// Returns the co-ordinates of the cell in which the selected piece is located
        /// under the scenario represented by the node <paramref name="node"/>.
        /// </summary>
        private Point nodeToPos(int node)
        {
            int posIndex = (node - 1) / 4;
            return new Point(posIndex % _level.Width, posIndex / _level.Width);
        }

        /// <summary>
        /// Returns the direction in which the Sokoban is located relative to the selected
        /// piece under the scenario represented by the node <paramref name="node"/>.
        /// </summary>
        private int nodeToDir(int node)
        {
            return (node - 1) % 4 + 1;
        }

        /// <summary>Returns the direction that is opposite <paramref name="dir"/>.</summary>
        private int oppositeDir(int dir)
        {
            return 5 - dir;
        }

        /// <summary>
        /// Returns the index of the node that represents the scenario in which the selected piece is located
        /// at <paramref name="pos"/>, and the Sokoban is located above it (<paramref name="dir"/> = 1),
        /// left of it (<paramref name="dir"/> = 2), right of it (<paramref name="dir"/> = 3) or below it
        /// (<paramref name="dir"/> = 4).
        /// </summary>
        private int nodeIndex(Point pos, int dir)
        {
            return 4 * pos.Y * _level.Width + 4 * pos.X + dir;
        }

        /// <summary>
        /// Given a cell where the selected piece is located, and the direction in which
        /// the Sokoban is located relative to it, returns the location of the Sokoban.
        /// </summary>
        private Point posDirToPos(Point pos, int dir)
        {
            if (dir == 1) return new Point(pos.X, pos.Y - 1);
            if (dir == 2) return new Point(pos.X - 1, pos.Y);
            if (dir == 3) return new Point(pos.X + 1, pos.Y);
            return new Point(pos.X, pos.Y + 1);
        }
    }
}
