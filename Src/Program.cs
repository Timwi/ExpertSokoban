using System;
using System.Windows.Forms;
using RT.Util;
using RT.Util.Lingo;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SettingsUtil.LoadSettings(out Settings);

#if DEBUG
            TranslationEnabled = true;
#else
            TranslationEnabled = args.Any(s => s == "translate");
#endif

            Tr = Lingo.LoadTranslationOrDefault<Translation>("ExpSok", ref Settings.Language);

            Application.Run(new Mainform());

            Settings.Save();
        }
    }
}