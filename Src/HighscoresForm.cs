using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using RT.Util;
using RT.Util.ExtensionMethods;
using RT.Util.Forms;

namespace ExpertSokoban
{
    public partial class HighscoresForm : ManagedForm
    {
        private SokobanLevel CurLevel;

        public HighscoresForm()
        {
            InitializeComponent();
            Lingo.TranslateControl(this, Program.Translation.Dialogs);
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Sets the form's contents in such a way that the specified highscores are displayed.
        /// </summary>
        /// <param name="Highscores">A dictionary mapping from player name to highscores.</param>
        /// <param name="Level">The level whose highscores are being displayed.</param>
        public void SetContents(Dictionary<string, Highscore> Highscores, SokobanLevel Level)
        {
            pnlHighscores.ColumnCount = 3;

            ctLevelPicture.ClientSize = new Size(ctLevelPicture.ClientSize.Width,
                (int) (ctLevelPicture.ClientSize.Width * Level.Height / Level.Width));
            CurLevel = Level.Clone();
            CurLevel.EnsureSpace(1);

            List<string> PlayerNames = new List<string>();
            foreach (string s in Highscores.Keys)
            {
                Highscore h = Highscores[s];
                int i = 0;
                while ((i < PlayerNames.Count) && (h.CompareTo(Highscores[PlayerNames[i]]) < 0))
                    i++;
                PlayerNames.Insert(i, s);
            }
            for (int i = 0; i < PlayerNames.Count; i++)
            {
                Label PlayerNameLabel = new Label();
                PlayerNameLabel.Font = new Font(Font, FontStyle.Bold);
                PlayerNameLabel.Text = PlayerNames[i];
                PlayerNameLabel.AutoSize = true;
                PlayerNameLabel.Margin = new Padding(5);
                pnlHighscores.Controls.Add(PlayerNameLabel, 1, i);

                Highscore h = Highscores[PlayerNames[i]];
                Label ScoreLabel = new Label();
                ScoreLabel.Text = Program.Translation.Dialogs.Highscores.Fmt(h.BestPushScore.E1, h.BestPushScore.E2);
                ScoreLabel.AutoSize = true;
                ScoreLabel.Margin = new Padding(5);
                pnlHighscores.Controls.Add(ScoreLabel, 2, i);
            }
            pnlHighscores.Controls.Add(btnOK, 2, PlayerNames.Count);

            pnlHighscores.Controls.Add(ctLevelPicture, 0, 0);
            pnlHighscores.SetRowSpan(ctLevelPicture, PlayerNames.Count + 1);

            while (pnlHighscores.ColumnStyles.Count < 3)
                pnlHighscores.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            while (pnlHighscores.RowStyles.Count < PlayerNames.Count + 1)
                pnlHighscores.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }

        private void LevelPicture_Paint(object sender, PaintEventArgs e)
        {
            Renderer Render = new Renderer(CurLevel, ctLevelPicture.Width, ctLevelPicture.Height);
            Render.Render(e.Graphics);
        }
    }
}