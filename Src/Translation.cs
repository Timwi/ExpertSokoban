using RT.Util;
using RT.Util.Lingo;

namespace ExpertSokoban
{
#pragma warning disable 1591    // Missing XML comment for publicly visible type or member

    public enum TranslationGroup
    {
        [LingoGroup("Menu: Level", null)]
        LevelMenu,

        [LingoGroup("Menu: Edit", null)]
        EditMenu,

        [LingoGroup("Menu: Options", null)]
        OptionsMenu,

        [LingoGroup("Menu: Help", null)]
        HelpMenu,

        [LingoGroup("Menu: Context", @"The context menu which appears when the user right-clicks in the level list.")]
        ContextMenu,

        [LingoGroup("Toolbar: Play", @"Tooltips which appear as the user hovers their mouse over a button on the ""Playing"" toolbar.")]
        ToolPlay,

        [LingoGroup("Toolbar: File", @"Tooltips which appear as the user hovers their mouse over a button on the ""Level file"" toolbar.")]
        ToolFile,

        [LingoGroup("Toolbar: File edit", @"Tooltips which appear as the user hovers their mouse over a button on the ""Level file editing"" toolbar.")]
        ToolFileEdit,

        [LingoGroup("Toolbar: Level edit", @"Tooltips which appear as the user hovers their mouse over a button on the ""Level editing"" toolbar.")]
        ToolEditLevel,

        [LingoGroup("Information messages", "Messages displayed to the user in a dialog box with only an OK button.")]
        Info,

        [LingoGroup("Confirmation messages", "Confirmation messages (usually yes/no questions) that are displayed to the user in dialog boxes (small windows) containing two or three buttons for the user to choose from.")]
        Confirm,

        [LingoGroup("Prompts", "Messages that prompt the user to supply information.")]
        Prompt,

        [LingoGroup("Message titles", "Strings displayed in the title bar of some information messages, prompts or confirmation messages. Most of these are identical to the name of an action (toolbar button or menu item).")]
        TitleBar,

        [LingoGroup("Status bar messages", "Messages that appear in the status bar at the bottom of the main window.")]
        StatusBar,

        [LingoGroup("Level list messages", "Messages displayed in the level list in the right-hand part of the main window.")]
        LevelList,

        [LingoGroup("Highscores", @"The Highscores window, which appears if you right-click a level and choose ""Display highscores"".")]
        Highscores,

        [LingoGroup("About", @"The About window, which appears if you choose ""About"" from the ""Help"" menu.")]
        About,

        [LingoGroup("General", null)]
        General,

        [LingoGroup("Accessibility", @"Strings which are not visually displayed, but may be used by screen readers to help blind users navigate the software.")]
        Accessibility,
    }

    [LingoStringClass]
#if DEBUG
    [LingoDebug(@"..\..\main\ExpSok\Translation.cs")]
#endif
    public class Translation : TranslationBase
    {
        public static readonly Language DefaultLanguage = Language.EnglishUK;

        public Translation() : base(DefaultLanguage) { }

        [LingoInGroup(TranslationGroup.General)]
        public TrString ProgramName = "Expert Sokoban";

        [LingoInGroup(TranslationGroup.General)]
        [LingoNotes("Specifies the resource name of the large image that appears on the screen when the user solves a level.")]
        public TrString LevelSolvedResourceName = "Skin_LevelSolved";

        [LingoInGroup(TranslationGroup.StatusBar)]
        public TrString Mainform_Status_Moves = "Moves: {0}";
        [LingoInGroup(TranslationGroup.StatusBar)]
        public TrString Mainform_Status_Pushes = "Pushes: {0}";
        [LingoInGroup(TranslationGroup.StatusBar)]
        public TrString Mainform_Status_PiecesRemaining = "Remaining pieces: {0}";

