using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using RT.Util;
using RT.Util.ExtensionMethods;
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
            var unused = Lingo.FindUnusedStrings(typeof(Translation), new Assembly[] { Assembly.GetExecutingAssembly() }).Select(f => f.DeclaringType.FullName + "." + f.Name).JoinString("\n");
            if (unused.Length > 0)
                MessageBox.Show("Unused string codes found:\n\n" + unused);

            Form form = new AboutBox(false);
            Lingo.TranslateControl(form, new Translation().AboutBox, @"..\..\main\ExpSok\TranslationAbout.g.cs");
            form.Close();

            form = new HighscoresForm(false);
            Lingo.TranslateControl(form, new Translation().Highscores, @"..\..\main\ExpSok\TranslationHighscores.g.cs");
            form.Close();

#else
            TranslationEnabled = args.Any(s => s == "translate");
#endif

            Tr = Lingo.LoadTranslationOrDefault<Translation>("ExpSok", ref Settings.Language);

            Application.Run(new Mainform());

            Settings.Save();
        }
    }
}