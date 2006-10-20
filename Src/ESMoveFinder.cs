using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using RT.Util;

namespace ExpertSokoban
{
    public class ESMoveFinder : ESFinder
    {
        private SokobanLevel FLevel;
        private int[] FPathLength;
        private int[] FPredecessor;
        private Queue<int> FQueue = new Queue<int>();

        public ESMoveFinder(SokobanLevel Level)
        {
            Run(Level, -1);
        }
        public ESMoveFinder(SokobanLevel Level, int StopIfFourSides)
        {
            Run(Level, StopIfFourSides);
        }
        public void Run(SokobanLevel Level, int StopIfFourSides)
        {
            FLevel = Level;
            FPathLength = new int[FLevel.Width * FLevel.Height];
            FPredecessor = new int[FLevel.Width * FLevel.Height];
            FQueue.Enqueue(FLevel.SokobanPos);
            FPathLength[FLevel.SokobanPos] = 1;

            while (FQueue.Count > 0)
            {
                int Pivot = FQueue.Dequeue();
                Examine(Pivot - FLevel.Width, Pivot);
                Examine(Pivot - 1, Pivot);
                Examine(Pivot + 1, Pivot);
                Examine(Pivot + FLevel.Width, Pivot);

                if (StopIfFourSides >= 0 &&
                    FPathLength[StopIfFourSides - FLevel.Width] > 0 &&
                    FPathLength[StopIfFourSides + FLevel.Width] > 0 &&
                    FPathLength[StopIfFourSides - 1] > 0 &&
                    FPathLength[StopIfFourSides + 1] > 0)
                    FQueue.Clear();
            }
        }

        private void Examine(int Pos, int Pivot)
        {
            if (FLevel.IsFree(Pos) && FPathLength[Pos] == 0)
            {
                FQueue.Enqueue(Pos);
                FPathLength[Pos] = FPathLength[Pivot]+1;
                FPredecessor[Pos] = Pivot;
            }
        }

        public override bool Valid(int Pos)
        {
            return (FPathLength[Pos] > 0);
        }

        public int PathLength(int Pos)
        {
            return FPathLength[Pos]-1;
        }
        
        public int[] Path (int Pos)
        {
            if (FPathLength[Pos] == 0) return null;
            int[] Result = new int [ FPathLength[Pos]-1 ];
            int At = Pos;
            for (int i = FPathLength[Pos]-2; i >= 0; i--)
            {
                Result[i] = At - FPredecessor[At];
                At = FPredecessor[At];
            }
            return Result;
        }
    }
}
