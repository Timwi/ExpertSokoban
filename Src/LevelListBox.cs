using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Collections;
using System.IO;
using RT.Util.Dialogs;
using System.Diagnostics;
using RT.Util;

namespace ExpertSokoban
{
    /// <summary>
    /// Encapsulates a list box that displays Sokoban levels and comments.
    /// </summary>
    public class LevelListBox : ListBox
    {
        #region Current level and state

        /// <summary>
        /// Encapsulates the state of the level list box.
        /// </summary>
        public enum LevelListBoxState { Playing, JustSolved, Editing }

        /// <summary>
        /// Determines whether the user is playing or editing levels.
        /// </summary>
        private LevelListBoxState FState = LevelListBoxState.Playing;

        /// <summary>
        /// Gets the current state of the list box. To set the state, use one of
        /// the methods such as EditSelectedLevel. The state determines whether
        /// the user is playing or editing levels.
        /// </summary>
        public LevelListBoxState State { get { return FState; } }

        /// <summary>
        /// The index of the currently active level. Depending on State, this
        /// could mean either the level being edited or the level being played.
        /// To ensure that the level is activated properly, set the ActiveLevelIndex
        /// instead of setting this value.
        /// </summary>
        private int? FActiveLevelIndex = null;

        /// <summary>
        /// The index of the currently active level. Depending on State, this
        /// could mean either the level being edited or the level being played.
        /// Setting this property activates the level correctly.
        /// </summary>
        private int? ActiveLevelIndex
        {
            get { return FActiveLevelIndex; }
            set
            {
                if (LevelActivating != null)
                {
                    // Confirm with the owner that the level can be changed
                    ConfirmEventArgs args = new ConfirmEventArgs();
                    args.ConfirmOK = true;
                    LevelActivating(this, args);
                    if (!args.ConfirmOK)
                        return;
                }

                FState = State;

                if (value == null)
                    FActiveLevelIndex = null;
                else if (value.Value < 0 || value.Value >= Items.Count || !(Items[value.Value] is SokobanLevel))
                    Ut.InternalError();
                else
                    FActiveLevelIndex = value.Value;

                RefreshItems();

                // Inform the owner that the level has changed
                LevelActivated(this, new EventArgs());

                // Make the active level selected
                SelectActiveLevel();
            }
        }

