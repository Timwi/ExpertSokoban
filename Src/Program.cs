using System;
using System.Linq;
using System.Windows.Forms;
using RT.Util;
using RT.Util.Lingo;
using RT.Util.Xml;
using RT.Util.Forms;

namespace ExpertSokoban
{
    static class Program
    {
        public static Translation Tr;
        public static Settings Settings;
        public static bool TranslationEnabled;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
#if DEBUG
            TranslationEnabled = true;
            bool dummy = new string[0].Any();  // hack to prevent removal of "using"
#else
            TranslationEnabled = args.Any(s => s == "translate");
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SettingsUtil.LoadSettings(out Settings);
            Tr = Lingo.LoadTranslationOrDefault<Translation>("ExpSok", ref Settings.Language);

            Application.Run(new Mainform());

            Settings.Save();
        }
    }
}