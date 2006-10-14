
import waba.fx.*;
import waba.ui.*;
import waba.util.*;
import waba.sys.*;
import superwaba.ext.xplat.ui.*;

public class BSMainArea extends Control
{
    private static final int STATE_MOVE = 0;
    private static final int STATE_PUSH = 1;
    private static final int STATE_SOLVED = 2;
    private static final int STATE_EDITING = 3;

    private static final int TOOL_WALL = 0;
    private static final int TOOL_PIECE = 1;
    private static final int TOOL_TARGET = 2;
    private static final int TOOL_SOKOBAN = 3;
    private static final int MAX_TOOL = 3;
    private static final Color TOOL_RECT_COLOR = new Color (0, 0x80, 0);

    private SokobanLevel currentLevel;
    private BSMoveFinder moveFinder;
    private BSPushFinder pushFinder;
    private int squareSizeX, squareSizeY, mySizeX, mySizeY, state, toolbarX,
                selX, selY, origPenDown, tool, clickedOnSquare, curImagesSize,

                // If, while state == STATE_PUSH, the user clicks somewhere
                // where the pushFinder has not yet found a path to, but is
                // still running, pleaseConsider (and pleaseConsiderDir) are
                // set to the square clicked on. If the pushFinder encounters
                // the square stored in pleaseConsider, the sequence is
                // executed. For an explanation of pleaseConsiderDir, see
                // getOrigPenDownDir().
                pleaseConsider, pleaseConsiderDir;

    private Image[] images;
    private SoundClip levelSolved, meep, piecePlaced, threadDone;

    private Image plainDisplay, moveDisplay, pushDisplay;
    private ExpSok parentWindow;

    private int[][] undoBuffer;
    private int[][] selPath;

    public BSMainArea (SokobanLevel level, ExpSok parentMw, boolean editing)
    {
        parentWindow = parentMw;
        currentLevel = level;
        levelSolved = new SoundClip ("leveldone.wav");
        meep = new SoundClip ("meep.wav");
        piecePlaced = new SoundClip ("pieceplaced.wav");
        threadDone = new SoundClip ("threaddone.wav");
        curImagesSize = 0;
        images = null;

        reinitSize (editing, true);
    }
    public void reinitSize (boolean editing, boolean inConstructor)
    {
        int useSize = (currentLevel.getSizeX() <= 11 &&
                       currentLevel.getSizeY() <= 11) ? 2 : 1;

        if (useSize == 2 && curImagesSize != 2)
        {
            images = new Image[14];
            images[0] = new Image ("blank2.bmp");
            images[1] = new Image ("wall2.bmp");
            images[2] = new Image ("piece2.bmp");
            images[3] = new Image ("target2.bmp");
            images[4] = new Image ("pieceot2.bmp");
            images[5] = new Image ("soko2.bmp");
            images[6] = new Image ("sokot2.bmp");
            images[7] = new Image ("canmove2.bmp");
            images[8] = new Image ("canmovet2.bmp");
            images[9] = new Image ("canpush2.bmp");
            images[10] = new Image ("canpusht2.bmp");
            images[11] = new Image ("canpushs2.bmp");
            images[12] = new Image ("canpushst2.bmp");
            images[13] = new Image ("piecesel2.bmp");
            curImagesSize = 2;
        }
        else if (useSize == 1 && curImagesSize != 1)
        {
            images = new Image[14];
            images[0] = new Image ("blank.bmp");
            images[1] = new Image ("wall.bmp");
            images[2] = new Image ("piece.bmp");
            images[3] = new Image ("target.bmp");
            images[4] = new Image ("pieceot.bmp");
            images[5] = new Image ("soko.bmp");
            images[6] = new Image ("sokot.bmp");
            images[7] = new Image ("canmove.bmp");
            images[8] = new Image ("canmovet.bmp");
            images[9] = new Image ("canpush.bmp");
            images[10] = new Image ("canpusht.bmp");
            images[11] = new Image ("canpushs.bmp");
            images[12] = new Image ("canpushst.bmp");
            images[13] = new Image ("piecesel.bmp");
            curImagesSize = 1;
        }
        squareSizeX = images[0].getWidth();
        squareSizeY = images[0].getHeight();
        mySizeX = currentLevel.getSizeX() * squareSizeX;
        mySizeY = currentLevel.getSizeY() * squareSizeY;
        if (editing)
        {
            mySizeY += squareSizeY + 2;
            state = STATE_EDITING;
            tool = TOOL_WALL;
            toolbarX = mySizeX/2 - (squareSizeX+2)*2;
        }
        else state = STATE_MOVE;
        if (!inConstructor)
            parentWindow.placeMainArea();
        updatePlain();
        repaint();
    }
    public void initialise()
    {
        updatePlain();
        if (state != STATE_EDITING)
        {
            undoBuffer = null;
            moveDisplay = copyImage (plainDisplay);
            reinitMoveFinder();
        }
    }
    private void reinitMoveFinder()
    {
        parentWindow.removeThread (pushFinder);
        parentWindow.removeThread (moveFinder);
        moveFinder = new BSMoveFinder (currentLevel, this, false);
        parentWindow.addThread (moveFinder, false);
    }
    public int getMyWidth() { return mySizeX; }
    public int getMyHeight() { return mySizeY; }
    public int getSquareSizeX() { return squareSizeX; }
    public int getSquareSizeY() { return squareSizeY; }

