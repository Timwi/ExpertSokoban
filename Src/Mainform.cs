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
    /// <summary>
    /// Encapsulates the main form of the application. Apart from the menus and
    /// toolbars, the two most important components are LevelList (which is of type
    /// LevelListBox) and MainArea (which is of type MainArea).
    /// </summary>
    public partial class Mainform : ManagedForm
    {
        /// <summary>
        /// While playing the game, determines whether any move has been made. While
        /// editing a level, determines if any change has been made to the level.
        /// </summary>
        private bool EverMovedOrEdited;

        /// <summary>
        /// A copy of the level the user is currently playing, saving the state it was
        /// in at the beginning. This is used to restart the level, and to add it to
        /// FSettings.SolvedLevels once the player solves it.
        /// </summary>
        private SokobanLevel OrigLevel;

        /// <summary>
        /// Determines whether any changes have been made to the level file (i.e. the
        /// contents of the level list).
        /// </summary>
        private bool FLevelFileChanged;

        /// <summary>
        /// Whether the current level file has been changed. Setting this property
        /// automatically updates the window title bar.
        /// </summary>
        private bool LevelFileChanged
        {
            get { return FLevelFileChanged; }
            set
            {
                FLevelFileChanged = value;
                UpdateControls();
            }
        }

        /// <summary>
        /// Constructor. Constructs the main form for the application.
        /// </summary>
        public Mainform()
        {
            InitializeComponent();

            // Restore the last used level pack
            try
            {
                int? LevelFound = LevelList.LoadLevelPack(ExpSokSettings.LevelFilename);
                if (LevelFound != null)
                    TakeLevel(LevelFound.Value, true);
            }
            catch
            {
                // Default level
                ExpSokSettings.LevelFilename = null;
                OrigLevel = SokobanLevel.TestLevel();
                MainArea.SetLevel(OrigLevel);
                LevelList.Items.Add(OrigLevel);
                LevelList.SelectedIndex = 0;
                LevelList.PlayingIndex = 0;
            }

            EverMovedOrEdited = false;
            LevelFileChanged = false;

            // Restore saved settings
            LevelListToolStrip1.Visible = ViewToolStrip1.Checked = ExpSokSettings.DisplayToolStrip1;
            LevelListToolStrip2.Visible = ViewToolStrip2.Checked = ExpSokSettings.DisplayToolStrip2;
            ViewEditToolStrip.Checked = ExpSokSettings.DisplayEditToolStrip;
            StatusBar.Visible = ViewStatusBar.Checked = ExpSokSettings.DisplayStatusBar;
            LevelListPanel.Width = ExpSokSettings.LevelListWidth < 50 ? 50 : ExpSokSettings.LevelListWidth;
            LevelListVisible(ExpSokSettings.DisplayLevelList);
            MovePathOptions.SetValue(ExpSokSettings.MoveDrawMode);
            PushPathOptions.SetValue(ExpSokSettings.PushDrawMode);
            EditToolOptions.SetValue(ExpSokSettings.LastUsedTool);
            ViewEndPos.Checked = MainArea.ShowEndPos = ExpSokSettings.ShowEndPos;

            if (ExpSokSettings.PlayerName == null || ExpSokSettings.PlayerName.Length == 0)
                ExpSokSettings.PlayerName = InputBox.GetLine("Please choose a name which will be used to identify " +
                    "you in highscore tables.\nYou can change this name later by selecting \"Change player name\" " +
                    "from the \"Level\" menu.", "", "Expert Sokoban");

            UpdateControls();
        }

        /// <summary>
        /// Invoked whenever the player makes a move (i.e. pushes a piece).
        /// </summary>
        private void MainArea_MoveMade(object sender, EventArgs e)
        {
            EverMovedOrEdited = true;
            UpdateControls();
        }

        /// <summary>
        /// Updates various GUI controls to reflect the current program state.
        /// </summary>
        private void UpdateControls()
        {
            Text = "Expert Sokoban - " +
                    (ExpSokSettings.PlayerName == null || ExpSokSettings.PlayerName.Length == 0
                        ? "(no player name)" : ExpSokSettings.PlayerName) +
                    " - " +
                    (ExpSokSettings.LevelFilename == null ? "(untitled)" : Path.GetFileName(ExpSokSettings.LevelFilename)) +
                    (LevelFileChanged ? " *" : "");

            StatusNull.Visible = MainArea.State == MainAreaState.Null;
            StatusMoves.Visible = MainArea.State == MainAreaState.Move || MainArea.State == MainAreaState.Push || MainArea.State == MainAreaState.Solved;
            StatusPushes.Visible = MainArea.State == MainAreaState.Move || MainArea.State == MainAreaState.Push || MainArea.State == MainAreaState.Solved;
            StatusPieces.Visible = MainArea.State == MainAreaState.Move || MainArea.State == MainAreaState.Push;
            StatusEdit.Visible = MainArea.State == MainAreaState.Editing;
            StatusSolved.Visible = MainArea.State == MainAreaState.Solved;

            if (MainArea.State == MainAreaState.Editing)
            {
                string Message = "The level is valid.";
                SokobanLevelStatus Status = MainArea.Level.Validity;
                if (Status == SokobanLevelStatus.NotEnclosed)
                    Message = "The level is invalid because it is not completely enclosed by a wall.";
                else if (Status == SokobanLevelStatus.TargetsPiecesMismatch)
                    Message = "The level is invalid because the number of pieces does not match the number of targets.";
                StatusEdit.Text = Message;
            }

            if (MainArea.State == MainAreaState.Move || MainArea.State == MainAreaState.Push || MainArea.State == MainAreaState.Solved)
            {
                StatusMoves.Text = "Moves: " + MainArea.Moves;
                StatusPushes.Text = "Pushes: " + MainArea.Pushes;
                StatusPieces.Text = "Remaining pieces: " + MainArea.Level.RemainingPieces;
            }
        }

        /// <summary>
        /// Invoked by "Level => Open level file". Allows the user to open a text file
        /// containing Sokoban levels.
        /// </summary>
        private void LevelOpen_Click(object sender, EventArgs e)
        {
            if (!MayDestroy("Open level file"))
                return;

            OpenFileDialog OpenDialog = new OpenFileDialog();
            if (OpenDialog.ShowDialog() != DialogResult.OK)
                return;

            int? FoundLevel;

            LevelListVisible(true);
            FoundLevel = LevelList.LoadLevelPack(OpenDialog.FileName);
            LevelFileChanged = false;
            ExpSokSettings.LevelFilename = OpenDialog.FileName;

            if (FoundLevel != null)
                // Found a valid level (preferably unsolved): display it and let the player play it.
                TakeLevel(FoundLevel.Value, true);
            else
            {
                // Otherwise, clear the main area.
                MainArea.Clear();
                LevelList.PlayingIndex = null;
            }
        }

        /// <summary>
        /// Opens the currently-selected level for playing. The user is asked for
        /// confirmation if they are currently playing or editing a level. The user
        /// is also alerted if the level is invalid. If the currently-selected item
        /// in the list is not a level, nothing happens.
        /// </summary>
        private void TakeLevel()
        {
            TakeLevel(LevelList.SelectedIndex, false);
        }

        /// <summary>
        /// Opens the specified level in the level list for playing. The user is asked
        /// for confirmation if they are currently playing or editing a level. The user
        /// is also alerted if the level is invalid.
        /// </summary>
        /// <param name="Index">Index in the level list that identifies the level.
        /// If the specified item is not a level, nothing happens.</param>
        private void TakeLevel(int Index)
        {
            TakeLevel(Index, false);
        }

        /// <summary>
        /// Opens the specified level in the level list for playing.
        /// </summary>
        /// <param name="Index">Index in the level list that identifies the level.
        /// If the specified item is not a level, nothing happens.</param>
        /// <param name="Override">If false, the user is asked for confirmation if they
        /// are currently playing or editing a level. The user is also alerted if the
        /// level is invalid. If true, no confirmations or alerts will pop up; if the
        /// level is invalid, the main area will be cleared.</param>
        private void TakeLevel(int Index, bool Override)
        {
            object Item = LevelList.Items[Index];
            if (!(Item is SokobanLevel))
                return;
            if (!Override && !MayDestroyMainAreaLevel("Open level"))
                return;

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
                UpdateControls();
            }
            else
            {
                LevelList.PlayingIndex = null;
                MainArea.Clear();
                UpdateControls();
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

        /// <summary>
        /// Determines (by asking the user if necessary) whether we are allowed to
        /// destroy the contents of both the main area and the level list.
        /// </summary>
        /// <param name="Title">Title bar caption to use in case any confirmation
        /// dialogs need to pop up.</param>
        private bool MayDestroy(string Title)
        {
            return MayDestroyMainAreaLevel(Title) && MayDestroyLevelFile(Title);
        }

        /// <summary>
        /// Determines (by asking the user if necessary) whether we are allowed to
        /// destroy the contents of the level list.
        /// </summary>
        /// <param name="Title">Title bar caption to use in case any confirmation
        /// dialogs need to pop up.</param>
        private bool MayDestroyLevelFile(string Title)
        {
            // If no changes have been made, we're definitely allowed.
            if (!LevelFileChanged)
                return true;

            // Ask the user if they want to save their changes to the level life.
            int Result = DlgMessage.Show("You have made changes to "
                + (ExpSokSettings.LevelFilename == null ? "(untitled)" : Path.GetFileName(ExpSokSettings.LevelFilename)) +
                ". Would you like to save those changes?", Title, DlgType.Question,
                "Save changes", "&Discard changes", "Cancel");

            // If they said "Cancel", bail out immediately.
            if (Result == 2)
                return false;

            // If they said "Save changes", call SaveWithDialog(). If they cancel that
            // dialog, bail out.
            if (Result == 0)
            {
                if (SaveWithDialog() != DialogResult.OK)
                    return false;
            }

            // In all other cases (file was saved, or "Discard changes" was clicked),
            // we're allowed.
            return true;
        }

        /// <summary>
        /// Determines (by asking the user if necessary) whether we are allowed to
        /// destroy the contents of the main area.
        /// </summary>
        /// <param name="Title">Title bar caption to use in case any confirmation
        /// dialogs need to pop up.</param>
        private bool MayDestroyMainAreaLevel(string Title)
        {
            // If we're not playing or editing, we're definitely allowed.
            if (MainArea.State == MainAreaState.Solved ||
                MainArea.State == MainAreaState.Null ||
                !EverMovedOrEdited)
                return true;

            // Ask the user the appropriate question.
            return MainArea.State == MainAreaState.Editing
                ? DlgMessage.Show("Are you sure you wish to discard your changes to the level you're editing?",
                    Title, DlgType.Warning, "Discard changes", "Cancel") == 0
                : DlgMessage.Show("Are you sure you wish to give up the current level?",
                    Title, DlgType.Warning, "Give up", "Cancel") == 0;
        }

        /// <summary>
        /// Invoked if the user double-clicks in the level list. If the selected item
        /// is a comment, edits the comment. Otherwise opens the level for playing.
        /// </summary>
        private void LevelList_DoubleClick(object sender, EventArgs e)
        {
            // Should never happen, but just be safe
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

        /// <summary>
        /// Invoked by "View => Display level list" or the LevelListClosePanel.
        /// Shows or hides the level list.
        /// </summary>
        private void ViewLevelList_Click(object sender, EventArgs e)
        {
            LevelListVisible(!LevelListPanel.Visible);
        }

        /// <summary>
        /// Invoked by "Level => Undo move". Undoes the last move.
        /// </summary>
        private void LevelUndo_Click(object sender, EventArgs e)
        {
            MainArea.Undo();
            UpdateControls();
        }

        /// <summary>
        /// Invoked by "Level => Retry level". Resets the level to its original state.
        /// Asks for confirmation first if the current level has been played or edited.
        /// </summary>
        private void LevelRetry_Click(object sender, EventArgs e)
        {
            if (MayDestroyMainAreaLevel("Retry level"))
            {
                MainArea.SetLevel(OrigLevel);
                EverMovedOrEdited = false;
            }
        }

        /// <summary>
        /// Invoked by "Level => Exit". Shuts down Expert Sokoban.
        /// </summary>
        private void LevelExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Invoked by "Edit => Add a comment" or the relevant toolbar button.
        /// Allows the user to add a comment to the level list.
        /// </summary>
        private void EditAddComment_Click(object sender, EventArgs e)
        {
            if (!LevelListPanel.Visible)
                ViewLevelList_Click(sender, e);
            string Comment = InputBox.GetLine("Please enter a comment:");
            if (Comment != null)
                AddLevelListItem(Comment);
        }

        /// <summary>
        /// Invoked by "Level => New level file" or the relevant toolbar button.
        /// Clears the level list (after asking for confirmation if it has changed).
        /// </summary>
        private void LevelNew_Click(object sender, EventArgs e)
        {
            // Show the level list if it isn't already visible
            if (!LevelListPanel.Visible)
                ViewLevelList_Click(sender, e);

            if (MayDestroy("New level file"))
            {
                LevelList.Items.Clear();
                LevelList.PlayingIndex = null;
                MainArea.Clear();
                ExpSokSettings.LevelFilename = null;
            }
        }

        /// <summary>
        /// Invoked by "Edit => Create new level" or the relevant toolbar button.
        /// Adds the trivial level as a new level to the level list.
        /// </summary>
        private void EditCreateLevel_Click(object sender, EventArgs e)
        {
            // Show the level list if it isn't already visible
            if (!LevelListPanel.Visible)
                ViewLevelList_Click(sender, e);

            AddLevelListItem(SokobanLevel.TrivialLevel());
        }

        /// <summary>
        /// Determines whether we are allowed to delete the level from the level list
        /// that is currently selected. If the user is currently playing the level, a
        /// confirmation message asks whether they want to give up. If they are
        /// currently editing it, a message asks whether they want to discard it. In
        /// both of those cases, the main area is cleared.
        /// </summary>
        /// <param name="NormalConfirmation">If true, a confirmation message will also
        /// appear if the user is not currently playing or editing the selected level.
        /// </param>
        /// <returns>True if allowed.</returns>
        private bool CanDeleteLevel(bool NormalConfirmation)
        {
            if (!LevelListPanel.Visible || LevelList.SelectedIndex < 0)
                return false;

            object Item = LevelList.Items[LevelList.SelectedIndex];

            // Confirmation message if user is currently editing the selected level
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
            // Confirmation message if user is currently playing the selected level
            else if (Item is SokobanLevel && LevelList.SelectedIndex == LevelList.PlayingIndex)
            {
                if (DlgMessage.Show("You are currently playing this level.\n\n" +
                    "Are you sure you wish to give up?", "Delete level",
                    DlgType.Warning, "Give up", "Cancel") == 1)
                    return false;
                MainArea.Clear();
                LevelList.PlayingIndex = null;
            }
            // Confirmation message if neither of the two cases apply
            else if (Item is SokobanLevel)
            {
                if (NormalConfirmation && DlgMessage.Show(
                    "Are you sure you wish to delete this level?",
                    "Delete level", DlgType.Question, "Delete level", "Cancel") == 1)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Invoked by "Edit => Delete". Deletes the selected level or comment from the
        /// level list.
        /// </summary>
        private void EditDelete_Click(object sender, EventArgs e)
        {
            if (CanDeleteLevel(true))
                RemoveLevelListItem(LevelList.SelectedIndex);
        }

        /// <summary>
        /// Deletes the specified item from the level list, while ensuring that the
        /// level list's EditingIndex/PlayingIndex remain intact. If this makes the
        /// level list empty, several menu items and toolbar buttons are disabled.
        /// </summary>
        /// <param name="Index"></param>
        private void RemoveLevelListItem(int Index)
        {
            // Remove the item
            LevelList.Items.RemoveAt(Index);

            // Restore the value of SelectedIndex (why does RemoveAt have to destroy this?)
            if (LevelList.Items.Count > 0 && Index < LevelList.Items.Count)
                LevelList.SelectedIndex = Index;
            else if (LevelList.Items.Count > 0)
                LevelList.SelectedIndex = LevelList.Items.Count-1;

            // Fix the values of EditingIndex and PlayingIndex
            // (ideally, the LevelListBox class should take care of this, but I couldn't
            // find a way to listen for things like Items.RemoveAt())
            if (LevelList.EditingIndex != null && LevelList.EditingIndex.Value > Index)
                LevelList.EditingIndex = LevelList.EditingIndex.Value-1;
            if (LevelList.PlayingIndex != null && LevelList.PlayingIndex.Value > Index)
                LevelList.PlayingIndex = LevelList.PlayingIndex.Value-1;

            // The level file has changed
            LevelFileChanged = true;

            // Set the enabled state of several menu items and toolbar buttons
            LevelNext.Enabled = LevelNextUnsolved.Enabled =
                LevelToolNext.Enabled = LevelToolNextUnsolved.Enabled =
                LevelPrevious.Enabled = LevelPreviousUnsolved.Enabled =
                LevelList.Items.Count > 0;
        }

        /// <summary>
        /// Invoked (indirectly, via MovePathOptions) by any of the "Display move path
        /// as..." menu items under "View". Sets the move path settings.
        /// </summary>
        private void MovePathOptions_ValueChanged(object sender, EventArgs e)
        {
            MainArea.MoveDrawMode = ExpSokSettings.MoveDrawMode = MovePathOptions.Value;
        }

        /// <summary>
        /// Invoked (indirectly, via PushPathOptions) by any of the "Display push path
        /// as..." menu items under "View". Sets the push path settings.
        /// </summary>
        private void PushPathOptions_ValueChanged(object sender, EventArgs e)
        {
            MainArea.PushDrawMode = ExpSokSettings.PushDrawMode = PushPathOptions.Value;
        }

        /// <summary>
        /// Invoked by "View => Display end position of Sokoban and piece".
        /// Sets the option.
        /// </summary>
        private void ViewEndPos_Click(object sender, EventArgs e)
        {
            ViewEndPos.Checked = !ViewEndPos.Checked;
            MainArea.ShowEndPos = ExpSokSettings.ShowEndPos = ViewEndPos.Checked;
        }

        /// <summary>
        /// Intercepts the user's attempt to close the form and asks for confirmation
        /// if the current level has been played or edited, and if the current level
        /// file has been modified.
        /// </summary>
        private void Mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!MayDestroy("Exit Expert Sokoban"))
                e.Cancel = true;
        }

        /// <summary>
        /// Invoked by "Edit => Copy" or the relevant toolbar button.
        /// Copies the selected level list item to the clipboard.
        /// </summary>
        private void EditCopy_Click(object sender, EventArgs e)
        {
            if (!LevelListPanel.Visible || LevelList.SelectedIndex < 0)
                return;
            Clipboard.SetData("SokobanData", LevelList.Items[LevelList.SelectedIndex]);
        }

        /// <summary>
        /// Invoked by "Edit => Cut" or the relevant toolbar button.
        /// Cuts the selected level list item into the clipboard.
        /// </summary>
        private void EditCut_Click(object sender, EventArgs e)
        {
            if (CanDeleteLevel(false))
            {
                EditCopy_Click(sender, e);
                RemoveLevelListItem(LevelList.SelectedIndex);
            }
        }

        /// <summary>
        /// Invoked by "Edit => Paste" or the relevant toolbar button.
        /// Pastes any items from the clipboard into the level list.
        /// </summary>
        private void EditPaste_Click(object sender, EventArgs e)
        {
            if (LevelListPanel.Visible && Clipboard.ContainsData("SokobanData"))
                AddLevelListItem(Clipboard.GetData("SokobanData"));
        }

        /// <summary>
        /// Adds the specified item to the level list, while ensuring that the level
        /// list's EditingIndex/PlayingIndex remain intact. If the level list was
        /// previously empty, several menu items and toolbar buttons become enabled.
        /// </summary>
        /// <param name="NewItem">The item to insert. May be a SokobanLevel object
        /// or a string (representing a comment).</param>
        private void AddLevelListItem(object NewItem)
        {
            if (LevelList.SelectedIndex < 0)
            {
                // If nothing is currently selected, add the item at the bottom
                // and then select it
                LevelList.Items.Add(NewItem);
                LevelList.SelectedIndex = LevelList.Items.Count-1;
            }
            else
            {
                // Otherwise, insert the item before the current item and select it
                LevelList.Items.Insert(LevelList.SelectedIndex, NewItem);
                LevelList.SelectedIndex -= 1;

                // Fix the values of EditingIndex and PlayingIndex
                // (ideally, the LevelListBox class should take care of this, but I
                // couldn't find a way to listen for things like Items.Insert())
                if (LevelList.EditingIndex != null && LevelList.EditingIndex.Value >= LevelList.SelectedIndex)
                    LevelList.EditingIndex = LevelList.EditingIndex.Value+1;
                if (LevelList.PlayingIndex != null && LevelList.PlayingIndex.Value >= LevelList.SelectedIndex)
                    LevelList.PlayingIndex = LevelList.PlayingIndex.Value+1;
            }

            // The level file has changed
            LevelFileChanged = true;

            // Enable all the relevant menu items and toolbar buttons
            LevelNext.Enabled = LevelNextUnsolved.Enabled =
                LevelToolNext.Enabled = LevelToolNextUnsolved.Enabled =
                LevelPrevious.Enabled = LevelPreviousUnsolved.Enabled = true;
        }

        /// <summary>
        /// Invoked by "Edit => Edit level". Switches the application into edit move
        /// and lets the user edit the level currently selected in the level list.
        /// </summary>
        private void EditEdit_Click(object sender, EventArgs e)
        {
            if (LevelList.SelectedIndex >= 0 &&
                LevelList.Items[LevelList.SelectedIndex] is SokobanLevel &&
                MayDestroyMainAreaLevel("Edit level"))
                EnterEditingMode();
        }

        /// <summary>
        /// Switches the visibility of the level list.
        /// </summary>
        /// <param name="On">True: shows the level list. False: hides it.</param>
        private void LevelListVisible(bool On)
        {
            ExpSokSettings.DisplayLevelList = On;

            // Level list itself
            LevelListPanel.Visible = On;
            LevelListSplitter.Visible = On;
            if (On)
            {
                LevelList.ComeOn_RefreshItems();
                LevelList.Focus();
            }
            else
                MainArea.Focus();
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
            ViewLevelList.Checked = On;
            ViewToolStrip1.Enabled = On;
            ViewToolStrip2.Enabled = On;

            // "Level" menu
            LevelSave.Enabled = On;
        }

        /// <summary>
        /// Invoked by the toolbar buttons for the Wall/Piece/Target/Sokoban edit tool.
        /// Switches the tool and sets the relevant menu item.
        /// </summary>
        private void EditTool_Click(object sender, EventArgs e)
        {
            EditToolOptions.SetValue(
                sender == EditToolWall ? MainAreaTool.Wall :
                sender == EditToolPiece ? MainAreaTool.Piece :
                sender == EditToolTarget ? MainAreaTool.Target : MainAreaTool.Sokoban);
        }

        /// <summary>
        /// Invoked by "Edit => Cancel editing" or the relevant toolbar button.
        /// Cancels editing and returns to playing mode.
        /// </summary>
        private void EditCancel_Click(object sender, EventArgs e)
        {
            if (LevelList.EditingIndex != null) // this should always be true
                TakeLevel(LevelList.EditingIndex.Value);
        }

        /// <summary>
        /// Invoked by "Edit => Finish editing" or the relevant toolbar button.
        /// Leaves editing mode and saves the modified level in the level list,
        /// then returns to playing mode.
        /// </summary>
        private void EditOK_Click(object sender, EventArgs e)
        {
            if (LevelList.EditingIndex == null) // this should never happen
                return;

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

        /// <summary>
        /// Opens the currently-selected level from the level list and switches into
        /// editing mode.
        /// </summary>
        private void EnterEditingMode()
        {
            LevelList.EditingIndex = LevelList.SelectedIndex;
            MainArea.SetLevelEdit(LevelList.Items[LevelList.SelectedIndex] as SokobanLevel);
            EverMovedOrEdited = false;
            SwitchEditingMode(true);
        }

        /// <summary>
        /// Sets all the relevant menu items and toolbar buttons to enabled/disabled for
        /// a switch between editing mode and playing mode.
        /// </summary>
        /// <param name="On">True: switches to editing mode. False: switches to playing
        /// mode.</param>
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

            UpdateControls();
        }

        /// <summary>
        /// Invoked by "Level => Save level file". See SaveWithDialog().
        /// </summary>
        private void LevelSave_Click(object sender, EventArgs e)
        {
            SaveWithDialog();
        }

        /// <summary>
        /// Saves the current contents of the level list to a text file. If no filename
        /// has been specified yet, pops up a Save dialog asking the user for one.
        /// </summary>
        /// <returns>DialogResult.OK if the file was saved, otherwise the return value
        /// from the Save dialog.</returns>
        private DialogResult SaveWithDialog()
        {
            // If we don't already have a filename, ask the user for one
            if (ExpSokSettings.LevelFilename == null)
            {
                SaveFileDialog SaveDialog = new SaveFileDialog();
                DialogResult Result = SaveDialog.ShowDialog();

                // If the user cancelled the dialog, bail out
                if (Result != DialogResult.OK)
                    return Result;

                // Update the current filename (this updates the window titlebar too)
                ExpSokSettings.LevelFilename = SaveDialog.FileName;
            }

            // Save the file
            StreamWriter StreamWriter = new StreamWriter(ExpSokSettings.LevelFilename, false, Encoding.UTF8);
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

            // File is now saved, so mark it as unchanged
            LevelFileChanged = false;

            return DialogResult.OK;
        }

        /// <summary>
        /// Invoked by "View => Display file toolbar". Shows/hides the file toolbar.
        /// </summary>
        private void ViewToolStrip1_Click(object sender, EventArgs e)
        {
            ViewToolStrip1.Checked = !ViewToolStrip1.Checked;
            LevelListToolStrip1.Visible = ExpSokSettings.DisplayToolStrip1 = ViewToolStrip1.Checked;
        }

        /// <summary>
        /// Invoked by "View => Display operations toolbar". Shows/hides the operations
        /// toolbar.
        /// </summary>
        private void ViewToolStrip2_Click(object sender, EventArgs e)
        {
            ViewToolStrip2.Checked = !ViewToolStrip2.Checked;
            LevelListToolStrip2.Visible = ExpSokSettings.DisplayToolStrip2 = ViewToolStrip2.Checked;
        }

        /// <summary>
        /// Invoked by "View => Display edit toolbar". Shows/hides the edit toolbar.
        /// </summary>
        private void ViewEditToolStrip_Click(object sender, EventArgs e)
        {
            ViewEditToolStrip.Checked = !ViewEditToolStrip.Checked;
            EditToolStrip.Visible = ExpSokSettings.DisplayEditToolStrip = ViewEditToolStrip.Checked;
        }

        /// <summary>
        /// Invoked whenever the user resizes the level list by dragging the splitter.
        /// Saves the level list width as a setting.
        /// </summary>
        private void LevelListPanel_Resize(object sender, EventArgs e)
        {
            ExpSokSettings.LevelListWidth = LevelListPanel.Width;
        }

        /// <summary>
        /// Invoked whenever the user makes any change to the level while editing it.
        /// Remembers that the level was modified.
        /// </summary>
        private void MainArea_LevelChanged(object sender, EventArgs e)
        {
            EverMovedOrEdited = true;
            UpdateControls();
        }

        /// <summary>
        /// This method is used to work around a bug in .NET's ListBox.
        /// Basically, if you add items to the ListBox while it is invisible,
        /// it screws up and sometimes crashes (some time later). During the
        /// form's initialisation, setting the ListBox's Visible to true
        /// doesn't help. Fortunately, calling RefreshItems() on the ListBox
        /// retrofixes the bug's effects, so we set a timer to do that.
        /// </summary>
        private void BugWorkaroundTimer_Tick(object sender, EventArgs e)
        {
            BugWorkaroundTimer.Enabled = false;
            LevelList.ComeOn_RefreshItems();
        }

        /// <summary>
        /// Invoked by any of the "Edit => Wall/Piece/Target/Sokoban tool" menu items.
        /// Switches to the selected tool.
        /// </summary>
        private void EditToolOptions_ValueChanged(object sender, EventArgs e)
        {
            MainArea.Tool = ExpSokSettings.LastUsedTool = EditToolOptions.Value;
            EditToolWall.Checked = EditToolOptions.Value == MainAreaTool.Wall;
            EditToolPiece.Checked = EditToolOptions.Value == MainAreaTool.Piece;
            EditToolTarget.Checked = EditToolOptions.Value == MainAreaTool.Target;
            EditToolSokoban.Checked = EditToolOptions.Value == MainAreaTool.Sokoban;
        }

        /// <summary>
        /// Invoked by "Help => About". Pops up the About box.
        /// </summary>
        private void HelpAbout_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }

        /// <summary>
        /// Invoked by "Help => Online Help". Pops up a dialog inviting the user to
        /// visit our website.
        /// </summary>
        private void HelpHelp_Click(object sender, EventArgs e)
        {
            DlgMessage.Show("Welcome to Expert Sokoban. You can " +
                "find detailed help about this product on our website:\n\n" +
                "http://www.cutebits.com", "Expert Sokoban", Properties.Resources.ExpertSokoban.ToBitmap());
        }

        /// <summary>
        /// Invoked whenever the user solves the level. Remembers the solved level and
        /// tells the level list to update itself.
        /// </summary>
        private void MainArea_LevelSolved(object sender, EventArgs e)
        {
            // If the user hasn't chosen a name for themselves yet, ask them
            if (ExpSokSettings.PlayerName == null || ExpSokSettings.PlayerName.Length == 0)
                ExpSokSettings.PlayerName = InputBox.GetLine("Congratulations! You've solved the current level.\n" +
                    "Please choose a name which will be used to identify you in highscore tables.\n" +
                    "If you do not choose a name now, your score for this level will not be recorded.\n" +
                    "You can change this name again later by selecting \"Change player name\" " +
                    "from the \"Level\" menu.", "", "Expert Sokoban");

            // If they still haven't chosen a name, discard the high score
            if (ExpSokSettings.PlayerName == null || ExpSokSettings.PlayerName.Length == 0)
                return;

            ExpSokSettings.UpdateHighscore(OrigLevel.ToString(), MainArea.Moves, MainArea.Pushes);

            LevelList.ComeOn_RefreshItems();
            UpdateControls();
        }

        /// <summary>
        /// Invoked by "Level => Next level", "Level => Next unsolved level" or any of
        /// the corresponding toolbar buttons. Finds the next level or next unsolved
        /// level in the level list and opens it for playing.
        /// </summary>
        /// <param name="e">The normal event parameter. If this is set to null, a
        /// congratulations message is displayed if there are no unsolved levels
        /// left.</param>
        private void LevelNext_Click(object sender, EventArgs e)
        {
            if (LevelList.Items.Count < 1)  // should never happen because buttons & menu items should be greyed out
                return;

            bool MustBeUnsolved = sender == LevelNextUnsolved || sender == LevelToolNextUnsolved;

            int OldIndex = LevelList.SelectedIndex, i = OldIndex;
            for (; ; )
            {
                i = (i+1) % LevelList.Items.Count;
                // Special case: if OldIndex is -1, we need to stop searching when we
                // reach the top of the list.
                if (i == OldIndex || (i == 0 && OldIndex == -1))
                {
                    if (e == null)
                        DlgMessage.ShowInfo("You have solved all levels in this level pack!", "Congratulations!");
                    else if (MustBeUnsolved)
                        DlgMessage.ShowInfo("There are no more unsolved levels in this level file.", "Next unsolved level");
                    else
                        DlgMessage.ShowInfo("There is no other level in the level file.", "Next level");
                    return;
                }
                if (LevelList.Items[i] is SokobanLevel &&
                    (!MustBeUnsolved || !ExpSokSettings.IsSolved(LevelList.Items[i].ToString())))
                {
                    // We've found a matching level
                    TakeLevel(i);
                    return;
                }
            }
        }

        /// <summary>
        /// Invoked by "Level => Previous level" or "Level => Previous unsolved level".
        /// Finds the previous level or previous unsolved level in the level list and
        /// opens it for playing.
        /// </summary>
        private void LevelPrevious_Click(object sender, EventArgs e)
        {
            if (LevelList.Items.Count < 1)  // should never happen because buttons & menu items should be greyed out
                return;

            bool MustBeUnsolved = sender == LevelPreviousUnsolved;

            int OldIndex = LevelList.SelectedIndex, i = OldIndex;
            for (; ; )
            {
                i = (i + LevelList.Items.Count - 1) % LevelList.Items.Count;
                // Special case: if OldIndex is -1, we need to stop searching when we
                // reach the bottom of the list.
                if (i == OldIndex || (i == LevelList.Items.Count-1 && OldIndex == -1))
                {
                    DlgMessage.ShowInfo(
                        MustBeUnsolved ? "No unsolved level found." : "There is no other level in the level file.",
                        MustBeUnsolved ? "Previous unsolved level" : "Previous level");
                    return;
                }
                if (LevelList.Items[i] is SokobanLevel &&
                    (!MustBeUnsolved || !ExpSokSettings.IsSolved(LevelList.Items[i].ToString())))
                {
                    // We've found a matching level
                    TakeLevel(i);
                    return;
                }
            }
        }

        /// <summary>
        /// Invoked by a click in the Main Area. Normally the Main Area handles all
        /// mouse clicks itself, but if it is in Solved state and the user clicks it,
        /// we want to move to the next unsolved level.
        /// </summary>
        private void MainArea_Click(object sender, EventArgs e)
        {
            if (MainArea.State == MainAreaState.Solved)
                LevelNext_Click(LevelNextUnsolved, null);
        }

        /// <summary>
        /// Invoked by "View => Display status bar". Toggles the visibility of the
        /// status bar at the bottom of the window.
        /// </summary>
        private void ViewStatusBar_Click(object sender, EventArgs e)
        {
            ViewStatusBar.Checked = !ViewStatusBar.Checked;
            StatusBar.Visible = ExpSokSettings.DisplayStatusBar = ViewStatusBar.Checked;
        }

        /// <summary>
        /// Invoked by "Level => Change player name". Allows the user to change the
        /// name used to identify them in highscore lists.
        /// </summary>
        private void LevelChangePlayer_Click(object sender, EventArgs e)
        {
            string Result = InputBox.GetLine("Please choose a name which will be used to identify " +
                "you in highscore tables.", ExpSokSettings.PlayerName, "Expert Sokoban");
            if (Result != null)
            {
                ExpSokSettings.PlayerName = Result;
                LevelList.ComeOn_RefreshItems();
                UpdateControls();
            }
        }

        /// <summary>
        /// Invoked by the user pressing Enter while the MainArea is in Solved state.
        /// This will open the next unsolved level.
        /// </summary>
        private void MainArea_EnterPressedWhileSolved(object sender, EventArgs e)
        {
            LevelNext_Click(LevelNextUnsolved, null);
        }

        /// <summary>
        /// Invoked if the user presses a key while the level list has focus.
        /// Currently handles the following keys:
        /// - Enter: opens the currently-selected level or edits the
        ///   currently-selected comment (equivalent to mouse double-click).
        /// - Escape: hides the level list.
        /// </summary>
        private void LevelList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift || e.Control || e.Alt)
                return;

            if (e.KeyCode == Keys.Enter)
                LevelList_DoubleClick(sender, new EventArgs());
            else if (e.KeyCode == Keys.Escape)
                LevelListVisible(false);
        }

        private void LevelList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                LevelList.SelectedIndex = LevelList.IndexFromPoint(e.Location);
                LevelList.Focus();
            }
        }
    }
}
