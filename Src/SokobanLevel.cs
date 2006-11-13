using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ExpertSokoban
{
    public enum SokobanCell
    {
        //Invalid,
        Blank,
        Wall,
        Piece,
        Target,
        PieceOnTarget
    }
    public enum SokobanLevelStatus
    {
        Valid,
        NotEnclosed,
        TargetsPiecesMismatch
    }

    [Serializable]
    public class SokobanLevel
    {
        private SokobanCell[] FLevel;
        private int FWidth, FHeight;
        private Point FSokobanPos;

        public int Width { get { return FWidth; } }
        public int Height { get { return FHeight; } }
        public Point SokobanPos { get { return FSokobanPos; } }

        public bool Solved
        {
            get
            {
                for (int i = 0; i < FWidth*FHeight; i++)
                    if (FLevel[i] == SokobanCell.Target || FLevel[i] == SokobanCell.Piece)
                        return false;
                return true;
            }
        }

        public SokobanLevel(string EncodedForm)
        {
            FWidth = 0;
            FHeight = 0;
            bool HaveSokobanPos = false;
            int CurX = 0;
            for (int i = 0; i < EncodedForm.Length; i++)
            {
                if (EncodedForm[i] == '\n')
                {
                    FHeight++;
                    if (FWidth < CurX)
                        FWidth = CurX;
                    CurX = 0;
                }
                else
                    CurX++;
            }
            FLevel = new SokobanCell[FWidth*FHeight];
            CurX = 0;
            int CurY = 0;
            for (int i = 0; i < EncodedForm.Length; i++)
            {
                if (EncodedForm[i] == '\n')
                {
                    CurX = 0;
                    CurY++;
                }
                else
                {
                    FLevel[CurX + FWidth*CurY] = 
                        EncodedForm[i] == '#' ? SokobanCell.Wall :
                        EncodedForm[i] == '$' ? SokobanCell.Piece :
                        EncodedForm[i] == '.' ? SokobanCell.Target :
                        EncodedForm[i] == '+' ? SokobanCell.Target :
                        EncodedForm[i] == '*' ? SokobanCell.PieceOnTarget :
                                                SokobanCell.Blank;
                    if (EncodedForm[i] == '@' || EncodedForm[i] == '+')
                    {
                        FSokobanPos = new Point(CurX, CurY);
                        HaveSokobanPos = true;
                    }

                    CurX++;
                }
            }

            // If the level didn't specify a starting position, try to find one.
            if (!HaveSokobanPos)
            {
                bool Done = false;
                for (int i = 0; i < FWidth && !Done; i++)
                    for (int j = 0; j < FHeight && !Done; j++)
                    {
                        bool xIn = false, yIn = false;
                        for (int x = 0; x <= i; x++)
                            if (FLevel[j*FWidth + x] == SokobanCell.Wall &&
                            (x == 0 || FLevel[j*FWidth + x-1] != SokobanCell.Wall))
                                xIn = !xIn;
                        for (int y = 0; y <= j; y++)
                            if (FLevel[y*FWidth + i] == SokobanCell.Wall &&
                            (y == 0 || FLevel[(y-1)*FWidth + i] != SokobanCell.Wall))
                                yIn = !yIn;
                        if (xIn && yIn)
                        {
                            FSokobanPos = new Point(i, j);
                            Done = true;
                        }
                    }
            }
        }
        public SokobanLevel(int Width, int Height, SokobanCell[] LevelData, Point SokobanPos)
        {
            FWidth = Width;
            FHeight = Height;
            FLevel = LevelData;
            FSokobanPos = SokobanPos;
        }
        public SokobanCell Cell(Point Pos)
        {
            return Cell(Pos.X, Pos.Y);
        }
        public SokobanCell Cell(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                return SokobanCell.Blank;
            return FLevel[y*FWidth + x];
        }
        public void SetSokobanPos(Point Pos) { if (Pos.X >= 0 && Pos.Y >= 0 && Pos.X < FWidth && Pos.Y < FHeight) FSokobanPos = Pos; }
        private void SetCell(int Index, SokobanCell c) { if (Index >= 0 && Index < FWidth*FHeight) FLevel[Index] = c; }
        public void SetCell(Point Pos, SokobanCell c) { SetCell(Pos.Y*FWidth + Pos.X, c); }
        public void SetCell(int x, int y, SokobanCell c) { SetCell(y*FWidth + x, c); }
        private bool IsPiece(int Index)
        {
            if (Index < 0 || Index > FLevel.Length) return false;
            return FLevel[Index] == SokobanCell.Piece || FLevel[Index] == SokobanCell.PieceOnTarget;
        }
        public bool IsPiece(Point Pos) { return IsPiece(Pos.Y*FWidth + Pos.X); }
        public bool IsPiece(int x, int y) { return IsPiece(y*FWidth + x); }
        private bool IsFree(int Index)
        {
            return (Index >= 0 && Index < FWidth*FHeight)
                ? (FLevel[Index] == SokobanCell.Blank || FLevel[Index] == SokobanCell.Target)
                : false;
        }
        public bool IsFree(Point Pos) { return IsFree(Pos.Y*FWidth + Pos.X); }
        public bool IsFree(int x, int y) { return IsFree(y*FWidth + x); }
        private void SetPiece(int Index)
        {
            if (Index >= 0 && Index < FWidth*FHeight)
            {
                if (FLevel[Index] == SokobanCell.Blank)
                    FLevel[Index] = SokobanCell.Piece;
                else if (FLevel[Index] == SokobanCell.Target)
                    FLevel[Index] = SokobanCell.PieceOnTarget;
            }
        }
        public void SetPiece(Point Pos) { SetPiece(Pos.Y*FWidth + Pos.X); }
        public void SetPiece(int x, int y) { SetPiece(y*FWidth + x); }
        private void RemovePiece(int Index)
        {
            if (Index >= 0 && Index < FWidth*FHeight)
            {
                if (FLevel[Index] == SokobanCell.Piece)
                    FLevel[Index] = SokobanCell.Blank;
                else if (FLevel[Index] == SokobanCell.PieceOnTarget)
                    FLevel[Index] = SokobanCell.Target;
            }
        }
        public void RemovePiece(Point Pos) { RemovePiece(Pos.Y*FWidth + Pos.X); }
        public void RemovePiece(int x, int y) { RemovePiece(y*FWidth + x); }
        public void MovePiece(int FromX, int FromY, int ToX, int ToY)
        {
            RemovePiece(FromX, FromY);
            SetPiece(ToX, ToY);
        }
        public void MovePiece(Point FromPos, Point ToPos)
        {
            RemovePiece(FromPos);
            SetPiece(ToPos);
        }

        // This will reduce the size of the level if there are rows or columns
        // around the edge that are entirely blank, but will leave (or create)
        // a one-square margin around the edge.
        public void EnsureSpace(int Margin)
        {
            int Left = 0, Right = 0, Top = 0, Bottom = 0;
            while (Left < FWidth && ColumnBlank(Left)) Left++;
            while (Right < FWidth && ColumnBlank(FWidth-Right-1)) Right++;
            while (Top < FHeight && RowBlank(Top)) Top++;
            while (Bottom < FHeight && RowBlank(FHeight-Bottom-1)) Bottom++;
            Resize(2*Margin-Left-Right, 2*Margin-Top-Bottom, Margin-Left, Margin-Top);
        }
        private bool ColumnBlank(int x)
        {
            if (FSokobanPos.X == x)
                return false;
            for (int y = 0; y < FHeight; y++)
                if (FLevel[y*FWidth + x] != SokobanCell.Blank)
                    return false;
            return true;
        }
        private bool RowBlank(int y)
        {
            if (FSokobanPos.Y == y)
                return false;
            for (int x = 0; x < FWidth; x++)
                if (FLevel[y*FWidth + x] != SokobanCell.Blank)
                    return false;
            return true;
        }

        // any parameter may be negative
        private void Resize(int AmountX, int AmountY, int ShiftX, int ShiftY)
        {
            if (AmountX == 0 && AmountY == 0) return;

            int NewWidth = FWidth + AmountX;
            int NewHeight = FHeight + AmountY;
            SokobanCell[] NewLevel = new SokobanCell[NewWidth * NewHeight];

            for (int x = 0; x < (AmountX < 0 ? NewWidth : FWidth); x++)
                for (int y = 0; y < (AmountY < 0 ? NewHeight : FHeight); y++)
                    NewLevel[(AmountY > 0 ? y+ShiftY : y)*NewWidth +
                             (AmountX > 0 ? x+ShiftX : x)] =
                    FLevel   [(AmountY < 0 ? y-ShiftY : y)*FWidth +
                             (AmountX < 0 ? x-ShiftX : x)];

            FSokobanPos.Offset(ShiftX, ShiftY);

            FLevel = NewLevel;
            FWidth = NewWidth;
            FHeight = NewHeight;
        }

        public SokobanLevel Clone()
        {
            SokobanCell[] NewLevel = (SokobanCell[]) FLevel.Clone();
            return new SokobanLevel(FWidth, FHeight, NewLevel, FSokobanPos);
        }

        /// <summary>
        /// Returns a value indicating whether the level is valid, and if not - why.
        /// </summary>
        public SokobanLevelStatus Validity
        {
            get
            {
                // Check if the number of pieces equals the number of targets
                int Pieces = 0, Targets = 0;
                for (int i = 0; i < FLevel.Length; i++)
                    if (FLevel[i] == SokobanCell.Piece)
                        Pieces++;
                    else if (FLevel[i] == SokobanCell.Target)
                        Targets++;
                if (Pieces != Targets)
                    return SokobanLevelStatus.TargetsPiecesMismatch;

                // Check if the level is properly enclosed
                MoveFinder Finder = new MoveFinder(this, MoveFinderOption.IgnorePieces);
                for (int i = 0; i < FWidth; i++)
                    if (Finder.Get(i, 0) || Finder.Get(i, FHeight-1))
                        return SokobanLevelStatus.NotEnclosed;
                for (int i = 0; i < FHeight; i++)
                    if (Finder.Get(0, i) || Finder.Get(FWidth-1, i))
                        return SokobanLevelStatus.NotEnclosed;

                return SokobanLevelStatus.Valid;
            }
        }

        public override string ToString()
        {
            string Output = "";
            for (int y = 0; y < FHeight; y++)
            {
                for (int x = 0; x < FWidth; x++)
                    Output +=
                        FLevel[y*FWidth + x] == SokobanCell.Blank && x == FSokobanPos.X && y == FSokobanPos.Y ? '@' :
                        FLevel[y*FWidth + x] == SokobanCell.Target && x == FSokobanPos.X && y == FSokobanPos.Y ? '+' :
                        FLevel[y*FWidth + x] == SokobanCell.Piece ? '$' :
                        FLevel[y*FWidth + x] == SokobanCell.PieceOnTarget ? '*' :
                        FLevel[y*FWidth + x] == SokobanCell.Target ? '.' :
                        FLevel[y*FWidth + x] == SokobanCell.Wall ? '#' : ' ';
                Output += "\n";
            }
            return Output;
        }

        public static SokobanLevel TestLevel()
        {
            return new SokobanLevel(
                "#####            #####\n" +
                "#   ##############  @#\n" +
                "#     $ #   $   $ $  #\n" +
                "# $$#     $   $   $  #\n" +
                "##  ###############$##\n" +
                " #$   $.*.$   $  $  # \n" +
                " #  #   #..$$$ $ # $# \n" +
                " ## #####...   $ #  # \n" +
                " # ..#  #....  $ #$ # \n" +
                " # ..#  #.....   #  # \n" +
                "###. #  #......$## $##\n" +
                "# ## ##########  $   #\n" +
                "# #              $   #\n" +
                "#   ##############  ##\n" +
                "#####            #####\n"
            );
        }

        public static SokobanLevel TrivialLevel()
        {
            return new SokobanLevel("#####\n#@$.#\n#####\n");
        }

        public override int GetHashCode()
        {
            // We want to make sure that same levels return same hash codes.
            // Same levels will already have same string representations,
            // so use the string representation to generate the hash code.
            return ToString().GetHashCode();
        }
    }
}
