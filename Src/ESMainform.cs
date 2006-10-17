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
        public ESMainform()
        {
            InitializeComponent();
            MainArea.SetLevel(SokobanLevel.TestLevel());
        }

        private void ToolExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ToolUndo_Click(object sender, EventArgs e)
        {
            MainArea.Undo();
        }

        private enum LevelReaderState
        {
            Comment, Empty, Level
        }

        private void ToolOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDialog = new OpenFileDialog();
            if (OpenDialog.ShowDialog() == DialogResult.OK)
            {
                LevelList.Items.Clear();
                StreamReader StreamReader = new StreamReader(OpenDialog.FileName, Encoding.UTF8);
                LevelReaderState State = LevelReaderState.Empty;
                String Line;
                String Comment = "";
                String LevelEncoded = "";
                do
                {
                    Line = StreamReader.ReadLine();
                    if (Line == null || Line.Length == 0)
                    {
                        if (State == LevelReaderState.Comment)
                        {
                            LevelList.Items.Add(Comment);
                            Comment = "";
                        }
                        else if (State == LevelReaderState.Level)
                        {
                            LevelList.Items.Add(new SokobanLevel(LevelEncoded));
                            LevelEncoded = "";
                        }
                        State = LevelReaderState.Empty;
                    }
                    else if (Line[0] == ';')
                    {
                        if (State == LevelReaderState.Level)
                        {
                            LevelList.Items.Add(new SokobanLevel(LevelEncoded));
                            LevelEncoded = "";
                        }
                        Comment += Line.Substring(1) + "\n";
                        State = LevelReaderState.Comment;
                    }
                    else
                    {
                        if (State == LevelReaderState.Comment)
                        {
                            LevelList.Items.Add(Comment);
                            Comment = "";
                        }
                        LevelEncoded += Line + "\n";
                        State = LevelReaderState.Level;
                    }
                }
                while (Line != null);
                StreamReader.Close();
                LevelListSplitter.Visible = true;
                LevelListPanel.Visible = true;
            }
        }

        private void LevelListClose_Click(object sender, EventArgs e)
        {
            LevelListPanel.Visible = false;
            LevelListSplitter.Visible = false;
        }

        private void LevelList_DoubleClick(object sender, EventArgs e)
        {
            object Item = LevelList.SelectedItem;
            if (Item is SokobanLevel)
            {
                if (MainArea.State == ESMainAreaState.Solved ||
                    MainArea.State == ESMainAreaState.Null ||
                    MessageBox.Show("Are you sure you wish to give up the current level?",
                    "Open level", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    MainArea.SetLevel(((SokobanLevel) Item).Clone());
                }
            }
        }
    }
}