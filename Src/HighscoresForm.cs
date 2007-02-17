using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ExpertSokoban
{
    public partial class HighscoresForm : Form
    {
        private SokobanLevel CurLevel;

        public HighscoresForm()
        {
            InitializeComponent();
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
            HighscoresTable.ColumnCount = 3;

            LevelPicture.ClientSize = new Size(LevelPicture.ClientSize.Width,
                (int)(LevelPicture.ClientSize.Width*Level.Height/Level.Width));
            CurLevel = Level;

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
                HighscoresTable.Controls.Add(PlayerNameLabel, 1, i);

                Highscore h = Highscores[PlayerNames[i]];
                Label ScoreLabel = new Label();
                ScoreLabel.Text = h.BestPushScore.E1 + " pushes, " + h.BestPushScore.E2 + " moves";
                ScoreLabel.AutoSize = true;
                ScoreLabel.Margin = new Padding(5);
                HighscoresTable.Controls.Add(ScoreLabel, 2, i);
            }
            HighscoresTable.Controls.Add(OKButton, 2, PlayerNames.Count);

            HighscoresTable.Controls.Add(LevelPicture, 0, 0);
            HighscoresTable.SetRowSpan(LevelPicture, PlayerNames.Count+1);

            while (HighscoresTable.ColumnStyles.Count < 3)
                HighscoresTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            while (HighscoresTable.RowStyles.Count < PlayerNames.Count+1)
                HighscoresTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }

        private void LevelPicture_Paint(object sender, PaintEventArgs e)
        {
            Renderer Render = new Renderer(CurLevel, LevelPicture.Width, LevelPicture.Height);
            Render.Render(e.Graphics);
        }
    }
}