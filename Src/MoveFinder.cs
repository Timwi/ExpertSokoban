using System.Collections.Generic;
using System.Drawing;
using RT.Util.Collections;

namespace ExpertSokoban
{
    /// <summary>
    /// Identifies one of the two modes of operation for a <see cref="MoveFinder"/>.
    /// </summary>
    public enum MoveFinderOption
    {
        /// <summary>
        /// Represents the normal mode of operation for a <see cref="MoveFinder"/>,
        /// where cells reachable from the current Sokoban position are deemed "valid".
        /// </summary>
        None,

        /// <summary>
        /// Represents a special mode of operation for a <see cref="MoveFinder"/> where
        /// all cells reachable from the Sokoban position are considered "valid" even if a
        /// piece is in the way (i.e. only walls are considered obstacles).
        /// </summary>
        IgnorePieces
    }

    /// <summary>
    /// Finds out (using a breath-first search) where the Sokoban can move, given a
    /// <see cref="SokobanLevel"/> object that represents the current situation of the game.
    /// </summary>
    public class MoveFinder : Virtual2DArray<bool>
    {
        /// <summary>The <see cref="SokobanLevel"/> we are examining.</summary>
        protected SokobanLevel _level;

        /// <summary>
        /// Stores the move path length for each destination cell. A value of 0 means
        /// that the cell is unreachable. All other values represent a move path length
        /// of one less than the value.
        /// </summary>
        private int[] _pathLength;

        /// <summary>
        /// For each cell, identifies the precessor cell in the path towards that cell.
        /// </summary>
        private Point[] _predecessor;

        /// <summary>
        /// (read-only) Returns the width of the level under consideration.
        /// </summary>
        public int Width { get { return _level.Width; } }

        /// <summary>
        /// (read-only) Returns the height of the level under consideration.
        /// </summary>
        public int Height { get { return _level.Height; } }

        /// <summary>Runs a normal MoveFinder.</summary>
        /// <param name="level">The Sokoban level under consideration.</param>
        public MoveFinder(SokobanLevel level)
        {
            run(level, null, false);
        }

        /// <summary>
        /// Runs a MoveFinder only so far as necessary to ascertain that all four cells
        /// adjacent to the specified cell are reachable.
        /// </summary>
        /// <param name="level">The Sokoban level under consideration.</param>
        /// <param name="stopIfFourSides">The cell whose four adjacent cells are under
        /// consideration.</param>
        public MoveFinder(SokobanLevel level, Point stopIfFourSides)
        {
            run(level, stopIfFourSides, false);
        }

        /// <summary>Runs a MoveFinder using the specified mode of operation.</summary>
        /// <param name="level">The Sokoban level under consideration.</param>
        /// <param name="specialOption">A set of <see cref="MoveFinderOption"/> flags
        /// specifying the behaviour of the MoveFinder.</param>
        public MoveFinder(SokobanLevel level, MoveFinderOption specialOption)
        {
            run(level, null, specialOption == MoveFinderOption.IgnorePieces);
        }

        /// <summary>Runs the MoveFinder using the specified options.</summary>
        /// <param name="level">The Sokoban level under consideration.</param>
        /// <param name="stopIfFourSides">If not null, runs a MoveFinder only so far as
        /// necessary to ascertain that all four cells adjacent to the specified cell
        /// are reachable.</param>
        /// <param name="ignorePieces">If true, only walls are considered obstacles.</param>
        private void run(SokobanLevel level, Point? stopIfFourSides, bool ignorePieces)
        {
            // Contains the set of cells yet to be considered
            Queue<Point> queue = new Queue<Point>();

            _level = level;
            _pathLength = new int[_level.Width * _level.Height];
            _predecessor = new Point[_level.Width * _level.Height];
            queue.Enqueue(_level.SokobanPos);
            _pathLength[_level.SokobanPos.Y * _level.Width + _level.SokobanPos.X] = 1;

            // Breadth-first search: extract an item from the queue, process it, and add
            // the newly-discovered items to the queue (Examine() does that).
            while (queue.Count > 0)
            {
                Point pivot = queue.Dequeue();
                examine(new Point(pivot.X, pivot.Y - 1), pivot, ignorePieces, queue);
                examine(new Point(pivot.X - 1, pivot.Y), pivot, ignorePieces, queue);
                examine(new Point(pivot.X + 1, pivot.Y), pivot, ignorePieces, queue);
                examine(new Point(pivot.X, pivot.Y + 1), pivot, ignorePieces, queue);

                if (stopIfFourSides != null &&
                    _pathLength[stopIfFourSides.Value.Y * _level.Width + stopIfFourSides.Value.X - _level.Width] > 0 &&
                    _pathLength[stopIfFourSides.Value.Y * _level.Width + stopIfFourSides.Value.X + _level.Width] > 0 &&
                    _pathLength[stopIfFourSides.Value.Y * _level.Width + stopIfFourSides.Value.X - 1] > 0 &&
                    _pathLength[stopIfFourSides.Value.Y * _level.Width + stopIfFourSides.Value.X + 1] > 0)
                    queue.Clear();
            }
        }

