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

            // Load settings and convert them if necessary
            PrgSettings.LoadSettings(new SettingsBinaryFile());
            //if (!ExpSokSettings.SettingsExist)
            //{
            //    if (ExpSokSettingsOldV1.SettingsExist)
            //        ExpSokSettings.FromOld(ExpSokSettingsOldV1);
            //}

            Application.Run(new Mainform());

            // Store settings
            PrgSettings.SaveSettings();
        }
    }
}