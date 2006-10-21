using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Media;
using System.Drawing.Drawing2D;
using RT.Controls;
using System.Drawing.Imaging;
using RT.Util;

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
    public enum ESPathDrawMode
    {
        None, Line, Arrows
    };

    public class ESMainArea : DoubleBufferedPanel
    {
        public ESMainAreaState State { get { return FState; } }
        public ESPathDrawMode MoveDrawMode { get { return FMoveDrawMode; } set { FMoveDrawMode = value; Invalidate(); } }
        public ESPathDrawMode PushDrawMode { get { return FPushDrawMode; } set { FPushDrawMode = value; Invalidate(); } }
        
        public event EventHandler MoveMade;

        private SokobanLevel FLevel;
        private ESRenderer Renderer;
        private ESMoveFinder MoveFinder;
        private ESPushFinder PushFinder;
        private ESMainAreaState FState;
        private ESMainAreaTool Tool;
        private ESPathDrawMode FMoveDrawMode, FPushDrawMode;

        private Brush MoveBrush = new SolidBrush(Color.FromArgb(32, 0, 255, 0));
        private Brush PushBrush = new SolidBrush(Color.FromArgb(32, 0, 0, 255));
        private Pen MovePen = new Pen(Color.FromArgb(128, 0, 255, 0), 1.5f);
        private Pen PushPen = new Pen(Color.FromArgb(128, 0, 0, 255), 1.5f);
        private Pen MovePathPen = new Pen(Color.FromArgb(255, 0, 192, 0), 2f);
        private Pen PushPathPen = new Pen(Color.FromArgb(255, 128, 0, 0), 3.5f);
        private SoundPlayer SndLevelSolved, SndMeep, SndPiecePlaced, SndEditorClick;

        private int SelX, SelY, OrigMouseDown, MouseOverCell;
        private int[][] UndoBuffer;

        // Push path, but encoded as a sequence of cell co-ordinates.
        private int[] PushCellSequence;
        private int[] MoveCellSequence;
        private int CellSeqSokoban;

        public ESMainArea()
        {
            Init();
            FMoveDrawMode = ESPathDrawMode.Line;
            FPushDrawMode = ESPathDrawMode.Arrows;
        }

        private void Init()
        {
            FLevel = null;
            Renderer = null;
            FState = ESMainAreaState.Null;
            SndLevelSolved = new SoundPlayer(Properties.Resources.SndLevelDone);
            SndMeep = new SoundPlayer(Properties.Resources.SndMeep);
            SndPiecePlaced = new SoundPlayer(Properties.Resources.SndPiecePlaced);
            SndEditorClick = new SoundPlayer(Properties.Resources.SndEditorClick);
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
                Image ImgLevelSolved = Properties.Resources.ImgLevelSolved;
                if (ClientSize.Width < ImgLevelSolved.Width)
                    e.Graphics.DrawImage(ImgLevelSolved,
                        0, (ClientSize.Height - ClientSize.Width * ImgLevelSolved.Height / ImgLevelSolved.Width)/2,
                        ClientSize.Width, ClientSize.Width * ImgLevelSolved.Height / ImgLevelSolved.Width);
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
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                if (MoveFinder != null && (State == ESMainAreaState.Move || State == ESMainAreaState.Push))
                {
                    GraphicsPath Path = Renderer.ValidPath(MoveFinder);
                    e.Graphics.FillPath(MoveBrush, Path);
                    e.Graphics.DrawPath(MovePen, Path);
                }
                if (PushFinder != null && State == ESMainAreaState.Push)
                {
                    GraphicsPath Path = Renderer.ValidPath(PushFinder);
                    e.Graphics.FillPath(PushBrush, Path);
                    e.Graphics.DrawPath(PushPen, Path);
                }
                if (FState == ESMainAreaState.Push)
                {
                    Renderer.DrawCell(e.Graphics, SelX, SelY, SokobanImage.PieceSelected);
                    if (PushCellSequence != null)
                    {
                        // Draw would-be Sokoban
                        BitmapUtil.DrawImageAlpha(e.Graphics, Properties.Resources.ImgSokoban,
                            RoundedRectangle(Renderer.CellRectForImage(CellSeqSokoban)), 0.5f);
                        // Draw piece end position
                        if (PushCellSequence.Length > 0)
                            BitmapUtil.DrawImageAlpha(e.Graphics,
                                FLevel.Cell(PushCellSequence[PushCellSequence.Length-1]) == SokobanCell.Target
                                ? Properties.Resources.ImgPieceTarget : Properties.Resources.ImgPiece,
                                RoundedRectangle(Renderer.CellRectForImage(PushCellSequence[PushCellSequence.Length-1])), 0.5f);

                        if (FMoveDrawMode == ESPathDrawMode.Arrows)
                            DrawArrowSequence(e.Graphics, FLevel.SokobanPos, MoveCellSequence,
                                -Renderer.CellWidth/4, -Renderer.CellHeight/4);
                        else if (FMoveDrawMode == ESPathDrawMode.Line && MoveCellSequence.Length > 0)
                            e.Graphics.DrawPath(MovePathPen, Renderer.LinePath(FLevel.SokobanPos, MoveCellSequence, 0.7f, 0.7f));

                        if (FPushDrawMode == ESPathDrawMode.Arrows)
                            DrawArrowSequence(e.Graphics, SelX + FLevel.Width*SelY, PushCellSequence, 0, 0);
                        else if (FPushDrawMode == ESPathDrawMode.Line && PushCellSequence.Length > 0)
                            e.Graphics.DrawPath(PushPathPen, Renderer.LinePath(SelY*FLevel.Width + SelX, PushCellSequence, 0.7f, 0.7f));
                    }
                }
            }
        }

        private void DrawArrowSequence(Graphics g, int InitialPos, int[] CellSequence,
            float InflateX, float InflateY)
        {
            int PrevCell = InitialPos;
            for (int i = 0; i < CellSequence.Length; i++)
            {
                RectangleF CellRect = Renderer.CellRect(CellSequence[i]);
                Image Image;
                if (PrevCell == CellSequence[i]-FLevel.Width)
                {
                    CellRect.Offset(0, -Renderer.CellHeight/2);
                    Image = Properties.Resources.ArrowDown;
                }
                else if (PrevCell == CellSequence[i]-1)
                {
                    CellRect.Offset(-Renderer.CellWidth/2, 0);
                    Image = Properties.Resources.ArrowRight;
                }
                else if (PrevCell == CellSequence[i]+1)
                {
                    CellRect.Offset(Renderer.CellWidth/2, 0);
                    Image = Properties.Resources.ArrowLeft;
                }
                else
                {
                    CellRect.Offset(0, Renderer.CellHeight/2);
                    Image = Properties.Resources.ArrowUp;
                }
                if (InflateX != 0 && InflateY != 0)
                    CellRect.Inflate(InflateX, InflateY);
                g.DrawImage(Image, CellRect);
                PrevCell = CellSequence[i];
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
            int[] NewPushCellSequence = new int[Pushes];
            int[] NewMoveCellSequence = new int[NewPushPath.Count];
            int PushIndex = 0;
            int MoveIndex = 0;
            foreach (int Elem in NewPushPath)
            {
                if (PiecePos == SokPos + Elem)
                {
                    PiecePos += Elem;
                    NewPushCellSequence[PushIndex] = PiecePos;
                    PushIndex++;
                }
                SokPos += Elem;
                NewMoveCellSequence[MoveIndex] = SokPos;
                MoveIndex++;
            }

            PushCellSequence = NewPushCellSequence;
            MoveCellSequence = NewMoveCellSequence;
            CellSeqSokoban = SokPos;
            Invalidate();
        }

        private void ClearPushPath()
        {
            if (PushCellSequence != null)
            {
                PushCellSequence = null;
                Invalidate();
            }
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
                OrigMouseDown = Renderer.CellFromPixel(e.Location);
        }

        private void ESMainArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (FState == ESMainAreaState.Push)
            {
                int Cell = Renderer.CellFromPixel(e.Location);

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

            int Cell = Renderer.CellFromPixel(e.Location);

            if (FState == ESMainAreaState.Editing)
            {
                SokobanCell CellType = FLevel.Cell(Cell);
                if (Tool == ESMainAreaTool.Wall)
                {
                    if (FLevel.SokobanPos != Cell)
                    {
                        FLevel.SetCell(Cell, CellType == SokobanCell.Wall ? SokobanCell.Blank : SokobanCell.Wall);
                        SndEditorClick.Play();
                    }
                    else SndMeep.Play();
                }
                else if (Tool == ESMainAreaTool.Piece)
                {
                    if (FLevel.SokobanPos != Cell && CellType != SokobanCell.Wall)
                    {
                        FLevel.SetCell(Cell,
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
                        FLevel.SetCell(Cell,
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
                        Invalidate(RoundedRectangle(Renderer.CellRectForImage(FLevel.SokobanPos)));
                        FLevel.SetSokobanPos(Cell);
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
                    Invalidate(RoundedRectangle(Renderer.CellRectForImage(Cell)));
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
                       PushFinder.Valid(Cell, GetOrigMouseDownDir(Cell))))
            {
                if (MoveFinder.Valid(Cell + FLevel.Width) ||
                    MoveFinder.Valid(Cell - FLevel.Width) ||
                    MoveFinder.Valid(Cell + 1) ||
                    MoveFinder.Valid(Cell - 1))
                {
                    SelX = Cell % FLevel.Width;
                    SelY = Cell / FLevel.Width;
                    MouseOverCell = 0;
                    PushCellSequence = null;
                    PushFinder = new ESPushFinder(FLevel, SelX, SelY, MoveFinder);
                    FState = ESMainAreaState.Push;
                    Invalidate();
                }
                else SndMeep.Play();
            }

            // If the user clicked a cell where the selected piece can't be pushed, meep
            else if (FState == ESMainAreaState.Push && (PushFinder == null || !PushFinder.Valid(Cell)))
            {
                SndMeep.Play();
            }

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
                            // need to push a piece
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

                // Did this push solve the level?
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
            OrigMouseDown = -1;
        }

        public void SetLevel(SokobanLevel Level)
        {
            FLevel = Level.Clone();
            FLevel.EnsureSpace();
            Renderer = new ESRenderer(FLevel, ClientSize);
            FState = ESMainAreaState.Move;
            ReinitMoveFinder();
            Refresh();
        }
    }
}
