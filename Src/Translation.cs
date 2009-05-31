using RT.Util;

namespace ExpertSokoban
{
#if DEBUG
    [LingoDebug(RelativePath = @"..\..\main\ExpSok\Translation.cs")]
#endif
    public class HighscoresFormTranslation
    {
        #region HighscoresFormTranslation
        public string HighscoresForm = "Highscores";
        public string Highscores = "{0} pushes, {1} moves";
        public string btnOK = "OK";
        #endregion
    }

#if DEBUG
    [LingoDebug(RelativePath = @"..\..\main\ExpSok\Translation.cs")]
#endif
    public class AboutBoxTranslation
    {
        #region AboutBoxTranslation
        public string AboutBox = "About Expert Sokoban";
        public string lblCredits = "Credits:\n    Programming: Timwi, Roman\n    Graphics: Roman, Timwi\n    Testing: Hawthorn";
        public string btnOK = "OK";
        #endregion
    }

#if DEBUG
    [LingoDebug(RelativePath = @"..\..\main\ExpSok\Translation.cs")]
#endif
    public class MainformTranslation
    {
        #region MainformTranslation
        public string Mainform = "Expert Sokoban";
        public string mnuMain = "Main menu";
        public string mnuLevel = "&Level";
        public string mnuLevelNew = "&New level file";
        public string mnuLevelOpen = "&Open level file...";
        public string mnuLevelSave = "&Save level file";
        public string mnuLevelSaveAs = "Save level file &as...";
        public string mnuLevelUndo = "&Undo move";
        public string mnuLevelRedo = "Redo &move";
        public string mnuLevelRetry = "&Retry level";
        public string mnuLevelHighscores = "Show &highscores";
        public string mnuLevelPrevious = "&Previous level";
        public string mnuLevelNext = "N&ext level";
        public string mnuLevelPreviousUnsolved = "Pre&vious unsolved level";
        public string mnuLevelNextUnsolved = "Next unsolve&d level";
        public string mnuLevelChangePlayer = "&Change player name...";
        public string mnuLevelExit = "E&xit";
        public string mnuEdit = "&Edit";
        public string mnuEditCreateLevel = "Create &new level";
        public string mnuEditEditLevel = "&Edit level";
        public string mnuEditAddComment = "Add a co&mment...";
        public string mnuEditDelete = "&Delete level/comment";
        public string mnuEditCut = "C&ut";
        public string mnuEditCopy = "&Copy";
        public string mnuEditPaste = "&Paste";
        public string mnuEditFinish = "&Finish editing";
        public string mnuEditCancel = "C&ancel editing";
        public string mnuEditWall = "&Wall tool";
        public string mnuEditPiece = "P&iece tool";
        public string mnuEditTarget = "&Target tool";
        public string mnuEditSokoban = "&Sokoban tool";
        public string mnuOptions = "&Options";
        public string mnuOptionsLevelList = "Display &level list";
        public string mnuOptionsPlayingToolbar = "Display pla&ying toolbar";
        public string mnuOptionsFileToolbars = "Display &editing toolbars (level pack)";
        public string mnuOptionsEditLevelToolbar = "Display editin&g toolbar (level)";
        public string mnuOptionsStatusBar = "Display stat&us bar";
        public string mnuOptionsMoveNo = "Don't display &move path";
        public string mnuOptionsMoveLine = "Display move path as li&ne";
        public string mnuOptionsMoveDots = "Display move path as &dots";
        public string mnuOptionsMoveArrows = "Display move path as &arrows";
        public string mnuOptionsPushNo = "Don't display &push path";
        public string mnuOptionsPushLine = "Display push path as l&ine";
        public string mnuOptionsPushDots = "Display push path as do&ts";
        public string mnuOptionsPushArrows = "Display push path as a&rrows";
        public string mnuOptionsEndPos = "Display end p&osition of Sokoban and piece";
        public string mnuOptionsAreaSokoban = "Display reac&hable area for Sokoban";
        public string mnuOptionsAreaPiece = "Display reacha&ble area for piece";
        public string mnuOptionsSound = "Enable &sound";
        public string mnuOptionsChangeLanguage = "&Change language";
        public string mnuHelp = "&Help";
        public string mnuHelpKeyboard = "&Keyboard shortcuts";
        public string mnuHelpAbout = "&About";
        public string lblStatusSolved = "You have solved the level. Congratulations!";
        public string lblStatusNull = "No levels currently selected. Select a level from the level list to play.";
        public string toolEditLevel = "Level edit toolbar";
        public string btnEditLevelWall = "Wall tool";
        public string btnEditLevelPiece = "Piece tool";
        public string btnEditLevelTarget = "Target tool";
        public string btnEditLevelSokoban = "Sokoban tool";
        public string btnEditLevelOK = "Finish editing";
        public string btnEditLevelCancel = "Cancel editing";
        public string toolFileEdit = "File edit toolbar";
        public string btnFileEditNewLevel = "Create new level";
        public string btnFileEditEditLevel = "Edit level";
        public string btnFileEditAddComment = "Add a comment";
        public string btnFileEditDeleteLevel = "Delete selected level or comment";
        public string toolFile = "File toolbar";
        public string btnFileNew = "New level file";
        public string btnFileOpen = "Open level file";
        public string btnFileSave = "Save level file";
        public string btnFileCut = "Cut";
        public string btnFileCopy = "Copy";
        public string btnFilePaste = "Paste";
        public string toolPlay = "Playing toolbar";
        public string btnPlayOpenLevel = "Open level file";
        public string btnPlayPrevLevel = "Previous level";
        public string btnPlayNextLevel = "Next level";
        public string btnPlayPrevUnsolvedLevel = "Previous unsolved level";
        public string btnPlayNextUnsolvedLevel = "Next unsolved level";
        #endregion
    }

#if DEBUG
    [LingoDebug(RelativePath = @"..\..\main\ExpSok\Translation.cs")]
#endif
    public class Translation
    {
        public string ThisLanguage = "English (GB)";
        public string ProgramName = "Expert Sokoban";
        public HighscoresFormTranslation Highscores = new HighscoresFormTranslation();
        public AboutBoxTranslation AboutBox = new AboutBoxTranslation();
        public MainformTranslation Mainform = new MainformTranslation();

