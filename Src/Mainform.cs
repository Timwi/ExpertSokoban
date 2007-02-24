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
        #region Startup / shutdown

        /// <summary>
        /// Constructor. Constructs the main form for the application.
        /// </summary>
        public Mainform()
        {
            InitializeComponent();

            // Restore saved settings
            OptionsPlayToolStrip.Checked = ExpSokSettings.DisplayPlayToolStrip;
            OptionsEditToolStrip.Checked = ExpSokSettings.DisplayEditToolStrip;
            OptionsEditLevelToolStrip.Checked = ExpSokSettings.DisplayEditLevelToolStrip;
            StatusBar.Visible = OptionsStatusBar.Checked = ExpSokSettings.DisplayStatusBar;
            LevelListPanel.Width = ExpSokSettings.LevelListWidth < 50 ? 50 : ExpSokSettings.LevelListWidth;
            MovePathOptions.SetValue(ExpSokSettings.MoveDrawMode);
            PushPathOptions.SetValue(ExpSokSettings.PushDrawMode);
            EditToolOptions.SetValue(ExpSokSettings.LastUsedTool);
            OptionsEndPos.Checked = MainArea.ShowEndPos = ExpSokSettings.ShowEndPos;
            OptionsAreaSokoban.Checked = MainArea.ShowAreaSokoban = ExpSokSettings.ShowAreaSokoban;
            OptionsAreaPiece.Checked = MainArea.ShowAreaPiece = ExpSokSettings.ShowAreaPiece;
            OptionsSound.Checked = MainArea.SoundEnabled = ExpSokSettings.SoundEnabled;
            ShowLevelList(ExpSokSettings.DisplayLevelList);

            if (ExpSokSettings.PlayerName == null || ExpSokSettings.PlayerName.Length == 0)
                ExpSokSettings.PlayerName = InputBox.GetLine("Please choose a name which will be used to identify " +
                    "you in highscore tables.\nYou can change this name later by selecting \"Change player name\" " +
                    "from the \"Level\" menu.", "", "Expert Sokoban");

            // Restore the last used level pack
            try
            {
                LevelList.LoadLevelPack(ExpSokSettings.LevelFilename);
            }
            catch
            {
                LevelList.NewList();
                LevelList.AddLevelListItem(SokobanLevel.TestLevel());
                LevelList.Modified = false;
            }

            LevelList.PlayFirstUnsolved();
            UpdateControls();
        }

        /// <summary>
        /// Intercepts the user's attempt to close the form and asks for confirmation
        /// if the current level has been played or edited, and if the current level
        /// file has been modified.
        /// </summary>
        private void Mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!MayDestroyEverything("Exit Expert Sokoban"))
                e.Cancel = true;
        }

        #endregion

        /// <summary>
        /// Updates various GUI controls to reflect the current program state.
        /// </summary>
        private void UpdateControls()
        {
            bool MainAreaNull = MainArea.State == MainAreaState.Null;
            bool MainAreaPlaying = MainArea.State == MainAreaState.Move || MainArea.State == MainAreaState.Push;
            bool MainAreaEditing = MainArea.State == MainAreaState.Editing;
            bool MainAreaSolved = MainArea.State == MainAreaState.Solved;

            // Caption
            Text = "Expert Sokoban - " +
                    (ExpSokSettings.PlayerName == null || ExpSokSettings.PlayerName.Length == 0
                        ? "(no player name)" : ExpSokSettings.PlayerName) +
                    " - " +
                    (ExpSokSettings.LevelFilename == null ? "(untitled)" : Path.GetFileName(ExpSokSettings.LevelFilename)) +
                    (LevelList.Modified ? " *" : "");

            // Status bar items
            StatusNull.Visible = MainAreaNull;
            StatusMoves.Visible = MainAreaPlaying || MainAreaSolved;
            StatusPushes.Visible = MainAreaPlaying || MainAreaSolved;
            StatusPieces.Visible = MainAreaPlaying;
            StatusEdit.Visible = MainAreaEditing;
            StatusSolved.Visible = MainAreaSolved;

            // Status bar text
            if (MainAreaEditing)
            {
                string Message = "The level is valid.";
                SokobanLevelStatus Status = MainArea.Level.Validity;
                if (Status == SokobanLevelStatus.NotEnclosed)
                    Message = "The level is invalid because it is not completely enclosed by a wall.";
                else if (Status == SokobanLevelStatus.TargetsPiecesMismatch)
                    Message = "The level is invalid because the number of pieces does not match the number of targets.";
                StatusEdit.Text = Message;
            }
            else if (!MainAreaNull)
            {
                StatusMoves.Text = "Moves: " + MainArea.Moves;
                StatusPushes.Text = "Pushes: " + MainArea.Pushes;
                if (!MainAreaSolved)
                    StatusPieces.Text = "Remaining pieces: " + MainArea.Level.RemainingPieces;
            }

            // "Level" menu
            LevelNew.Enabled = true;
            LevelOpen.Enabled = true;
            LevelSave.Enabled = true;
            LevelSaveAs.Enabled = true;

            LevelUndo.Enabled = !MainAreaEditing;
            LevelRedo.Enabled = !MainAreaEditing;
            LevelRetry.Enabled = !MainAreaEditing;

            LevelPrevious.Enabled = !MainAreaEditing && LevelList.Items.Count > 0;
            LevelNext.Enabled = !MainAreaEditing && LevelList.Items.Count > 0;
            LevelPreviousUnsolved.Enabled = !MainAreaEditing && LevelList.Items.Count > 0;
            LevelNextUnsolved.Enabled = !MainAreaEditing && LevelList.Items.Count > 0;

            LevelChangePlayer.Enabled = true;

            // "Edit" menu
            EditCreateLevel.Enabled = !MainAreaEditing;
            EditEdit.Enabled = !MainAreaEditing;
            EditAddComment.Enabled = !MainAreaEditing;

            EditCut.Enabled = LevelList.Visible;
            EditCopy.Enabled = LevelList.Visible;
            EditPaste.Enabled = LevelList.Visible;
            EditDelete.Enabled = LevelList.Visible;

            EditFinish.Enabled = MainAreaEditing;
            EditCancel.Enabled = MainAreaEditing;

            EditWall.Enabled = MainAreaEditing;
            EditPiece.Enabled = MainAreaEditing;
            EditTarget.Enabled = MainAreaEditing;
            EditSokoban.Enabled = MainAreaEditing;

            // "Options" menu
            OptionsLevelList.Enabled = true;
            OptionsPlayToolStrip.Enabled = LevelList.Visible;
            OptionsEditToolStrip.Enabled = LevelList.Visible;
            OptionsEditLevelToolStrip.Enabled = LevelList.Visible && MainAreaEditing;
            OptionsStatusBar.Enabled = true;

            OptionsMoveNo.Enabled = !MainAreaEditing && !MainAreaNull;
            OptionsMoveLine.Enabled = !MainAreaEditing && !MainAreaNull;
            OptionsMoveDots.Enabled = !MainAreaEditing && !MainAreaNull;
            OptionsMoveArrows.Enabled = !MainAreaEditing && !MainAreaNull;
            OptionsPushNo.Enabled = !MainAreaEditing && !MainAreaNull;
            OptionsPushLine.Enabled = !MainAreaEditing && !MainAreaNull;
            OptionsPushDots.Enabled = !MainAreaEditing && !MainAreaNull;
            OptionsPushArrows.Enabled = !MainAreaEditing && !MainAreaNull;

            OptionsEndPos.Enabled = !MainAreaEditing && !MainAreaNull;

            // Level list context menu
            ContextCut.Enabled = LevelList.SelectedLevel != null;
            ContextCopyItem.Enabled = LevelList.SelectedLevel != null;
            ContextDelete.Enabled = LevelList.SelectedLevel != null;
            ContextEdit.Enabled = LevelList.SelectedLevel != null;
            ContextHide.Enabled = LevelList.Visible;
            ContextHighscores.Enabled = LevelList.SelectedLevel != null;
            ContextNewComment.Enabled = LevelList.Visible;
            ContextNewLevel.Enabled = LevelList.Visible;
            ContextPaste.Enabled = LevelList.Visible;
            ContextPlay.Enabled = LevelList.SelectedLevel != null;

            // Toolbar visibility
            PlayToolStrip.Visible = OptionsPlayToolStrip.Checked;
            Edit2ToolStrip.Visible = OptionsEditToolStrip.Checked;
            Edit1ToolStrip.Visible = OptionsEditToolStrip.Checked;
            EditLevelToolStrip.Visible = MainAreaEditing && OptionsEditLevelToolStrip.Checked;

            // Toolbar buttons
            LevelToolNext.Enabled = LevelNext.Enabled;
            LevelToolNextUnsolved.Enabled = LevelNextUnsolved.Enabled;
            LevelToolPrev.Enabled = LevelPrevious.Enabled;
            LevelToolPrevUnsolved.Enabled = LevelPreviousUnsolved.Enabled;
            LevelToolNew.Enabled = LevelNew.Enabled;
            LevelToolOpen.Enabled = LevelOpen.Enabled;
            LevelToolOpen2.Enabled = LevelOpen.Enabled;
            LevelToolSave.Enabled = LevelSave.Enabled;
            LevelToolNewLevel.Enabled = EditCreateLevel.Enabled;
            LevelToolEdit.Enabled = EditEdit.Enabled;
            LevelToolComment.Enabled = EditAddComment.Enabled;
            LevelToolCut.Enabled = EditCut.Enabled;
            LevelToolCopy.Enabled = EditCopy.Enabled;
            LevelToolPaste.Enabled = EditPaste.Enabled;
            LevelToolDelete.Enabled = EditDelete.Enabled;
        }

        /// <summary>
        /// Determines (by asking the user if necessary) whether we are allowed to
        /// destroy the contents of both the main area and the level list.
        /// </summary>
        /// <param name="Caption">Title bar caption to use in case any confirmation
        /// dialogs need to pop up.</param>
        private bool MayDestroyEverything(string Caption)
        {
            return MainArea.MayDestroy(Caption) && LevelList.MayDestroy(Caption);
        }

        /// <summary>
        /// Opens the currently-selected level from the level list and switches into
        /// editing mode.
        /// </summary>
        private void EnterEditingMode()
        {
            LevelList.EditSelectedLevel();
        }

        #region MainArea events

        /// <summary>
        /// Invoked by a click in the Main Area. Normally the Main Area handles all
        /// mouse clicks itself, but if it is in Solved state and the user clicks it,
        /// we want to move to the next unsolved level.
        /// </summary>
        private void MainArea_Click(object sender, EventArgs e)
        {
            if (MainArea.State == MainAreaState.Solved)
                LevelList.PlayNext(true, true);
        }

        /// <summary>
        /// Invoked by the user pressing Space while the MainArea is in Solved state.
        /// This will open the next unsolved level.
        /// </summary>
        private void MainArea_KeyDown(object sender, KeyEventArgs e)
        {
            // IMPORTANT: We can't use Enter here unfortunately.
            // If the user uses the keyboard to play the level, Enter will be
            // the last key they press to place the last piece in the last target.
            // That would already trigger this for the same keypress.
            if (MainArea.State == MainAreaState.Solved && e.KeyCode == Keys.Space)
                LevelList.PlayNext(true, true);
        }

        /// <summary>
        /// Invoked whenever the user solves the level. Remembers the solved level and
        /// tells the level list to update itself.
        /// </summary>
        private void MainArea_LevelSolved(object sender, EventArgs e)
        {
            // Set the LevelListBox to JustSolved state
            LevelList.JustSolved();
            
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

            ExpSokSettings.UpdateHighscore(LevelList.ActiveLevel.ToString(), MainArea.Moves, MainArea.Pushes);

            LevelList.RefreshItems();
        }

        #endregion

        #region LevelList-related code

        /// <summary>
        /// Switches the visibility of the level list. Focuses either the level list
        /// or the main area as appropriate.
        /// </summary>
        /// <param name="Show">True: shows the level list. False: hides it.</param>
        private void ShowLevelList(bool Show)
        {
            ExpSokSettings.DisplayLevelList = Show;

            LevelListPanel.Visible = Show;
            LevelListSplitter.Visible = Show;

            if (Show)
            {
                LevelList.RefreshItems();
                LevelList.Focus();
                LevelList.SelectActiveLevel();
            }
            else
                MainArea.Focus();
        }

        /// <summary>
        /// Called by the level list to verify that it is OK to switch levels.
        /// </summary>
        private void LevelList_LevelActivating(object sender, ConfirmEventArgs e)
        {
            e.ConfirmOK = MainArea.MayDestroy("Open level");
        }

        /// <summary>
        /// Current level changed in the LevelList - so we need to update a few things.
        /// </summary>
        private void LevelList_LevelActivated(object sender, EventArgs e)
        {
            if (LevelList.ActiveLevel == null)
            {
                MainArea.Clear();
                return;
            }

            SokobanLevelStatus Status = LevelList.ActiveLevel.Validity;

            if (Status == SokobanLevelStatus.Valid)
            {
                if (LevelList.State == LevelListBox.LevelListBoxState.Editing)
                    MainArea.SetLevelEdit(LevelList.ActiveLevel);
                else
                    MainArea.SetLevel(LevelList.ActiveLevel);
            }
            else
            {
                string Problem = Status == SokobanLevelStatus.NotEnclosed
                    ? "The level is not completely enclosed by a wall."
                    : "The number of pieces does not match the number of targets.";
                if (DlgMessage.Show("The level could not be opened because it is invalid.\n\n" + Problem +
                    "\n\nYou must edit the level in order to address this issue. " +
                    "Would you like to edit the level now?", "Open level",
                    DlgType.Error, "Edit level", "Cancel") == 0)
                {
                    MainArea.Modified = false;
                    EnterEditingMode();
                }
            }
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
            LevelList.RefreshItems();
        }

        /// <summary>
        /// Invoked if the user presses a key while the level list has focus.
        /// Currently handles the following keys:
        /// - Escape: hides the level list.
        /// </summary>
        private void LevelList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && e.Modifiers == 0)
                ShowLevelList(false);
        }

        /// <summary>
        /// Handle the Play menu item in the context menu
        /// </summary>
        private void ContextPlay_Click(object sender, EventArgs e)
        {
            LevelList.PlaySelected();
        }

        #endregion

        #region "Level" menu

        /// <summary>
        /// Invoked by "Level => New level file" or the relevant toolbar button.
        /// Clears the level list (after asking for confirmation if it has changed).
        /// </summary>
        private void LevelNew_Click(object sender, EventArgs e)
        {
            if (!MayDestroyEverything("New level file"))
                return;

            // Show the level list if it isn't already visible
            if (!LevelListPanel.Visible)
                ShowLevelList(true);

            LevelList.NewList();
        }

        /// <summary>
        /// Invoked by "Level => Open level file" or the relevant toolbar button.
        /// Allows the user to load a text file containing Sokoban levels.
        /// </summary>
        private void LevelOpen_Click(object sender, EventArgs e)
        {
            if (!MayDestroyEverything("Open level file"))
                return;

            OpenFileDialog OpenDialog = new OpenFileDialog();
            if (OpenDialog.ShowDialog() != DialogResult.OK)
                return;

            MainArea.Modified = false;
            ShowLevelList(true);
            LevelList.LoadLevelPack(OpenDialog.FileName);
            LevelList.PlayFirstUnsolved();
        }

        /// <summary>
        /// Invoked by "Level => Save level file" or the relevant toolbar button.
        /// Saves the level pack to a file, using the existing filename if it exists,
        /// or popping up a Save As dialog if it doesn't.
        /// </summary>
        private void LevelSave_Click(object sender, EventArgs e)
        {
            LevelList.SaveWithDialog(false);
        }

        /// <summary>
        /// Invoked by "Level => Save level file as" or the relevant toolbar button.
        /// Asks the user where to save the level pack, then saves it there.
        /// </summary>
        private void LevelSaveAs_Click(object sender, EventArgs e)
        {
            LevelList.SaveWithDialog(true);
        }

        /// <summary>
        /// Invoked by "Level => Undo move". Undoes the last move.
        /// </summary>
        private void LevelUndo_Click(object sender, EventArgs e)
        {
            MainArea.Undo();
        }

        /// <summary>
        /// Invoked by "Level => Redo move". Redoes the last undone move.
        /// </summary>
        private void LevelRedo_Click(object sender, EventArgs e)
        {
            MainArea.Redo();
        }

        /// <summary>
        /// Invoked by "Level => Retry level". Resets the level to its original state.
        /// Asks for confirmation first if the current level has been played or edited.
        /// </summary>
        private void LevelRetry_Click(object sender, EventArgs e)
        {
            if (!MainArea.MayDestroy("Retry level"))
                return;

            MainArea.SetLevel(LevelList.ActiveLevel);
        }

        /// <summary>
        /// Finds the previous level in the level list and opens it for playing.
        /// </summary>
        private void LevelPrevious_Click(object sender, EventArgs e)
        {
            LevelList.PlayPrev(false);
        }

        /// <summary>
        /// Finds the next level in the level list and opens it for playing.
        /// </summary>
        private void LevelNext_Click(object sender, EventArgs e)
        {
            LevelList.PlayNext(false, false);
        }

        /// <summary>
        /// Finds the previous unsolved level in the level list and opens it for playing.
        /// </summary>
        private void LevelPreviousUnsolved_Click(object sender, EventArgs e)
        {
            LevelList.PlayPrev(true);
        }

        /// <summary>
        /// Finds the next unsolved level in the level list and opens it for playing.
        /// </summary>
        private void LevelNextUnsolved_Click(object sender, EventArgs e)
        {
            LevelList.PlayNext(true, false);
        }

        /// <summary>
        /// Invoked by "Level => Change player name". Allows the user to change the
        /// name used to identify them in highscore lists.
        /// </summary>
        private void LevelChangePlayer_Click(object sender, EventArgs e)
        {
            string Result = InputBox.GetLine("Please choose a name which will be used to identify " +
                "you in highscore tables.", ExpSokSettings.PlayerName, "Expert Sokoban");
            if (Result == null)
                return;

            ExpSokSettings.PlayerName = Result;
            LevelList.RefreshItems();
        }

        /// <summary>
        /// Invoked by "Level => Exit". Shuts down Expert Sokoban.
        /// </summary>
        private void LevelExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region "Edit" menu

        /// <summary>
        /// Invoked by "Edit => Create new level" or the relevant toolbar button.
        /// Adds the trivial level as a new level to the level list.
        /// </summary>
        private void EditCreateLevel_Click(object sender, EventArgs e)
        {
            // Show the level list if it isn't already visible
            if (!LevelListPanel.Visible)
                ShowLevelList(true);

            LevelList.AddLevelListItem(SokobanLevel.TrivialLevel());
        }

        /// <summary>
        /// Invoked by "Edit => Edit level". Switches the application into edit mode
        /// and lets the user edit the level currently selected in the level list.
        /// </summary>
        private void EditEdit_Click(object sender, EventArgs e)
        {
            if (LevelList.SelectedIndex >= 0 &&
                LevelList.Items[LevelList.SelectedIndex] is SokobanLevel)
            {
                EnterEditingMode();
            }
        }

        /// <summary>
        /// Invoked by "Edit => Add a comment" or the relevant toolbar button.
        /// Allows the user to add a comment to the level list.
        /// </summary>
        private void EditAddComment_Click(object sender, EventArgs e)
        {
            if (!LevelListPanel.Visible)
                OptionsLevelList_Click(sender, e);
            string Comment = InputBox.GetLine("Please enter a comment:");
            if (Comment != null)
                LevelList.AddLevelListItem(Comment);
        }

        /// <summary>
        /// Invoked by "Edit => Cut" or the relevant toolbar button.
        /// Cuts the selected level list item into the clipboard.
        /// </summary>
        private void EditCut_Click(object sender, EventArgs e)
        {
            if (!LevelList.MayDeleteSelectedItem(false))
                return;
            if (LevelList.SelectedLevelActive())
                MainArea.Clear();
            EditCopy_Click(sender, e);
            LevelList.RemoveLevelListItem();
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
        /// Invoked by "Edit => Paste" or the relevant toolbar button.
        /// Pastes any items from the clipboard into the level list.
        /// </summary>
        private void EditPaste_Click(object sender, EventArgs e)
        {
            if (!LevelListPanel.Visible || !Clipboard.ContainsData("SokobanData"))
                return;

            LevelList.AddLevelListItem(Clipboard.GetData("SokobanData"));
        }

        /// <summary>
        /// Invoked by "Edit => Delete". Deletes the selected level or comment from the
        /// level list.
        /// </summary>
        private void EditDelete_Click(object sender, EventArgs e)
        {
            if (!LevelList.MayDeleteSelectedItem(true))
                return;
            if (LevelList.SelectedLevelActive())
                MainArea.Clear();
            LevelList.RemoveLevelListItem();
        }

        /// <summary>
        /// Invoked by "Edit => Finish editing" or the relevant toolbar button.
        /// Leaves editing mode and saves the modified level in the level list,
        /// then returns to playing mode.
        /// </summary>
        private void EditFinish_Click(object sender, EventArgs e)
        {
            if (LevelList.ActiveLevel == null || LevelList.State != LevelListBox.LevelListBoxState.Editing) // this should never happen
                Ut.InternalError();

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
            
            // LevelList.EditAccept() will trigger LevelList_LevelActivating(), which in
            // turn will ask the user if they want to discard their changes to the level.
            // Since we don't want this, we have to set the Modified flag for the MainArea
            // to false before calling LevelList.EditAccept().
            MainArea.Modified = false;
            
            LevelList.EditAccept(Level);
        }

        /// <summary>
        /// Invoked by "Edit => Cancel editing" or the relevant toolbar button.
        /// Cancels editing and returns to playing mode.
        /// </summary>
        private void EditCancel_Click(object sender, EventArgs e)
        {
            if (LevelList.ActiveLevel == null || LevelList.State != LevelListBox.LevelListBoxState.Editing) // this should never happen
                Ut.InternalError();

            LevelList.EditCancel();
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

        #endregion

        #region "Options" menu

        /// <summary>
        /// Invoked by "Options => Display level list" or the LevelListClosePanel.
        /// Shows or hides the level list.
        /// </summary>
        private void OptionsLevelList_Click(object sender, EventArgs e)
        {
            ShowLevelList(!LevelListPanel.Visible);
        }

        /// <summary>
        /// Invoked by "Options => Display playing toolbar". Shows/hides the playing toolbar.
        /// </summary>
        private void OptionsPlayToolStrip_Click(object sender, EventArgs e)
        {
            OptionsPlayToolStrip.Checked = !OptionsPlayToolStrip.Checked;
            Edit1ToolStrip.Visible = ExpSokSettings.DisplayPlayToolStrip = OptionsPlayToolStrip.Checked;
        }

        /// <summary>
        /// Invoked by "Options => Display editing toolbars (level pack)". Shows/hides the edit toolbar.
        /// </summary>
        private void OptionsEditToolStrip_Click(object sender, EventArgs e)
        {
            OptionsEditToolStrip.Checked = !OptionsEditToolStrip.Checked;
            EditLevelToolStrip.Visible = ExpSokSettings.DisplayEditToolStrip = OptionsEditToolStrip.Checked;
        }

        /// <summary>
        /// Invoked by "Options => Display editing toolbar (level)". Shows/hides the operations
        /// toolbar.
        /// </summary>
        private void OptionsEditLevelToolStrip_Click(object sender, EventArgs e)
        {
            OptionsEditLevelToolStrip.Checked = !OptionsEditLevelToolStrip.Checked;
            Edit2ToolStrip.Visible = ExpSokSettings.DisplayEditLevelToolStrip = OptionsEditLevelToolStrip.Checked;
        }

        /// <summary>
        /// Invoked by "Options => Display status bar". Toggles the visibility of the
        /// status bar at the bottom of the window.
        /// </summary>
        private void OptionsStatusBar_Click(object sender, EventArgs e)
        {
            OptionsStatusBar.Checked = !OptionsStatusBar.Checked;
            StatusBar.Visible = ExpSokSettings.DisplayStatusBar = OptionsStatusBar.Checked;
        }

        /// <summary>
        /// Invoked (indirectly, via MovePathOptions) by any of the "Display move path
        /// as..." menu items under "Options". Sets the move path settings.
        /// </summary>
        private void MovePathOptions_ValueChanged(object sender, EventArgs e)
        {
            MainArea.MoveDrawMode = ExpSokSettings.MoveDrawMode = MovePathOptions.Value;
        }

        /// <summary>
        /// Invoked (indirectly, via PushPathOptions) by any of the "Display push path
        /// as..." menu items under "Options". Sets the push path settings.
        /// </summary>
        private void PushPathOptions_ValueChanged(object sender, EventArgs e)
        {
            MainArea.PushDrawMode = ExpSokSettings.PushDrawMode = PushPathOptions.Value;
        }

        /// <summary>
        /// Invoked by "Options => Display end position of Sokoban and piece".
        /// Sets the option.
        /// </summary>
        private void OptionsEndPos_Click(object sender, EventArgs e)
        {
            OptionsEndPos.Checked = !OptionsEndPos.Checked;
            MainArea.ShowEndPos = ExpSokSettings.ShowEndPos = OptionsEndPos.Checked;
        }

        /// <summary>
        /// Invoked by "Help => Keyboard shortcuts". Displays a message box outlining
        /// the keyboard shortcuts that are not directly documented in the menus.
        /// </summary>
        private void HelpKeyboard_Click(object sender, EventArgs e)
        {
            string helpfile = Ut.AppPath + "ExpSok.chm";
            if (File.Exists(helpfile))
                Help.ShowHelp(this, helpfile, HelpNavigator.TopicId, "30");
            else
                DlgMessage.ShowWarning("Help file not found. Attempted to open from:\n" + helpfile, null);
        }

        /// <summary>
        /// Invoked by "Help => About". Pops up the About box.
        /// </summary>
        private void HelpAbout_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        #endregion

        #region Other events

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
        /// Ensures all UI controls are up-to-date all the time.
        /// </summary>
        private void UpdateControlsTimer_Tick(object sender, EventArgs e)
        {
            UpdateControls();
        }

        #endregion

        private void LevelHighscores_Click(object sender, EventArgs e)
        {
            if (LevelList.SelectedLevel != null)
            {
                string l = LevelList.SelectedLevel.ToString();
                if (!ExpSokSettings.Highscores.ContainsKey(l))
                    DlgMessage.ShowInfo("The selected level does not have any highscores associated with it yet.",
                        "No highscores for this level");
                else
                {
                    HighscoresForm hsf = new HighscoresForm();
                    hsf.SetContents(ExpSokSettings.Highscores[l], LevelList.SelectedLevel);
                    hsf.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Invoked by "Options => Display reachable area for Sokoban".
        /// Sets the option.
        /// </summary>
        private void OptionsAreaSokoban_Click(object sender, EventArgs e)
        {
            OptionsAreaSokoban.Checked = !OptionsAreaSokoban.Checked;
            MainArea.ShowAreaSokoban = ExpSokSettings.ShowAreaSokoban = OptionsAreaSokoban.Checked;
        }

        /// <summary>
        /// Invoked by "Options => Display reachable area for piece".
        /// Sets the option.
        /// </summary>
        private void OptionsAreaPiece_Click(object sender, EventArgs e)
        {
            OptionsAreaPiece.Checked = !OptionsAreaPiece.Checked;
            MainArea.ShowAreaPiece = ExpSokSettings.ShowAreaPiece = OptionsAreaPiece.Checked;
        }

        /// <summary>
        /// Invoked by "Options => Enable sound". Sets the option.
        /// </summary>
        private void OptionsSound_Click(object sender, EventArgs e)
        {
            OptionsSound.Checked = !OptionsSound.Checked;
            MainArea.SoundEnabled = ExpSokSettings.SoundEnabled = OptionsSound.Checked;
        }
    }
}