        /// <summary>
        /// Gets the currently active level, or null if none. There is currently no
        /// need for a public method to set a certain level, hence this is read-only.
        /// </summary>
        public SokobanLevel ActiveLevel
        {
            get
            {
                if (FActiveLevelIndex == null)
                    return null;
                // The following throws an exception if the index is out of bounds or
                // not a sokoban level. Will tell us if FActiveLevelIndex got out of sync.
                return (SokobanLevel)Items[FActiveLevelIndex.Value];
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
                    return (SokobanLevel)Items[SelectedIndex];
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the index of the item that is currently being edited.
        /// Comparing this to an integer index is equivalent to also verifying that state is Editing.
        /// Setting this is equivalent to setting both State and FActiveLevelIndex.
        /// </summary>
        private int? EditingIndex
        {
            get { return FState == LevelListBoxState.Editing ? (int?)FActiveLevelIndex : null; }
            set { FState = LevelListBoxState.Editing; ActiveLevelIndex = value; }
        }

        /// <summary>
        /// Gets or sets the index of the item that is currently being played.
        /// Comparing this to an integer index is equivalent to also verifying that state is Playing.
        /// Setting this is equivalent to setting both State and FActiveLevelIndex.
        /// </summary>
        private int? PlayingIndex
        {
            get { return FState == LevelListBoxState.Playing ? (int?)FActiveLevelIndex : 
                         FState == LevelListBoxState.JustSolved ? (int?)FActiveLevelIndex : null; }
            set { FState = LevelListBoxState.Playing; ActiveLevelIndex = value; }
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
        private bool FModified = false;

        /// <summary>
        /// Determines whether any changes have been made to the level file (i.e. the
        /// contents of the level list).
        /// </summary>
        public bool Modified
        {
            get { return FModified; }
            set
            {
                if (value)
                    Ut.InternalError("Not allowed to set Modified to true");
                else
                    FModified = false;
            }
        }

        /// <summary>
        /// Main constructor.
        /// </summary>
        public LevelListBox()
        {
            // Listen for some events
            MeasureItem += new MeasureItemEventHandler(LevelListBox_MeasureItem);
            DrawItem += new DrawItemEventHandler(LevelListBox_DrawItem);
            Resize += new EventHandler(LevelListBox_Resize);
            KeyDown += new KeyEventHandler(LevelListBox_KeyDown);
            DoubleClick += new EventHandler(LevelListBox_DoubleClick);
            MouseDown += new MouseEventHandler(LevelListBox_MouseDown);

            DrawMode = DrawMode.OwnerDrawVariable;
            FCachedWidth = ClientSize.Width;
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
        void LevelListBox_DoubleClick(object sender, EventArgs e)
        {
            // This does indeed happen - if you double-click an empty list for instance.
            if (SelectedIndex < 0)
                return;

            if (SelectedItem is SokobanLevel)
            {
                ActiveLevelIndex = SelectedIndex;
                FState = LevelListBoxState.Playing;
            }
            else
            {
                string NewComment = InputBox.GetLine("Please enter the revised comment:", (string)SelectedItem);
                if (NewComment != null)
                {
                    Items[SelectedIndex] = NewComment;
                    FModified = true;
                }
            }
        }

        /// <summary>
        /// Invoked if the user presses a key while the level list has focus.
        /// Currently handles the following keys:
        /// - Enter: opens the currently-selected level or edits the
        ///   currently-selected comment (equivalent to mouse double-click).
        /// </summary>
        void LevelListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Modifiers == 0)
                LevelListBox_DoubleClick(sender, new EventArgs());
        }

        /// <summary>
        /// Invoked by a mouse click in the level list. Handles the case where the user
        /// presses the right mouse button; we want to select the clicked-on item before
        /// showing the context menu.
        /// </summary>
        private void LevelListBox_MouseDown(object sender, MouseEventArgs e)
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
        private Dictionary<SokobanLevel, Image> FCachedRenderings = new Dictionary<SokobanLevel, Image>();

        /// <summary>
        /// Remembers the width of the level list for which the renderings were
        /// cached. The renderings only need to be updated when the width changes,
        /// not when the height changes.
        /// </summary>
        private int FCachedWidth = 0;

        /// <summary>
        /// The colour used to represent an item if it is currently being edited.
        /// </summary>
        private Color EditingColor = Color.FromArgb(255, 192, 128);
        
        /// <summary>
        /// The colour used to represent an item if it is currently being played.
        /// </summary>
        private Color PlayingColor = Color.FromArgb(64, 224, 128);

        /// <summary>
        /// The colour used to represent an item if it has been solved before by the
        /// current player.
        /// </summary>
        private Color SolvedColor = Color.FromArgb(64, 128, 255);

        /// <summary>
        /// The colour used to represent an item that has never been solved and is not
        /// currently being played or edited.
        /// </summary>
        private Color NeutralColor = Color.Silver;

        /// <summary>
        /// Occurs when an item in the level list requires drawing.
        /// </summary>
        private void LevelListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count || DesignMode)
                return;

            // The item we are trying to draw is a SokobanLevel
            if (Items[e.Index] is SokobanLevel)
            {
                string Key = Items[e.Index].ToString();
                bool IsSolved = ExpSokSettings.IsSolved(Key);
                string SolvedMessage = IsSolved
                        ?   "Solved (" + 
                            ExpSokSettings.Highscores[Key][ExpSokSettings.PlayerName].BestPushScore.E1 + "/" +
                            ExpSokSettings.Highscores[Key][ExpSokSettings.PlayerName].BestPushScore.E2 + ")"
                        :   "";
                bool IsPlaying = (e.Index == PlayingIndex) && State == LevelListBoxState.Playing;
                bool IsJustSolved = (e.Index == PlayingIndex) && State == LevelListBoxState.JustSolved;
                bool IsEditing = (e.Index == EditingIndex);

                int UseHeight = e.Bounds.Height-10;
                SizeF MessageSize1 = new SizeF(0, 0);
                SizeF MessageSize2 = new SizeF(0, 0);
                if (IsPlaying || IsEditing || IsJustSolved)
                {
                    MessageSize1 = e.Graphics.MeasureString(
                        IsEditing ? "Currently editing" :
                        IsJustSolved ? "Just solved" : "Currently playing", Font);
                    MessageSize1.Height += 5;
                }
                if (IsSolved)
                {
                    MessageSize2 = e.Graphics.MeasureString(SolvedMessage, Font);
                    MessageSize2.Height += 5;
                }

                Image Rendering = GetRendering((SokobanLevel)Items[e.Index], e.Bounds.Width-10,
                    e.Bounds.Height-10-(int)MessageSize1.Height-(int)MessageSize2.Height);
                e.DrawBackground();
                e.DrawFocusRectangle();
                if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                    e.Graphics.FillRectangle(
                        new LinearGradientBrush(
                            e.Bounds,
                            Color.White,
                            IsEditing ? EditingColor : IsPlaying ? PlayingColor : IsSolved ? SolvedColor : NeutralColor,
                            90,
                            false
                        ),
                        e.Bounds
                    );
                if (IsPlaying || IsEditing || IsJustSolved)
                {
                    e.Graphics.FillRectangle(
                        new LinearGradientBrush(
                            new RectangleF(e.Bounds.Left+5, e.Bounds.Top+4, e.Bounds.Width-10, MessageSize1.Height-3),
                            IsEditing ? EditingColor : PlayingColor,
                            Color.White,
                            90,
                            false
                        ),
                        new RectangleF(e.Bounds.Left+5, e.Bounds.Top+5, e.Bounds.Width-10, MessageSize1.Height-5)
                    );
                    e.Graphics.DrawString(
                        IsEditing ? "Currently editing" : IsJustSolved ? "Just Solved" : "Currently playing",
                        Font,
                        new SolidBrush(Color.Black),
                        e.Bounds.Left + e.Bounds.Width/2 - MessageSize1.Width/2,
                        e.Bounds.Top + 5
                    );
                }
                if (IsSolved)
                {
                    e.Graphics.FillRectangle(
                        new LinearGradientBrush(
                            new RectangleF(e.Bounds.Left+5, e.Bounds.Top+4+MessageSize1.Height,
                                e.Bounds.Width-10, MessageSize2.Height-3),
                            SolvedColor, Color.White, 90, false),
                        new RectangleF(e.Bounds.Left+5, e.Bounds.Top+5+MessageSize1.Height,
                            e.Bounds.Width-10, MessageSize2.Height-5)
                    );
                    e.Graphics.DrawString(SolvedMessage, Font, new SolidBrush(Color.Black),
                        e.Bounds.Left + e.Bounds.Width/2 - MessageSize2.Width/2,
                        e.Bounds.Top + 5 + MessageSize1.Height
                    );
                }
                e.Graphics.DrawImage(Rendering, e.Bounds.Left + 5,
                    e.Bounds.Top + 5 + MessageSize1.Height + MessageSize2.Height);
            }
            // The item we are trying to draw is a comment
            else
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                string Str = Items[e.Index] is string ? (string)Items[e.Index] : Items[e.Index].ToString();
                if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                    e.Graphics.FillRectangle(new LinearGradientBrush(e.Bounds, Color.White, Color.Silver, 90, false), e.Bounds);
                e.Graphics.DrawString(Str, Font, new SolidBrush(e.ForeColor), e.Bounds.Left + 5, e.Bounds.Top + 5);
            }
        }

