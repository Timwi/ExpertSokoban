using System.Collections.Generic;
using System.Drawing;
using RT.Util.Collections;

namespace ExpertSokoban
{
    /// <summary>
    /// Given a SokobanLevel and a selected piece, finds out (using Dijsktra's
    /// Algorithm) what empty cells the piece can be pushed to, and for each such
    /// cell, the optimal path (where "optimal" means "least number of pushes").
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
        private int[] FPushLength;

        /// <summary>
        /// Stores the number of moves required to push the piece to a specific
        /// destination cell with the Sokoban ending up on a specific cell adjacent
        /// to the destination cell of the piece. The array is layed out as explained
        /// in the documentation for FPushLength.
        /// </summary>
        private int[] FMoveLength;

        /// <summary>
        /// For each node, stores the predecessor node, which is the node that "comes
        /// before it" in the path leading up to that node. A complete path can be
        /// extracted by following the precessor from the desired destination node to
        /// the source node (node #0).
        /// </summary>
        private int[] FPredecessor;

        /// <summary>
        /// Given a node (first index), which represents both a position for the piece
        /// and for the Sokoban, and given a direction from the piece (second index,
        /// values are 1 to 4), stores the move path necessary to get from the former
        /// node to the one where the piece is in the same place, but the Sokoban is in
        /// the cell that is adjacent to the piece in the specified direction.
        /// </summary>
        private Point[][][] FPath;

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
            private PushFinder FParent;
            private ThreeIntegers[] FElement;
            private int FNumElements;

            public bool Empty { get { return FNumElements == 0; } }

            public SpecialHeap(PushFinder Parent)
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

        /// <summary>
        /// Remembers the MoveFinder passed in the constructor, which is for the initial
        /// level situation. It is required in order to construct the beginning of the
        /// path in Path().
        /// </summary>
        private MoveFinder FMoveFinder;

        /// <summary>
        /// The actual level we're working with. While Dijkstra's algorithm is running,
        /// the level will be modified temporarily (we might want to run a MoveFinder
        /// on a modified version of the level), but it will be returned to its original
        /// state before the constructor returns.
        /// </summary>
        private SokobanLevel FLevel;

        /// <summary>
        /// (read-only) Returns the width of the level.
        /// </summary>
        public int Width { get { return FLevel.Width; } }

        /// <summary>
        /// (read-only) Returns the height of the level.
        /// </summary>
        public int Height { get { return FLevel.Height; } }

