using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using RT.Util;
using RT.Util.Settings;
using RT.Util.Forms;
using RT.Util.Dialogs;

namespace ExpertSokoban
{
    public partial class Mainform : ManagedForm
    {
        private bool EverMovedOrEdited;
        private bool LevelFileChanged;
        private SokobanLevel OrigLevel;
        private String LevelFilename;
        private MainFormSettings FSettings;

        public Mainform()
        {
            InitializeComponent();
            // Start with the default level
            OrigLevel = SokobanLevel.TestLevel();
            MainArea.SetLevel(OrigLevel);
            LevelList.Items.Add(OrigLevel);
            LevelList.SelectedIndex = 0;
            LevelList.PlayingIndex = 0;
            EverMovedOrEdited = false;
            LevelFileChanged = false;
            LevelFilename = null;

            // Restore saved settings
            FSettings = PrgSettings.Store.GetObject("ExpSok Mainform", null) as MainFormSettings;
            if (FSettings == null) FSettings = new MainFormSettings();
            LevelListToolStrip1.Visible = ViewToolStrip1.Checked = FSettings.DisplayToolStrip1;
            LevelListToolStrip2.Visible = ViewToolStrip2.Checked = FSettings.DisplayToolStrip2;
            ViewEditToolStrip.Checked = FSettings.DisplayEditToolStrip;
            LevelListPanel.Width = FSettings.LevelListPanelWidth > 50 ? FSettings.LevelListPanelWidth : 50;
            LevelListVisible(FSettings.DisplayLevelList);
            MovePathOptions.SetValue(FSettings.MoveDrawMode);
            PushPathOptions.SetValue(FSettings.PushDrawMode);
            EditToolOptions.SetValue(FSettings.LastUsedTool);

            ViewEndPos.Checked = MainArea.ShowEndPos = FSettings.ShowEndPos;
        }

        private void MainArea_MoveMade(object sender, EventArgs e)
        {
            EverMovedOrEdited = true;
        }

        // Used only by LevelOpen_Click()
        private enum LevelReaderState { Comment, Empty, Level }

        private void LevelOpen_Click(object sender, EventArgs e)
        {
            if (MayDestroy("Open level file"))
            {
                OpenFileDialog OpenDialog = new OpenFileDialog();
                if (OpenDialog.ShowDialog() == DialogResult.OK)
                {
                    LevelListVisible(true);
                    LevelList.BeginUpdate();
                    LevelList.Items.Clear();
                    int? FoundValidLevel = null;
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
                                SokobanLevel NewLevel = new SokobanLevel(LevelEncoded);
                                LevelList.Items.Add(NewLevel);
                                LevelEncoded = "";
                                if (NewLevel.IsValid() == SokobanLevelStatus.Valid && FoundValidLevel == null)
                                    FoundValidLevel = LevelList.Items.Count-1;
                            }
                            State = LevelReaderState.Empty;
                        }
                        else if (Line[0] == ';')
                        {
                            if (State == LevelReaderState.Level)
                            {
                                SokobanLevel NewLevel = new SokobanLevel(LevelEncoded);
                                LevelList.Items.Add(NewLevel);
                                LevelEncoded = "";
                                if (NewLevel.IsValid() == SokobanLevelStatus.Valid && FoundValidLevel == null)
                                    FoundValidLevel = LevelList.Items.Count-1;
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
                    LevelList.EndUpdate();
                    StreamReader.Close();
                    LevelFileChanged = false;
                    LevelFilename = OpenDialog.FileName;
                    if (FoundValidLevel == null)
                    {
                        MainArea.Clear();
                        LevelList.PlayingIndex = null;
                    }
                    else
                        TakeLevel(FoundValidLevel.Value, true);
                }
            }
        }

