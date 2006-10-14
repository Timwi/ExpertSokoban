
import waba.fx.*;
import waba.ui.*;
import waba.util.*;
import waba.sys.*;
import superwaba.ext.xplat.ui.*;

public class BSMoveFinder implements Thread
{
    private SokobanLevel level;
    private int[] pathLength;
    private int[] predecessor;
    private BSIntQueue queue = new BSIntQueue();
    private boolean done, running, doAll, runningToCompletion, stopWhenFourSides,
                    foundTop, foundLeft, foundRight, foundBottom;
    private int swfsPos;
    private BSMainArea ma;

    public BSMoveFinder (SokobanLevel l, BSMainArea parentMa, boolean optDoAll, int swfs)
    {
        this (l, parentMa, optDoAll);
        stopWhenFourSides = true;
        swfsPos = swfs;
        foundTop    = !l.isFree (swfs - l.getSizeX());
        foundLeft   = !l.isFree (swfs - 1);
        foundRight  = !l.isFree (swfs + 1);
        foundBottom = !l.isFree (swfs + l.getSizeX());
    }
    public BSMoveFinder (SokobanLevel l, BSMainArea parentMa, boolean optDoAll)
    {
        level = l;
        done = false;
        ma = parentMa;
        doAll = optDoAll;
        pathLength = new int [ level.getSizeX() * level.getSizeY() ];
        predecessor = new int [ level.getSizeX() * level.getSizeY() ];
        int sp = level.getSokobanPos();
        queue.add (sp);
        pathLength[sp] = 1;
        if (!doAll) ma.moveThreadFound (sp);
    }
    public void runToCompletion (int square)
    {
        runningToCompletion = true;
        doAll = true;
        stopWhenFourSides = true;
        swfsPos = square;
        int sx = level.getSizeX();
        foundTop    = !level.isFree (square-sx) || pathLength[square-sx] > 0;
        foundLeft   = !level.isFree (square- 1) || pathLength[square- 1] > 0;
        foundRight  = !level.isFree (square+ 1) || pathLength[square+ 1] > 0;
        foundBottom = !level.isFree (square+sx) || pathLength[square+sx] > 0;
        if (foundTop && foundLeft && foundRight && foundBottom)
            done = true;
        else
            run();
    }
    public void run()
    {
        if (done) return;
        do
        {
            int item = queue.extract();
            int cpos = item-level.getSizeX();
            int c = level.getCell (cpos);
            if ((c == SokobanLevel.BLANK || c == SokobanLevel.TARGET) && pathLength[cpos] == 0)
            {
                queue.add (cpos);
                pathLength[cpos] = pathLength[item]+1;
                predecessor[cpos] = item;
                if (stopWhenFourSides)
                {
                    if (cpos == swfsPos - level.getSizeX()) foundTop = true;
                    if (cpos == swfsPos - 1               ) foundLeft = true;
                    if (cpos == swfsPos + 1               ) foundRight = true;
                    if (cpos == swfsPos + level.getSizeX()) foundBottom = true;
                }
                if (!doAll || runningToCompletion) ma.moveThreadFound (cpos);
            }
            cpos += level.getSizeX()-1;
            c = level.getCell (cpos);
            if ((c == SokobanLevel.BLANK || c == SokobanLevel.TARGET) && pathLength[cpos] == 0)
            {
                queue.add (cpos);
                pathLength[cpos] = pathLength[item]+1;
                predecessor[cpos] = item;
                if (stopWhenFourSides)
                {
                    if (cpos == swfsPos - level.getSizeX()) foundTop = true;
                    if (cpos == swfsPos - 1               ) foundLeft = true;
                    if (cpos == swfsPos + 1               ) foundRight = true;
                    if (cpos == swfsPos + level.getSizeX()) foundBottom = true;
                }
                if (!doAll || runningToCompletion) ma.moveThreadFound (cpos);
            }
            cpos += 2;
            c = level.getCell (cpos);
            if ((c == SokobanLevel.BLANK || c == SokobanLevel.TARGET) && pathLength[cpos] == 0)
            {
                queue.add (cpos);
                pathLength[cpos] = pathLength[item]+1;
                predecessor[cpos] = item;
                if (stopWhenFourSides)
                {
                    if (cpos == swfsPos - level.getSizeX()) foundTop = true;
                    if (cpos == swfsPos - 1               ) foundLeft = true;
                    if (cpos == swfsPos + 1               ) foundRight = true;
                    if (cpos == swfsPos + level.getSizeX()) foundBottom = true;
                }
                if (!doAll || runningToCompletion) ma.moveThreadFound (cpos);
            }
            cpos += level.getSizeX()-1;
            c = level.getCell (cpos);
            if ((c == SokobanLevel.BLANK || c == SokobanLevel.TARGET) && pathLength[cpos] == 0)
            {
                queue.add (cpos);
                pathLength[cpos] = pathLength[item]+1;
                predecessor[cpos] = item;
                if (stopWhenFourSides)
                {
                    if (cpos == swfsPos - level.getSizeX()) foundTop = true;
                    if (cpos == swfsPos - 1               ) foundLeft = true;
                    if (cpos == swfsPos + 1               ) foundRight = true;
                    if (cpos == swfsPos + level.getSizeX()) foundBottom = true;
                }
                if (!doAll || runningToCompletion) ma.moveThreadFound (cpos);
            }

            if (queue.empty() || (stopWhenFourSides && foundTop && foundLeft && foundRight && foundBottom))
            {
                done = true;
                if (!doAll || runningToCompletion)
                    ma.moveThreadDone();
            }
        }
        while (doAll && !done);
    }
    public boolean moveValid (int pos) { return (pathLength[pos] > 0); }
    public int getPathLength (int pos) { return pathLength[pos]-1; }
    public void started() { running = true; }
    public void stopped() { running = false; }
    public boolean isRunning() { return running; }
    public boolean isDone() { return done; }
    public int[] getPath (int pos)
    {
        if (pathLength[pos] == 0) return null;
        int[] ret = new int [ pathLength[pos]-1 ];
        int curAt = pos;
        for (int i = pathLength[pos]-2; i >= 0; i--)
        {
            ret[i] = curAt - predecessor[curAt];
            curAt = predecessor[curAt];
        }
        return ret;
    }
}
