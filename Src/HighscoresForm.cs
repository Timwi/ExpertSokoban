using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using RT.Util.Forms;
using RT.Util.Lingo;

namespace ExpertSokoban
{
    /// <summary>
    /// Encapsulates the Highscores window.
    /// </summary>
    public partial class HighscoresForm : ManagedForm
    {
        private SokobanLevel _level;

        /// <summary>Constructor.</summary>
        public HighscoresForm()
            : base(Program.Settings.HighscoresFormSettings)
        {
            InitializeComponent();
            Lingo.TranslateControl(this, Program.Tr.Highscores);
        }

        private void okClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Sets the form's contents in such a way that the specified highscores are displayed.
        /// </summary>
        /// <param name="highscores">A dictionary mapping from player name to highscores.</param>
        /// <param name="level">The level whose highscores are being displayed.</param>
        public void SetContents(Dictionary<string, Highscore> highscores, SokobanLevel level)
        {
            pnlHighscores.ColumnCount = 3;

            ctLevelPicture.ClientSize = new Size(ctLevelPicture.ClientSize.Width,
                (int) (ctLevelPicture.ClientSize.Width * level.Height / level.Width));
            _level = level.Clone();
            _level.EnsureSpace(1);

            List<string> playerNames = new List<string>();
            foreach (string s in highscores.Keys)
            {
                Highscore h = highscores[s];
                int i = 0;
                while ((i < playerNames.Count) && (h.CompareTo(highscores[playerNames[i]]) < 0))
                    i++;
                playerNames.Insert(i, s);
            }
            for (int i = 0; i < playerNames.Count; i++)
            {
                Label lblPlayerName = new Label();
                lblPlayerName.Font = new Font(Font, FontStyle.Bold);
                lblPlayerName.Text = playerNames[i];
                lblPlayerName.AutoSize = true;
                lblPlayerName.Margin = new Padding(5);
                pnlHighscores.Controls.Add(lblPlayerName, 1, i);

                Highscore h = highscores[playerNames[i]];
                Label lblScore = new Label();
                lblScore.Text = Program.Tr.Highscores.Highscores.Fmt(Program.Tr.Language.GetNumberSystem(), h.BestPushScore.Moves, h.BestPushScore.Pushes);
                lblScore.AutoSize = true;
                lblScore.Margin = new Padding(5);
                pnlHighscores.Controls.Add(lblScore, 2, i);
            }
            pnlHighscores.Controls.Add(btnOK, 2, playerNames.Count);

            pnlHighscores.Controls.Add(ctLevelPicture, 0, 0);
            pnlHighscores.SetRowSpan(ctLevelPicture, playerNames.Count + 1);

            while (pnlHighscores.ColumnStyles.Count < 3)
                pnlHighscores.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            while (pnlHighscores.RowStyles.Count < playerNames.Count + 1)
                pnlHighscores.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }

        private void paintLevel(object sender, PaintEventArgs e)
        {
            new Renderer(_level, ctLevelPicture.Width, ctLevelPicture.Height).Render(e.Graphics);
        }
    }
}