        [LingoNotes(@"Specifies the resource name of the large image that appears on the screen when the user solves a level.")]
        public string LevelSolvedResourceName = "Skin_LevelSolved";

        public string LevelList_NewComment_Prompt = "Please enter the revised comment:";
        public string LevelList_LevelSolved = "Solved";
        public string LevelList_CurrentlyEditing = "Currently editing";
        public string LevelList_JustSolved = "Just solved";
        public string LevelList_CurrentlyPlaying = "Currently playing";
        public string LevelList_Message_CannotSaveSettings = "The settings could not be saved.";
        public string LevelList_Message_CannotSaveSettings_Title = "Error saving settings";
        public string LevelList_Message_AllSolved = "You have solved all levels in this level pack!";
        public string LevelList_Message_AllSolved_Title = "Congratulations!";
        public string LevelList_Message_NoMoreUnsolved = "There are no more unsolved levels in this level file.";
        public string LevelList_Message_Next_Title = "Next level";
        public string LevelList_Message_NextUnsolved_Title = "Next unsolved level";
        public string LevelList_Message_Prev_Title = "Previous level";
        public string LevelList_Message_PrevUnsolved_Title = "Previous unsolved level";
        public string LevelList_Message_NoOtherLevel = "There is no other level in the level file.";
        public string LevelList_Message_SaveChanges = "You have made changes to {0}. Would you like to save those changes?";
        public string LevelList_Message_SaveChanges_btnSave = "Save changes";
        public string LevelList_Message_SaveChanges_btnDiscard = "&Discard changes";
        public string LevelList_Message_DeleteLevel_CurrentlyEditing = "You are currently editing this level.\n\nIf you delete this level now, all your modifications will be discarded.\n\nAre you sure you wish to do this?";
        public string LevelList_Message_DeleteLevel_Title = "Delete level";
        public string LevelList_Message_DeleteLevel_CurrentlyPlaying = "You are currently playing this level.\n\nAre you sure you wish to give up?";
        public string LevelList_Message_DeleteLevel_Sure = "Are you sure you wish to delete this level?";
        public string LevelList_Message_DeleteLevel_btnDelete = "Delete level";

