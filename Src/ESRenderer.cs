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
        public SokobanLevel Lvl;
        public int OriginX, OriginY;
        public int SquareW, SquareH;

        public Bitmap RenderImage(int Width, int Height)
        {
            Bitmap img = new Bitmap(Width, Height);
            Graphics ig = Graphics.FromImage(img);

            ig.Clear(Color.FromArgb(240, 240, 240));

            for (int x = 0; x < Lvl.getSizeX(); x++)
                for (int y = 0; y < Lvl.getSizeY(); y++)
                    RenderSquare(ig, x, y);

            return img;
        }

        public void RenderSquare(Graphics g, int pos)
        {
            RenderSquare(g, Lvl.PosToX(pos), Lvl.PosToY(pos));
        }

        public void RenderSquare(Graphics g, int x, int y)
        {
            // Draw level
            switch (Lvl.getCell(x, y))
            {
                case SokobanSquare.WALL:
                    DrawSquare(g, x, y, SokobanImage.Wall);
                    break;
                case SokobanSquare.TARGET:
                    DrawSquare(g, x, y, SokobanImage.Target);
                    break;
                case SokobanSquare.PIECE_ON_TARGET:
                    DrawSquare(g, x, y, SokobanImage.TargetUnderPiece);
                    break;
            }
            // Draw piece
            switch (Lvl.getCell(x, y))
            {
                case SokobanSquare.PIECE:
                    DrawSquare(g, x, y, SokobanImage.Piece);
                    break;
                case SokobanSquare.PIECE_ON_TARGET:
                    DrawSquare(g, x, y, SokobanImage.PieceOnTarget);
                    break;
            }
            // Draw Sokoban
            if (x == Lvl.getSokobanX() && y == Lvl.getSokobanY())
                DrawSquare(g, x, y, SokobanImage.Sokoban);
        }

        public int SquareScrX(int CellX)
        {
            return CellX*SquareW + OriginX;
        }

        public int SquareScrY(int CellY)
        {
            return CellY*SquareH + OriginY;
        }

        public Rectangle SquareRect(int CellX, int CellY)
        {
            Rectangle r = new Rectangle();
            r.X = CellX*SquareW + OriginX;
            r.Y = CellY*SquareH + OriginY;
            r.Width = SquareW;
            r.Height = SquareH;
            return r;
        }

        public void DrawSquare(Graphics g, int pos, SokobanImage imageType)
        {
            DrawSquare(g, Lvl.PosToX(pos), Lvl.PosToY(pos), imageType);
        }

        public void DrawSquare(Graphics g, int x, int y, SokobanImage imageType)
        {
            Tuple2<Image, Size> tp = GetImage(imageType);
            RectangleF destRect = new RectangleF();
            destRect.X = SquareScrX(x);
            destRect.Y = SquareScrY(y);
            destRect.Width = (float)SquareW / tp.E2.Width * tp.E1.Width;
            destRect.Height = (float)SquareH / tp.E2.Height * tp.E1.Height;
            g.DrawImage(tp.E1, destRect);
        }

        private Tuple2<Image, Size> GetImage(SokobanImage imageType)
        {
            Image img = null;
            switch (imageType)
            {
                case SokobanImage.Piece:
                    img = Properties.Resources.img_piece;
                    break;
                case SokobanImage.PieceOnTarget:
                    img = Properties.Resources.img_piece_target;
                    break;
                case SokobanImage.PieceSelected:
                    img = Properties.Resources.img_piece_selected;
                    break;
                case SokobanImage.Sokoban:
                    img = Properties.Resources.img_sokoban;
                    break;
                case SokobanImage.Wall:
                    img = Properties.Resources.img_wall;
                    break;
                case SokobanImage.Target:
                    img = Properties.Resources.img_target;
                    break;
                case SokobanImage.TargetUnderPiece:
                    img = Properties.Resources.img_target_under;
                    break;
                default:
                    throw new Exception("Unknown sokoban image type");
            }
            return new Tuple2<Image, Size>(img, new Size(100, 100));
        }

    }
}
