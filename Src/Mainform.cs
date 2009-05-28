using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RT.Util;
using RT.Util.Dialogs;
using RT.Util.ExtensionMethods;
using RT.Util.Forms;

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
            Lingo.TranslateControl(this, Program.Translation);
            mnuOptionsChangeLanguage.DropDownItems.AddRange(RT.Util.Lingo.LanguageToolStripMenuItems<Translation>("ExpSok.*.xml", @"^ExpSok\.(.*)\.xml$", t => t.ThisLanguage, (t, m) =>
            {
                ExpSokSettings.Language = m == null ? null : m.Groups[1].Value;
                Program.Translation = t;
                Lingo.TranslateControl(this, Program.Translation);
                lstLevels.RefreshItems();
            }).ToArray());

            // Restore saved settings
            mnuOptionsPlayingToolbar.Checked = ExpSokSettings.DisplayPlayingToolbar;
            mnuOptionsFileToolbars.Checked = ExpSokSettings.DisplayFileToolbars;
            mnuOptionsEditLevelToolbar.Checked = ExpSokSettings.DisplayEditLevelToolbar;
            ctStatusBar.Visible = mnuOptionsStatusBar.Checked = ExpSokSettings.DisplayStatusBar;
            pnlLevelList.Width = ExpSokSettings.LevelListWidth < 50 ? 50 : ExpSokSettings.LevelListWidth;
            grpMovePathOptions.SetValue(ExpSokSettings.MoveDrawMode);
            grpPushPathOptions.SetValue(ExpSokSettings.PushDrawMode);
            grpEditTool.SetValue(ExpSokSettings.LastUsedTool);
            mnuOptionsEndPos.Checked = ctMainArea.ShowEndPos = ExpSokSettings.ShowEndPos;
            mnuOptionsAreaSokoban.Checked = ctMainArea.ShowAreaSokoban = ExpSokSettings.ShowAreaSokoban;
            mnuOptionsAreaPiece.Checked = ctMainArea.ShowAreaPiece = ExpSokSettings.ShowAreaPiece;
            mnuOptionsSound.Checked = ctMainArea.SoundEnabled = ExpSokSettings.SoundEnabled;
            showLevelList(ExpSokSettings.DisplayLevelList);

            if (ExpSokSettings.PlayerName == null || ExpSokSettings.PlayerName.Length == 0)
                ExpSokSettings.PlayerName = InputBox.GetLine(Program.Translation.Mainform_ChooseName_FirstRun, "", Program.Translation.ProgramName);

            // Restore the last used level pack
            try
            {
                lstLevels.LoadLevelPack(ExpSokSettings.LevelFilename);
            }
            catch
            {
                lstLevels.NewList();
                lstLevels.AddLevelListItem(SokobanLevel.TestLevel());
                lstLevels.Modified = false;
            }

            lstLevels.PlayFirstUnsolved();
            updateControls();
        }

        /// <summary>
        /// Intercepts the user's attempt to close the form and asks for confirmation
        /// if the current level has been played or edited, and if the current level
        /// file has been modified.
        /// </summary>
        private void formClosing(object sender, FormClosingEventArgs e)
        {
            if (!mayDestroyEverything(Program.Translation.Mainform_MessageTitle_Exit))
                e.Cancel = true;
        }

        #endregion

        /// <summary>
        /// Updates various GUI controls to reflect the current program state.
        /// </summary>
        private void updateControls()
        {
            bool MainAreaNull = ctMainArea.State == MainAreaState.Null;
            bool MainAreaPlaying = ctMainArea.State == MainAreaState.Move || ctMainArea.State == MainAreaState.Push;
            bool MainAreaEditing = ctMainArea.State == MainAreaState.Editing;
            bool MainAreaSolved = ctMainArea.State == MainAreaState.Solved;

            // Caption
            Text = Program.Translation.ProgramName + " � " +
                    (ExpSokSettings.PlayerName == null || ExpSokSettings.PlayerName.Length == 0
                        ? Program.Translation.PlayerNameMissing : ExpSokSettings.PlayerName) +
                    " � " +
                    (ExpSokSettings.LevelFilename == null ? Program.Translation.FileName_Untitled : Path.GetFileName(ExpSokSettings.LevelFilename)) +
                    (lstLevels.Modified ? " �" : "");

            // Status bar text
            if (MainAreaEditing)
            {
                string Message = Program.Translation.Mainform_Validity_Valid;
                SokobanLevelStatus Status = ctMainArea.Level.Validity;
                if (Status == SokobanLevelStatus.NotEnclosed)
                    Message = Program.Translation.Mainform_Validity_NotEnclosed;
                else if (Status == SokobanLevelStatus.TargetsPiecesMismatch)
                    Message = Program.Translation.Mainform_Validity_WrongNumber;
                lblStatusEdit.Text = Message;
            }
            else if (!MainAreaNull)
            {
                lblStatusMoves.Text = Program.Translation.Mainform_Status_Moves.Fmt(ctMainArea.Moves);
                lblStatusPushes.Text = Program.Translation.Mainform_Status_Pushes.Fmt(ctMainArea.Pushes);
                if (!MainAreaSolved)
                    lblStatusPieces.Text = Program.Translation.Mainform_Status_PiecesRemaining.Fmt(ctMainArea.Level.RemainingPieces);
            }

            // Status bar items
            lblStatusNull.Visible = MainAreaNull;
            lblStatusMoves.Visible = MainAreaPlaying || MainAreaSolved;
            lblStatusPushes.Visible = MainAreaPlaying || MainAreaSolved;
            lblStatusPieces.Visible = MainAreaPlaying;
            lblStatusEdit.Visible = MainAreaEditing;
            lblStatusSolved.Visible = MainAreaSolved;

            // "Level" menu
            mnuLevelNew.Enabled = true;
            mnuLevelOpen.Enabled = true;
            mnuLevelSave.Enabled = true;
            mnuLevelSaveAs.Enabled = true;

            mnuLevelUndo.Enabled = !MainAreaEditing && !MainAreaNull;
            mnuLevelRedo.Enabled = !MainAreaEditing && !MainAreaNull;
            mnuLevelRetry.Enabled = !MainAreaEditing && !MainAreaNull;
            mnuLevelHighscores.Enabled = lstLevels.ActiveLevel != null;

            mnuLevelPrevious.Enabled = !MainAreaEditing && lstLevels.Items.Count > 0;
            mnuLevelNext.Enabled = !MainAreaEditing && lstLevels.Items.Count > 0;
            mnuLevelPreviousUnsolved.Enabled = !MainAreaEditing && lstLevels.Items.Count > 0;
            mnuLevelNextUnsolved.Enabled = !MainAreaEditing && lstLevels.Items.Count > 0;

            mnuLevelChangePlayer.Enabled = true;

            // "Edit" menu
            mnuEditCreateLevel.Enabled = !MainAreaEditing;
            mnuEditEditLevel.Enabled = !MainAreaEditing;
            mnuEditAddComment.Enabled = !MainAreaEditing;

            mnuEditCut.Enabled = lstLevels.Visible;
            mnuEditCopy.Enabled = lstLevels.Visible;
            mnuEditPaste.Enabled = lstLevels.Visible;
            mnuEditDelete.Enabled = lstLevels.Visible;

            mnuEditFinish.Enabled = MainAreaEditing;
            mnuEditCancel.Enabled = MainAreaEditing;

            mnuEditWall.Enabled = MainAreaEditing;
            mnuEditPiece.Enabled = MainAreaEditing;
            mnuEditTarget.Enabled = MainAreaEditing;
            mnuEditSokoban.Enabled = MainAreaEditing;

            // "Options" menu
            mnuOptionsLevelList.Enabled = true;
            mnuOptionsPlayingToolbar.Enabled = lstLevels.Visible;
            mnuOptionsFileToolbars.Enabled = lstLevels.Visible;
            mnuOptionsEditLevelToolbar.Enabled = lstLevels.Visible && MainAreaEditing;
            mnuOptionsStatusBar.Enabled = true;

            mnuOptionsMoveNo.Enabled = !MainAreaEditing && !MainAreaNull;
            mnuOptionsMoveLine.Enabled = !MainAreaEditing && !MainAreaNull;
            mnuOptionsMoveDots.Enabled = !MainAreaEditing && !MainAreaNull;
            mnuOptionsMoveArrows.Enabled = !MainAreaEditing && !MainAreaNull;
            mnuOptionsPushNo.Enabled = !MainAreaEditing && !MainAreaNull;
            mnuOptionsPushLine.Enabled = !MainAreaEditing && !MainAreaNull;
            mnuOptionsPushDots.Enabled = !MainAreaEditing && !MainAreaNull;
            mnuOptionsPushArrows.Enabled = !MainAreaEditing && !MainAreaNull;

            mnuOptionsEndPos.Enabled = !MainAreaEditing && !MainAreaNull;

            // Level list context menu
            mnuContextPlay.Enabled = lstLevels.SelectedLevel != null;
            mnuContextEdit.Enabled = lstLevels.SelectedLevel != null;
            mnuContextHighscores.Enabled = lstLevels.SelectedLevel != null;
            mnuContextNewLevel.Enabled = lstLevels.Visible;
            mnuContextNewComment.Enabled = lstLevels.Visible;
            mnuContextCut.Enabled = lstLevels.SelectedLevel != null;
            mnuContextCopy.Enabled = lstLevels.SelectedLevel != null;
            mnuContextPaste.Enabled = lstLevels.Visible;
            mnuContextDelete.Enabled = lstLevels.SelectedIndex >= 0;
            mnuContextHide.Enabled = lstLevels.Visible;

            // Toolbar visibility
            toolPlay.Visible = mnuOptionsPlayingToolbar.Checked;
            toolFileEdit.Visible = mnuOptionsFileToolbars.Checked;
            toolFile.Visible = mnuOptionsFileToolbars.Checked;
            toolEditLevel.Visible = MainAreaEditing && mnuOptionsEditLevelToolbar.Checked;

            // Toolbar buttons
            btnPlayNextLevel.Enabled = mnuLevelNext.Enabled;
            btnPlayNextUnsolvedLevel.Enabled = mnuLevelNextUnsolved.Enabled;
            btnPlayPrevLevel.Enabled = mnuLevelPrevious.Enabled;
            btnPlayPrevUnsolvedLevel.Enabled = mnuLevelPreviousUnsolved.Enabled;
            btnFileNew.Enabled = mnuLevelNew.Enabled;
            btnFileOpen.Enabled = mnuLevelOpen.Enabled;
            btnPlayOpenLevel.Enabled = mnuLevelOpen.Enabled;
            btnFileSave.Enabled = mnuLevelSave.Enabled;
            btnFileEditNewLevel.Enabled = mnuEditCreateLevel.Enabled;
            btnFileEditEditLevel.Enabled = mnuEditEditLevel.Enabled;
            btnFileEditAddComment.Enabled = mnuEditAddComment.Enabled;
            btnFileCut.Enabled = mnuEditCut.Enabled;
            btnFileCopy.Enabled = mnuEditCopy.Enabled;
            btnFilePaste.Enabled = mnuEditPaste.Enabled;
            btnFileEditDeleteLevel.Enabled = mnuEditDelete.Enabled;
        }

        /// <summary>
        /// Determines (by asking the user if necessary) whether we are allowed to
        /// destroy the contents of both the main area and the level list.
        /// </summary>
        /// <param name="Caption">Title bar caption to use in case any confirmation
        /// dialogs need to pop up.</param>
        private bool mayDestroyEverything(string Caption)
        {
            return ctMainArea.MayDestroy(Caption) && lstLevels.MayDestroy(Caption);
        }

        /// <summary>
        /// Opens the currently-selected level from the level list and switches into
        /// editing mode.
        /// </summary>
        private void enterEditingMode()
        {
            lstLevels.EditSelectedLevel();
        }

        #region MainArea events

        /// <summary>
        /// Invoked by a click in the Main Area. Normally the Main Area handles all
        /// mouse clicks itself, but if it is in Solved state and the user clicks it,
        /// we want to move to the next unsolved level.
        /// </summary>
        private void mainAreaClick(object sender, EventArgs e)
        {
            if (ctMainArea.State == MainAreaState.Solved)
                lstLevels.PlayNext(true, true);
        }

        /// <summary>
        /// Invoked by the user pressing Space while the MainArea is in Solved state.
        /// This will open the next unsolved level.
        /// </summary>
        private void mainAreaKeyDown(object sender, KeyEventArgs e)
        {
            // IMPORTANT: We can't use Enter here unfortunately.
            // If the user uses the keyboard to play the level, Enter will be
            // the last key they press to place the last piece in the last target.
            // That would already trigger this for the same keypress.
            if (ctMainArea.State == MainAreaState.Solved && e.KeyCode == Keys.Space)
                lstLevels.PlayNext(true, true);
        }

        /// <summary>
        /// Invoked whenever the user solves the level. Remembers the solved level and
        /// tells the level list to update itself.
        /// </summary>
        private void levelSolved(object sender, EventArgs e)
        {
            // Set the LevelListBox to JustSolved state
            lstLevels.JustSolved();

            // If the user hasn't chosen a name for themselves yet, ask them
            if (ExpSokSettings.PlayerName == null || ExpSokSettings.PlayerName.Length == 0)
                ExpSokSettings.PlayerName = InputBox.GetLine(Program.Translation.Mainform_ChooseName_SolvedLevel, "", Program.Translation.ProgramName);

            // If they still haven't chosen a name, discard the high score
            if (ExpSokSettings.PlayerName == null || ExpSokSettings.PlayerName.Length == 0)
                return;

            ExpSokSettings.UpdateHighscore(lstLevels.ActiveLevel.ToString(), ctMainArea.Moves, ctMainArea.Pushes);

            lstLevels.RefreshItems();
        }

        #endregion

        #region LevelList-related code

        /// <summary>
        /// Switches the visibility of the level list. Focuses either the level list
        /// or the main area as appropriate.
        /// </summary>
        /// <param name="Show">True: shows the level list. False: hides it.</param>
        private void showLevelList(bool Show)
        {
            ExpSokSettings.DisplayLevelList = Show;

            pnlLevelList.Visible = Show;
            ctLevelListSplitter.Visible = Show;
            mnuOptionsLevelList.Checked = Show;

            if (Show)
            {
                lstLevels.RefreshItems();
                lstLevels.Focus();
                lstLevels.SelectActiveLevel();
            }
            else
                ctMainArea.Focus();
        }

        /// <summary>
        /// Called by the level list to verify that it is OK to switch levels.
        /// </summary>
        private void levelActivating(object sender, ConfirmEventArgs e)
        {
            e.ConfirmOK = ctMainArea.MayDestroy(Program.Translation.Mainform_MessageTitle_OpenLevel);
        }

        /// <summary>
        /// Current level changed in the LevelList - so we need to update a few things.
        /// </summary>
        private void levelActivated(object sender, EventArgs e)
        {
            if (lstLevels.ActiveLevel == null)
            {
                ctMainArea.Clear();
                return;
            }

            if (lstLevels.State == LevelListBox.LevelListBoxState.Editing)
                ctMainArea.SetLevelEdit(lstLevels.ActiveLevel);
            else
                ctMainArea.SetLevel(lstLevels.ActiveLevel);
        }

        /// <summary>
        /// Invoked whenever the user resizes the level list by dragging the splitter.
        /// Saves the level list width as a setting.
        /// </summary>
        private void levelListPanelResize(object sender, EventArgs e)
        {
            ExpSokSettings.LevelListWidth = pnlLevelList.Width;
        }

        /// <summary>
        /// This method is used to work around a bug in .NET's ListBox.
        /// Basically, if you add items to the ListBox while it is invisible,
        /// it screws up and sometimes crashes (some time later). During the
        /// form's initialisation, setting the ListBox's Visible to true
        /// doesn't help. Fortunately, calling RefreshItems() on the ListBox
        /// retrofixes the bug's effects, so we set a timer to do that.
        /// </summary>
        private void bugWorkaround(object sender, EventArgs e)
        {
            tmrBugWorkaround.Enabled = false;
            lstLevels.RefreshItems();
        }

        /// <summary>
        /// Invoked if the user presses a key while the level list has focus.
        /// Currently handles the following keys:
        /// - Escape: hides the level list.
        /// </summary>
        private void levelListKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && e.Modifiers == 0)
                showLevelList(false);
        }

        /// <summary>
        /// Handle the Play menu item in the context menu
        /// </summary>
        private void playLevel(object sender, EventArgs e)
        {
            lstLevels.PlaySelected();
        }

        #endregion

        #region "Level" menu

        /// <summary>
        /// Invoked by "Level => New level file" or the relevant toolbar button.
        /// Clears the level list (after asking for confirmation if it has changed).
        /// </summary>
        private void newLevel(object sender, EventArgs e)
        {
            if (!mayDestroyEverything(Program.Translation.Mainform_MessageTitle_NewLevelFile))
                return;

            // Show the level list if it isn't already visible
            if (!pnlLevelList.Visible)
                showLevelList(true);

            lstLevels.NewList();
        }

        /// <summary>
        /// Invoked by "Level => Open level file" or the relevant toolbar button.
        /// Allows the user to load a text file containing Sokoban levels.
        /// </summary>
        private void openLevel(object sender, EventArgs e)
        {
            if (!mayDestroyEverything(Program.Translation.Mainform_MessageTitle_OpenLevelFile))
                return;

            OpenFileDialog OpenDialog = new OpenFileDialog();
            OpenDialog.DefaultExt = "txt";
            OpenDialog.Filter = Program.Translation.Save_FileType_TextFiles + "|*.txt|" + Program.Translation.Save_FileType_AllFiles + "|*.*";
            OpenDialog.InitialDirectory = ExpSokSettings.LastOpenSaveDirectory;
            if (OpenDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                lstLevels.LoadLevelPack(OpenDialog.FileName);
            }
            catch (LevelListBox.InvalidLevelException)
            {
                DlgMessage.ShowError(Program.Translation.Mainform_InvalidFile, Program.Translation.Mainform_InvalidFile_Title);
                return;
            }

            ExpSokSettings.LastOpenSaveDirectory = Path.GetDirectoryName(OpenDialog.FileName);
            ctMainArea.Modified = false;
            showLevelList(true);
            lstLevels.PlayFirstUnsolved();
        }

        /// <summary>
        /// Invoked by "Level => Save level file" or the relevant toolbar button.
        /// Saves the level pack to a file, using the existing filename if it exists,
        /// or popping up a Save As dialog if it doesn't.
        /// </summary>
        private void saveLevel(object sender, EventArgs e)
        {
            lstLevels.SaveWithDialog(false);
        }

        /// <summary>
        /// Invoked by "Level => Save level file as" or the relevant toolbar button.
        /// Asks the user where to save the level pack, then saves it there.
        /// </summary>
        private void saveLevelAs(object sender, EventArgs e)
        {
            lstLevels.SaveWithDialog(true);
        }

        /// <summary>
        /// Invoked by "Level => Undo move". Undoes the last move.
        /// </summary>
        private void undo(object sender, EventArgs e)
        {
            ctMainArea.Undo();
        }

        /// <summary>
        /// Invoked by "Level => Redo move". Redoes the last undone move.
        /// </summary>
        private void redo(object sender, EventArgs e)
        {
            ctMainArea.Redo();
        }

        /// <summary>
        /// Invoked by "Level => Retry level". Resets the level to its original state.
        /// Asks for confirmation first if the current level has been played or edited.
        /// </summary>
        private void retryLevel(object sender, EventArgs e)
        {
            if (!ctMainArea.MayDestroy(Program.Translation.Mainform_MessageTitle_RetryLevel))
                return;

            if (lstLevels.ActiveLevel != null && ctMainArea.State != MainAreaState.Null)
                ctMainArea.SetLevel(lstLevels.ActiveLevel);
        }

        /// <summary>
        /// Finds the previous level in the level list and opens it for playing.
        /// </summary>
        private void prevLevel(object sender, EventArgs e)
        {
            lstLevels.PlayPrev(false);
        }

        /// <summary>
        /// Finds the next level in the level list and opens it for playing.
        /// </summary>
        private void nextLevel(object sender, EventArgs e)
        {
            lstLevels.PlayNext(false, false);
        }

        /// <summary>
        /// Finds the previous unsolved level in the level list and opens it for playing.
        /// </summary>
        private void prevUnsolvedLevel(object sender, EventArgs e)
        {
            lstLevels.PlayPrev(true);
        }

        /// <summary>
        /// Finds the next unsolved level in the level list and opens it for playing.
        /// </summary>
        private void nextUnsolvedLevel(object sender, EventArgs e)
        {
            lstLevels.PlayNext(true, false);
        }

        /// <summary>
        /// Invoked by "Level => Change player name". Allows the user to change the
        /// name used to identify them in highscore lists.
        /// </summary>
        private void changePlayer(object sender, EventArgs e)
        {
            string Result = InputBox.GetLine(Program.Translation.Mainform_ChooseName, ExpSokSettings.PlayerName, Program.Translation.ProgramName);
            if (Result == null)
                return;

            ExpSokSettings.PlayerName = Result;
            lstLevels.RefreshItems();
        }

        /// <summary>
        /// Invoked by "Level => Show highscores". Displays the highscores for the
        /// currently selected level.
        /// </summary>
        private void showHighscores(object sender, EventArgs e)
        {
            if (lstLevels.SelectedLevel != null)
            {
                string l = lstLevels.SelectedLevel.ToString();
                if (!ExpSokSettings.Highscores.ContainsKey(l))
                    DlgMessage.ShowInfo(Program.Translation.Mainform_NoHighscores, Program.Translation.Mainform_NoHighscores_Title);
                else
                {
                    HighscoresForm hsf = new HighscoresForm();
                    hsf.SetContents(ExpSokSettings.Highscores[l], lstLevels.SelectedLevel);
                    hsf.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Invoked by "Level => Exit". Shuts down Expert Sokoban.
        /// </summary>
        private void exit(object sender, EventArgs e)
        {
            // Can't use Application.Exit() because of a bug in .NET 3.0.
            Close();
        }

        #endregion

        #region "Edit" menu

        /// <summary>
        /// Invoked by "Edit => Create new level" or the relevant toolbar button.
        /// Adds the trivial level as a new level to the level list.
        /// </summary>
        private void createLevel(object sender, EventArgs e)
        {
            // Show the level list if it isn't already visible
            if (!pnlLevelList.Visible)
                showLevelList(true);

            lstLevels.AddLevelListItem(SokobanLevel.TrivialLevel());
        }

        /// <summary>
        /// Invoked by "Edit => Edit level". Switches the application into edit mode
        /// and lets the user edit the level currently selected in the level list.
        /// </summary>
        private void editLevel(object sender, EventArgs e)
        {
            if (lstLevels.SelectedIndex >= 0 &&
                lstLevels.Items[lstLevels.SelectedIndex] is SokobanLevel)
            {
                enterEditingMode();
            }
        }

        /// <summary>
        /// Invoked by "Edit => Add a comment" or the relevant toolbar button.
        /// Allows the user to add a comment to the level list.
        /// </summary>
        private void addComment(object sender, EventArgs e)
        {
            if (!pnlLevelList.Visible)
                toggleLevelList(sender, e);
            string Comment = InputBox.GetLine(Program.Translation.Mainform_LevelList_NewComment);
            if (Comment != null)
                lstLevels.AddLevelListItem(Comment);
        }

        /// <summary>
        /// Invoked by "Edit => Cut" or the relevant toolbar button.
        /// Cuts the selected level list item into the clipboard.
        /// </summary>
        private void cut(object sender, EventArgs e)
        {
            if (!lstLevels.MayDeleteSelectedItem(false))
                return;
            if (lstLevels.SelectedLevelActive())
                ctMainArea.Clear();
            copy(sender, e);
            lstLevels.RemoveLevelListItem();
        }

        /// <summary>
        /// Invoked by "Edit => Copy" or the relevant toolbar button.
        /// Copies the selected level list item to the clipboard.
        /// </summary>
        private void copy(object sender, EventArgs e)
        {
            if (!pnlLevelList.Visible || lstLevels.SelectedIndex < 0)
                return;
            Clipboard.SetData("SokobanData", lstLevels.Items[lstLevels.SelectedIndex]);
        }

        /// <summary>
        /// Invoked by "Edit => Paste" or the relevant toolbar button.
        /// Pastes any items from the clipboard into the level list.
        /// </summary>
        private void paste(object sender, EventArgs e)
        {
            if (!pnlLevelList.Visible || !Clipboard.ContainsData("SokobanData"))
                return;

            lstLevels.AddLevelListItem(Clipboard.GetData("SokobanData"));
        }

        /// <summary>
        /// Invoked by "Edit => Delete". Deletes the selected level or comment from the
        /// level list.
        /// </summary>
        private void deleteLevelOrComment(object sender, EventArgs e)
        {
            if (!lstLevels.MayDeleteSelectedItem(true))
                return;
            if (lstLevels.SelectedLevelActive())
                ctMainArea.Clear();
            lstLevels.RemoveLevelListItem();
        }

        /// <summary>
        /// Invoked by "Edit => Finish editing" or the relevant toolbar button.
        /// Leaves editing mode and saves the modified level in the level list,
        /// then returns to playing mode.
        /// </summary>
        private void finishEditingLevel(object sender, EventArgs e)
        {
            if (lstLevels.ActiveLevel == null || lstLevels.State != LevelListBox.LevelListBoxState.Editing) // this should never happen
                Ut.InternalError();

            SokobanLevelStatus Status = ctMainArea.Level.Validity;
            if (Status != SokobanLevelStatus.Valid)
            {
                String Problem = Status == SokobanLevelStatus.NotEnclosed
                    ? Program.Translation.Mainform_Validity_NotEnclosed
                    : Program.Translation.Mainform_Validity_WrongNumber;
                if (DlgMessage.Show(Program.Translation.Mainform_Validity_CannotSave + "\n\n" +
                    Problem + "\n\n" + Program.Translation.Mainform_Validity_CannotSave_Warning,
                    Program.Translation.Mainform_MessageTitle_FinishEditing, DlgType.Error,
                    Program.Translation.Mainform_Validity_CannotSave_btnSave,
                    Program.Translation.Mainform_Validity_CannotSave_btnResume) == 1)
                    return;
            }

            SokobanLevel Level = ctMainArea.Level.Clone();
            Level.EnsureSpace(0);

            // LevelList.EditAccept() will trigger LevelList_LevelActivating(), which in
            // turn will ask the user if they want to discard their changes to the level.
            // Since we don't want this, we have to set the Modified flag for the MainArea
            // to false before calling LevelList.EditAccept().
            ctMainArea.Modified = false;

            lstLevels.EditAccept(Level);
        }

        /// <summary>
        /// Invoked by "Edit => Cancel editing" or the relevant toolbar button.
        /// Cancels editing and returns to playing mode.
        /// </summary>
        private void cancelEditingLevel(object sender, EventArgs e)
        {
            if (lstLevels.ActiveLevel == null || lstLevels.State != LevelListBox.LevelListBoxState.Editing) // this should never happen
                Ut.InternalError();

            lstLevels.EditCancel();
        }

        /// <summary>
        /// Invoked by any of the "Edit => Wall/Piece/Target/Sokoban tool" menu items.
        /// Switches to the selected tool.
        /// </summary>
        private void changeEditTool(object sender, EventArgs e)
        {
            ctMainArea.Tool = ExpSokSettings.LastUsedTool = grpEditTool.Value;
            btnEditLevelWall.Checked = grpEditTool.Value == MainAreaTool.Wall;
            btnEditLevelPiece.Checked = grpEditTool.Value == MainAreaTool.Piece;
            btnEditLevelTarget.Checked = grpEditTool.Value == MainAreaTool.Target;
            btnEditLevelSokoban.Checked = grpEditTool.Value == MainAreaTool.Sokoban;
        }

        #endregion

        #region "Options" and "Help" menus

        /// <summary>
        /// Invoked by "Options => Display level list" or the LevelListClosePanel.
        /// Shows or hides the level list.
        /// </summary>
        private void toggleLevelList(object sender, EventArgs e)
        {
            showLevelList(!pnlLevelList.Visible);
        }

        /// <summary>
        /// Invoked by "Options => Display playing toolbar". Shows/hides the playing toolbar.
        /// </summary>
        private void togglePlayingToolbar(object sender, EventArgs e)
        {
            mnuOptionsPlayingToolbar.Checked = !mnuOptionsPlayingToolbar.Checked;
            toolPlay.Visible = ExpSokSettings.DisplayPlayingToolbar = mnuOptionsPlayingToolbar.Checked;
        }

        /// <summary>
        /// Invoked by "Options => Display editing toolbars (level pack)". Shows/hides the two toolbars for editing level pack files.
        /// </summary>
        private void toggleFileEditToolbar(object sender, EventArgs e)
        {
            mnuOptionsFileToolbars.Checked = !mnuOptionsFileToolbars.Checked;
            toolFile.Visible = toolFileEdit.Visible = ExpSokSettings.DisplayFileToolbars = mnuOptionsFileToolbars.Checked;
        }

        /// <summary>
        /// Invoked by "Options => Display editing toolbar (level)". Shows/hides the toolbar for editing levels.
        /// </summary>
        private void toggleEditLevelToolbar(object sender, EventArgs e)
        {
            mnuOptionsEditLevelToolbar.Checked = !mnuOptionsEditLevelToolbar.Checked;
            toolEditLevel.Visible = ExpSokSettings.DisplayEditLevelToolbar = mnuOptionsEditLevelToolbar.Checked;
        }

        /// <summary>
        /// Invoked by "Options => Display status bar". Toggles the visibility of the
        /// status bar at the bottom of the window.
        /// </summary>
        private void toggleStatusBar(object sender, EventArgs e)
        {
            mnuOptionsStatusBar.Checked = !mnuOptionsStatusBar.Checked;
            ctStatusBar.Visible = ExpSokSettings.DisplayStatusBar = mnuOptionsStatusBar.Checked;
        }

        /// <summary>
        /// Invoked (indirectly, via MovePathOptions) by any of the "Display move path
        /// as..." menu items under "Options". Sets the move path settings.
        /// </summary>
        private void changeMovePathOption(object sender, EventArgs e)
        {
            ctMainArea.MoveDrawMode = ExpSokSettings.MoveDrawMode = grpMovePathOptions.Value;
        }

        /// <summary>
        /// Invoked (indirectly, via PushPathOptions) by any of the "Display push path
        /// as..." menu items under "Options". Sets the push path settings.
        /// </summary>
        private void changePushPathOption(object sender, EventArgs e)
        {
            ctMainArea.PushDrawMode = ExpSokSettings.PushDrawMode = grpPushPathOptions.Value;
        }

        /// <summary>
        /// Invoked by "Options => Display end position of Sokoban and piece".
        /// Sets the option.
        /// </summary>
        private void changeEndPosOption(object sender, EventArgs e)
        {
            mnuOptionsEndPos.Checked = !mnuOptionsEndPos.Checked;
            ctMainArea.ShowEndPos = ExpSokSettings.ShowEndPos = mnuOptionsEndPos.Checked;
        }

        /// <summary>
        /// Invoked by "Options => Display reachable area for Sokoban".
        /// Sets the option.
        /// </summary>
        private void toggleReachableAreaSokoban(object sender, EventArgs e)
        {
            mnuOptionsAreaSokoban.Checked = !mnuOptionsAreaSokoban.Checked;
            ctMainArea.ShowAreaSokoban = ExpSokSettings.ShowAreaSokoban = mnuOptionsAreaSokoban.Checked;
        }

        /// <summary>
        /// Invoked by "Options => Display reachable area for piece".
        /// Sets the option.
        /// </summary>
        private void toggleReachableAreaPiece(object sender, EventArgs e)
        {
            mnuOptionsAreaPiece.Checked = !mnuOptionsAreaPiece.Checked;
            ctMainArea.ShowAreaPiece = ExpSokSettings.ShowAreaPiece = mnuOptionsAreaPiece.Checked;
        }

        /// <summary>
        /// Invoked by "Options => Enable sound". Sets the option.
        /// </summary>
        private void toggleSound(object sender, EventArgs e)
        {
            mnuOptionsSound.Checked = !mnuOptionsSound.Checked;
            ctMainArea.SoundEnabled = ExpSokSettings.SoundEnabled = mnuOptionsSound.Checked;
        }

        /// <summary>
        /// Invoked by "Help => Keyboard shortcuts". Displays a message box outlining
        /// the keyboard shortcuts that are not directly documented in the menus.
        /// </summary>
        private void helpKeyboardShortcuts(object sender, EventArgs e)
        {
            string helpfile = PathUtil.AppPath + "ExpSok.chm";
            if (File.Exists(helpfile))
                Help.ShowHelp(this, helpfile, HelpNavigator.TopicId, "30");
            else
                DlgMessage.ShowWarning(Program.Translation.Mainform_Error_HelpFileNotFound.Fmt(helpfile), null);
        }

        /// <summary>
        /// Invoked by "Help => About". Pops up the About box.
        /// </summary>
        private void helpAbout(object sender, EventArgs e)
        {
            using (var a = new AboutBox())
            {
                a.ShowDialog();
            }
        }

        #endregion

        #region Other events

        /// <summary>
        /// Invoked by the toolbar buttons for the Wall/Piece/Target/Sokoban edit tool.
        /// Switches the tool and sets the relevant menu item.
        /// </summary>
        private void changeEditingTool(object sender, EventArgs e)
        {
            grpEditTool.SetValue(
                sender == btnEditLevelWall ? MainAreaTool.Wall :
                sender == btnEditLevelPiece ? MainAreaTool.Piece :
                sender == btnEditLevelTarget ? MainAreaTool.Target : MainAreaTool.Sokoban);
        }

        /// <summary>
        /// Ensures all UI controls are up-to-date all the time.
        /// </summary>
        private void updateControls(object sender, EventArgs e)
        {
            updateControls();
        }

        #endregion
    }
}
