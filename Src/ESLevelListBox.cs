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
        private Hashtable CachedRenderings;
        private int LastWidth;

        public LevelListBox()
        {
            this.MeasureItem += new MeasureItemEventHandler(ESLevelListBox_MeasureItem);
            this.DrawItem += new DrawItemEventHandler(ESLevelListBox_DrawItem);
            this.Resize += new EventHandler(ESLevelListBox_Resize);
            CachedRenderings = new Hashtable();
            this.DrawMode = DrawMode.OwnerDrawVariable;
            LastWidth = ClientSize.Width;
        }

        private void ESLevelListBox_Resize(object sender, EventArgs e)
        {
            if (ClientSize.Width != LastWidth)
            {
                LastWidth = ClientSize.Width;
                CachedRenderings = new Hashtable();
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
            if (CachedRenderings.ContainsKey(Level))
                return (Image) CachedRenderings[Level];

            Image Rendering = new Bitmap(Width, Height);
            new Renderer(Level, Width, Height, new SolidBrush(Color.Transparent)).Render(Graphics.FromImage(Rendering));
            CachedRenderings[Level] = Rendering;
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
