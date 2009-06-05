using RT.Util;
using RT.Util.Lingo;

namespace ExpertSokoban
{
    [LingoGroup("Expert Sokoban", "This group contains various individual strings.")]
#if DEBUG
    [LingoDebug(RelativePath = @"..\..\main\ExpSok\Translation.cs")]
#endif
    public class Translation : TranslationBase
    {
        public Translation() : base(Language.EnglishUK) { }

        public TrString ProgramName = "Expert Sokoban";

        [LingoNotes(@"Specifies the resource name of the large image that appears on the screen when the user solves a level.")]
        public TrString LevelSolvedResourceName = "Skin_LevelSolved";

        public TrString Mainform_Status_Moves = "Moves: {0}";
        public TrString Mainform_Status_Pushes = "Pushes: {0}";
        public TrString Mainform_Status_PiecesRemaining = "Remaining pieces: {0}";

        public TrString Mainform_Validity_Valid = "The level is valid.";
        public TrString Mainform_Validity_NotEnclosed = "The level is invalid because it is not completely enclosed by a wall.";
        public TrString Mainform_Validity_WrongNumber = "The level is invalid because the number of pieces does not match the number of targets.";

        public TrString LevelList_LevelSolved = "Solved";
        public TrString LevelList_CurrentlyEditing = "Currently editing";
        public TrString LevelList_JustSolved = "Just solved";
        public TrString LevelList_CurrentlyPlaying = "Currently playing";

        public TrString Save_FileType_TextFiles = "Text files";
        public TrString Save_FileType_AllFiles = "All files";
        public TrString FileName_Untitled = "(untitled)";

        [LingoNotes("Displayed in the window title bar if the player has not chosen a name yet.")]
        public TrString PlayerNameMissing = "(no player name)";

        [LingoGroup("Confirmation messages", "This group contains confirmation messages and error messages displayed to the user in a separate, small window.")]
        public class ConfirmationMessagesTranslation
        {
            public TrString NewComment_Title = "New comment";
            public TrString NewComment_Prompt = "Please enter a comment:";
            public TrString EditComment_Title = "Edit comment";
            public TrString EditComment_Prompt = "Please enter the revised comment:";

            public TrString LevelList_Message_CannotSaveSettings = "The settings could not be saved.";
            public TrString LevelList_Message_CannotSaveSettings_Title = "Error saving settings";
            public TrString LevelList_Message_AllSolved = "You have solved all levels in this level pack!";
            public TrString LevelList_Message_AllSolved_Title = "Congratulations!";
            public TrString LevelList_Message_NoMoreUnsolved = "There are no more unsolved levels in this level file.";
            public TrString LevelList_Message_Next_Title = "Next level";
            public TrString LevelList_Message_NextUnsolved_Title = "Next unsolved level";
            public TrString LevelList_Message_Prev_Title = "Previous level";
            public TrString LevelList_Message_PrevUnsolved_Title = "Previous unsolved level";
            public TrString LevelList_Message_NoOtherLevel = "There is no other level in the level file.";
            public TrString LevelList_Message_SaveChanges = "You have made changes to {0}. Would you like to save those changes?";
            public TrString LevelList_Message_SaveChanges_btnSave = "Save changes";
            public TrString LevelList_Message_SaveChanges_btnDiscard = "&Discard changes";
            public TrString LevelList_Message_DeleteLevel_CurrentlyEditing = "You are currently editing this level.\n\nIf you delete this level now, all your modifications will be discarded.\n\nAre you sure you wish to do this?";
            public TrString LevelList_Message_DeleteLevel_Title = "Delete level";
            public TrString LevelList_Message_DeleteLevel_CurrentlyPlaying = "You are currently playing this level.\n\nAre you sure you wish to give up?";
            public TrString LevelList_Message_DeleteLevel_Sure = "Are you sure you wish to delete this level?";
            public TrString LevelList_Message_DeleteLevel_btnDelete = "Delete level";

            public TrString MainArea_Message_DiscardChanges = "Are you sure you wish to discard your changes to the level you're editing?";
            public TrString MainArea_Message_GiveUp = "Are you sure you wish to give up the current level?";

            public TrString Mainform_ChooseName = "Please choose a name which will be used to identify you in highscore tables.";
            public TrString Mainform_ChooseName_FirstRun = "Please choose a name which will be used to identify you in highscore tables.\nYou can change this name later by selecting \"Change player name\" from the \"Level\" menu.";
            public TrString Mainform_ChooseName_SolvedLevel = "Congratulations! You've solved the current level.\nPlease choose a name which will be used to identify you in highscore tables.\nIf you do not choose a name now, your score for this level will not be recorded.\nYou can change this name again later by selecting \"Change player name\" from the \"Level\" menu.";

