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
            get
            {
                if (FState == LevelListBoxState.Editing)
                    return FPlayingEditingIndex;
                else
                    return null;
            }
            set
            {
                if (value == null)
                    FState = LevelListBoxState.Null;
                else
                {
                    FState = LevelListBoxState.Editing;
                    FPlayingEditingIndex = value.Value;
                }
            }
        }
        public int? PlayingIndex
        {
            get
            {
                if (FState == LevelListBoxState.Playing)
                    return FPlayingEditingIndex;
                else
                    return null;
            }
            set
            {
                if (value == null)
                    FState = LevelListBoxState.Null;
                else
                {
                    FState = LevelListBoxState.Playing;
                    FPlayingEditingIndex = value.Value;
                }
            }
        }

        public LevelListBox()
        {
            this.MeasureItem += new MeasureItemEventHandler(ESLevelListBox_MeasureItem);
            this.DrawItem += new DrawItemEventHandler(ESLevelListBox_DrawItem);
            this.Resize += new EventHandler(ESLevelListBox_Resize);
            this.DrawMode = DrawMode.OwnerDrawVariable;
            FLastWidth = ClientSize.Width;
        }

        private void ESLevelListBox_Resize(object sender, EventArgs e)
        {
            if (ClientSize.Width != FLastWidth)
            {
                FLastWidth = ClientSize.Width;
                FCachedRenderings = new Hashtable();
                RefreshItems();
            }
        }

        private void ESLevelListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index > -1 && e.Index < Items.Count && !DesignMode)
            {
                if (Items[e.Index] is SokobanLevel)
                {
                    Image rendering = GetRendering ((SokobanLevel) Items[e.Index], e.Bounds.Width-10, e.Bounds.Height-10);
                    e.DrawBackground();
                    e.DrawFocusRectangle();
                    if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                        e.Graphics.FillRectangle(new LinearGradientBrush(e.Bounds, Color.White, Color.Silver, 90, false), e.Bounds);
                    e.Graphics.DrawImage(rendering, e.Bounds.Left + 5, e.Bounds.Top + 5);
                }
                else if (Items[e.Index] is string)
                {
                    e.DrawBackground();
                    e.DrawFocusRectangle();
                    string Str = (string) Items[e.Index];
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
                return (Image) FCachedRenderings[Level];

            Image Rendering = new Bitmap(Width, Height);
            new Renderer(Level, Width, Height, new SolidBrush(Color.Transparent)).Render(Graphics.FromImage(Rendering));
            FCachedRenderings[Level] = Rendering;
            return Rendering;
        }

        private void ESLevelListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index > -1 && e.Index < Items.Count && !DesignMode)
            {
                if (Items[e.Index] is SokobanLevel)
                {
                    SokobanLevel Level = (SokobanLevel) Items[e.Index];
                    e.ItemHeight = (ClientSize.Width-10)*Level.Height/Level.Width + 10;
                }
                else if (Items[e.Index] is string)
                    e.ItemHeight = (int) e.Graphics.MeasureString((string) Items[e.Index], Font).Height + 10;
                else
                    e.ItemHeight = (int) e.Graphics.MeasureString(Items[e.Index].ToString(), Font).Height + 10;

                if (e.ItemHeight > 255)
                    e.ItemHeight = 255;
            }
        }
    }
}