    private void addUndo (int prevSokPos, boolean push, int fromPush, int toPush)
    {
        int n = 0;
        if (undoBuffer == null)
            undoBuffer = new int[1][];
        else
        {
            n = undoBuffer.length;
            int[][] newUndoBuffer = new int [ n+1 ][];
            for (int i = 0; i < undoBuffer.length; i++)
                newUndoBuffer[i] = undoBuffer[i];
            undoBuffer = newUndoBuffer;
        }

        int[] newElem = new int [ push ? 3 : 1 ];
        newElem[0] = prevSokPos;
        if (push) { newElem[1] = fromPush; newElem[2] = toPush; }
        undoBuffer[n] = newElem;
    }
    public void undo()
    {
        parentWindow.removeThread (moveFinder);
        parentWindow.removeThread (pushFinder);

        if (undoBuffer == null) return;
        if (state == STATE_EDITING || state == STATE_SOLVED) return;

        int[] extr = undoBuffer [ undoBuffer.length-1 ];
        if (undoBuffer.length == 1) undoBuffer = null; else
        {
            int[][] newUndoBuffer = new int [ undoBuffer.length-1 ][];
            for (int i = 0; i < undoBuffer.length-1; i++)
                newUndoBuffer[i] = undoBuffer[i];
            undoBuffer = newUndoBuffer;
        }

        boolean push = extr.length > 1;
        currentLevel.setSokobanPos (extr[0]);
        if (push) currentLevel.movePiece (extr[2], extr[1]);

        state = STATE_MOVE;
        pleaseConsider = 0;
        pleaseConsiderDir = 0;
        updatePlain();
        moveDisplay = copyImage (plainDisplay);
        reinitMoveFinder();
    }

    public void onPaint (Graphics g)
    {
        g.drawImage ((state == STATE_PUSH) ? pushDisplay :
                     (state == STATE_EDITING) ? plainDisplay : moveDisplay, 0, 0);
        if (state == STATE_EDITING)
        {
            g.drawImage (images[1],               1+toolbarX, mySizeY-squareSizeY-1);
            g.drawImage (images[2],   squareSizeX+3+toolbarX, mySizeY-squareSizeY-1);
            g.drawImage (images[3], 2*squareSizeX+5+toolbarX, mySizeY-squareSizeY-1);
            g.drawImage (images[5], 3*squareSizeX+7+toolbarX, mySizeY-squareSizeY-1);
            drawToolRect (g, TOOL_RECT_COLOR);
        }
    }
    private void drawToolRect (Graphics g, Color c)
    {
        g.setForeColor (c);
        g.drawRect (toolbarX + tool*(squareSizeX+2), mySizeY-squareSizeY-2, squareSizeX+2, squareSizeY+2);
    }