        public string MainArea_Message_DiscardChanges = "Are you sure you wish to discard your changes to the level you're editing?";
        public string MainArea_Message_GiveUp = "Are you sure you wish to give up the current level?";

        public string Mainform_ChooseName = "Please choose a name which will be used to identify you in highscore tables.";
        public string Mainform_ChooseName_FirstRun = "Please choose a name which will be used to identify you in highscore tables.\nYou can change this name later by selecting \"Change player name\" from the \"Level\" menu.";
        public string Mainform_ChooseName_SolvedLevel = "Congratulations! You've solved the current level.\nPlease choose a name which will be used to identify you in highscore tables.\nIf you do not choose a name now, your score for this level will not be recorded.\nYou can change this name again later by selecting \"Change player name\" from the \"Level\" menu.";

        public string Mainform_Status_Moves = "Moves: {0}";
        public string Mainform_Status_Pushes = "Pushes: {0}";
        public string Mainform_Status_PiecesRemaining = "Remaining pieces: {0}";
        public string Mainform_Validity_Valid = "The level is valid.";
        public string Mainform_Validity_NotEnclosed = "The level is invalid because it is not completely enclosed by a wall.";
        public string Mainform_Validity_WrongNumber = "The level is invalid because the number of pieces does not match the number of targets.";
        public string Mainform_Validity_CannotOpen = "The level could not be opened because it is invalid.";
        public string Mainform_Validity_CannotOpen_Fix = "You must edit the level in order to address this issue. Would you like to edit the level now?";
        public string Mainform_Validity_CannotOpen_btnEdit = "Edit level";
        public string Mainform_Validity_CannotSave = "The following problem has been detected with this level:";
        public string Mainform_Validity_CannotSave_Warning = "You cannot play this level until you address this issue. Are you sure you wish to leave the level in this invalid state?";
        public string Mainform_Validity_CannotSave_btnSave = "Save level anyway";
        public string Mainform_Validity_CannotSave_btnResume = "Resume editing";
        public string Mainform_InvalidFile = "The selected file is not a valid Sokoban level file.";
        public string Mainform_InvalidFile_Title = "Error opening level file";
        public string Mainform_NoHighscores = "The selected level does not have any highscores associated with it yet.";
        public string Mainform_NoHighscores_Title = "No highscores for this level";

        public string Mainform_MessageTitle_Exit = "Exit Expert Sokoban";
        public string Mainform_MessageTitle_OpenLevel = "Open level";
        public string Mainform_MessageTitle_RetryLevel = "Retry level";
        public string Mainform_MessageTitle_NewLevelFile = "New level file";
        public string Mainform_MessageTitle_OpenLevelFile = "Open level file";
        public string Mainform_MessageTitle_FinishEditing = "Finish editing";

        public string Mainform_LevelList_NewComment = "Please enter a comment:";
        public string Mainform_Error_HelpFileNotFound = "The help file ({0}) could not be found.";

        public string Save_FileType_TextFiles = "Text files";
        public string Save_FileType_AllFiles = "All files";
        public string FileName_Untitled = "(untitled)";
        public string Dialogs_btnDiscard = "Discard changes";
        public string Dialogs_btnGiveUp = "Give up";
        public string Dialogs_btnCancel = "Cancel";
        public string PlayerNameMissing = "(no player name)";
    }
}
