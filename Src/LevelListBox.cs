using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Collections;

namespace ExpertSokoban
{
    public class LevelListBox : ListBox
    {
        private enum LevelListBoxState { Null, Playing, Editing }

        private Hashtable FCachedRenderings = new Hashtable();
        private int FLastWidth = 0;
        private int FPlayingEditingIndex = 0;
        private LevelListBoxState FState = LevelListBoxState.Null;

        public int? EditingIndex
        {
            get { return FState == LevelListBoxState.Editing ? (int?)FPlayingEditingIndex : null; }
            set { SetPlayingEditing(LevelListBoxState.Editing, value); }
        }

        public int? PlayingIndex
        {
            get { return FState == LevelListBoxState.Playing ? (int?)FPlayingEditingIndex : null; }
            set { SetPlayingEditing(LevelListBoxState.Playing, value); }
        }

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

        public LevelListBox()
        {
            this.MeasureItem += new MeasureItemEventHandler(LevelListBox_MeasureItem);
            this.DrawItem += new DrawItemEventHandler(LevelListBox_DrawItem);
            this.Resize += new EventHandler(LevelListBox_Resize);
            this.DrawMode = DrawMode.OwnerDrawVariable;
            FLastWidth = ClientSize.Width;
        }

        private void LevelListBox_Resize(object sender, EventArgs e)
        {
            if (ClientSize.Width != FLastWidth)
            {
                FLastWidth = ClientSize.Width;
                FCachedRenderings = new Hashtable();
                RefreshItems();
            }
        }

        private void LevelListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index > -1 && e.Index < Items.Count && !DesignMode)
            {
                if (Items[e.Index] is SokobanLevel)
                {
                    int UseHeight = e.Bounds.Height-10;
                    SizeF MessageSize = new SizeF(0, 0);
                    if (e.Index == FPlayingEditingIndex && FState != LevelListBoxState.Null)
                    {
                        MessageSize = e.Graphics.MeasureString(
                            FState == LevelListBoxState.Editing ? "Currently editing" : "Currently playing",
                            Font
                        );
                        MessageSize.Height += 5;
                    }

                    Image Rendering = GetRendering((SokobanLevel)Items[e.Index], e.Bounds.Width-10,
                        e.Bounds.Height-10-(int)MessageSize.Height);
                    e.DrawBackground();
                    e.DrawFocusRectangle();
                    if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                        e.Graphics.FillRectangle(
                            new LinearGradientBrush(
                                e.Bounds,
                                Color.White,
                                e.Index == FPlayingEditingIndex && FState == LevelListBoxState.Editing ? Color.FromArgb(255, 192, 128) :
                                e.Index == FPlayingEditingIndex && FState == LevelListBoxState.Playing ? Color.FromArgb(64, 224, 128) :
                                Color.Silver,
                                90,
                                false
                            ),
                            e.Bounds
                        );
                    if (e.Index == FPlayingEditingIndex && FState != LevelListBoxState.Null)
                    {
                        e.Graphics.FillRectangle(
                            new LinearGradientBrush(
                                new RectangleF(e.Bounds.Left+5, e.Bounds.Top+4, e.Bounds.Width-10, MessageSize.Height-3),
                                e.Index == FPlayingEditingIndex && FState == LevelListBoxState.Editing
                                    ? Color.FromArgb(255, 192, 128) : Color.FromArgb(64, 224, 128),
                                Color.White,
                                90,
                                false
                            ),
                            new RectangleF(e.Bounds.Left+5, e.Bounds.Top+5, e.Bounds.Width-10, MessageSize.Height-5)
                        );
                        e.Graphics.DrawString(
                            FState == LevelListBoxState.Editing ? "Currently editing" : "Currently playing",
                            Font,
                            new SolidBrush(Color.Black),
                            e.Bounds.Left + e.Bounds.Width/2 - MessageSize.Width/2,
                            e.Bounds.Top + 5
                        );
                    }
                    e.Graphics.DrawImage(Rendering, e.Bounds.Left + 5, e.Bounds.Top + 5 + MessageSize.Height);
                }
                else if (Items[e.Index] is string)
                {
                    e.DrawBackground();
                    e.DrawFocusRectangle();
                    string Str = (string)Items[e.Index];
                    if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                        e.Graphics.FillRectangle(new LinearGradientBrush(e.Bounds, Color.White, Color.Silver, 90, false), e.Bounds);
                    e.Graphics.DrawString(Str, Font, new SolidBrush(e.ForeColor), e.Bounds.Left + 5, e.Bounds.Top + 5);
                }
                else
                {
                    e.DrawBackground();
                    e.DrawFocusRectangle();
                    string Str = Items[e.Index].ToString();
                    if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                        e.Graphics.FillRectangle(new LinearGradientBrush(e.Bounds, Color.White, Color.Silver, 90, false), e.Bounds);
                    e.Graphics.DrawString(Str, Font, new SolidBrush(e.ForeColor), e.Bounds.Left + 5, e.Bounds.Top + 5);
                }
            }
        }

        private Image GetRendering(SokobanLevel Level, int Width, int Height)
        {
            if (FCachedRenderings.ContainsKey(Level))
                return (Image)FCachedRenderings[Level];

            Image Rendering = new Bitmap(Width, Height);
            new Renderer(Level, Width, Height, new SolidBrush(Color.Transparent)).Render(Graphics.FromImage(Rendering));
            FCachedRenderings[Level] = Rendering;
            return Rendering;
        }

        private void LevelListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index > -1 && e.Index < Items.Count && !DesignMode)
            {
                if (Items[e.Index] is SokobanLevel)
                {
                    SokobanLevel Level = (SokobanLevel)Items[e.Index];
                    e.ItemHeight = (ClientSize.Width-10)*Level.Height/Level.Width + 10;
                }
                else if (Items[e.Index] is string)
                    e.ItemHeight = (int)e.Graphics.MeasureString((string)Items[e.Index], Font).Height + 10;
                else
                    e.ItemHeight = (int)e.Graphics.MeasureString(Items[e.Index].ToString(), Font).Height + 10;

                if (e.Index == FPlayingEditingIndex && FState == LevelListBoxState.Playing)
                    e.ItemHeight += (int)e.Graphics.MeasureString("Currently playing", Font).Height + 5;
                else if (e.Index == FPlayingEditingIndex && FState == LevelListBoxState.Editing)
                    e.ItemHeight += (int)e.Graphics.MeasureString("Currently editing", Font).Height + 5;

                if (e.ItemHeight > 255)
                    e.ItemHeight = 255;
            }
        }

        public void ComeOn_RefreshItems()
        {
            RefreshItems();
        }
    }
}
