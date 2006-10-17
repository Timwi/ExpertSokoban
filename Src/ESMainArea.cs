using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Media;
using System.Drawing.Drawing2D;
using RT.Controls;

namespace ExpertSokoban
{
    public enum ESMainAreaState
    {
        Null, Move, Push, Solved, Editing
    };
    public enum ESMainAreaTool
    {
        Wall, Piece, Target, Sokoban
    };

    public class ESMainArea : DoubleBufferedPanel
    {
        public ESMainAreaState State { get { return FState; } }
        public event EventHandler MoveMade;

        private SokobanLevel FLevel;
        private ESRenderer Renderer;
        private ESMoveFinder MoveFinder;
        private ESPushFinder PushFinder;
        private ESMainAreaState FState;
        private ESMainAreaTool Tool;

        private Color ToolRectColour = Color.FromArgb(0, 0x80, 0);
        private Brush MoveBrush = new SolidBrush(Color.FromArgb(100, Color.LimeGreen));
        private Brush PushBrush = new SolidBrush(Color.FromArgb(100, Color.LightBlue));
        private Brush PushPathBrush = new SolidBrush(Color.FromArgb(0, 0, 0x80));

        private int SelX, SelY, OrigMouseDown, MouseOverCell;

        /// If, while State == Push, the user clicks somewhere where the PushFinder has
        /// not yet found a path to, but is still running, Consider (and ConsiderDirection)
        /// are set to the cell clicked on. If the PushFinder encounters the cell stored
        /// in Consider, the sequence is executed. For an explanation of ConsiderDir, see
        /// GetOrigMouseDownDir().
        private int Consider, ConsiderDirection;

        private SoundPlayer SndLevelSolved, SndMeep, SndPiecePlaced, SndThreadDone;
        private int[][] UndoBuffer;

        // Push path, but encoded as a sequence of cell co-ordinates.
        private int[] PrevCellSequence;

        private delegate void MoveFoundCallback(int Pos);
        private delegate void MoveFinderDoneCallback();
        private delegate void PushFoundCallback(int Pos);
        private delegate void PushFinderDoneCallback(bool AnythingPossible);

        public ESMainArea()
        {
            FLevel = null;
            Renderer = null;
            FState = ESMainAreaState.Null;
            Init();
        }

        public ESMainArea(SokobanLevel Level)
        {
            Init();
            SetLevel(Level);
        }

        private void Init()
        {
            SndLevelSolved = new SoundPlayer(Properties.Resources.SndLevelDone);
            SndMeep = new SoundPlayer(Properties.Resources.SndMeep);
            SndPiecePlaced = new SoundPlayer(Properties.Resources.SndPiecePlaced);
            SndThreadDone = new SoundPlayer(Properties.Resources.SndThreadDone);
            this.MouseDown += new MouseEventHandler(ESMainArea_MouseDown);
            this.MouseMove += new MouseEventHandler(ESMainArea_MouseMove);
            this.MouseUp += new MouseEventHandler(ESMainArea_MouseUp);
            this.Paint += new PaintEventHandler(ESMainArea_Paint);
            this.PaintBuffer += new PaintEventHandler(ESMainArea_PaintBuffer);
            Timer t = new Timer();
            t.Tick += new EventHandler(TimerTick);
            t.Interval = 1;
            t.Enabled = true;
        }

