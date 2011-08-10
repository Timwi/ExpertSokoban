using System;
using System.Threading;
using System.Windows.Forms;
using RT.Util;
using RT.Util.Lingo;

namespace ExpertSokoban
{
    static class Program
    {
        public static Translation Tr;
        public static Settings Settings;
        public static bool TranslationEnabled = true;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Mutex m = null;
            try { m = new Mutex(true, "Global\\ExpSokMutex7FDC0158CF9E"); }
            catch { }

            SettingsUtil.LoadSettings(out Settings);

            Tr = Lingo.LoadTranslationOrDefault<Translation>("ExpSok", ref Settings.Language);

            Application.Run(new Mainform());

            GC.KeepAlive(m);

            Settings.Save();
        }
    }
}