
import waba.fx.*;
import waba.ui.*;
import waba.util.*;
import waba.sys.*;
import superwaba.ext.xplat.ui.*;

public class SokobanLevel
{
    public static final int INVALID = -1;
    public static final int BLANK = 0;
    public static final int WALL = 1;
    public static final int PIECE = 2;
    public static final int TARGET = 3;
    public static final int PIECE_ON_TARGET = 4;

    public static final int[] testLevel =
    {
        WALL, WALL, WALL, WALL, WALL,
        WALL, BLANK, PIECE, TARGET, WALL,
        WALL, WALL, WALL, WALL, WALL
    };
    public static final int testLevelSizeX = 5;
    public static final int testLevelSizeY = 3;
    public static final int testSokobanPos = 6;

    private int[] level;
    private int levelSizeX, levelSizeY, sokobanPos;

    public SokobanLevel() { level = null; }
    public SokobanLevel (int sizeX, int sizeY, int[] levelData, int sokPos)
    {
        levelSizeX = sizeX;
        levelSizeY = sizeY;
        level = levelData;
        sokobanPos = sokPos;
    }
    public SokobanLevel (int sizeX, int sizeY)
    {
        levelSizeX = sizeX;
        levelSizeY = sizeY;
        level = new int [ levelSizeX*levelSizeY ];
        for (int i = 0; i < levelSizeX * levelSizeY; i++)
            level[i] = ((i+1) % levelSizeX < 2) ? WALL : BLANK;
        sokobanPos = levelSizeX+1;
    }
    //public void loadFromFile (String filename, String name)
    //{
    //}
    //public static LevelFileInfo levelsInFile (String filename);
    //{
    //}
    public int getCell (int pos)
    {
        return getCell (pos % levelSizeX, pos / levelSizeX);
    }
    public int getCell (int x, int y)
    {
        if (x < 0 || y < 0 || x >= getSizeX() || y >= getSizeY())
            return INVALID;
        return level [ y*levelSizeX + x ];
    }
    public int getSizeX() { return levelSizeX; }
    public int getSizeY() { return levelSizeY; }
    public int getSokobanPos() { return sokobanPos; }
    public int getSokobanX() { return sokobanPos % levelSizeX; }
    public int getSokobanY() { return sokobanPos / levelSizeX; }
    public void setSokobanPos (int pos) { sokobanPos = pos; }
    public void setSokobanPos (int x, int y) { sokobanPos = y * levelSizeX + x; }
    public void setCell (int x, int y, int c) { level [ y*levelSizeX + x ] = c; }
    public boolean isPiece (int pos)
    {
        if (pos < 0 || pos > level.length) return false;
        return level[pos] == PIECE || level[pos] == PIECE_ON_TARGET;
    }
    public boolean isPiece (int x, int y) { return isPiece (y*levelSizeX + x); }
    public boolean isFree (int pos) { return level[pos] == BLANK || level[pos] == TARGET; }
    public boolean isFree (int x, int y) { return isFree (y*levelSizeX + x); }
    public void setPiece (int x, int y) { setPiece (y*levelSizeX + x); }
    public void setPiece (int pos)
    {
        if (level[pos] == BLANK) level[pos] = PIECE; else
        if (level[pos] == TARGET) level[pos] = PIECE_ON_TARGET;
    }
    public void removePiece (int x, int y) { removePiece (y*levelSizeX + x); }
    public void removePiece (int pos)
    {
        if (level[pos] == PIECE) level[pos] = BLANK; else
        if (level[pos] == PIECE_ON_TARGET) level[pos] = TARGET;
    }
    public void movePiece (int fromx, int fromy, int tox, int toy)
    {
        removePiece (fromx, fromy);
        setPiece (tox, toy);
    }
    public void movePiece (int fromPos, int toPos)
    {
        removePiece (fromPos);
        setPiece (toPos);
    }
    public void setSize (int x, int y)
    {
        int[] newLevel = new int [ x*y ];
        for (int k = 0; k < x*y; k++) newLevel[k] = BLANK;
        for (int i = 0; i < (levelSizeX < x ? levelSizeX : x); i++)
            for (int j = 0; j < (levelSizeY < y ? levelSizeY : y); j++)
                newLevel [ j*x+i ] = level [ j*levelSizeX+i ];
        level = newLevel;
        levelSizeX = x;
        levelSizeY = y;
    }
    public boolean solved()
    {
        for (int i = 0; i < levelSizeX*levelSizeY; i++)
            if (level[i] == TARGET || level[i] == PIECE)
                return false;
        return true;
    }
    public SokobanLevel clone()
    {
        SokobanLevel newLevel = new SokobanLevel (levelSizeX, levelSizeY);
        for (int i = 0; i < levelSizeX*levelSizeY; i++)
            newLevel.setCell (i % levelSizeX, i / levelSizeX, level[i]);
        newLevel.setSokobanPos (getSokobanX(), getSokobanY());
        return newLevel;
    }

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
            boolean done = false;
            for (int i = 0; i < levelSizeX && !done; i++)
                for (int j = 0; j < levelSizeY && !done; j++)
                {
                    boolean xin = false, yin = false;
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

    // If margin == false, this will reduce the size of the level if there
    // are rows or columns around the edge that are entirely blank.
    // If margin == true, it will leave (or create) a one-square margin
    // (this is for the level editor, so the user can make the level bigger).
    // There is an exception, though: The width of the level is always at
    // least 7. This is to ensure that the "LEVEL SOLVED" message fits in. ;-)
    public void ensureSpace (boolean margin)
    {
        int m = (margin ? 1 : 0);

        int l = 0; while (l < levelSizeX && columnBlank (l)) l++;
        int r = 0; while (r < levelSizeX && columnBlank (levelSizeX-r-1)) r++;
        int u = 0; while (u < levelSizeY && rowBlank (u)) u++;
        int d = 0; while (d < levelSizeY && rowBlank (levelSizeY-d-1)) d++;

        // This is the hack: Have the width be at least 7.
        while (levelSizeX + 2*m - l - r < 7) { l--; r--; }

        resize (2*m-l-r, 2*m-u-d, m-l, m-u);
    }
    private boolean columnBlank (int x)
    {
        for (int y = 0; y < levelSizeY; y++)
            if (level [ y*levelSizeX + x ] != BLANK)
                return false;
        return true;
    }
    private boolean rowBlank (int y)
    {
        for (int x = 0; x < levelSizeX; x++)
            if (level [ y*levelSizeX + x ] != BLANK)
                return false;
        return true;
    }

    // any parameter may be negative!
    private void resize (int amountX, int amountY, int shiftX, int shiftY)
    {
        if (amountX == 0 && amountY == 0) return;

        int newSizeX = levelSizeX + amountX;
        int newSizeY = levelSizeY + amountY;
        int[] newLevel = new int [ newSizeX * newSizeY ];

        for (int x = 0; x < (amountX < 0 ? newSizeX : levelSizeX); x++)
        for (int y = 0; y < (amountY < 0 ? newSizeY : levelSizeY); y++)
            newLevel [ (amountY > 0 ? y+shiftY : y)*newSizeX +
                       (amountX > 0 ? x+shiftX : x)              ] =
               level [ (amountY < 0 ? y-shiftY : y)*levelSizeX +
                       (amountX < 0 ? x-shiftX : x)              ];

        sokobanPos += (sokobanPos / levelSizeX) * amountX + shiftX;
        sokobanPos += shiftY * newSizeX;

        level = newLevel;
        levelSizeX = newSizeX;
        levelSizeY = newSizeY;
    }
};
