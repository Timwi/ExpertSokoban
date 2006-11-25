using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Collections;
using System.IO;

namespace ExpertSokoban
{
    /// <summary>
    /// Encapsulates a list box that displays Sokoban levels and comments.
    /// </summary>
    public class LevelListBox : ListBox
    {
        /// <summary>
        /// Encapsulates the state of the level list box.
        /// </summary>
        private enum LevelListBoxState { Null, Playing, Editing }

        /// <summary>
        /// Caches complete renderings for levels to ensure smooth scrolling.
        /// </summary>
        private Dictionary<SokobanLevel, Image> FCachedRenderings = new Dictionary<SokobanLevel, Image>();

        /// <summary>
        /// Remembers the last width of the level list. The cached renderings only need
        /// to be updated when the width changes, not when the height changes.
        /// </summary>
        private int FLastWidth = 0;

        /// <summary>
        /// The index of one element that is currently being played or edited.
        /// </summary>
        private int FPlayingEditingIndex = 0;

        /// <summary>
        /// Whether the "special element" is currently being played or edited (or neither)
        /// </summary>
        private LevelListBoxState FState = LevelListBoxState.Null;

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
        /// Gets or sets the index of the item that is currently being edited.
        /// Setting this property automatically sets PlayingIndex to null.
        /// </summary>
        public int? EditingIndex
        {
            get { return FState == LevelListBoxState.Editing ? (int?)FPlayingEditingIndex : null; }
            set { SetPlayingEditing(LevelListBoxState.Editing, value); }
        }

        /// <summary>
        /// Gets or sets the index of the item that is currently being played.
        /// Setting this property automatically sets EditingIndex to null.
        /// </summary>
        public int? PlayingIndex
        {
            get { return FState == LevelListBoxState.Playing ? (int?)FPlayingEditingIndex : null; }
            set { SetPlayingEditing(LevelListBoxState.Playing, value); }
        }

        /// <summary>
        /// Main constructor.
        /// </summary>
        public LevelListBox()
        {
            this.MeasureItem += new MeasureItemEventHandler(LevelListBox_MeasureItem);
            this.DrawItem += new DrawItemEventHandler(LevelListBox_DrawItem);
            this.Resize += new EventHandler(LevelListBox_Resize);
            this.DrawMode = DrawMode.OwnerDrawVariable;
            FLastWidth = ClientSize.Width;
        }

        /// <summary>
        /// Private method to handle assignments to PlayingIndex or EditingIndex.
        /// </summary>
        /// <param name="State">LevelListBoxState.Playing or LevelListBoxState.Editing.</param>
        /// <param name="Value">Index of the item, or null if neither playing nor editing.</param>
        private void SetPlayingEditing(LevelListBoxState State, int? Value)
        {
            int OldIndex = FPlayingEditingIndex;
            if (Value == null || Value.Value < 0 || Value.Value >= Items.Count)
                FState = LevelListBoxState.Null;
            else
            {
                FState = State;
                FPlayingEditingIndex = Value.Value;
            }
            // Why oh why does RefreshItems() have to change SelectedIndex?
            int OldSelectedIndex = SelectedIndex;
            RefreshItems();
            SelectedIndex = OldSelectedIndex;
        }

        /// <summary>
        /// Occurs when the LevelListBox is resized. If the width has changed, the
        /// cached renderings are discarded and the levels re-rendered.
        /// </summary>
        private void LevelListBox_Resize(object sender, EventArgs e)
        {
            if (ClientSize.Width != FLastWidth)
            {
                FLastWidth = ClientSize.Width;
                FCachedRenderings = new Dictionary<SokobanLevel, Image>();
                RefreshItems();
            }
        }

        /// <summary>
        /// Occurs when an item in the level list requires drawing.
        /// </summary>
        private void LevelListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index > -1 && e.Index < Items.Count && !DesignMode)
            {
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
                    bool IsPlaying = (e.Index == FPlayingEditingIndex && FState == LevelListBoxState.Playing);
                    bool IsEditing = (e.Index == FPlayingEditingIndex && FState == LevelListBoxState.Editing);

                    int UseHeight = e.Bounds.Height-10;
                    SizeF MessageSize1 = new SizeF(0, 0);
                    SizeF MessageSize2 = new SizeF(0, 0);
                    if (IsPlaying || IsEditing)
                    {
                        MessageSize1 = e.Graphics.MeasureString(
                            IsEditing ? "Currently editing" : "Currently playing", Font);
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
                    if (IsPlaying || IsEditing)
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
                            IsEditing ? "Currently editing" : "Currently playing",
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
        /// Occurs when the list box needs to determine the size (height) of an item.
        /// Levels are measured in such a way that the height of the item is to its
        /// width as the height of the level is to its width. The "currently playing"
        /// or "currently editing" and the "solved" messages are taken into account.
        /// </summary>
        private void LevelListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index > -1 && e.Index < Items.Count && !DesignMode)
            {
                if (Items[e.Index] is SokobanLevel)
                {
                    SokobanLevel Level = (SokobanLevel)Items[e.Index];
                    e.ItemHeight = (ClientSize.Width-10)*Level.Height/Level.Width + 10;

                    if (e.Index == FPlayingEditingIndex && FState == LevelListBoxState.Playing)
                        e.ItemHeight += (int)e.Graphics.MeasureString("Currently playing", Font).Height + 5;
                    else if (e.Index == FPlayingEditingIndex && FState == LevelListBoxState.Editing)
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
        }

        /// <summary>
        /// Calls RefreshItems() which, inexplicably, is protected.
        /// </summary>
        public void ComeOn_RefreshItems()
        {
            RefreshItems();
        }

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

            if (FoundUnsolvedLevel != null)
                // If we found a valid unsolved level, display it and let the player play it.
                return FoundUnsolvedLevel.Value;
            else if (FoundValidLevel != null)
                // If not, but we found a valid level, display that instead.
                return FoundValidLevel.Value;
            else
                return null;
        }
    }
}
