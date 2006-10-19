using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RT.Util;

namespace ExpertSokoban
{
    public partial class ESMainform : Form
    {
        private bool EverMoved;
        private SokobanLevel OrigLevel;
        
        // For a bug workaround
        private bool LevelListEverShown = false;

        public ESMainform()
        {
            InitializeComponent();
            OrigLevel = SokobanLevel.TestLevel();
            MainArea.SetLevel(OrigLevel);
            EverMoved = false;
        }

        private void MainArea_MoveMade(object sender, EventArgs e)
        {
            EverMoved = true;
        }

        // Used only by ToolOpen_Click()
        private enum ESMFLevelReaderState { Comment, Empty, Level }

        private void MenuOpen_Click(object sender, EventArgs e)
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

        private void TakeLevel()
        {
            object Item = LevelList.SelectedItem;
            if (Item is SokobanLevel && MayDestroy())
            {
                OrigLevel = (SokobanLevel) Item;
                MainArea.SetLevel(OrigLevel);
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
            if (LevelList.SelectedIndex < 0)
                return;

            object Item = LevelList.SelectedItem;
            if (Item is SokobanLevel)
                TakeLevel();
            else
            {
                string NewComment = InputBox.GetLine("Please enter the revised comment:", (string)Item);
                if (NewComment != null)
                    LevelList.Items[LevelList.SelectedIndex] = NewComment;
            }
        }

        private void LevelList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                LevelList_DoubleClick(sender, new EventArgs());
        }

        private void MenuShowList_Click(object sender, EventArgs e)
        {
            if (LevelListPanel.Visible)
            {
                LevelListPanel.Visible = false;
                LevelListSplitter.Visible = false;
                MenuShowList.Checked = false;
                MenuLevelsSub.Visible = false;
            }
            else
            {
                LevelListSplitter.Visible = true;
                LevelListPanel.Visible = true;
                MenuShowList.Checked = true;
                MenuLevelsSub.Visible = true;
                if (!LevelListEverShown)
                {
                    LevelList.Items.Add(OrigLevel);
                    LevelListEverShown = true;
                }
                LevelList.Focus();
                if (LevelList.SelectedIndex < 0)
                    LevelList.SelectedIndex = 0;
            }
        }

        private void MenuUndo_Click(object sender, EventArgs e)
        {
            MainArea.Undo();
        }

        private void MenuRetry_Click(object sender, EventArgs e)
        {
            if (MayDestroy())
            {
                MainArea.SetLevel(OrigLevel);
                EverMoved = false;
            }
        }

        private void MenuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MenuLevAddComment_Click(object sender, EventArgs e)
        {
            string Comment = InputBox.GetLine("Please enter a comment:");
            if (Comment != null)
            {
                if (LevelList.SelectedIndex < 0)
                {
                    LevelList.Items.Add(Comment);
                    LevelList.SelectedIndex = LevelList.Items.Count-1;
                }
                else
                {
                    LevelList.Items.Insert(LevelList.SelectedIndex, Comment);
                    LevelList.SelectedIndex -= 1;
                }
            }
        }

        private void MenuLevClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to delete all the levels in the levels list?",
                "Expert Sokoban", MessageBoxButtons.YesNo) == DialogResult.Yes)
                LevelList.Items.Clear();
        }

        private void MenuLevCreate_Click(object sender, EventArgs e)
        {
            SokobanLevel NewLevel = SokobanLevel.TrivialLvel();
            if (LevelList.SelectedIndex < 0)
            {
                LevelList.Items.Insert(0, NewLevel);
                LevelList.SelectedIndex = 0;
            }
            else
            {
                LevelList.Items.Insert(LevelList.SelectedIndex+1, NewLevel);
                LevelList.SelectedIndex += 1;
            }
        }

        private void LevelToolDelete_Click(object sender, EventArgs e)
        {
            if (LevelList.SelectedIndex < 0)
                return;

            object Item = LevelList.Items[LevelList.SelectedIndex];
            if ((Item is SokobanLevel && MessageBox.Show("Are you sure you wish to delete this level?",
                "Expert Sokoban", MessageBoxButtons.YesNo) == DialogResult.Yes) ||
                Item is string)
            {
                int OldIndex = LevelList.SelectedIndex;
                LevelList.Items.RemoveAt(OldIndex);
                if (LevelList.Items.Count > 0 && OldIndex < LevelList.Items.Count)
                    LevelList.SelectedIndex = OldIndex;
                else if (LevelList.Items.Count > 0)
                    LevelList.SelectedIndex = LevelList.Items.Count-1;
            }
        }

        private void LevelList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                LevelToolDelete_Click(sender, new EventArgs());
        }
    }
}