        private void ESMainArea_PaintBuffer(object sender, PaintEventArgs e)
        {
            if (FState != ESMainAreaState.Null)
            {
                Renderer = new ESRenderer(FLevel, ClientSize);
                Renderer.Render(e.Graphics);
            }
            if (FState == ESMainAreaState.Solved)
            {
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                Image ImgLevelSolved = Properties.Resources.ImgLevelSolved;
                if (ClientSize.Width < ImgLevelSolved.Width)
                    e.Graphics.DrawImage(ImgLevelSolved,
                        0,
                        (ClientSize.Height - ClientSize.Width * ImgLevelSolved.Height / ImgLevelSolved.Width)/2,
                        ClientSize.Width,
                        ClientSize.Width * ImgLevelSolved.Height / ImgLevelSolved.Width);
                else
                    e.Graphics.DrawImage(ImgLevelSolved,
                        ClientSize.Width/2 - ImgLevelSolved.Width/2,
                        ClientSize.Height/2 - ImgLevelSolved.Height/2);
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if ((FState == ESMainAreaState.Move || FState == ESMainAreaState.Push) && MoveFinder != null && !MoveFinder.Done)
                MoveFinder.SingleStep();
            if (FState == ESMainAreaState.Push && PushFinder != null && !PushFinder.Done)
                PushFinder.SingleStep();
        }

        private void ReinitMoveFinder()
        {
            MoveFinder = new ESMoveFinder(FLevel, false, this,
                new MoveFoundCallback(MoveFound),
                new MoveFinderDoneCallback(MoveFinderDone));
        }

        private void AddUndo(int prevSokPos, bool push, int fromPush, int toPush)
        {
            int n = 0;
            if (UndoBuffer == null)
                UndoBuffer = new int[1][];
            else
            {
                n = UndoBuffer.Length;
                int[][] newUndoBuffer = new int[n+1][];
                for (int i = 0; i < UndoBuffer.Length; i++)
                    newUndoBuffer[i] = UndoBuffer[i];
                UndoBuffer = newUndoBuffer;
            }

            int[] newElem = new int[push ? 3 : 1];
            newElem[0] = prevSokPos;
            if (push) { newElem[1] = fromPush; newElem[2] = toPush; }
            UndoBuffer[n] = newElem;
        }

        public void Undo()
        {
            if (UndoBuffer == null) return;
            if (FState == ESMainAreaState.Editing || FState == ESMainAreaState.Solved)
                return;
            if (FLevel == null) return;

            int[] Extracted = UndoBuffer[UndoBuffer.Length-1];
            if (UndoBuffer.Length == 1) UndoBuffer = null;
            else
            {
                int[][] NewUndoBuffer = new int[UndoBuffer.Length-1][];
                for (int i = 0; i < UndoBuffer.Length-1; i++)
                    NewUndoBuffer[i] = UndoBuffer[i];
                UndoBuffer = NewUndoBuffer;
            }

            bool Push = Extracted.Length > 1;
            FLevel.SetSokobanPos(Extracted[0]);
            if (Push) FLevel.MovePiece(Extracted[2], Extracted[1]);

            Consider = 0;
            ConsiderDirection = 0;
            FState = ESMainAreaState.Move;
            ReinitMoveFinder();
            Refresh();
        }

        private void ESMainArea_Paint(object sender, PaintEventArgs e)
        {
            if (FState != ESMainAreaState.Null)
            {
                for (int i = 0; i < FLevel.Width*FLevel.Height; i++)
                    if (i != FLevel.SokobanPos && PushFinder != null && PushFinder.PushValid(i) && FState == ESMainAreaState.Push)
                        e.Graphics.FillRectangle(PushBrush, Renderer.GetCellRect(i));
                    else if (i != FLevel.SokobanPos && MoveFinder != null && MoveFinder.MoveValid(i) &&
                        (FState == ESMainAreaState.Move || FState == ESMainAreaState.Push))
                        e.Graphics.FillRectangle(MoveBrush, Renderer.GetCellRect(i));
                if (FState == ESMainAreaState.Push)
                {
                    Renderer.DrawCell(e.Graphics, SelX, SelY, SokobanImage.PieceSelected);
                    if (PrevCellSequence != null)
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        for (int i = 0; i < PrevCellSequence.Length; i++)
                        {
                            RectangleF CellRect = Renderer.GetCellRect(PrevCellSequence[i]);
                            e.Graphics.FillEllipse(PushPathBrush,
                                CellRect.Left + Renderer.CellWidth/3,
                                CellRect.Top + Renderer.CellHeight/3,
                                Renderer.CellWidth/3, Renderer.CellHeight/3);
                        }
                    }
                }
            }
        }

