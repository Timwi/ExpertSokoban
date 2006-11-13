using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;
using RT.Util;
using RT.Util.Dialogs;
using RT.Util.Forms;
using RT.Util.Settings;

namespace ExpertSokoban
{
    public partial class Mainform : ManagedForm
    {
        private bool EverMovedOrEdited;
        private SokobanLevel OrigLevel;
        private String FLevelFilename;
        private bool FLevelFileChanged;
        private MainFormSettings FSettings;

        private string LevelFilename
        {
            get { return FLevelFilename; }
            set
            {
                FLevelFilename = value;
                UpdateTitlebar();
            }
        }

        private bool LevelFileChanged
        {
            get { return FLevelFileChanged; }
            set
            {
                FLevelFileChanged = value;
                UpdateTitlebar();
            }
        }

        private void UpdateTitlebar()
        {
            Text = "Expert Sokoban - " +
                    (FLevelFilename == null ? "(untitled)" : Path.GetFileName(FLevelFilename)) +
                    (FLevelFileChanged ? " *" : "");
        }

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
            if (FSettings.SolvedLevels == null) FSettings.SolvedLevels = new Hashtable();
            LevelListToolStrip1.Visible = ViewToolStrip1.Checked = FSettings.DisplayToolStrip1;
            LevelListToolStrip2.Visible = ViewToolStrip2.Checked = FSettings.DisplayToolStrip2;
            ViewEditToolStrip.Checked = FSettings.DisplayEditToolStrip;
            LevelListPanel.Width = FSettings.LevelListPanelWidth > 50 ? FSettings.LevelListPanelWidth : 50;
            LevelListVisible(FSettings.DisplayLevelList);
            MovePathOptions.SetValue(FSettings.MoveDrawMode);
            PushPathOptions.SetValue(FSettings.PushDrawMode);
            EditToolOptions.SetValue(FSettings.LastUsedTool);
            LevelList.SetSolvedLevels(FSettings.SolvedLevels);

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
            if (!MayDestroy("Open level file"))
                return;

            OpenFileDialog OpenDialog = new OpenFileDialog();
            if (OpenDialog.ShowDialog() != DialogResult.OK)
                return;

            LevelListVisible(true);
            LevelList.BeginUpdate();
            LevelList.Items.Clear();
            int? FoundValidLevel = null;
            int? FoundUnsolvedLevel = null;
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
                        if (NewLevel.Validity == SokobanLevelStatus.Valid)
                        {
                            if (FoundValidLevel == null)
                                FoundValidLevel = LevelList.Items.Count-1;
                            if (FoundUnsolvedLevel == null && !FSettings.SolvedLevels.ContainsKey(NewLevel.ToString()))
                                FoundUnsolvedLevel = LevelList.Items.Count-1;
                        }
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
                        if (NewLevel.Validity == SokobanLevelStatus.Valid)
                        {
                            if (FoundValidLevel == null)
                                FoundValidLevel = LevelList.Items.Count-1;
                            if (FoundUnsolvedLevel == null && !FSettings.SolvedLevels.ContainsKey(NewLevel.ToString()))
                                FoundUnsolvedLevel = LevelList.Items.Count-1;
                        }
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
            } while (Line != null);
            LevelList.EndUpdate();
            StreamReader.Close();
            LevelFileChanged = false;
            LevelFilename = OpenDialog.FileName;
            if (FoundUnsolvedLevel != null)
                TakeLevel(FoundUnsolvedLevel.Value, true);
            else if (FoundValidLevel != null)
                TakeLevel(FoundValidLevel.Value, true);
            else
            {
                MainArea.Clear();
                LevelList.PlayingIndex = null;
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
                SokobanLevelStatus Status = OrigLevel.Validity;
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
                        if (DlgMessage.Show("The level could not be opened because it is invalid.\n\n" + Problem +
                            "\n\nYou must edit the level in order to address this issue. " +
                            "Would you like to edit the level now?", "Open level",
                            DlgType.Error, "Edit level", "Cancel") == 0)
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

            int Result = DlgMessage.Show("You have made changes to "
                + (LevelFilename == null ? "(untitled)" : Path.GetFileName(LevelFilename)) +
                ". Would you like to save those changes?", Title, DlgType.Question,
                "Save changes", "&Discard changes", "Cancel");
            if (Result == 2)
                return false;
            if (Result == 0)
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
                ? DlgMessage.Show("Are you sure you wish to discard your changes to the level you're editing?",
                    Title, DlgType.Warning, "Discard changes", "Cancel") == 0
                : DlgMessage.Show("Are you sure you wish to give up the current level?",
                    Title, DlgType.Warning, "Give up", "Cancel") == 0;
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
            AddLevelListItem(SokobanLevel.TrivialLevel());
        }

        private bool CanDeleteLevel(bool NormalConfirmation)
        {
            if (!LevelListPanel.Visible || LevelList.SelectedIndex < 0)
                return false;

            object Item = LevelList.Items[LevelList.SelectedIndex];

            if (Item is SokobanLevel && LevelList.SelectedIndex == LevelList.EditingIndex)
            {
                if (DlgMessage.Show("You are currently editing this level.\n\n" +
                    "If you delete this level now, all your modifications will be discarded.\n\n" +
                    "Are you sure you wish to do this?", "Delete level",
                    DlgType.Warning, "Discard changes", "Cancel") == 1)
                    return false;
                MainArea.Clear();
                LevelList.EditingIndex = null;
                SwitchEditingMode(false);
            }
            else if (Item is SokobanLevel && LevelList.SelectedIndex == LevelList.PlayingIndex)
            {
                if (DlgMessage.Show("You are currently playing this level.\n\n" +
                    "Are you sure you wish to give up?", "Delete level",
                    DlgType.Warning, "Give up", "Cancel") == 1)
                    return false;
                MainArea.Clear();
                LevelList.PlayingIndex = null;
            }
            else if (Item is SokobanLevel)
            {
                if (NormalConfirmation && DlgMessage.Show(
                    "Are you sure you wish to delete this level?",
                    "Delete level", DlgType.Question, "Delete level", "Cancel") == 1)
                    return false;
            }
            return true;
        }

