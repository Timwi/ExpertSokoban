using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RT.Util.Lingo;
using RT.Util.Xml;

namespace ExpertSokoban
{
    public static class Program
    {
        public static Translation Tr = new Translation();
        public static ExpSokSettings Settings;
        public static bool TranslationEnabled = true;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            TranslationEnabled = args.Any(s => s == "translate");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try { Settings = XmlClassify.LoadObjectFromXmlFile<ExpSokSettings>(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"ExpSok.settings.xml")); }
            catch { Settings = new ExpSokSettings(); }
            if (!Lingo.TryLoadTranslation("ExpSok", Settings.Language, ref Tr))
                Settings.Language = null;

            Application.Run(new Mainform());

            // Store settings
            XmlClassify.SaveObjectToXmlFile(Settings, Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"ExpSok.settings.xml"));
        }
    }
}