        /// <summary>
        /// Main constructor. Runs the push finder.
        /// </summary>
        /// <param name="Level">The Sokoban level under consideration.</param>
        /// <param name="SelectedPiece">The co-ordinates of the currently selected piece
        /// (which is the source node for our algorithm).</param>
        /// <param name="MoveFinder">The MoveFinder for the initial level situation.</param>
        public PushFinder(SokobanLevel Level, Point SelectedPiece, MoveFinder MoveFinder)
        {
            FLevel = Level;
            SpecialHeap PriorityQueue = new SpecialHeap(this);
            Point OrigPiecePos = SelectedPiece;
            Point OrigSokPos = FLevel.SokobanPos;
            int ArraySize = 4 * Level.Width * Level.Height;
            FPushLength = new int[ArraySize];
            FMoveLength = new int[ArraySize];
            FPredecessor = new int[ArraySize];
            PackedBooleans Extracted = new PackedBooleans(ArraySize);
            FPath = new Point[ArraySize][][];
            FMoveFinder = MoveFinder;

            // In Dijkstra's algorithm, you usually start with a priority queue containing only
            // the source node. Then the first iteration extracts that source node from the
            // priority queue again, relaxes all its edges, and inserts the newly-discovered
            // nodes. We will run this "first iteration" manually because our source node is
            // special and not like all the others. We will end up inserting up to four nodes
            // into the priority queue, and then we will proceed with the iterative algorithm.
            AddIfValid(NodeIndex(OrigPiecePos, 1), new Point(OrigPiecePos.X, OrigPiecePos.Y-1), PriorityQueue);
            AddIfValid(NodeIndex(OrigPiecePos, 2), new Point(OrigPiecePos.X-1, OrigPiecePos.Y), PriorityQueue);
            AddIfValid(NodeIndex(OrigPiecePos, 3), new Point(OrigPiecePos.X+1, OrigPiecePos.Y), PriorityQueue);
            AddIfValid(NodeIndex(OrigPiecePos, 4), new Point(OrigPiecePos.X, OrigPiecePos.Y+1), PriorityQueue);

            // Dijkstra's algorithm: We extract a node from our priority queue and relax
            // all its outgoing edges. However, we might not yet know the length of those
            // edges; we might have to invoke MoveFinder to determine them. We also don't
            // have all nodes in the priority queue; we are adding them as we discover them.
            while (!PriorityQueue.Empty)
            {
                // Extract the next item and filter out duplicates.
                // Since reheapification is only possible when adding or extracting items,
                // not when items change their value, we simply add nodes to the priority
                // queue every time a node changes its value. This means some nodes will
                // have several copies in the priority queue, and we are only interested
                // in each node the first time it is extracted.
                int Node = PriorityQueue.Extract();
                if (Extracted.Get(Node)) continue;
                Extracted.Set(Node, true);

                // The item we have extracted represents a node in our graph
                // that we want to run Dijkstra's algorithm on. That node, in
                // turn, represents a situation in which:
                //  (1) the piece we are moving is at the position given by Position;
                //  (2) the Sokoban is located:
                //          1 = above the piece, 2 = left, 3 = right, 4 = below
                //      as given by DirFrom.
                Point Position = NodeToPos(Node);
                int DirFrom = NodeToDir(Node);

                // For the duration of this one iteration of the algorithm, we will
                // manipulate the level by placing the piece and the Sokoban in the
                // right place. We will undo this change at the end of the iteration.
                FLevel.MovePiece(OrigPiecePos, Position);
                Point NewSokPos = PosDirToPos(Position, DirFrom);
                FLevel.SetSokobanPos(NewSokPos);

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

                MoveFinder mf = new MoveFinder(FLevel, Position);
                FPath[Node] = new Point[5][];

                // For any of these cells, mf.Path() will return null if you can't walk to it
                if (DirFrom != 1) FPath[Node][1] = mf.Path(new Point(Position.X, Position.Y-1));
                if (DirFrom != 2) FPath[Node][2] = mf.Path(new Point(Position.X-1, Position.Y));
                if (DirFrom != 3) FPath[Node][3] = mf.Path(new Point(Position.X+1, Position.Y));
                if (DirFrom != 4) FPath[Node][4] = mf.Path(new Point(Position.X, Position.Y+1));

                if (FPath[Node][1] != null) RelaxEdge(Node, NodeIndex(Position, 1), 0, FPath[Node][1].Length, PriorityQueue);
                if (FPath[Node][2] != null) RelaxEdge(Node, NodeIndex(Position, 2), 0, FPath[Node][2].Length, PriorityQueue);
                if (FPath[Node][3] != null) RelaxEdge(Node, NodeIndex(Position, 3), 0, FPath[Node][3].Length, PriorityQueue);
                if (FPath[Node][4] != null) RelaxEdge(Node, NodeIndex(Position, 4), 0, FPath[Node][4].Length, PriorityQueue);

                // Finally, consider possibility (2): pushing the piece.
                Point PushTo = PosDirToPos(Position, OppositeDir(DirFrom));
                if (FLevel.IsFree(PushTo))
                    RelaxEdge(Node, NodeIndex(PushTo, DirFrom), 1, 1, PriorityQueue);

                // Before moving on to the next iteration, restore the level as it was.
                FLevel.MovePiece(Position, OrigPiecePos);
                FLevel.SetSokobanPos(OrigSokPos);
            }
        }