        [LingoInGroup(TranslationGroup.LevelList)]
        public TrString LevelList_LevelSolved = "Solved";
        [LingoInGroup(TranslationGroup.LevelList)]
        public TrString LevelList_CurrentlyEditing = "Currently editing";
        [LingoInGroup(TranslationGroup.LevelList)]
        public TrString LevelList_JustSolved = "Just solved";
        [LingoInGroup(TranslationGroup.LevelList)]
        public TrString LevelList_CurrentlyPlaying = "Currently playing";

        [LingoInGroup(TranslationGroup.General)]
        [LingoNotes(@"Used in the ""File type"" drop-down in the ""Save"" dialog to refer to text files, i.e. files with the *.txt extension.")]
        public TrString Save_FileType_TextFiles = "Text files";
        [LingoInGroup(TranslationGroup.General)]
        [LingoNotes(@"Used in the ""File type"" drop-down in the ""Save"" dialog to refer to all files.")]
        public TrString Save_FileType_AllFiles = "All files";
        [LingoInGroup(TranslationGroup.General)]
        [LingoNotes("Displayed in the main window's title bar to signify that the current level file has not yet been named (i.e. it has no filename).")]
        public TrString FileName_Untitled = "(untitled)";
        [LingoInGroup(TranslationGroup.General)]
        [LingoNotes("Displayed in the main window's title bar if the player has not chosen a name yet.")]
        public TrString PlayerNameMissing = "(no player name)";

        [LingoInGroup(TranslationGroup.Prompt)]
        [LingoNotes(@"Displayed when the user clicks ""New comment"".")]
        public TrString NewComment_Prompt = "Please enter a comment:";
        [LingoInGroup(TranslationGroup.Prompt)]
        [LingoNotes(@"Displayed when the user clicks ""Edit comment"".")]
        public TrString EditComment_Prompt = "Please enter the revised comment:";

        [LingoInGroup(TranslationGroup.Info)]
        public TrString LevelList_Message_CannotSaveSettings_Title = "Error saving settings";
        [LingoInGroup(TranslationGroup.Info)]
        public TrString LevelList_Message_CannotSaveSettings = "The settings could not be saved.";

        [LingoInGroup(TranslationGroup.Info)]
        public TrString LevelList_Message_AllSolved_Title = "Congratulations!";
        [LingoInGroup(TranslationGroup.Info)]
        public TrString LevelList_Message_AllSolved = "You have solved all levels in this level file!";
        [LingoInGroup(TranslationGroup.Info)]
        public TrString LevelList_Message_Next_Title = "Next level";
        [LingoInGroup(TranslationGroup.Info)]
        public TrString LevelList_Message_NextUnsolved_Title = "Next unsolved level";
        [LingoInGroup(TranslationGroup.Info)]
        public TrString LevelList_Message_Prev_Title = "Previous level";
        [LingoInGroup(TranslationGroup.Info)]
        public TrString LevelList_Message_PrevUnsolved_Title = "Previous unsolved level";
        [LingoInGroup(TranslationGroup.Info)]
        public TrString LevelList_Message_NoMoreUnsolved = "There are no more unsolved levels in this level file.";
        [LingoInGroup(TranslationGroup.Info)]
        public TrString LevelList_Message_NoOtherLevel = "There is no other level in the level file.";
        [LingoInGroup(TranslationGroup.Info)]
        public TrString Mainform_InvalidFile_Title = "Error opening level file";
        [LingoInGroup(TranslationGroup.Info)]
        public TrString Mainform_InvalidFile = "The selected file is not a valid Sokoban level file.";
        [LingoInGroup(TranslationGroup.Info)]
        public TrString Mainform_NoHighscores_Title = "No highscores for this level";
        [LingoInGroup(TranslationGroup.Info)]
        public TrString Mainform_NoHighscores = "The selected level does not have any highscores associated with it yet.";