            public TrString Mainform_Validity_CannotOpen = "The level could not be opened because it is invalid.";
            public TrString Mainform_Validity_CannotOpen_Fix = "You must edit the level in order to address this issue. Would you like to edit the level now?";
            public TrString Mainform_Validity_CannotOpen_btnEdit = "Edit level";
            public TrString Mainform_Validity_CannotSave = "The following problem has been detected with this level:";
            public TrString Mainform_Validity_CannotSave_Warning = "You cannot play this level until you address this issue. Are you sure you wish to leave the level in this invalid state?";
            public TrString Mainform_Validity_CannotSave_btnSave = "Save level anyway";
            public TrString Mainform_Validity_CannotSave_btnResume = "Resume editing";

            public TrString Mainform_InvalidFile = "The selected file is not a valid Sokoban level file.";
            public TrString Mainform_InvalidFile_Title = "Error opening level file";
            public TrString Mainform_NoHighscores = "The selected level does not have any highscores associated with it yet.";
            public TrString Mainform_NoHighscores_Title = "No highscores for this level";

            public TrString Mainform_MessageTitle_Exit = "Exit Expert Sokoban";
            public TrString Mainform_MessageTitle_OpenLevel = "Open level";
            public TrString Mainform_MessageTitle_RetryLevel = "Retry level";
            public TrString Mainform_MessageTitle_NewLevelFile = "New level file";
            public TrString Mainform_MessageTitle_OpenLevelFile = "Open level file";
            public TrString Mainform_MessageTitle_FinishEditing = "Finish editing";

            public TrString Mainform_Error_HelpFileNotFound = "The help file ({0}) could not be found.";

