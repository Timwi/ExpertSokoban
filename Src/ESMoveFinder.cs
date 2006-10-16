using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ExpertSokoban
{
    public class ESMoveFinder
    {
        private SokobanLevel level;
        private int[] pathLength;
        private int[] predecessor;
        private ESIntQueue queue = new ESIntQueue();
        private bool first, done, running, doAll, runningToCompletion, stopWhenFourSides,
                     foundTop, foundLeft, foundRight, foundBottom;
        private int swfsPos;
        private Control callbackOwner;
        private Delegate callbackFound, callbackDone;

        public ESMoveFinder(SokobanLevel l, bool optDoAll, Control Owner, Delegate FoundCallback, Delegate DoneCallback, int swfs)
        {
            Init (l, optDoAll, Owner, FoundCallback, DoneCallback);
            stopWhenFourSides = true;
            swfsPos = swfs;
            foundTop    = !l.isFree (swfs - l.getSizeX());
            foundLeft   = !l.isFree (swfs - 1);
            foundRight  = !l.isFree (swfs + 1);
            foundBottom = !l.isFree (swfs + l.getSizeX());
        }
        public ESMoveFinder(SokobanLevel l, bool optDoAll, Control Owner, Delegate FoundCallback, Delegate DoneCallback)
        {
            Init(l, optDoAll, Owner, FoundCallback, DoneCallback);
        }
        private void Init(SokobanLevel l, bool optDoAll, Control Owner, Delegate FoundCallback, Delegate DoneCallback)
        {
            callbackOwner = Owner;
            callbackFound = FoundCallback;
            callbackDone = DoneCallback;
            level = l;
            done = false;
            first = true;
            doAll = optDoAll;
            pathLength = new int[level.getSizeX() * level.getSizeY()];
            predecessor = new int[level.getSizeX() * level.getSizeY()];
            int sp = level.getSokobanPos();
            queue.add(sp);
            pathLength[sp] = 1;
        }
        public void RunToCompletion (int square)
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
                SingleStep();
        }
        public void SingleStep()
        {
            if (done) return;
            if (first && !doAll && callbackOwner != null)
            {
                callbackOwner.Invoke(callbackFound, new object[] { level.getSokobanPos() });
                first = false;
            }

            do
            {
                int item = queue.extract();
                int cpos = item-level.getSizeX();
                SokobanCell c = level.getCell (cpos);
                if ((c == SokobanCell.Blank || c == SokobanCell.Target) && pathLength[cpos] == 0)
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
                    if ((!doAll || runningToCompletion) && callbackOwner != null)
                        callbackOwner.Invoke(callbackFound, new object[] { cpos });
                }
                cpos += level.getSizeX()-1;
                c = level.getCell (cpos);
                if ((c == SokobanCell.Blank || c == SokobanCell.Target) && pathLength[cpos] == 0)
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
                    if ((!doAll || runningToCompletion) && callbackOwner != null)
                        callbackOwner.Invoke(callbackFound, new object[] { cpos });
                }
                cpos += 2;
                c = level.getCell (cpos);
                if ((c == SokobanCell.Blank || c == SokobanCell.Target) && pathLength[cpos] == 0)
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
                    if ((!doAll || runningToCompletion) && callbackOwner != null)
                        callbackOwner.Invoke(callbackFound, new object[] { cpos });
                }
                cpos += level.getSizeX()-1;
                c = level.getCell (cpos);
                if ((c == SokobanCell.Blank || c == SokobanCell.Target) && pathLength[cpos] == 0)
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
                    if ((!doAll || runningToCompletion) && callbackOwner != null)
                        callbackOwner.Invoke(callbackFound, new object[] { cpos });
                }

                if (queue.isEmpty() || (stopWhenFourSides && foundTop && foundLeft && foundRight && foundBottom))
                {
                    done = true;
                    if ((!doAll || runningToCompletion) && callbackOwner != null)
                        callbackOwner.Invoke(callbackDone, new object[] { });
                }
            }
            while (doAll && !done);
        }
        public bool moveValid (int pos) { return (pathLength[pos] > 0); }
        public int getPathLength (int pos) { return pathLength[pos]-1; }
        public void started() { running = true; }
        public void stopped() { running = false; }
        public bool isRunning() { return running; }
        public bool isDone() { return done; }
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
}
