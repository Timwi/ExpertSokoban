using System;
using System.Drawing;

namespace ExpertSokoban
{
    /// <summary>
    /// Represents the possible contents of a single cell in a Sokoban level.
    /// </summary>
    enum SokobanCell
    {
        /// <summary>The cell is empty.</summary>
        Blank,

        /// <summary>The cell is a wall (unpassable).</summary>
        Wall,

        /// <summary>The cell is a piece (pushable).</summary>
        Piece,

        /// <summary>The cell is a target. The level is considered "solved" if all pieces are pushed onto all the targets.</summary>
        Target,

        /// <summary>The cell is a target with a piece on it.</summary>
        PieceOnTarget
    }

    /// <summary>Represents possible errors that can make a Sokoban level unplayable.</summary>
    enum SokobanLevelStatus
    {
        /// <summary>The level is valid.</summary>
        Valid,

        /// <summary>The level is not entirely enclosed by an outer wall. This way it could be
        /// possible for the Sokoban to leave the level to the outside.</summary>
        NotEnclosed,

        /// <summary>The level is not solvable because the number of targets does not equal the
        /// number of pieces.</summary>
        TargetsPiecesMismatch
    }

    /// <summary>Represents a Sokoban level, or a specific situation during the game.</summary>
    [Serializable]
    sealed class SokobanLevel : IEquatable<SokobanLevel>
    {
        /// <summary>Stores the individual cells of the level.</summary>
        private SokobanCell[] _cells;

        /// <summary>The width of the level.</summary>
        private int _width;

        /// <summary>The height of the level.</summary>
        private int _height;

        /// <summary>The current position of the Sokoban.</summary>
        private Point _sokobanPos;

        /// <summary>(read-only) Returns the width of the level.</summary>
        public int Width { get { return _width; } }

        /// <summary>(read-only) Returns the height of the level.</summary>
        public int Height { get { return _height; } }

        /// <summary>(read-only) Returns the current Sokoban position.</summary>
        public Point SokobanPos { get { return _sokobanPos; } }

        /// <summary>(read-only) Returns true if the level is in a solved state (all the pieces are on a target).</summary>
        public bool Solved
        {
            get
            {
                for (int i = 0; i < _width * _height; i++)
                    if (_cells[i] == SokobanCell.Target || _cells[i] == SokobanCell.Piece)
                        return false;
                return true;
            }
        }

        /// <summary>(read-only) Returns the number of pieces that are not on a target.</summary>
        public int RemainingPieces
        {
            get
            {
                int ret = 0;
                for (int i = 0; i < _width * _height; i++)
                    if (_cells[i] == SokobanCell.Piece)
                        ret++;
                return ret;
            }
        }

        /// <summary>Constructs a Sokoban level from its string representation.</summary>
        public SokobanLevel(string encodedForm)
        {
            _width = 0;
            _height = 0;
            bool haveSokobanPos = false;
            int curX = 0;
            for (int i = 0; i < encodedForm.Length; i++)
            {
                if (encodedForm[i] == '\n')
                {
                    _height++;
                    if (_width < curX)
                        _width = curX;
                    curX = 0;
                }
                else
                    curX++;
            }
            _cells = new SokobanCell[_width * _height];
            curX = 0;
            int curY = 0;
            for (int i = 0; i < encodedForm.Length; i++)
            {
                if (encodedForm[i] == '\n')
                {
                    curX = 0;
                    curY++;
                }
                else
                {
                    _cells[curX + _width * curY] =
                        encodedForm[i] == '#' ? SokobanCell.Wall :
                        encodedForm[i] == '$' ? SokobanCell.Piece :
                        encodedForm[i] == '.' ? SokobanCell.Target :
                        encodedForm[i] == '+' ? SokobanCell.Target :
                        encodedForm[i] == '*' ? SokobanCell.PieceOnTarget :
                                                SokobanCell.Blank;
                    if (encodedForm[i] == '@' || encodedForm[i] == '+')
                    {
                        _sokobanPos = new Point(curX, curY);
                        haveSokobanPos = true;
                    }

                    curX++;
                }
            }

            // If the level didn't specify a starting position, try to find one.
            if (!haveSokobanPos)
            {
                bool done = false;
                for (int i = 0; i < _width && !done; i++)
                    for (int j = 0; j < _height && !done; j++)
                    {
                        bool xIn = false, yIn = false;
                        for (int x = 0; x <= i; x++)
                            if (_cells[j * _width + x] == SokobanCell.Wall && (x == 0 || _cells[j * _width + x - 1] != SokobanCell.Wall))
                                xIn = !xIn;
                        for (int y = 0; y <= j; y++)
                            if (_cells[y * _width + i] == SokobanCell.Wall && (y == 0 || _cells[(y - 1) * _width + i] != SokobanCell.Wall))
                                yIn = !yIn;
                        if (xIn && yIn)
                        {
                            _sokobanPos = new Point(i, j);
                            done = true;
                        }
                    }
            }
        }

