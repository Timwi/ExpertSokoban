
import waba.fx.*;
import waba.io.*;
import waba.ui.*;
import waba.util.*;
import waba.sys.*;
import superwaba.ext.xplat.ui.*;

public class BSLevelFile
{
    private String fileName;
    private String[] levelFile;

    public BSLevelFile (String filename)
    {
        fileName = filename;
        File f = new File (filename, File.READ_ONLY);
        int s = f.getSize();
        byte[] bbuffer = new byte [ s ];
        f.readBytes (bbuffer, 0, s);
        char[] buffer = waba.sys.UTF8CharacterConverter.bytes2chars (bbuffer, 0, s);

        int lines = 1;  // there is one line more than there are newline characters!
        for (int i = 0; i < buffer.length; i++)
            if (buffer[i] == (char)13 ||
                (buffer[i] == (char)10 && (i == 0 || buffer[i-1] != (char)13)))
                lines++;

        levelFile = new String [ lines ];
        int index = 0;
        levelFile[0] = "";

        for (int i = 0; i < buffer.length; i++)
        {
            if (buffer[i] == (char)13 ||
                (buffer[i] == (char)10 && (i == 0 || buffer[i-1] != (char)13)))
            {
                index++;
                levelFile[index] = "";
            }
            else if (buffer[i] != (char)10 && buffer[i] != (char)13)
                levelFile[index] += buffer[i];
        }
    }

    class VectorAndInt
    {
        private Vector v;
        private int i;
        public VectorAndInt (Vector vector, int integer)
        { v = vector; i = integer; }
        public Vector getVector() { return v; }
        public int getInt() { return i; }
    }

    private VectorAndInt readNext (int i)
    {
        int curLine = i;
        Vector ret = new Vector();
        while (curLine < levelFile.length &&
               (levelFile[curLine].equals ("") ||
                levelFile[curLine].charAt (0) == ';'))
            curLine++;

        if (curLine == levelFile.length) return null;

        while (curLine < levelFile.length            &&
               !levelFile[curLine].equals ("")       &&
               levelFile[curLine].charAt (0) != ';')
        {
            ret.add (levelFile[curLine]);
            curLine++;
        }
        return new VectorAndInt (ret, curLine);
    }

    public SokobanLevel decodeLevel (int levelNum)
    {
        int curLine = 0;
        int curLevelNum = 0;
        Vector curLev = null;

        while (curLevelNum < levelNum)
        {
            VectorAndInt vi = readNext (curLine);
            if (vi == null) return null;
            curLine = vi.getInt();
            curLev = vi.getVector();
            curLevelNum++;
        }

        return SokobanLevel.decode (curLev);
    }

    public void saveLevel (int levelNum, SokobanLevel l)
    {
        Vm.debug ("saveLevel");
        Vector newFile = new Vector();
        int curLine = 0;
        int curLevelNum = 0;
        boolean atEnd = false;

        while (curLevelNum < levelNum-1)
        {
            Vm.debug ("while (curLevelNum < levelNum-1): curLevelNum = " + curLevelNum
                + "; levelNum-1 = " + (levelNum-1));
            VectorAndInt vi = readNext (curLine);
            if (vi == null) // we're at the end of file, just append the level
            {
                atEnd = true;
                break;
            }
            for (int i = curLine; i < vi.getInt(); i++)
                newFile.add (levelFile[i]);
            curLine = vi.getInt();
            curLevelNum++;
        }

        Vm.debug ("atEnd = " + atEnd);
        if (!atEnd)     // skip the level we are supposed to overwrite
        {
            VectorAndInt vi = readNext (curLine);
            if (vi == null)
                atEnd = true;
            else
                curLine = vi.getInt();
        }

        // Encode level
        newFile.add ("");
        newFile.add ("; " + levelNum);
        for (int y = 0; y < l.getSizeY(); y++)
        {
            String thisLine = "";
            for (int x = 0; x < l.getSizeX(); x++)
                thisLine += (l.getSokobanX() == x && l.getSokobanY() == y)
                            ?  (l.getCell (x, y) == SokobanLevel.TARGET ? '+' : '@')
                            :  (l.getCell (x, y) == SokobanLevel.WALL   ? '#' :
                                l.getCell (x, y) == SokobanLevel.PIECE  ? '$' :
                                l.getCell (x, y) == SokobanLevel.TARGET ? '.' :
                                l.getCell (x, y) == SokobanLevel.PIECE_ON_TARGET ? '*' : ' ');
            newFile.add (thisLine);
        }

        VectorAndInt vi = atEnd ? null : readNext (curLine);
        while (vi != null)
        {
            for (int i = curLine; i < vi.getInt(); i++)
                newFile.add (levelFile[i]);
            curLine = vi.getInt();
            vi = readNext (curLine);
        }

        Vm.debug ("save to file");

        // Convert vector "newFile" into String[] and save into actual file
        levelFile = new String [ newFile.size() ];
        File f = new File (fileName, File.DONT_OPEN);
        f.delete();
        f = new File (fileName, File.CREATE);
        for (int i = 0; i < newFile.size(); i++)
        {
            levelFile[i] = (String) newFile.items[i];
            int ln = levelFile[i].length();
            char[] toWrite = new char[ln+2];
            for (int j = 0; j < levelFile[i].length(); j++)
                toWrite[j] = levelFile[i].charAt (j);
            toWrite [ ln ] = (char)13;
            toWrite [ln+1] = (char)10;
            byte[] buffer = waba.sys.UTF8CharacterConverter.chars2bytes (toWrite, 0, toWrite.length-1);
            f.writeBytes (buffer, 0, buffer.length);
        }
    }
}
