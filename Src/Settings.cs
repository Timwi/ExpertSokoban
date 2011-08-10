using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using RT.Util;
using RT.Util.Forms;
using RT.Util.Lingo;

namespace ExpertSokoban
{
    /// <summary>Encapsulates all the user settings that are persisted between runs of the application.</summary>
    [Settings("ExpSok", SettingsKind.UserSpecific)]
    class Settings : SettingsThreadedBase
    {
        /// <summary>The settings for the main form (size, position, etc.).</summary>
        public ManagedForm.Settings MainFormSettings = new ManagedForm.Settings();
        /// <summary>The settings for the highscores form (size, position, etc.).</summary>
        public ManagedForm.Settings HighscoresFormSettings = new ManagedForm.Settings();
        /// <summary>The settings for the ChoosePlayerNameForm (size, position, etc.).</summary>
        public ManagedForm.Settings ChoosePlayerNameFormSettings = new ManagedForm.Settings();
        /// <summary>The settings for the translation form (size, position, etc.).</summary>
        public TranslationForm<Translation>.Settings TranslationFormSettings = new TranslationForm<Translation>.Settings();

        /// <summary>Move path draw move</summary>
        public PathDrawMode MoveDrawMode = PathDrawMode.None;

        /// <summary>Push path draw move</summary>
        public PathDrawMode PushDrawMode = PathDrawMode.Line;

        /// <summary>Whether the Sokoban and piece end-position should be displayed</summary>
        public bool ShowEndPos = true;

        /// <summary>Whether the area the Sokoban can move to (the "move area") should be displayed or not.</summary>
        public bool ShowAreaSokoban = true;

        /// <summary>Whether the area the selected piece can be pushed to (the "push area") should be displayed or not.</summary>
        public bool ShowAreaPiece = true;

        /// <summary>Whether sound is enabled.</summary>
        public bool SoundEnabled = true;

        /// <summary>Whether the Sokoban is animated when a move is made.</summary>
        public bool AnimationEnabled = true;

        /// <summary>Whether letter-based control is enabled.</summary>
        public bool LetteringEnabled = false;

        /// <summary>Whether the level list should be displayed</summary>
        public bool DisplayLevelList = true;

        /// <summary>Whether the playing toolbar should be displayed</summary>
        public bool DisplayPlayingToolbar = true;

        /// <summary>Whether the level file toolbars should be displayed</summary>
        public bool DisplayFileToolbars = true;

        /// <summary>Whether the edit toolbar (level) should be displayed</summary>
        public bool DisplayEditLevelToolbar = true;

        /// <summary>Whether the status bar should be displayed</summary>
        public bool DisplayStatusBar = true;

        /// <summary>Last used editing tool</summary>
        public MainAreaTool LastUsedTool = MainAreaTool.Wall;

        /// <summary>Width of the panel containing the level list. The default value is chosen minimally such that the toolbar buttons still fit.</summary>
        public int LevelListWidth = 152;

        /// <summary>The filename of the current/last played level file. Null if none.</summary>
        public string LevelFilename = "OriginalLevels.txt";

        /// <summary>Currently selected player name.</summary>
        public string PlayerName = null;

        /// <summary>Player highscores. First key is the string representation of the level. Second key is the player's name.</summary>
        public Dictionary<string, Dictionary<string, Highscore>> Highscores = new Dictionary<string, Dictionary<string, Highscore>>();

        /// <summary>Remembers the path where a file was last opened from or saved to.</summary>
        public string LastOpenSaveDirectory = Path.GetDirectoryName(Application.ExecutablePath);

        /// <summary>Stores the code of the language the user selected.</summary>
        public Language Language = Translation.DefaultLanguage;

        /// <summary>Determines whether the current player has solved the given level.</summary>
        /// <param name="level">String representation of the level to check.</param>
        /// <returns>True if the level has ever been solved by the current player.</returns>
        public bool IsSolved(string level)
        {
            if (string.IsNullOrEmpty(PlayerName))
                return false;
            return Highscores.ContainsKey(level) && Highscores[level].ContainsKey(PlayerName);
        }

        /// <summary>Updates the highscore for the current player.</summary>
        /// <param name="level">String representation of the level.</param>
        /// <param name="moves">Number of moves required to solve the level.</param>
        /// <param name="pushes">Number of pushes required to solve the level.</param>
        public void UpdateHighscore(string level, int moves, int pushes)
        {
            if (string.IsNullOrEmpty(PlayerName))
                throw new Exception("Player name is not set.");
            if (!Highscores.ContainsKey(level))
                Highscores[level] = new Dictionary<string, Highscore>();
            if (!Highscores[level].ContainsKey(PlayerName))
                Highscores[level][PlayerName] = new Highscore();
            Highscores[level][PlayerName].UpdateWith(new Score(moves, pushes));
        }

        /// <summary>Constructor.</summary>
        public Settings()
        {
            // Make a default window size of 4/5th the width/height of the working area
            var w = Screen.PrimaryScreen.WorkingArea.Width * 4 / 5;
            var h = Screen.PrimaryScreen.WorkingArea.Height * 4 / 5;
            MainFormSettings.DimensionsByRes[SystemInformation.VirtualScreen.Width + "x" + SystemInformation.VirtualScreen.Height] = new ManagedForm.FormDimensions
            {
                Left = Screen.PrimaryScreen.WorkingArea.Width / 2 - w / 2 + Screen.PrimaryScreen.WorkingArea.Left,
                Top = Screen.PrimaryScreen.WorkingArea.Height / 2 - h / 2 + Screen.PrimaryScreen.WorkingArea.Top,
                Width = w,
                Height = h,
                Maximized = false
            };
        }

        protected override SettingsThreadedBase CloneForSaveThreaded()
        {
            // The cloning ensures that the settings are saved exactly as they were at the time when SaveThreaded was called.
            // This doesn't matter for ExpSok as all settings changes are always saved anyway, so no need to clone.
            return this;
        }
    }
}
