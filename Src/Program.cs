using System;
using System.IO;
using System.Windows.Forms;
using RT.Util.Lingo;
using RT.Util.Xml;

namespace ExpertSokoban
{
    public static class Program
    {
        public static Translation Tr = new Translation();
        public static ExpSokSettings Settings;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if DEBUG
            XmlClassify.SaveObjectToXmlFile(Tr, Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"ExpSok-translation.template.xml"));
#endif

            try { Settings = XmlClassify.LoadObjectFromXmlFile<ExpSokSettings>(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"ExpSok.settings.xml")); }
            catch { Settings = new ExpSokSettings(); }
            Lingo.TryLoadTranslation("ExpSok", Settings.Language, ref Tr);

            Application.Run(new Mainform());

            // Store settings
            XmlClassify.SaveObjectToXmlFile(Settings, Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"ExpSok.settings.xml"));
        }
    }
}