        /// <summary>
        /// Determines whether the Sokoban can move from the source node to Pos, and if
        /// so, adds this as a newly-discovered node to the PriorityQueue. This is used
        /// only to add the first four items into the priority queue at the beginning of
        /// the algorithm.
        /// </summary>
        /// <param name="ArrIndex">Index of the node the Sokoban is moving to.</param>
        /// <param name="Pos">Co-ordinates of the cell the Sokoban is moving to.</param>
        /// <param name="PriorityQueue">The priority queue.</param>
        private void AddIfValid(int ArrIndex, Point Pos, SpecialHeap PriorityQueue)
        {
            if (FMoveFinder.Get(Pos))
            {
                FPushLength[ArrIndex] = 1;
                FMoveLength[ArrIndex] = FMoveFinder.PathLength(Pos)+1;
                PriorityQueue.Add(ArrIndex);
            }
        }

        /// <summary>
        /// Relaxes an edge, which is Dijkstrian for: Given that going from FromNode to
        /// ToNode requires PushLength pushes and MoveLength moves, check if this gives
        /// rise to a shorter way to reach ToNode from the source node, and if so,
        /// update the node with the new path lengths and the new predecessor, and
        /// insert it into the priority queue.
        /// </summary>
        private void RelaxEdge(int FromNode, int ToNode, int PushLength, int MoveLength, SpecialHeap PriorityQueue)
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
                PriorityQueue.Add(ToNode);
            }
        }

        /// <summary>
        /// Returns true if it is possible to push the selected piece to Pos in such a
        /// way that the Sokoban will end up adjacent to it in the direction Direction.
        /// </summary>
        public bool GetDir(Point Pos, int Direction)
        {
            return GetDir(Pos.X, Pos.Y, Direction);
        }

        /// <summary>
        /// Returns true if it is possible to push the selected piece to the cell given
        /// by x and y in such a way that the Sokoban will end up adjacent to it in the
        /// direction Direction.
        /// </summary>
        public bool GetDir(int x, int y, int Direction)
        {
            int Index = 4*(y*FLevel.Width + x) + Direction;
            return Index <= 0 || Index >= FPushLength.Length ? false : FPushLength[Index] > 0;
        }

        /// <summary>
        /// Returns true if it is possible to push the selected piece to Pos,
        /// irrespective of where the Sokoban will end up.
        /// </summary>
        public bool Get(Point Pos)
        {
            return Get(Pos.X, Pos.Y);
        }

        /// <summary>
        /// Returns true if it is possible to push the selected piece to the cell given
        /// by x and y, irrespective of where the Sokoban will end up.
        /// </summary>
        public bool Get(int x, int y)
        {
            return GetDir(x, y, 1) || GetDir(x, y, 2) ||
                   GetDir(x, y, 3) || GetDir(x, y, 4);
        }

        /// <summary>
        /// Returns the path (sequence of Sokoban movements) that lead from the current
        /// situation (Level in the constructor) to one where the selected piece
        /// (SelectedPiece in the constructor) is moved to Pos. If PreferDir is between
        /// 1 and 4, and it is possible to push the piece in such a way that the Sokoban
        /// will end up above, left of, right of, or below the piece (1, 2, 3, 4,
        /// respectively), that sequence is returned, otherwise the most push-efficient
        /// one.
        /// </summary>
        public Point[] Path(Point Pos, int PreferDir)
        {
            if (Pos.X < 0 || Pos.Y < 0 || Pos.X >= FLevel.Width || Pos.Y >= FLevel.Height || !Get(Pos))
                return null;

            int Dir = 0;
            if ((PreferDir > 0 && PreferDir < 5) && FPushLength[NodeIndex(Pos, PreferDir)] > 0)
                Dir = PreferDir;
            else
            {
                // Find which of the four nodes has the shortest path
                Dir =
                    FPushLength[NodeIndex(Pos, 1)] > 0 ? 1 :
                    FPushLength[NodeIndex(Pos, 2)] > 0 ? 2 :
                    FPushLength[NodeIndex(Pos, 3)] > 0 ? 3 : 4;

                if (Dir == 1 && FPushLength[NodeIndex(Pos, 2)] > 0 &&
                    (FPushLength[NodeIndex(Pos, 2)] < FPushLength[NodeIndex(Pos, 1)] ||
                     (FPushLength[NodeIndex(Pos, 2)] == FPushLength[NodeIndex(Pos, 1)] &&
                      FMoveLength[NodeIndex(Pos, 2)] < FMoveLength[NodeIndex(Pos, 1)])))
                    Dir = 2;
                if (Dir < 3 && FPushLength[NodeIndex(Pos, 3)] > 0 &&
                    (FPushLength[NodeIndex(Pos, 3)] < FPushLength[NodeIndex(Pos, Dir)] ||
                     (FPushLength[NodeIndex(Pos, 3)] == FPushLength[NodeIndex(Pos, Dir)] &&
                      FMoveLength[NodeIndex(Pos, 3)] < FMoveLength[NodeIndex(Pos, Dir)])))
                    Dir = 3;
                if (Dir < 4 && FPushLength[NodeIndex(Pos, 4)] > 0 &&
                    (FPushLength[NodeIndex(Pos, 4)] < FPushLength[NodeIndex(Pos, Dir)] ||
                     (FPushLength[NodeIndex(Pos, 4)] == FPushLength[NodeIndex(Pos, Dir)] &&
                      FMoveLength[NodeIndex(Pos, 4)] < FMoveLength[NodeIndex(Pos, Dir)])))
                    Dir = 4;
            }

            // The list will be created backwards, i.e. we start at the
            // node that represents the end-result and work backwards
            // (via FPredecessor) to Node 0, the source node.
            List<Point> Result = new List<Point>();
            int Node = NodeIndex(Pos, Dir);
            int Predecessor = FPredecessor[Node];
            while (Predecessor != 0)
            {
                if (NodeToPos(Predecessor) == NodeToPos(Node))
                    // moving the Sokoban around
                    Result.InsertRange(0, FPath[Predecessor][NodeToDir(Node)]);
                else
                    // pushing the piece
                    Result.Insert(0, PosDirToPos(NodeToPos(Node), NodeToDir(Node)));

                Node = Predecessor;
                Predecessor = FPredecessor[Node];
            }
            
            // moving the Sokoban to the piece at the beginning
            Result.InsertRange(0, FMoveFinder.Path(PosDirToPos(NodeToPos(Node), NodeToDir(Node))));
            
            return Result.ToArray();
        }

        /// <summary>
        /// Returns the co-ordinates of the cell in which the selected piece is located
        /// under the scenario represented by the node Node.
        /// </summary>
        private Point NodeToPos(int Node)
        {
            int PosIndex = (Node-1)/4;
            return new Point(PosIndex % FLevel.Width, PosIndex / FLevel.Width);
        }

        /// <summary>
        /// Returns the direction in which the Sokoban is located relative to the
        /// selected piece under the scenario represented by the node Node.
        /// </summary>
        private int NodeToDir(int Node)
        {
            return (Node-1) % 4 + 1;
        }

        /// <summary>
        /// Returns the direction that is opposite Dir.
        /// </summary>
        private int OppositeDir(int Dir)
        {
            return 5-Dir;
        }

        /// <summary>
        /// Returns the index of the node that represents the scenario in which the
        /// selected piece is located at Pos, and the Sokoban is located above it
        /// (Dir = 1), left of it (Dir = 2), right of it (Dir = 3) or below it
        /// (Dir = 4).
        /// </summary>
        private int NodeIndex(Point Pos, int Dir)
        {
            return 4*Pos.Y*FLevel.Width + 4*Pos.X + Dir;
        }

        /// <summary>
        /// Given a cell where the selected piece is located, and the direction in which
        /// the Sokoban is located relative to it, returns the location of the Sokoban.
        /// </summary>
        private Point PosDirToPos(Point Pos, int Dir)
        {
            if (Dir == 1) return new Point(Pos.X, Pos.Y-1);
            if (Dir == 2) return new Point(Pos.X-1, Pos.Y);
            if (Dir == 3) return new Point(Pos.X+1, Pos.Y);
            return new Point(Pos.X, Pos.Y+1);
        }
    }
}
