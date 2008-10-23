using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using RT.Util;
using System.Drawing.Drawing2D;
using RT.Util.Drawing;
using RT.Util.Collections;

namespace ExpertSokoban
{
    /// <summary>
    /// Enumerates the various images used in visually rendering a SokobanLevel.
    /// </summary>
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

    /// <summary>
    /// Encapsulates the fields and methods used in visually rendering a SokobanLevel.
    /// </summary>
    public class Renderer
    {
        /// <summary>
        /// The SokobanLevel that is being rendered.
        /// </summary>
        private SokobanLevel FLevel;

        /// <summary>
        /// The width of a cell in pixel.
        /// </summary>
        private int FCellWidth;
        
        /// <summary>
        /// The height of a cell in pixel.
        /// </summary>
        private int FCellHeight;

        /// <summary>
        /// The width of the rectangle into which the rendered level should fit.
        /// </summary>
        private int FClientWidth;

        /// <summary>
        /// The height of the rectangle into which the rendered level should fit.
        /// </summary>
        private int FClientHeight;

        /// <summary>
        /// The X co-ordinate of the top-left corner of where the level is rendered.
        /// </summary>
        private int FOriginX;

        /// <summary>
        /// The Y co-ordinate of the top-left corner of where the level is rendered.
        /// </summary>
        private int FOriginY;

        /// <summary>
        /// The brush used to fill the background of the level.
        /// </summary>
        private Brush FBackgroundBrush = new SolidBrush(Color.FromArgb(255, 255, 192));

        /// <summary>
        /// (read-only) Returns the width of a single cell in pixel.
        /// </summary>
        public int CellWidth { get { return FCellWidth; } }

        /// <summary>
        /// (read-only) Returns the hright of a single cell in pixel.
        /// </summary>
        public int CellHeight { get { return FCellHeight; } }

        /// <summary>
        /// (read-only) Returns the size of a single cell in pixel.
        /// </summary>
        public Size CellSize { get { return new Size(FCellWidth, FCellHeight); } }

        /// <summary>
        /// (read-only) Returns the X co-ordinate of the top-left corner of where the level is rendered.
        /// </summary>
        public int OriginX { get { return FOriginX; } }

        /// <summary>
        /// (read-only) Returns the Y co-ordinate of the top-left corner of where the level is rendered.
        /// </summary>
        public int OriginY { get { return FOriginY; } }

        #region Scaled image caching

        /// <summary>
        /// Holds cached resized images. This is shared among all instances
        /// of the Renderer.
        /// </summary>
        private static Dictionary<Size, Dictionary<SokobanImage, Bitmap>> CachedImage = new Dictionary<Size, Dictionary<SokobanImage, Bitmap>>();

        /// <summary>
        /// Holds the last time a particular size was used.
        /// </summary>
        private static Dictionary<Size, DateTime> CachedImageAge = new Dictionary<Size, DateTime>();

        /// <summary>
        /// Defines the number of sizes cached. The oldest set of images will be
        /// discarded if the limit is exceeded.
        /// </summary>
        private const int CachedMaxSizes = 20;

        #endregion

        /// <summary>
        /// Constructs a Renderer object.
        /// </summary>
        /// <param name="Level">The SokobanLevel that is to be rendered.</param>
        /// <param name="ClientSize">The size (in pixels) of the client area into which the level is to be rendered.</param>
        public Renderer(SokobanLevel Level, Size ClientSize)
        {
            Init(Level, ClientSize.Width, ClientSize.Height);
        }

        /// <summary>
        /// Constructs a Renderer object.
        /// </summary>
        /// <param name="Level">The SokobanLevel that is to be rendered.</param>
        /// <param name="ClientWidth">The width (in pixels) of the client area into which the level is to be rendered.</param>
        /// <param name="ClientHeight">The height (in pixels) of the client area into which the level is to be rendered.</param>
        public Renderer(SokobanLevel Level, int ClientWidth, int ClientHeight)
        {
            Init(Level, ClientWidth, ClientHeight);
        }