        [LingoInGroup(TranslationGroup.TitleBar)]
        public TrString NewComment_Title = "New comment";
        [LingoInGroup(TranslationGroup.TitleBar)]
        public TrString EditComment_Title = "Edit comment";
        [LingoInGroup(TranslationGroup.TitleBar)]
        public TrString LevelList_Message_DeleteLevel_Title = "Delete level";

        [LingoInGroup(TranslationGroup.Confirm)]
        public TrString LevelList_Message_SaveChanges = "You have made changes to {0}. Would you like to save those changes?";
        [LingoInGroup(TranslationGroup.Confirm)]
        public TrString LevelList_Message_DeleteLevel_CurrentlyEditing = "You are currently editing this level.\n\nIf you delete this level now, all your modifications will be discarded.\n\nAre you sure you wish to do this?";
        [LingoInGroup(TranslationGroup.Confirm)]
        public TrString LevelList_Message_DeleteLevel_CurrentlyPlaying = "You are currently playing this level.\n\nAre you sure you wish to give up and delete this level?";
        [LingoInGroup(TranslationGroup.Confirm)]
        public TrString LevelList_Message_DeleteLevel_Sure = "Are you sure you wish to delete this level?";
        [LingoInGroup(TranslationGroup.Confirm)]
        public TrString LevelList_Message_DeleteLevel_btnDelete = "&Delete level";
        [LingoInGroup(TranslationGroup.Confirm)]
        public TrString MainArea_Message_SaveChanges = "Would you like to save your changes to the level you're editing?";
        [LingoInGroup(TranslationGroup.Confirm)]
        public TrString MainArea_Message_GiveUp = "Are you sure you wish to give up the current level?";

        [LingoInGroup(TranslationGroup.Prompt)]
        public TrString Mainform_ChooseName = "Please choose a name which will be used to identify you in highscore tables.";
        [LingoInGroup(TranslationGroup.Prompt)]
        public TrString Mainform_ChooseName_FirstRun = "Please choose a name which will be used to identify you in highscore tables.\nYou can change this name later by selecting \"Change player name\" from the \"Level\" menu.";
        [LingoInGroup(TranslationGroup.Prompt)]
        public TrString Mainform_ChooseName_SolvedLevel = "Congratulations! You've solved the current level.\nPlease choose a name which will be used to identify you in highscore tables.\nIf you do not choose a name now, your score for this level will not be recorded.\nYou can change this name again later by selecting \"Change player name\" from the \"Level\" menu.";

        [LingoInGroup(TranslationGroup.Confirm)]
        public TrString Mainform_Validity_CannotOpen = "The level could not be opened because it is invalid.";
        [LingoInGroup(TranslationGroup.StatusBar)]
        [LingoNotes("Displayed in the status bar while editing a level. See the next two strings for reasons why a level may be invalid.")]
        public TrString Mainform_Validity_Valid = "The level is valid.";
        [LingoInGroup(TranslationGroup.StatusBar)]
        [LingoInGroup(TranslationGroup.Confirm)]
        public TrString Mainform_Validity_NotEnclosed = "The level is invalid because it is not completely enclosed by a wall.";
        [LingoInGroup(TranslationGroup.StatusBar)]
        [LingoInGroup(TranslationGroup.Confirm)]
        public TrString Mainform_Validity_WrongNumber = "The level is invalid because the number of pieces does not match the number of targets.";
        [LingoInGroup(TranslationGroup.Confirm)]
        public TrString Mainform_Validity_CannotOpen_Fix = "You must edit the level in order to address this issue. Would you like to edit the level now?";
        [LingoInGroup(TranslationGroup.Confirm)]
        public TrString Mainform_Validity_CannotOpen_btnEdit = "&Edit level";
        [LingoInGroup(TranslationGroup.Confirm)]
        public TrString Mainform_Validity_CannotSave_Warning = "You cannot play this level until you address this issue. Are you sure you wish to leave the level in this invalid state?";
        [LingoInGroup(TranslationGroup.Confirm)]
        public TrString Mainform_Validity_CannotSave_btnSave = "&Save level anyway";
        [LingoInGroup(TranslationGroup.Confirm)]
        public TrString Mainform_Validity_CannotSave_btnResume = "&Resume editing";

