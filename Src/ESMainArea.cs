using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Media;
using System.Drawing.Drawing2D;

namespace ExpertSokoban
{
    public enum ESMainAreaState
    {
        STATE_NULL, STATE_MOVE, STATE_PUSH, STATE_SOLVED, STATE_EDITING
    };
    public enum ESMainAreaTool
    {
        TOOL_WALL, TOOL_PIECE, TOOL_TARGET, TOOL_SOKOBAN
    };

    public class ESMainArea : Panel
    {
        private Image LevelImage;
        private SokobanLevel Level;
        private ESRenderer Renderer;
        private ESMoveFinder MoveFinder;
        private ESPushFinder PushFinder;
        private ESMainAreaState State;
        private ESMainAreaTool Tool;

        private Color ToolRectColour = Color.FromArgb(0, 0x80, 0);
        private Brush MoveBrush = new SolidBrush(Color.FromArgb(100, Color.LimeGreen));
        private Brush PushBrush = new SolidBrush(Color.FromArgb(100, Color.LightBlue));

        private int SelX, SelY, OrigMouseDown, ClickedOnCell,

                    // If, while State == STATE_PUSH, the user clicks somewhere
                    // where the PushFinder has not yet found a path to, but is
                    // still running, PleaseConsider (and PleaseConsiderDir) are
                    // set to the square clicked on. If the PushFinder encounters
                    // the square stored in PleaseConsider, the sequence is
                    // executed. For an explanation of PleaseConsiderDir, see
                    // GetOrigMouseDownDir().
                    PleaseConsider, PleaseConsiderDir;

        private SoundPlayer sndLevelSolved, sndMeep, sndPiecePlaced, sndThreadDone;
        private int[][] undoBuffer;

        private delegate void moveThreadFoundCallback(int pos);
        private delegate void moveThreadDoneCallback();
        private delegate void pushThreadFoundCallback(int pos);
        private delegate void pushThreadDoneCallback(bool anythingPossible);

        public ESMainArea()
        {
            Level = null;
            Renderer = null;
            State = ESMainAreaState.STATE_NULL;
            Init(false);
        }

        public ESMainArea(SokobanLevel level, bool editing)
        {
            Level = level;
            Renderer = new ESRenderer(Level, ClientSize);
            Init(editing);
            RedrawLevelImage();
        }

        private void Init(bool editing)
        {
            sndLevelSolved = new SoundPlayer(Properties.Resources.SndLevelDone);
            sndMeep = new SoundPlayer(Properties.Resources.SndMeep);
            sndPiecePlaced = new SoundPlayer(Properties.Resources.SndPiecePlaced);
            sndThreadDone = new SoundPlayer(Properties.Resources.SndThreadDone);
            this.MouseDown += new MouseEventHandler(ESMainArea_MouseDown);
            this.MouseMove += new MouseEventHandler(ESMainArea_MouseMove);
            this.MouseUp += new MouseEventHandler(ESMainArea_MouseUp);
            this.Paint += new PaintEventHandler(ESMainArea_Paint);
            this.Resize += new EventHandler(ESMainArea_Resize);
            Timer t = new Timer();
            t.Tick += new EventHandler(TimerTick);
            t.Interval = 1;
            t.Enabled = true;
        }

        private void ESMainArea_Resize(object sender, EventArgs e)
        {
            if (State == ESMainAreaState.STATE_NULL)
                return;

            Renderer = new ESRenderer(Level, ClientSize);
            RedrawLevelImage();
            Refresh();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (State == ESMainAreaState.STATE_MOVE && MoveFinder != null && !MoveFinder.isDone())
                MoveFinder.SingleStep();
            if (State == ESMainAreaState.STATE_PUSH && PushFinder != null && !PushFinder.isDone())
                PushFinder.SingleStep();
        }

        private void ReinitMoveFinder()
        {
            MoveFinder = new ESMoveFinder(Level, false, this,
                new moveThreadFoundCallback(moveThreadFound),
                new moveThreadDoneCallback(moveThreadDone));
        }

