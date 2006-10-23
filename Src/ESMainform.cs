using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RT.Util;
using RT.Util.Settings;

namespace ExpertSokoban
{
    public partial class ESMainform : ManagedForm
    {
        private bool EverMoved;
        private SokobanLevel OrigLevel;
        private int EditingIndex;

        // For a bug workaround
        private bool LevelListEverShown = false;

        public ESMainform()
        {
            InitializeComponent();
            LoadPrgSettings();
            OrigLevel = SokobanLevel.TestLevel();
            MainArea.SetLevel(OrigLevel);
            EverMoved = false;

            (MainArea.MoveDrawMode == ESPathDrawMode.Arrows ? ViewMoveArrows :
                MainArea.MoveDrawMode == ESPathDrawMode.Dots ? ViewMoveDots :
                MainArea.MoveDrawMode == ESPathDrawMode.Line ? ViewMoveLine : ViewMoveNo).Checked = true;
            (MainArea.PushDrawMode == ESPathDrawMode.Arrows ? ViewPushArrows :
                MainArea.PushDrawMode == ESPathDrawMode.Dots ? ViewPushDots :
                MainArea.PushDrawMode == ESPathDrawMode.Line ? ViewPushLine : ViewPushNo).Checked = true;
            ViewEndPos.Checked = MainArea.ShowEndPos;
        }

        /// <summary>
        /// Loads all program settings
        /// </summary>
        private void LoadPrgSettings()
        {
            PrgSettings.OpenForRead();
            this.LoadSettings(PrgSettings.Store, "MainForm", "");
            PrgSettings.Close();
        }

        /// <summary>
        /// Saves all program settings
        /// </summary>
        private void SavePrgSettings()
        {
            PrgSettings.OpenForWrite(true);
            this.SaveSettings(PrgSettings.Store, "MainForm", "");
            PrgSettings.Close();
        }

        private void MainArea_MoveMade(object sender, EventArgs e)
        {
            EverMoved = true;
        }

        // Used only by MenuOpen_Click()
        private enum ESMFLevelReaderState { Comment, Empty, Level }

