using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using RT.Util.Collections;
using RT.Util.Drawing;

namespace ExpertSokoban
{
    /// <summary>
    /// Enumerates the various images used in visually rendering a SokobanLevel.
    /// </summary>
    public enum SokobanImage
    {
        /// <summary>Indicates a wall.</summary>
        Wall,
        /// <summary>Indicates a target.</summary>
        Target,
        /// <summary>Indicates a target that is under a piece.</summary>
        TargetUnderPiece,
        /// <summary>Indicates a piece.</summary>
        Piece,
        /// <summary>Indicates a piece that is on a target.</summary>
        PieceOnTarget,
        /// <summary>Indicates a selected piece.</summary>
        PieceSelected,
        /// <summary>Indicates the Sokoban.</summary>
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
        private SokobanLevel _level;

        /// <summary>
        /// The width of a cell in pixel.
        /// </summary>
        private int _cellWidth;

        /// <summary>
        /// The height of a cell in pixel.
        /// </summary>
        private int _cellHeight;

        /// <summary>
        /// The width of the rectangle into which the rendered level should fit.
        /// </summary>
        private int _clientWidth;

        /// <summary>
        /// The height of the rectangle into which the rendered level should fit.
        /// </summary>
        private int _clientHeight;

        /// <summary>
        /// The X co-ordinate of the top-left corner of where the level is rendered.
        /// </summary>
        private int _originX;

        /// <summary>
        /// The Y co-ordinate of the top-left corner of where the level is rendered.
        /// </summary>
        private int _originY;

        /// <summary>
        /// The brush used to fill the background of the level.
        /// </summary>
        private Brush _backgroundBrush = new SolidBrush(Color.FromArgb(255, 255, 192));

        /// <summary>
        /// (read-only) Returns the width of a single cell in pixel.
        /// </summary>
        public int CellWidth { get { return _cellWidth; } }

        /// <summary>
        /// (read-only) Returns the hright of a single cell in pixel.
        /// </summary>
        public int CellHeight { get { return _cellHeight; } }

        /// <summary>
        /// (read-only) Returns the size of a single cell in pixel.
        /// </summary>
        public Size CellSize { get { return new Size(_cellWidth, _cellHeight); } }

        /// <summary>
        /// (read-only) Returns the X co-ordinate of the top-left corner of where the level is rendered.
        /// </summary>
        public int OriginX { get { return _originX; } }

        /// <summary>
        /// (read-only) Returns the Y co-ordinate of the top-left corner of where the level is rendered.
        /// </summary>
        public int OriginY { get { return _originY; } }

        #region Scaled image caching

        /// <summary>
        /// Holds cached resized images. This is shared among all instances
        /// of the Renderer.
        /// </summary>
        private static Dictionary<Size, Dictionary<SokobanImage, Bitmap>> _cachedImage = new Dictionary<Size, Dictionary<SokobanImage, Bitmap>>();

        /// <summary>
        /// Holds the last time a particular size was used.
        /// </summary>
        private static Dictionary<Size, DateTime> _cachedImageAge = new Dictionary<Size, DateTime>();

        /// <summary>
        /// Defines the number of sizes cached. The oldest set of images will be
        /// discarded if the limit is exceeded.
        /// </summary>
        private const int _cachedMaxSizes = 20;

        #endregion

        /// <summary>
        /// Constructs a Renderer object.
        /// </summary>
        /// <param name="level">The SokobanLevel that is to be rendered.</param>
        /// <param name="clientSize">The size (in pixels) of the client area into which the level is to be rendered.</param>
        public Renderer(SokobanLevel level, Size clientSize)
        {
            initialize(level, clientSize.Width, clientSize.Height);
        }

        /// <summary>
        /// Constructs a Renderer object.
        /// </summary>
        /// <param name="level">The SokobanLevel that is to be rendered.</param>
        /// <param name="clientWidth">The width (in pixels) of the client area into which the level is to be rendered.</param>
        /// <param name="clientHeight">The height (in pixels) of the client area into which the level is to be rendered.</param>
        public Renderer(SokobanLevel level, int clientWidth, int clientHeight)
        {
            initialize(level, clientWidth, clientHeight);
        }

