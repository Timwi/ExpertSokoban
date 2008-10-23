using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using RT.Util;
using System.Drawing;
using RT.Util.Collections;

namespace ExpertSokoban
{
    /// <summary>
    /// Identifies one of the two modes of operation for a MoveFinder.
    /// </summary>
    public enum MoveFinderOption
    {
        /// <summary>
        /// Represents the normal mode of operation for a MoveFinder, where cells
        /// reachable from the current Sokoban position are deemed "valid".
        /// </summary>
        None,

        /// <summary>
        /// Represents a special mode of operation where all cells reachable from the
        /// Sokoban position are considered "valid" even if a piece is in the way (i.e.
        /// only walls are considered obstacles).
        /// </summary>
        IgnorePieces
    }

    /// <summary>
    /// Finds out (using a breath-first search) where the Sokoban can move, given a
    /// SokobanLevel object that represents the current situation of the game.
    /// </summary>
    public class MoveFinder : Virtual2DArray<bool>
    {
        /// <summary>
        /// The SokobanLevel we are examining.
        /// </summary>
        protected SokobanLevel FLevel;

        /// <summary>
        /// Stores the move path length for each destination cell. A value of 0 means
        /// that the cell is unreachable. All other values represent a move path length
        /// of one less than the value.
        /// </summary>
        private int[] FPathLength;

        /// <summary>
        /// For each cell, identifies the precessor cell in the path towards that cell.
        /// </summary>
        private Point[] FPredecessor;

        /// <summary>
        /// (read-only) Returns the width of the level under consideration.
        /// </summary>
        public int Width { get { return FLevel.Width; } }

        /// <summary>
        /// (read-only) Returns the height of the level under consideration.
        /// </summary>
        public int Height { get { return FLevel.Height; } }

        /// <summary>
        /// Runs a normal MoveFinder.
        /// </summary>
        /// <param name="Level">The Sokoban level under consideration.</param>
        public MoveFinder(SokobanLevel Level)
        {
            Run(Level, null, false);
        }

        /// <summary>
        /// Runs a MoveFinder only so far as necessary to ascertain that all four cells
        /// adjacent to the specified cell are reachable.
        /// </summary>
        /// <param name="Level">The Sokoban level under consideration.</param>
        /// <param name="StopIfFourSides">The cell whose four adjacent cells are under
        /// consideration.</param>
        public MoveFinder(SokobanLevel Level, Point StopIfFourSides)
        {
            Run(Level, StopIfFourSides, false);
        }

        /// <summary>
        /// Runs a MoveFinder using the specified mode of operation.
        /// </summary>
        /// <param name="Level">The Sokoban level under consideration.</param>
        /// <param name="SpecialOption">An option specifying the behaviour of the
        /// MoveFinder. Refer to the documentation of MoveFinderOption.</param>
        public MoveFinder(SokobanLevel Level, MoveFinderOption SpecialOption)
        {
            Run(Level, null, SpecialOption == MoveFinderOption.IgnorePieces);
        }

        /// <summary>
        /// Runs the MoveFinder using the specified options.
        /// </summary>
        /// <param name="Level">The Sokoban level under consideration.</param>
        /// <param name="StopIfFourSides">If not null, runs a MoveFinder only so far as
        /// necessary to ascertain that all four cells adjacent to the specified cell
        /// are reachable.</param>
        /// <param name="IgnorePieces">If true, only walls are considered obstacles.</param>
        private void Run(SokobanLevel Level, Point? StopIfFourSides, bool IgnorePieces)
        {
            // Contains the set of cells yet to be considered
            Queue<Point> Queue = new Queue<Point>();

            FLevel = Level;
            FPathLength = new int[FLevel.Width * FLevel.Height];
            FPredecessor = new Point[FLevel.Width * FLevel.Height];
            Queue.Enqueue(FLevel.SokobanPos);
            FPathLength[FLevel.SokobanPos.Y * FLevel.Width + FLevel.SokobanPos.X] = 1;

            // Breadth-first search: extract an item from the queue, process it, and add
            // the newly-discovered items to the queue (Examine() does that).
            while (Queue.Count > 0)
            {
                Point Pivot = Queue.Dequeue();
                Examine(new Point(Pivot.X, Pivot.Y-1), Pivot, IgnorePieces, Queue);
                Examine(new Point(Pivot.X-1, Pivot.Y), Pivot, IgnorePieces, Queue);
                Examine(new Point(Pivot.X+1, Pivot.Y), Pivot, IgnorePieces, Queue);
                Examine(new Point(Pivot.X, Pivot.Y+1), Pivot, IgnorePieces, Queue);

                if (StopIfFourSides != null &&
                    FPathLength[StopIfFourSides.Value.Y*FLevel.Width + StopIfFourSides.Value.X - FLevel.Width] > 0 &&
                    FPathLength[StopIfFourSides.Value.Y*FLevel.Width + StopIfFourSides.Value.X + FLevel.Width] > 0 &&
                    FPathLength[StopIfFourSides.Value.Y*FLevel.Width + StopIfFourSides.Value.X - 1] > 0 &&
                    FPathLength[StopIfFourSides.Value.Y*FLevel.Width + StopIfFourSides.Value.X + 1] > 0)
                    Queue.Clear();
            }
        }

