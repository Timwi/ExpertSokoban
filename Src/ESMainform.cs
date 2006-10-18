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
        private bool EverMoved;

        public ESMainform()
        {
            InitializeComponent();
            MainArea.SetLevel(SokobanLevel.TestLevel());
            EverMoved = false;
        }

        private void MainArea_MoveMade(object sender, EventArgs e)
        {
            EverMoved = true;
        }

        private void ToolExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ToolUndo_Click(object sender, EventArgs e)
        {
            MainArea.Undo();
        }

        // Used only by ToolOpen_Click()
        private enum ESMFLevelReaderState { Comment, Empty, Level }

        private void ToolOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDialog = new OpenFileDialog();
            if (OpenDialog.ShowDialog() == DialogResult.OK)
            {
                LevelListSplitter.Visible = true;
                LevelListPanel.Visible = true;
                LevelList.Focus();
                LevelList.BeginUpdate();
                LevelList.Items.Clear();
                StreamReader StreamReader = new StreamReader(OpenDialog.FileName, Encoding.UTF8);
                ESMFLevelReaderState State = ESMFLevelReaderState.Empty;
                String Line;
                String Comment = "";
                String LevelEncoded = "";
                do
                {
                    Line = StreamReader.ReadLine();
                    if (Line == null || Line.Length == 0)
                    {
                        if (State == ESMFLevelReaderState.Comment)
                        {
                            LevelList.Items.Add(Comment);
                            Comment = "";
                        }
                        else if (State == ESMFLevelReaderState.Level)
                        {
                            LevelList.Items.Add(new SokobanLevel(LevelEncoded));
                            LevelEncoded = "";
                        }
                        State = ESMFLevelReaderState.Empty;
                    }
                    else if (Line[0] == ';')
                    {
                        if (State == ESMFLevelReaderState.Level)
                        {
                            LevelList.Items.Add(new SokobanLevel(LevelEncoded));
                            LevelEncoded = "";
                        }
                        Comment += Line.Substring(1) + "\n";
                        State = ESMFLevelReaderState.Comment;
                    }
                    else
                    {
                        if (State == ESMFLevelReaderState.Comment)
                        {
                            LevelList.Items.Add(Comment);
                            Comment = "";
                        }
                        LevelEncoded += Line + "\n";
                        State = ESMFLevelReaderState.Level;
                    }
                }
                while (Line != null);
                LevelList.EndUpdate();
                StreamReader.Close();
            }
        }

        private void LevelListClose_Click(object sender, EventArgs e)
        {
            LevelListPanel.Visible = false;
            LevelListSplitter.Visible = false;
        }

        private void TakeLevel()
        {
            object Item = LevelList.SelectedItem;
            if (Item is SokobanLevel && MayDestroy())
            {
                MainArea.SetLevel(((SokobanLevel) Item).Clone());
                EverMoved = false;
            }
        }

        private bool MayDestroy()
        {
            if (MainArea.State == ESMainAreaState.Solved ||
                MainArea.State == ESMainAreaState.Null ||
                !EverMoved)
                return true;

            return MessageBox.Show("Are you sure you wish to give up the current level?",
                "Open level", MessageBoxButtons.YesNo) == DialogResult.Yes;
        }

        private void LevelList_DoubleClick(object sender, EventArgs e)
        {
            TakeLevel();
        }

        private void LevelList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                TakeLevel();
        }
    }
}