        /// <summary>
        /// Constructs a Renderer object.
        /// </summary>
        /// <param name="Level">The SokobanLevel that is to be rendered.</param>
        /// <param name="ClientSize">The size (in pixels) of the client area into which the level is to be rendered.</param>
        /// <param name="BackgroundBrush">The Brush used to fill the background of the level.</param>
        public Renderer(SokobanLevel Level, Size ClientSize, Brush BackgroundBrush)
        {
            FBackgroundBrush = BackgroundBrush;
            Init(Level, ClientSize.Width, ClientSize.Height);
        }

        /// <summary>
        /// Constructs a Renderer object.
        /// </summary>
        /// <param name="Level">The SokobanLevel that is to be rendered.</param>
        /// <param name="ClientWidth">The width (in pixels) of the client area into which the level is to be rendered.</param>
        /// <param name="ClientHeight">The height (in pixels) of the client area into which the level is to be rendered.</param>
        /// <param name="BackgroundBrush">The Brush used to fill the background of the level.</param>
        public Renderer(SokobanLevel Level, int ClientWidth, int ClientHeight, Brush BackgroundBrush)
        {
            FBackgroundBrush = BackgroundBrush;
            Init(Level, ClientWidth, ClientHeight);
        }

        /// <summary>
        /// Initialisation. Called by each of the overloaded constructors to perform the
        /// common initialisation work.
        /// </summary>
        /// <param name="Level">The SokobanLevel that is to be rendered.</param>
        /// <param name="ClientWidth">The width (in pixels) of the client area into which the level is to be rendered.</param>
        /// <param name="ClientHeight">The height (in pixels) of the client area into which the level is to be rendered.</param>
        private void Init(SokobanLevel Level, int ClientWidth, int ClientHeight)
        {
            FClientWidth = ClientWidth;
            FClientHeight = ClientHeight;
            FLevel = Level;
            int IdealWidth = FClientHeight*Level.Width/Level.Height;
            FCellWidth = FCellHeight =
                FClientWidth > IdealWidth ? FClientHeight/Level.Height : FClientWidth/Level.Width;

            FOriginX = (int)(FClientWidth/2f - FCellWidth*Level.Width/2f);
            FOriginY = (int)(FClientHeight/2f - FCellHeight*Level.Height/2f);
        }

        /// <summary>
        /// Renders the level.
        /// </summary>
        /// <param name="g">Graphics object to render the level onto.</param>
        public void Render(Graphics g)
        {
            g.FillRectangle(FBackgroundBrush, new Rectangle(0, 0, FClientWidth, FClientHeight));
            for (int x = 0; x < FLevel.Width; x++)
                for (int y = 0; y < FLevel.Height; y++)
                    RenderCellAsPartOfCompleteRender(g, x, y);
        }

        /// <summary>
        /// Returns the co-ordinates of the cell that contains the given pixel.
        /// </summary>
        /// <param name="Pixel">Co-ordinates of a pixel.</param>
        /// <returns>Co-ordinates of a cell.</returns>
        public Point CellFromPixel(Point Pixel)
        {
            return new Point((int)((Pixel.X - FOriginX) / FCellWidth), (int)((Pixel.Y - FOriginY) / FCellHeight));
        }

        /// <summary>
        /// Renders a particular cell (while taking the necessary rendering work for all
        /// adjacent cells into account).
        /// </summary>
        /// <param name="g">Graphics object to render the cell onto.</param>
        /// <param name="Pos">Co-ordinates of the cell to render.</param>
        public void RenderCell(Graphics g, Point Pos)
        {
            RenderCell(g, Pos.X, Pos.Y);
        }