        /// <summary>
        /// Renders the specified level and returns the finished image. Renderings are
        /// cached for efficiency.
        /// </summary>
        /// <param name="Level">The SokobanLevel to be rendered.</param>
        /// <param name="Width">Width of the area to render into.</param>
        /// <param name="Height">Height of the area to render into.</param>
        private Image GetRendering(SokobanLevel Level, int Width, int Height)
        {
            if (FCachedRenderings.ContainsKey(Level))
                return (Image)FCachedRenderings[Level];

            Image Rendering = new Bitmap(Width, Height);
            new Renderer(Level, Width, Height, new SolidBrush(Color.Transparent)).Render(Graphics.FromImage(Rendering));
            FCachedRenderings[Level] = Rendering;
            return Rendering;
        }

        /// <summary>
        /// Occurs when the LevelListBox is resized. If the width has changed, the
        /// cached renderings are discarded and the levels re-rendered.
        /// </summary>
        private void LevelListBox_Resize(object sender, EventArgs e)
        {
            if (ClientSize.Width != FCachedWidth)
            {
                FCachedWidth = ClientSize.Width;
                FCachedRenderings = new Dictionary<SokobanLevel, Image>();
                RefreshItems();
            }
        }

        /// <summary>
        /// Occurs when the list box needs to determine the size (height) of an item.
        /// Levels are measured in such a way that the height of the item is to its
        /// width as the height of the level is to its width. The "currently playing"
        /// or "currently editing" and the "solved" messages are taken into account.
        /// </summary>
        private void LevelListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count || DesignMode)
                return;

            if (Items[e.Index] is SokobanLevel)
            {
                SokobanLevel Level = (SokobanLevel)Items[e.Index];
                e.ItemHeight = (ClientSize.Width-10)*Level.Height/Level.Width + 10;

                if (e.Index == PlayingIndex)    // also covers "Just Solved"
                    e.ItemHeight += (int)e.Graphics.MeasureString("Currently playing", Font).Height + 5;
                else if (e.Index == EditingIndex)
                    e.ItemHeight += (int)e.Graphics.MeasureString("Currently editing", Font).Height + 5;

                if (ExpSokSettings.IsSolved(Items[e.Index].ToString()))
                    e.ItemHeight += (int)e.Graphics.MeasureString("Solved", Font).Height + 5;
            }
            else if (Items[e.Index] is string)
                e.ItemHeight = (int)e.Graphics.MeasureString((string)Items[e.Index], Font).Height + 10;
            else
                e.ItemHeight = (int)e.Graphics.MeasureString(Items[e.Index].ToString(), Font).Height + 10;

            if (e.ItemHeight > 255)
                e.ItemHeight = 255;
        }

        #endregion

        #region Loading from / saving to files

        /// <summary>
        /// Used only by LoadLevelPack(). Encapsulates the states that occur while
        /// reading a text file containing levels.
        /// </summary>
        private enum LevelReaderState { Empty, Comment, Level }

        /// <summary>
        /// Loads a level pack from the level list. Returns an index to the first unsolved
        /// level, or if all solved - the first valid level, or if all invalid - null.
        /// </summary>
        public int? LoadLevelPack(string FileName)
        {
            BeginUpdate();
            Items.Clear();

            // Have we encountered a valid level yet?
            int? FoundValidLevel = null;
            // Have we encountered a valid unsolved level yet?
            int? FoundUnsolvedLevel = null;
            // Class to read from the text file
            StreamReader StreamReader = new StreamReader(FileName, Encoding.UTF8);
            // State we're in (Empty, Comment or Level)
            LevelReaderState State = LevelReaderState.Empty;
            // Line last read
            String Line;
            // Current comment (gets appended to until we reach the end of the comment)
            String Comment = "";
            // Current level (gets appended to until we reach the end of the level)
            String LevelEncoded = "";

            do
            {
                Line = StreamReader.ReadLine();

                // Decide whether this line belongs to a level or comment
                LevelReaderState NewState =
                            (Line == null || Line.Length == 0) ? LevelReaderState.Empty :
                            Line[0] == ';' ? LevelReaderState.Comment :
                            LevelReaderState.Level;

                // If we are switching from level to comment or vice versa,
                // or reaching the end of the file, add the level or comment
                // to the level list and empty the relevant variable

                if (NewState != State && State == LevelReaderState.Comment)
                {
                    Items.Add(Comment);
                    Comment = "";
                }
                else if (NewState != State && State == LevelReaderState.Level)
                {
                    SokobanLevel NewLevel = new SokobanLevel(LevelEncoded);
                    NewLevel.EnsureSpace(0);
                    Items.Add(NewLevel);
                    LevelEncoded = "";
                    if (NewLevel.Validity == SokobanLevelStatus.Valid)
                    {
                        if (FoundValidLevel == null)
                            FoundValidLevel = Items.Count-1;
                        if (FoundUnsolvedLevel == null && !ExpSokSettings.IsSolved(NewLevel.ToString()))
                            FoundUnsolvedLevel = Items.Count-1;
                    }
                }

                // Append the line we just read to the relevant variable
                if (NewState == LevelReaderState.Comment)
                    Comment += Line.Substring(1) + "\n";
                else if (NewState == LevelReaderState.Level)
                    LevelEncoded += Line + "\n";

                // Update the state
                State = NewState;
            } while (Line != null);

            EndUpdate();
            StreamReader.Close();

            FModified = false;

            ExpSokSettings.LevelFilename = FileName;

            if (FoundUnsolvedLevel != null)
                // If we found a valid unsolved level, display it and let the player play it.
                return FoundUnsolvedLevel.Value;
            else if (FoundValidLevel != null)
                // If not, but we found a valid level, display that instead.
                return FoundValidLevel.Value;
            else
                return null;
        }

        /// <summary>
        /// Saves the current level list into a file.
        /// </summary>
        public void SaveLevelPack(string FileName)
        {
            // Save the file
            StreamWriter StreamWriter = new StreamWriter(FileName, false, Encoding.UTF8);
            foreach (object Item in Items)
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
            FModified = false;
        }

        /// <summary>
        /// Saves the level pack to the currently selected file name. If none, or
        /// if ForceDialog is true, a Save dialog is shown first. Returns true if
        /// saved successfully.
        /// </summary>
        public bool SaveWithDialog(bool ForceDialog)
        {
            // Check if have a file name
            if (ForceDialog || ExpSokSettings.LevelFilename == null)
            {
                SaveFileDialog SaveDialog = new SaveFileDialog();
                DialogResult Result = SaveDialog.ShowDialog();

                // If the user cancelled the dialog, bail out
                if (Result != DialogResult.OK)
                    return false;

                // Update the current filename
                ExpSokSettings.LevelFilename = SaveDialog.FileName;
            }

            // Just save.
            try
            {
                SaveLevelPack(ExpSokSettings.LevelFilename);
                return true;
            }
            catch (Exception e)
            {
                // If anything fails here, don't crash. Just tell the caller that save
                // hasn't actually happened.
                DlgMessage.ShowError("The settings could not be saved.\n" + e.Message, "Error saving settings");
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
            EditingIndex = SelectedIndex;
        }

        /// <summary>
        /// Accept the specified level as the new version of the level being edited.
        /// Turns off the Edit mode and starts playing the new level.
        /// </summary>
        public void EditAccept(SokobanLevel Level)
        {
            // Update the level
            Items[FActiveLevelIndex.Value] = Level;
            FModified = true;
            // Play this level
            PlayingIndex = FActiveLevelIndex;
            SelectedIndex = FActiveLevelIndex.Value; // select this level in the box - probably desirable?
        }

        /// <summary>
        /// Cancel editing of the current level. User's confirmation is NOT asked.
        /// Selects current level for playing.
        /// </summary>
        public void EditCancel()
        {
            // Play this level
            PlayingIndex = FActiveLevelIndex;
            SelectActiveLevel(); // select this level in the box - probably desirable?
        }

        /// <summary>
        /// Creates a new level list with no levels.
        /// </summary>
        public void NewList()
        {
            ExpSokSettings.LevelFilename = null;
            Items.Clear();
            ActiveLevelIndex = null;
        }

        /// <summary>
        /// Adds the specified item to the level list, while ensuring that the level
        /// list's EditingIndex/PlayingIndex remain intact.
        /// </summary>
        /// <param name="NewItem">The item to insert. May be a SokobanLevel object
        /// or a string (representing a comment).</param>
        public void AddLevelListItem(object NewItem)
        {
            if (SelectedIndex < 0)
            {
                // If nothing is currently selected, add the item at the bottom
                // and then select it
                Items.Add(NewItem);
                SelectedIndex = Items.Count - 1;
            }
            else
            {
                // Otherwise, insert the item before the current item and select it
                Items.Insert(SelectedIndex, NewItem);
                SelectedIndex -= 1;

                // Fix the values of EditingIndex and PlayingIndex
                if (FActiveLevelIndex != null && FActiveLevelIndex >= SelectedIndex)
                    FActiveLevelIndex = FActiveLevelIndex.Value + 1;
            }

            // The level file has changed
            FModified = true;
        }

        /// <summary>
        /// Deletes the selected item from the level list, while ensuring that the
        /// level list's EditingIndex/PlayingIndex and SelectedIndex remain intact.
        /// EditingIndex/PlayingIndex is set to null if the selected item is the
        /// active level.
        /// </summary>
        public void RemoveLevelListItem()
        {
            int Index = SelectedIndex;

            // Remove the item
            Items.RemoveAt(SelectedIndex);

            // Restore the value of SelectedIndex (why does RemoveAt have to destroy this?)
            if (Items.Count > 0 && Index < Items.Count)
                SelectedIndex = Index;
            else if (Items.Count > 0)
                SelectedIndex = Items.Count - 1;

            // Fix the values of EditingIndex and PlayingIndex
            if (FActiveLevelIndex != null && FActiveLevelIndex.Value > Index)
                FActiveLevelIndex = FActiveLevelIndex.Value - 1;
            else if (FActiveLevelIndex != null && FActiveLevelIndex.Value == Index)
                FActiveLevelIndex = null;

            // The level file has changed
            FModified = true;
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

            PlayingIndex = SelectedIndex;
        }

        /// <summary>
        /// Activates the first unsolved level for playing. If none, activates the first
        /// level.
        /// </summary>
        public void PlayFirstUnsolved()
        {
            SelectedIndex = -1;
            int? i = FindPrevNext(true, true);

            if (i != null)
            {
                PlayingIndex = i.Value;
                return;
            }

            i = FindPrevNext(false, true);

            if (i == null)
                PlayingIndex = null; // empty level pack
            else
                PlayingIndex = i.Value;
        }

        /// <summary>
        /// If State is Playing, sets State to JustSolved.
        /// </summary>
        public void JustSolved()
        {
            if (State != LevelListBoxState.Playing)
                return;

            FState = LevelListBoxState.JustSolved;
            RefreshItems();
        }

        /// <summary>
        /// Activates the next level for playing. Wraps around and starts from the first
        /// level if necessary. If no next level is found, an appropriate message is
        /// displayed to the user. The function will trigger the LevelActivating event
        /// before activating the level.
        /// </summary>
        /// <param name="MustBeUnsolved">Specifies whether to activate the immediately
        /// next level (false) or the next unsolved level (true).</param>
        /// <param name="CongratulateIfAll">If true, congratulates the user about having
        /// solved all levels if no more can be found.</param>
        public void PlayNext(bool MustBeUnsolved, bool CongratulateIfAll)
        {
            int? i = FindPrevNext(MustBeUnsolved, true);

            if (i == null)
            {
                if (CongratulateIfAll)
                    DlgMessage.ShowInfo("You have solved all levels in this level pack!", "Congratulations!");
                else if (MustBeUnsolved)
                    DlgMessage.ShowInfo("There are no more unsolved levels in this level file.", "Next unsolved level");
                else
                    DlgMessage.ShowInfo("There is no other level in the level file.", "Next level");
            }
            else
                PlayingIndex = i.Value;
        }

        /// <summary>
        /// Activates the previous level for playing. Wraps around and starts from the
        /// last level if necessary. If no previous level is found, an appropriate
        /// message is displayed to the user. The function will trigger the
        /// LevelActivating event before activating the level.
        /// </summary>
        /// <param name="MustBeUnsolved">Specifies whether to activate the immediately
        /// previous level (false) or the previous unsolved level (true).</param>
        public void PlayPrev(bool MustBeUnsolved)
        {
            int? i = FindPrevNext(MustBeUnsolved, false);

            if (i == null)
                DlgMessage.ShowInfo(
                    MustBeUnsolved ? "There are no more unsolved levels in this level file." : "There is no other level in the level file.",
                    MustBeUnsolved ? "Previous unsolved level" : "Previous level");
            else
                PlayingIndex = i.Value;
        }

        /// <summary>
        /// Returns the index of the next level in the specified direction, or null if
        /// there is none.
        /// </summary>
        /// <param name="MustBeUnsolved">If true, finds a level that has not yet been
        /// solved by the current player, and returns null if all levels have been
        /// solved. If false, returns the immediately next or previous level.</param>
        /// <param name="Forward">If true, searches for the next level, otherwise the
        /// previous.</param>
        private int? FindPrevNext(bool MustBeUnsolved, bool Forward)
        {
            if (Items.Count < 1)
                return null;

            int StartIndex = SelectedIndex == -1 ? (Forward ? Items.Count-1 : 0) : SelectedIndex;
            int i = StartIndex;
            for (; ; )
            {
                // Next item
                i = (i + (Forward ? 1 : (Items.Count - 1))) % Items.Count;

                if (Items[i] is SokobanLevel &&
                    (!MustBeUnsolved || !ExpSokSettings.IsSolved(Items[i].ToString())))
                {
                    // We've found a matching level
                    return i;
                }

                if (i == StartIndex)
                    return null;
            }
        }

        #endregion

        /// <summary>
        /// Selects the currently active level in the level list. If no level is
        /// currently active, the selection does not change.
        /// </summary>
        public void SelectActiveLevel()
        {
            if (Items.Count == 0)
                return;

            if (FActiveLevelIndex != null)
                SelectedIndex = FActiveLevelIndex.Value;
        }

        /// <summary>
        /// Determines (by asking the user if necessary) whether we are allowed to
        /// destroy the contents of the level list.
        /// </summary>
        /// <param name="Caption">Title bar caption to use in case any confirmation
        /// dialogs need to pop up.</param>
        public bool MayDestroy(string Caption)
        {
            // If no changes have been made, we're definitely allowed.
            if (!Modified)
                return true;

            // Ask the user if they want to save their changes to the level file.
            int Result = DlgMessage.Show("You have made changes to "
                + (ExpSokSettings.LevelFilename == null ? "(untitled)" : Path.GetFileName(ExpSokSettings.LevelFilename)) +
                ". Would you like to save those changes?", Caption, DlgType.Question,
                "Save changes", "&Discard changes", "Cancel");

            // If they said "Cancel", bail out immediately.
            if (Result == 2)
                return false;

            // If they said "Save changes", call SaveWithDialog(). If they cancel that
            // dialog, bail out.
            if (Result == 0)
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
        /// <param name="NormalConfirmation">If true, a confirmation message will also
        /// appear if the user is not currently playing or editing the selected level.
        /// </param>
        /// <returns>True if allowed.</returns>
        public bool MayDeleteSelectedItem(bool NormalConfirmation)
        {
            if (!Visible || SelectedIndex < 0)
                return false;

            object Item = Items[SelectedIndex];

            // Confirmation message if user is currently editing the selected level
            if (Item is SokobanLevel && SelectedIndex == EditingIndex)
            {
                if (DlgMessage.Show("You are currently editing this level.\n\n" +
                    "If you delete this level now, all your modifications will be discarded.\n\n" +
                    "Are you sure you wish to do this?", "Delete level",
                    DlgType.Warning, "Discard changes", "Cancel") == 1)
                    return false;
            }
            // Confirmation message if user is currently playing the selected level
            else if (Item is SokobanLevel && SelectedIndex == PlayingIndex)
            {
                if (DlgMessage.Show("You are currently playing this level.\n\n" +
                    "Are you sure you wish to give up?", "Delete level",
                    DlgType.Warning, "Give up", "Cancel") == 1)
                    return false;
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
        /// Determines whether the selected level is the active level or not.
        /// </summary>
        public bool SelectedLevelActive()
        {
            return SelectedIndex == FActiveLevelIndex;
        }
    }
}