        /// <summary>
        /// Constructs a level directly given its width, height, cell contents and Sokoban position.
        /// </summary>
        private SokobanLevel(int width, int height, SokobanCell[] levelData, Point sokobanPos)
        {
            _width = width;
            _height = height;
            _cells = levelData;
            _sokobanPos = sokobanPos;
        }

        /// <summary>Returns the contents of the specified cell.</summary>
        public SokobanCell Cell(Point pos)
        {
            return Cell(pos.X, pos.Y);
        }

        /// <summary>Returns the contents of the specified cell.</summary>
        public SokobanCell Cell(int x, int y)
        {
            if (x < 0 || y < 0 || x >= _width || y >= _height)
                return SokobanCell.Blank;
            return _cells[y * _width + x];
        }

        /// <summary>Moves the Sokoban to the specified position.</summary>
        /// <param name="pos">Position to move the Sokoban to.</param>
        public void SetSokobanPos(Point pos)
        {
            if (pos.X >= 0 && pos.Y >= 0 && pos.X < _width && pos.Y < _height)
                _sokobanPos = pos;
        }

        /// <summary>Changes the contents of the specified cell.</summary>
        private void setCell(int index, SokobanCell c)
        {
            if (index >= 0 && index < _width * _height)
                _cells[index] = c;
        }

        /// <summary>Changes the contents of the specified cell.</summary>
        public void SetCell(Point pos, SokobanCell c)
        {
            setCell(pos.Y * _width + pos.X, c);
        }

        /// <summary>Changes the contents of the specified cell.</summary>
        public void SetCell(int x, int y, SokobanCell c)
        {
            setCell(y * _width + x, c);
        }

        /// <summary>
        /// Returns true if the cell identified by the specified <paramref name="index"/>
        /// contains a piece, irrespective of whether it's on a target or not.
        /// </summary>
        private bool isPiece(int index)
        {
            if (index < 0 || index > _cells.Length) return false;
            return _cells[index] == SokobanCell.Piece || _cells[index] == SokobanCell.PieceOnTarget;
        }

        /// <summary>
        /// Returns true if the specified cell contains a piece, irrespective of whether
        /// it's on a target or not.
        /// </summary>
        public bool IsPiece(Point pos)
        {
            if (pos.X < 0 || pos.X >= _width || pos.Y < 0 || pos.Y >= _height)
                return false;
            return isPiece(pos.Y * _width + pos.X);
        }

