using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using RT.Util;
using System.Drawing.Drawing2D;

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
        private Brush FBackgroundBrush = new SolidBrush(Color.FromArgb(255, 255, 192));

        public float CellWidth { get { return FCellWidth; } }
        public float CellHeight { get { return FCellHeight; } }
        public SizeF CellSize { get { return new SizeF(FCellWidth, FCellHeight); } }
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
            return new Point((int)((Pixel.X - FOriginX) / FCellWidth), (int)((Pixel.Y - FOriginY) / FCellHeight));
        }

        public void RenderCell(Graphics g, Point Pos)
        {
            RenderCell(g, Pos.X, Pos.Y);
        }

        public void RenderCell(Graphics g, int x, int y)
        {
            RectangleF Rect = CellRect(x, y);
            g.SetClip(new Rectangle((int) (Rect.Left-FCellWidth*0.25f), (int) (Rect.Top-FCellHeight*0.25f),
                (int) (2*FCellWidth), (int) (2*FCellHeight)));
            g.FillRectangle(FBackgroundBrush, 0, 0, FClientWidth, FClientHeight);
            for (int i = x-1; i <= x+1; i++)
                for (int j = y-1; j <= y+1; j++)
                    RenderCellAsPartOfCompleteRender(g, i, j);
        }

        public void RenderCells(Graphics g, Point RenderFrom, Point RenderTo)
        {
            int FromX = Math.Min(RenderFrom.X, RenderTo.X),
                FromY = Math.Min(RenderFrom.Y, RenderTo.Y),
                ToX = Math.Max(RenderFrom.X, RenderTo.X),
                ToY = Math.Max(RenderFrom.Y, RenderTo.Y);
            g.SetClip(ClippingRectForCells(RenderFrom, RenderTo));
            for (int i = FromX; i <= ToX; i++)
                for (int j = FromY; j <= ToY; j++)
                    RenderCellAsPartOfCompleteRender(g, i, j);
        }

        public RectangleF ClippingRectForCells(Point CellFrom, Point CellTo)
        {
            int FromX = Math.Min(CellFrom.X, CellTo.X),
                FromY = Math.Min(CellFrom.Y, CellTo.Y),
                ToX = Math.Max(CellFrom.X, CellTo.X),
                ToY = Math.Max(CellFrom.Y, CellTo.Y);
            RectangleF RectFrom = CellRectForImage(FromX, FromY);
            RectangleF RectTo = CellRectForImage(ToX, ToY);
            return new RectangleF(RectFrom.Left, RectTo.Top, RectTo.Right-RectFrom.Left, RectTo.Bottom-RectFrom.Top);
        }

        private void RenderCellAsPartOfCompleteRender(Graphics g, Point Pos)
        {
            RenderCellAsPartOfCompleteRender(g, Pos.X, Pos.Y);
        }

        private void RenderCellAsPartOfCompleteRender(Graphics g, int x, int y)
        {
            if (x < 0 || x >= FLevel.Width || y < 0 || y >= FLevel.Height)
                return;

            if (FLevel.Cell(x, y) != SokobanCell.Wall)
                g.InterpolationMode = InterpolationMode.High;

            // Draw level
            switch (FLevel.Cell(x, y))
            {
                case SokobanCell.Wall:
                    g.InterpolationMode = InterpolationMode.Default;
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
            if (x == FLevel.SokobanPos.X && y == FLevel.SokobanPos.Y)
                DrawCell(g, x, y, SokobanImage.Sokoban);
        }

        public void DrawCell(Graphics g, Point Pos, SokobanImage ImageType)
        {
            DrawCell(g, Pos.X, Pos.Y, ImageType);
        }

        public void DrawCell(Graphics g, int x, int y, SokobanImage ImageType)
        {
            g.DrawImage(GetImage(ImageType), CellRectForImage(x, y));
        }

        public RectangleF CellRectForImage(Point Pos)
        {
            return CellRectForImage(Pos.X, Pos.Y);
        }

        public RectangleF CellRectForImage(int x, int y)
        {
            RectangleF Src = CellRect(x, y);
            return new RectangleF(Src.X, Src.Y, Src.Width*1.5f+1, Src.Height*1.5f+1);
        }

        public RectangleF CellRect(Point Pos)
        {
            return CellRect(Pos.X, Pos.Y);
        }

        public RectangleF CellRect(int x, int y)
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

        public GraphicsPath ValidPath(Virtual2DArray<bool> Finder)
        {
            Point[][] Outlines = BitmapUtil.BoolsToPaths(Finder);
            SizeF Margin = new SizeF(FCellWidth/5, FCellHeight/5);
            SizeF Diameter = new SizeF(FCellWidth/2, FCellHeight/2);
            GraphicsPath Result = new GraphicsPath();
            for (int i = 0; i < Outlines.Length; i++)
            {
                Result.StartFigure();
                for (int j = 0; j < Outlines[i].Length; j++)
                {
                    Point Point1 = Outlines[i][j];
                    Point Point2 = Outlines[i][(j+1) % Outlines[i].Length];
                    Point Point3 = Outlines[i][(j+2) % Outlines[i].Length];
                    RectangleF Rect = CellRect(Point2);

                    int Dir1 = GetDir(Point1, Point2);
                    int Dir2 = GetDir(Point2, Point3);

                    // Rounded corners ("outer" corners)
                    if (Dir1 == 0 && Dir2 == 2) // top left corner
                        Result.AddArc(Rect.X+Margin.Width, Rect.Y+Margin.Height, Diameter.Width, Diameter.Height, 180, 90);
                    else if (Dir1 == 2 && Dir2 == 3)  // top right corner
                        Result.AddArc(Rect.X-Margin.Width-Diameter.Width, Rect.Y+Margin.Height, Diameter.Width, Diameter.Height, 270, 90);
                    else if (Dir1 == 3 && Dir2 == 1) // bottom right corner
                        Result.AddArc(Rect.X-Margin.Width-Diameter.Width, Rect.Y-Margin.Height-Diameter.Height, Diameter.Width, Diameter.Height, 0, 90);
                    else if (Dir1 == 1 && Dir2 == 0) // bottom left corner
                        Result.AddArc(Rect.X+Margin.Width, Rect.Y-Margin.Height-Diameter.Height, Diameter.Width, Diameter.Height, 90, 90);

                    // Unrounded corners ("inner" corners)
                    else if (Dir1 == 1 && Dir2 == 3) // top left corner
                        Result.AddLine(Rect.X-Margin.Width, Rect.Y-Margin.Height, Rect.X-Margin.Width, Rect.Y-Margin.Height);
                    else if (Dir1 == 0 && Dir2 == 1) // top right corner
                        Result.AddLine(Rect.X+Margin.Width, Rect.Y-Margin.Height, Rect.X+Margin.Width, Rect.Y-Margin.Height);
                    else if (Dir1 == 2 && Dir2 == 0) // bottom right corner
                        Result.AddLine(Rect.X+Margin.Width, Rect.Y+Margin.Height, Rect.X+Margin.Width, Rect.Y+Margin.Height);
                    else if (Dir1 == 3 && Dir2 == 2) // bottom left corner
                        Result.AddLine(Rect.X-Margin.Width, Rect.Y+Margin.Height, Rect.X-Margin.Width, Rect.Y+Margin.Height);
                }
                Result.CloseFigure();
            }
            return Result;
        }

        private int GetDir(Point From, Point To)
        {
            return From.X == To.X
                ? (From.Y > To.Y ? 0 : 3)
                : (From.X > To.X ? 1 : 2);
        }

        public GraphicsPath LinePath(Point StartPos, Point[] CellSequence, float DiameterX, float DiameterY)
        {
            if (CellSequence.Length < 1)
                return null;

            GraphicsPath Result = new GraphicsPath();
            RectangleF StartRect = CellRect(StartPos);
            Result.AddLine(StartRect.X + FCellWidth/2, StartRect.Y + FCellHeight/2,
                StartRect.X + FCellWidth/2, StartRect.Y + FCellHeight/2);
            DiameterX *= FCellWidth;
            DiameterY *= FCellHeight;
            for (int i = -1; i < CellSequence.Length-2; i++)
            {
                Point Pos1 = i == -1 ? StartPos : CellSequence[i];
                Point Pos2 = CellSequence[i+1];
                Point Pos3 = CellSequence[i+2];
                RectangleF Pos2Rect = CellRect(Pos2);
                PointF Center = new PointF(Pos2Rect.X + FCellWidth/2, Pos2Rect.Y + FCellHeight/2);

                int Dir1 = GetDir(Pos1, Pos2);
                int Dir2 = GetDir(Pos2, Pos3);

                // Clockwise turns
                if (Dir1 == 0 && Dir2 == 2) // top left corner
                    Result.AddArc(Center.X, Center.Y, DiameterX, DiameterY, 180, 90);
                else if (Dir1 == 2 && Dir2 == 3) // top right corner
                    Result.AddArc(Center.X-DiameterX, Center.Y, DiameterX, DiameterY, 270, 90);
                else if (Dir1 == 3 && Dir2 == 1) // bottom right corner
                    Result.AddArc(Center.X-DiameterX, Center.Y-DiameterY, DiameterX, DiameterY, 0, 90);
                else if (Dir1 == 1 && Dir2 == 0) // bottom left corner
                    Result.AddArc(Center.X, Center.Y-DiameterY, DiameterX, DiameterY, 90, 90);
                
                // Anti-clockwise turns
                else if (Dir1 == 1 && Dir2 == 3) // top left corner
                    Result.AddArc(Center.X, Center.Y, DiameterX, DiameterY, 270, -90);
                else if (Dir1 == 0 && Dir2 == 1) // top right corner
                    Result.AddArc(Center.X-DiameterX, Center.Y, DiameterX, DiameterY, 360, -90);
                else if (Dir1 == 2 && Dir2 == 0) // bottom right corner
                    Result.AddArc(Center.X-DiameterX, Center.Y-DiameterY, DiameterX, DiameterY, 90, -90);
                else if (Dir1 == 3 && Dir2 == 2) // bottom left corner
                    Result.AddArc(Center.X, Center.Y-DiameterY, DiameterX, DiameterY, 180, -90);
                else
                    Result.AddLine(Center, Center);
            }
            RectangleF EndRect = CellRect(CellSequence[CellSequence.Length-1]);
            Result.AddLine(EndRect.X + FCellWidth/2, EndRect.Y + FCellHeight/2,
                EndRect.X + FCellWidth/2, EndRect.Y + FCellHeight/2);
            return Result;
        }

    }
}
