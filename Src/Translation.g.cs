using RT.Util.Lingo;

namespace ExpertSokoban
{
    [LingoStringClass, LingoInGroup(TranslationGroup.About)]
    sealed partial class AboutBoxTranslation
    {
#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoNotes("Title bar.")]
        public TrString AboutBox = "About Expert Sokoban";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoNotes("This is displayed in a box in the About dialog. Please feel free to add a line to credit yourself for your translation work.")]
        public TrString lblCredits = "Credits:\r\n    Programming: Timwi, Roman\r\n    Graphics: Roman, Timwi\r\n    Testing: Hawthorn";

#if DEBUG
        [LingoAutoGenerated]
#endif
        public TrString btnOK = "OK";
    }

    [LingoStringClass, LingoInGroup(TranslationGroup.Highscores)]
    sealed partial class HighscoresFormTranslation
    {
#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoNotes("Title bar.")]
        public TrString HighscoresForm = "Highscores";

#if DEBUG
        [LingoAutoGenerated]
#endif
        public TrString btnOK = "OK";
    }

    [LingoStringClass]
    sealed partial class MainformTranslation
    {
#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.General), LingoNotes("Main window title bar.")]
        public TrString Mainform = "Expert Sokoban";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.Accessibility), LingoNotes("Describes the main menu bar.")]
        public TrString mnuMain = "Main menu";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.LevelMenu)]
        public TrString mnuLevel = "&Level";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.LevelMenu)]
        public TrString mnuLevelNew = "&New level file";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.LevelMenu)]
        public TrString mnuLevelOpen = "&Open level file...";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.LevelMenu)]
        public TrString mnuLevelSave = "&Save level file";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.LevelMenu)]
        public TrString mnuLevelSaveAs = "Save level file &as...";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.LevelMenu)]
        public TrString mnuLevelUndo = "&Undo move";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.LevelMenu)]
        public TrString mnuLevelRedo = "Redo &move";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.LevelMenu)]
        public TrString mnuLevelRetry = "&Retry level";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.LevelMenu)]
        public TrString mnuLevelHighscores = "Show &highscores";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.LevelMenu)]
        public TrString mnuLevelPrevious = "&Previous level";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.LevelMenu)]
        public TrString mnuLevelNext = "N&ext level";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.LevelMenu)]
        public TrString mnuLevelPreviousUnsolved = "Pre&vious unsolved level";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.LevelMenu)]
        public TrString mnuLevelNextUnsolved = "Next unsolve&d level";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.LevelMenu)]
        public TrString mnuLevelChangePlayer = "&Change player name...";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.LevelMenu)]
        public TrString mnuLevelExit = "E&xit";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.EditMenu)]
        public TrString mnuEdit = "&Edit";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.EditMenu)]
        public TrString mnuEditCreateLevel = "Create &new level";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.EditMenu)]
        public TrString mnuEditEditLevel = "&Edit level";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.EditMenu)]
        public TrString mnuEditAddComment = "Add a co&mment...";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.EditMenu)]
        public TrString mnuEditDelete = "&Delete level/comment";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.EditMenu)]
        public TrString mnuEditCut = "C&ut";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.EditMenu)]
        public TrString mnuEditCopy = "&Copy";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.EditMenu)]
        public TrString mnuEditPaste = "&Paste";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.EditMenu)]
        public TrString mnuEditFinish = "&Finish editing";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.EditMenu)]
        public TrString mnuEditCancel = "C&ancel editing";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.EditMenu)]
        public TrString mnuEditWall = "&Wall tool";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.EditMenu)]
        public TrString mnuEditPiece = "P&iece tool";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.EditMenu)]
        public TrString mnuEditTarget = "&Target tool";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.EditMenu)]
        public TrString mnuEditSokoban = "&Sokoban tool";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptions = "&Options";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsLevelList = "Display &level list";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsPlayingToolbar = "Display pla&ying toolbar";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsFileToolbars = "Display &editing toolbars (level file)";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsEditLevelToolbar = "Display editin&g toolbar (level)";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsStatusBar = "Display stat&us bar";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsMoveNo = "Don\'t display &move path";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsMoveLine = "Display move path as li&ne";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsMoveDots = "Display move path as &dots";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsMoveArrows = "Display mo&ve path as arrows";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsPushNo = "Don\'t display &push path";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsPushLine = "Display push path as l&ine";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsPushDots = "Display push path as do&ts";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsPushArrows = "Display push path as arro&ws";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsEndPos = "Display end p&osition of Sokoban and piece";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsAreaSokoban = "Display reac&hable area for Sokoban";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsAreaPiece = "Display reachable area &for piece";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsLetterControl = "Enable lette&r control";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsLetterControlNext = "Show ne&xt letter control set";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsSound = "Enable &sound";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsAnimation = "Enable &animations";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.OptionsMenu)]
        public TrString mnuOptionsChangeLanguage = "&Change language";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.HelpMenu)]
        public TrString mnuHelp = "&Help";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.HelpMenu)]
        public TrString mnuHelpHelp = "&Help...";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.HelpMenu)]
        public TrString mnuHelpAbout = "&About";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.StatusBar)]
        public TrString lblStatusSolved = "You have solved the level. Congratulations!";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.StatusBar)]
        public TrString lblStatusNull = "No levels currently selected. Select a level from the level list to play.";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.Accessibility), LingoNotes("Describes the toolbar which contains commands for editing a level.")]
        public TrString toolEditLevel = "Level edit toolbar";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolEditLevel)]
        public TrString btnEditLevelWall = "Wall tool";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolEditLevel)]
        public TrString btnEditLevelPiece = "Piece tool";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolEditLevel)]
        public TrString btnEditLevelTarget = "Target tool";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolEditLevel)]
        public TrString btnEditLevelSokoban = "Sokoban tool";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolEditLevel)]
        public TrString btnEditLevelOK = "Finish editing";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolEditLevel)]
        public TrString btnEditLevelCancel = "Cancel editing";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.Accessibility), LingoNotes("Describes the toolbar which contains commands for editing a level file.")]
        public TrString toolFileEdit = "File edit toolbar";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolFileEdit)]
        public TrString btnFileEditNewLevel = "Create new level";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolFileEdit)]
        public TrString btnFileEditEditLevel = "Edit level";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolFileEdit)]
        public TrString btnFileEditAddComment = "Add a comment";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolFileEdit)]
        public TrString btnFileEditDeleteLevel = "Delete selected level or comment";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.Accessibility), LingoNotes("Describes the toolbar which contains commands for handling level files.")]
        public TrString toolFile = "File toolbar";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolFile)]
        public TrString btnFileNew = "New level file";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolFile)]
        public TrString btnFileOpen = "Open level file";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolFile)]
        public TrString btnFileSave = "Save level file";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolFile)]
        public TrString btnFileCut = "Cut";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolFile)]
        public TrString btnFileCopy = "Copy";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolFile)]
        public TrString btnFilePaste = "Paste";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.Accessibility), LingoNotes("Describes the toolbar which contains commands for playing the game.")]
        public TrString toolPlay = "Playing toolbar";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolPlay)]
        public TrString btnPlayOpenLevel = "Open level file";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolPlay)]
        public TrString btnPlayPrevLevel = "Previous level";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolPlay)]
        public TrString btnPlayNextLevel = "Next level";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolPlay)]
        public TrString btnPlayPrevUnsolvedLevel = "Previous unsolved level";