            public TrString Dialogs_btnOK = "OK";
            public TrString Dialogs_btnCancel = "Cancel";
            public TrString Dialogs_btnDiscard = "Discard changes";
            public TrString Dialogs_btnGiveUp = "Give up";
        }
        public ConfirmationMessagesTranslation ConfirmationMessages = new ConfirmationMessagesTranslation();

#if DEBUG
        [LingoDebug(RelativePath = @"..\..\main\ExpSok\Translation.cs")]
#endif
        [LingoGroup("Highscores dialog", @"This group contains strings used in the highscores window (accessible by right-clicking on a level and choosing ""Show highscores"").")]
        public class HighscoresFormTranslation
        {
            #region HighscoresFormTranslation
            public TrString HighscoresForm = "Highscores";
            public TrStringNumbers Highscores = new TrStringNumbers(new[] { "{0} move, {1} push", "{0} moves, {1} push", "{0} move, {1} pushes", "{0} moves, {1} pushes" }, new[] { true, true });
            public TrString btnOK = "OK";
            #endregion
        }
        public HighscoresFormTranslation Highscores = new HighscoresFormTranslation();

#if DEBUG
        [LingoDebug(RelativePath = @"..\..\main\ExpSok\Translation.cs")]
#endif
        [LingoGroup("About dialog", "This group contains strings used in the About window (accessible from the Help menu).")]
        public class AboutBoxTranslation
        {
            #region AboutBoxTranslation
            public TrString AboutBox = "About Expert Sokoban";
            public TrString lblCredits = "Credits:\n    Programming: Timwi, Roman\n    Graphics: Roman, Timwi\n    Testing: Hawthorn";
            public TrString btnOK = "OK";
            #endregion
        }
        public AboutBoxTranslation AboutBox = new AboutBoxTranslation();

#if DEBUG
        [LingoDebug(RelativePath = @"..\..\main\ExpSok\Translation.cs")]
#endif
        [LingoGroup("Menus, toolbars, statusbar", @"This group contains all the menu items and toolbar button labels, and some of the statusbar messages.")]
        public class MainformTranslation
        {
            #region MainformTranslation
            public TrString Mainform = "Expert Sokoban";
            public TrString mnuMain = "Main menu";
            public TrString mnuLevel = "&Level";
            public TrString mnuLevelNew = "&New level file";
            public TrString mnuLevelOpen = "&Open level file...";
            public TrString mnuLevelSave = "&Save level file";
            public TrString mnuLevelSaveAs = "Save level file &as...";
            public TrString mnuLevelUndo = "&Undo move";
            public TrString mnuLevelRedo = "Redo &move";
            public TrString mnuLevelRetry = "&Retry level";
            public TrString mnuLevelHighscores = "Show &highscores";
            public TrString mnuLevelPrevious = "&Previous level";
            public TrString mnuLevelNext = "N&ext level";
            public TrString mnuLevelPreviousUnsolved = "Pre&vious unsolved level";
            public TrString mnuLevelNextUnsolved = "Next unsolve&d level";
            public TrString mnuLevelChangePlayer = "&Change player name...";
            public TrString mnuLevelExit = "E&xit";
            public TrString mnuEdit = "&Edit";
            public TrString mnuEditCreateLevel = "Create &new level";
            public TrString mnuEditEditLevel = "&Edit level";
            public TrString mnuEditAddComment = "Add a co&mment...";
            public TrString mnuEditDelete = "&Delete level/comment";
            public TrString mnuEditCut = "C&ut";
            public TrString mnuEditCopy = "&Copy";
            public TrString mnuEditPaste = "&Paste";
            public TrString mnuEditFinish = "&Finish editing";
            public TrString mnuEditCancel = "C&ancel editing";
            public TrString mnuEditWall = "&Wall tool";
            public TrString mnuEditPiece = "P&iece tool";
            public TrString mnuEditTarget = "&Target tool";
            public TrString mnuEditSokoban = "&Sokoban tool";
            public TrString mnuOptions = "&Options";
            public TrString mnuOptionsLevelList = "Display &level list";
            public TrString mnuOptionsPlayingToolbar = "Display pla&ying toolbar";
            public TrString mnuOptionsFileToolbars = "Display &editing toolbars (level pack)";
            public TrString mnuOptionsEditLevelToolbar = "Display editin&g toolbar (level)";
            public TrString mnuOptionsStatusBar = "Display stat&us bar";
            public TrString mnuOptionsMoveNo = "Don't display &move path";
            public TrString mnuOptionsMoveLine = "Display move path as li&ne";
            public TrString mnuOptionsMoveDots = "Display move path as &dots";
            public TrString mnuOptionsMoveArrows = "Display move path as &arrows";
            public TrString mnuOptionsPushNo = "Don't display &push path";
            public TrString mnuOptionsPushLine = "Display push path as l&ine";
            public TrString mnuOptionsPushDots = "Display push path as do&ts";
            public TrString mnuOptionsPushArrows = "Display push path as a&rrows";
            public TrString mnuOptionsEndPos = "Display end p&osition of Sokoban and piece";
            public TrString mnuOptionsAreaSokoban = "Display reac&hable area for Sokoban";
            public TrString mnuOptionsAreaPiece = "Display reacha&ble area for piece";
            public TrString mnuOptionsSound = "Enable &sound";
            public TrString mnuOptionsChangeLanguage = "&Change language";
            public TrString mnuHelp = "&Help";
            public TrString mnuHelpKeyboard = "&Keyboard shortcuts";
            public TrString mnuHelpAbout = "&About";
            public TrString lblStatusSolved = "You have solved the level. Congratulations!";
            public TrString lblStatusNull = "No levels currently selected. Select a level from the level list to play.";
            public TrString toolEditLevel = "Level edit toolbar";
            public TrString btnEditLevelWall = "Wall tool";
            public TrString btnEditLevelPiece = "Piece tool";
            public TrString btnEditLevelTarget = "Target tool";
            public TrString btnEditLevelSokoban = "Sokoban tool";
            public TrString btnEditLevelOK = "Finish editing";
            public TrString btnEditLevelCancel = "Cancel editing";
            public TrString toolFileEdit = "File edit toolbar";
            public TrString btnFileEditNewLevel = "Create new level";
            public TrString btnFileEditEditLevel = "Edit level";
            public TrString btnFileEditAddComment = "Add a comment";
            public TrString btnFileEditDeleteLevel = "Delete selected level or comment";
            public TrString toolFile = "File toolbar";
            public TrString btnFileNew = "New level file";
            public TrString btnFileOpen = "Open level file";
            public TrString btnFileSave = "Save level file";
            public TrString btnFileCut = "Cut";
            public TrString btnFileCopy = "Copy";
            public TrString btnFilePaste = "Paste";
            public TrString toolPlay = "Playing toolbar";
            public TrString btnPlayOpenLevel = "Open level file";
            public TrString btnPlayPrevLevel = "Previous level";
            public TrString btnPlayNextLevel = "Next level";
            public TrString btnPlayPrevUnsolvedLevel = "Previous unsolved level";
            public TrString btnPlayNextUnsolvedLevel = "Next unsolved level";
            #endregion
        }
        public MainformTranslation Mainform = new MainformTranslation();
    }
}