        /// <summary>
        /// Constructs a Renderer object.
        /// </summary>
        /// <param name="level">The SokobanLevel that is to be rendered.</param>
        /// <param name="clientSize">The size (in pixels) of the client area into which the level is to be rendered.</param>
        /// <param name="backgroundBrush">The Brush used to fill the background of the level.</param>
        public Renderer(SokobanLevel level, Size clientSize, Brush backgroundBrush)
        {
            _backgroundBrush = backgroundBrush;
            initialize(level, clientSize.Width, clientSize.Height);
        }

        /// <summary>
        /// Constructs a Renderer object.
        /// </summary>
        /// <param name="level">The SokobanLevel that is to be rendered.</param>
        /// <param name="clientWidth">The width (in pixels) of the client area into which the level is to be rendered.</param>
        /// <param name="clientHeight">The height (in pixels) of the client area into which the level is to be rendered.</param>
        /// <param name="backgroundBrush">The Brush used to fill the background of the level.</param>
        public Renderer(SokobanLevel level, int clientWidth, int clientHeight, Brush backgroundBrush)
        {
            _backgroundBrush = backgroundBrush;
            initialize(level, clientWidth, clientHeight);
        }

        /// <summary>
        /// Initialisation. Called by each of the overloaded constructors to perform the
        /// common initialisation work.
        /// </summary>
        /// <param name="level">The SokobanLevel that is to be rendered.</param>
        /// <param name="clientWidth">The width (in pixels) of the client area into which the level is to be rendered.</param>
        /// <param name="clientHeight">The height (in pixels) of the client area into which the level is to be rendered.</param>
        private void initialize(SokobanLevel level, int clientWidth, int clientHeight)
        {
            _clientWidth = clientWidth;
            _clientHeight = clientHeight;
            _level = level;
            int idealWidth = _clientHeight * level.Width / level.Height;
            _cellWidth = _cellHeight =
                _clientWidth > idealWidth ? _clientHeight / level.Height : _clientWidth / level.Width;

            _originX = (int) (_clientWidth / 2f - _cellWidth * level.Width / 2f);
            _originY = (int) (_clientHeight / 2f - _cellHeight * level.Height / 2f);
        }

        /// <summary>Renders the level.</summary>
        /// <param name="g">Graphics object to render the level onto.</param>
        public void Render(Graphics g)
        {
            g.FillRectangle(_backgroundBrush, new Rectangle(0, 0, _clientWidth, _clientHeight));
            for (int x = 0; x < _level.Width; x++)
                for (int y = 0; y < _level.Height; y++)
                    renderCellAsPartOfCompleteRender(g, x, y);
        }

        /// <summary>Returns the co-ordinates of the cell that contains the given pixel.</summary>
        /// <param name="pixel">Co-ordinates of a pixel.</param>
        /// <returns>Co-ordinates of a cell.</returns>
        public Point CellFromPixel(Point pixel)
        {
            return new Point((int) ((pixel.X - _originX) / _cellWidth), (int) ((pixel.Y - _originY) / _cellHeight));
        }

        /// <summary>
        /// Renders a particular cell (while taking the necessary rendering work for all
        /// adjacent cells into account).
        /// </summary>
        /// <param name="g">Graphics object to render the cell onto.</param>
        /// <param name="pos">Co-ordinates of the cell to render.</param>
        public void RenderCell(Graphics g, Point pos)
        {
            RenderCell(g, pos.X, pos.Y);
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
            Rectangle rect = CellRect(x, y);
            g.SetClip(new Rectangle((int) (rect.Left - _cellWidth * 0.25f), (int) (rect.Top - _cellHeight * 0.25f),
                (int) (2 * _cellWidth), (int) (2 * _cellHeight)));
            g.FillRectangle(_backgroundBrush, 0, 0, _clientWidth, _clientHeight);
            for (int i = x - 1; i <= x + 1; i++)
                for (int j = y - 1; j <= y + 1; j++)
                    renderCellAsPartOfCompleteRender(g, i, j);
        }

