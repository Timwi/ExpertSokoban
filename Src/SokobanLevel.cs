using System;
using System.Collections.Generic;
using System.Text;

namespace ExpertSokoban
{
    public enum SokobanSquare
    {
        INVALID,
        BLANK,
        WALL,
        PIECE,
        TARGET,
        PIECE_ON_TARGET
    };

    public class SokobanLevel
    {
        public static string testLevelStr = 
            "#####            #####" +
            "#   ##############  @#" +
            "#     $ #   $   $ $  #" +
            "# $$#     $   $   $  #" +
            "##  ###############$##" +
            " #$   $.*.$   $  $  # " +
            " #  #   #..$$$ $ # $# " +
            " ## #####...   $ #  # " +
            " # ..#  #....  $ #$ # " +
            " # ..#  #.....   #  # " +
            "###. #  #......$## $##" +
            "# ## ##########  $   #" +
            "# #              $   #" +
            "#   ##############  ##" +
            "#####            #####";

        public static SokobanLevel getTestLevel()
        {
            int sx = 22;
            int sy = 15;
            int sokPos = -1;
            SokobanSquare[] levelData = new SokobanSquare[sx * sy];
            for (int i = 0; i < testLevelStr.Length; i++)
            {
                levelData[i] = 
                    testLevelStr[i] == '#' ? SokobanSquare.WALL :
                    testLevelStr[i] == '$' ? SokobanSquare.PIECE :
                    testLevelStr[i] == '.' ? SokobanSquare.TARGET :
                    testLevelStr[i] == '*' ? SokobanSquare.PIECE_ON_TARGET : SokobanSquare.BLANK;
                if (testLevelStr[i] == '@')
                    sokPos = i;
            }
            // If the level didn't specify a starting position, try to find one.
            if (sokPos == -1)
            {
                bool done = false;
                for (int i = 0; i < sx && !done; i++)
                    for (int j = 0; j < sy && !done; j++)
                    {
                        bool xin = false, yin = false;
                        for (int x = 0; x <= i; x++)
                            if (levelData[j*sx + x] == SokobanSquare.WALL &&
                            (x == 0 || levelData[j*sx + x-1] != SokobanSquare.WALL))
                                xin = !xin;
                        for (int y = 0; y <= j; y++)
                            if (levelData[y*sx + i] == SokobanSquare.WALL &&
                            (y == 0 || levelData[(y-1)*sx + i] != SokobanSquare.WALL))
                                yin = !yin;
                        if (xin && yin)
                        {
                            sokPos = j*sx+i;
                            done = true;
                        }
                    }
            }
            return new SokobanLevel(sx, sy, levelData, sokPos);
        }

        private SokobanSquare[] level;
        private int levelSizeX, levelSizeY, sokobanPos;

