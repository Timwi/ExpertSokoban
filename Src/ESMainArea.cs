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
        private SoundPlayer SndLevelSolved, SndMeep, SndPiecePlaced, SndThreadDone;

        private int SelX, SelY, OrigMouseDown, MouseOverCell;
        private int[][] UndoBuffer;

        // Push path, but encoded as a sequence of cell co-ordinates.
        private int[] PrevCellSequence;

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

        private void ReinitMoveFinder()
        {
            MoveFinder = new ESMoveFinder(FLevel);
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

        private void UpdatePushPath(List<int> NewPushPath)
        {
            if (NewPushPath == null)
            {
                ClearPushPath();
                return;
            }

            int SokPos = FLevel.SokobanPos;
            int PiecePos = SelY * FLevel.Width + SelX;

            // First determine the number of actual pushes in the path
            int Pushes = 0;
            foreach (int Elem in NewPushPath)
            {
                if (PiecePos == SokPos + Elem)
                {
                    Pushes++;
                    PiecePos += Elem;
                }
                SokPos += Elem;
            }

            // Now create the sequence of cells that make up the push path
            SokPos = FLevel.SokobanPos;
            PiecePos = SelY * FLevel.Width + SelX;
            int[] CellSequence = new int[Pushes];
            int Index = 0;
            foreach (int Elem in NewPushPath)
            {
                if (PiecePos == SokPos + Elem)
                {
                    PiecePos += Elem;
                    CellSequence[Index] = PiecePos;
                    Index++;
                }
                SokPos += Elem;
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
                    UpdatePushPath(PushFinder.Path(Cell, GetOrigMouseDownDir(Cell)));
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

            // If the user clicked on a piece, initiate the PushFinder,
            // even if we are already in State==Push (because we want to
            // be able to select another piece).
            // Special case: if the user dragged from next to a piece onto
            // the piece, and this signifies a valid push according to the
            // current PushFinder, then we want to execute the push instead.
            else if (FLevel.IsPiece(Cell) &&
                     FState != ESMainAreaState.Solved &&
                     !(PushFinder != null &&
                       GetOrigMouseDownDir(Cell) != 0 &&
                       PushFinder.PushValid(Cell, GetOrigMouseDownDir(Cell))))
            {
                if (MoveFinder.MoveValid(Cell + FLevel.Width) ||
                    MoveFinder.MoveValid(Cell - FLevel.Width) ||
                    MoveFinder.MoveValid(Cell + 1) ||
                    MoveFinder.MoveValid(Cell - 1))
                {
                    SelX = Clicked.X;
                    SelY = Clicked.Y;
                    MouseOverCell = 0;
                    PrevCellSequence = null;
                    PushFinder = new ESPushFinder(FLevel, SelX, SelY, MoveFinder);
                    FState = ESMainAreaState.Push;
                    Invalidate();
                }
                else SndMeep.Play();
            }

            // If the user clicked a cell where the selected piece can't be pushed, meep
            else if (FState == ESMainAreaState.Push && PushFinder != null && !PushFinder.PushValid(Cell))
                SndMeep.Play();

            // If the user clicked a cell where the selected piece *CAN* be pushed, execute the push
            else if (FState == ESMainAreaState.Push && PushFinder != null)
            {
                // Remove all the move and push colourings
                CreateGraphics().DrawImage(Buffer, 0, 0);

                // Move the Sokoban around visibly
                Graphics g = Graphics.FromImage(Buffer);
                int OrigSokPos = FLevel.SokobanPos;
                int OrigPushPos = -1, LastPushPos = -1;
                bool EverPushed = false;
                foreach (int Move in PushFinder.Path(Cell, GetOrigMouseDownDir(Cell)))
                {
                    if (Move != 0)
                    {
                        System.Threading.Thread.Sleep(20);
                        int PrevSokPos = FLevel.SokobanPos;
                        int NewSokPos = PrevSokPos + Move;
                        if (FLevel.IsPiece(FLevel.SokobanPos + Move))
                        {
                            // need to push
                            int PushTo = NewSokPos + Move;
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
                }

                // Did this push solved the level?
                if (FLevel.Solved)
                {
                    FState = ESMainAreaState.Solved;
                    SndLevelSolved.Play();
                    Refresh();
                }
                else
                {
                    // Make sure that selecting the same piece again is possible
                    SelX = -1;
                    SelY = -1;
                    OrigMouseDown = -1;

                    SndPiecePlaced.Play();
                    AddUndo(OrigSokPos, OrigPushPos != -1, OrigPushPos, LastPushPos);
                    FState = ESMainAreaState.Move;
                    ReinitMoveFinder();
                    Invalidate();
                }

                // Fire the MoveMade event
                if (MoveMade != null)
                    MoveMade(this, new EventArgs());
            }
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
