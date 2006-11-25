using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ExpertSokoban
{
    /// <summary>
    /// Represents the possible contents of a single cell in a Sokoban level.
    /// </summary>
    public enum SokobanCell
    {
        /// <summary>
        /// The cell is empty.
        /// </summary>
        Blank,

        /// <summary>
        /// The cell is a wall (unpassable).
        /// </summary>
        Wall,
        
        /// <summary>
        /// The cell is a piece (pushable).
        /// </summary>
        Piece,
        
        /// <summary>
        /// The cell is a target (the level is considered "solved" if all pieces are
        /// pushed onto all the targets).
        /// </summary>
        Target,
        
        /// <summary>
        /// The cell is a target with a piece on it.
        /// </summary>
        PieceOnTarget
    }

    /// <summary>
    /// Represents possible errors that can make a Sokoban level unplayable.
    /// </summary>
    public enum SokobanLevelStatus
    {
        /// <summary>
        /// The level is valid.
        /// </summary>
        Valid,

        /// <summary>
        /// The level is not entirely enclosed by an outer wall. This way it could be
        /// possible for the Sokoban to leave the level to the outside.
        /// </summary>
        NotEnclosed,

        /// <summary>
        /// The level is not solvable because the number of targets does not equal the
        /// number of pieces.
        /// </summary>
        TargetsPiecesMismatch
    }

    /// <summary>
    /// Represents a Sokoban level, or a specific situation during the game.
    /// (Use ToString() when comparing levels or adding them to dictionaries.)
    /// </summary>
    [Serializable]
    public class SokobanLevel : IEquatable<SokobanLevel>
    {
        /// <summary>
        /// Stores the individual cells of the level.
        /// </summary>
        private SokobanCell[] FLevel;

        /// <summary>
        /// The width of the level.
        /// </summary>
        private int FWidth;
        
        /// <summary>
        /// The height of the level.
        /// </summary>
        private int FHeight;

        /// <summary>
        /// The current position of the Sokoban.
        /// </summary>
        private Point FSokobanPos;

        /// <summary>
        /// (read-only) Returns the width of the level.
        /// </summary>
        public int Width { get { return FWidth; } }

        /// <summary>
        /// (read-only) Returns the height of the level.
        /// </summary>
        public int Height { get { return FHeight; } }

        /// <summary>
        /// (read-only) Returns the current Sokoban position.
        /// </summary>
        public Point SokobanPos { get { return FSokobanPos; } }

        /// <summary>
        /// (read-only) Returns true if the level is in a solved state
        /// (all the pieces are on a target).
        /// </summary>
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

        /// <summary>
        /// (read-only) Returns the number of pieces that are not on a target.
        /// </summary>
        public int RemainingPieces
        {
            get
            {
                int Ret = 0;
                for (int i = 0; i < FWidth*FHeight; i++)
                    if (FLevel[i] == SokobanCell.Piece)
                        Ret++;
                return Ret;
            }
        }

        /// <summary>
        /// Constructs a Sokoban level from its string representation.
        /// </summary>
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

        /// <summary>
        /// Constructs a level directly given its width, height, cell contents and Sokoban position.
        /// </summary>
        private SokobanLevel(int Width, int Height, SokobanCell[] LevelData, Point SokobanPos)
        {
            FWidth = Width;
            FHeight = Height;
            FLevel = LevelData;
            FSokobanPos = SokobanPos;
        }

        /// <summary>
        /// Returns the contents of the specified cell.
        /// </summary>
        public SokobanCell Cell(Point Pos)
        {
            return Cell(Pos.X, Pos.Y);
        }

        /// <summary>
        /// Returns the contents of the specified cell.
        /// </summary>
        public SokobanCell Cell(int x, int y)
        {
            if (x < 0 || y < 0 || x >= FWidth || y >= FHeight)
                return SokobanCell.Blank;
            return FLevel[y*FWidth + x];
        }

        /// <summary>
        /// Moves the Sokoban to the specified position.
        /// </summary>
        /// <param name="Pos"></param>
        public void SetSokobanPos(Point Pos)
        {
            if (Pos.X >= 0 && Pos.Y >= 0 && Pos.X < FWidth && Pos.Y < FHeight)
                FSokobanPos = Pos;
        }

        /// <summary>
        /// Changes the contents of the specified cell.
        /// </summary>
        private void SetCell(int Index, SokobanCell c)
        {
            if (Index >= 0 && Index < FWidth*FHeight)
                FLevel[Index] = c;
        }

        /// <summary>
        /// Changes the contents of the specified cell.
        /// </summary>
        public void SetCell(Point Pos, SokobanCell c)
        {
            SetCell(Pos.Y*FWidth + Pos.X, c);
        }

        /// <summary>
        /// Changes the contents of the specified cell.
        /// </summary>
        public void SetCell(int x, int y, SokobanCell c)
        {
            SetCell(y*FWidth + x, c);
        }

        /// <summary>
        /// Returns true if the cell identified by the specified array index contains a
        /// piece, irrespective of whether it's on a target or not.
        /// </summary>
        private bool IsPiece(int Index)
        {
            if (Index < 0 || Index > FLevel.Length) return false;
            return FLevel[Index] == SokobanCell.Piece || FLevel[Index] == SokobanCell.PieceOnTarget;
        }

        /// <summary>
        /// Returns true if the specified cell contains a piece, irrespective of whether
        /// it's on a target or not.
        /// </summary>
        public bool IsPiece(Point Pos)
        {
            if (Pos.X < 0 || Pos.X >= FWidth || Pos.Y < 0 || Pos.Y >= FHeight)
                return false;
            return IsPiece(Pos.Y*FWidth + Pos.X);
        }

        /// <summary>
        /// Returns true if the specified cell contains a piece, irrespective of whether
        /// it's on a target or not.
        /// </summary>
        public bool IsPiece(int x, int y)
        {
            if (x < 0 || x >= FWidth || y < 0 || y >= FHeight)
                return false;
            return IsPiece(y*FWidth + x);
        }

        /// <summary>
        /// Returns true if the cell identified by the specified array index is either
        /// blank or a target without a piece on it.
        /// </summary>
        private bool IsFree(int Index)
        {
            return (Index >= 0 && Index < FWidth*FHeight)
                ? (FLevel[Index] == SokobanCell.Blank || FLevel[Index] == SokobanCell.Target)
                : false;
        }

        /// <summary>
        /// Returns true if the specified cell is either blank or a target without a
        /// piece on it.
        /// </summary>
        public bool IsFree(Point Pos)
        {
            return IsFree(Pos.Y*FWidth + Pos.X);
        }

        /// <summary>
        /// Returns true if the specified cell is either blank or a target without a
        /// piece on it.
        /// </summary>
        public bool IsFree(int x, int y)
        {
            return IsFree(y*FWidth + x);
        }

        /// <summary>
        /// Adds a piece to the cell identified by the specified array index.
        /// </summary>
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

        /// <summary>
        /// Adds a piece to the specified cell. If it is a target, it becomes a piece on
        /// a target.
        /// </summary>
        public void SetPiece(Point Pos)
        {
            SetPiece(Pos.Y*FWidth + Pos.X);
        }

        /// <summary>
        /// Adds a piece to the specified cell. If it is a target, it becomes a piece on
        /// a target.
        /// </summary>
        public void SetPiece(int x, int y)
        {
            SetPiece(y*FWidth + x);
        }

        /// <summary>
        /// Removes a piece from the cell identified by the specified array index.
        /// </summary>
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

        /// <summary>
        /// Removes a piece from the specified cell. If it is a piece on a target, the
        /// cell becomes a target.
        /// </summary>
        public void RemovePiece(Point Pos)
        {
            RemovePiece(Pos.Y*FWidth + Pos.X);
        }

        /// <summary>
        /// Removes a piece from the specified cell. If it is a piece on a target, the
        /// cell becomes a target.
        /// </summary>
        public void RemovePiece(int x, int y)
        {
            RemovePiece(y*FWidth + x);
        }

        /// <summary>
        /// Moves a piece from the specified location to another.
        /// </summary>
        public void MovePiece(int FromX, int FromY, int ToX, int ToY)
        {
            RemovePiece(FromX, FromY);
            SetPiece(ToX, ToY);
        }

        /// <summary>
        /// Moves a piece from the specified location to another.
        /// </summary>
        public void MovePiece(Point FromPos, Point ToPos)
        {
            RemovePiece(FromPos);
            SetPiece(ToPos);
        }

        /// <summary>
        /// Resizes the level in such a way that there is a margin (containing all blank
        /// cells) of the specified width around the level.
        /// </summary>
        /// <param name="Margin">The size of the margin.</param>
        public void EnsureSpace(int Margin)
        {
            int Left = 0, Right = 0, Top = 0, Bottom = 0;
            while (Left < FWidth && ColumnBlank(Left)) Left++;
            while (Right < FWidth && ColumnBlank(FWidth-Right-1)) Right++;
            while (Top < FHeight && RowBlank(Top)) Top++;
            while (Bottom < FHeight && RowBlank(FHeight-Bottom-1)) Bottom++;
            Resize(2*Margin-Left-Right, 2*Margin-Top-Bottom, Margin-Left, Margin-Top);
        }

        /// <summary>
        /// Determines whether a column is entirely blank or not.
        /// </summary>
        private bool ColumnBlank(int x)
        {
            if (FSokobanPos.X == x)
                return false;
            for (int y = 0; y < FHeight; y++)
                if (FLevel[y*FWidth + x] != SokobanCell.Blank)
                    return false;
            return true;
        }

        /// <summary>
        /// Determines whether a row is entirely blank or not.
        /// </summary>
        private bool RowBlank(int y)
        {
            if (FSokobanPos.Y == y)
                return false;
            for (int x = 0; x < FWidth; x++)
                if (FLevel[y*FWidth + x] != SokobanCell.Blank)
                    return false;
            return true;
        }

        /// <summary>
        /// Resizes the level.
        /// </summary>
        /// <param name="AmountX">Amount by which to change the width of the level.</param>
        /// <param name="AmountY">Amount by which to change the height of the level.</param>
        /// <param name="ShiftX">Amount by which to shift the contents of the level in X direction.</param>
        /// <param name="ShiftY">Amount by which to shift the contents of the level in Y direction.</param>
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

        /// <summary>
        /// Returns a deep copy of this SokobanLevel object.
        /// </summary>
        public SokobanLevel Clone()
        {
            SokobanCell[] NewLevel = (SokobanCell[]) FLevel.Clone();
            return new SokobanLevel(FWidth, FHeight, NewLevel, FSokobanPos);
        }

        /// <summary>
        /// Returns a value indicating whether the level is valid, and if not, why not.
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

        /// <summary>
        /// Returns a (somewhat human-readable) string representation of this level.
        /// </summary>
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

        /// <summary>
        /// Returns a Sokoban level that can be used for testing purposes.
        /// </summary>
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

        /// <summary>
        /// Returns the smallest valid unsolved solvable level.
        /// </summary>
        public static SokobanLevel TrivialLevel()
        {
            return new SokobanLevel("#####\n#@$.#\n#####\n");
        }

        /// <summary>
        /// Returns true if the specified object represents the same level.
        /// </summary>
        public bool Equals(SokobanLevel Other)
        {
            return ToString().Equals(Other.ToString());
        }
    }
}
