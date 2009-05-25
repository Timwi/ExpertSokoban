using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RT.Util.Settings;
using RT.Util.Xml;
using System.IO;
using RT.Util;

namespace ExpertSokoban
{
    public static class Program
    {
        public static Translation Translation = new Translation();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
#if DEBUG
            XmlClassify.SaveObjectToXmlFile(Translation, Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"ExpSok-translation.template.xml"));
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Load settings and convert them if necessary
            PrgSettings.LoadSettings(new SettingsBinaryFile());
            //if (!ExpSokSettings.SettingsExist)
            //{
            //    if (ExpSokSettingsOldV1.SettingsExist)
            //        ExpSokSettings.FromOld(ExpSokSettingsOldV1);
            //}

            Lingo.TryLoadTranslation("ExpSok", ExpSokSettings.Language, ref Translation);

            Application.Run(new Mainform());

            // Store settings
            PrgSettings.SaveSettings();
        }
    }
}