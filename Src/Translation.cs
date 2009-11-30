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
    public class Translation : TranslationBase
    {
        public static readonly Language DefaultLanguage = Language.EnglishUK;

        public Translation() : base(DefaultLanguage) { }

        // Classes containing auto-generated string codes
        public HighscoresFormTranslation Highscores = new HighscoresFormTranslation();
        public AboutBoxTranslation AboutBox = new AboutBoxTranslation();
        public MainformTranslation Mainform = new MainformTranslation();
        public ContextMenuTranslation Context = new ContextMenuTranslation();

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
    }

    public partial class HighscoresFormTranslation
    {
        public TrStringNum Highscores = new TrStringNum(new[] { "{0} move, {1} push", "{0} moves, {1} push", "{0} move, {1} pushes", "{0} moves, {1} pushes" }, new[] { true, true });
    }

    public partial class AboutBoxTranslation
    {
        public TrString Version = "Version {0}";
    }

    public partial class MainformTranslation { }
    public partial class ContextMenuTranslation { }

#pragma warning restore 1591    // Missing XML comment for publicly visible type or member
}
