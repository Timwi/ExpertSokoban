using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;
using RT.Util;
using RT.Util.Dialogs;

namespace ExpertSokoban
{
    /// <summary>
    /// Encapsulates a list box that displays Sokoban levels and comments.
    /// </summary>
    sealed class LevelListBox : ListBox
    {
        #region Current level and state

        /// <summary>
        /// Encapsulates the state of the level list box.
        /// </summary>
        public enum LevelListBoxState
        {
            /// <summary>Indicates that the user is currently playing a level.</summary>
            Playing,
            /// <summary>Indicates that the "Level solved" image is currently being displayed.</summary>
            JustSolved,
            /// <summary>Indicates that the user is currently editing a level.</summary>
            Editing
        }

        /// <summary>
        /// Determines whether the user is playing or editing levels.
        /// </summary>
        private LevelListBoxState _state = LevelListBoxState.Playing;

        /// <summary>
        /// Gets the current state of the list box. To set the state, use one of
        /// the methods such as EditSelectedLevel. The state determines whether
        /// the user is playing or editing levels.
        /// </summary>
        public LevelListBoxState State { get { return _state; } }

        /// <summary>
        /// The index of the currently active level. Depending on State, this
        /// could mean either the level being edited or the level being played.
        /// To ensure that the level is activated properly, set the ActiveLevelIndex
        /// instead of setting this value.
        /// </summary>
        private int? _activeLevelIndex = null;

        /// <summary>
        /// Attempts to open the level at the specified index either for playing or editing.
        /// </summary>
        /// <param name="index">The index of the level to be opened, or null to deselect the current level.</param>
        /// <param name="state">Must be <see cref="LevelListBoxState.Playing"/> or <see cref="LevelListBoxState.Editing"/>. Ignored if index is null.</param>
        private void setActiveLevel(int? index, LevelListBoxState state)
        {
            if (LevelActivating != null)
            {
                // Confirm with the owner that the level can be changed
                ConfirmEventArgs args = new ConfirmEventArgs { ConfirmOK = true };
                LevelActivating(this, args);
                if (!args.ConfirmOK)
                    return;
            }

            if (index == null)
                _activeLevelIndex = null;
            else if (index.Value < 0 || index.Value >= Items.Count || !(Items[index.Value] is SokobanLevel))
                Ut.InternalError();
            else
            {
                SokobanLevelStatus status;
                if (state == LevelListBoxState.Playing && (status = ((SokobanLevel) Items[index.Value]).Validity) != SokobanLevelStatus.Valid)
                {
                    string Problem = status == SokobanLevelStatus.NotEnclosed
                        ? Program.Tr.Mainform_Validity_NotEnclosed
                        : Program.Tr.Mainform_Validity_WrongNumber;
                    if (DlgMessage.Show(Program.Tr.Mainform_Validity_CannotOpen + "\n\n" + Problem + "\n\n" + Program.Tr.Mainform_Validity_CannotOpen_Fix,
                        Program.Tr.Mainform_MessageTitle_OpenLevel, DlgType.Error, Program.Tr.Mainform_Validity_CannotOpen_btnEdit, Program.Tr.Dialogs_btnCancel) == 0)
                    {
                        _state = LevelListBoxState.Editing;
                        _activeLevelIndex = index.Value;
                    }
                    else
                        return;
                }
                else
                {
                    _state = state;
                    _activeLevelIndex = index.Value;
                }
            }

            RefreshItems();

            // Inform the owner that the level has changed
            LevelActivated(this, new EventArgs());

            // Make the active level selected
            SelectActiveLevel();
        }

        /// <summary>
        /// Gets the currently active level, or null if none. There is currently no
        /// need for a public method to set a certain level, hence this is read-only.
        /// </summary>
        public SokobanLevel ActiveLevel
        {
            get
            {
                if (_activeLevelIndex == null)
                    return null;
                // The following throws an exception if the index is out of bounds or
                // not a Sokoban level. Will tell us if _activeLevelIndex got out of sync.
                return (SokobanLevel) Items[_activeLevelIndex.Value];
            }
        }

