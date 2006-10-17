using System;
using System.Collections.Generic;
using System.Text;

namespace ExpertSokoban
{
    public enum SokobanCell
    {
        Invalid,
        Blank,
        Wall,
        Piece,
        Target,
        PieceOnTarget
    };

    public class SokobanLevel
    {
        private SokobanCell[] FLevel;
        private int FWidth, FHeight, FSokobanPos;

        public int Width { get { return FWidth; } }
        public int Height { get { return FHeight; } }
        public int SokobanPos { get { return FSokobanPos; } }
        public int SokobanX { get { return FSokobanPos % Width; } }
        public int SokobanY { get { return FSokobanPos / Width; } }

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
            FSokobanPos = -1;
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
                        EncodedForm[i] == '*' ? SokobanCell.PieceOnTarget :
                                                SokobanCell.Blank;
                    if (EncodedForm[i] == '@')
                        FSokobanPos = (CurX + FWidth*CurY);

                    CurX++;
                }
            }

            // If the level didn't specify a starting position, try to find one.
            if (FSokobanPos == -1)
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
                            FSokobanPos = j*FWidth+i;
                            Done = true;
                        }
                    }
            }
        }
        public SokobanLevel(int Width, int Height, SokobanCell[] LevelData, int SokobanPos)
        {
            FWidth = Width;
            FHeight = Height;
            FLevel = LevelData;
            FSokobanPos = SokobanPos;
        }
        public SokobanLevel(int Width, int Height)
        {
            FWidth = Width;
            FHeight = Height;
            FLevel = new SokobanCell[FWidth*FHeight];
            for (int i = 0; i < FWidth * FHeight; i++)
                FLevel[i] = ((i+1) % FWidth < 2) ? SokobanCell.Wall : SokobanCell.Blank;
            FSokobanPos = FWidth+1;
        }
        public SokobanCell Cell(int Pos)
        {
            return Cell(Pos % FWidth, Pos / FWidth);
        }
        public SokobanCell Cell(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                return SokobanCell.Invalid;
            return FLevel[y*FWidth + x];
        }
        public void SetSokobanPos(int Pos) { FSokobanPos = Pos; }
        public void SetSokobanPos(int x, int y) { FSokobanPos = y * FWidth + x; }
        public void SetCell(int x, int y, SokobanCell c) { FLevel[y*FWidth + x] = c; }
        public bool IsPiece(int Pos)
        {
            if (Pos < 0 || Pos > FLevel.Length) return false;
            return FLevel[Pos] == SokobanCell.Piece || FLevel[Pos] == SokobanCell.PieceOnTarget;
        }
        public bool IsPiece(int x, int y) { return IsPiece(y*FWidth + x); }
        public bool IsFree(int Pos) { return FLevel[Pos] == SokobanCell.Blank || FLevel[Pos] == SokobanCell.Target; }
        public bool IsFree(int x, int y) { return IsFree(y*FWidth + x); }
        public void SetPiece(int x, int y) { SetPiece(y*FWidth + x); }
        public void SetPiece(int Pos)
        {
            if (FLevel[Pos] == SokobanCell.Blank)
                FLevel[Pos] = SokobanCell.Piece;
            else if (FLevel[Pos] == SokobanCell.Target)
                FLevel[Pos] = SokobanCell.PieceOnTarget;
        }
        public void RemovePiece(int x, int y) { RemovePiece(y*FWidth + x); }
        public void RemovePiece(int Pos)
        {
            if (FLevel[Pos] == SokobanCell.Piece)
                FLevel[Pos] = SokobanCell.Blank;
            else if (FLevel[Pos] == SokobanCell.PieceOnTarget)
                FLevel[Pos] = SokobanCell.Target;
        }
        public void MovePiece(int FromX, int FromY, int ToX, int ToY)
        {
            RemovePiece(FromX, FromY);
            SetPiece(ToX, ToY);
        }
        public void MovePiece(int FromPos, int ToPos)
        {
            RemovePiece(FromPos);
            SetPiece(ToPos);
        }

        // This will reduce the size of the level if there are rows or columns
        // around the edge that are entirely blank, but will leave (or create)
        // a one-square margin around the edge.
        public void EnsureSpace()
        {
            int Left = 0, Right = 0, Top = 0, Bottom = 0;
            while (Left < FWidth && ColumnBlank(Left)) Left++;
            while (Right < FWidth && ColumnBlank(FWidth-Right-1)) Right++;
            while (Top < FHeight && RowBlank(Top)) Top++;
            while (Bottom < FHeight && RowBlank(FHeight-Bottom-1)) Bottom++;
            Resize(2-Left-Right, 2-Top-Bottom, 1-Left, 1-Top);
        }
        private bool ColumnBlank(int x)
        {
            for (int y = 0; y < FHeight; y++)
                if (FLevel[y*FWidth + x] != SokobanCell.Blank)
                    return false;
            return true;
        }
        private bool RowBlank(int y)
        {
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

            FSokobanPos += (FSokobanPos / FWidth) * AmountX + ShiftX;
            FSokobanPos += ShiftY * NewWidth;

            FLevel = NewLevel;
            FWidth = NewWidth;
            FHeight = NewHeight;
        }

        public SokobanLevel Clone()
        {
            SokobanCell[] NewLevel = (SokobanCell[]) FLevel.Clone();
            return new SokobanLevel(FWidth, FHeight, NewLevel, FSokobanPos);
        }

        public int PosToX(int pos)
        {
            return pos % FWidth;
        }

        public int PosToY(int pos)
        {
            return pos / FWidth;
        }

        public static string TestLevelEncoded = 
            "#####            #####" +
            "#   ##############  @#" +
            "#     $ #   $   $ $  #" +
            "# $$#     $   $   $  #" +
            "##  ###############$##" +
            " #$   $.*.$   $  $  # " +
            " #  #   #..$$$ $ # $# " +
            " ## #####...   $ #  # " +
            " # ..#  #....  $ #$ # " +
            " # ..#  #.....   #  # " +
            "###. #  #......$## $##" +
            "# ## ##########  $   #" +
            "# #              $   #" +
            "#   ##############  ##" +
            "#####            #####";

        public static SokobanLevel TestLevel()
        {
            int Width = 22;
            int Height = 15;
            int SokPos = -1;
            SokobanCell[] LevelData = new SokobanCell[Width * Height];
            for (int i = 0; i < TestLevelEncoded.Length; i++)
            {
                LevelData[i] = 
                    TestLevelEncoded[i] == '#' ? SokobanCell.Wall :
                    TestLevelEncoded[i] == '$' ? SokobanCell.Piece :
                    TestLevelEncoded[i] == '.' ? SokobanCell.Target :
                    TestLevelEncoded[i] == '*' ? SokobanCell.PieceOnTarget : SokobanCell.Blank;
                if (TestLevelEncoded[i] == '@')
                    SokPos = i;
            }
            // If the level didn't specify a starting position, try to find one.
            if (SokPos == -1)
            {
                bool Done = false;
                for (int i = 0; i < Width && !Done; i++)
                    for (int j = 0; j < Height && !Done; j++)
                    {
                        bool xIn = false, yIn = false;
                        for (int x = 0; x <= i; x++)
                            if (LevelData[j*Width + x] == SokobanCell.Wall &&
                            (x == 0 || LevelData[j*Width + x-1] != SokobanCell.Wall))
                                xIn = !xIn;
                        for (int y = 0; y <= j; y++)
                            if (LevelData[y*Width + i] == SokobanCell.Wall &&
                            (y == 0 || LevelData[(y-1)*Width + i] != SokobanCell.Wall))
                                yIn = !yIn;
                        if (xIn && yIn)
                        {
                            SokPos = j*Width+i;
                            Done = true;
                        }
                    }
            }
            return new SokobanLevel(Width, Height, LevelData, SokPos);
        }
    }
}
