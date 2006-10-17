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
        private Brush FBackgroundBrush = new SolidBrush(Color.FromArgb(240, 240, 240));

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

        public ESRenderer(SokobanLevel Level, Size ClientSize, Brush BackgroundBrush)
        {
            FBackgroundBrush = BackgroundBrush;
            Init(Level, ClientSize.Width, ClientSize.Height);
        }

        public ESRenderer(SokobanLevel Level, int ClientWidth, int ClientHeight, Brush BackgroundBrush)
        {
            FBackgroundBrush = BackgroundBrush;
            Init(Level, ClientWidth, ClientHeight);
        }

        private void Init(SokobanLevel Level, int ClientWidth, int ClientHeight)
        {
            FClientWidth = ClientWidth;
            FClientHeight = ClientHeight;
            FLevel = Level;
            int IdealWidth = FClientHeight*Level.Width/Level.Height;
            FCellWidth = FCellHeight = FClientWidth > IdealWidth 
                ? (float) FClientHeight/Level.Height
                : (float) FClientWidth/Level.Width;
            FOriginX = (int) (FClientWidth > IdealWidth ? (FClientWidth/2 - FCellWidth*Level.Width/2) : 0);
            FOriginY = (int) (FClientWidth > IdealWidth ? 0 : (FClientHeight/2 - FCellHeight*Level.Height/2));
        }

        public void Render(Graphics g)
        {
            g.FillRectangle(FBackgroundBrush, new Rectangle(0, 0, FClientWidth, FClientHeight));
            for (int x = 0; x < FLevel.Width; x++)
                for (int y = 0; y < FLevel.Height; y++)
                    RenderCellAsPartOfCompleteRender(g, x, y);
        }

        public Point CellFromPixel(Point Pixel)
        {
            return new Point(
                (int) ((Pixel.X - FOriginX) / CellWidth),
                (int) ((Pixel.Y - FOriginY) / CellHeight));
        }

        public void RenderCell(Graphics g, int pos)
        {
            RenderCell(g, FLevel.PosToX(pos), FLevel.PosToY(pos));
        }

        public void RenderCell(Graphics g, int x, int y)
        {
            g.SetClip(GetMultiCellRect(x-1, y-1, x+1, y+1));
            g.FillRectangle(FBackgroundBrush, 0, 0, FClientWidth, FClientHeight);
            for (int i = x-1; i <= x+1; i++)
                for (int j = y-1; j <= y+1; j++)
                    RenderCellAsPartOfCompleteRender(g, i, j);
        }

        private void RenderCellAsPartOfCompleteRender(Graphics g, int pos)
        {
            RenderCellAsPartOfCompleteRender(g, FLevel.PosToX(pos), FLevel.PosToY(pos));
        }

        private void RenderCellAsPartOfCompleteRender(Graphics g, int x, int y)
        {
            if (x < 0 || x >= FLevel.Width || y < 0 || y >= FLevel.Height)
                return;

            // Draw level
            switch (FLevel.Cell(x, y))
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
            switch (FLevel.Cell(x, y))
            {
                case SokobanCell.Piece:
                    DrawCell(g, x, y, SokobanImage.Piece);
                    break;
                case SokobanCell.PieceOnTarget:
                    DrawCell(g, x, y, SokobanImage.PieceOnTarget);
                    break;
            }
            // Draw Sokoban
            if (x == FLevel.SokobanX && y == FLevel.SokobanY)
                DrawCell(g, x, y, SokobanImage.Sokoban);
        }

        public void DrawCell(Graphics g, int Pos, SokobanImage ImageType)
        {
            DrawCell(g, FLevel.PosToX(Pos), FLevel.PosToY(Pos), ImageType);
        }

        public void DrawCell(Graphics g, int x, int y, SokobanImage ImageType)
        {
            g.DrawImage(GetImage(ImageType), GetCellRectForImage(x, y));
        }

        public RectangleF GetCellRectForImage(int Pos)
        {
            return GetCellRectForImage(FLevel.PosToX(Pos), FLevel.PosToY(Pos));
        }

        public RectangleF GetCellRectForImage(int x, int y)
        {
            RectangleF Src = GetCellRect(x, y);
            return new RectangleF(Src.X, Src.Y, Src.Width*1.5f+1, Src.Height*1.5f+1);
        }

        public RectangleF GetCellRect(int Pos)
        {
            return GetCellRect(FLevel.PosToX(Pos), FLevel.PosToY(Pos));
        }

        public RectangleF GetCellRect(int x, int y)
        {
            return new RectangleF(x*FCellWidth + FOriginX, y*FCellHeight + FOriginY, FCellWidth, FCellHeight);
        }

        public RectangleF GetMultiCellRect(int FromX, int FromY, int ToX, int ToY)
        {
            return new RectangleF(FromX*FCellWidth + FOriginX, FromY*FCellHeight + FOriginY,
                FCellWidth * (ToX-FromX+1), FCellHeight * (ToY-FromY+1));
        }

        private Image GetImage(SokobanImage ImageType)
        {
            switch (ImageType)
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