        /// <summary>
        /// Gets the currently selected level, or null if the currently selected item
        /// is not a level.
        /// </summary>
        public SokobanLevel SelectedLevel
        {
            get
            {
                if (SelectedIndex < 0)
                    return null;
                if (Items[SelectedIndex] is SokobanLevel)
                    return (SokobanLevel) Items[SelectedIndex];
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the index of the item that is currently being edited.
        /// Comparing this to an integer index is equivalent to also verifying that state is Editing.
        /// Setting this is equivalent to calling setActiveLevel().
        /// </summary>
        private int? editingIndex
        {
            get { return _state == LevelListBoxState.Editing ? (int?) _activeLevelIndex : null; }
            set { setActiveLevel(value, LevelListBoxState.Editing); }
        }

        /// <summary>
        /// Gets or sets the index of the item that is currently being played.
        /// Comparing this to an integer index is equivalent to also verifying that state is Playing.
        /// Setting this is equivalent to calling setActiveLevel().
        /// </summary>
        private int? playingIndex
        {
            get
            {
                return _state == LevelListBoxState.Playing ? (int?) _activeLevelIndex :
                       _state == LevelListBoxState.JustSolved ? (int?) _activeLevelIndex : null;
            }
            set { setActiveLevel(value, LevelListBoxState.Playing); }
        }

        /// <summary>
        /// Invoked before activating a level. Set the ConfirmOK parameter to false to
        /// cancel the level change.
        /// </summary>
        public event ConfirmEventHandler LevelActivating;

        /// <summary>
        /// Sent after a level has been activated. Use ActiveLevel to get the new level.
        /// </summary>
        public event EventHandler LevelActivated;

        #endregion

        /// <summary>
        /// Determines whether any changes have been made to the level file (i.e. the
        /// contents of the level list).
        /// </summary>
        private bool _modified = false;

        /// <summary>
        /// Determines whether any changes have been made to the level file (i.e. the
        /// contents of the level list).
        /// </summary>
        public bool Modified
        {
            get { return _modified; }
            set
            {
                if (value)
                    Ut.InternalError("Not allowed to set Modified to true");
                else
                    _modified = false;
            }
        }

        /// <summary>
        /// Main constructor.
        /// </summary>
        public LevelListBox()
        {
            // Listen for some events
            MeasureItem += new MeasureItemEventHandler(measureItem);
            DrawItem += new DrawItemEventHandler(drawItem);
            Resize += new EventHandler(resize);
            KeyDown += new KeyEventHandler(keyDown);
            DoubleClick += new EventHandler(doubleClick);
            MouseDown += new MouseEventHandler(mouseDown);

            DrawMode = DrawMode.OwnerDrawVariable;
            _cachedWidth = ClientSize.Width;
        }

        /// <summary>
        /// Calls RefreshItems() which, inexplicably, is protected.
        /// </summary>
        public new void RefreshItems()
        {
            base.RefreshItems();
        }

        #region Event handlers

        /// <summary>
        /// Invoked if the user double-clicks in the level list. If the selected item
        /// is a comment, edits the comment. Otherwise opens the level for playing.
        /// </summary>
        private void doubleClick(object sender, EventArgs e)
        {
            // This does indeed happen - if you double-click an empty list for instance.
            if (SelectedIndex < 0)
                return;

            if (SelectedItem is SokobanLevel)
                setActiveLevel(SelectedIndex, LevelListBoxState.Playing);
            else
            {
                string newComment = InputBox.GetLine(Program.Tr.EditComment_Prompt, (string) SelectedItem, Program.Tr.EditComment_Title, Program.Tr.Dialogs_btnOK, Program.Tr.Dialogs_btnCancel);
                if (newComment != null)
                {
                    Items[SelectedIndex] = newComment;
                    _modified = true;
                }
            }
        }

        /// <summary>
        /// Invoked if the user presses a key while the level list has focus.
        /// Currently handles the following keys:
        /// - Enter: opens the currently-selected level or edits the
        ///   currently-selected comment (equivalent to mouse double-click).
        /// </summary>
        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Modifiers == 0)
                doubleClick(sender, new EventArgs());
        }