        /// <summary>
        /// Examines a cell to see if the length of the path to it can be shortened (or
        /// whether the cell has even been discovered yet).
        /// </summary>
        /// <param name="pos">Cell under consideration.</param>
        /// <param name="pivot">Becomes the new predecessor if it makes the path length
        /// shorter.</param>
        /// <param name="ignorePieces">If true, only walls are considered obstacles.</param>
        /// <param name="queue">The queue to insert newly discovered cells into.</param>
        private void examine(Point pos, Point pivot, bool ignorePieces, Queue<Point> queue)
        {
            if (pos.X < 0 || pos.X >= _level.Width || pos.Y < 0 || pos.Y >= _level.Height)
                return;
            if ((_level.IsFree(pos) || (ignorePieces && _level.Cell(pos) != SokobanCell.Wall)) &&
                _pathLength[pos.Y * _level.Width + pos.X] == 0)
            {
                if (pos.X > 0 && pos.X < _level.Width - 1 && pos.Y > 0 && pos.Y < _level.Height - 1)
                    queue.Enqueue(pos);
                _pathLength[pos.Y * _level.Width + pos.X] = _pathLength[pivot.Y * _level.Width + pivot.X] + 1;
                _predecessor[pos.Y * _level.Width + pos.X] = pivot;
            }
        }

        /// <summary>Returns true if the specified cell is reachable.</summary>
        public virtual bool Get(Point pos)
        {
            if (pos.X < 0 || pos.X >= _level.Width || pos.Y < 0 || pos.Y >= _level.Height)
                return false;
            int index = pos.Y * _level.Width + pos.X;
            return index < 0 || index >= _pathLength.Length ? false : _pathLength[index] > 0;
        }

        /// <summary>Returns true if the specified cell is reachable.</summary>
        public virtual bool Get(int x, int y)
        {
            if (x < 0 || x >= _level.Width || y < 0 || y >= _level.Height)
                return false;
            int index = y * _level.Width + x;
            return index < 0 || index >= _pathLength.Length ? false : _pathLength[index] > 0;
        }

        /// <summary>
        /// Returns the length of the path from the source position to the specified
        /// cell, or -1 if it is not reachable.
        /// </summary>
        public int PathLength(Point pos)
        {
            int index = pos.Y * _level.Width + pos.X;
            return index < 0 || index >= _pathLength.Length ? -1 : _pathLength[index] - 1;
        }

        /// <summary>
        /// Returns the entire path of cells from the source position to the specified
        /// cell, or null if it is not reachable.
        /// </summary>
        public Point[] Path(Point pos)
        {
            if (_pathLength[pos.Y * _level.Width + pos.X] == 0)
                return null;

            Point[] result = new Point[_pathLength[pos.Y * _level.Width + pos.X] - 1];
            Point at = pos;
            for (int i = result.Length - 1; i >= 0; i--)
            {
                result[i] = at;
                at = _predecessor[at.Y * _level.Width + at.X];
            }
            return result;
        }
    }

    /// <summary>
    /// Encapsulates a <see cref="MoveFinder"/>, but overrides the methods <see cref="Get(Point)"/> and <see cref="Get(int, int)"/>
    /// in such a way that it returns true only for the reachable cells at the outline of the level.
    /// </summary>
    public class MoveFinderOutline : MoveFinder
    {
        /// <summary>
        /// Main constructor.
        /// </summary>
        public MoveFinderOutline(SokobanLevel level)
            : base(level, MoveFinderOption.IgnorePieces)
        {
            // This space intentionally left blank
            // (the base constructor calls run())
        }

        /// <summary>Returns true if the specified cell is valid and lies at the outline of the level.</summary>
        public override bool Get(Point pos)
        {
            return (pos.X == 0 || pos.X == _level.Width - 1 ||
                    pos.Y == 0 || pos.Y == _level.Height - 1)
                    && base.Get(pos);
        }

        /// <summary>Returns true if the specified cell is valid and lies at the outline of the level.</summary>
        public override bool Get(int x, int y)
        {
            return (x == 0 || x == _level.Width - 1 ||
                    y == 0 || y == _level.Height - 1)
                    && base.Get(x, y);
        }
    }
}
