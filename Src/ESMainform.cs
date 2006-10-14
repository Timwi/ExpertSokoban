using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ExpertSokoban
{
    public partial class ESMainform : Form
    {
        private SokobanLevel[] currentLevels;

        public ESMainform()
        {
            InitializeComponent();
            currentLevels = new SokobanLevel[] { SokobanLevel.getTestLevel() };
            MainArea.SetLevel(currentLevels[0]);
        }

        private void ToolExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ToolUndo_Click(object sender, EventArgs e)
        {
            MainArea.undo();
        }

        private void ToolEdit_Click(object sender, EventArgs e)
        {
            MainArea.reinitSize(true);
        }

        private enum LevelReaderState
        {
            Comment, Empty, Level
        };
        private void ToolOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(dlg.FileName, Encoding.UTF8);
                LevelReaderState state = LevelReaderState.Empty;
                String line;
                String curComment = "";
                String curLevel = "";
                do
                {
                    line = streamReader.ReadLine();
                    if (line == null || line.Length == 0)
                    {
                        if (state == LevelReaderState.Comment)
                        {
                            LevelList.Items.Add(curComment);
                            curComment = "";
                        }
                        else if (state == LevelReaderState.Level)
                        {
                            LevelList.Items.Add(new SokobanLevel(curLevel));
                            curLevel = "";
                        }
                        state = LevelReaderState.Empty;
                    }
                    else if (line[0] == ';')
                    {
                        if (state == LevelReaderState.Level)
                        {
                            LevelList.Items.Add(new SokobanLevel(curLevel));
                            curLevel = "";
                        }
                        curComment += line.Substring(1) + "\n";
                        state = LevelReaderState.Comment;
                    }
                    else
                    {
                        if (state == LevelReaderState.Comment)
                        {
                            LevelList.Items.Add(curComment);
                            curComment = "";
                        }
                        curLevel += line + "\n";
                        state = LevelReaderState.Level;
                    }
                }
                while (line != null);
                streamReader.Close();
                LevelListSplitter.Visible = true;
                LevelListPanel.Visible = true;
            }
        }

        private void LevelListClose_Click(object sender, EventArgs e)
        {
            LevelListPanel.Visible = false;
            LevelListSplitter.Visible = false;
        }
    }
}