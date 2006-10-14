using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Media;
using System.Threading;
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
        private Color ToolRectColour = Color.FromArgb(0, 0x80, 0);

        private SokobanLevel currentLevel;
        private ESMoveFinder moveFinder;
        private ESPushFinder pushFinder;
        private ESMainAreaState state;
        private ESMainAreaTool tool;
        private int squareSizeX, squareSizeY, mySizeX, mySizeY, toolbarX,
                    selX, selY, origPenDown, clickedOnSquare, originX, originY,

                    // If, while state == STATE_PUSH, the user clicks somewhere
                    // where the pushFinder has not yet found a path to, but is
                    // still running, pleaseConsider (and pleaseConsiderDir) are
                    // set to the square clicked on. If the pushFinder encounters
                    // the square stored in pleaseConsider, the sequence is
                    // executed. For an explanation of pleaseConsiderDir, see
                    // getOrigPenDownDir().
                    pleaseConsider, pleaseConsiderDir;

        private SoundPlayer sndLevelSolved, sndMeep, sndPiecePlaced, sndThreadDone;
        private Image plainDisplay, moveDisplay, pushDisplay;
        private int[][] undoBuffer;

        private delegate void moveThreadFoundCallback(int pos);
        private delegate void moveThreadDoneCallback();
        private delegate void pushThreadFoundCallback(int pos);
        private delegate void pushThreadDoneCallback(bool anythingPossible);

        public ESMainArea()
        {
            currentLevel = null;
            state = ESMainAreaState.STATE_NULL;
            Init(false);
        }

        public ESMainArea(SokobanLevel level, bool editing)
        {
            currentLevel = level;
            Init(editing);
        }

        private void Init(bool editing)
        {
            sndLevelSolved = new SoundPlayer(Properties.Resources.SndLevelDone);
            sndMeep = new SoundPlayer(Properties.Resources.SndMeep);
            sndPiecePlaced = new SoundPlayer(Properties.Resources.SndPiecePlaced);
            sndThreadDone = new SoundPlayer(Properties.Resources.SndThreadDone);
            reinitSize(editing);
            this.MouseDown += new MouseEventHandler(ESMainArea_MouseDown);
            this.MouseMove += new MouseEventHandler(ESMainArea_MouseMove);
            this.MouseUp += new MouseEventHandler(ESMainArea_MouseUp);
            this.Paint += new PaintEventHandler(ESMainArea_Paint);
            this.Resize += new EventHandler(ESMainArea_Resize);
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Tick += new EventHandler(TimerTick);
            t.Interval = 1;
            t.Enabled = true;
        }

        private void ESMainArea_Resize(object sender, EventArgs e)
        {
            if (currentLevel != null)
            {
                originX = ClientSize.Width/2 - mySizeX/2;
                originY = ClientSize.Height/2 - mySizeY/2;
                updatePlain();
                if (state == ESMainAreaState.STATE_MOVE || state == ESMainAreaState.STATE_PUSH)
                    makeMoveDisplay();
                if (state == ESMainAreaState.STATE_PUSH)
                    makePushDisplay();
                Refresh();
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (state == ESMainAreaState.STATE_MOVE && moveFinder != null && !moveFinder.isDone())
                moveFinder.run();
            if (state == ESMainAreaState.STATE_PUSH && pushFinder != null && !pushFinder.isDone())
                pushFinder.run();
        }

        public void reinitSize(bool editing)
        {
            if (currentLevel != null)
            {
                squareSizeX = Properties.Resources.ImgBlank.Width;
                squareSizeY = Properties.Resources.ImgBlank.Height;
                mySizeX = currentLevel.getSizeX() * squareSizeX;
                mySizeY = currentLevel.getSizeY() * squareSizeY;
                if (editing)
                {
                    mySizeY += squareSizeY + 2;
                    state = ESMainAreaState.STATE_EDITING;
                    tool = ESMainAreaTool.TOOL_WALL;
                    toolbarX = mySizeX/2 - (squareSizeX+2)*2;
                }
                else state = ESMainAreaState.STATE_MOVE;
                originX = ClientSize.Width/2 - mySizeX/2;
                originY = ClientSize.Height/2 - mySizeY/2;
                updatePlain();
                Refresh();
            }
        }
        public void initialise()
        {
            updatePlain();
            if (state != ESMainAreaState.STATE_EDITING)
            {
                undoBuffer = null;
                moveDisplay = copyImage(plainDisplay);
                reinitMoveFinder();
            }
        }
        private void reinitMoveFinder()
        {
            moveFinder = new ESMoveFinder(currentLevel, false, this,
                new moveThreadFoundCallback(moveThreadFound),
                new moveThreadDoneCallback(moveThreadDone));
            moveDisplay = copyImage(plainDisplay);
        }
        public int getMyWidth() { return mySizeX; }
        public int getMyHeight() { return mySizeY; }
        public int getSquareSizeX() { return squareSizeX; }
        public int getSquareSizeY() { return squareSizeY; }

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
            if (state == ESMainAreaState.STATE_EDITING || state == ESMainAreaState.STATE_SOLVED)
                return;
            if (currentLevel == null) return;

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
            currentLevel.setSokobanPos(extr[0]);
            if (push) currentLevel.movePiece(extr[2], extr[1]);

            pleaseConsider = 0;
            pleaseConsiderDir = 0;
            updatePlain();
            moveDisplay = copyImage(plainDisplay);
            reinitMoveFinder();
            state = ESMainAreaState.STATE_MOVE;
        }

        private void ESMainArea_Paint(object sender, PaintEventArgs e)
        {
            if (currentLevel != null)
            {
                e.Graphics.DrawImage((state == ESMainAreaState.STATE_PUSH) ? pushDisplay :
                                     (state == ESMainAreaState.STATE_EDITING) ? plainDisplay : moveDisplay, 0, 0);
                if (state == ESMainAreaState.STATE_EDITING)
                {
                    e.Graphics.DrawImage(Properties.Resources.ImgWall, 1+toolbarX+originX, mySizeY-squareSizeY-1+originY);
                    e.Graphics.DrawImage(Properties.Resources.ImgPiece, squareSizeX+3+toolbarX+originX, mySizeY-squareSizeY-1+originY);
                    e.Graphics.DrawImage(Properties.Resources.ImgTarget, 2*squareSizeX+5+toolbarX+originX, mySizeY-squareSizeY-1+originY);
                    e.Graphics.DrawImage(Properties.Resources.ImgSokoban, 3*squareSizeX+7+toolbarX+originX, mySizeY-squareSizeY-1+originY);
                    drawToolRect(e.Graphics, ToolRectColour);
                }
            }
        }
        private void drawToolRect(Graphics g, Color c)
        {
            int toolIndex = tool == ESMainAreaTool.TOOL_WALL ? 0 :
                            tool == ESMainAreaTool.TOOL_PIECE ? 1 :
                            tool == ESMainAreaTool.TOOL_TARGET ? 2 : 3;
            g.DrawRectangle(new Pen(c), 
                toolbarX + toolIndex*(squareSizeX+2) + originX, 
                mySizeY-squareSizeY-2+originY, 
                squareSizeX+1, squareSizeY+1);
        }

        private void drawSquare(int pos, Graphics g)
        { drawSquare(pos % currentLevel.getSizeX(), pos / currentLevel.getSizeX(), g); }
        private void drawSquare(int x, int y, Graphics g)
        {
            SokobanSquare cell = currentLevel.getCell(x, y);
            Image imageToDraw = getImageToDraw(cell,
                currentLevel.getSokobanX() == x && currentLevel.getSokobanY() == y);
            g.DrawImage(imageToDraw, originX+x*squareSizeX, originY+y*squareSizeY);
        }
        private Image getImageToDraw(SokobanSquare squareType, bool withSokoban)
        {
            return squareType == SokobanSquare.BLANK && withSokoban  ? Properties.Resources.ImgSokoban :
                   squareType == SokobanSquare.PIECE                 ? Properties.Resources.ImgPiece :
                   squareType == SokobanSquare.PIECE_ON_TARGET       ? Properties.Resources.ImgPieceTarget :
                   squareType == SokobanSquare.TARGET && withSokoban ? Properties.Resources.ImgSokobanTarget :
                   squareType == SokobanSquare.TARGET                ? Properties.Resources.ImgTarget :
                   squareType == SokobanSquare.WALL                  ? Properties.Resources.ImgWall :
                                                                       Properties.Resources.ImgBlank;
        }
        public void updatePlain()
        {
            plainDisplay = new Bitmap(ClientSize.Width, ClientSize.Height);
            Graphics ig = Graphics.FromImage(plainDisplay);
            for (int i = 0; i < currentLevel.getSizeX(); i++)
                for (int j = 0; j < currentLevel.getSizeY(); j++)
                    drawSquare(i, j, ig);
        }

        public void makeMoveDisplay()
        {
            moveDisplay = copyImage(plainDisplay);
            Graphics cg = Graphics.FromImage(moveDisplay);
            for (int i = 0; i < currentLevel.getSizeX(); i++)
                for (int j = 0; j < currentLevel.getSizeY(); j++)
                    if (moveFinder.moveValid(j*currentLevel.getSizeX() + i))
                    {
                        bool isTarget = currentLevel.getCell(i, j) == SokobanSquare.TARGET;
                        bool isSokoban = currentLevel.getSokobanX() == i && currentLevel.getSokobanY() == j;
                        cg.DrawImage(isSokoban ? Properties.Resources.ImgSokoban :
                                     isTarget  ? Properties.Resources.ImgCanMoveTarget :
                                                 Properties.Resources.ImgCanMove,
                            i*squareSizeX + originX, j*squareSizeY + originY);
                    }
        }

        public void makePushDisplay()
        {
            pushDisplay = copyImage(moveDisplay);
            Graphics cg = Graphics.FromImage(pushDisplay);
            for (int i = 0; i < currentLevel.getSizeX(); i++)
                for (int j = 0; j < currentLevel.getSizeY(); j++)
                    if (pushFinder.pushValid(j*currentLevel.getSizeX() + i))
                    {
                        bool isTarget = currentLevel.getCell(i, j) == SokobanSquare.TARGET;
                        bool isSokoban = currentLevel.getSokobanX() == i && currentLevel.getSokobanY() == j;
                        cg.DrawImage(isTarget && isSokoban ? Properties.Resources.ImgCanPushSokobanTarget :
                                      isTarget  ? Properties.Resources.ImgCanPushTarget :
                                      isSokoban ? Properties.Resources.ImgCanPushSokoban :
                                                  Properties.Resources.ImgCanPush,
                            i*squareSizeX + originX, j*squareSizeY + originY);
                    }
            cg.DrawImage(Properties.Resources.ImgPieceSelected, selX*squareSizeX, selY*squareSizeY);
        }

        private void drawPushPath(int square, int posInDir)
        {
            Graphics cg = CreateGraphics();
            cg.DrawImage(pushDisplay, 0, 0);
            int[][] moves = pushFinder.getMoves(square, posInDir);
            int curSokPos = currentLevel.getSokobanPos();
            int curPiecePos = selY * currentLevel.getSizeX() + selX;
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
                                int x = curPiecePos % currentLevel.getSizeX();
                                int y = curPiecePos / currentLevel.getSizeX();
                                cg.SmoothingMode = SmoothingMode.AntiAlias;
                                cg.FillEllipse(b,
                                    squareSizeX*x + squareSizeX/2 + originX - squareSizeX/6,
                                    squareSizeY*y + squareSizeY/2 + originY - squareSizeY/6,
                                    squareSizeX/3, squareSizeY/3);
                            }
                            else
                                // just move Sokoban
                                curSokPos += moves[i][j];
                        }
        }

        // If you just tap a square to push a piece to, BSPushFinder will find
        // the shortest pushpath regardless of where the Sokoban will end up.
        // If you tap *next* to the square you want to push the piece to and then
        // *drag* onto the square before releasing, then we will try to move the
        // piece in such a way that the Sokoban will end up on the square you
        // first tapped. getOrigPenDownDir() determines in which direction you
        // dragged. This value is passed on to BSPushFinder.getMoves().
        private int getOrigPenDownDir(int square)
        {
            int sx = currentLevel.getSizeX();
            return (origPenDown == square-sx) ? 1 :
                   (origPenDown == square- 1) ? 2 :
                   (origPenDown == square+ 1) ? 3 :
                   (origPenDown == square+sx) ? 4 : 0;
        }
        private void ESMainArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (state == ESMainAreaState.STATE_PUSH)
            {
                int clickedX = (e.X-originX) / squareSizeX;
                int clickedY = (e.Y-originY) / squareSizeY;
                int square = clickedY * currentLevel.getSizeX() + clickedX;
                origPenDown = square;
                clickedOnSquare = square;
                if (pushFinder.pushValid(square))
                    drawPushPath(square, getOrigPenDownDir(square));
                else
                    CreateGraphics().DrawImage(pushDisplay, 0, 0);
            }
        }
        private void ESMainArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (state == ESMainAreaState.STATE_PUSH)
            {
                int clickedX = (e.X-originX) / squareSizeX;
                int clickedY = (e.Y-originY) / squareSizeY;
                int square = clickedY * currentLevel.getSizeX() + clickedX;

                if (square != clickedOnSquare)
                {
                    clickedOnSquare = square;
                    if (pushFinder.pushValid(square))
                        drawPushPath(square, getOrigPenDownDir(square));
                    else
                        CreateGraphics().DrawImage(pushDisplay, 0, 0);
                }
            }
        }
        private void ESMainArea_MouseUp(object sender, MouseEventArgs e)
        {
            int clickedX = (e.X-originX) / squareSizeX;
            int clickedY = (e.Y-originY) / squareSizeY;
            int square = clickedY * currentLevel.getSizeX() + clickedX;

            if (state == ESMainAreaState.STATE_EDITING)
            {
                Graphics g1 = Graphics.FromImage(plainDisplay);
                Graphics g2 = CreateGraphics();
                if (e.Y-originY > mySizeY-squareSizeY-3)
                {
                    int clickedToolIndex = (e.X - toolbarX - originX) / (squareSizeX+2);
                    ESMainAreaTool clickedTool =
                            clickedToolIndex == 0 ? ESMainAreaTool.TOOL_WALL :
                            clickedToolIndex == 1 ? ESMainAreaTool.TOOL_PIECE :
                            clickedToolIndex == 2 ? ESMainAreaTool.TOOL_TARGET : ESMainAreaTool.TOOL_SOKOBAN;
                    if (clickedTool != tool)
                    {
                        drawToolRect(g1, BackColor);
                        drawToolRect(g2, BackColor);
                        tool = clickedTool;
                        drawToolRect(g1, ToolRectColour);
                        drawToolRect(g2, ToolRectColour);
                    }
                }
                else
                {
                    SokobanSquare cell = currentLevel.getCell(clickedX, clickedY);
                    if (tool == ESMainAreaTool.TOOL_WALL)
                    {
                        if (currentLevel.getSokobanX() != clickedX || currentLevel.getSokobanY() != clickedY)
                        {
                            currentLevel.setCell(clickedX, clickedY,
                                cell == SokobanSquare.WALL ? SokobanSquare.BLANK : SokobanSquare.WALL);
                            drawSquare(clickedX, clickedY, g1);
                            drawSquare(clickedX, clickedY, g2);
                            sndThreadDone.Play();
                        }
                        else sndMeep.Play();
                    }
                    else if (tool == ESMainAreaTool.TOOL_PIECE)
                    {
                        if ((currentLevel.getSokobanX() != clickedX || currentLevel.getSokobanY() != clickedY)
                                && cell != SokobanSquare.WALL)
                        {
                            currentLevel.setCell(clickedX, clickedY,
                                cell == SokobanSquare.PIECE_ON_TARGET ? SokobanSquare.TARGET :
                                    cell == SokobanSquare.BLANK ? SokobanSquare.PIECE :
                                    cell == SokobanSquare.TARGET ? SokobanSquare.PIECE_ON_TARGET :
                                        SokobanSquare.BLANK);
                            drawSquare(clickedX, clickedY, g1);
                            drawSquare(clickedX, clickedY, g2);
                            sndPiecePlaced.Play();
                        }
                        else sndMeep.Play();
                    }
                    else if (tool == ESMainAreaTool.TOOL_TARGET)
                    {
                        if (cell != SokobanSquare.WALL)
                        {
                            currentLevel.setCell(clickedX, clickedY,
                                cell == SokobanSquare.PIECE_ON_TARGET ? SokobanSquare.PIECE :
                                    cell == SokobanSquare.BLANK ? SokobanSquare.TARGET :
                                    cell == SokobanSquare.TARGET ? SokobanSquare.BLANK :
                                        SokobanSquare.PIECE_ON_TARGET);
                            drawSquare(clickedX, clickedY, g1);
                            drawSquare(clickedX, clickedY, g2);
                            sndPiecePlaced.Play();
                        }
                        else sndMeep.Play();
                    }
                    else if (tool == ESMainAreaTool.TOOL_SOKOBAN)
                    {
                        if (cell != SokobanSquare.WALL &&
                                cell != SokobanSquare.PIECE &&
                                cell != SokobanSquare.PIECE_ON_TARGET)
                        {
                            int oldSokobanPos = currentLevel.getSokobanPos();
                            currentLevel.setSokobanPos(clickedX, clickedY);
                            drawSquare(oldSokobanPos, g1);
                            drawSquare(oldSokobanPos, g2);
                            drawSquare(clickedX, clickedY, g1);
                            drawSquare(clickedX, clickedY, g2);
                            sndPiecePlaced.Play();
                        }
                        else sndMeep.Play();
                    }
                    int prevSizeX = currentLevel.getSizeX();
                    int prevSizeY = currentLevel.getSizeY();
                    currentLevel.ensureSpace(true);
                    if (currentLevel.getSizeX() != prevSizeX ||
                            currentLevel.getSizeY() != prevSizeY)
                        reinitSize(true);
                }
            }
            else if (currentLevel.isPiece(square) &&
                         state != ESMainAreaState.STATE_SOLVED &&
                         !(pushFinder != null &&
                           getOrigPenDownDir(square) != 0 &&
                           pushFinder.pushValid(square, getOrigPenDownDir(square))))
            {
                if (moveFinder != null && !moveFinder.isDone())
                    moveFinder.runToCompletion(square);

                if (moveFinder.moveValid(square + currentLevel.getSizeX()) ||
                        moveFinder.moveValid(square - currentLevel.getSizeX()) ||
                        moveFinder.moveValid(square + 1) ||
                        moveFinder.moveValid(square - 1))
                {
                    selX = clickedX;
                    selY = clickedY;
                    pushDisplay = copyImage(moveDisplay);
                    Graphics.FromImage(pushDisplay).DrawImage(Properties.Resources.ImgPieceSelected, 
                        originX+selX*squareSizeX, originY+selY*squareSizeY);
                    CreateGraphics().DrawImage(pushDisplay, 0, 0);
                    pleaseConsider = 0;
                    pushFinder = new ESPushFinder(currentLevel, selX, selY, this,
                        new pushThreadFoundCallback(pushThreadFound),
                        new pushThreadDoneCallback(pushThreadDone),
                        moveFinder);
                    state = ESMainAreaState.STATE_PUSH;
                }
                else sndMeep.Play();
            }
            else if (state == ESMainAreaState.STATE_PUSH && pushFinder != null)
            {
                if (!pushFinder.isDone() && !pushFinder.pushValid(square))
                {
                    pleaseConsider = square;
                    pleaseConsiderDir = getOrigPenDownDir(square);
                }
                else
                    processPushClick(square, getOrigPenDownDir(square));
            }
        }

        private void processPushClick(int square, int squareDir)
        {
            if (!pushFinder.pushValid(square))
            {
                sndMeep.Play();
                return;
            }

            int[][] moves = pushFinder.getMoves(square, squareDir);
            Graphics cg = CreateGraphics();
            cg.DrawImage(plainDisplay, 0, 0);
            bool everPushed = false;
            int origPushPos = -1;
            int lastPushPos = -1;
            int origSokPos = currentLevel.getSokobanPos();
            for (int i = 0; i < moves.Length; i++)
                if (moves[i] != null)
                    for (int j = 0; j < moves[i].Length; j++)
                        if (moves[i][j] != 0)
                        {
                            Thread.Sleep(20);
                            if (currentLevel.isPiece(currentLevel.getSokobanPos() + moves[i][j]))
                            {
                                // need to push
                                int prevSokPos = currentLevel.getSokobanPos();
                                int newSokPos = prevSokPos + moves[i][j];
                                int pushTo = newSokPos + moves[i][j];
                                currentLevel.movePiece(newSokPos, pushTo);
                                currentLevel.setSokobanPos(newSokPos);
                                drawSquare(pushTo, cg);
                                drawSquare(newSokPos, cg);
                                drawSquare(prevSokPos, cg);
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
                                int prevSokPos = currentLevel.getSokobanPos();
                                int newSokPos = prevSokPos + moves[i][j];
                                currentLevel.setSokobanPos(newSokPos);
                                drawSquare(newSokPos, cg);
                                drawSquare(prevSokPos, cg);
                            }
                        }
            // optimised version of updatePlain() :)
            Graphics pg = Graphics.FromImage(plainDisplay);
            drawSquare(origSokPos, pg);
            drawSquare(currentLevel.getSokobanPos(), pg);
            if (origPushPos != -1) drawSquare(origPushPos, pg);
            if (lastPushPos != -1) drawSquare(lastPushPos, pg);
            moveDisplay = copyImage(plainDisplay);

            if (currentLevel.solved())
                levelSolved(cg);
            else
            {
                sndPiecePlaced.Play();
                addUndo(origSokPos, origPushPos != -1, origPushPos, lastPushPos);
                cg.DrawImage(moveDisplay, 0, 0);
                reinitMoveFinder();
                state = ESMainAreaState.STATE_MOVE;
            }
        }

        private void levelSolved(Graphics cg)
        {
            state = ESMainAreaState.STATE_SOLVED;
            Graphics g = Graphics.FromImage(moveDisplay);
            Brush b = new SolidBrush(Color.FromArgb(0, 0, 0xA0));
            g.FillEllipse(b, getMyWidth()/2-28, getMyHeight()/2-10, 56, 20);
            g.DrawEllipse(new Pen(Color.Black), getMyWidth()/2-28, getMyHeight()/2-10, 56, 20);
            g.DrawString("LEVEL SOLVED", Font, new SolidBrush(Color.White), getMyWidth()/2 - 41, getMyHeight()/2 - 8);
            cg.DrawImage(moveDisplay, 0, 0);
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
            if (pos == currentLevel.getSokobanPos()) return;

            Image d = currentLevel.getCell(pos) == SokobanSquare.TARGET ? 
                Properties.Resources.ImgCanMoveTarget : Properties.Resources.ImgCanMove;
            Graphics.FromImage(moveDisplay).DrawImage(d,
                originX + (pos % currentLevel.getSizeX())*squareSizeX, 
                originY + (pos / currentLevel.getSizeX())*squareSizeY);

            if (state == ESMainAreaState.STATE_PUSH && !pushFinder.pushValid(pos))
            {
                Graphics.FromImage(pushDisplay).DrawImage(d,
                    originX + (pos % currentLevel.getSizeX())*squareSizeX, 
                    originY + (pos / currentLevel.getSizeX())*squareSizeY);
                CreateGraphics().DrawImage(pushDisplay, 0, 0);
            }
            else if (state == ESMainAreaState.STATE_MOVE)
                CreateGraphics().DrawImage(moveDisplay, 0, 0);
        }
        public void moveThreadDone()
        {
            CreateGraphics().DrawImage(moveDisplay, 0, 0);
        }

        public void pushThreadFound(int pos)
        {
            if (pos == pleaseConsider)
                processPushClick(pleaseConsider, pleaseConsiderDir);
            else
            {
                if (currentLevel.isPiece(pos)) return;
                Image d = currentLevel.getSokobanPos() == pos 
                    ? (currentLevel.getCell(pos) == SokobanSquare.TARGET ? Properties.Resources.ImgCanPushSokobanTarget
                                                                         : Properties.Resources.ImgCanPushSokoban)
                    : (currentLevel.getCell(pos) == SokobanSquare.TARGET ? Properties.Resources.ImgCanPushTarget
                                                                         : Properties.Resources.ImgCanPush);
                Graphics.FromImage(pushDisplay).DrawImage(d,
                    originX + (pos % currentLevel.getSizeX())*squareSizeX,
                    originY + (pos / currentLevel.getSizeX())*squareSizeY);
                CreateGraphics().DrawImage(pushDisplay, 0, 0);
            }
        }

        public void pushThreadDone(bool anythingPossible)
        {
            if (!anythingPossible && pleaseConsider == 0)
                sndMeep.Play();
            else if (pleaseConsider != 0)
                processPushClick(pleaseConsider, pleaseConsiderDir);
        }

        public void SetLevel(SokobanLevel sokobanLevel)
        {
            currentLevel = sokobanLevel;
            reinitSize(false);
            reinitMoveFinder();
            Refresh();
        }
    }
}