        /// <summary>
        /// Invoked by a mouse click in the level list. Handles the case where the user
        /// presses the right mouse button; we want to select the clicked-on item before
        /// showing the context menu.
        /// </summary>
        private void mouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int i = IndexFromPoint(e.Location);
                if (i >= 0 && i < Items.Count)
                    SelectedIndex = IndexFromPoint(e.Location);
                Focus();
            }
        }

        #endregion

        #region Rendering-related

        /// <summary>
        /// Caches complete renderings for levels to ensure smooth scrolling.
        /// </summary>
        private Dictionary<SokobanLevel, Image> _cachedRenderings = new Dictionary<SokobanLevel, Image>();

        /// <summary>
        /// Remembers the width of the level list for which the renderings were
        /// cached. The renderings only need to be updated when the width changes,
        /// not when the height changes.
        /// </summary>
        private int _cachedWidth = 0;

        /// <summary>
        /// The colour used to represent an item if it is currently being edited.
        /// </summary>
        private Color _editingColor = Color.FromArgb(255, 192, 128);

        /// <summary>
        /// The colour used to represent an item if it is currently being played.
        /// </summary>
        private Color _playingColor = Color.FromArgb(64, 224, 128);

        /// <summary>
        /// The colour used to represent an item if it has been solved before by the
        /// current player.
        /// </summary>
        private Color _solvedColor = Color.FromArgb(64, 128, 255);

        /// <summary>
        /// The colour used to represent an item that has never been solved and is not
        /// currently being played or edited.
        /// </summary>
        private Color _neutralColor = Color.Silver;

        /// <summary>
        /// Occurs when an item in the level list requires drawing.
        /// </summary>
        private void drawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count || DesignMode)
                return;

            // The item we are trying to draw is a SokobanLevel
            if (Items[e.Index] is SokobanLevel)
            {
                string key = Items[e.Index].ToString();
                bool isSolved = Program.Settings.IsSolved(key);
                string solvedMsg = isSolved
                        ? Program.Tr.LevelList_LevelSolved + " (" +
                            Program.Settings.Highscores[key][Program.Settings.PlayerName].BestPushScore.Moves + "/" +
                            Program.Settings.Highscores[key][Program.Settings.PlayerName].BestPushScore.Pushes + ")"
                        : "";
                bool isPlaying = (e.Index == playingIndex) && State == LevelListBoxState.Playing;
                bool isJustSolved = (e.Index == playingIndex) && State == LevelListBoxState.JustSolved;
                bool isEditing = (e.Index == editingIndex);

                int useHeight = e.Bounds.Height - 10;
                SizeF msgSize1 = new SizeF(0, 0);
                SizeF msgSize2 = new SizeF(0, 0);
                if (isPlaying || isEditing || isJustSolved)
                {
                    msgSize1 = e.Graphics.MeasureString(
                        isEditing ? Program.Tr.LevelList_CurrentlyEditing :
                        isJustSolved ? Program.Tr.LevelList_JustSolved : Program.Tr.LevelList_CurrentlyPlaying, Font);
                    msgSize1.Height += 5;
                }
                if (isSolved)
                {
                    msgSize2 = e.Graphics.MeasureString(solvedMsg, Font);
                    msgSize2.Height += 5;
                }

                Image rendering = getRendering((SokobanLevel) Items[e.Index], e.Bounds.Width - 10,
                    e.Bounds.Height - 10 - (int) msgSize1.Height - (int) msgSize2.Height);
                e.DrawBackground();
                e.DrawFocusRectangle();
                if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                    e.Graphics.FillRectangle(
                        new LinearGradientBrush(
                            e.Bounds,
                            Color.White,
                            isEditing ? _editingColor : isPlaying ? _playingColor : isSolved ? _solvedColor : _neutralColor,
                            90,
                            false
                        ),
                        e.Bounds
                    );
                if (isPlaying || isEditing || isJustSolved)
                {
                    e.Graphics.FillRectangle(
                        new LinearGradientBrush(
                            new RectangleF(e.Bounds.Left + 5, e.Bounds.Top + 4, e.Bounds.Width - 10, msgSize1.Height - 3),
                            isEditing ? _editingColor : _playingColor,
                            Color.White,
                            90,
                            false
                        ),
                        new RectangleF(e.Bounds.Left + 5, e.Bounds.Top + 5, e.Bounds.Width - 10, msgSize1.Height - 5)
                    );
                    e.Graphics.DrawString(
                        isEditing ? Program.Tr.LevelList_CurrentlyEditing : isJustSolved ? Program.Tr.LevelList_JustSolved : Program.Tr.LevelList_CurrentlyPlaying,
                        Font,
                        new SolidBrush(Color.Black),
                        e.Bounds.Left + e.Bounds.Width / 2 - msgSize1.Width / 2,
                        e.Bounds.Top + 5
                    );
                }
                if (isSolved)
                {
                    e.Graphics.FillRectangle(
                        new LinearGradientBrush(
                            new RectangleF(e.Bounds.Left + 5, e.Bounds.Top + 4 + msgSize1.Height,
                                e.Bounds.Width - 10, msgSize2.Height - 3),
                            _solvedColor, Color.White, 90, false),
                        new RectangleF(e.Bounds.Left + 5, e.Bounds.Top + 5 + msgSize1.Height,
                            e.Bounds.Width - 10, msgSize2.Height - 5)
                    );
                    e.Graphics.DrawString(solvedMsg, Font, new SolidBrush(Color.Black),
                        e.Bounds.Left + e.Bounds.Width / 2 - msgSize2.Width / 2,
                        e.Bounds.Top + 5 + msgSize1.Height
                    );
                }
                e.Graphics.DrawImage(rendering, e.Bounds.Left + 5,
                    e.Bounds.Top + 5 + msgSize1.Height + msgSize2.Height);
            }
            // The item we are trying to draw is a comment
            else
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                string str = Items[e.Index] is string ? (string) Items[e.Index] : Items[e.Index].ToString();
                if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                    e.Graphics.FillRectangle(new LinearGradientBrush(e.Bounds, Color.White, Color.Silver, 90, false), e.Bounds);
                e.Graphics.DrawString(str, Font, new SolidBrush(e.ForeColor), e.Bounds.Left + 5, e.Bounds.Top + 5);
            }
        }

        /// <summary>
        /// Renders the specified level and returns the finished image. Renderings are
        /// cached for efficiency.
        /// </summary>
        /// <param name="level">The SokobanLevel to be rendered.</param>
        /// <param name="width">Width of the area to render into.</param>
        /// <param name="height">Height of the area to render into.</param>
        private Image getRendering(SokobanLevel level, int width, int height)
        {
            if (_cachedRenderings.ContainsKey(level))
                return (Image) _cachedRenderings[level];

            Image rendering = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(rendering))
                new Renderer(level, width, height, new SolidBrush(Color.Transparent)).Render(graphics);
            _cachedRenderings[level] = rendering;
            return rendering;
        }

        /// <summary>
        /// Occurs when the LevelListBox is resized. If the width has changed, the
        /// cached renderings are discarded and the levels re-rendered.
        /// </summary>
        private void resize(object sender, EventArgs e)
        {
            if (ClientSize.Width != _cachedWidth)
            {
                _cachedWidth = ClientSize.Width;
                _cachedRenderings = new Dictionary<SokobanLevel, Image>();
                RefreshItems();
            }
        }

        /// <summary>
        /// Occurs when the list box needs to determine the size (height) of an item.
        /// Levels are measured in such a way that the height of the item is to its
        /// width as the height of the level is to its width. The "currently playing"
        /// or "currently editing" and the "solved" messages are taken into account.
        /// </summary>
        private void measureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count || DesignMode)
                return;

            if (Items[e.Index] is SokobanLevel)
            {
                SokobanLevel level = (SokobanLevel) Items[e.Index];
                e.ItemHeight = (ClientSize.Width - 10) * level.Height / level.Width + 10;

                if (e.Index == playingIndex)    // also covers "Just Solved"
                    e.ItemHeight += (int) e.Graphics.MeasureString(Program.Tr.LevelList_CurrentlyPlaying, Font).Height + 5;
                else if (e.Index == editingIndex)
                    e.ItemHeight += (int) e.Graphics.MeasureString(Program.Tr.LevelList_CurrentlyEditing, Font).Height + 5;

                if (Program.Settings.IsSolved(Items[e.Index].ToString()))
                    e.ItemHeight += (int) e.Graphics.MeasureString(Program.Tr.LevelList_LevelSolved, Font).Height + 5;
            }
            else if (Items[e.Index] is string && (Items[e.Index] as string).Length == 0)
                e.ItemHeight = (int) e.Graphics.MeasureString("Mg", Font).Height + 10;
            else if (Items[e.Index] is string)
                e.ItemHeight = (int) e.Graphics.MeasureString((string) Items[e.Index], Font).Height + 10;
            else
                e.ItemHeight = (int) e.Graphics.MeasureString(Items[e.Index].ToString(), Font).Height + 10;

            if (e.ItemHeight > 255)
                e.ItemHeight = 255;
        }

        #endregion

        #region Loading from / saving to files

        /// <summary>
        /// Used only by <see cref="LoadLevelPack(string)"/>. Encapsulates the states that occur while
        /// reading a text file containing levels.
        /// </summary>
        private enum levelReaderState { Empty, Comment, Level }

        /// <summary>
        /// The exception thrown by <see cref="LoadLevelPack(string)"/> if the level file is invalid.
        /// </summary>
        public sealed class InvalidLevelException : Exception
        {
            /// <summary>Constructor.</summary>
            public InvalidLevelException() { }
        }

        /// <summary>
        /// Loads a level file from the level list.
        /// </summary>
        public void LoadLevelPack(string path)
        {
            // Will contain the list of levels and comments that we have loaded.
            // (We only want to add them to the listbox at the end in case we
            // throw an exception, because then we want to keep the old file.)
            List<object> loadedItems = new List<object>();

            // Class to read from the text file
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            // State we're in (Empty, Comment or Level)
            levelReaderState state = levelReaderState.Empty;
            // Line last read
            String line;
            // Current comment (gets appended to until we reach the end of the comment)
            String comment = "";
            // Current level (gets appended to until we reach the end of the level)
            String levelEncoded = "";

            do
            {
                line = sr.ReadLine();

                // If this line begins with a semicolon (';'), it's a comment.
                // Otherwise, it is considered part of the level and must
                // contain only the characters #@$.*+ or space.
                if (line != null && line.Length > 0 && line[0] != ';')
                {
                    // check for invalid characters
                    for (int i = 0; i < line.Length; i++)
                        if (line[i] != ' ' && line[i] != '#' && line[i] != '@' &&
                            line[i] != '$' && line[i] != '.' && line[i] != '*' &&
                            line[i] != '+')
                            throw new InvalidLevelException();
                }

                // Decide whether this line belongs to a level or comment
                levelReaderState newState =
                            (line == null || line.Length == 0) ? levelReaderState.Empty :
                            line[0] == ';' ? levelReaderState.Comment :
                            levelReaderState.Level;

                // If we are switching from level to comment or vice versa,
                // or reaching the end of the file, add the level or comment
                // to the level list and empty the relevant variable

                if (newState != state && state == levelReaderState.Comment)
                {
                    loadedItems.Add(comment);
                    comment = "";
                }
                else if (newState != state && state == levelReaderState.Level)
                {
                    SokobanLevel newLevel = new SokobanLevel(levelEncoded);
                    newLevel.EnsureSpace(0);
                    loadedItems.Add(newLevel);
                    levelEncoded = "";
                }

                // Append the line we just read to the relevant variable
                if (newState == levelReaderState.Comment)
                    comment += line.Substring(1) + "\n";
                else if (newState == levelReaderState.Level)
                    levelEncoded += line + "\n";

                // Update the state
                state = newState;
            } while (line != null);

            _activeLevelIndex = null;
            BeginUpdate();
            Items.Clear();
            foreach (object Item in loadedItems)
                Items.Add(Item);
            EndUpdate();
            sr.Close();

            _modified = false;

            Program.Settings.LevelFilename = path;
        }

        /// <summary>
        /// Saves the current level list into a file.
        /// </summary>
        public void SaveLevelPack(string path)
        {
            // Save the file
            StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
            foreach (object item in Items)
            {
                if (item is SokobanLevel)
                    sw.WriteLine((item as SokobanLevel).ToString());
                else if (item is string)
                    sw.WriteLine(";" + (string) item + "\n");
                else
                    sw.WriteLine(";" + item.ToString() + "\n");
            }
            sw.Close();

            // File is now saved, so mark it as unchanged
            _modified = false;
        }

        /// <summary>
        /// Saves the level file to the currently selected file name. If none, or
        /// if forceDialog is true, a Save dialog is shown first. Returns true if
        /// saved successfully.
        /// </summary>
        public bool SaveWithDialog(bool forceDialog)
        {
            // Check if have a file name
            if (forceDialog || Program.Settings.LevelFilename == null)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = "txt";
                dlg.Filter = Program.Tr.Save_FileType_TextFiles + "|*.txt|" + Program.Tr.Save_FileType_AllFiles + "|*.*";
                dlg.InitialDirectory = Program.Settings.LastOpenSaveDirectory;
                DialogResult result = dlg.ShowDialog();

                // If the user cancelled the dialog, bail out
                if (result != DialogResult.OK)
                    return false;

                // Update the current filename
                Program.Settings.LastOpenSaveDirectory = Path.GetDirectoryName(dlg.FileName);
                Program.Settings.LevelFilename = dlg.FileName;
            }

            // Just save.
            try
            {
                SaveLevelPack(Program.Settings.LevelFilename);
                return true;
            }
            catch (Exception e)
            {
                // If anything fails here, don't crash. Just tell the caller that save hasn't actually happened.
                DlgMessage.Show(Program.Tr.LevelList_Message_CannotSaveSettings + "\n" + e.Message, Program.Tr.LevelList_Message_CannotSaveSettings_Title, DlgType.Error);
                return false;
            }
        }

        #endregion

        #region Editing functions

        /// <summary>
        /// Switches the selected level into editing mode.
        /// The function will switch the level nicely, i.e. ask the owner if it's OK
        /// to give up the current level.
        /// </summary>
        public void EditSelectedLevel()
        {
            editingIndex = SelectedIndex;
        }

        /// <summary>
        /// Accept the specified level as the new version of the level being edited.
        /// Turns off the Edit mode and starts playing the new level (if it is valid).
        /// </summary>
        public void EditAccept(SokobanLevel level)
        {
            // Update the level
            Items[_activeLevelIndex.Value] = level;
            _modified = true;
            // Play this level
            if (level.Validity == SokobanLevelStatus.Valid)
            {
                playingIndex = _activeLevelIndex;
                SelectedIndex = _activeLevelIndex.Value; // select this level in the box - probably desirable?
            }
            else
                playingIndex = null;
        }

        /// <summary>
        /// Cancel editing of the current level. User's confirmation is NOT asked.
        /// Selects current level for playing.
        /// </summary>
        public void EditCancel()
        {
            if (ActiveLevel == null)
                return;
            if (ActiveLevel.Validity == SokobanLevelStatus.Valid)
                playingIndex = _activeLevelIndex;
            else
                playingIndex = null;
            SelectActiveLevel(); // select this level in the box - probably desirable?
        }

        /// <summary>
        /// Creates a new level list with no levels.
        /// </summary>
        public void NewList()
        {
            Program.Settings.LevelFilename = null;
            Items.Clear();
            setActiveLevel(null, LevelListBoxState.Playing);
            Modified = false;
        }

        /// <summary>
        /// Adds the specified item to the level list, while ensuring that the level
        /// list's EditingIndex/PlayingIndex remain intact.
        /// </summary>
        /// <param name="item">The item to insert. May be a SokobanLevel object
        /// or a string (representing a comment).</param>
        public void AddLevelListItem(object item)
        {
            if (SelectedIndex < 0)
            {
                // If nothing is currently selected, add the item at the bottom
                // and then select it
                Items.Add(item);
                SelectedIndex = Items.Count - 1;
            }
            else
            {
                // Otherwise, insert the item before the current item and select it
                Items.Insert(SelectedIndex, item);
                SelectedIndex -= 1;

                // Fix the values of EditingIndex and PlayingIndex
                if (_activeLevelIndex != null && _activeLevelIndex >= SelectedIndex)
                    _activeLevelIndex = _activeLevelIndex.Value + 1;
            }

            // The level file has changed
            _modified = true;
        }

        /// <summary>
        /// Deletes the selected item from the level list, while ensuring that the
        /// level list's EditingIndex/PlayingIndex and SelectedIndex remain intact.
        /// EditingIndex/PlayingIndex is set to null if the selected item is the
        /// active level.
        /// </summary>
        public void RemoveLevelListItem()
        {
            int index = SelectedIndex;

            // Remove the item
            Items.RemoveAt(SelectedIndex);

            // Restore the value of SelectedIndex (why does RemoveAt have to destroy this?)
            if (Items.Count > 0 && index < Items.Count)
                SelectedIndex = index;
            else if (Items.Count > 0)
                SelectedIndex = Items.Count - 1;

            // Fix the values of EditingIndex and PlayingIndex
            if (_activeLevelIndex != null && _activeLevelIndex.Value > index)
                _activeLevelIndex = _activeLevelIndex.Value - 1;
            else if (_activeLevelIndex != null && _activeLevelIndex.Value == index)
                _activeLevelIndex = null;

            // The level file has changed
            _modified = true;
        }

        #endregion

        #region Activate next/prev/etc level

        /// <summary>
        /// Activates the selected level for playing.
        /// </summary>
        public void PlaySelected()
        {
            if (SelectedIndex < 0)
                return;
            if (!(SelectedItem is SokobanLevel))
                Ut.InternalError();

            playingIndex = SelectedIndex;
        }

        /// <summary>
        /// Activates the first unsolved level for playing. If none, activates the first
        /// level.
        /// </summary>
        public void PlayFirstUnsolved()
        {
            SelectedIndex = -1;
            int? i = findPrevNext(true, true);

            if (i != null)
            {
                playingIndex = i.Value;
                return;
            }

            i = findPrevNext(false, true);

            if (i == null)
                playingIndex = null; // empty level file
            else
                playingIndex = i.Value;
        }

        /// <summary>
        /// If State is Playing, sets State to JustSolved.
        /// </summary>
        public void JustSolved()
        {
            if (_state != LevelListBoxState.Playing)
                return;

            _state = LevelListBoxState.JustSolved;
            RefreshItems();
        }

        /// <summary>
        /// Activates the next level for playing. Wraps around and starts from the first
        /// level if necessary. If no next level is found, an appropriate message is
        /// displayed to the user. The function will trigger the LevelActivating event
        /// before activating the level.
        /// </summary>
        /// <param name="mustBeUnsolved">Specifies whether to activate the immediately
        /// next level (false) or the next unsolved level (true).</param>
        /// <param name="congratulateIfAll">If true, congratulates the user about having
        /// solved all levels if no more can be found.</param>
        public void PlayNext(bool mustBeUnsolved, bool congratulateIfAll)
        {
            int? i = findPrevNext(mustBeUnsolved, true);

            if (i == null)
            {
                if (congratulateIfAll)
                    DlgMessage.Show(Program.Tr.LevelList_Message_AllSolved, Program.Tr.LevelList_Message_AllSolved_Title, DlgType.Info, Program.Tr.Dialogs_btnOK);
                else if (mustBeUnsolved)
                    DlgMessage.Show(Program.Tr.LevelList_Message_NoMoreUnsolved, Program.Tr.LevelList_Message_NextUnsolved_Title, DlgType.Info, Program.Tr.Dialogs_btnOK);
                else
                    DlgMessage.Show(Program.Tr.LevelList_Message_NoOtherLevel, Program.Tr.LevelList_Message_Next_Title, DlgType.Info, Program.Tr.Dialogs_btnOK);
            }
            else
                playingIndex = i.Value;
        }

        /// <summary>
        /// Activates the previous level for playing. Wraps around and starts from the
        /// last level if necessary. If no previous level is found, an appropriate
        /// message is displayed to the user. The function will trigger the
        /// LevelActivating event before activating the level.
        /// </summary>
        /// <param name="mustBeUnsolved">Specifies whether to activate the immediately
        /// previous level (false) or the previous unsolved level (true).</param>
        public void PlayPrev(bool mustBeUnsolved)
        {
            int? i = findPrevNext(mustBeUnsolved, false);

            if (i == null)
                DlgMessage.Show(
                    mustBeUnsolved ? Program.Tr.LevelList_Message_NoMoreUnsolved : Program.Tr.LevelList_Message_NoOtherLevel,
                    mustBeUnsolved ? Program.Tr.LevelList_Message_PrevUnsolved_Title : Program.Tr.LevelList_Message_Prev_Title,
                    DlgType.Info, Program.Tr.Dialogs_btnOK);
            else
                playingIndex = i.Value;
        }

        /// <summary>
        /// Returns the index of the next level in the specified direction, or null if
        /// there is none.
        /// </summary>
        /// <param name="mustBeUnsolved">If true, finds a level that has not yet been
        /// solved by the current player, and returns null if all levels have been
        /// solved. If false, returns the immediately next or previous level.</param>
        /// <param name="forward">If true, searches for the next level, otherwise the
        /// previous.</param>
        private int? findPrevNext(bool mustBeUnsolved, bool forward)
        {
            if (Items.Count < 1)
                return null;

            int startIndex = _activeLevelIndex == null ? (forward ? Items.Count - 1 : 0) : _activeLevelIndex.Value;
            int i = startIndex;
            while (true)
            {
                // Next item
                i = (i + (forward ? 1 : (Items.Count - 1))) % Items.Count;

                if (_activeLevelIndex != null && i == _activeLevelIndex)
                    return null;

                if (Items[i] is SokobanLevel &&
                    (!mustBeUnsolved || !Program.Settings.IsSolved(Items[i].ToString())))
                {
                    // We've found a matching level
                    return i;
                }

                if (i == startIndex)
                    return null;
            }
        }

        /// <summary>
        /// Selects the currently active level in the level list. If no level is
        /// currently active, the selection does not change.
        /// </summary>
        public void SelectActiveLevel()
        {
            if (Items.Count == 0)
                return;

            if (_activeLevelIndex != null)
                SelectedIndex = _activeLevelIndex.Value;
        }

        /// <summary>
        /// Determines whether the selected level is the active level or not.
        /// </summary>
        public bool SelectedLevelActive()
        {
            return SelectedIndex == _activeLevelIndex;
        }

        #endregion

        /// <summary>
        /// Determines (by asking the user if necessary) whether we are allowed to
        /// destroy the contents of the level list.
        /// </summary>
        /// <param name="caption">Title bar caption to use in case any confirmation
        /// dialogs need to pop up.</param>
        public bool MayDestroy(string caption)
        {
            // If no changes have been made, we're definitely allowed.
            if (!_modified)
                return true;

            // Ask the user if they want to save their changes to the level file.
            int result = DlgMessage.Show(Program.Tr.LevelList_Message_SaveChanges.Fmt(
                    (Program.Settings.LevelFilename == null ? Program.Tr.FileName_Untitled.Translation : Path.GetFileName(Program.Settings.LevelFilename))
                ), caption, DlgType.Question,
                Program.Tr.Dialogs_btnSave,
                Program.Tr.Dialogs_btnDiscard,
                Program.Tr.Dialogs_btnCancel);

            // If they said "Cancel", bail out immediately.
            if (result == 2)
                return false;

            // If they said "Save changes", call SaveWithDialog(). If they cancel that
            // dialog, bail out.
            if (result == 0)
            {
                if (!SaveWithDialog(false))
                    return false;
            }

            // In all other cases (file was saved, or "Discard changes" was clicked),
            // we're allowed.
            return true;
        }

        /// <summary>
        /// Determines whether we are allowed to delete the selected item from the
        /// level list. If the user is currently playing the level, a confirmation
        /// message asks whether they want to give up. If they are currently editing it,
        /// a message asks whether they want to discard it.
        /// </summary>
        /// <param name="normalConfirmation">If true, a confirmation message will also
        /// appear if the user is not currently playing or editing the selected level.
        /// </param>
        /// <returns>True if allowed.</returns>
        public bool MayDeleteSelectedItem(bool normalConfirmation)
        {
            if (!Visible || SelectedIndex < 0)
                return false;

            object item = Items[SelectedIndex];

            // Confirmation message if user is currently editing the selected level
            if (item is SokobanLevel && SelectedIndex == editingIndex)
            {
                if (DlgMessage.Show(Program.Tr.LevelList_Message_DeleteLevel_CurrentlyEditing, Program.Tr.LevelList_Message_DeleteLevel_Title,
                    DlgType.Warning, Program.Tr.Dialogs_btnDiscard, Program.Tr.Dialogs_btnCancel) == 1)
                    return false;
            }
            // Confirmation message if user is currently playing the selected level
            else if (item is SokobanLevel && SelectedIndex == playingIndex)
            {
                if (DlgMessage.Show(Program.Tr.LevelList_Message_DeleteLevel_CurrentlyPlaying, Program.Tr.LevelList_Message_DeleteLevel_Title,
                    DlgType.Warning, Program.Tr.Dialogs_btnGiveUp, Program.Tr.Dialogs_btnCancel) == 1)
                    return false;
            }
            // Confirmation message if neither of the two cases apply
            else if (item is SokobanLevel)
            {
                if (normalConfirmation && DlgMessage.Show(
                    Program.Tr.LevelList_Message_DeleteLevel_Sure,
                    Program.Tr.LevelList_Message_DeleteLevel_Title, DlgType.Question, Program.Tr.LevelList_Message_DeleteLevel_btnDelete, Program.Tr.Dialogs_btnCancel) == 1)
                    return false;
            }
            return true;
        }
    }
}
