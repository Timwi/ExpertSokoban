
import waba.fx.*;
import waba.ui.*;
import waba.util.*;
import waba.sys.*;
import superwaba.ext.xplat.ui.*;

public class BSIntQueue
{
    private int[] contents;
    private int firstElement, lastElementPlusOne;
    private boolean empty;
    public BSIntQueue()
    {
        contents = new int[64];
        firstElement = 0;
        lastElementPlusOne = 0;
        empty = true;
    }
    public void add (int i)
    {
        if (firstElement == lastElementPlusOne && !empty)
        {
            int newLength = 2*contents.length;
            int[] newContents = new int [ newLength ];
            for (int j = 0; j < firstElement; j++)
                newContents[j] = contents[j];
            for (int j = firstElement; j < contents.length; j++)
                newContents [ j+contents.length ] = contents[j];
            firstElement += contents.length;
            contents = newContents;
        }
        contents[lastElementPlusOne] = i;
        lastElementPlusOne++;
        if (lastElementPlusOne == contents.length) lastElementPlusOne = 0;
        empty = false;
    }
    public int extract()
    {
        int ret = contents [ firstElement ];
        firstElement++;
        if (firstElement == contents.length) firstElement = 0;
        if (firstElement == lastElementPlusOne) empty = true;
        return ret;
    }
    public int size()
    {
        if (empty) return 0;
        int i = lastElementPlusOne - firstElement;
        return (i <= 0) ? i + contents.length : i;
    }
    public boolean empty() { return empty; }
}
