using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using RT.Util;
using RT.Util.Settings;

using Score=RT.Util.Tuple<int /* pushes */, int /* moves */>;

namespace ExpertSokoban
{
    public static class ExpSokSettings
    {
        /// <summary>
        /// Move path draw move
        /// </summary>
        public static PathDrawMode MoveDrawMode
        {
            get { return PrgSettings.Store.Get<PathDrawMode>("ExpSok.MoveDrawMode", PathDrawMode.Line); }
            set { PrgSettings.Store.Set("ExpSok.MoveDrawMode", value); }
        }

        /// <summary>
        /// Push path draw move
        /// </summary>
        public static PathDrawMode PushDrawMode
        {
            get { return PrgSettings.Store.Get<PathDrawMode>("ExpSok.PushDrawMode", PathDrawMode.Line); }
            set { PrgSettings.Store.Set("ExpSok.PushDrawMode", value); }
        }

        /// <summary>
        /// Whether the Sokoban and piece end-position should be displayed
        /// </summary>
        public static bool ShowEndPos
        {
            get { return PrgSettings.Store.Get("ExpSok.ShowEndPos", true); }
            set { PrgSettings.Store.Set("ExpSok.ShowEndPos", value); }
        }

        /// <summary>
        /// Whether the level list should be displayed
        /// </summary>
        public static bool DisplayLevelList
        {
            get { return PrgSettings.Store.Get("ExpSok.DisplayLevelList", false); }
            set { PrgSettings.Store.Set("ExpSok.DisplayLevelList", value); }
        }

        /// <summary>
        /// Whether the play toolbar should be displayed
        /// </summary>
        public static bool DisplayPlayToolStrip
        {
            get { return PrgSettings.Store.Get("ExpSok.DisplayPlayToolStrip", true); }
            set { PrgSettings.Store.Set("ExpSok.DisplayPlayToolStrip", value); }
        }

        /// <summary>
        /// Whether the edit toolbars (level pack) should be displayed
        /// </summary>
        public static bool DisplayEditToolStrip
        {
            get { return PrgSettings.Store.Get("ExpSok.DisplayEditToolStrip", true); }
            set { PrgSettings.Store.Set("ExpSok.DisplayEditToolStrip", value); }
        }

        /// <summary>
        /// Whether the edit toolbar (level) should be displayed
        /// </summary>
        public static bool DisplayEditLevelToolStrip
        {
            get { return PrgSettings.Store.Get("ExpSok.DisplayEditLevelToolStrip", true); }
            set { PrgSettings.Store.Set("ExpSok.DisplayEditLevelToolStrip", value); }
        }

        /// <summary>
        /// Whether the status bar should be displayed
        /// </summary>
        public static bool DisplayStatusBar
        {
            get { return PrgSettings.Store.Get("ExpSok.DisplayStatusBar", true); }
            set { PrgSettings.Store.Set("ExpSok.DisplayStatusBar", value); }
        }

        /// <summary>
        /// Last used editing tool
        /// </summary>
        public static MainAreaTool LastUsedTool
        {
            get { return PrgSettings.Store.Get<MainAreaTool>("ExpSok.LastUsedTool", MainAreaTool.Wall); }
            set { PrgSettings.Store.Set("ExpSok.LastUsedTool", value); }
        }

        /// <summary>
        /// Width of the panel containing the level list. The default value is chosen
        /// minimally such that the toolbar buttons still fit.
        /// </summary>
        public static int LevelListWidth
        {
            get { return PrgSettings.Store.Get("ExpSok.LevelListWidth", 152); }
            set { PrgSettings.Store.Set("ExpSok.LevelListWidth", value); }
        }

        /// <summary>
        /// The filename of the current/last played level file. Null if none.
        /// </summary>
        public static string LevelFilename
        {
            get { return PrgSettings.Store.Get("ExpSok.LevelFilename", (string)null); }
            set { PrgSettings.Store.Set("ExpSok.LevelFilename", value); }
        }

        /// <summary>
        /// Currently selected player name
        /// </summary>
        public static string PlayerName
        {
            get { return PrgSettings.Store.Get("ExpSok.PlayerName", (string)null); }
            set { PrgSettings.Store.Set("ExpSok.PlayerName", value); }
        }

        /// <summary>
        /// Player highscores. First key is the string representation of the level.
        /// Second key is the player's name.
        /// </summary>
        public static Dictionary<string /* String representation of a level */,
            Dictionary<string /* Player name */, Highscore>> Highscores
        {
            get
            {
                if (!PrgSettings.Store.Exists("ExpSok.Highscores"))
                    PrgSettings.Store.Set("ExpSok.Highscores", new Dictionary<string, Dictionary<string, Highscore>>());
                return PrgSettings.Store.Get<Dictionary<string, Dictionary<string, Highscore>>>("ExpSok.Highscores");
            }
            set { PrgSettings.Store.Set("ExpSok.Highscores", value); }
        }

        /// <summary>
        /// Helper method to determine whether the current player has solved the given level
        /// </summary>
        /// <param name="Level">String representation of the level to check.</param>
        /// <returns>True if the level has ever been solved by the current player.</returns>
        public static bool IsSolved(string Level)
        {
            if (PlayerName == null || PlayerName.Length == 0)
                return false;
            return Highscores.ContainsKey(Level) && Highscores[Level].ContainsKey(PlayerName);
        }

        /// <summary>
        /// Helper method to update highscore for the current player.
        /// </summary>
        /// <param name="Level">String representation of the level</param>
        public static void UpdateHighscore(string Level, int Moves, int Pushes)
        {
            if (PlayerName == null || PlayerName.Length == 0)
                throw new Exception("Player name is not set.");
            if (!Highscores.ContainsKey(Level))
                Highscores[Level] = new Dictionary<string, Highscore>();
            if (!Highscores[Level].ContainsKey(PlayerName))
                Highscores[Level][PlayerName] = new Highscore();
            Highscores[Level][PlayerName].UpdateWith(new Score(Moves, Pushes));
        }

        /// <summary>
        /// If true, this means the settings existed. This can be used to determine whether
        /// this particular version of settings exists.
        /// </summary>
        public static bool SettingsExist
        {
            get { return PrgSettings.Store.Get("ExpSok.SettingsVersion1", false); }
            set { PrgSettings.Store.Set("ExpSok.SettingsVersion1", value); }
        }
    }
}
