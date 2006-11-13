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
    public class MainFormSettingsVersion1
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

        public static MainFormSettingsVersion1 GetSettings()
        {
            object Settings = PrgSettings.Store.GetObject("ExpSok Mainform", null);
            if (Settings == null) return new MainFormSettingsVersion1();

            // Convert old versions of the settings to new versions
            if (Settings is MainFormSettings)
                Settings = MainFormSettingsVersion1.ConvertFrom (Settings as MainFormSettings);

            if (Settings is MainFormSettingsVersion1)
                return Settings as MainFormSettingsVersion1;

            return new MainFormSettingsVersion1();
        }

        private static MainFormSettingsVersion1 ConvertFrom(MainFormSettings OldVersion)
        {
            MainFormSettingsVersion1 Settings = new MainFormSettingsVersion1();
            Settings.MoveDrawMode = OldVersion.MoveDrawMode;
            Settings.PushDrawMode = OldVersion.PushDrawMode;
            Settings.ShowEndPos = OldVersion.ShowEndPos;
            Settings.DisplayLevelList = OldVersion.DisplayLevelList;
            Settings.DisplayToolStrip1 = OldVersion.DisplayToolStrip1;
            Settings.DisplayToolStrip2 = OldVersion.DisplayToolStrip2;
            Settings.DisplayEditToolStrip = OldVersion.DisplayEditToolStrip;
            Settings.LastUsedTool = OldVersion.LastUsedTool;
            Settings.LevelListPanelWidth = OldVersion.LevelListPanelWidth;

            // Convert the old Hashtable to a Dictionary
            Settings.SolvedLevels = new Dictionary<string, bool>();
            foreach (object i in OldVersion.SolvedLevels.Keys)
            {
                if (i is string)
                    Settings.SolvedLevels[i as string] = true;
                else if (i is SokobanLevel)
                    Settings.SolvedLevels[(i as SokobanLevel).ToString()] = true;
            }

            return Settings;
        }
    }

    /// <summary>
    /// This is an OLD VERSION of the class that encapsulates the settings for the
    /// application which are saved at shutdown and restored at startup.
    /// </summary>
    [Serializable]
    public class MainFormSettings
    {
        public PathDrawMode MoveDrawMode = PathDrawMode.Line;
        public PathDrawMode PushDrawMode = PathDrawMode.Line;
        public bool ShowEndPos = true;
        public bool DisplayLevelList = false;
        public bool DisplayToolStrip1 = true;
        public bool DisplayToolStrip2 = true;
        public bool DisplayEditToolStrip = true;
        public MainAreaTool LastUsedTool = MainAreaTool.Wall;
        public int LevelListPanelWidth = 152;
        public Hashtable SolvedLevels = new Hashtable();
    }
}