        /// <summary>
        /// Renders a particular cell (while taking the necessary rendering work for all
        /// adjacent cells into account).
        /// </summary>
        /// <param name="g">Graphics object to render the cell onto.</param>
        /// <param name="x">X co-ordinate of the cell to render.</param>
        /// <param name="y">Y co-ordinate of the cell to render.</param>
        public void RenderCell(Graphics g, int x, int y)
        {
            Rectangle Rect = CellRect(x, y);
            g.SetClip(new Rectangle((int) (Rect.Left-FCellWidth*0.25f), (int) (Rect.Top-FCellHeight*0.25f),
                (int) (2*FCellWidth), (int) (2*FCellHeight)));
            g.FillRectangle(FBackgroundBrush, 0, 0, FClientWidth, FClientHeight);
            for (int i = x-1; i <= x+1; i++)
                for (int j = y-1; j <= y+1; j++)
                    RenderCellAsPartOfCompleteRender(g, i, j);
        }

        /// <summary>
        /// Renders a rectangular area of cells (while taking the necessary rendering
        /// work for all adjacent cells into account).
        /// </summary>
        /// <param name="g">Graphics object to render the cell onto.</param>
        /// <param name="RenderFrom">Co-ordinates of the top-left cell of the rectangle to render.</param>
        /// <param name="RenderTo">Co-ordinates of the bottom-right cell of the rectangle to render.</param>
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

        /// <summary>
        /// Returns the pixel rectangle that corresponds to the given rectangular area
        /// of cells.
        /// </summary>
        /// <param name="CellFrom">Co-ordinates of the top-left cell of the rectangle.</param>
        /// <param name="CellTo">Co-ordinates of the bottom-right cell of the rectangle.</param>
        /// <returns></returns>
        public Rectangle ClippingRectForCells(Point CellFrom, Point CellTo)
        {
            int FromX = Math.Min(CellFrom.X, CellTo.X),
                FromY = Math.Min(CellFrom.Y, CellTo.Y),
                ToX = Math.Max(CellFrom.X, CellTo.X),
                ToY = Math.Max(CellFrom.Y, CellTo.Y);
            Rectangle RectFrom = CellRectForImage(FromX, FromY);
            Rectangle RectTo = CellRectForImage(ToX, ToY);
            return new Rectangle(RectFrom.Left, RectTo.Top, RectTo.Right-RectFrom.Left, RectTo.Bottom-RectFrom.Top);
        }

        /// <summary>
        /// Renders a cell without taking the adjacent cells into account.
        /// </summary>
        /// <param name="g">Graphics object to render onto.</param>
        /// <param name="Pos">Co-ordinates of the cell to render.</param>
        private void RenderCellAsPartOfCompleteRender(Graphics g, Point Pos)
        {
            RenderCellAsPartOfCompleteRender(g, Pos.X, Pos.Y);
        }

        /// <summary>
        /// Renders a cell without taking the adjacent cells into account.
        /// </summary>
        /// <param name="g">Graphics object to render onto.</param>
        /// <param name="x">X co-ordinate of the cell to render.</param>
        /// <param name="y">Y co-ordinate of the cell to render.</param>
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

        /// <summary>
        /// Draws a specified image into the specified cell.
        /// </summary>
        /// <param name="g">Graphics object to render onto.</param>
        /// <param name="Pos">Co-ordinates of the cell to render into.</param>
        /// <param name="ImageType">Identifies the image to be drawn.</param>
        public void DrawCell(Graphics g, Point Pos, SokobanImage ImageType)
        {
            DrawCell(g, Pos.X, Pos.Y, ImageType);
        }

        /// <summary>
        /// Draws a specified image into the specified cell.
        /// </summary>
        /// <param name="g">Graphics object to render onto.</param>
        /// <param name="x">X co-ordinate of the cell to render into.</param>
        /// <param name="y">Y co-ordinate of the cell to render into.</param>
        /// <param name="ImageType">Identifies the image to be drawn.</param>
        public void DrawCell(Graphics g, int x, int y, SokobanImage ImageType)
        {
            Rectangle rect = CellRect(x, y);
            g.DrawImageUnscaled(GetScaledImage(ImageType), rect.Left, rect.Top);
        }

        /// <summary>
        /// Returns a pixel rectangle that corresponds to the area occupied by the
        /// image that represents a given cell. This is NOT the outline of the cell
        /// itself; the image extends beyond the bottom-right of the cell by a factor
        /// of 1.5. In order to retrieve the outline of the cell itself, use CellRect().
        /// </summary>
        /// <param name="Pos">Co-ordinates of the cell.</param>
        public Rectangle CellRectForImage(Point Pos)
        {
            return CellRectForImage(Pos.X, Pos.Y);
        }