        [LingoInGroup(TranslationGroup.TitleBar)]
        public TrString Mainform_MessageTitle_Exit = "Exit Expert Sokoban";
        [LingoInGroup(TranslationGroup.TitleBar)]
        public TrString Mainform_MessageTitle_OpenLevel = "Open level";
        [LingoInGroup(TranslationGroup.TitleBar)]
        public TrString Mainform_MessageTitle_RetryLevel = "Retry level";
        [LingoInGroup(TranslationGroup.TitleBar)]
        public TrString Mainform_MessageTitle_NewLevelFile = "New level file";
        [LingoInGroup(TranslationGroup.TitleBar)]
        public TrString Mainform_MessageTitle_OpenLevelFile = "Open level file";
        [LingoInGroup(TranslationGroup.TitleBar)]
        public TrString Mainform_MessageTitle_FinishEditing = "Finish editing";

        [LingoInGroup(TranslationGroup.Info)]
        public TrString Mainform_Error_HelpFileNotFound = "The help file ({0}) could not be found.";

        [LingoInGroup(TranslationGroup.Confirm)]
        [LingoNotes(@"Button in many dialogs; goes together with the ""Cancel"" button below.")]
        public TrString Dialogs_btnOK = "OK";
        [LingoInGroup(TranslationGroup.Confirm)]
        [LingoNotes(@"Button in many dialogs; goes together with the ""OK"" button above.")]
        public TrString Dialogs_btnCancel = "Cancel";
        [LingoInGroup(TranslationGroup.Confirm)]
        [LingoNotes(@"Button in dialogs where the user has a choice to save or discard their changes to a level or to the level file.")]
        public TrString Dialogs_btnSave = "&Save changes";
        [LingoInGroup(TranslationGroup.Confirm)]
        [LingoNotes(@"Button in dialogs where the user has a choice to save or discard their changes to a level or to the level file.")]
        public TrString Dialogs_btnDiscard = "&Discard changes";
        [LingoInGroup(TranslationGroup.Confirm)]
        [LingoNotes(@"Button in dialogs where the user has a choice to give up the level they are currently playing.")]
        public TrString Dialogs_btnGiveUp = "&Give up";

#if DEBUG
        [LingoDebug(@"..\..\main\ExpSok\Translation.cs")]
#endif
        [LingoStringClass]
        public class HighscoresFormTranslation
        {
            #region HighscoresFormTranslation
            [LingoInGroup(TranslationGroup.Highscores)]
            [LingoNotes("Title bar.")]
            public TrString HighscoresForm = "Highscores";
            [LingoInGroup(TranslationGroup.Highscores)]
            public TrStringNum Highscores = new TrStringNum(new[] { "{0} move, {1} push", "{0} moves, {1} push", "{0} move, {1} pushes", "{0} moves, {1} pushes" }, new[] { true, true });
            [LingoInGroup(TranslationGroup.Highscores)]
            public TrString btnOK = "OK";
            #endregion
        }
        public HighscoresFormTranslation Highscores = new HighscoresFormTranslation();

#if DEBUG
        [LingoDebug(@"..\..\main\ExpSok\Translation.cs")]
#endif
        [LingoStringClass]
        public class AboutBoxTranslation
        {
            #region AboutBoxTranslation
            [LingoInGroup(TranslationGroup.About)]
            [LingoNotes("Title bar.")]
            public TrString AboutBox = "About Expert Sokoban";
            [LingoInGroup(TranslationGroup.About)]
            [LingoNotes("This is displayed in a box in the About dialog. Please feel free to add a line to credit yourself for your translation work.")]
            public TrString lblCredits = "Credits:\n    Programming: Timwi, Roman\n    Graphics: Roman, Timwi\n    Testing: Hawthorn";
            [LingoInGroup(TranslationGroup.About)]
            public TrString btnOK = "OK";
            #endregion
            [LingoInGroup(TranslationGroup.About)]
            public TrString Version = "Version {0}";
        }
        public AboutBoxTranslation AboutBox = new AboutBoxTranslation();

#if DEBUG
        [LingoDebug(@"..\..\main\ExpSok\Translation.cs")]
#endif
        [LingoStringClass]
        public class MainformTranslation
        {
            #region MainformTranslation
            [LingoInGroup(TranslationGroup.General)]
            [LingoNotes("Main window title bar.")]
            public TrString Mainform = "Expert Sokoban";
            [LingoInGroup(TranslationGroup.Accessibility)]
            [LingoNotes("Describes the main menu bar.")]
            public TrString mnuMain = "Main menu";
            [LingoInGroup(TranslationGroup.LevelMenu)]
            public TrString mnuLevel = "&Level";
            [LingoInGroup(TranslationGroup.LevelMenu)]
            public TrString mnuLevelNew = "&New level file";
            [LingoInGroup(TranslationGroup.LevelMenu)]
            public TrString mnuLevelOpen = "&Open level file...";
            [LingoInGroup(TranslationGroup.LevelMenu)]
            public TrString mnuLevelSave = "&Save level file";
            [LingoInGroup(TranslationGroup.LevelMenu)]
            public TrString mnuLevelSaveAs = "Save level file &as...";
            [LingoInGroup(TranslationGroup.LevelMenu)]
            public TrString mnuLevelUndo = "&Undo move";
            [LingoInGroup(TranslationGroup.LevelMenu)]
            public TrString mnuLevelRedo = "Redo &move";
            [LingoInGroup(TranslationGroup.LevelMenu)]
            public TrString mnuLevelRetry = "&Retry level";
            [LingoInGroup(TranslationGroup.LevelMenu)]
            public TrString mnuLevelHighscores = "Show &highscores";
            [LingoInGroup(TranslationGroup.LevelMenu)]
            public TrString mnuLevelPrevious = "&Previous level";
            [LingoInGroup(TranslationGroup.LevelMenu)]
            public TrString mnuLevelNext = "N&ext level";
            [LingoInGroup(TranslationGroup.LevelMenu)]
            public TrString mnuLevelPreviousUnsolved = "Pre&vious unsolved level";
            [LingoInGroup(TranslationGroup.LevelMenu)]
            public TrString mnuLevelNextUnsolved = "Next unsolve&d level";
            [LingoInGroup(TranslationGroup.LevelMenu)]
            public TrString mnuLevelChangePlayer = "&Change player name...";
            [LingoInGroup(TranslationGroup.LevelMenu)]
            public TrString mnuLevelExit = "E&xit";
            [LingoInGroup(TranslationGroup.EditMenu)]
            public TrString mnuEdit = "&Edit";
            [LingoInGroup(TranslationGroup.EditMenu)]
            public TrString mnuEditCreateLevel = "Create &new level";
            [LingoInGroup(TranslationGroup.EditMenu)]
            public TrString mnuEditEditLevel = "&Edit level";
            [LingoInGroup(TranslationGroup.EditMenu)]
            public TrString mnuEditAddComment = "Add a co&mment...";
            [LingoInGroup(TranslationGroup.EditMenu)]
            public TrString mnuEditDelete = "&Delete level/comment";
            [LingoInGroup(TranslationGroup.EditMenu)]
            public TrString mnuEditCut = "C&ut";
            [LingoInGroup(TranslationGroup.EditMenu)]
            public TrString mnuEditCopy = "&Copy";
            [LingoInGroup(TranslationGroup.EditMenu)]
            public TrString mnuEditPaste = "&Paste";
            [LingoInGroup(TranslationGroup.EditMenu)]
            public TrString mnuEditFinish = "&Finish editing";
            [LingoInGroup(TranslationGroup.EditMenu)]
            public TrString mnuEditCancel = "C&ancel editing";
            [LingoInGroup(TranslationGroup.EditMenu)]
            public TrString mnuEditWall = "&Wall tool";
            [LingoInGroup(TranslationGroup.EditMenu)]
            public TrString mnuEditPiece = "P&iece tool";
            [LingoInGroup(TranslationGroup.EditMenu)]
            public TrString mnuEditTarget = "&Target tool";
            [LingoInGroup(TranslationGroup.EditMenu)]
            public TrString mnuEditSokoban = "&Sokoban tool";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptions = "&Options";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsLevelList = "Display &level list";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsPlayingToolbar = "Display pla&ying toolbar";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsFileToolbars = "Display &editing toolbars (level file)";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsEditLevelToolbar = "Display editin&g toolbar (level)";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsStatusBar = "Display stat&us bar";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsMoveNo = "Don't display &move path";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsMoveLine = "Display move path as li&ne";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsMoveDots = "Display move path as &dots";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsMoveArrows = "Display mo&ve path as arrows";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsPushNo = "Don't display &push path";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsPushLine = "Display push path as l&ine";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsPushDots = "Display push path as do&ts";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsPushArrows = "Display push path as arro&ws";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsEndPos = "Display end p&osition of Sokoban and piece";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsAreaSokoban = "Display reac&hable area for Sokoban";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsAreaPiece = "Display reachable area &for piece";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsLetterControl = "Enable lette&r control";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsLetterControlNext = "Show ne&xt letter control set";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsSound = "Enable &sound";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsAnimation = "Enable &animations";
            [LingoInGroup(TranslationGroup.OptionsMenu)]
            public TrString mnuOptionsChangeLanguage = "&Change language";
            [LingoInGroup(TranslationGroup.HelpMenu)]
            public TrString mnuHelp = "&Help";
            [LingoInGroup(TranslationGroup.HelpMenu)]
            public TrString mnuHelpHelp = "&Help...";
            [LingoInGroup(TranslationGroup.HelpMenu)]
            public TrString mnuHelpAbout = "&About";
            [LingoInGroup(TranslationGroup.StatusBar)]
            public TrString lblStatusSolved = "You have solved the level. Congratulations!";
            [LingoInGroup(TranslationGroup.StatusBar)]
            public TrString lblStatusNull = "No levels currently selected. Select a level from the level list to play.";
            [LingoInGroup(TranslationGroup.Accessibility)]
            [LingoNotes(@"Describes the toolbar which contains commands for editing a level.")]
            public TrString toolEditLevel = "Level edit toolbar";
            [LingoInGroup(TranslationGroup.ToolEditLevel)]
            public TrString btnEditLevelWall = "Wall tool";
            [LingoInGroup(TranslationGroup.ToolEditLevel)]
            public TrString btnEditLevelPiece = "Piece tool";
            [LingoInGroup(TranslationGroup.ToolEditLevel)]
            public TrString btnEditLevelTarget = "Target tool";
            [LingoInGroup(TranslationGroup.ToolEditLevel)]
            public TrString btnEditLevelSokoban = "Sokoban tool";
            [LingoInGroup(TranslationGroup.ToolEditLevel)]
            public TrString btnEditLevelOK = "Finish editing";
            [LingoInGroup(TranslationGroup.ToolEditLevel)]
            public TrString btnEditLevelCancel = "Cancel editing";
            [LingoInGroup(TranslationGroup.Accessibility)]
            [LingoNotes(@"Describes the toolbar which contains commands for editing a level file.")]
            public TrString toolFileEdit = "File edit toolbar";
            [LingoInGroup(TranslationGroup.ToolFileEdit)]
            public TrString btnFileEditNewLevel = "Create new level";
            [LingoInGroup(TranslationGroup.ToolFileEdit)]
            public TrString btnFileEditEditLevel = "Edit level";
            [LingoInGroup(TranslationGroup.ToolFileEdit)]
            public TrString btnFileEditAddComment = "Add a comment";
            [LingoInGroup(TranslationGroup.ToolFileEdit)]
            public TrString btnFileEditDeleteLevel = "Delete selected level or comment";
            [LingoInGroup(TranslationGroup.Accessibility)]
            [LingoNotes(@"Describes the toolbar which contains commands for handling level files.")]
            public TrString toolFile = "File toolbar";
            [LingoInGroup(TranslationGroup.ToolFile)]
            public TrString btnFileNew = "New level file";
            [LingoInGroup(TranslationGroup.ToolFile)]
            public TrString btnFileOpen = "Open level file";
            [LingoInGroup(TranslationGroup.ToolFile)]
            public TrString btnFileSave = "Save level file";
            [LingoInGroup(TranslationGroup.ToolFile)]
            public TrString btnFileCut = "Cut";
            [LingoInGroup(TranslationGroup.ToolFile)]
            public TrString btnFileCopy = "Copy";
            [LingoInGroup(TranslationGroup.ToolFile)]
            public TrString btnFilePaste = "Paste";
            [LingoInGroup(TranslationGroup.Accessibility)]
            [LingoNotes(@"Describes the toolbar which contains commands for playing the game.")]
            public TrString toolPlay = "Playing toolbar";
            [LingoInGroup(TranslationGroup.ToolPlay)]
            public TrString btnPlayOpenLevel = "Open level file";
            [LingoInGroup(TranslationGroup.ToolPlay)]
            public TrString btnPlayPrevLevel = "Previous level";
            [LingoInGroup(TranslationGroup.ToolPlay)]
            public TrString btnPlayNextLevel = "Next level";
            [LingoInGroup(TranslationGroup.ToolPlay)]
            public TrString btnPlayPrevUnsolvedLevel = "Previous unsolved level";
            [LingoInGroup(TranslationGroup.ToolPlay)]
            public TrString btnPlayNextUnsolvedLevel = "Next unsolved level";
            #endregion
        }
        public MainformTranslation Mainform = new MainformTranslation();

#if DEBUG
        [LingoDebug(@"..\..\main\ExpSok\Translation.cs")]
#endif
        [LingoStringClass]
        public class ContextMenuTranslation
        {
            #region ContextMenuTranslation
            [LingoInGroup(TranslationGroup.ContextMenu)]
            public TrString mnuContextPlay = "Pl&ay this level";
            [LingoInGroup(TranslationGroup.ContextMenu)]
            public TrString mnuContextEdit = "&Edit this level";
            [LingoInGroup(TranslationGroup.ContextMenu)]
            public TrString mnuContextHighscores = "Show &highscores";
            [LingoInGroup(TranslationGroup.ContextMenu)]
            public TrString mnuContextNewLevel = "C&reate a new level here";
            [LingoInGroup(TranslationGroup.ContextMenu)]
            public TrString mnuContextNewComment = "&Insert a comment here";
            [LingoInGroup(TranslationGroup.ContextMenu)]
            public TrString mnuContextCut = "C&ut";
            [LingoInGroup(TranslationGroup.ContextMenu)]
            public TrString mnuContextCopy = "&Copy";
            [LingoInGroup(TranslationGroup.ContextMenu)]
            public TrString mnuContextPaste = "&Paste";
            [LingoInGroup(TranslationGroup.ContextMenu)]
            public TrString mnuContextDelete = "&Delete";
            [LingoInGroup(TranslationGroup.ContextMenu)]
            public TrString mnuContextHide = "Hide &level list";
            #endregion
        }
        public ContextMenuTranslation Context = new ContextMenuTranslation();
    }

#pragma warning restore 1591    // Missing XML comment for publicly visible type or member
}