        /// <summary>
        /// Returns true if the specified cell contains a piece, irrespective of whether
        /// it's on a target or not.
        /// </summary>
        public bool IsPiece(int x, int y)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height)
                return false;
            return isPiece(y * _width + x);
        }

        /// <summary>
        /// Returns true if the cell identified by the specified array index is either
        /// blank or a target without a piece on it.
        /// </summary>
        private bool isFree(int index)
        {
            return (index >= 0 && index < _width * _height)
                ? (_cells[index] == SokobanCell.Blank || _cells[index] == SokobanCell.Target)
                : false;
        }

        /// <summary>
        /// Returns true if the specified cell is either blank or a target without a
        /// piece on it.
        /// </summary>
        public bool IsFree(Point pos)
        {
            return isFree(pos.Y * _width + pos.X);
        }

        /// <summary>
        /// Returns true if the specified cell is either blank or a target without a
        /// piece on it.
        /// </summary>
        public bool IsFree(int x, int y)
        {
            return isFree(y * _width + x);
        }

        /// <summary>
        /// Adds a piece to the cell identified by the specified array index.
        /// </summary>
        private void setPiece(int index)
        {
            if (index >= 0 && index < _width * _height)
            {
                if (_cells[index] == SokobanCell.Blank)
                    _cells[index] = SokobanCell.Piece;
                else if (_cells[index] == SokobanCell.Target)
                    _cells[index] = SokobanCell.PieceOnTarget;
            }
        }

        /// <summary>Adds a piece to the specified cell. If it is a target, it becomes a piece on a target.</summary>
        public void SetPiece(Point pos)
        {
            setPiece(pos.Y * _width + pos.X);
        }

        /// <summary>Adds a piece to the specified cell. If it is a target, it becomes a piece on a target.</summary>
        public void SetPiece(int x, int y)
        {
            setPiece(y * _width + x);
        }

        /// <summary>Removes a piece from the cell identified by the specified <paramref name="index"/>.</summary>
        private void removePiece(int index)
        {
            if (index >= 0 && index < _width * _height)
            {
                if (_cells[index] == SokobanCell.Piece)
                    _cells[index] = SokobanCell.Blank;
                else if (_cells[index] == SokobanCell.PieceOnTarget)
                    _cells[index] = SokobanCell.Target;
            }
        }

        /// <summary>Removes a piece from the specified cell. If it is a piece on a target, the cell becomes a target.</summary>
        public void RemovePiece(Point pos)
        {
            removePiece(pos.Y * _width + pos.X);
        }

        /// <summary>Removes a piece from the specified cell. If it is a piece on a target, the cell becomes a target.</summary>
        public void RemovePiece(int x, int y)
        {
            removePiece(y * _width + x);
        }

        /// <summary>Moves a piece from the specified location to another.</summary>
        public void MovePiece(int fromX, int fromY, int toX, int toY)
        {
            RemovePiece(fromX, fromY);
            SetPiece(toX, toY);
        }

        /// <summary>
        /// Moves a piece from the specified location to another.
        /// </summary>
        public void MovePiece(Point fromPos, Point toPos)
        {
            RemovePiece(fromPos);
            SetPiece(toPos);
        }

        /// <summary>
        /// Resizes the level in such a way that there is a margin (containing all blank
        /// cells) of the specified width around the level.
        /// </summary>
        /// <param name="margin">The size of the margin.</param>
        public void EnsureSpace(int margin)
        {
            int left = 0, right = 0, top = 0, bottom = 0;
            while (left < _width && columnBlank(left)) left++;
            while (right < _width && columnBlank(_width - right - 1)) right++;
            while (top < _height && rowBlank(top)) top++;
            while (bottom < _height && rowBlank(_height - bottom - 1)) bottom++;
            resize(2 * margin - left - right, 2 * margin - top - bottom, margin - left, margin - top);
        }

        /// <summary>Determines whether a column is entirely blank or not.</summary>
        private bool columnBlank(int x)
        {
            if (_sokobanPos.X == x)
                return false;
            for (int y = 0; y < _height; y++)
                if (_cells[y * _width + x] != SokobanCell.Blank)
                    return false;
            return true;
        }

        /// <summary>Determines whether a row is entirely blank or not.</summary>
        private bool rowBlank(int y)
        {
            if (_sokobanPos.Y == y)
                return false;
            for (int x = 0; x < _width; x++)
                if (_cells[y * _width + x] != SokobanCell.Blank)
                    return false;
            return true;
        }

        /// <summary>Resizes the level.</summary>
        /// <param name="amountX">Amount by which to change the width of the level.</param>
        /// <param name="amountY">Amount by which to change the height of the level.</param>
        /// <param name="shiftX">Amount by which to shift the contents of the level in X direction.</param>
        /// <param name="shiftY">Amount by which to shift the contents of the level in Y direction.</param>
        private void resize(int amountX, int amountY, int shiftX, int shiftY)
        {
            if (amountX == 0 && amountY == 0) return;

            int newWidth = _width + amountX;
            int newHeight = _height + amountY;
            SokobanCell[] newLevel = new SokobanCell[newWidth * newHeight];

            for (int x = 0; x < (amountX < 0 ? newWidth : _width); x++)
                for (int y = 0; y < (amountY < 0 ? newHeight : _height); y++)
                    newLevel[(amountY > 0 ? y + shiftY : y) * newWidth +
                             (amountX > 0 ? x + shiftX : x)] =
                    _cells[(amountY < 0 ? y - shiftY : y) * _width +
                             (amountX < 0 ? x - shiftX : x)];

            _sokobanPos.Offset(shiftX, shiftY);

            _cells = newLevel;
            _width = newWidth;
            _height = newHeight;
        }

        /// <summary>Returns a deep copy of this <see cref="SokobanLevel"/> object.</summary>
        public SokobanLevel Clone()
        {
            SokobanCell[] newLevel = (SokobanCell[]) _cells.Clone();
            return new SokobanLevel(_width, _height, newLevel, _sokobanPos);
        }

        /// <summary>Returns a value indicating whether the level is valid, and if not, why not.</summary>
        public SokobanLevelStatus Validity
        {
            get
            {
                // Check if the number of pieces equals the number of targets
                int pieces = 0, targets = 0;
                for (int i = 0; i < _cells.Length; i++)
                    if (_cells[i] == SokobanCell.Piece)
                        pieces++;
                    else if (_cells[i] == SokobanCell.Target)
                        targets++;
                if (pieces != targets)
                    return SokobanLevelStatus.TargetsPiecesMismatch;

                // Check if the level is properly enclosed
                MoveFinder finder = new MoveFinder(this, MoveFinderOption.IgnorePieces);
                for (int i = 0; i < _width; i++)
                    if (finder.Get(i, 0) || finder.Get(i, _height - 1))
                        return SokobanLevelStatus.NotEnclosed;
                for (int i = 0; i < _height; i++)
                    if (finder.Get(0, i) || finder.Get(_width - 1, i))
                        return SokobanLevelStatus.NotEnclosed;

                return SokobanLevelStatus.Valid;
            }
        }

        /// <summary>Returns a (somewhat human-readable) string representation of this level.</summary>
        public override string ToString()
        {
            char[] chars = new char[(_width + 1) * _height];
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                    chars[y * (_width + 1) + x] =
                        _cells[y * _width + x] == SokobanCell.Blank && x == _sokobanPos.X && y == _sokobanPos.Y ? '@' :
                        _cells[y * _width + x] == SokobanCell.Target && x == _sokobanPos.X && y == _sokobanPos.Y ? '+' :
                        _cells[y * _width + x] == SokobanCell.Piece ? '$' :
                        _cells[y * _width + x] == SokobanCell.PieceOnTarget ? '*' :
                        _cells[y * _width + x] == SokobanCell.Target ? '.' :
                        _cells[y * _width + x] == SokobanCell.Wall ? '#' : ' ';
                chars[y * (_width + 1) + _width] = '\n';
            }
            return new string(chars);
        }

        /// <summary>Returns a Sokoban level that can be used for testing purposes.</summary>
        public static SokobanLevel TestLevel
        {
            get
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
        }

        /// <summary>Returns the smallest valid unsolved solvable level.</summary>
        public static SokobanLevel TrivialLevel { get { return new SokobanLevel("#####\n#@$.#\n#####\n"); } }

        /// <summary>Returns true if the specified object represents the same level as this one.</summary>
        public bool Equals(SokobanLevel other)
        {
            return ToString().Equals(other.ToString());
        }

        /// <summary>Returns the hash code for this <see cref="SokobanLevel"/>.</summary>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