        private void UpdatePushPath(int[][] NewPushPath)
        {
            if (NewPushPath == null)
            {
                ClearPushPath();
                return;
            }

            int SokPos = FLevel.SokobanPos;
            int PiecePos = SelY * FLevel.Width + SelX;

            int PathLength = 0;

            for (int i = 0; i < NewPushPath.Length; i++)
                if (NewPushPath[i] != null)
                    for (int j = 0; j < NewPushPath[i].Length; j++)
                    {
                        if (PiecePos == SokPos + NewPushPath[i][j])
                        {
                            PathLength++;
                            PiecePos += NewPushPath[i][j];
                        }
                        SokPos += NewPushPath[i][j];
                    }

            SokPos = FLevel.SokobanPos;
            PiecePos = SelY * FLevel.Width + SelX;
            int[] CellSequence = new int[PathLength];
            int Index = 0;

            for (int i = 0; i < NewPushPath.Length; i++)
                if (NewPushPath[i] != null)
                    for (int j = 0; j < NewPushPath[i].Length; j++)
                    {
                        if (PiecePos == SokPos + NewPushPath[i][j])
                        {
                            // need to push
                            PiecePos += NewPushPath[i][j];
                            CellSequence[Index] = PiecePos;
                            Index++;
                        }
                        SokPos += NewPushPath[i][j];
                    }

            // Find the first index at which PrevCellSequence and CellSequence differ,
            // and invalidate the drawing area from there
            int FirstDiff = 0;
            if (PrevCellSequence != null)
            {
                while (FirstDiff < Math.Min(PrevCellSequence.Length, CellSequence.Length) && PrevCellSequence[FirstDiff] == CellSequence[FirstDiff])
                    FirstDiff++;
                for (int j = FirstDiff; j < PrevCellSequence.Length; j++)
                    Invalidate(RoundedRectangle(Renderer.GetCellRect(PrevCellSequence[j])));
            }
            for (int j = FirstDiff; j < CellSequence.Length; j++)
                Invalidate(RoundedRectangle(Renderer.GetCellRect(CellSequence[j])));
            PrevCellSequence = CellSequence;
        }

        private void ClearPushPath()
        {
            if (PrevCellSequence != null)
                for (int i = 0; i < PrevCellSequence.Length; i++)
                    Invalidate(RoundedRectangle(Renderer.GetCellRect(PrevCellSequence[i])));
            PrevCellSequence = null;
        }

        private Rectangle RoundedRectangle(RectangleF Src)
        {
            return new Rectangle((int) Src.X-2, (int) Src.Y-2, (int) Src.Width+4, (int) Src.Height+4);
        }

        // If you just click a cell to push a piece to, PushFinder will find
        // the shortest push-path regardless of where the Sokoban will end up.
        // If you mouse-down *next* to the cell you want to push the piece to and 
        // then *drag* onto the cell before releasing, then we will try to move
        // the piece in such a way that the Sokoban will end up on the cell you
        // clicked first. GetOrigMouseDownDir() determines in which direction you
        // dragged. This value is passed on to PushFinder.getMoves().
        private int GetOrigMouseDownDir(int Cell)
        {
            return (OrigMouseDown == Cell-FLevel.Width) ? 1 :
                   (OrigMouseDown == Cell-           1) ? 2 :
                   (OrigMouseDown == Cell+           1) ? 3 :
                   (OrigMouseDown == Cell+FLevel.Width) ? 4 : 0;
        }

        private void ESMainArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (FState == ESMainAreaState.Push)
            {
                Point MouseAt = Renderer.CellFromPixel(e.Location);
                OrigMouseDown = MouseAt.Y * FLevel.Width + MouseAt.X;
            }
        }