        /// <summary>
        /// Renders a rectangular area of cells (while taking the necessary rendering
        /// work for all adjacent cells into account).
        /// </summary>
        /// <param name="g">Graphics object to render the cell onto.</param>
        /// <param name="renderFrom">Co-ordinates of the top-left cell of the rectangle to render.</param>
        /// <param name="renderTo">Co-ordinates of the bottom-right cell of the rectangle to render.</param>
        public void RenderCells(Graphics g, Point renderFrom, Point renderTo)
        {
            int fromX = Math.Min(renderFrom.X, renderTo.X);
            int fromY = Math.Min(renderFrom.Y, renderTo.Y);
            int toX = Math.Max(renderFrom.X, renderTo.X);
            int toY = Math.Max(renderFrom.Y, renderTo.Y);
            g.SetClip(ClippingRectForCells(renderFrom, renderTo));
            for (int i = fromX; i <= toX; i++)
                for (int j = fromY; j <= toY; j++)
                    renderCellAsPartOfCompleteRender(g, i, j);
        }

        /// <summary>
        /// Returns the pixel rectangle that corresponds to the given rectangular area
        /// of cells.
        /// </summary>
        /// <param name="cellFrom">Co-ordinates of the top-left cell of the rectangle.</param>
        /// <param name="cellTo">Co-ordinates of the bottom-right cell of the rectangle.</param>
        /// <returns></returns>
        public Rectangle ClippingRectForCells(Point cellFrom, Point cellTo)
        {
            int fromX = Math.Min(cellFrom.X, cellTo.X);
            int fromY = Math.Min(cellFrom.Y, cellTo.Y);
            int toX = Math.Max(cellFrom.X, cellTo.X);
            int toY = Math.Max(cellFrom.Y, cellTo.Y);
            Rectangle rectFrom = CellRectForImage(fromX, fromY);
            Rectangle rectTo = CellRectForImage(toX, toY);
            return new Rectangle(rectFrom.Left, rectTo.Top, rectTo.Right - rectFrom.Left, rectTo.Bottom - rectFrom.Top);
        }

        /// <summary>
        /// Renders a cell without taking the adjacent cells into account.
        /// </summary>
        /// <param name="g">Graphics object to render onto.</param>
        /// <param name="pos">Co-ordinates of the cell to render.</param>
        private void renderCellAsPartOfCompleteRender(Graphics g, Point pos)
        {
            renderCellAsPartOfCompleteRender(g, pos.X, pos.Y);
        }

        /// <summary>Renders a cell without taking the adjacent cells into account.</summary>
        /// <param name="g">Graphics object to render onto.</param>
        /// <param name="x">X co-ordinate of the cell to render.</param>
        /// <param name="y">Y co-ordinate of the cell to render.</param>
        private void renderCellAsPartOfCompleteRender(Graphics g, int x, int y)
        {
            if (x < 0 || x >= _level.Width || y < 0 || y >= _level.Height)
                return;

            if (_level.Cell(x, y) != SokobanCell.Wall)
                g.InterpolationMode = InterpolationMode.High;

            // Draw level
            switch (_level.Cell(x, y))
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
            switch (_level.Cell(x, y))
            {
                case SokobanCell.Piece:
                    DrawCell(g, x, y, SokobanImage.Piece);
                    break;
                case SokobanCell.PieceOnTarget:
                    DrawCell(g, x, y, SokobanImage.PieceOnTarget);
                    break;
            }
            // Draw Sokoban
            if (x == _level.SokobanPos.X && y == _level.SokobanPos.Y)
                DrawCell(g, x, y, SokobanImage.Sokoban);
        }

