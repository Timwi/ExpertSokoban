using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace ExpertSokoban
{
    public class ESLevelListBox : ListBox
    {
        public ESLevelListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawVariable;
            this.MeasureItem += new MeasureItemEventHandler(ESLevelListBox_MeasureItem);
            this.DrawItem += new DrawItemEventHandler(ESLevelListBox_DrawItem);
            this.Resize += new EventHandler(ESLevelListBox_Resize);
        }

        private void ESLevelListBox_Resize(object sender, EventArgs e)
        {
            RefreshItems();
        }

        private void ESLevelListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index > -1 && e.Index < Items.Count && !DesignMode)
            {
                if (Items[e.Index] is SokobanLevel)
                {
                    ESRenderer r = new ESRenderer((SokobanLevel) Items[e.Index], e.Bounds.Width-10, e.Bounds.Height-10);
                    e.DrawBackground();
                    e.DrawFocusRectangle();
                    if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                        e.Graphics.FillRectangle(new LinearGradientBrush(e.Bounds, Color.White, Color.Silver, 90, false), e.Bounds);
                    e.Graphics.TranslateTransform(5 + e.Bounds.Left, 5 + e.Bounds.Top);
                    r.Render(e.Graphics);
                }
                else if (Items[e.Index] is string)
                {
                    e.DrawBackground();
                    e.DrawFocusRectangle();
                    string str = (string) Items[e.Index];
                    if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                        e.Graphics.FillRectangle(new LinearGradientBrush(e.Bounds, Color.White, Color.Silver, 90, false), e.Bounds);
                    e.Graphics.DrawString(str, Font, new SolidBrush(e.ForeColor), e.Bounds.Left + 5, e.Bounds.Top + 5);
                }
                else
                {
                    e.DrawBackground();
                    e.DrawFocusRectangle();
                    string str = Items[e.Index].ToString();
                    if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                        e.Graphics.FillRectangle(new LinearGradientBrush(e.Bounds, Color.White, Color.Silver, 90, false), e.Bounds);
                    e.Graphics.DrawString(str, Font, new SolidBrush(e.ForeColor), e.Bounds.Left + 5, e.Bounds.Top + 5);
                }
            }
        }

        private void ESLevelListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index > -1 && e.Index < Items.Count && !DesignMode)
            {
                if (Items[e.Index] is SokobanLevel)
                {
                    SokobanLevel level = (SokobanLevel) Items[e.Index];
                    e.ItemHeight = (ClientSize.Width-10)*level.getSizeY()/level.getSizeX() + 10;
                }
                else if (Items[e.Index] is string)
                {
                    e.ItemHeight = (int) e.Graphics.MeasureString((string) Items[e.Index], Font).Height + 10;
                }
                else
                {
                    e.ItemHeight = (int) e.Graphics.MeasureString(Items[e.Index].ToString(), Font).Height + 10;
                }
            }
        }
    }
}