        private void ESMainArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (FState == ESMainAreaState.Push)
            {
                Point MouseAt = Renderer.CellFromPixel(e.Location);
                int Cell = MouseAt.Y * FLevel.Width + MouseAt.X;

                if (Cell != MouseOverCell)
                {
                    MouseOverCell = Cell;
                    UpdatePushPath(PushFinder.getMoves(Cell, GetOrigMouseDownDir(Cell)));
                }
            }
        }

        private void ESMainArea_MouseUp(object sender, MouseEventArgs e)
        {
            if (FState == ESMainAreaState.Null)
                return;

            Point Clicked = Renderer.CellFromPixel(e.Location);
            int Cell = Clicked.Y * FLevel.Width + Clicked.X;

            if (FState == ESMainAreaState.Editing)
            {
                SokobanCell CellType = FLevel.Cell(Cell);
                if (Tool == ESMainAreaTool.Wall)
                {
                    if (FLevel.SokobanX != Clicked.X || FLevel.SokobanY != Clicked.Y)
                    {
                        FLevel.SetCell(Clicked.X, Clicked.Y,
                            CellType == SokobanCell.Wall ? SokobanCell.Blank : SokobanCell.Wall);
                        SndThreadDone.Play();
                    }
                    else SndMeep.Play();
                }
                else if (Tool == ESMainAreaTool.Piece)
                {
                    if ((FLevel.SokobanX != Clicked.X || FLevel.SokobanY != Clicked.Y)
                                && CellType != SokobanCell.Wall)
                    {
                        FLevel.SetCell(Clicked.X, Clicked.Y,
                            CellType == SokobanCell.PieceOnTarget ? SokobanCell.Target :
                            CellType == SokobanCell.Blank         ? SokobanCell.Piece :
                            CellType == SokobanCell.Target        ? SokobanCell.PieceOnTarget :
                                                                    SokobanCell.Blank);
                        SndPiecePlaced.Play();
                    }
                    else SndMeep.Play();
                }
                else if (Tool == ESMainAreaTool.Target)
                {
                    if (CellType != SokobanCell.Wall)
                    {
                        FLevel.SetCell(Clicked.X, Clicked.Y,
                            CellType == SokobanCell.PieceOnTarget ? SokobanCell.Piece :
                            CellType == SokobanCell.Blank         ? SokobanCell.Target :
                            CellType == SokobanCell.Target        ? SokobanCell.Blank :
                                                                    SokobanCell.PieceOnTarget);
                        SndPiecePlaced.Play();
                    }
                    else SndMeep.Play();
                }
                else if (Tool == ESMainAreaTool.Sokoban)
                {
                    if (CellType != SokobanCell.Wall &&
                        CellType != SokobanCell.Piece &&
                        CellType != SokobanCell.PieceOnTarget)
                    {
                        Invalidate(RoundedRectangle(Renderer.GetCellRectForImage(FLevel.SokobanPos)));
                        FLevel.SetSokobanPos(Clicked.X, Clicked.Y);
                        SndPiecePlaced.Play();
                    }
                    else SndMeep.Play();
                }
                int PrevSizeX = FLevel.Width;
                int PrevSizeY = FLevel.Height;
                FLevel.EnsureSpace();
                if (FLevel.Width != PrevSizeX || FLevel.Height != PrevSizeY)
                    Refresh();
                else
                    Invalidate(RoundedRectangle(Renderer.GetCellRectForImage(Clicked.X, Clicked.Y)));
            }
            else if (FLevel.IsPiece(Cell) &&
                     FState != ESMainAreaState.Solved &&
                     !(PushFinder != null &&
                       GetOrigMouseDownDir(Cell) != 0 &&
                       PushFinder.PushValid(Cell, GetOrigMouseDownDir(Cell))))
            {
                if (MoveFinder != null && !MoveFinder.Done)
                    MoveFinder.RunToCompletion(Cell);

                if (MoveFinder.MoveValid(Cell + FLevel.Width) ||
                    MoveFinder.MoveValid(Cell - FLevel.Width) ||
                    MoveFinder.MoveValid(Cell + 1) ||
                    MoveFinder.MoveValid(Cell - 1))
                {
                    SelX = Clicked.X;
                    SelY = Clicked.Y;
                    Consider = 0;
                    MouseOverCell = 0;
                    PrevCellSequence = null;
                    PushFinder = new ESPushFinder(FLevel, SelX, SelY, this,
                        new PushFoundCallback(PushFound),
                        new PushFinderDoneCallback(PushFinderDone),
                        MoveFinder);
                    FState = ESMainAreaState.Push;
                    Invalidate();
                }
                else SndMeep.Play();
            }
            else if (FState == ESMainAreaState.Push && PushFinder != null)
            {
                if (!PushFinder.Done && !PushFinder.PushValid(Cell))
                {
                    Consider = Cell;
                    ConsiderDirection = GetOrigMouseDownDir(Cell);
                }
                else
                    ProcessPushClick(Cell, GetOrigMouseDownDir(Cell));
            }
        }

        private void ProcessPushClick(int Cell, int Direction)
        {
            if (!PushFinder.PushValid(Cell))
            {
                SndMeep.Play();
                return;
            }

            // Remove all the move and push colourings
            CreateGraphics().DrawImage(Buffer, 0, 0);

            // Move the Sokoban around visibly
            int[][] Moves = PushFinder.getMoves(Cell, Direction);
            Graphics g = Graphics.FromImage(Buffer);
            int OrigSokPos = FLevel.SokobanPos;
            int OrigPushPos = -1, LastPushPos = -1;
            bool EverPushed = false;
            for (int i = 0; i < Moves.Length; i++)
                if (Moves[i] != null)
                    for (int j = 0; j < Moves[i].Length; j++)
                        if (Moves[i][j] != 0)
                        {
                            System.Threading.Thread.Sleep(20);
                            int PrevSokPos = FLevel.SokobanPos;
                            int NewSokPos = PrevSokPos + Moves[i][j];
                            if (FLevel.IsPiece(FLevel.SokobanPos + Moves[i][j]))
                            {
                                // need to push
                                int PushTo = NewSokPos + Moves[i][j];
                                FLevel.MovePiece(NewSokPos, PushTo);
                                FLevel.SetSokobanPos(NewSokPos);
                                Renderer.RenderCell(g, PushTo);
                                if (!EverPushed)
                                {
                                    OrigPushPos = NewSokPos;
                                    EverPushed = true;
                                }
                                LastPushPos = PushTo;
                            }
                            else
                                // just move Sokoban
                                FLevel.SetSokobanPos(NewSokPos);
                            Renderer.RenderCell(g, NewSokPos);
                            Renderer.RenderCell(g, PrevSokPos);
                            CreateGraphics().DrawImage(Buffer, 0, 0);
                        }

            if (FLevel.Solved)
                LevelSolved();
            else
            {
                SndPiecePlaced.Play();
                AddUndo(OrigSokPos, OrigPushPos != -1, OrigPushPos, LastPushPos);
                FState = ESMainAreaState.Move;
                ReinitMoveFinder();
                Refresh();
            }
            if (MoveMade != null)
                MoveMade(this, new EventArgs());
        }

        private void LevelSolved()
        {
            FState = ESMainAreaState.Solved;
            SndLevelSolved.Play();
            Refresh();
        }

        private void MoveFound(int Pos)
        {
            if (Pos == FLevel.SokobanPos) return;
            if (FState != ESMainAreaState.Push || !PushFinder.PushValid(Pos))
                CreateGraphics().FillRectangle(MoveBrush, Renderer.GetCellRect(Pos));
        }

        private void MoveFinderDone()
        {
        }

        private void PushFound(int pos)
        {
            if (pos == Consider)
                ProcessPushClick(Consider, ConsiderDirection);
            else 
                Invalidate(RoundedRectangle(Renderer.GetCellRect(pos)));
        }

        private void PushFinderDone(bool AnythingPossible)
        {
            if (!AnythingPossible && Consider == 0)
                SndMeep.Play();
            else if (Consider != 0)
                ProcessPushClick(Consider, ConsiderDirection);
            else
                SndThreadDone.Play();
        }

        public void SetLevel(SokobanLevel Level)
        {
            FLevel = Level;
            FLevel.EnsureSpace();
            Renderer = new ESRenderer(FLevel, ClientSize);
            FState = ESMainAreaState.Move;
            ReinitMoveFinder();
            Refresh();
        }
    }
}