        /// <summary>
        /// Examines a cell to see if the length of the path to it can be shortened (or
        /// whether the cell has even been discovered yet).
        /// </summary>
        /// <param name="Pos">Cell under consideration.</param>
        /// <param name="Pivot">Becomes the new predecessor if it makes the path length
        /// shorter.</param>
        /// <param name="IgnorePieces">If true, only walls are considered obstacles.</param>
        /// <param name="Queue">The queue to insert newly discovered cells into.</param>
        private void Examine(Point Pos, Point Pivot, bool IgnorePieces, Queue<Point> Queue)
        {
            if (Pos.X < 0 || Pos.X >= FLevel.Width || Pos.Y < 0 || Pos.Y >= FLevel.Height)
                return;
            if ((FLevel.IsFree(Pos) || (IgnorePieces && FLevel.Cell(Pos) != SokobanCell.Wall)) &&
                FPathLength[Pos.Y*FLevel.Width + Pos.X] == 0)
            {
                if (Pos.X > 0 && Pos.X < FLevel.Width-1 && Pos.Y > 0 && Pos.Y < FLevel.Height-1)
                    Queue.Enqueue(Pos);
                FPathLength[Pos.Y*FLevel.Width + Pos.X] = FPathLength[Pivot.Y*FLevel.Width + Pivot.X]+1;
                FPredecessor[Pos.Y*FLevel.Width + Pos.X] = Pivot;
            }
        }

        /// <summary>
        /// Returns true if the specified cell is reachable.
        /// </summary>
        public virtual bool Get(Point Pos)
        {
            if (Pos.X < 0 || Pos.X >= FLevel.Width || Pos.Y < 0 || Pos.Y >= FLevel.Height)
                return false;
            int Index = Pos.Y*FLevel.Width + Pos.X;
            return Index < 0 || Index >= FPathLength.Length ? false : FPathLength[Index] > 0;
        }

        /// <summary>
        /// Returns true if the specified cell is reachable.
        /// </summary>
        public virtual bool Get(int x, int y)
        {
            if (x < 0 || x >= FLevel.Width || y < 0 || y >= FLevel.Height)
                return false;
            int Index = y*FLevel.Width + x;
            return Index < 0 || Index >= FPathLength.Length ? false : FPathLength[Index] > 0;
        }

        /// <summary>
        /// Returns the length of the path from the source position to the specified
        /// cell, or -1 if it is not reachable.
        /// </summary>
        public int PathLength(Point Pos)
        {
            int Index = Pos.Y*FLevel.Width + Pos.X;
            return Index < 0 || Index >= FPathLength.Length ? -1 : FPathLength[Index]-1;
        }

        /// <summary>
        /// Returns the entire path of cells from the source position to the specified
        /// cell, or null if it is not reachable.
        /// </summary>
        public Point[] Path (Point Pos)
        {
            if (FPathLength[Pos.Y*FLevel.Width + Pos.X] == 0)
                return null;

            Point[] Result = new Point[FPathLength[Pos.Y*FLevel.Width + Pos.X]-1];
            Point At = Pos;
            for (int i = Result.Length-1; i >= 0; i--)
            {
                Result[i] = At;
                At = FPredecessor[At.Y*FLevel.Width + At.X];
            }
            return Result;
        }
    }

    /// <summary>
    /// Encapsulates a MoveFinder, but overrides the Get() method in such a way that it
    /// returns true only for the reachable cells at the outline of the level.
    /// </summary>
    public class MoveFinderOutline : MoveFinder
    {
        /// <summary>
        /// Main constructor.
        /// </summary>
        public MoveFinderOutline(SokobanLevel Level)
            : base(Level, MoveFinderOption.IgnorePieces)
        {
            // This space intentionally left blank
            // (the base constructor calls Run())
        }

        /// <summary>
        /// Returns true if the specified cell is valid and lies at the outline of the
        /// level.
        /// </summary>
        public override bool Get(Point Pos)
        {
            return (Pos.X == 0 || Pos.X == FLevel.Width-1 ||
                    Pos.Y == 0 || Pos.Y == FLevel.Height-1)
                    && base.Get(Pos);
        }

        /// <summary>
        /// Returns true if the specified cell is valid and lies at the outline of the
        /// level.
        /// </summary>
        public override bool Get(int x, int y)
        {
            return (x == 0 || x == FLevel.Width-1 ||
                    y == 0 || y == FLevel.Height-1)
                    && base.Get(x, y);
        }
    }
}
