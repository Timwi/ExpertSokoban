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
        private int EditingIndex;

        // For a bug workaround
        private bool LevelListEverShown;

        public Mainform()
        {
            InitializeComponent();
            FSettings = EasySettings.Get("ExpSok Mainform", new MainFormSettings()) as MainFormSettings;
            OrigLevel = SokobanLevel.TestLevel();
            MainArea.SetLevel(OrigLevel);
            EverMovedOrEdited = false;
            LevelFileChanged = false;
            LevelFilename = null;
            LevelListEverShown = false;

            MainArea.MoveDrawMode = FSettings.MoveDrawMode;
            MainArea.PushDrawMode = FSettings.PushDrawMode;
            MainArea.ShowEndPos = FSettings.ShowEndPos;
            LevelListToolStrip1.Visible = ViewToolStrip1.Checked = FSettings.DisplayToolStrip1;
            LevelListToolStrip2.Visible = ViewToolStrip2.Checked = FSettings.DisplayToolStrip2;
            ViewEditToolStrip.Checked = FSettings.DisplayEditToolStrip;
            LevelListPanel.Width = FSettings.LevelListPanelWidth > 50 ? FSettings.LevelListPanelWidth : 50;
            LevelListVisible(FSettings.DisplayLevelList);
            SetTool(FSettings.LastUsedTool);

            (MainArea.MoveDrawMode == PathDrawMode.Arrows ? ViewMoveArrows :
                MainArea.MoveDrawMode == PathDrawMode.Dots ? ViewMoveDots :
                MainArea.MoveDrawMode == PathDrawMode.Line ? ViewMoveLine : ViewMoveNo).Checked = true;
            (MainArea.PushDrawMode == PathDrawMode.Arrows ? ViewPushArrows :
                MainArea.PushDrawMode == PathDrawMode.Dots ? ViewPushDots :
                MainArea.PushDrawMode == PathDrawMode.Line ? ViewPushLine : ViewPushNo).Checked = true;
            ViewEndPos.Checked = MainArea.ShowEndPos;
        }

        private void MainArea_MoveMade(object sender, EventArgs e)
        {
            EverMovedOrEdited = true;
        }

        // Used only by LevelOpen_Click()
        private enum LevelReaderState { Comment, Empty, Level }

        private void LevelOpen_Click(object sender, EventArgs e)
        {
            if (MayDestroyLevelFile("Open level file"))
            {
                OpenFileDialog OpenDialog = new OpenFileDialog();
                if (OpenDialog.ShowDialog() == DialogResult.OK)
                {
                    LevelListVisible(true);
                    LevelList.BeginUpdate();
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
                    LevelList.EndUpdate();
                    StreamReader.Close();
                    LevelListEverShown = true;
                    LevelFileChanged = false;
                    LevelFilename = OpenDialog.FileName;
                }
            }
        }

        private void TakeLevel() { TakeLevel(LevelList.SelectedIndex); }
        private void TakeLevel(int Index)
        {
            object Item = LevelList.Items[Index];
            if (Item is SokobanLevel && MayDestroyMainAreaLevel("Open level"))
            {
                OrigLevel = (SokobanLevel)Item;
                SokobanLevelStatus Status = OrigLevel.IsValid();
                if (Status == SokobanLevelStatus.Valid)
                {
                    MainArea.SetLevel(OrigLevel);
                    EverMovedOrEdited = false;
                    LevelList.SelectedIndex = Index;
                }
                else
                {
                    String Problem = Status == SokobanLevelStatus.NotEnclosed
                        ? "The level is not completely enclosed by a wall."
                        : "The number of pieces does not match the number of targets.";
                    MessageBox.Show("The level could not be opened because it is invalid.\n\n" + Problem +
                        "\n\nPlease edit the level in order to address this issue, then try again.", "Expert Sokoban");
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
                if (MayDestroyMainAreaLevel("Open level"))
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
            if (MayDestroyLevelFile("New level file"))
            {
                LevelList.Items.Clear();
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

            if (Item is SokobanLevel)
            {
                if (MainArea.State == MainAreaState.Editing && LevelList.SelectedIndex == EditingIndex)
                {
                    DialogResult Result = MessageBox.Show("You are currently editing this level. " +
                        "If you delete this level now, all your modifications will be discarded. " +
                        "Are you sure you wish to do this?", "Delete level", MessageBoxButtons.YesNo);
                    if (Result == DialogResult.No)
                        return;
                }
                else if (MessageBox.Show("Are you sure you wish to delete this level?",
                    "Expert Sokoban", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    return;
            }
            int OldIndex = LevelList.SelectedIndex;
            LevelList.Items.RemoveAt(OldIndex);
            if (LevelList.Items.Count > 0 && OldIndex < LevelList.Items.Count)
                LevelList.SelectedIndex = OldIndex;
            else if (LevelList.Items.Count > 0)
                LevelList.SelectedIndex = LevelList.Items.Count-1;
            if (OldIndex < EditingIndex)
                EditingIndex--;
        }

        private void ViewMove_Click(object sender, EventArgs e)
        {
            ViewMoveNo.Checked = sender == ViewMoveNo;
            ViewMoveLine.Checked = sender == ViewMoveLine;
            ViewMoveDots.Checked = sender == ViewMoveDots;
            ViewMoveArrows.Checked = sender == ViewMoveArrows;

            MainArea.MoveDrawMode =
                sender == ViewMoveLine ? PathDrawMode.Line :
                sender == ViewMoveDots ? PathDrawMode.Dots :
                sender == ViewMoveArrows ? PathDrawMode.Arrows : PathDrawMode.None;
            FSettings.MoveDrawMode = MainArea.MoveDrawMode;
        }

        private void ViewPush_Click(object sender, EventArgs e)
        {
            ViewPushNo.Checked = sender == ViewPushNo;
            ViewPushLine.Checked = sender == ViewPushLine;
            ViewPushDots.Checked = sender == ViewPushDots;
            ViewPushArrows.Checked = sender == ViewPushArrows;

            MainArea.PushDrawMode =
                sender == ViewPushLine ? PathDrawMode.Line :
                sender == ViewPushDots ? PathDrawMode.Dots :
                sender == ViewPushArrows ? PathDrawMode.Arrows : PathDrawMode.None;
            FSettings.PushDrawMode = MainArea.PushDrawMode;
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
            int OldIndex = LevelList.SelectedIndex;
            LevelList.Items.RemoveAt(OldIndex);
            if (LevelList.Items.Count > 0 && OldIndex < LevelList.Items.Count)
                LevelList.SelectedIndex = OldIndex;
            else if (LevelList.Items.Count > 0)
                LevelList.SelectedIndex = LevelList.Items.Count-1;
        }

        private void EditPaste_Click(object sender, EventArgs e)
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

        private void EditEdit_Click(object sender, EventArgs e)
        {
            if (LevelList.SelectedIndex >= 0 &&
                LevelList.Items[LevelList.SelectedIndex] is SokobanLevel &&
                MayDestroyMainAreaLevel("Edit level"))
                EnterEditingMode();
        }

        private void Mainform_FormClosed(object sender, FormClosedEventArgs e)
        {
            EasySettings.Set("ExpSok Mainform", FSettings);
        }

        private void LevelListVisible(bool On)
        {
            FSettings.DisplayLevelList = On;

            // Level list itself
            LevelListPanel.Visible = On;
            LevelListSplitter.Visible = On;
            if (On) LevelList.Focus();
            if (On && !LevelListEverShown)
            {
                LevelList.Items.Add(OrigLevel);
                LevelListEverShown = true;
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
            SetTool(
                sender == EditToolWall || sender == EditWall ? MainAreaTool.Wall :
                sender == EditToolPiece || sender == EditPiece ? MainAreaTool.Piece :
                sender == EditToolTarget || sender == EditTarget ? MainAreaTool.Target :
                MainAreaTool.Sokoban);
        }

        private void SetTool(MainAreaTool NewTool)
        {
            MainArea.Tool = FSettings.LastUsedTool = NewTool;
            EditToolWall.Checked = EditWall.Checked = NewTool == MainAreaTool.Wall;
            EditToolPiece.Checked = EditPiece.Checked = NewTool == MainAreaTool.Piece;
            EditToolTarget.Checked = EditTarget.Checked = NewTool == MainAreaTool.Target;
            EditToolSokoban.Checked = EditSokoban.Checked = NewTool == MainAreaTool.Sokoban;
        }

        private void EditCancel_Click(object sender, EventArgs e)
        {
            LeaveEditingMode();
        }

        private void EditOK_Click(object sender, EventArgs e)
        {
            if (MayDestroyMainAreaLevel("Finish editing"))
            {
                LevelList.Items[EditingIndex] = MainArea.Level.Clone();
                LeaveEditingMode();
            }
        }

        private void EnterEditingMode()
        {
            EditingIndex = LevelList.SelectedIndex;
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

        private void LeaveEditingMode()
        {
            LevelList.SelectedIndex = EditingIndex;
            TakeLevel(EditingIndex);
            SwitchEditingMode(false);
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
