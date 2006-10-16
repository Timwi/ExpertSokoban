using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using RT.Util;

namespace ExpertSokoban
{
    public enum SokobanImage
    {
        Wall,
        Target,
        TargetUnderPiece,
        Piece,
        PieceOnTarget,
        PieceSelected,
        Sokoban
    }

    public class ESRenderer
    {
        private SokobanLevel FLevel;
        private float FCellWidth, FCellHeight;
        private int FClientWidth, FClientHeight;
        private int FOriginX, FOriginY;
        private Color FBackgroundColour = Color.FromArgb(240, 240, 240);

        public float CellWidth { get { return FCellWidth; } }
        public float CellHeight { get { return FCellHeight; } }
        public int OriginX { get { return FOriginX; } }
        public int OriginY { get { return FOriginY; } }

        public ESRenderer(SokobanLevel Level, Size ClientSize)
        {
            Init(Level, ClientSize.Width, ClientSize.Height);
        }

        public ESRenderer(SokobanLevel Level, int ClientWidth, int ClientHeight)
        {
            Init(Level, ClientWidth, ClientHeight);
        }

        private void Init(SokobanLevel Level, int ClientWidth, int ClientHeight)
        {
            FClientWidth = ClientWidth;
            FClientHeight = ClientHeight;
            FLevel = Level;
            int IdealWidth = FClientHeight*Level.getSizeX()/Level.getSizeY();
            FCellWidth = FCellHeight = FClientWidth > IdealWidth 
                ? (float) FClientHeight/Level.getSizeY()
                : (float) FClientWidth/Level.getSizeX();
            FOriginX = (int) (FClientWidth > IdealWidth ? (FClientWidth/2 - FCellWidth*Level.getSizeX()/2) : 0);
            FOriginY = (int) (FClientWidth > IdealWidth ? 0 : (FClientHeight/2 - FCellHeight*Level.getSizeY()/2));
        }

        public void Render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(FBackgroundColour), new Rectangle(0, 0, FClientWidth, FClientHeight));
            for (int x = 0; x < FLevel.getSizeX(); x++)
                for (int y = 0; y < FLevel.getSizeY(); y++)
                    RenderCell(g, x, y);
        }

        public Point CellFromPixel(Point pixel)
        {
            return new Point(
                (int) ((pixel.X - FOriginX) / CellWidth),
                (int) ((pixel.Y - FOriginY) / CellHeight));
        }

        public void RenderCell(Graphics g, int pos)
        {
            RenderCell(g, FLevel.PosToX(pos), FLevel.PosToY(pos), false);
        }

        public void RenderCell(Graphics g, int x, int y)
        {
            RenderCell(g, x, y, false);
        }

        public void RenderCell(Graphics g, int x, int y, bool drawBackground)
        {
            // Draw level
            switch (FLevel.getCell(x, y))
            {
                case SokobanCell.Wall:
                    DrawCell(g, x, y, SokobanImage.Wall);
                    break;
                case SokobanCell.Target:
                    DrawCell(g, x, y, SokobanImage.Target);
                    break;
                case SokobanCell.PieceOnTarget:
                    DrawCell(g, x, y, SokobanImage.TargetUnderPiece);
                    break;
            }
            // Draw piece
            switch (FLevel.getCell(x, y))
            {
                case SokobanCell.Piece:
                    DrawCell(g, x, y, SokobanImage.Piece);
                    break;
                case SokobanCell.PieceOnTarget:
                    DrawCell(g, x, y, SokobanImage.PieceOnTarget);
                    break;
            }
            // Draw Sokoban
            if (x == FLevel.getSokobanX() && y == FLevel.getSokobanY())
                DrawCell(g, x, y, SokobanImage.Sokoban);
        }

        public void DrawCell(Graphics g, int pos, SokobanImage imageType)
        {
            DrawCell(g, FLevel.PosToX(pos), FLevel.PosToY(pos), imageType);
        }

        public void DrawCell(Graphics g, int x, int y, SokobanImage imageType)
        {
            g.DrawImage(GetImage(imageType), GetCellRectForImage(x, y));
        }

        public RectangleF GetCellRectForImage(int x, int y)
        {
            RectangleF tmp = GetCellRect(x, y);
            return new RectangleF(tmp.X, tmp.Y, tmp.Width*1.5f, tmp.Height*1.5f);
        }

        public RectangleF GetCellRect(int pos)
        {
            return GetCellRect(FLevel.PosToX(pos), FLevel.PosToY(pos));
        }

        public RectangleF GetCellRect(int x, int y)
        {
            RectangleF ret = new RectangleF();
            ret.X = x*FCellWidth + FOriginX;
            ret.Y = y*FCellHeight + FOriginY;
            ret.Width = FCellWidth;
            ret.Height = FCellHeight;
            return ret;
        }

        private Image GetImage(SokobanImage imageType)
        {
            switch (imageType)
            {
                case SokobanImage.Piece:
                    return Properties.Resources.ImgPiece;
                case SokobanImage.PieceOnTarget:
                    return Properties.Resources.ImgPieceTarget;
                case SokobanImage.PieceSelected:
                    return Properties.Resources.ImgPieceSelected;
                case SokobanImage.Sokoban:
                    return Properties.Resources.ImgSokoban;
                case SokobanImage.Wall:
                    return Properties.Resources.ImgWall;
                case SokobanImage.Target:
                    return Properties.Resources.ImgTarget;
                case SokobanImage.TargetUnderPiece:
                    return Properties.Resources.ImgTargetUnder;
                default:
                    throw new Exception("Unknown Sokoban image type");
            }
        }
    }
}