    private void drawSquare (int pos, Graphics g)
    { drawSquare (pos % currentLevel.getSizeX(), pos / currentLevel.getSizeX(), g); }
    private void drawSquare (int x, int y, Graphics g)
    {
        int imageToDraw = currentLevel.getCell (x, y);
        if (currentLevel.getSokobanX() == x && currentLevel.getSokobanY() == y)
            imageToDraw = (imageToDraw == SokobanLevel.TARGET) ? 6 : 5;
        g.drawImage (images[imageToDraw], x*squareSizeX, y*squareSizeY);
    }
    public void updatePlain()
    {
        plainDisplay = new Image (currentLevel.getSizeX() * squareSizeX, currentLevel.getSizeY() * squareSizeY);
        Graphics ig = plainDisplay.getGraphics();
        for (int i = 0; i < currentLevel.getSizeX(); i++)
            for (int j = 0; j < currentLevel.getSizeY(); j++)
                drawSquare (i, j, ig);
    }

    public void makeMoveDisplay()
    {
        moveDisplay = copyImage (plainDisplay);
        Graphics cg = moveDisplay.getGraphics();
        cg.setForeColor (new Color (0, 0xC0, 0));
        for (int i = 0; i < currentLevel.getSizeX(); i++)
            for (int j = 0; j < currentLevel.getSizeY(); j++)
                if (moveFinder.moveValid (j*currentLevel.getSizeX() + i))
                    cg.drawImage (images [ (currentLevel.getCell (i, j) == SokobanLevel.TARGET)
                        ? 8 : 7 ], i*squareSizeX, j*squareSizeY);
    }

    public void makePushDisplay()
    {
        pushDisplay = copyImage (moveDisplay);
        Graphics cg = pushDisplay.getGraphics();
        cg.setForeColor (new Color (0, 0, 0xC0));
        for (int i = 0; i < currentLevel.getSizeX(); i++)
            for (int j = 0; j < currentLevel.getSizeY(); j++)
                if (pushFinder.pushValid (j*currentLevel.getSizeX() + i))
                    cg.drawImage (images [
                        (currentLevel.getSokobanX() == i && currentLevel.getSokobanY() == j)
                            ? (currentLevel.getCell (i, j) == SokobanLevel.TARGET)
                                ? 12 : 11
                            : (currentLevel.getCell (i, j) == SokobanLevel.TARGET)
                                ? 10 : 9
                        ], i*squareSizeX, j*squareSizeY);
        cg.drawImage (images [13], selX*squareSizeX, selY*squareSizeY);
    }