        /// <summary>
        /// Returns a pixel rectangle that corresponds to the area occupied by the
        /// image that represents a given cell. This is NOT the outline of the cell
        /// itself; the image extends beyond the bottom-right of the cell by a factor
        /// of 1.5. In order to retrieve the outline of the cell itself, use CellRect().
        /// </summary>
        /// <param name="x">X co-ordinate of the cell.</param>
        /// <param name="y">Y co-ordinate of the cell.</param>
        public Rectangle CellRectForImage(int x, int y)
        {
            Rectangle Src = CellRect(x, y);
            return new Rectangle(Src.X, Src.Y, (int)(Src.Width*1.5f), (int)(Src.Height*1.5f));
        }

        /// <summary>
        /// Returns a pixel rectangle that corresponds to the area logically occupied by
        /// the given cell. This rectangle is SMALLER than the image that is drawn to
        /// represent the cell; in order to retrieve the rectangle for the image, use
        /// CellRectForImage().
        /// </summary>
        /// <param name="Pos">Co-ordinates of the cell.</param>
        public Rectangle CellRect(Point Pos)
        {
            return CellRect(Pos.X, Pos.Y);
        }

        /// <summary>
        /// Returns a pixel rectangle that corresponds to the area logically occupied by
        /// the given cell. This rectangle is SMALLER than the image that is drawn to
        /// represent the cell; in order to retrieve the rectangle for the image, use
        /// CellRectForImage().
        /// </summary>
        /// <param name="x">X co-ordinate of the cell.</param>
        /// <param name="y">Y co-ordinate of the cell.</param>
        public Rectangle CellRect(int x, int y)
        {
            return new Rectangle(x*FCellWidth + FOriginX, y*FCellHeight + FOriginY, FCellWidth, FCellHeight);
        }

        /// <summary>
        /// Returns the original (unscaled) image of the specified type.
        /// </summary>
        private Image GetImage(SokobanImage ImageType)
        {
            switch (ImageType)
            {
                case SokobanImage.Piece:
                    return Properties.Resources.Skin_Piece;
                case SokobanImage.PieceOnTarget:
                    return Properties.Resources.Skin_PieceTarget;
                case SokobanImage.PieceSelected:
                    return Properties.Resources.Skin_PieceSelected;
                case SokobanImage.Sokoban:
                    return Properties.Resources.Skin_Sokoban;
                case SokobanImage.Wall:
                    return Properties.Resources.Skin_Wall;
                case SokobanImage.Target:
                    return Properties.Resources.Skin_Target;
                case SokobanImage.TargetUnderPiece:
                    return Properties.Resources.Skin_TargetUnder;
                default:
                    throw new Exception("Unknown Sokoban image type");
            }
        }

        /// <summary>
        /// Returns the specified image scaled for the current cell size.
        /// Images are cached for efficiency.
        /// </summary>
        private Image GetScaledImage(SokobanImage ImageType)
        {
            // If we have it - return it
            Size sz = new Size(CellWidth, CellHeight);
            if (CachedImage.ContainsKey(sz))
            {
                CachedImageAge[sz] = DateTime.Now;
                return CachedImage[sz][ImageType];
            }

            // Discard the oldest version if too many
            if (CachedImage.Count > CachedMaxSizes)
            {
                KeyValuePair<Size, DateTime> last = new KeyValuePair<Size, DateTime>(new Size(), DateTime.Now);
                foreach (KeyValuePair<Size, DateTime> kvp in CachedImageAge)
                    if (kvp.Value < last.Value)
                        last = kvp;
                CachedImage.Remove(last.Key);
                CachedImageAge.Remove(last.Key);
            }

            // Create scaled versions
            CachedImage[sz] = new Dictionary<SokobanImage, Bitmap>();
            RectangleF dest = new RectangleF(0, 0, CellWidth*1.5f+1f, CellHeight*1.5f+1f);
            Graphics g;
            foreach (SokobanImage si in Enum.GetValues(typeof(SokobanImage)))
            {
                CachedImage[sz][si] = new Bitmap((int)dest.Width+1, (int)dest.Height+1);
                g = Graphics.FromImage(CachedImage[sz][si]);
                if (si == SokobanImage.Wall)
                    g.InterpolationMode = InterpolationMode.Bicubic;
                else
                    g.InterpolationMode = InterpolationMode.High;
                g.DrawImage(GetImage(si), dest);
            }

            // Return the requested one
            CachedImageAge[sz] = DateTime.Now;
            return CachedImage[sz][ImageType];
        }

