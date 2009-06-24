using System;
using System.Linq;
using System.Windows.Forms;
using RT.Util;
using RT.Util.Lingo;
using RT.Util.Xml;

namespace ExpertSokoban
{
    public static class Program
    {
        public static Translation Tr;
        public static ExpSokSettings Settings;
        public static bool TranslationEnabled = true;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
#if DEBUG
            TranslationEnabled = true;
            bool dummy = new string[] { }.Any();  // hack to prevent removal of "using"
#else
            TranslationEnabled = args.Any(s => s == "translate");
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try { Settings = XmlClassify.LoadObjectFromXmlFile<ExpSokSettings>(PathUtil.AppPathCombine(@"ExpSok.settings.xml")); }
            catch { Settings = new ExpSokSettings(); }

            Tr = Lingo.LoadTranslation<Translation>("ExpSok", ref Settings.Language);

            Application.Run(new Mainform());

            // Store settings
            XmlClassify.SaveObjectToXmlFile(Settings, PathUtil.AppPathCombine(@"ExpSok.settings.xml"));
        }
    }
}