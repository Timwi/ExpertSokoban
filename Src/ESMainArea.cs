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
    }
    public enum ESMainAreaTool
    {
        Wall, Piece, Target, Sokoban
    }
    public enum ESPathDrawMode
    {
        None, Line, Arrows, Dots
    }
    public class ESUndo { }
    public class ESUndoMove : ESUndo
    {
        public Point MovedFrom, MovedTo;
        public ESUndoMove(Point From, Point To) { MovedFrom = From; MovedTo = To; }
    }
    public class ESUndoPush : ESUndo
    {
        public Point MovedSokobanFrom, MovedSokobanTo, MovedPieceFrom, MovedPieceTo;
        public ESUndoPush(Point SFrom, Point STo, Point PFrom, Point PTo)
        {
            MovedSokobanFrom = SFrom;
            MovedSokobanTo = STo;
            MovedPieceFrom = PFrom;
            MovedPieceTo = PTo;
        }
    }

    public class ESMainArea : DoubleBufferedPanel
    {
        public ESMainAreaState State { get { return FState; } }
        public ESPathDrawMode MoveDrawMode { get { return FMoveDrawMode; } set { FMoveDrawMode = value; Invalidate(); } }
        public ESPathDrawMode PushDrawMode { get { return FPushDrawMode; } set { FPushDrawMode = value; Invalidate(); } }
        public bool ShowEndPos { get { return FShowEndPos; } set { FShowEndPos = value; Invalidate(); } }
        
        public event EventHandler MoveMade;

        private SokobanLevel FLevel;
        private ESRenderer Renderer;
        private ESMoveFinder MoveFinder;
        private ESPushFinder PushFinder;
        private ESMainAreaState FState;
        private ESMainAreaTool Tool;
        private ESPathDrawMode FMoveDrawMode, FPushDrawMode;
        private bool FShowEndPos;

        private Brush MoveBrush = new SolidBrush(Color.FromArgb(32, 0, 255, 0));
        private Brush PushBrush = new SolidBrush(Color.FromArgb(32, 0, 0, 255));
        private Pen MovePen = new Pen(Color.FromArgb(128, 0, 255, 0), 1.5f);
        private Pen PushPen = new Pen(Color.FromArgb(128, 0, 0, 255), 1.5f);
        private Pen MovePathPen = new Pen(Color.FromArgb(255, 0, 192, 0), 2f);
        private Pen PushPathPen = new Pen(Color.FromArgb(0, 0, 0x80), 3.5f);
        private Brush MovePathBrush = new SolidBrush(Color.FromArgb(0, 128, 0));
        private Brush PushPathBrush = new SolidBrush(Color.FromArgb(0, 0, 128));
        private SoundPlayer SndLevelSolved, SndMeep, SndPiecePlaced, SndEditorClick;

        private Point? Sel, OrigMouseDown, MouseOverCell;
        private Stack<ESUndo> FUndo = new Stack<ESUndo>();

        // Push path, but encoded as a sequence of cell co-ordinates.
        private Point[] PushCellSequence;
        private Point[] MoveCellSequence;
        private Point CellSeqSokoban;

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
                    Renderer.DrawCell(e.Graphics, Sel.Value, SokobanImage.PieceSelected);
                    if (PushCellSequence != null)
                    {
                        if (ShowEndPos)
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
                        }

                        if (FMoveDrawMode == ESPathDrawMode.Arrows)
                            DrawArrowSequence(e.Graphics, FLevel.SokobanPos, MoveCellSequence,
                                -Renderer.CellWidth/4, -Renderer.CellHeight/4);
                        else if (FMoveDrawMode == ESPathDrawMode.Line && MoveCellSequence.Length > 0)
                            e.Graphics.DrawPath(MovePathPen, Renderer.LinePath(FLevel.SokobanPos, MoveCellSequence, 0.7f, 0.7f));
                        else if (FMoveDrawMode == ESPathDrawMode.Dots)
                            for (int i = 0; i < MoveCellSequence.Length; i++)
                            {
                                RectangleF CellRect = Renderer.CellRect(MoveCellSequence[i]);
                                e.Graphics.FillEllipse(MovePathBrush,
                                    CellRect.Left + Renderer.CellWidth*2/5,
                                    CellRect.Top + Renderer.CellHeight*2/5,
                                    Renderer.CellWidth/5, Renderer.CellHeight/5);
                            }

                        if (FPushDrawMode == ESPathDrawMode.Arrows)
                            DrawArrowSequence(e.Graphics, Sel.Value, PushCellSequence, 0, 0);
                        else if (FPushDrawMode == ESPathDrawMode.Line && PushCellSequence.Length > 0)
                            e.Graphics.DrawPath(PushPathPen, Renderer.LinePath(Sel.Value, PushCellSequence, 0.7f, 0.7f));
                        else if (FPushDrawMode == ESPathDrawMode.Dots)
                            for (int i = 0; i < PushCellSequence.Length; i++)
                            {
                                RectangleF CellRect = Renderer.CellRect(PushCellSequence[i]);
                                e.Graphics.FillEllipse(PushPathBrush,
                                    CellRect.Left + Renderer.CellWidth/3,
                                    CellRect.Top + Renderer.CellHeight/3,
                                    Renderer.CellWidth/3, Renderer.CellHeight/3);
                            }
                    }
                }
            }
        }

        private void DrawArrowSequence(Graphics g, Point InitialPos, Point[] CellSequence,
            float InflateX, float InflateY)
        {
            Point PrevCell = InitialPos;
            for (int i = 0; i < CellSequence.Length; i++)
            {
                RectangleF CellRect = Renderer.CellRect(CellSequence[i]);
                Image Image;
                if (PrevCell.X == CellSequence[i].X && PrevCell.Y == CellSequence[i].Y-1)
                {
                    CellRect.Offset(0, -Renderer.CellHeight/2);
                    Image = Properties.Resources.ArrowDown;
                }
                if (PrevCell.X == CellSequence[i].X-1 && PrevCell.Y == CellSequence[i].Y)
                {
                    CellRect.Offset(-Renderer.CellWidth/2, 0);
                    Image = Properties.Resources.ArrowRight;
                }
                if (PrevCell.X == CellSequence[i].X+1 && PrevCell.Y == CellSequence[i].Y)
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

        private void UpdatePushPath(Point[] NewMoveCellSequence)
        {
            if (NewMoveCellSequence == null)
            {
                ClearPushPath();
                return;
            }

            Point SokPos = FLevel.SokobanPos;
            Point PiecePos = Sel.Value;

            // First determine the number of actual pushes in the path
            int Pushes = 0;
            foreach (Point Elem in NewMoveCellSequence)
            {
                if (Elem.Equals(PiecePos))
                {
                    Pushes++;
                    PiecePos.Offset(PiecePos.X-SokPos.X, PiecePos.Y-SokPos.Y);
                }
                SokPos = Elem;
            }

            // Now create the sequences of cells that make up the paths
            SokPos = FLevel.SokobanPos;
            PiecePos = Sel.Value;
            Point[] NewPushCellSequence = new Point[Pushes];
            int PushIndex = 0;
            foreach (Point Elem in NewMoveCellSequence)
            {
                if (Elem.Equals(PiecePos))
                {
                    PiecePos.Offset(PiecePos.X-SokPos.X, PiecePos.Y-SokPos.Y);
                    NewPushCellSequence[PushIndex] = PiecePos;
                    PushIndex++;
                }
                SokPos = Elem;
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
        private int OrigMouseDownDir(Point Cell)
        {
            if (OrigMouseDown == null) return 0;
            return (OrigMouseDown.Value.X == Cell.X && OrigMouseDown.Value.Y == Cell.Y-1) ? 1 :
                   (OrigMouseDown.Value.X == Cell.X-1 && OrigMouseDown.Value.Y == Cell.Y) ? 2 :
                   (OrigMouseDown.Value.X == Cell.X+1 && OrigMouseDown.Value.Y == Cell.Y) ? 3 :
                   (OrigMouseDown.Value.X == Cell.X && OrigMouseDown.Value.Y == Cell.Y+1) ? 4 : 0;
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
                Point Cell = Renderer.CellFromPixel(e.Location);
                if (MouseOverCell == null || !Cell.Equals(MouseOverCell.Value))
                {
                    MouseOverCell = Cell;
                    UpdatePushPath(PushFinder.Path(Cell, OrigMouseDownDir(Cell)));
                }
            }
        }

        private void ESMainArea_MouseUp(object sender, MouseEventArgs e)
        {
            if (FState == ESMainAreaState.Null)
                return;

            Point Cell = Renderer.CellFromPixel(e.Location);

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
                       OrigMouseDownDir(Cell) != 0 &&
                       PushFinder.GetDir(Cell, OrigMouseDownDir(Cell))))
            {
                if (MoveFinder.Get(Cell.X, Cell.Y+1) ||
                    MoveFinder.Get(Cell.X, Cell.Y-1) ||
                    MoveFinder.Get(Cell.X+1, Cell.Y) ||
                    MoveFinder.Get(Cell.X-1, Cell.Y))
                {
                    Sel = Cell;
                    MouseOverCell = null;
                    PushCellSequence = null;
                    PushFinder = new ESPushFinder(FLevel, Cell, MoveFinder);
                    FState = ESMainAreaState.Push;
                    Invalidate();
                }
                else SndMeep.Play();
            }

            // If the user clicked a cell where the selected piece can't be pushed, meep
            else if (FState == ESMainAreaState.Push && (PushFinder == null || !PushFinder.Get(Cell)))
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
                Point OrigSokPos = FLevel.SokobanPos;
                Point? OrigPushPos = null, LastPushPos = null;
                bool EverPushed = false;
                foreach (Point Move in MoveCellSequence)
                {
                    System.Threading.Thread.Sleep(20);
                    Point PrevSokPos = FLevel.SokobanPos;
                    if (FLevel.IsPiece(Move))
                    {
                        // need to push a piece
                        Point PushTo = new Point(2*Move.X-PrevSokPos.X, 2*Move.Y-PrevSokPos.Y);
                        FLevel.MovePiece(Move, PushTo);
                        FLevel.SetSokobanPos(Move);
                        Renderer.RenderCell(g, PushTo);
                        if (!EverPushed)
                        {
                            OrigPushPos = Move;
                            EverPushed = true;
                        }
                        LastPushPos = PushTo;
                    }
                    else
                        // just move Sokoban
                        FLevel.SetSokobanPos(Move);
                    Renderer.RenderCell(g, Move);
                    Renderer.RenderCell(g, PrevSokPos);
                    CreateGraphics().DrawImage(Buffer, 0, 0);
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
                    Sel = null;
                    SndPiecePlaced.Play();
                    if (OrigPushPos == null)
                        FUndo.Push(new ESUndoMove(OrigSokPos, FLevel.SokobanPos));
                    else
                        FUndo.Push(new ESUndoPush(OrigSokPos, FLevel.SokobanPos, OrigPushPos.Value, LastPushPos.Value));
                    FState = ESMainAreaState.Move;
                    ReinitMoveFinder();
                    Invalidate();
                }

                // Fire the MoveMade event
                if (MoveMade != null)
                    MoveMade(this, new EventArgs());
            }
            OrigMouseDown = null;
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

        public void Undo()
        {
            if (FState == ESMainAreaState.Editing || FState == ESMainAreaState.Solved ||
                FUndo.Count == 0 || FLevel == null)
                return;

            ESUndo Item = FUndo.Pop();

            if (Item is ESUndoMove)
                FLevel.SetSokobanPos((Item as ESUndoMove).MovedFrom);
            else
            {
                ESUndoPush ItemPush = Item as ESUndoPush;
                FLevel.SetSokobanPos(ItemPush.MovedSokobanFrom);
                FLevel.MovePiece(ItemPush.MovedPieceTo, ItemPush.MovedPieceFrom);
            }

            FState = ESMainAreaState.Move;
            ReinitMoveFinder();
            Refresh();
        }
    }
}