        /// <summary>Draws a specified image into the specified cell.</summary>
        /// <param name="g">Graphics object to render onto.</param>
        /// <param name="pos">Co-ordinates of the cell to render into.</param>
        /// <param name="imageType">Identifies the image to be drawn.</param>
        public void DrawCell(Graphics g, Point pos, SokobanImage imageType)
        {
            DrawCell(g, pos.X, pos.Y, imageType);
        }

        /// <summary>
        /// Draws a specified image into the specified cell.
        /// </summary>
        /// <param name="g">Graphics object to render onto.</param>
        /// <param name="x">X co-ordinate of the cell to render into.</param>
        /// <param name="y">Y co-ordinate of the cell to render into.</param>
        /// <param name="imageType">Identifies the image to be drawn.</param>
        public void DrawCell(Graphics g, int x, int y, SokobanImage imageType)
        {
            Rectangle rect = CellRect(x, y);
            g.DrawImageUnscaled(getScaledImage(imageType), rect.Left, rect.Top);
        }

        /// <summary>
        /// Returns a pixel rectangle that corresponds to the area occupied by the
        /// image that represents a given cell. This is NOT the outline of the cell
        /// itself; the image extends beyond the bottom-right of the cell by a factor
        /// of 1.5. In order to retrieve the outline of the cell itself, use CellRect().
        /// </summary>
        /// <param name="pos">Co-ordinates of the cell.</param>
        public Rectangle CellRectForImage(Point pos)
        {
            return CellRectForImage(pos.X, pos.Y);
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
            Rectangle src = CellRect(x, y);
            return new Rectangle(src.X, src.Y, (int) (src.Width * 1.5f), (int) (src.Height * 1.5f));
        }

        /// <summary>
        /// Returns a pixel rectangle that corresponds to the area logically occupied by
        /// the given cell. This rectangle is SMALLER than the image that is drawn to
        /// represent the cell; in order to retrieve the rectangle for the image, use
        /// <see cref="CellRectForImage(Point)"/>.
        /// </summary>
        /// <param name="pos">Co-ordinates of the cell.</param>
        public Rectangle CellRect(Point pos)
        {
            return CellRect(pos.X, pos.Y);
        }

        /// <summary>
        /// Returns a pixel rectangle that corresponds to the area logically occupied by
        /// the given cell. This rectangle is SMALLER than the image that is drawn to
        /// represent the cell; in order to retrieve the rectangle for the image, use
        /// <see cref="CellRectForImage(int,int)"/>.
        /// </summary>
        /// <param name="x">X co-ordinate of the cell.</param>
        /// <param name="y">Y co-ordinate of the cell.</param>
        public Rectangle CellRect(int x, int y)
        {
            return new Rectangle(x * _cellWidth + _originX, y * _cellHeight + _originY, _cellWidth, _cellHeight);
        }