#if DEBUG
        [LingoAutoGenerated]
#endif
        [LingoInGroup(TranslationGroup.ToolPlay)]
        public TrString btnPlayNextUnsolvedLevel = "Next unsolved level";
    }

    [LingoStringClass, LingoInGroup(TranslationGroup.ContextMenu)]
    sealed partial class ContextMenuTranslation
    {
#if DEBUG
        [LingoAutoGenerated]
#endif
        public TrString mnuContextPlay = "Pl&ay this level";

#if DEBUG
        [LingoAutoGenerated]
#endif
        public TrString mnuContextEdit = "&Edit this level";

#if DEBUG
        [LingoAutoGenerated]
#endif
        public TrString mnuContextHighscores = "Show &highscores";

#if DEBUG
        [LingoAutoGenerated]
#endif
        public TrString mnuContextNewLevel = "C&reate a new level here";

#if DEBUG
        [LingoAutoGenerated]
#endif
        public TrString mnuContextNewComment = "&Insert a comment here";

#if DEBUG
        [LingoAutoGenerated]
#endif
        public TrString mnuContextCut = "C&ut";

#if DEBUG
        [LingoAutoGenerated]
#endif
        public TrString mnuContextCopy = "&Copy";

#if DEBUG
        [LingoAutoGenerated]
#endif
        public TrString mnuContextPaste = "&Paste";

#if DEBUG
        [LingoAutoGenerated]
#endif
        public TrString mnuContextDelete = "&Delete";

#if DEBUG
        [LingoAutoGenerated]
#endif
        public TrString mnuContextHide = "Hide &level list";
    }
}