        private void addUndo(int prevSokPos, bool push, int fromPush, int toPush)
        {
            int n = 0;
            if (undoBuffer == null)
                undoBuffer = new int[1][];
            else
            {
                n = undoBuffer.Length;
                int[][] newUndoBuffer = new int[n+1][];
                for (int i = 0; i < undoBuffer.Length; i++)
                    newUndoBuffer[i] = undoBuffer[i];
                undoBuffer = newUndoBuffer;
            }

            int[] newElem = new int[push ? 3 : 1];
            newElem[0] = prevSokPos;
            if (push) { newElem[1] = fromPush; newElem[2] = toPush; }
            undoBuffer[n] = newElem;
        }

        public void undo()
        {
            if (undoBuffer == null) return;
            if (State == ESMainAreaState.STATE_EDITING || State == ESMainAreaState.STATE_SOLVED)
                return;
            if (Level == null) return;

            int[] extr = undoBuffer[undoBuffer.Length-1];
            if (undoBuffer.Length == 1) undoBuffer = null;
            else
            {
                int[][] newUndoBuffer = new int[undoBuffer.Length-1][];
                for (int i = 0; i < undoBuffer.Length-1; i++)
                    newUndoBuffer[i] = undoBuffer[i];
                undoBuffer = newUndoBuffer;
            }

            bool push = extr.Length > 1;
            Level.setSokobanPos(extr[0]);
            if (push) Level.movePiece(extr[2], extr[1]);

            PleaseConsider = 0;
            PleaseConsiderDir = 0;
            State = ESMainAreaState.STATE_MOVE;
            RedrawLevelImage();
            ReinitMoveFinder();
        }

        private void ESMainArea_Paint(object sender, PaintEventArgs e)
        {
            if (State != ESMainAreaState.STATE_NULL)
            {
                e.Graphics.DrawImage(LevelImage, 0, 0);
                for (int i = 0; i < Level.getSizeX()*Level.getSizeY(); i++)
                    if (PushFinder != null && PushFinder.pushValid(i) && State == ESMainAreaState.STATE_PUSH)
                        e.Graphics.FillRectangle(PushBrush, Renderer.GetCellRect(i));
                    else if (MoveFinder != null && MoveFinder.moveValid(i) &&
                        (State == ESMainAreaState.STATE_MOVE || State == ESMainAreaState.STATE_PUSH))
                        e.Graphics.FillRectangle(MoveBrush, Renderer.GetCellRect(i));
            }
        }

        public void RedrawLevelImage()
        {
            LevelImage = new Bitmap(ClientSize.Width, ClientSize.Height);
            Renderer.Render(Graphics.FromImage(LevelImage));
        }

        private void drawPushPath(int square, int posInDir)
        {
            Graphics cg = CreateGraphics();
            cg.DrawImage(LevelImage, 0, 0);
            int[][] moves = PushFinder.getMoves(square, posInDir);
            int curSokPos = Level.getSokobanPos();
            int curPiecePos = SelY * Level.getSizeX() + SelX;
            Brush b = new SolidBrush(Color.FromArgb(0, 0, 0x80));

            for (int i = 0; i < moves.Length; i++)
                if (moves[i] != null)
                    for (int j = 0; j < moves[i].Length; j++)
                        if (moves[i][j] != 0)
                        {
                            if (curPiecePos == curSokPos + moves[i][j])
                            {
                                // need to push
                                curPiecePos += moves[i][j];
                                curSokPos += moves[i][j];
                                int x = curPiecePos % Level.getSizeX();
                                int y = curPiecePos / Level.getSizeX();
                                float SquareW = Renderer.CellWidth;
                                cg.SmoothingMode = SmoothingMode.AntiAlias;
                                RectangleF CellRect = Renderer.GetCellRect(x, y);
                                cg.FillEllipse(b,
                                    CellRect.Left + Renderer.CellWidth/3,
                                    CellRect.Top + Renderer.CellHeight/3,
                                    Renderer.CellWidth/3, Renderer.CellHeight/3);
                            }
                            else
                                // just move Sokoban
                                curSokPos += moves[i][j];
                        }
        }