        private void LevelOpen_Click(object sender, EventArgs e)
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
                LevelListEverShown = true;
            }
        }

        private void TakeLevel()
        {
            object Item = LevelList.SelectedItem;
            if (Item is SokobanLevel && MayDestroy("Open level"))
            {
                OrigLevel = (SokobanLevel) Item;
                MainArea.SetLevel(OrigLevel);
                EverMoved = false;
            }
        }

        private bool MayDestroy(string Title)
        {
            if (MainArea.State == ESMainAreaState.Solved ||
                MainArea.State == ESMainAreaState.Null ||
                !EverMoved)
                return true;

            return MessageBox.Show("Are you sure you wish to give up the current level?",
                Title, MessageBoxButtons.YesNo) == DialogResult.Yes;
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
                string NewComment = InputBox.GetLine("Please enter the revised comment:", (string) Item);
                if (NewComment != null)
                    LevelList.Items[LevelList.SelectedIndex] = NewComment;
            }
        }

        private void LevelList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                LevelList_DoubleClick(sender, new EventArgs());
        }

        private void ViewLevelsList_Click(object sender, EventArgs e)
        {
            if (LevelListPanel.Visible)
            {
                LevelListPanel.Visible = false;
                LevelListSplitter.Visible = false;
                ViewLevelsList.Checked = false;
                LevelMenu.Visible = false;
            }
            else
            {
                LevelListSplitter.Visible = true;
                LevelListPanel.Visible = true;
                ViewLevelsList.Checked = true;
                LevelMenu.Visible = true;
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

        private void GameUndo_Click(object sender, EventArgs e)
        {
            MainArea.Undo();
        }

        private void GameRetry_Click(object sender, EventArgs e)
        {
            if (MayDestroy("Retry level"))
            {
                MainArea.SetLevel(OrigLevel);
                EverMoved = false;
            }
        }

        private void GameExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LevelAddComment_Click(object sender, EventArgs e)
        {
            if (!LevelListPanel.Visible)
                ViewLevelsList_Click(sender, e);
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

        private void LevelNew_Click(object sender, EventArgs e)
        {
            if (!LevelListPanel.Visible)
                ViewLevelsList_Click(sender, e);
            if (MessageBox.Show("Are you sure you wish to delete all the levels in the levels list?",
                "Expert Sokoban", MessageBoxButtons.YesNo) == DialogResult.Yes)
                LevelList.Items.Clear();
        }

        private void LevelCreate_Click(object sender, EventArgs e)
        {
            if (!LevelListPanel.Visible)
                ViewLevelsList_Click(sender, e);
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

        private void LevelDelete_Click(object sender, EventArgs e)
        {
            if (!LevelListPanel.Visible || LevelList.SelectedIndex < 0)
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

        private void ViewMove_Click(object sender, EventArgs e)
        {
            ViewMoveNo.Checked = sender == ViewMoveNo;
            ViewMoveLine.Checked = sender == ViewMoveLine;
            ViewMoveDots.Checked = sender == ViewMoveDots;
            ViewMoveArrows.Checked = sender == ViewMoveArrows;

            MainArea.MoveDrawMode =
                sender == ViewMoveLine ? ESPathDrawMode.Line :
                sender == ViewMoveDots ? ESPathDrawMode.Dots :
                sender == ViewMoveArrows ? ESPathDrawMode.Arrows : ESPathDrawMode.None;
        }

        private void ViewPush_Click(object sender, EventArgs e)
        {
            ViewPushNo.Checked = sender == ViewPushNo;
            ViewPushLine.Checked = sender == ViewPushLine;
            ViewPushDots.Checked = sender == ViewPushDots;
            ViewPushArrows.Checked = sender == ViewPushArrows;

            MainArea.PushDrawMode =
                sender == ViewPushLine ? ESPathDrawMode.Line :
                sender == ViewPushDots ? ESPathDrawMode.Dots :
                sender == ViewPushArrows ? ESPathDrawMode.Arrows : ESPathDrawMode.None;
        }

        private void ViewEndPos_Click(object sender, EventArgs e)
        {
            ViewEndPos.Checked = !ViewEndPos.Checked;
            MainArea.ShowEndPos = ViewEndPos.Checked;
        }

        private void ESMainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!MayDestroy("Exit Expert Sokoban"))
                e.Cancel = true;
        }

        private void LevelCopy_Click(object sender, EventArgs e)
        {
            if (!LevelListPanel.Visible || LevelList.SelectedIndex < 0)
                return;
            Clipboard.SetData("SokobanData", LevelList.Items[LevelList.SelectedIndex]);
        }

        private void LevelCut_Click(object sender, EventArgs e)
        {
            if (!LevelListPanel.Visible || LevelList.SelectedIndex < 0)
                return;
            LevelCopy_Click(sender, e);
            int OldIndex = LevelList.SelectedIndex;
            LevelList.Items.RemoveAt(OldIndex);
            if (LevelList.Items.Count > 0 && OldIndex < LevelList.Items.Count)
                LevelList.SelectedIndex = OldIndex;
            else if (LevelList.Items.Count > 0)
                LevelList.SelectedIndex = LevelList.Items.Count-1;
        }

        private void LevelPaste_Click(object sender, EventArgs e)
        {
            if (!LevelListPanel.Visible)
                return;
            if (Clipboard.ContainsData("SokobanData"))
            {
                if (LevelList.SelectedIndex < 0)
                {
                    LevelList.Items.Add(Clipboard.GetData("SokobanData"));
                    LevelList.SelectedIndex = LevelList.Items.Count-1;
                }
                else
                {
                    LevelList.Items.Insert(LevelList.SelectedIndex, Clipboard.GetData("SokobanData"));
                    LevelList.SelectedIndex -= 1;
                }
            }
        }

        private void LevelEdit_Click(object sender, EventArgs e)
        {
            if (LevelList.SelectedIndex < 0)
                return;
            EditingIndex = LevelList.SelectedIndex;
            LevelListPanel.Visible = false;
            LevelListSplitter.Visible = false;
            LevelMenu.Visible = false;
            EditMenu.Visible = true;
        }

        private void ESMainform_FormClosed(object sender, FormClosedEventArgs e)
        {
            SavePrgSettings();
        }
    }
}