    private void drawPushPath (int square, int posInDir)
    {
        Graphics cg = createGraphics();
        cg.drawImage (pushDisplay, 0, 0);
        int[][] moves = pushFinder.getMoves (square, posInDir);
        int curSokPos = currentLevel.getSokobanPos();
        int curPiecePos = selY * currentLevel.getSizeX() + selX;
        cg.setBackColor (new Color (0, 0, 0x80));

        for (int i = 0; i < moves.length; i++)
            if (moves[i] != null)
                for (int j = 0; j < moves[i].length; j++)
                    if (moves[i][j] != 0)
                    {
                        if (curPiecePos == curSokPos + moves[i][j])
                        {
                            // need to push
                            curPiecePos += moves[i][j];
                            curSokPos += moves[i][j];
                            int x = curPiecePos % currentLevel.getSizeX();
                            int y = curPiecePos / currentLevel.getSizeX();
                            cg.fillEllipse (squareSizeX*x + squareSizeX/2, squareSizeY*y + squareSizeY/2,
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
    private int getOrigPenDownDir (int square)
    {
        int sx = currentLevel.getSizeX();
        return (origPenDown == square-sx) ? 1 :
               (origPenDown == square- 1) ? 2 :
               (origPenDown == square+ 1) ? 3 :
               (origPenDown == square+sx) ? 4 : 0;
    }
    public void onEvent (Event e)
    {
        if ((e.type == PenEvent.PEN_DOWN || e.type == PenEvent.PEN_DRAG) && e.target == this && state == STATE_PUSH)
        {
            PenEvent pe = (PenEvent) e;
            int clickedX = pe.x / squareSizeX;
            int clickedY = pe.y / squareSizeY;
            int square = clickedY * currentLevel.getSizeX() + clickedX;

            if (e.type == PenEvent.PEN_DOWN)
                origPenDown = square;
            if (e.type == PenEvent.PEN_DOWN || square != clickedOnSquare)
            {
                clickedOnSquare = square;
                if (pushFinder.pushValid (square))
                    drawPushPath (square, getOrigPenDownDir (square));
                else
                    createGraphics().drawImage (pushDisplay, 0, 0);
            }
        }
        else if (e.type == PenEvent.PEN_UP && e.target == this)
        {
            PenEvent pe = (PenEvent) e;
            int clickedX = pe.x / squareSizeX;
            int clickedY = pe.y / squareSizeY;
            int square = clickedY * currentLevel.getSizeX() + clickedX;

            if (state == STATE_EDITING)
            {
                Graphics g1 = plainDisplay.getGraphics();
                Graphics g2 = createGraphics();
                if (pe.y > mySizeY-squareSizeY-3)
                {
                    int clickedTool = (pe.x - toolbarX) / (squareSizeX+2);
                    if (clickedTool >= 0 &&
                        clickedTool <= MAX_TOOL &&
                        clickedTool != tool)
                    {
                        drawToolRect (g1, getBackColor());
                        drawToolRect (g2, getBackColor());
                        tool = clickedTool;
                        drawToolRect (g1, TOOL_RECT_COLOR);
                        drawToolRect (g2, TOOL_RECT_COLOR);
                    }
                }
                else
                {
                    int cell = currentLevel.getCell (clickedX, clickedY);
                    if (tool == TOOL_WALL)
                    {
                        if (currentLevel.getSokobanX() != clickedX || currentLevel.getSokobanY() != clickedY)
                        {
                            currentLevel.setCell (clickedX, clickedY,
                                cell == SokobanLevel.WALL ? SokobanLevel.BLANK : SokobanLevel.WALL);
                            drawSquare (clickedX, clickedY, g1);
                            drawSquare (clickedX, clickedY, g2);
                            threadDone.play();
                        }
                        else meep.play();
                    }
                    else if (tool == TOOL_PIECE)
                    {
                        if ((currentLevel.getSokobanX() != clickedX || currentLevel.getSokobanY() != clickedY)
                            && cell != SokobanLevel.WALL)
                        {
                            currentLevel.setCell (clickedX, clickedY,
                                cell == SokobanLevel.PIECE_ON_TARGET ? SokobanLevel.TARGET :
                                cell == SokobanLevel.BLANK ? SokobanLevel.PIECE :
                                cell == SokobanLevel.TARGET ? SokobanLevel.PIECE_ON_TARGET :
                                    SokobanLevel.BLANK);
                            drawSquare (clickedX, clickedY, g1);
                            drawSquare (clickedX, clickedY, g2);
                            piecePlaced.play();
                        }
                        else meep.play();
                    }
                    else if (tool == TOOL_TARGET)
                    {
                        if (cell != SokobanLevel.WALL)
                        {
                            currentLevel.setCell (clickedX, clickedY,
                                cell == SokobanLevel.PIECE_ON_TARGET ? SokobanLevel.PIECE :
                                cell == SokobanLevel.BLANK ? SokobanLevel.TARGET :
                                cell == SokobanLevel.TARGET ? SokobanLevel.BLANK :
                                    SokobanLevel.PIECE_ON_TARGET);
                            drawSquare (clickedX, clickedY, g1);
                            drawSquare (clickedX, clickedY, g2);
                            piecePlaced.play();
                        }
                        else meep.play();
                    }
                    else if (tool == TOOL_SOKOBAN)
                    {
                        if (cell != SokobanLevel.WALL &&
                            cell != SokobanLevel.PIECE &&
                            cell != SokobanLevel.PIECE_ON_TARGET)
                        {
                            int oldSokobanPos = currentLevel.getSokobanPos();
                            currentLevel.setSokobanPos (clickedX, clickedY);
                            drawSquare (oldSokobanPos, g1);
                            drawSquare (oldSokobanPos, g2);
                            drawSquare (clickedX, clickedY, g1);
                            drawSquare (clickedX, clickedY, g2);
                            piecePlaced.play();
                        }
                        else meep.play();
                    }
                    int prevSizeX = currentLevel.getSizeX();
                    int prevSizeY = currentLevel.getSizeY();
                    currentLevel.ensureSpace (true);
                    if (currentLevel.getSizeX() != prevSizeX ||
                        currentLevel.getSizeY() != prevSizeY)
                        reinitSize (true, false);
                }
            }
            else if (currentLevel.isPiece (square) &&
                     state != STATE_SOLVED &&
                     !(pushFinder != null &&
                       getOrigPenDownDir (square) != 0 &&
                       pushFinder.pushValid (square, getOrigPenDownDir (square))))
            {
                if (moveFinder != null && !moveFinder.isDone())
                {
                    moveFinder.runToCompletion (square);
                    parentWindow.removeThread (moveFinder);
                }
                if (moveFinder.moveValid (square + currentLevel.getSizeX()) ||
                    moveFinder.moveValid (square - currentLevel.getSizeX()) ||
                    moveFinder.moveValid (square + 1) ||
                    moveFinder.moveValid (square - 1))
                {
                    // Interrupt the pushFinder if there is any.
                    parentWindow.removeThread (pushFinder);

                    selX = clickedX;
                    selY = clickedY;
                    pushDisplay = copyImage (moveDisplay);
                    pushDisplay.getGraphics().drawImage (images[13], selX*squareSizeX, selY*squareSizeY);
                    createGraphics().drawImage (pushDisplay, 0, 0);
                    state = STATE_PUSH;

                    pleaseConsider = 0;
                    pushFinder = new BSPushFinder (currentLevel, selX, selY, this, moveFinder);
                    parentWindow.addThread (pushFinder, false);
                }
                else meep.play();
            }
            else if (state == STATE_PUSH && pushFinder != null)
            {
                if (!pushFinder.isDone() && !pushFinder.pushValid (square))
                {
                    pleaseConsider = square;
                    pleaseConsiderDir = getOrigPenDownDir (square);
                }
                else
                    processPushClick (square, getOrigPenDownDir (square));
            }
        }
    }

    private void processPushClick (int square, int squareDir)
    {
        if (!pushFinder.pushValid (square))
        {
            meep.play();
            return;
        }

        parentWindow.removeThread (pushFinder);

        int[][] moves = pushFinder.getMoves (square, squareDir);
        Graphics cg = createGraphics();
        cg.drawImage (plainDisplay, 0, 0);
        boolean everPushed = false;
        int origPushPos = -1;
        int lastPushPos = -1;
        int origSokPos = currentLevel.getSokobanPos();
        for (int i = 0; i < moves.length; i++)
            if (moves[i] != null)
                for (int j = 0; j < moves[i].length; j++)
                    if (moves[i][j] != 0)
                    {
                        Vm.sleep (20);
                        if (currentLevel.isPiece (currentLevel.getSokobanPos() + moves[i][j]))
                        {
                            // need to push
                            int prevSokPos = currentLevel.getSokobanPos();
                            int newSokPos = prevSokPos + moves[i][j];
                            int pushTo = newSokPos + moves[i][j];
                            currentLevel.movePiece (newSokPos, pushTo);
                            currentLevel.setSokobanPos (newSokPos);
                            drawSquare (pushTo, cg);
                            drawSquare (newSokPos, cg);
                            drawSquare (prevSokPos, cg);
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
                            currentLevel.setSokobanPos (newSokPos);
                            drawSquare (newSokPos, cg);
                            drawSquare (prevSokPos, cg);
                        }
                    }
        // optimised version of updatePlain() :)
        Graphics pg = plainDisplay.getGraphics();
        drawSquare (origSokPos, pg);
        drawSquare (currentLevel.getSokobanPos(), pg);
        if (origPushPos != -1) drawSquare (origPushPos, pg);
        if (lastPushPos != -1) drawSquare (lastPushPos, pg);
        moveDisplay = copyImage (plainDisplay);

        if (currentLevel.solved())
            levelSolved (cg);
        else
        {
            piecePlaced.play();
            addUndo (origSokPos, origPushPos != -1, origPushPos, lastPushPos);
            cg.drawImage (moveDisplay, 0, 0);
            state = STATE_MOVE;
            pushFinder = null;
            reinitMoveFinder();
        }
    }

    private void levelSolved (Graphics cg)
    {
        state = STATE_SOLVED;
        Graphics g = moveDisplay.getGraphics();
        g.setBackColor (new Color (0, 0, 0xA0));
        g.fillEllipse (getMyWidth()/2, getMyHeight()/2, 57, 20);
        g.setForeColor (Color.BLACK);
        g.drawEllipse (getMyWidth()/2, getMyHeight()/2, 57, 20);
        g.setForeColor (Color.WHITE);
        g.drawText ("LEVEL SOLVED", getMyWidth()/2 - 41, getMyHeight()/2 - 8);
        cg.drawImage (moveDisplay, 0, 0);
        levelSolved.play();
    }

    private Image copyImage (Image i)
    {
        Image ret = new Image (i.getWidth(), i.getHeight());
        Graphics g = ret.getGraphics();
        g.drawImage (i, 0, 0);
        return ret;
    }

    public void moveThreadFound (int pos)
    {
        if (pos == currentLevel.getSokobanPos()) return;

        moveDisplay.getGraphics().drawImage (images [ (currentLevel.getCell (pos) == SokobanLevel.TARGET)
            ? 8 : 7 ], (pos % currentLevel.getSizeX())*squareSizeX,
                       (pos / currentLevel.getSizeX())*squareSizeY);

        if (state == STATE_PUSH && !pushFinder.pushValid (pos))
        {
            pushDisplay.getGraphics().drawImage (images [ (currentLevel.getCell (pos) == SokobanLevel.TARGET)
                ? 8 : 7 ], (pos % currentLevel.getSizeX())*squareSizeX,
                           (pos / currentLevel.getSizeX())*squareSizeY);
            createGraphics().drawImage (pushDisplay, 0, 0);
        }

        if (state == STATE_MOVE)
            createGraphics().drawImage (moveDisplay, 0, 0);
    }
    public void moveThreadDone()
    {
        parentWindow.removeThread (moveFinder);
        createGraphics().drawImage (moveDisplay, 0, 0);
    }

    public void pushThreadNothingPossible()
    {
        meep.play();
    }

    public void pushThreadFound (int pos)
    {
        if (pos == pleaseConsider)
            processPushClick (pleaseConsider, pleaseConsiderDir);
        else
        {
            if (currentLevel.isPiece (pos)) return;
            pushDisplay.getGraphics().drawImage (images [ (currentLevel.getSokobanPos() == pos)
                    ? (currentLevel.getCell (pos) == SokobanLevel.TARGET)
                        ? 12 : 11
                    : (currentLevel.getCell (pos) == SokobanLevel.TARGET)
                        ? 10 : 9
                ], (pos % currentLevel.getSizeX())*squareSizeX,
                   (pos / currentLevel.getSizeX())*squareSizeY);
            createGraphics().drawImage (pushDisplay, 0, 0);
        }
    }

    public void pushThreadDone()
    {
        parentWindow.removeThread (pushFinder);
        if (pleaseConsider != 0)
            processPushClick (pleaseConsider, pleaseConsiderDir);
        else
            threadDone.play();
    }
    public void aboutToDestroy()
    {
        if (moveFinder != null) parentWindow.removeThread (moveFinder);
        if (pushFinder != null) parentWindow.removeThread (pushFinder);
    }
}
