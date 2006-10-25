using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using RT.Util;
using System.Drawing;

namespace ExpertSokoban
{
    public class MoveFinder : Virtual2DArray<bool>
    {
        private SokobanLevel FLevel;
        private int[] FPathLength;
        private Point[] FPredecessor;
        private Queue<Point> FQueue = new Queue<Point>();

        public int Width { get { return FLevel.Width; } }
        public int Height { get { return FLevel.Height; } }

        public MoveFinder(SokobanLevel Level)
        {
            Run(Level, null);
        }
        public MoveFinder(SokobanLevel Level, Point? StopIfFourSides)
        {
            Run(Level, StopIfFourSides);
        }
        public void Run(SokobanLevel Level, Point? StopIfFourSides)
        {
            FLevel = Level;
            FPathLength = new int[FLevel.Width * FLevel.Height];
            FPredecessor = new Point[FLevel.Width * FLevel.Height];
            FQueue.Enqueue(FLevel.SokobanPos);
            FPathLength[FLevel.SokobanPos.Y * FLevel.Width + FLevel.SokobanPos.X] = 1;

            while (FQueue.Count > 0)
            {
                Point Pivot = FQueue.Dequeue();
                Examine(new Point(Pivot.X, Pivot.Y-1), Pivot);
                Examine(new Point(Pivot.X-1, Pivot.Y), Pivot);
                Examine(new Point(Pivot.X+1, Pivot.Y), Pivot);
                Examine(new Point(Pivot.X, Pivot.Y+1), Pivot);

                if (StopIfFourSides != null &&
                    FPathLength[StopIfFourSides.Value.Y*FLevel.Width + StopIfFourSides.Value.X - FLevel.Width] > 0 &&
                    FPathLength[StopIfFourSides.Value.Y*FLevel.Width + StopIfFourSides.Value.X + FLevel.Width] > 0 &&
                    FPathLength[StopIfFourSides.Value.Y*FLevel.Width + StopIfFourSides.Value.X - 1] > 0 &&
                    FPathLength[StopIfFourSides.Value.Y*FLevel.Width + StopIfFourSides.Value.X + 1] > 0)
                    FQueue.Clear();
            }
        }

        private void Examine(Point Pos, Point Pivot)
        {
            if (FLevel.IsFree(Pos) && FPathLength[Pos.Y*FLevel.Width + Pos.X] == 0)
            {
                FQueue.Enqueue(Pos);
                FPathLength[Pos.Y*FLevel.Width + Pos.X] = FPathLength[Pivot.Y*FLevel.Width + Pivot.X]+1;
                FPredecessor[Pos.Y*FLevel.Width + Pos.X] = Pivot;
            }
        }

        public bool Get(Point Pos)
        {
            int Index = Pos.Y*FLevel.Width + Pos.X;
            return Index < 0 || Index >= FPathLength.Length ? false : FPathLength[Index] > 0;
        }

        public bool Get(int x, int y)
        {
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
}
