using System;
using System.Collections.Generic;
using System.Text;
using RT.Util.Settings;
using System.Collections;

namespace ExpertSokoban
{
    /// <summary>
    /// This class encapsulates all the settings for the application which are saved at
    /// application shutdown and restored at application startup.
    /// </summary>
    [Serializable]
    public class ExpSokSettingsVer1
    {
        /// <summary>
        /// Move path draw move
        /// </summary>
        public PathDrawMode MoveDrawMode = PathDrawMode.Line;

        /// <summary>
        /// Push path draw move
        /// </summary>
        public PathDrawMode PushDrawMode = PathDrawMode.Line;

        /// <summary>
        /// Whether the Sokoban and piece end-position should be displayed
        /// </summary>
        public bool ShowEndPos = true;

        /// <summary>
        /// Whether the level list should be displayed
        /// </summary>
        public bool DisplayLevelList = false;

        /// <summary>
        /// Whether the file toolbar should be displayed
        /// </summary>
        public bool DisplayToolStrip1 = true;

        /// <summary>
        /// Whether the operations toolbar should be displayed
        /// </summary>
        public bool DisplayToolStrip2 = true;

        /// <summary>
        /// Whether the edit toolbar should be displayed
        /// </summary>
        public bool DisplayEditToolStrip = true;

        /// <summary>
        /// Whether the status bar should be displayed
        /// </summary>
        public bool DisplayStatusBar = true;

        /// <summary>
        /// Last used editing tool
        /// </summary>
        public MainAreaTool LastUsedTool = MainAreaTool.Wall;

        /// <summary>
        /// Width of the panel containing the level list. The default value is chosen
        /// minimally such that the toolbar buttons still fit.
        /// </summary>
        public int LevelListPanelWidth = 152;

        /// <summary>
        /// Collection of levels the user has already solved
        /// </summary>
        public Dictionary<string, bool> SolvedLevels = new Dictionary<string, bool>();

        public static ExpSokSettingsVer1 GetSettings()
        {
            object Settings = PrgSettings.Store.GetObject("ExpSok Mainform", null);
            if (Settings == null) return new ExpSokSettingsVer1();

            // Convert old versions of the settings to new versions
            // Un-comment this (and change the below to Ver2) when ExpSokSettingsVer2 comes along
            // if (Settings is ExpSokSettingsVer1)
            //    Settings = ExpSokSettingsVer2.ConvertFrom (Settings as ExpSokSettingsVer1);

            if (Settings is ExpSokSettingsVer1)
                return Settings as ExpSokSettingsVer1;

            return new ExpSokSettingsVer1();
        }
    }
}
