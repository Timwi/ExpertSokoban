using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using RT.Util;

namespace ExpertSokoban
{
    public class ESMoveFinder
    {
        private SokobanLevel FLevel;
        private int[] FPathLength;
        private int[] FPredecessor;
        private IntQueue FQueue = new IntQueue();
        private bool FFirst, FDone, FDoAll, FRunningToCompletion, FStopWhenFourSides,
                     FFoundTop, FFoundLeft, FFoundRight, FFoundBottom;
        private int StopIfFoundPos;
        private Control FCallbackOwner;
        private Delegate FCallbackFound, FCallbackDone;

        public bool Done { get { return FDone; } }

        public ESMoveFinder(SokobanLevel Level, bool DoAll, Control Owner, Delegate FoundCallback, Delegate DoneCallback, int StopIfFound)
        {
            Init (Level, DoAll, Owner, FoundCallback, DoneCallback);
            FStopWhenFourSides = true;
            StopIfFoundPos = StopIfFound;
            FFoundTop    = !Level.IsFree (StopIfFound - Level.Width);
            FFoundLeft   = !Level.IsFree (StopIfFound - 1);
            FFoundRight  = !Level.IsFree (StopIfFound + 1);
            FFoundBottom = !Level.IsFree (StopIfFound + Level.Width);
        }
        public ESMoveFinder(SokobanLevel Level, bool DoAll, Control Owner, Delegate FoundCallback, Delegate DoneCallback)
        {
            Init(Level, DoAll, Owner, FoundCallback, DoneCallback);
        }
        private void Init(SokobanLevel Level, bool DoAll, Control Owner, Delegate FoundCallback, Delegate DoneCallback)
        {
            FCallbackOwner = Owner;
            FCallbackFound = FoundCallback;
            FCallbackDone = DoneCallback;
            FLevel = Level;
            FDone = false;
            FFirst = true;
            FDoAll = DoAll;
            FPathLength = new int[FLevel.Width * FLevel.Height];
            FPredecessor = new int[FLevel.Width * FLevel.Height];
            FQueue.Add(FLevel.SokobanPos);
            FPathLength[FLevel.SokobanPos] = 1;
        }
        public void RunToCompletion (int square)
        {
            FRunningToCompletion = true;
            FDoAll = true;
            FStopWhenFourSides = true;
            StopIfFoundPos = square;
            FFoundTop    = !FLevel.IsFree (square-FLevel.Width) || FPathLength[square-FLevel.Width] > 0;
            FFoundLeft   = !FLevel.IsFree (square-           1) || FPathLength[square-           1] > 0;
            FFoundRight  = !FLevel.IsFree (square+           1) || FPathLength[square+           1] > 0;
            FFoundBottom = !FLevel.IsFree (square+FLevel.Width) || FPathLength[square+FLevel.Width] > 0;
            if (FFoundTop && FFoundLeft && FFoundRight && FFoundBottom)
                FDone = true;
            else
                SingleStep();
        }
        public void SingleStep()
        {
            if (FDone) return;
            if (FFirst && !FDoAll && FCallbackOwner != null)
            {
                FCallbackOwner.Invoke(FCallbackFound, new object[] { FLevel.SokobanPos });
                FFirst = false;
            }

            do
            {
                int Pivot = FQueue.Extract();
                // Look at the cell above the pivot...
                int CellPos = Pivot-FLevel.Width;
                SokobanCell Cell = FLevel.Cell (CellPos);
                if ((Cell == SokobanCell.Blank || Cell == SokobanCell.Target) && FPathLength[CellPos] == 0)
                {
                    FQueue.Add (CellPos);
                    FPathLength[CellPos] = FPathLength[Pivot]+1;
                    FPredecessor[CellPos] = Pivot;
                    if (FStopWhenFourSides)
                    {
                        if (CellPos == StopIfFoundPos - FLevel.Width) FFoundTop = true;
                        if (CellPos == StopIfFoundPos - 1           ) FFoundLeft = true;
                        if (CellPos == StopIfFoundPos + 1           ) FFoundRight = true;
                        if (CellPos == StopIfFoundPos + FLevel.Width) FFoundBottom = true;
                    }
                    if ((!FDoAll || FRunningToCompletion) && FCallbackOwner != null)
                        FCallbackOwner.Invoke(FCallbackFound, new object[] { CellPos });
                }
                // ... left of the pivot ...
                CellPos += FLevel.Width-1;
                Cell = FLevel.Cell (CellPos);
                if ((Cell == SokobanCell.Blank || Cell == SokobanCell.Target) && FPathLength[CellPos] == 0)
                {
                    FQueue.Add (CellPos);
                    FPathLength[CellPos] = FPathLength[Pivot]+1;
                    FPredecessor[CellPos] = Pivot;
                    if (FStopWhenFourSides)
                    {
                        if (CellPos == StopIfFoundPos - FLevel.Width) FFoundTop = true;
                        if (CellPos == StopIfFoundPos - 1           ) FFoundLeft = true;
                        if (CellPos == StopIfFoundPos + 1           ) FFoundRight = true;
                        if (CellPos == StopIfFoundPos + FLevel.Width) FFoundBottom = true;
                    }
                    if ((!FDoAll || FRunningToCompletion) && FCallbackOwner != null)
                        FCallbackOwner.Invoke(FCallbackFound, new object[] { CellPos });
                }
                // ... right of the pivot ...
                CellPos += 2;
                Cell = FLevel.Cell (CellPos);
                if ((Cell == SokobanCell.Blank || Cell == SokobanCell.Target) && FPathLength[CellPos] == 0)
                {
                    FQueue.Add (CellPos);
                    FPathLength[CellPos] = FPathLength[Pivot]+1;
                    FPredecessor[CellPos] = Pivot;
                    if (FStopWhenFourSides)
                    {
                        if (CellPos == StopIfFoundPos - FLevel.Width) FFoundTop = true;
                        if (CellPos == StopIfFoundPos - 1           ) FFoundLeft = true;
                        if (CellPos == StopIfFoundPos + 1           ) FFoundRight = true;
                        if (CellPos == StopIfFoundPos + FLevel.Width) FFoundBottom = true;
                    }
                    if ((!FDoAll || FRunningToCompletion) && FCallbackOwner != null)
                        FCallbackOwner.Invoke(FCallbackFound, new object[] { CellPos });
                }
                // ... and below the pivot
                CellPos += FLevel.Width-1;
                Cell = FLevel.Cell (CellPos);
                if ((Cell == SokobanCell.Blank || Cell == SokobanCell.Target) && FPathLength[CellPos] == 0)
                {
                    FQueue.Add (CellPos);
                    FPathLength[CellPos] = FPathLength[Pivot]+1;
                    FPredecessor[CellPos] = Pivot;
                    if (FStopWhenFourSides)
                    {
                        if (CellPos == StopIfFoundPos - FLevel.Width) FFoundTop = true;
                        if (CellPos == StopIfFoundPos - 1           ) FFoundLeft = true;
                        if (CellPos == StopIfFoundPos + 1           ) FFoundRight = true;
                        if (CellPos == StopIfFoundPos + FLevel.Width) FFoundBottom = true;
                    }
                    if ((!FDoAll || FRunningToCompletion) && FCallbackOwner != null)
                        FCallbackOwner.Invoke(FCallbackFound, new object[] { CellPos });
                }

                if (FQueue.Empty || (FStopWhenFourSides && FFoundTop && FFoundLeft && FFoundRight && FFoundBottom))
                {
                    FDone = true;
                    if ((!FDoAll || FRunningToCompletion) && FCallbackOwner != null)
                        FCallbackOwner.Invoke(FCallbackDone, new object[] { });
                }
            }
            while (FDoAll && !FDone);
        }
        public bool MoveValid (int Pos) { return (FPathLength[Pos] > 0); }
        public int PathLength (int Pos) { return FPathLength[Pos]-1; }
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
