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
        /// <param name="HighScores">A dictionary mapping from player name to highscores.</param>
        public void SetContents(Dictionary<string, Highscore> Highscores)
        {
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
                HighscoresTable.Controls.Add(PlayerNameLabel, 0, i);

                Highscore h = Highscores[PlayerNames[i]];
                Label ScoreLabel = new Label();
                ScoreLabel.Text = h.BestPushScore.E1 + " pushes, " + h.BestPushScore.E2 + " moves";
                HighscoresTable.Controls.Add(ScoreLabel, 1, i);
            }
            HighscoresTable.Controls.Add(OKButton, 1, PlayerNames.Count);
        }
    }
}