
import waba.fx.*;
import waba.io.*;
import waba.ui.*;
import waba.util.*;
import waba.sys.*;
import superwaba.ext.xplat.ui.*;

public class BSFileDialog extends Window
{
    private String sPath;
    private File fPath;
    private String sFile;

    private ComboBox pathCombo;
    private ListBox fileList;
    private Label pathLabel, fileLabel;
    private BSButton okButton, cancelButton;

    private boolean isOK;

    class BSFDItem
    {
        private String showString, realString;
        public BSFDItem (String s, String r)
        {
            showString = s;
            realString = r;
            Vm.debug ("Adding '" + s + "'/'" + r + "'");
        }
        public String toString() { return showString; }
        public String realString() { return realString; }
    }

    public BSFileDialog (String initPath, String initFile)
    {
        super ("Load level file", NO_BORDER);
        sFile = initFile;
        initElements (initPath, true);
    }

    private String spaces (int i)
    {
        String s = "";
        while (i-- > 0) s += " ";
        return s;
    }
    private void initElements (String initPath, boolean initAll)
    {
        sPath = initPath;
        Vm.debug ("sPath: " + sPath);
        fPath = new File (sPath+"/");

        Rect pr = initAll ? null : pathCombo.getRect();
        Rect fr = initAll ? null : fileList.getRect();

        if (pathCombo != null) remove (pathCombo);
        if (fileList  != null) remove (fileList);

        String workWith = sPath.substring (1);
        String pathSoFar = "";
        Vector itemList = new Vector();
        itemList.add (new BSFDItem ("\\", "\\"));
        int indent = 1;
        while (workWith.indexOf ('\\') > -1)
        {
            int p = workWith.indexOf ('\\');
            pathSoFar += "\\" + workWith.substring (0, p);
            itemList.add (new BSFDItem (
                spaces (3*indent) + workWith.substring (0, p),
                pathSoFar));
            workWith = workWith.substring (p+1);
            indent++;
        }
        if (workWith.length() > 0)
        {
            pathSoFar += "\\" + workWith;
            itemList.add (new BSFDItem (spaces (3*indent) + workWith, pathSoFar));
            indent++;
        }
        int selPath = indent-1;

        Vector fileItems = new Vector();
        String[] files = fPath.listFiles();
        int selFile = -1;
        if (files != null)
        {
            Convert.qsort (files, 0, files.length-1);
            for (int i = 0; i < files.length; i++)
            {
                String f = files[i];
                if (f.charAt (f.length()-1) == '/')
                    itemList.add (new BSFDItem (
                        spaces (3*indent) + f.substring (0, f.length()-1),
                        pathSoFar + "\\" + f.substring (0, f.length()-1)));
                else if (f.substring (f.length()-4).equals (".txt"))
                {
                    if (sFile.equals (sPath + "\\" + f))
                        selFile = i;
                    fileItems.add (new BSFDItem (
                        f.substring (0, f.length()-4),
                        pathSoFar + "\\" + f));
                }
            }
        }

        Object[] itemListA = new BSFDItem [ itemList.size() ];
        for (int i = 0; i < itemList.size(); i++)
            itemListA[i] = itemList.items[i];
        Object[] fileListA = new BSFDItem [ fileItems.size() ];
        for (int i = 0; i < fileItems.size(); i++)
            fileListA[i] = fileItems.items[i];

        pathCombo = new ComboBox (itemListA);
        if (initAll)
        {
            pathLabel = new Label ("Directory:");
            add (pathLabel, LEFT + 5, TOP + 5);
            add (pathCombo, AFTER + 5, SAME);
        }
        else add (pathCombo, pr.x, pr.y);
        pathCombo.select (selPath);

        fileList = new ListBox (fileListA);
        if (initAll)
        {
            fileLabel = new Label ("File:");
            add (fileLabel, LEFT + 5, AFTER + 5);
            add (fileList, SAME, AFTER + 2);
            cancelButton = new BSButton ("Cancel");
            add (cancelButton, RIGHT - 5, BOTTOM - 5);
            okButton = new BSButton ("OK");
            add (okButton, BEFORE - 5, SAME);
        }
        else add (fileList, fr.x, fr.y);

        Rect r = fileList.getRect();
        fileList.setRect (r.x, r.y, 230, 200);

        if (selFile > -1) fileList.select (selFile);
    }
    public void onEvent (Event e)
    {
        if (e.type == ControlEvent.PRESSED && e.target == okButton)
        {
            isOK = true;
            unpop();
        }
        else if (e.type == ControlEvent.PRESSED && e.target == cancelButton)
        {
            isOK = false;
            unpop();
        }
        else if (e.type == ControlEvent.PRESSED && e.target == fileList)
        {
            BSFDItem item = (BSFDItem) fileList.getSelectedItem();
            sFile = item.realString();
        }
        else if (e.type == ControlEvent.PRESSED && e.target == pathCombo)
        {
            BSFDItem item = (BSFDItem) pathCombo.getSelectedItem();
            initElements (item.realString(), false);
        }
    }
    public boolean okPressed() { return isOK; }
    public String getFileName() { return sFile; }
}