        public SokobanLevel(string encodedForm)
        {
            levelSizeX = 0;
            levelSizeY = 0;
            sokobanPos = -1;
            int curx = 0;
            for (int i = 0; i < encodedForm.Length; i++)
            {
                if (encodedForm[i] == '\n')
                {
                    levelSizeY++;
                    if (levelSizeX < curx)
                        levelSizeX = curx;
                    curx = 0;
                }
                else
                    curx++;
            }
            level = new SokobanSquare[levelSizeX*levelSizeY];
            curx = 0;
            int cury = 0;
            for (int i = 0; i < encodedForm.Length; i++)
            {
                if (encodedForm[i] == '\n')
                    cury++;
                else
                {
                    level[curx + levelSizeX*cury] = 
                        encodedForm[i] == '#' ? SokobanSquare.WALL :
                        encodedForm[i] == '$' ? SokobanSquare.PIECE :
                        encodedForm[i] == '.' ? SokobanSquare.TARGET :
                        encodedForm[i] == '*' ? SokobanSquare.PIECE_ON_TARGET :
                                                SokobanSquare.BLANK;
                    if (encodedForm[i] == '@')
                        sokobanPos = (curx + levelSizeX*cury);
                }
            }

            // If the level didn't specify a starting position, try to find one.
            if (sokobanPos == -1)
            {
                bool done = false;
                for (int i = 0; i < levelSizeX && !done; i++)
                    for (int j = 0; j < levelSizeY && !done; j++)
                    {
                        bool xin = false, yin = false;
                        for (int x = 0; x <= i; x++)
                            if (level[j*levelSizeX + x] == SokobanSquare.WALL &&
                            (x == 0 || level[j*levelSizeX + x-1] != SokobanSquare.WALL))
                                xin = !xin;
                        for (int y = 0; y <= j; y++)
                            if (level[y*levelSizeX + i] == SokobanSquare.WALL &&
                            (y == 0 || level[(y-1)*levelSizeX + i] != SokobanSquare.WALL))
                                yin = !yin;
                        if (xin && yin)
                        {
                            sokobanPos = j*levelSizeX+i;
                            done = true;
                        }
                    }
            }
        }
        public SokobanLevel(int sizeX, int sizeY, SokobanSquare[] levelData, int sokPos)
        {
            levelSizeX = sizeX;
            levelSizeY = sizeY;
            level = levelData;
            sokobanPos = sokPos;
        }
        public SokobanLevel(int sizeX, int sizeY)
        {
            levelSizeX = sizeX;
            levelSizeY = sizeY;
            level = new SokobanSquare[levelSizeX*levelSizeY];
            for (int i = 0; i < levelSizeX * levelSizeY; i++)
                level[i] = ((i+1) % levelSizeX < 2) ? SokobanSquare.WALL : SokobanSquare.BLANK;
            sokobanPos = levelSizeX+1;
        }
        public SokobanSquare getCell(int pos)
        {
            return getCell(pos % levelSizeX, pos / levelSizeX);
        }
        public SokobanSquare getCell(int x, int y)
        {
            if (x < 0 || y < 0 || x >= getSizeX() || y >= getSizeY())
                return SokobanSquare.INVALID;
            return level[y*levelSizeX + x];
        }
        public int getSizeX() { return levelSizeX; }
        public int getSizeY() { return levelSizeY; }
        public int getSokobanPos() { return sokobanPos; }
        public int getSokobanX() { return sokobanPos % levelSizeX; }
        public int getSokobanY() { return sokobanPos / levelSizeX; }
        public void setSokobanPos(int pos) { sokobanPos = pos; }
        public void setSokobanPos(int x, int y) { sokobanPos = y * levelSizeX + x; }
        public void setCell(int x, int y, SokobanSquare c) { level[y*levelSizeX + x] = c; }
        public bool isPiece(int pos)
        {
            if (pos < 0 || pos > level.Length) return false;
            return level[pos] == SokobanSquare.PIECE || level[pos] == SokobanSquare.PIECE_ON_TARGET;
        }
        public bool isPiece(int x, int y) { return isPiece(y*levelSizeX + x); }
        public bool isFree(int pos) { return level[pos] == SokobanSquare.BLANK || level[pos] == SokobanSquare.TARGET; }
        public bool isFree(int x, int y) { return isFree(y*levelSizeX + x); }
        public void setPiece(int x, int y) { setPiece(y*levelSizeX + x); }
        public void setPiece(int pos)
        {
            if (level[pos] == SokobanSquare.BLANK)
                level[pos] = SokobanSquare.PIECE;
            else if (level[pos] == SokobanSquare.TARGET)
                level[pos] = SokobanSquare.PIECE_ON_TARGET;
        }
        public void removePiece(int x, int y) { removePiece(y*levelSizeX + x); }
        public void removePiece(int pos)
        {
            if (level[pos] == SokobanSquare.PIECE)
                level[pos] = SokobanSquare.BLANK;
            else if (level[pos] == SokobanSquare.PIECE_ON_TARGET)
                level[pos] = SokobanSquare.TARGET;
        }
        public void movePiece(int fromx, int fromy, int tox, int toy)
        {
            removePiece(fromx, fromy);
            setPiece(tox, toy);
        }
        public void movePiece(int fromPos, int toPos)
        {
            removePiece(fromPos);
            setPiece(toPos);
        }
        public void setSize(int x, int y)
        {
            SokobanSquare[] newLevel = new SokobanSquare[x*y];
            for (int k = 0; k < x*y; k++) newLevel[k] = SokobanSquare.BLANK;
            for (int i = 0; i < (levelSizeX < x ? levelSizeX : x); i++)
                for (int j = 0; j < (levelSizeY < y ? levelSizeY : y); j++)
                    newLevel[j*x+i] = level[j*levelSizeX+i];
            level = newLevel;
            levelSizeX = x;
            levelSizeY = y;
        }
        public bool solved()
        {
            for (int i = 0; i < levelSizeX*levelSizeY; i++)
                if (level[i] == SokobanSquare.TARGET || level[i] == SokobanSquare.PIECE)
                    return false;
            return true;
        }
        public SokobanLevel clone()
        {
            SokobanLevel newLevel = new SokobanLevel(levelSizeX, levelSizeY);
            for (int i = 0; i < levelSizeX*levelSizeY; i++)
                newLevel.setCell(i % levelSizeX, i / levelSizeX, level[i]);
            newLevel.setSokobanPos(getSokobanX(), getSokobanY());
            return newLevel;
        }

