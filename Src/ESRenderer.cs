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

        public int CellFromPixel(Point Pixel)
        {
            return (int) ((Pixel.X - FOriginX) / FCellWidth) +
                FLevel.Width * (int) ((Pixel.Y - FOriginY) / FCellHeight);
        }

        public void RenderCell(Graphics g, int pos)
        {
            RenderCell(g, FLevel.PosToX(pos), FLevel.PosToY(pos));
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

        public void RenderCells(Graphics g, int RenderFrom, int RenderTo)
        {
            int FromX = Math.Min(RenderFrom % FLevel.Width, RenderTo % FLevel.Width),
                FromY = Math.Min(RenderFrom / FLevel.Width, RenderTo / FLevel.Width),
                ToX = Math.Max(RenderFrom % FLevel.Width, RenderTo % FLevel.Width),
                ToY = Math.Max(RenderFrom / FLevel.Width, RenderTo / FLevel.Width);
            g.SetClip(ClippingRectForCells(RenderFrom, RenderTo));
            for (int i = FromX; i <= ToX; i++)
                for (int j = FromY; j <= ToY; j++)
                    RenderCellAsPartOfCompleteRender(g, i, j);
        }

        public RectangleF ClippingRectForCells(int CellFrom, int CellTo)
        {
            int FromX = Math.Min(CellFrom % FLevel.Width, CellTo % FLevel.Width),
                FromY = Math.Min(CellFrom / FLevel.Width, CellTo / FLevel.Width),
                ToX = Math.Max(CellFrom % FLevel.Width, CellTo % FLevel.Width),
                ToY = Math.Max(CellFrom / FLevel.Width, CellTo / FLevel.Width);
            RectangleF RectFrom = CellRectForImage(FromX, FromY);
            RectangleF RectTo = CellRectForImage(ToX, ToY);
            return new RectangleF(RectFrom.Left, RectTo.Top, RectTo.Right-RectFrom.Left, RectTo.Bottom-RectFrom.Top);
        }

        private void RenderCellAsPartOfCompleteRender(Graphics g, int pos)
        {
            RenderCellAsPartOfCompleteRender(g, FLevel.PosToX(pos), FLevel.PosToY(pos));
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
            if (x == FLevel.SokobanX && y == FLevel.SokobanY)
                DrawCell(g, x, y, SokobanImage.Sokoban);
        }

        public void DrawCell(Graphics g, int Pos, SokobanImage ImageType)
        {
            DrawCell(g, FLevel.PosToX(Pos), FLevel.PosToY(Pos), ImageType);
        }

        public void DrawCell(Graphics g, int x, int y, SokobanImage ImageType)
        {
            g.DrawImage(GetImage(ImageType), CellRectForImage(x, y));
        }

        public RectangleF CellRectForImage(int Pos)
        {
            return CellRectForImage(FLevel.PosToX(Pos), FLevel.PosToY(Pos));
        }

        public RectangleF CellRectForImage(int x, int y)
        {
            RectangleF Src = CellRect(x, y);
            return new RectangleF(Src.X, Src.Y, Src.Width*1.5f+1, Src.Height*1.5f+1);
        }

        public RectangleF CellRect(int Pos)
        {
            return CellRect(FLevel.PosToX(Pos), FLevel.PosToY(Pos));
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

        public GraphicsPath ValidPath(ESFinder Finder)
        {
            List<List<int>> ActiveSegments = new List<List<int>>();
            List<List<int>> CompletedPaths = new List<List<int>>();
            for (int y = 0; y < FLevel.Height-1; y++)
            {
                List<ESRendererPathEvent> Events = FindEvents(ActiveSegments, Finder, y);
                for (int i = 0; i < Events.Count; i += 2)
                {
                    if (Events[i] is ESRendererPathEventSegment && Events[i+1] is ESRendererPathEventSegment)
                    {
                        int Index1 = ((ESRendererPathEventSegment) Events[i]).SegmentIndex;
                        int Index2 = ((ESRendererPathEventSegment) Events[i+1]).SegmentIndex;
                        bool Start = ((ESRendererPathEventSegment) Events[i]).StartOfSegment;
                        if (Index1 == Index2 && Start)
                        {
                            // A segment becomes a closed path
                            ActiveSegments[Index2].Add(Events[i+1].Pos);
                            ActiveSegments[Index2].Add(Events[i].Pos);
                            CompletedPaths.Add(ActiveSegments[Index2]);
                        }
                        else if (Index1 == Index2)
                        {
                            // A segment becomes a closed path
                            ActiveSegments[Index2].Add(Events[i].Pos);
                            ActiveSegments[Index2].Add(Events[i+1].Pos);
                            CompletedPaths.Add(ActiveSegments[Index2]);
                        }
                        else if (Start)
                        {
                            // Two segments join up
                            ActiveSegments[Index2].Add(Events[i+1].Pos);
                            ActiveSegments[Index2].Add(Events[i].Pos);
                            ActiveSegments[Index1].InsertRange(0, ActiveSegments[Index2]);
                        }
                        else
                        {
                            // Two segments join up
                            ActiveSegments[Index1].Add(Events[i].Pos);
                            ActiveSegments[Index1].Add(Events[i+1].Pos);
                            ActiveSegments[Index1].AddRange(ActiveSegments[Index2]);
                        }
                        ActiveSegments.RemoveAt(Index2);
                        for (int Correction = i+2; Correction < Events.Count; Correction++)
                        {
                            if (Events[Correction] is ESRendererPathEventSegment &&
                                (Events[Correction] as ESRendererPathEventSegment).SegmentIndex == Index2)
                                (Events[Correction] as ESRendererPathEventSegment).SegmentIndex = Index1;
                            if (Events[Correction] is ESRendererPathEventSegment &&
                                (Events[Correction] as ESRendererPathEventSegment).SegmentIndex > Index2)
                                (Events[Correction] as ESRendererPathEventSegment).SegmentIndex--;
                        }
                    }
                    else if (Events[i] is ESRendererPathEventValidityChange && Events[i+1] is ESRendererPathEventValidityChange)
                    {
                        // Both events are validity changes - create a new segment
                        if (Finder.Valid(Events[i].Pos))
                            ActiveSegments.Add(new List<int>(new int[] { Events[i].Pos, Events[i+1].Pos }));
                        else
                            ActiveSegments.Add(new List<int>(new int[] { Events[i+1].Pos, Events[i].Pos }));
                    }
                    else if (Events[i] is ESRendererPathEventSegment) // ... && Events[i+1] is ESRendererPathEventValidityChange
                    {
                        ESRendererPathEventSegment Ev = Events[i] as ESRendererPathEventSegment;
                        if (Ev.StartOfSegment)
                        {
                            ActiveSegments[Ev.SegmentIndex].Insert(0, Ev.Pos);
                            if (Ev.Pos != Events[i+1].Pos)
                                ActiveSegments[Ev.SegmentIndex].Insert(0, Events[i+1].Pos);
                        }
                        else
                        {
                            ActiveSegments[Ev.SegmentIndex].Add(Ev.Pos);
                            if (Ev.Pos != Events[i+1].Pos)
                                ActiveSegments[Ev.SegmentIndex].Add(Events[i+1].Pos);
                        }
                    }
                    else  // ... Events[i] is ESRendererPathEventValidityChange && Events[i+1] is ESRendererPathEventSegment
                    {
                        ESRendererPathEventSegment Ev = Events[i+1] as ESRendererPathEventSegment;
                        if (Ev.StartOfSegment)
                        {
                            ActiveSegments[Ev.SegmentIndex].Insert(0, Ev.Pos);
                            if (Ev.Pos != Events[i].Pos)
                                ActiveSegments[Ev.SegmentIndex].Insert(0, Events[i].Pos);
                        }
                        else
                        {
                            ActiveSegments[Ev.SegmentIndex].Add(Ev.Pos);
                            if (Ev.Pos != Events[i].Pos)
                                ActiveSegments[Ev.SegmentIndex].Add(Events[i].Pos);
                        }
                    }
                }
            }

            SizeF Margin = new SizeF (FCellWidth/5, FCellHeight/5);
            SizeF ESize = new SizeF(FCellWidth/2, FCellHeight/2);
            GraphicsPath Result = new GraphicsPath();
            for (int i = 0; i < CompletedPaths.Count; i++)
            {
                Result.StartFigure();
                for (int j = 0; j < CompletedPaths[i].Count; j++)
                {
                    int Pos1 = CompletedPaths[i][j];
                    int Pos2 = CompletedPaths[i][(j+1) % CompletedPaths[i].Count];
                    int Pos3 = CompletedPaths[i][(j+2) % CompletedPaths[i].Count];
                    RectangleF Pos2Rect = CellRect(Pos2);

                    int Dir1 = GetDir(Pos1, Pos2);
                    int Dir2 = GetDir(Pos2, Pos3);

                    // Rounded corners ("outer" corners)
                    if (Dir1 == 0 && Dir2 == 2 && Finder.Valid(Pos2)) // top left corner
                        Result.AddArc(Pos2Rect.X+Margin.Width, Pos2Rect.Y+Margin.Height, ESize.Width, ESize.Height, 180, 90);
                    else if (Dir1 == 2 && Dir2 == 3 && Finder.Valid(Pos2-1))  // top right corner
                        Result.AddArc(Pos2Rect.X-Margin.Width-ESize.Width, Pos2Rect.Y+Margin.Height, ESize.Width, ESize.Height, 270, 90);
                    else if (Dir1 == 3 && Dir2 == 1 && Finder.Valid (Pos2-1-FLevel.Width)) // bottom right corner
                        Result.AddArc(Pos2Rect.X-Margin.Width-ESize.Width, Pos2Rect.Y-Margin.Height-ESize.Height, ESize.Width, ESize.Height, 0, 90);
                    else if (Dir1 == 1 && Dir2 == 0 && Finder.Valid (Pos2-FLevel.Width)) // bottom left corner
                        Result.AddArc(Pos2Rect.X+Margin.Width, Pos2Rect.Y-Margin.Height-ESize.Height, ESize.Width, ESize.Height, 90, 90);

                    // Unrounded corners ("inner" corners)
                    else if (Dir1 == 1 && Dir2 == 3) // top left corner
                        Result.AddLine(Pos2Rect.X-Margin.Width, Pos2Rect.Y-Margin.Height, Pos2Rect.X-Margin.Width, Pos2Rect.Y-Margin.Height);
                    else if (Dir1 == 0 && Dir2 == 1) // top right corner
                        Result.AddLine(Pos2Rect.X+Margin.Width, Pos2Rect.Y-Margin.Height, Pos2Rect.X+Margin.Width, Pos2Rect.Y-Margin.Height);
                    else if (Dir1 == 2 && Dir2 == 0) // bottom right corner
                        Result.AddLine(Pos2Rect.X+Margin.Width, Pos2Rect.Y+Margin.Height, Pos2Rect.X+Margin.Width, Pos2Rect.Y+Margin.Height);
                    else if (Dir1 == 3 && Dir2 == 2) // bottom left corner
                        Result.AddLine(Pos2Rect.X-Margin.Width, Pos2Rect.Y+Margin.Height, Pos2Rect.X-Margin.Width, Pos2Rect.Y+Margin.Height);
                }
                Result.CloseFigure();
            }
            return Result;
        }

        private int GetDir(int FromPos, int ToPos)
        {
            if ((FromPos-ToPos) % FLevel.Width == 0)
                return FromPos > ToPos ? 0 : 3;
            return FromPos > ToPos ? 1 : 2;
        }

        private List<ESRendererPathEvent> FindEvents(List<List<int>> ActiveSegments, ESFinder Finder, int y)
        {
            List<ESRendererPathEvent> Results = new List<ESRendererPathEvent>();

            // First add all the validity change events in the correct order
            for (int i = 1; i < FLevel.Width; i++)
                if (Finder.Valid(y*FLevel.Width + i) != Finder.Valid(y*FLevel.Width + i - 1))
                    Results.Add(new ESRendererPathEventValidityChange(y*FLevel.Width + i));

            // Now insert the segment events in the right places
            for (int i = 0; i < ActiveSegments.Count; i++)
            {
                int Index = 0;
                while (Index < Results.Count && Results[Index].Pos <= ActiveSegments[i][0] + FLevel.Width)
                    Index++;
                Results.Insert(Index, new ESRendererPathEventSegment(i, true, ActiveSegments[i][0] + FLevel.Width));
                Index = 0;
                while (Index < Results.Count && Results[Index].Pos < ActiveSegments[i][ActiveSegments[i].Count-1] + FLevel.Width)
                    Index++;
                Results.Insert(Index, new ESRendererPathEventSegment(i, false, ActiveSegments[i][ActiveSegments[i].Count-1] + FLevel.Width));
            }
            return Results;
        }

        private abstract class ESRendererPathEvent
        {
            public int Pos;
        }
        private class ESRendererPathEventSegment : ESRendererPathEvent
        {
            public int SegmentIndex;
            public bool StartOfSegment;
            public ESRendererPathEventSegment(int Index, bool Start, int p)
            {
                SegmentIndex = Index;
                StartOfSegment = Start;
                Pos = p;
            }
        }
        private class ESRendererPathEventValidityChange : ESRendererPathEvent
        {
            public ESRendererPathEventValidityChange(int p)
            {
                Pos = p;
            }
        }

        public GraphicsPath LinePath(int StartPos, int[] CellSequence, float DiameterX, float DiameterY)
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
                int Pos1 = i == -1 ? StartPos : CellSequence[i];
                int Pos2 = CellSequence[i+1];
                int Pos3 = CellSequence[i+2];
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