        /// <summary>
        /// Given a MoveFinder or PushFinder, returns a GraphicsPath object that can be
        /// used to visualise the area deemed "valid" by the relevant Finder.
        /// </summary>
        /// <param name="Finder">A MoveFinder or PushFinder object.</param>
        public GraphicsPath ValidPath(Virtual2DArray<bool> Finder)
        {
            Point[][] Outlines = GraphicsUtil.BoolsToPaths(Finder);
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
                    Rectangle Rect = CellRect(Point2);

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

        /// <summary>
        /// Returns the value 0, 1, 2 or 3 if the cell specified by the second parameter
        /// is above, left of, right of and below the cell specified by the first
        /// parameter, respectively. If neither of the four is the case, the result is
        /// undefined.
        /// </summary>
        private int GetDir(Point From, Point To)
        {
            return From.X == To.X
                ? (From.Y > To.Y ? 0 : 3)
                : (From.X > To.X ? 1 : 2);
        }

        /// <summary>
        /// Returns a GraphicsPath object that can be used to visualise a path through
        /// a sequence of cells. This can be a move path or a push path.
        /// </summary>
        /// <param name="StartPos">Starting position for the path.</param>
        /// <param name="CellSequence">Sequence of cells for the path, not including the
        /// starting position. Each cell must be directly adjacent to the previous cell,
        /// and the first cell must be directly adjacent to the cell specified by
        /// StartPos. Otherwise, the behaviour is undefined.</param>
        /// <param name="DiameterX">Proportion of the cell width that is used to round
        /// the corners.</param>
        /// <param name="DiameterY">Proportion of the cell height that is used to round
        /// the corners.</param>
        public GraphicsPath LinePath(Point StartPos, Point[] CellSequence, float DiameterX, float DiameterY)
        {
            if (CellSequence.Length < 1)
                return null;

            GraphicsPath Result = new GraphicsPath();
            Rectangle StartRect = CellRect(StartPos);
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
            Rectangle EndRect = CellRect(CellSequence[CellSequence.Length-1]);
            Result.AddLine(EndRect.X + FCellWidth/2, EndRect.Y + FCellHeight/2,
                EndRect.X + FCellWidth/2, EndRect.Y + FCellHeight/2);
            return Result;
        }

        /// <summary>
        /// Returns a GraphicsPath object that can be used to visualise a selection
        /// cursor that encompasses one cell.
        /// </summary>
        /// <param name="Cell">Cell to generate a cursor for.</param>
        public GraphicsPath SelectorPath(Point Cell)
        {
            GraphicsPath Result = new GraphicsPath();
            Rectangle Rect = CellRect(Cell);
            Result.AddArc(Rect.Left-CellWidth/10, Rect.Top-CellWidth/10, CellWidth/5, CellHeight/5, 180, 90);
            Result.AddArc(Rect.Right-CellWidth/10, Rect.Top-CellWidth/10, CellWidth/5, CellHeight/5, 270, 90);
            Result.AddArc(Rect.Right-CellWidth/10, Rect.Bottom-CellWidth/10, CellWidth/5, CellHeight/5, 0, 90);
            Result.AddArc(Rect.Left-CellWidth/10, Rect.Bottom-CellWidth/10, CellWidth/5, CellHeight/5, 90, 90);
            Result.CloseAllFigures();
            return Result;
        }
    }
}
