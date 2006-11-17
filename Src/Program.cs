using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RT.Util.Settings;

namespace ExpertSokoban
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PrgSettings.LoadSettings(new SettingsBinaryFile());
            Application.Run(new Mainform());
            PrgSettings.SaveSettings();
        }

        /// <summary>
        /// Encapsulates all the settings that are saved at application shutdown and
        /// restored at application startup.
        /// </summary>
        public static ExpSokSettingsVer1 Settings;
    }
}