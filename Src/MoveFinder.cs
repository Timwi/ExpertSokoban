using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using RT.Util;
using System.Drawing;

namespace ExpertSokoban
{
    public enum MoveFinderOption { None, IgnorePieces }
    public class MoveFinder : Virtual2DArray<bool>
    {
        protected SokobanLevel FLevel;
        private int[] FPathLength;
        private Point[] FPredecessor;
        private Queue<Point> FQueue = new Queue<Point>();

        public int Width { get { return FLevel.Width; } }
        public int Height { get { return FLevel.Height; } }

        public MoveFinder(SokobanLevel Level)
        {
            Run(Level, null, false);
        }
        public MoveFinder(SokobanLevel Level, Point? StopIfFourSides)
        {
            Run(Level, StopIfFourSides, false);
        }
        public MoveFinder(SokobanLevel Level, MoveFinderOption SpecialOption)
        {
            Run(Level, null, SpecialOption == MoveFinderOption.IgnorePieces);
        }
        public void Run(SokobanLevel Level, Point? StopIfFourSides, bool IgnorePieces)
        {
            FLevel = Level;
            FPathLength = new int[FLevel.Width * FLevel.Height];
            FPredecessor = new Point[FLevel.Width * FLevel.Height];
            FQueue.Enqueue(FLevel.SokobanPos);
            FPathLength[FLevel.SokobanPos.Y * FLevel.Width + FLevel.SokobanPos.X] = 1;

            while (FQueue.Count > 0)
            {
                Point Pivot = FQueue.Dequeue();
                Examine(new Point(Pivot.X, Pivot.Y-1), Pivot, IgnorePieces);
                Examine(new Point(Pivot.X-1, Pivot.Y), Pivot, IgnorePieces);
                Examine(new Point(Pivot.X+1, Pivot.Y), Pivot, IgnorePieces);
                Examine(new Point(Pivot.X, Pivot.Y+1), Pivot, IgnorePieces);

                if (StopIfFourSides != null &&
                    FPathLength[StopIfFourSides.Value.Y*FLevel.Width + StopIfFourSides.Value.X - FLevel.Width] > 0 &&
                    FPathLength[StopIfFourSides.Value.Y*FLevel.Width + StopIfFourSides.Value.X + FLevel.Width] > 0 &&
                    FPathLength[StopIfFourSides.Value.Y*FLevel.Width + StopIfFourSides.Value.X - 1] > 0 &&
                    FPathLength[StopIfFourSides.Value.Y*FLevel.Width + StopIfFourSides.Value.X + 1] > 0)
                    FQueue.Clear();
            }
        }

        private void Examine(Point Pos, Point Pivot, bool IgnorePieces)
        {
            if (Pos.X < 0 || Pos.X >= FLevel.Width || Pos.Y < 0 || Pos.Y >= FLevel.Height)
                return;
            if ((FLevel.IsFree(Pos) || (IgnorePieces && FLevel.Cell(Pos) != SokobanCell.Wall)) &&
                FPathLength[Pos.Y*FLevel.Width + Pos.X] == 0)
            {
                if (Pos.X > 0 && Pos.X < FLevel.Width-1 && Pos.Y > 0 && Pos.Y < FLevel.Height-1)
                    FQueue.Enqueue(Pos);
                FPathLength[Pos.Y*FLevel.Width + Pos.X] = FPathLength[Pivot.Y*FLevel.Width + Pivot.X]+1;
                FPredecessor[Pos.Y*FLevel.Width + Pos.X] = Pivot;
            }
        }

        public virtual bool Get(Point Pos)
        {
            if (Pos.X < 0 || Pos.X >= FLevel.Width || Pos.Y < 0 || Pos.Y >= FLevel.Height)
                return false;
            int Index = Pos.Y*FLevel.Width + Pos.X;
            return Index < 0 || Index >= FPathLength.Length ? false : FPathLength[Index] > 0;
        }

        public virtual bool Get(int x, int y)
        {
            if (x < 0 || x >= FLevel.Width || y < 0 || y >= FLevel.Height)
                return false;
            int Index = y*FLevel.Width + x;
            return Index < 0 || Index >= FPathLength.Length ? false : FPathLength[Index] > 0;
        }

        public int PathLength(Point Pos)
        {
            int Index = Pos.Y*FLevel.Width + Pos.X;
            return Index < 0 || Index >= FPathLength.Length ? -1 : FPathLength[Index]-1;
        }
        
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

    public class MoveFinderOutline : MoveFinder
    {
        public MoveFinderOutline(SokobanLevel Level)
            : base(Level, MoveFinderOption.IgnorePieces)
        {
            // This space intentionally left blank
            // (the base constructor calls Run())
        }
        public override bool Get(Point Pos)
        {
            return (Pos.X == 0 || Pos.X == FLevel.Width-1 ||
                Pos.Y == 0 || Pos.Y == FLevel.Height-1)
                && base.Get(Pos);
        }
        public override bool Get(int x, int y)
        {
            return (x == 0 || x == FLevel.Width-1 ||
                y == 0 || y == FLevel.Height-1)
                && base.Get(x, y);
        }
    }
}
