using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RT.Util.Settings;
using RT.Util.Xml;
using System.IO;

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
            var tr_de = XmlClassify.LoadObjectFromXmlFile<Translation>(@"C:\c\main\ExpSok\Translations\ExpSok.de.xml");
            XmlClassify.SaveObjectToXmlFile(tr_de, @"C:\c\main\ExpSok\Translations\ExpSok.de.xml");
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

            Application.Run(new Mainform());

            // Store settings
            PrgSettings.SaveSettings();
        }
    }
}