        // If you just click a cell to push a piece to, PushFinder will find
        // the shortest push-path regardless of where the Sokoban will end up.
        // If you mouse-down *next* to the cell you want to push the piece to and 
        // then *drag* onto the cell before releasing, then we will try to move
        // the piece in such a way that the Sokoban will end up on the cell you
        // clicked first. GetOrigMouseDownDir() determines in which direction you
        // dragged. This value is passed on to PushFinder.getMoves().
        private int GetOrigMouseDownDir(int cell)
        {
            int sx = Level.getSizeX();
            return (OrigMouseDown == cell-sx) ? 1 :
                   (OrigMouseDown == cell- 1) ? 2 :
                   (OrigMouseDown == cell+ 1) ? 3 :
                   (OrigMouseDown == cell+sx) ? 4 : 0;
        }

        private void ESMainArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (State == ESMainAreaState.STATE_PUSH)
            {
                Point Clicked = Renderer.CellFromPixel(e.Location);
                int Cell = Clicked.Y * Level.getSizeX() + Clicked.X;
                OrigMouseDown = Cell;
                ClickedOnCell = Cell;
                if (PushFinder.pushValid(Cell))
                    drawPushPath(Cell, GetOrigMouseDownDir(Cell));
            }
        }

        private void ESMainArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (State == ESMainAreaState.STATE_PUSH)
            {
                Point Clicked = Renderer.CellFromPixel(e.Location);
                int Cell = Clicked.Y * Level.getSizeX() + Clicked.X;

                if (Cell != ClickedOnCell)
                {
                    ClickedOnCell = Cell;
                    if (PushFinder.pushValid(Cell))
                        drawPushPath(Cell, GetOrigMouseDownDir(Cell));
                }
            }
        }

        private void ESMainArea_MouseUp(object sender, MouseEventArgs e)
        {
            Point Clicked = Renderer.CellFromPixel(e.Location);
            int Cell = Clicked.Y * Level.getSizeX() + Clicked.X;

            if (State == ESMainAreaState.STATE_EDITING)
            {
                Graphics g1 = Graphics.FromImage(LevelImage);
                Graphics g2 = CreateGraphics();
                SokobanCell CellType = Level.getCell(Cell);
                if (Tool == ESMainAreaTool.TOOL_WALL)
                {
                    if (Level.getSokobanX() != Clicked.X || Level.getSokobanY() != Clicked.Y)
                    {
                        Level.setCell(Clicked.X, Clicked.Y,
                            CellType == SokobanCell.Wall ? SokobanCell.Blank : SokobanCell.Wall);
                        Renderer.RenderCell(g1, Clicked.X, Clicked.Y);
                        Renderer.RenderCell(g2, Clicked.X, Clicked.Y);
                        sndThreadDone.Play();
                    }
                    else sndMeep.Play();
                }
                else if (Tool == ESMainAreaTool.TOOL_PIECE)
                {
                    if ((Level.getSokobanX() != Clicked.X || Level.getSokobanY() != Clicked.Y)
                                && CellType != SokobanCell.Wall)
                    {
                        Level.setCell(Clicked.X, Clicked.Y,
                            CellType == SokobanCell.PieceOnTarget ? SokobanCell.Target :
                                    CellType == SokobanCell.Blank ? SokobanCell.Piece :
                                    CellType == SokobanCell.Target ? SokobanCell.PieceOnTarget :
                                        SokobanCell.Blank);
                        Renderer.RenderCell(g1, Clicked.X, Clicked.Y);
                        Renderer.RenderCell(g2, Clicked.X, Clicked.Y);
                        sndPiecePlaced.Play();
                    }
                    else sndMeep.Play();
                }
                else if (Tool == ESMainAreaTool.TOOL_TARGET)
                {
                    if (CellType != SokobanCell.Wall)
                    {
                        Level.setCell(Clicked.X, Clicked.Y,
                            CellType == SokobanCell.PieceOnTarget ? SokobanCell.Piece :
                                    CellType == SokobanCell.Blank ? SokobanCell.Target :
                                    CellType == SokobanCell.Target ? SokobanCell.Blank :
                                        SokobanCell.PieceOnTarget);
                        Renderer.RenderCell(g1, Clicked.X, Clicked.Y);
                        Renderer.RenderCell(g2, Clicked.X, Clicked.Y);
                        sndPiecePlaced.Play();
                    }
                    else sndMeep.Play();
                }
                else if (Tool == ESMainAreaTool.TOOL_SOKOBAN)
                {
                    if (CellType != SokobanCell.Wall &&
                                CellType != SokobanCell.Piece &&
                                CellType != SokobanCell.PieceOnTarget)
                    {
                        int oldSokobanPos = Level.getSokobanPos();
                        Level.setSokobanPos(Clicked.X, Clicked.Y);
                        Renderer.RenderCell(g1, oldSokobanPos);
                        Renderer.RenderCell(g2, oldSokobanPos);
                        Renderer.RenderCell(g1, Clicked.X, Clicked.Y);
                        Renderer.RenderCell(g2, Clicked.X, Clicked.Y);
                        sndPiecePlaced.Play();
                    }
                    else sndMeep.Play();
                }
                int prevSizeX = Level.getSizeX();
                int prevSizeY = Level.getSizeY();
                Level.ensureSpace(true);
                if (Level.getSizeX() != prevSizeX || Level.getSizeY() != prevSizeY)
                    RedrawLevelImage();

            }
            else if (Level.isPiece(Cell) &&
                         State != ESMainAreaState.STATE_SOLVED &&
                         !(PushFinder != null &&
                           GetOrigMouseDownDir(Cell) != 0 &&
                           PushFinder.pushValid(Cell, GetOrigMouseDownDir(Cell))))
            {
                if (MoveFinder != null && !MoveFinder.isDone())
                    MoveFinder.RunToCompletion(Cell);

                if (MoveFinder.moveValid(Cell + Level.getSizeX()) ||
                        MoveFinder.moveValid(Cell - Level.getSizeX()) ||
                        MoveFinder.moveValid(Cell + 1) ||
                        MoveFinder.moveValid(Cell - 1))
                {
                    SelX = Clicked.X;
                    SelY = Clicked.Y;
                    Renderer.DrawCell(Graphics.FromImage(LevelImage), SelX, SelY, SokobanImage.PieceSelected);
                    CreateGraphics().DrawImage(LevelImage, 0, 0);
                    PleaseConsider = 0;
                    PushFinder = new ESPushFinder(Level, SelX, SelY, this,
                        new pushThreadFoundCallback(pushThreadFound),
                        new pushThreadDoneCallback(pushThreadDone),
                        MoveFinder);
                    State = ESMainAreaState.STATE_PUSH;
                }
                else sndMeep.Play();
            }
            else if (State == ESMainAreaState.STATE_PUSH && PushFinder != null)
            {
                if (!PushFinder.isDone() && !PushFinder.pushValid(Cell))
                {
                    PleaseConsider = Cell;
                    PleaseConsiderDir = GetOrigMouseDownDir(Cell);
                }
                else
                    processPushClick(Cell, GetOrigMouseDownDir(Cell));
            }
        }

        private void processPushClick(int square, int squareDir)
        {
            if (!PushFinder.pushValid(square))
            {
                sndMeep.Play();
                return;
            }

            int[][] moves = PushFinder.getMoves(square, squareDir);
            Graphics cg = CreateGraphics();
            cg.DrawImage(LevelImage, 0, 0);
            bool everPushed = false;
            int origPushPos = -1;
            int lastPushPos = -1;
            int origSokPos = Level.getSokobanPos();
            for (int i = 0; i < moves.Length; i++)
                if (moves[i] != null)
                    for (int j = 0; j < moves[i].Length; j++)
                        if (moves[i][j] != 0)
                        {
                            System.Threading.Thread.Sleep(20);
                            if (Level.isPiece(Level.getSokobanPos() + moves[i][j]))
                            {
                                // need to push
                                int prevSokPos = Level.getSokobanPos();
                                int newSokPos = prevSokPos + moves[i][j];
                                int pushTo = newSokPos + moves[i][j];
                                Level.movePiece(newSokPos, pushTo);
                                Level.setSokobanPos(newSokPos);
                                Renderer.RenderCell(cg, pushTo);
                                Renderer.RenderCell(cg, newSokPos);
                                Renderer.RenderCell(cg, prevSokPos);
                                if (!everPushed)
                                {
                                    origPushPos = newSokPos;
                                    everPushed = true;
                                }
                                lastPushPos = pushTo;
                            }
                            else
                            {
                                // just move Sokoban
                                int prevSokPos = Level.getSokobanPos();
                                int newSokPos = prevSokPos + moves[i][j];
                                Level.setSokobanPos(newSokPos);
                                Renderer.RenderCell(cg, newSokPos);
                                Renderer.RenderCell(cg, prevSokPos);
                            }
                        }

            Graphics pg = Graphics.FromImage(LevelImage);
            Renderer.RenderCell(pg, origSokPos);
            Renderer.RenderCell(pg, Level.getSokobanPos());
            if (origPushPos != -1) Renderer.RenderCell(pg, origPushPos);
            if (lastPushPos != -1) Renderer.RenderCell(pg, lastPushPos);

            if (Level.solved())
                LevelSolved(cg);
            else
            {
                sndPiecePlaced.Play();
                addUndo(origSokPos, origPushPos != -1, origPushPos, lastPushPos);
                cg.DrawImage(LevelImage, 0, 0);
                State = ESMainAreaState.STATE_MOVE;
                ReinitMoveFinder();
            }
        }

        private void LevelSolved(Graphics cg)
        {
            State = ESMainAreaState.STATE_SOLVED;
            Graphics g = Graphics.FromImage(LevelImage);
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            Image ImgLevelSolved = Properties.Resources.ImgLevelSolved;
            if (LevelImage.Width < ImgLevelSolved.Width)
                g.DrawImage(ImgLevelSolved, 
                    0, 
                    LevelImage.Height/2 - (LevelImage.Width * ImgLevelSolved.Height / LevelImage.Height)/2,
                    LevelImage.Width,
                    LevelImage.Width * ImgLevelSolved.Height / LevelImage.Height);
            else
                g.DrawImage (ImgLevelSolved,
                    LevelImage.Width/2 - ImgLevelSolved.Width/2,
                    LevelImage.Height/2 - ImgLevelSolved.Height/2);

            sndLevelSolved.Play();
        }

        private Image copyImage(Image i)
        {
            Image ret = new Bitmap(i.Width, i.Height);
            Graphics.FromImage(ret).DrawImage(i, 0, 0);
            return ret;
        }

        public void moveThreadFound(int pos)
        {
            if (pos == Level.getSokobanPos()) return;

            if (State != ESMainAreaState.STATE_PUSH || !PushFinder.pushValid(pos))
                Graphics.FromImage(LevelImage).FillRectangle(MoveBrush, Renderer.GetCellRect(pos));
            CreateGraphics().DrawImage(LevelImage, 0, 0);
        }

        public void moveThreadDone()
        {
            CreateGraphics().DrawImage(LevelImage, 0, 0);
        }

        public void pushThreadFound(int pos)
        {
            if (pos == PleaseConsider)
                processPushClick(PleaseConsider, PleaseConsiderDir);
            else if (!Level.isPiece(pos))
            {
                Graphics g = CreateGraphics();
                Renderer.RenderCell(g, pos);
                g.FillRectangle(PushBrush, Renderer.GetCellRect(pos));
            }
        }

        public void pushThreadDone(bool anythingPossible)
        {
            if (!anythingPossible && PleaseConsider == 0)
                sndMeep.Play();
            else if (PleaseConsider != 0)
                processPushClick(PleaseConsider, PleaseConsiderDir);
        }

        public void SetLevel(SokobanLevel NewLevel)
        {
            Level = NewLevel;
            Renderer = new ESRenderer(Level, ClientSize);
            State = ESMainAreaState.STATE_MOVE;
            RedrawLevelImage();
            ReinitMoveFinder();
            Refresh();
        }
    }
}
