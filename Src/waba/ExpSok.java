
import waba.fx.*;
import waba.io.*;
import waba.ui.*;
import waba.util.*;
import waba.sys.*;
import superwaba.ext.xplat.ui.*;

public class ExpSok extends MainWindow
{
    private SokobanLevel currentLevel;
    private BSButton prevLevelButton, nextLevelButton, undoButton,
                restartButton, loadLevelButton, editButton, exitButton;

    // Either one of the following two is always null
    private BSMainArea mainArea;

    private String filePath;
    private String levelFileName;
    private int levelNumber;
    private BSLevelFile levelFile;
    private boolean editing;

    public ExpSok()
    {
        //waba.sys.Settings.setPalmOSStyle(true);
        Vm.debug (Vm.ERASE_DEBUG);

        Convert.setDefaultConverter ("UTF8");
        backColor = new Color (0xFF, 0xFF, 0xCE);

        currentLevel = null;
        loadSettings();
        if (levelFileName != null && !levelFileName.equals (""))
        {
            levelFile = new BSLevelFile (levelFileName);
            if (levelNumber > 0)
                currentLevel = levelFile.decodeLevel (levelNumber);
        }

        if (currentLevel == null)
            currentLevel = new SokobanLevel (SokobanLevel.testLevelSizeX,
                SokobanLevel.testLevelSizeY, SokobanLevel.testLevel, SokobanLevel.testSokobanPos);

        currentLevel.ensureSpace (false);

        prevLevelButton = new BSButton ("Prev");
        nextLevelButton = new BSButton ("Next");
        undoButton      = new BSButton ("Undo");
        restartButton   = new BSButton ("Restart");
        loadLevelButton = new BSButton ("Load");
        editButton      = new BSButton ("Edit");
        exitButton      = new BSButton ("Exit");

        add (prevLevelButton, LEFT+4, BOTTOM-3);
        add (nextLevelButton, AFTER+5, SAME);
        add (undoButton     , AFTER+5, SAME);
        add (restartButton  , AFTER+5, SAME);
        add (loadLevelButton, AFTER+5, SAME);
        add (editButton     , AFTER+5, SAME);
        add (exitButton     , AFTER+5, SAME);

        editing = false;
        mainArea = null;
        initMainArea();
    }

    private void initMainArea()
    {
        if (mainArea != null)
        {
            mainArea.aboutToDestroy();
            remove (mainArea);
        }

        Rect cr = getClientRect();
        mainArea = new BSMainArea (currentLevel, this, editing);
        add (mainArea, 0, 0);
        placeMainArea();
        mainArea.initialise();
    }
    public void placeMainArea()
    {
        Rect cr = getClientRect();
        mainArea.setRect (cr.width/2 - mainArea.getMyWidth()/2,
            (cr.height-20)/2 - mainArea.getMyHeight()/2,
            mainArea.getMyWidth(), mainArea.getMyHeight());
        repaint();
    }

    private void loadSettings()
    {
        filePath = "\\";
        String settings = Settings.appSettings;
        if (settings == null || settings.equals ("")) return;
        int p = settings.indexOf ("|");
        if (p == -1) { filePath = settings; return; }
        filePath = settings.substring (0, p);
        settings = settings.substring (p+1);

        p = settings.indexOf ("|");
        if (p == -1) { levelFileName = settings; return; }
        levelFileName = settings.substring (0, p);
        settings = settings.substring (p+1);

        levelNumber = Convert.toInt (settings);
    }

    private void doExit()
    {
        Settings.appSettings = filePath + "|" + levelFileName + "|" + levelNumber;
        exit (0);
    }

    public void onEvent (Event e)
    {
        if (e.type == ControlEvent.PRESSED)
        {
            if (e.target == prevLevelButton && levelNumber > 1)
            {
                levelNumber--;
                currentLevel = levelFile.decodeLevel (levelNumber);
                currentLevel.ensureSpace (true);
                initMainArea();
            }
            else if (e.target == nextLevelButton)
            {
                levelNumber++;
                SokobanLevel tmpLevel = levelFile.decodeLevel (levelNumber);
                if (tmpLevel == null && editing)
                {
                    MessageBox mb = new MessageBox ("Expert Sokoban",
                        "There is no next level|(this is the last level in the file).||" +
                        "Would you like to create a new one?", new String[] { "Yes", "No" });
                    mb.setBackColor (new Color (0xcf, 0xff, 0xcf));
                    mb.setForeColor (Color.BLACK);
                    popupBlockingModal (mb);
                    if (mb.getPressedButtonIndex() == 0)
                        tmpLevel = new SokobanLevel (SokobanLevel.testLevelSizeX,
                            SokobanLevel.testLevelSizeY, SokobanLevel.testLevel,
                            SokobanLevel.testSokobanPos);
                }
                if (tmpLevel == null)
                    levelNumber--;
                else
                {
                    currentLevel = tmpLevel;
                    currentLevel.ensureSpace (editing);
                    initMainArea();
                }
            }
            else if (e.target == undoButton) mainArea.undo();
            else if (e.target == restartButton)
            {
                currentLevel = levelFile.decodeLevel (levelNumber);
                initMainArea();
            }
            else if (e.target == loadLevelButton)
            {
                BSFileDialog fileDialog = new BSFileDialog (filePath, levelFileName);
                popupBlockingModal (fileDialog);

                if (fileDialog.okPressed())
                {
                    Graphics g = mainArea.createGraphics();
                    g.setBackColor (new Color (0, 0, 0xA0));
                    g.fillEllipse (mainArea.getMyWidth()/2, mainArea.getMyHeight()/2, 57, 20);
                    g.setForeColor (Color.BLACK);
                    g.drawEllipse (mainArea.getMyWidth()/2, mainArea.getMyHeight()/2, 57, 20);
                    g.setForeColor (Color.WHITE);
                    g.drawText ("LOADING...", mainArea.getMyWidth()/2 - 27, mainArea.getMyHeight()/2 - 8);
                    levelFileName = fileDialog.getFileName();
                    levelFile = new BSLevelFile (levelFileName);
                    levelNumber = 1;
                    currentLevel = levelFile.decodeLevel (levelNumber);
                    initMainArea();
                }
            }
            else if (e.target == editButton)
            {
                if (editing)
                {
                    currentLevel.ensureSpace (false);
                    levelFile.saveLevel (levelNumber, currentLevel);
                }
                else
                {
                    currentLevel = levelFile.decodeLevel (levelNumber);
                    currentLevel.ensureSpace (true);
                }
                editing = !editing;
                editButton.setText (editing ? "Play" : "Edit");
                initMainArea();
            }
            else if (e.target == exitButton) doExit();
        }
    }
}