        private void TakeLevel() { TakeLevel(LevelList.SelectedIndex, false); }
        private void TakeLevel(int Index) { TakeLevel(Index, false); }
        private void TakeLevel(int Index, bool Override)
        {
            object Item = LevelList.Items[Index];
            if (Item is SokobanLevel && (Override || MayDestroyMainAreaLevel("Open level")))
            {
                if (MainArea.State == MainAreaState.Editing)
                    SwitchEditingMode(false);

                OrigLevel = (SokobanLevel)Item;
                SokobanLevelStatus Status = OrigLevel.IsValid();
                if (Status == SokobanLevelStatus.Valid)
                {
                    MainArea.SetLevel(OrigLevel);
                    EverMovedOrEdited = false;
                    LevelList.SelectedIndex = Index;
                    LevelList.PlayingIndex = Index;
                }
                else
                {
                    LevelList.PlayingIndex = null;
                    MainArea.Clear();
                    if (!Override)
                    {
                        String Problem = Status == SokobanLevelStatus.NotEnclosed
                            ? "The level is not completely enclosed by a wall."
                            : "The number of pieces does not match the number of targets.";
                        if (MessageBox.Show("The level could not be opened because it is invalid.\n\n" + Problem +
                            "\n\nYou must edit the level in order to address this issue. " +
                            "Would you like to edit the level now?", "Expert Sokoban", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            EnterEditingMode();
                    }
                }
            }
        }

        private bool MayDestroy(string Title)
        {
            return MayDestroyMainAreaLevel(Title) && MayDestroyLevelFile(Title);
        }

        private bool MayDestroyLevelFile(string Title)
        {
            if (!LevelFileChanged)
                return true;

            DialogResult Result = MessageBox.Show("You have made changes to "
                + (LevelFilename == null ? "(untitled)" : LevelFilename) +
                ". Would you like to save those changes?", Title, MessageBoxButtons.YesNoCancel);
            if (Result == DialogResult.Cancel)
                return false;
            if (Result == DialogResult.Yes)
            {
                if (SaveWithDialog() != DialogResult.OK)
                    return false;
            }
            return true;
        }

        private bool MayDestroyMainAreaLevel(string Title)
        {
            if (MainArea.State == MainAreaState.Solved ||
                MainArea.State == MainAreaState.Null ||
                !EverMovedOrEdited)
                return true;

            return MainArea.State == MainAreaState.Editing
                ? MessageBox.Show("Are you sure you wish to discard your changes to the level you're editing?",
                    Title, MessageBoxButtons.YesNo) == DialogResult.Yes
                : MessageBox.Show("Are you sure you wish to give up the current level?",
                    Title, MessageBoxButtons.YesNo) == DialogResult.Yes;
        }

        private void LevelList_DoubleClick(object sender, EventArgs e)
        {
            if (LevelList.SelectedIndex < 0)
                return;

            object Item = LevelList.SelectedItem;
            if (Item is SokobanLevel)
            {
                TakeLevel();
            }
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

        private void ViewLevelsList_Click(object sender, EventArgs e)
        {
            LevelListVisible(!LevelListPanel.Visible);
        }

        private void LevelUndo_Click(object sender, EventArgs e)
        {
            MainArea.Undo();
        }

        private void LevelRetry_Click(object sender, EventArgs e)
        {
            if (MayDestroyMainAreaLevel("Retry level"))
            {
                MainArea.SetLevel(OrigLevel);
                EverMovedOrEdited = false;
            }
        }

        private void LevelExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void EditAddComment_Click(object sender, EventArgs e)
        {
            if (!LevelListPanel.Visible)
                ViewLevelsList_Click(sender, e);
            string Comment = InputBox.GetLine("Please enter a comment:");
            if (Comment != null)
                AddLevelListItem(Comment);
        }

        private void LevelNew_Click(object sender, EventArgs e)
        {
            if (!LevelListPanel.Visible)
                ViewLevelsList_Click(sender, e);
            if (MayDestroy("New level file"))
            {
                LevelList.Items.Clear();
                LevelList.PlayingIndex = null;
                MainArea.Clear();
                LevelFilename = null;
            }
        }

        private void EditCreateLevel_Click(object sender, EventArgs e)
        {
            if (!LevelListPanel.Visible)
                ViewLevelsList_Click(sender, e);
            SokobanLevel NewLevel = SokobanLevel.TrivialLevel();
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

        private void EditDelete_Click(object sender, EventArgs e)
        {
            if (!LevelListPanel.Visible || LevelList.SelectedIndex < 0)
                return;

            object Item = LevelList.Items[LevelList.SelectedIndex];

            if (Item is SokobanLevel && LevelList.SelectedIndex == LevelList.EditingIndex)
            {
                if (MessageBox.Show("You are currently editing this level.\n\n" +
                    "If you delete this level now, all your modifications will be discarded.\n\n" +
                    "Are you sure you wish to do this?", "Delete level", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
                MainArea.Clear();
                LevelList.EditingIndex = null;
                SwitchEditingMode(false);
            }
            else if (Item is SokobanLevel && LevelList.SelectedIndex == LevelList.PlayingIndex)
            {
                if (MessageBox.Show("You are currently playing this level.\n\n" +
                    "Are you sure you wish to give up?", "Delete level", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
                MainArea.Clear();
                LevelList.PlayingIndex = null;
            }
            else if (Item is SokobanLevel)
            {
                if (MessageBox.Show("Are you sure you wish to delete this level?",
                   "Expert Sokoban", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }

            RemoveLevelListItem(LevelList.SelectedIndex);
        }

        private void RemoveLevelListItem(int Index)
        {
            LevelList.Items.RemoveAt(Index);
            if (LevelList.Items.Count > 0 && Index < LevelList.Items.Count)
                LevelList.SelectedIndex = Index;
            else if (LevelList.Items.Count > 0)
                LevelList.SelectedIndex = LevelList.Items.Count-1;
            if (LevelList.EditingIndex != null && LevelList.EditingIndex.Value > Index)
                LevelList.EditingIndex = LevelList.EditingIndex.Value-1;
            if (LevelList.PlayingIndex != null && LevelList.PlayingIndex.Value > Index)
                LevelList.PlayingIndex = LevelList.PlayingIndex.Value-1;
        }

        private void MovePathOptions_ValueChanged(object sender, EventArgs e)
        {
            MainArea.MoveDrawMode = FSettings.MoveDrawMode = MovePathOptions.Value;
        }

        private void PushPathOptions_ValueChanged(object sender, EventArgs e)
        {
            MainArea.PushDrawMode = FSettings.PushDrawMode = PushPathOptions.Value;
        }

        private void ViewEndPos_Click(object sender, EventArgs e)
        {
            ViewEndPos.Checked = !ViewEndPos.Checked;
            MainArea.ShowEndPos = FSettings.ShowEndPos = ViewEndPos.Checked;
        }

        private void Mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!MayDestroy("Exit Expert Sokoban"))
                e.Cancel = true;
        }

        private void EditCopy_Click(object sender, EventArgs e)
        {
            if (!LevelListPanel.Visible || LevelList.SelectedIndex < 0)
                return;
            Clipboard.SetData("SokobanData", LevelList.Items[LevelList.SelectedIndex]);
        }

        private void EditCut_Click(object sender, EventArgs e)
        {
            if (!LevelListPanel.Visible || LevelList.SelectedIndex < 0)
                return;
            EditCopy_Click(sender, e);
            RemoveLevelListItem(LevelList.SelectedIndex);
        }

        private void EditPaste_Click(object sender, EventArgs e)
        {
            if (!LevelListPanel.Visible)
                return;
            if (Clipboard.ContainsData("SokobanData"))
                AddLevelListItem(Clipboard.GetData("SokobanData"));
        }

        private void AddLevelListItem(object NewItem)
        {
            if (LevelList.SelectedIndex < 0)
            {
                LevelList.Items.Add(NewItem);
                LevelList.SelectedIndex = LevelList.Items.Count-1;
            }
            else
            {
                LevelList.Items.Insert(LevelList.SelectedIndex, NewItem);
                LevelList.SelectedIndex -= 1;
                if (LevelList.EditingIndex != null && LevelList.EditingIndex.Value >= LevelList.SelectedIndex)
                    LevelList.EditingIndex = LevelList.EditingIndex.Value+1;
                if (LevelList.PlayingIndex != null && LevelList.PlayingIndex.Value >= LevelList.SelectedIndex)
                    LevelList.PlayingIndex = LevelList.PlayingIndex.Value+1;
            }
        }

        private void EditEdit_Click(object sender, EventArgs e)
        {
            if (LevelList.SelectedIndex >= 0 &&
                LevelList.Items[LevelList.SelectedIndex] is SokobanLevel &&
                MayDestroyMainAreaLevel("Edit level"))
                EnterEditingMode();
        }

        private void Mainform_FormClosed(object sender, FormClosedEventArgs e)
        {
            PrgSettings.Store.Set("ExpSok Mainform", FSettings);
        }

        private void LevelListVisible(bool On)
        {
            FSettings.DisplayLevelList = On;

            // Level list itself
            LevelListPanel.Visible = On;
            LevelListSplitter.Visible = On;
            if (On)
            {
                LevelList.ComeOn_RefreshItems();
                LevelList.Focus();
            }
            if (LevelList.SelectedIndex < 0 && LevelList.Items.Count > 0)
                LevelList.SelectedIndex = 0;

            // "Edit" menu
            EditCreateLevel.Enabled = On;
            EditEdit.Enabled = On;
            EditAddComment.Enabled = On;
            EditCut.Enabled = On;
            EditCopy.Enabled = On;
            EditPaste.Enabled = On;
            EditDelete.Enabled = On;

            // "View" menu
            ViewLevelsList.Checked = On;
            ViewToolStrip1.Enabled = On;
            ViewToolStrip2.Enabled = On;

            // "Level" menu
            LevelSave.Enabled = On;
        }

        private void EditTool_Click(object sender, EventArgs e)
        {
            EditToolOptions.SetValue(
                sender == EditToolWall ? MainAreaTool.Wall :
                sender == EditToolPiece ? MainAreaTool.Piece :
                sender == EditToolTarget ? MainAreaTool.Target : MainAreaTool.Sokoban);
        }

        private void EditCancel_Click(object sender, EventArgs e)
        {
            if (LevelList.EditingIndex != null) // this should always be true
                TakeLevel(LevelList.EditingIndex.Value);
        }

        private void EditOK_Click(object sender, EventArgs e)
        {
            if (LevelList.EditingIndex != null) // this should always be true
            {
                SokobanLevelStatus Status = MainArea.Level.IsValid();
                if (Status != SokobanLevelStatus.Valid)
                {
                    String Problem = Status == SokobanLevelStatus.NotEnclosed
                        ? "The level is not completely enclosed by a wall."
                        : "The number of pieces does not match the number of targets.";
                    if (MessageBox.Show("The following problem has been detected with this level:\n\n" +
                        Problem + "\n\nYou cannot play this level until you address this issue.\n\n" +
                        "Are you sure you wish to leave the level in this invalid state?",
                        "Expert Sokoban", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }

                SokobanLevel Level = MainArea.Level.Clone();
                Level.EnsureSpace(0);
                LevelList.Items[LevelList.EditingIndex.Value] = Level;
                SwitchEditingMode(false);
                TakeLevel(LevelList.EditingIndex.Value, true);
            }
        }

        private void EnterEditingMode()
        {
            LevelList.EditingIndex = LevelList.SelectedIndex;
            MainArea.SetLevelEdit(LevelList.Items[LevelList.SelectedIndex] as SokobanLevel);
            EverMovedOrEdited = false;
            SwitchEditingMode(true);
        }

        private void SwitchEditingMode(bool On)
        {
            // edit toolbar (visibility)
            EditToolStrip.Visible = On && ViewEditToolStrip.Checked;

            // buttons on other toolbars
            LevelToolEdit.Enabled = !On;

            // "Level" menu items
            LevelUndo.Enabled = !On;
            LevelRetry.Enabled = !On;

            // "Edit" menu items
            EditEdit.Enabled = !On;
            EditFinish.Enabled = On;
            EditCancel.Enabled = On;
            EditWall.Enabled = On;
            EditPiece.Enabled = On;
            EditTarget.Enabled = On;
            EditSokoban.Enabled = On;

            // "View" menu items
            ViewEditToolStrip.Enabled = On;
            ViewMoveNo.Enabled = !On;
            ViewMoveLine.Enabled = !On;
            ViewMoveArrows.Enabled = !On;
            ViewMoveDots.Enabled = !On;
            ViewPushNo.Enabled = !On;
            ViewPushLine.Enabled = !On;
            ViewPushArrows.Enabled = !On;
            ViewPushDots.Enabled = !On;
            ViewEndPos.Enabled = !On;
        }

        private void LevelSave_Click(object sender, EventArgs e)
        {
            SaveWithDialog();
        }

        private DialogResult SaveWithDialog()
        {
            if (LevelFilename == null)
            {
                SaveFileDialog SaveDialog = new SaveFileDialog();
                DialogResult Result = SaveDialog.ShowDialog();
                if (Result != DialogResult.OK)
                    return Result;
                LevelFilename = SaveDialog.FileName;
            }

            StreamWriter StreamWriter = new StreamWriter(LevelFilename, false, Encoding.UTF8);
            foreach (object Item in LevelList.Items)
            {
                if (Item is SokobanLevel)
                    StreamWriter.WriteLine((Item as SokobanLevel).ToString());
                else if (Item is string)
                    StreamWriter.WriteLine(";" + (Item as string) + "\n");
                else
                    StreamWriter.WriteLine(";" + Item.ToString() + "\n");
            }
            StreamWriter.Close();

            LevelFileChanged = false;
            return DialogResult.OK;
        }

        private void ViewToolStrip1_Click(object sender, EventArgs e)
        {
            ViewToolStrip1.Checked = !ViewToolStrip1.Checked;
            LevelListToolStrip1.Visible = FSettings.DisplayToolStrip1 = ViewToolStrip1.Checked;
        }

        private void ViewToolStrip2_Click(object sender, EventArgs e)
        {
            ViewToolStrip2.Checked = !ViewToolStrip2.Checked;
            LevelListToolStrip2.Visible = FSettings.DisplayToolStrip2 = ViewToolStrip2.Checked;
        }

        private void ViewEditToolStrip_Click(object sender, EventArgs e)
        {
            ViewEditToolStrip.Checked = !ViewEditToolStrip.Checked;
            EditToolStrip.Visible = FSettings.DisplayEditToolStrip = ViewEditToolStrip.Checked;
        }

        private void LevelListPanel_Resize(object sender, EventArgs e)
        {
            FSettings.LevelListPanelWidth = LevelListPanel.Width;
        }

        private void MainArea_LevelChanged(object sender, EventArgs e)
        {
            EverMovedOrEdited = true;
        }

        /// <summary>
        /// This method is used to work around a bug in .NET's ListBox.
        /// Basically, if you add items to the ListBox while it is invisible,
        /// it screws up and sometimes crashes (some time later). During the
        /// form's initialisation, setting the ListBox's Visible to true
        /// doesn't help. Fortunately, calling RefreshItems() on the ListBox
        /// retrofixes the bug's effects, so we set a timer to do that.
        /// </summary>
        /// <param name="sender">You know.</param>
        /// <param name="e">You know.</param>
        private void BugWorkaroundTimer_Tick(object sender, EventArgs e)
        {
            BugWorkaroundTimer.Enabled = false;
            LevelList.ComeOn_RefreshItems();
        }

        private void EditToolOptions_ValueChanged(object sender, EventArgs e)
        {
            MainArea.Tool = FSettings.LastUsedTool = EditToolOptions.Value;
            EditToolWall.Checked = EditToolOptions.Value == MainAreaTool.Wall;
            EditToolPiece.Checked = EditToolOptions.Value == MainAreaTool.Piece;
            EditToolTarget.Checked = EditToolOptions.Value == MainAreaTool.Target;
            EditToolSokoban.Checked = EditToolOptions.Value == MainAreaTool.Sokoban;
        }
    }

    [Serializable]
    public class MainFormSettings
    {
        public PathDrawMode MoveDrawMode = PathDrawMode.Line;
        public PathDrawMode PushDrawMode = PathDrawMode.Line;
        public bool ShowEndPos = true, DisplayLevelList = false,
            DisplayToolStrip1 = true, DisplayToolStrip2 = true,
            DisplayEditToolStrip = true;
        public MainAreaTool LastUsedTool = MainAreaTool.Wall;
        public int LevelListPanelWidth = 152;
    }
}
