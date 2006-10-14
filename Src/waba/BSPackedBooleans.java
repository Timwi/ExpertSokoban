
import waba.sys.*;
import waba.ui.*;
import waba.fx.*;
import waba.util.*;
import waba.io.*;

public class BSPackedBooleans
{
    private byte[] arr;
    public BSPackedBooleans (int length) { arr = new byte [ (length+7)/8 ]; }
    public boolean get (int index) { return (arr[index/8] & (1 << (index % 8))) != 0; }
    public void set (int index, boolean value)
    {
        if (value)
            arr[index/8] |= 1 << (index % 8);
        else
            arr[index/8] &= ~(byte)(1 << (index % 8));
    }
}