        /*
        public static SokobanLevel decode (Vector v)
        {
            int levelSizeX = 0;
            int levelSizeY = v.size();
            int sokobanPos = -1;
            for (int i = 0; i < levelSizeY; i++)
            {
                int l = ((String)(v.items[i])).length();
                if (l > levelSizeX) levelSizeX = l;
            }
            int[] levelData = new int [ levelSizeX * levelSizeY ];
            for (int j = 0; j < levelSizeY; j++)
            {
                for (int i = 0; i < ((String)(v.items[j])).length(); i++)
                {
                    char c = ((String)(v.items[j])).charAt (i);
                    levelData [ j*levelSizeX + i ] = (c == '#') ? WALL : (c == '$') ? PIECE :
                        (c == '.' || c == '+') ? TARGET : (c == '*') ? PIECE_ON_TARGET : BLANK;
                    if (c == '@' || c == '+') sokobanPos = j*levelSizeX + i;
                }
                for (int i = ((String)(v.items[j])).length(); i < levelSizeX; i++)
                    levelData [ j*levelSizeX + i ] = BLANK;
            }

            // If the level didn't specify a starting position, try to find one.
            if (sokobanPos == -1)
            {
                bool done = false;
                for (int i = 0; i < levelSizeX && !done; i++)
                    for (int j = 0; j < levelSizeY && !done; j++)
                    {
                        bool xin = false, yin = false;
                        for (int x = 0; x <= i; x++)
                            if (levelData [ j*levelSizeX + x ] == WALL &&
                                (x == 0 || levelData [ j*levelSizeX + x-1 ] != WALL))
                                xin = !xin;
                        for (int y = 0; y <= j; y++)
                            if (levelData [ y*levelSizeX + i ] == WALL &&
                                (y == 0 || levelData [ (y-1)*levelSizeX + i ] != WALL))
                                yin = !yin;
                        if (xin && yin)
                        {
                            sokobanPos = j*levelSizeX+i;
                            done = true;
                        }
                    }
            }

            return new SokobanLevel (levelSizeX, levelSizeY, levelData, sokobanPos);
        }
        */

        // If margin == false, this will reduce the size of the level if there
        // are rows or columns around the edge that are entirely blank.
        // If margin == true, it will leave (or create) a one-square margin
        // (this is for the level editor, so the user can make the level bigger).
        // There is an exception, though: The width of the level is always at
        // least 7. This is to ensure that the "LEVEL SOLVED" message fits in. ;-)
        public void ensureSpace(bool margin)
        {
            int m = (margin ? 1 : 0);

            int l = 0; while (l < levelSizeX && columnBlank(l)) l++;
            int r = 0; while (r < levelSizeX && columnBlank(levelSizeX-r-1)) r++;
            int u = 0; while (u < levelSizeY && rowBlank(u)) u++;
            int d = 0; while (d < levelSizeY && rowBlank(levelSizeY-d-1)) d++;

            // This is the hack: Have the width be at least 7.
            while (levelSizeX + 2*m - l - r < 7) { l--; r--; }

            resize(2*m-l-r, 2*m-u-d, m-l, m-u);
        }
        private bool columnBlank(int x)
        {
            for (int y = 0; y < levelSizeY; y++)
                if (level[y*levelSizeX + x] != SokobanSquare.BLANK)
                    return false;
            return true;
        }
        private bool rowBlank(int y)
        {
            for (int x = 0; x < levelSizeX; x++)
                if (level[y*levelSizeX + x] != SokobanSquare.BLANK)
                    return false;
            return true;
        }

        // any parameter may be negative!
        private void resize(int amountX, int amountY, int shiftX, int shiftY)
        {
            if (amountX == 0 && amountY == 0) return;

            int newSizeX = levelSizeX + amountX;
            int newSizeY = levelSizeY + amountY;
            SokobanSquare[] newLevel = new SokobanSquare[newSizeX * newSizeY];

            for (int x = 0; x < (amountX < 0 ? newSizeX : levelSizeX); x++)
                for (int y = 0; y < (amountY < 0 ? newSizeY : levelSizeY); y++)
                    newLevel[(amountY > 0 ? y+shiftY : y)*newSizeX +
                             (amountX > 0 ? x+shiftX : x)] =
                    level   [(amountY < 0 ? y-shiftY : y)*levelSizeX +
                             (amountX < 0 ? x-shiftX : x)];

            sokobanPos += (sokobanPos / levelSizeX) * amountX + shiftX;
            sokobanPos += shiftY * newSizeX;

            level = newLevel;
            levelSizeX = newSizeX;
            levelSizeY = newSizeY;
        }

        public SokobanLevel Clone()
        {
            SokobanSquare[] newLevel = (SokobanSquare[]) level.Clone();
            return new SokobanLevel(levelSizeX, levelSizeY, newLevel, sokobanPos);
        }

        public int PosToX(int pos)
        {
            return pos % levelSizeX;
        }

        public int PosToY(int pos)
        {
            return pos / levelSizeX;
        }
    }
}