        private void EditDelete_Click(object sender, EventArgs e)
        {
            if (CanDeleteLevel(true))
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
            LevelFileChanged = true;
            LevelNext.Enabled = LevelNextUnsolved.Enabled =
                LevelToolNext.Enabled = LevelToolNextUnsolved.Enabled =
                LevelPrevious.Enabled = LevelPreviousUnsolved.Enabled =
                LevelList.Items.Count > 0;
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
            if (CanDeleteLevel(false))
            {
                EditCopy_Click(sender, e);
                RemoveLevelListItem(LevelList.SelectedIndex);
            }
        }

        private void EditPaste_Click(object sender, EventArgs e)
        {
            if (LevelListPanel.Visible && Clipboard.ContainsData("SokobanData"))
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
            LevelFileChanged = true;
            LevelNext.Enabled = LevelNextUnsolved.Enabled =
                LevelToolNext.Enabled = LevelToolNextUnsolved.Enabled =
                LevelPrevious.Enabled = LevelPreviousUnsolved.Enabled = true;
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
                SokobanLevelStatus Status = MainArea.Level.Validity;
                if (Status != SokobanLevelStatus.Valid)
                {
                    String Problem = Status == SokobanLevelStatus.NotEnclosed
                        ? "The level is not completely enclosed by a wall."
                        : "The number of pieces does not match the number of targets.";
                    if (DlgMessage.Show("The following problem has been detected with this level:\n\n" +
                        Problem + "\n\nYou cannot play this level until you address this issue.\n\n" +
                        "Are you sure you wish to leave the level in this invalid state?",
                        "Finish editing", DlgType.Error, "Save level anyway",
                        "Resume editing") == 1)
                        return;
                }

                SokobanLevel Level = MainArea.Level.Clone();
                Level.EnsureSpace(0);
                LevelList.Items[LevelList.EditingIndex.Value] = Level;
                SwitchEditingMode(false);
                TakeLevel(LevelList.EditingIndex.Value, true);
                LevelFileChanged = true;
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

        private void HelpAbout_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }

        private void HelpHelp_Click(object sender, EventArgs e)
        {
            DlgMessage.Show("Welcome to Expert Sokoban. You can " +
                "find detailed help about this product on our website:\n\n" +
                "http://www.cutebits.com", "Expert Sokoban", Properties.Resources.ExpertSokoban.ToBitmap());
        }

        private void Mainform_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (MainArea.State == MainAreaState.Push)
                    MainArea.Deselect();
                e.Handled = true;
            }
        }

        private void MainArea_LevelSolved(object sender, EventArgs e)
        {
            FSettings.SolvedLevels[OrigLevel.ToString()] = true;
            LevelList.ComeOn_RefreshItems();
        }

        /// <summary>
        /// Pass null to "e" to display congratulations if all levels solved.
        /// </summary>
        private void LevelNext_Click(object sender, EventArgs e)
        {
            if (LevelList.Items.Count < 1)  // should never happen because buttons & menu items should be greyed out
                return;

            bool MustBeUnsolved = sender == LevelNextUnsolved || sender == LevelToolNextUnsolved;

            int OldIndex = LevelList.SelectedIndex, i = OldIndex;
            for (; ; )
            {
                i = (i+1) % LevelList.Items.Count;
                if (i == OldIndex || (i == 0 && OldIndex == -1))
                {
                    if (e==null)
                        DlgMessage.ShowInfo("You have solved all levels in this level pack!", "Congratulations!!!");
                    else if (MustBeUnsolved)
                        DlgMessage.ShowInfo("There are no more unsolved levels in this level file.", "Next unsolved level");
                    else
                        DlgMessage.ShowInfo("There is no other level in the level file.", "Next level");

                    return;
                }
                if (LevelList.Items[i] is SokobanLevel &&
                    (!MustBeUnsolved || !FSettings.SolvedLevels.ContainsKey(LevelList.Items[i].ToString())))
                {
                    TakeLevel(i);
                    return;
                }
            }
        }

        private void LevelPrevious_Click(object sender, EventArgs e)
        {
            if (LevelList.Items.Count < 1)  // should never happen because buttons & menu items should be greyed out
                return;

            bool MustBeUnsolved = sender == LevelPreviousUnsolved;

            int OldIndex = LevelList.SelectedIndex, i = OldIndex;
            for (; ; )
            {
                i = (i + LevelList.Items.Count - 1) % LevelList.Items.Count;
                if (i == OldIndex || (i == LevelList.Items.Count-1 && OldIndex == -1))
                {
                    DlgMessage.Show("There is no other level in the level file.",
                        MustBeUnsolved ? "Previous unsolved level" : "Previous level",
                        DlgType.Info);
                    return;
                }
                if (LevelList.Items[i] is SokobanLevel &&
                    (!MustBeUnsolved || !FSettings.SolvedLevels.ContainsKey(LevelList.Items[i].ToString())))
                {
                    TakeLevel(i);
                    return;
                }
            }
        }

        /// <summary>
        /// Do next unsolved level if clicking on the Level Solved message.
        /// </summary>
        private void MainArea_Click(object sender, EventArgs e)
        {
            if (MainArea.State == MainAreaState.Solved)
                LevelNext_Click(LevelNextUnsolved, null);
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
        public Hashtable SolvedLevels = new Hashtable();
    }
}