        /// <summary>Returns the original (unscaled) image of the specified type.</summary>
        private Image getImage(SokobanImage imageType)
        {
            switch (imageType)
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

        /// <summary>Returns the specified image scaled for the current cell size. Images are cached for efficiency.</summary>
        private Image getScaledImage(SokobanImage imageType)
        {
            // If we have it - return it
            Size sz = new Size(CellWidth, CellHeight);
            if (_cachedImage.ContainsKey(sz))
            {
                _cachedImageAge[sz] = DateTime.Now;
                return _cachedImage[sz][imageType];
            }

            // Discard the oldest version if too many
            if (_cachedImage.Count > _cachedMaxSizes)
            {
                KeyValuePair<Size, DateTime> last = new KeyValuePair<Size, DateTime>(new Size(), DateTime.Now);
                foreach (KeyValuePair<Size, DateTime> kvp in _cachedImageAge)
                    if (kvp.Value < last.Value)
                        last = kvp;
                _cachedImage.Remove(last.Key);
                _cachedImageAge.Remove(last.Key);
            }

            // Create scaled versions
            _cachedImage[sz] = new Dictionary<SokobanImage, Bitmap>();
            RectangleF dest = new RectangleF(0, 0, CellWidth * 1.5f + 1f, CellHeight * 1.5f + 1f);
            Graphics g;
            foreach (SokobanImage si in Enum.GetValues(typeof(SokobanImage)))
            {
                _cachedImage[sz][si] = new Bitmap((int) dest.Width + 1, (int) dest.Height + 1);
                g = Graphics.FromImage(_cachedImage[sz][si]);
                if (si == SokobanImage.Wall)
                    g.InterpolationMode = InterpolationMode.Bicubic;
                else
                    g.InterpolationMode = InterpolationMode.High;
                g.DrawImage(getImage(si), dest);
            }

            // Return the requested one
            _cachedImageAge[sz] = DateTime.Now;
            return _cachedImage[sz][imageType];
        }

        /// <summary>Given a <see cref="MoveFinder"/> or <see cref="PushFinder"/>, generates the "outline" of the reachable area.
        /// If there are several disjoint regions, several separate outlines are generated.</summary>
        /// <param name="input">The input <see cref="MoveFinder"/> or <see cref="PushFinder"/> to generate the outline from.</param>
        /// <returns>An array of paths, where each path is an array of points. The co-ordinates of the points are the indexes in the input array.</returns>
        /// <example>
        /// <para>An input array full of booleans set to false generates an empty output array.</para>
        /// <para>An input array full of booleans set to true generates a single output path which describes the complete rectangle.</para>
        /// </example>
        private static Point[][] boolsToPaths(Virtual2DArray<bool> input)
        {
            List<List<Point>> activeSegments = new List<List<Point>>();
            List<Point[]> completedPaths = new List<Point[]>();
            for (int y = 0; y <= input.Height; y++)
            {
                List<pathEvent> events = findEvents(activeSegments, input, y);
                for (int i = 0; i < events.Count; i += 2)
                {
                    if (events[i] is pathEventSegment && events[i + 1] is pathEventSegment)
                    {
                        int index1 = ((pathEventSegment) events[i]).SegmentIndex;
                        int index2 = ((pathEventSegment) events[i + 1]).SegmentIndex;
                        bool start = ((pathEventSegment) events[i]).StartOfSegment;
                        if (index1 == index2 && start)
                        {
                            // A segment becomes a closed path
                            activeSegments[index2].Add(new Point(events[i + 1].X, y));
                            activeSegments[index2].Add(new Point(events[i].X, y));
                            completedPaths.Add(activeSegments[index2].ToArray());
                        }
                        else if (index1 == index2)
                        {
                            // A segment becomes a closed path
                            activeSegments[index2].Add(new Point(events[i].X, y));
                            activeSegments[index2].Add(new Point(events[i + 1].X, y));
                            completedPaths.Add(activeSegments[index2].ToArray());
                        }
                        else if (start)
                        {
                            // Two segments join up
                            activeSegments[index2].Add(new Point(events[i + 1].X, y));
                            activeSegments[index2].Add(new Point(events[i].X, y));
                            activeSegments[index1].InsertRange(0, activeSegments[index2]);
                        }
                        else
                        {
                            // Two segments join up
                            activeSegments[index1].Add(new Point(events[i].X, y));
                            activeSegments[index1].Add(new Point(events[i + 1].X, y));
                            activeSegments[index1].AddRange(activeSegments[index2]);
                        }
                        activeSegments.RemoveAt(index2);
                        for (int correction = i + 2; correction < events.Count; correction++)
                        {
                            if (events[correction] is pathEventSegment &&
                                (events[correction] as pathEventSegment).SegmentIndex == index2)
                                (events[correction] as pathEventSegment).SegmentIndex = index1;
                            if (events[correction] is pathEventSegment &&
                                (events[correction] as pathEventSegment).SegmentIndex > index2)
                                (events[correction] as pathEventSegment).SegmentIndex--;
                        }
                    }
                    else if (events[i] is pathEventChange && events[i + 1] is pathEventChange)
                    {
                        // Both events are changes - create a new segment
                        activeSegments.Add(new List<Point>(new Point[] { 
                            new Point (events[input.Get(events[i].X, y) ? i : i+1].X,y),
                            new Point (events[input.Get(events[i].X, y) ? i+1 : i].X,y)
                        }));
                    }
                    else if (events[i] is pathEventSegment) // ... && Events[i+1] is RTUtilPathEventChange
                    {
                        pathEventSegment ev = events[i] as pathEventSegment;
                        if (ev.StartOfSegment)
                        {
                            activeSegments[ev.SegmentIndex].Insert(0, new Point(ev.X, y));
                            if (ev.X != events[i + 1].X)
                                activeSegments[ev.SegmentIndex].Insert(0, new Point(events[i + 1].X, y));
                        }
                        else
                        {
                            activeSegments[ev.SegmentIndex].Add(new Point(ev.X, y));
                            if (ev.X != events[i + 1].X)
                                activeSegments[ev.SegmentIndex].Add(new Point(events[i + 1].X, y));
                        }
                    }
                    else  // ... Events[i] is pathEventChange && Events[i+1] is pathEventSegment
                    {
                        pathEventSegment ev = events[i + 1] as pathEventSegment;
                        if (ev.StartOfSegment)
                        {
                            activeSegments[ev.SegmentIndex].Insert(0, new Point(ev.X, y));
                            if (ev.X != events[i].X)
                                activeSegments[ev.SegmentIndex].Insert(0, new Point(events[i].X, y));
                        }
                        else
                        {
                            activeSegments[ev.SegmentIndex].Add(new Point(ev.X, y));
                            if (ev.X != events[i].X)
                                activeSegments[ev.SegmentIndex].Add(new Point(events[i].X, y));
                        }
                    }
                }
            }
            return completedPaths.ToArray();
        }

        private static List<pathEvent> findEvents(List<List<Point>> activeSegments, Virtual2DArray<bool> input, int y)
        {
            List<pathEvent> results = new List<pathEvent>();

            // First add all the validity change events in the correct order
            if (y < input.Height)
            {
                for (int x = 0; x <= input.Width; x++)  // "<=" is intentional
                    if (input.Get(x, y) != input.Get(x - 1, y))
                        results.Add(new pathEventChange(x));
            }

            // Now insert the segment events in the right places
            for (int i = 0; i < activeSegments.Count; i++)
            {
                int index = 0;
                while (index < results.Count && results[index].X <= activeSegments[i][0].X)
                    index++;
                results.Insert(index, new pathEventSegment(i, true, activeSegments[i][0].X));
                index = 0;
                while (index < results.Count && results[index].X < activeSegments[i][activeSegments[i].Count - 1].X)
                    index++;
                results.Insert(index, new pathEventSegment(i, false, activeSegments[i][activeSegments[i].Count - 1].X));
            }
            return results;
        }

        private abstract class pathEvent
        {
            public int X;
        }

        private class pathEventSegment : pathEvent
        {
            public int SegmentIndex;
            public bool StartOfSegment;
            public pathEventSegment(int index, bool start, int newX)
            {
                SegmentIndex = index;
                StartOfSegment = start;
                X = newX;
            }
        }

        private class pathEventChange : pathEvent
        {
            public pathEventChange(int newX)
            {
                X = newX;
            }
        }

        /// <summary>
        /// Given a <see cref="MoveFinder"/> or <see cref="PushFinder"/>, returns a <see cref="GraphicsPath"/> that can be
        /// used to visualise the area deemed "valid" by the relevant finder.
        /// </summary>
        /// <param name="finder">A <see cref="MoveFinder"/> or <see cref="PushFinder"/> object.</param>
        public GraphicsPath ValidPath(Virtual2DArray<bool> finder)
        {
            Point[][] outlines = boolsToPaths(finder);
            SizeF margin = new SizeF(_cellWidth / 5, _cellHeight / 5);
            SizeF diameter = new SizeF(_cellWidth / 2, _cellHeight / 2);
            GraphicsPath result = new GraphicsPath();
            if (diameter.Width == 0 || diameter.Height == 0)
                return result;
            for (int i = 0; i < outlines.Length; i++)
            {
                result.StartFigure();
                for (int j = 0; j < outlines[i].Length; j++)
                {
                    Point point1 = outlines[i][j];
                    Point point2 = outlines[i][(j + 1) % outlines[i].Length];
                    Point point3 = outlines[i][(j + 2) % outlines[i].Length];
                    Rectangle rect = CellRect(point2);

                    int dir1 = getDir(point1, point2);
                    int dir2 = getDir(point2, point3);

                    // Rounded corners ("outer" corners)
                    if (dir1 == 0 && dir2 == 2) // top left corner
                        result.AddArc(rect.X + margin.Width, rect.Y + margin.Height, diameter.Width, diameter.Height, 180, 90);
                    else if (dir1 == 2 && dir2 == 3)  // top right corner
                        result.AddArc(rect.X - margin.Width - diameter.Width, rect.Y + margin.Height, diameter.Width, diameter.Height, 270, 90);
                    else if (dir1 == 3 && dir2 == 1) // bottom right corner
                        result.AddArc(rect.X - margin.Width - diameter.Width, rect.Y - margin.Height - diameter.Height, diameter.Width, diameter.Height, 0, 90);
                    else if (dir1 == 1 && dir2 == 0) // bottom left corner
                        result.AddArc(rect.X + margin.Width, rect.Y - margin.Height - diameter.Height, diameter.Width, diameter.Height, 90, 90);

                    // Unrounded corners ("inner" corners)
                    else if (dir1 == 1 && dir2 == 3) // top left corner
                        result.AddLine(rect.X - margin.Width, rect.Y - margin.Height, rect.X - margin.Width, rect.Y - margin.Height);
                    else if (dir1 == 0 && dir2 == 1) // top right corner
                        result.AddLine(rect.X + margin.Width, rect.Y - margin.Height, rect.X + margin.Width, rect.Y - margin.Height);
                    else if (dir1 == 2 && dir2 == 0) // bottom right corner
                        result.AddLine(rect.X + margin.Width, rect.Y + margin.Height, rect.X + margin.Width, rect.Y + margin.Height);
                    else if (dir1 == 3 && dir2 == 2) // bottom left corner
                        result.AddLine(rect.X - margin.Width, rect.Y + margin.Height, rect.X - margin.Width, rect.Y + margin.Height);
                }
                result.CloseFigure();
            }
            return result;
        }

        /// <summary>
        /// Returns the value 0, 1, 2 or 3 if the cell specified by the second parameter
        /// is above, left of, right of and below the cell specified by the first
        /// parameter, respectively. If neither of the four is the case, the result is
        /// undefined.
        /// </summary>
        private int getDir(Point from, Point to)
        {
            return from.X == to.X
                ? (from.Y > to.Y ? 0 : 3)
                : (from.X > to.X ? 1 : 2);
        }

        /// <summary>
        /// Returns a GraphicsPath object that can be used to visualise a path through
        /// a sequence of cells. This can be a move path or a push path.
        /// </summary>
        /// <param name="startPos">Starting position for the path.</param>
        /// <param name="cellSequence">Sequence of cells for the path, not including the
        /// starting position. Each cell must be directly adjacent to the previous cell,
        /// and the first cell must be directly adjacent to the cell specified by
        /// StartPos. Otherwise, the behaviour is undefined.</param>
        /// <param name="diameterX">Proportion of the cell width that is used to round
        /// the corners.</param>
        /// <param name="diameterY">Proportion of the cell height that is used to round
        /// the corners.</param>
        public GraphicsPath LinePath(Point startPos, Point[] cellSequence, float diameterX, float diameterY)
        {
            if (cellSequence.Length < 1)
                return null;

            GraphicsPath result = new GraphicsPath();
            Rectangle startRect = CellRect(startPos);
            result.AddLine(startRect.X + _cellWidth / 2, startRect.Y + _cellHeight / 2,
                startRect.X + _cellWidth / 2, startRect.Y + _cellHeight / 2);
            diameterX *= _cellWidth;
            diameterY *= _cellHeight;
            for (int i = -1; i < cellSequence.Length - 2; i++)
            {
                Point pos1 = i == -1 ? startPos : cellSequence[i];
                Point pos2 = cellSequence[i + 1];
                Point pos3 = cellSequence[i + 2];
                RectangleF pos2Rect = CellRect(pos2);
                PointF center = new PointF(pos2Rect.X + _cellWidth / 2, pos2Rect.Y + _cellHeight / 2);

                int dir1 = getDir(pos1, pos2);
                int dir2 = getDir(pos2, pos3);

                // Clockwise turns
                if (dir1 == 0 && dir2 == 2) // top left corner
                    result.AddArc(center.X, center.Y, diameterX, diameterY, 180, 90);
                else if (dir1 == 2 && dir2 == 3) // top right corner
                    result.AddArc(center.X - diameterX, center.Y, diameterX, diameterY, 270, 90);
                else if (dir1 == 3 && dir2 == 1) // bottom right corner
                    result.AddArc(center.X - diameterX, center.Y - diameterY, diameterX, diameterY, 0, 90);
                else if (dir1 == 1 && dir2 == 0) // bottom left corner
                    result.AddArc(center.X, center.Y - diameterY, diameterX, diameterY, 90, 90);

                // Anti-clockwise turns
                else if (dir1 == 1 && dir2 == 3) // top left corner
                    result.AddArc(center.X, center.Y, diameterX, diameterY, 270, -90);
                else if (dir1 == 0 && dir2 == 1) // top right corner
                    result.AddArc(center.X - diameterX, center.Y, diameterX, diameterY, 360, -90);
                else if (dir1 == 2 && dir2 == 0) // bottom right corner
                    result.AddArc(center.X - diameterX, center.Y - diameterY, diameterX, diameterY, 90, -90);
                else if (dir1 == 3 && dir2 == 2) // bottom left corner
                    result.AddArc(center.X, center.Y - diameterY, diameterX, diameterY, 180, -90);
                else
                    result.AddLine(center, center);
            }
            Rectangle endRect = CellRect(cellSequence[cellSequence.Length - 1]);
            result.AddLine(endRect.X + _cellWidth / 2, endRect.Y + _cellHeight / 2,
                endRect.X + _cellWidth / 2, endRect.Y + _cellHeight / 2);
            return result;
        }

        /// <summary>
        /// Returns a <see cref="GraphicsPath"/> object that can be used to visualise a selection
        /// cursor that encompasses one cell.
        /// </summary>
        /// <param name="cell">Cell to generate a cursor for.</param>
        public GraphicsPath SelectorPath(Point cell)
        {
            GraphicsPath result = new GraphicsPath();
            Rectangle rect = CellRect(cell);
            result.AddArc(rect.Left - CellWidth / 10, rect.Top - CellWidth / 10, CellWidth / 5, CellHeight / 5, 180, 90);
            result.AddArc(rect.Right - CellWidth / 10, rect.Top - CellWidth / 10, CellWidth / 5, CellHeight / 5, 270, 90);
            result.AddArc(rect.Right - CellWidth / 10, rect.Bottom - CellWidth / 10, CellWidth / 5, CellHeight / 5, 0, 90);
            result.AddArc(rect.Left - CellWidth / 10, rect.Bottom - CellWidth / 10, CellWidth / 5, CellHeight / 5, 90, 90);
            result.CloseAllFigures();
            return result;
        }
    }
}
