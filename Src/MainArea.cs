using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using RT.Util;
using RT.Util.Controls;
using RT.Util.Dialogs;
using RT.Util.Drawing;

namespace ExpertSokoban
{
    /// <summary>
    /// Encapsulates the different states the Main Area can be in.
    /// Used by the property MainArea.State.
    /// </summary>
    public enum MainAreaState
    {
        /// <summary>
        /// The Main Area is empty. It is not displaying a level. In this state, the
        /// Main Area does not respond to any input.
        /// </summary>
        Null,

        /// <summary>
        /// The Main Area is in playing state, and the user has not selected a piece.
        /// </summary>
        Move,

        /// <summary>
        /// The Main Area is in playing state, and the user has selected a piece.
        /// </summary>
        Push,

        /// <summary>
        /// The Main Area is in playing state, and the user has solved the level. In
        /// this state, the Main Area does not respond to any input.
        /// </summary>
        Solved,

        /// <summary>
        /// The Main Area is in editing state (the user can edit the level). Use the
        /// MainArea.Tool property to specify what a mouse click should do.
        /// </summary>
        Editing
    }

    /// <summary>
    /// Encapsulates the different tools a user can use while editing a level.
    /// Used by the property MainArea.Tool.
    /// </summary>
    public enum MainAreaTool
    {
        /// <summary>
        /// Clicking a cell places or removes a wall.
        /// </summary>
        Wall,

        /// <summary>
        /// Clicking a cell places or removes a piece.
        /// </summary>
        Piece,

        /// <summary>
        /// Clicking a cell places or removes a target.
        /// </summary>
        Target,

        /// <summary>
        /// Clicking a cell moves the Sokoban's starting position.
        /// </summary>
        Sokoban
    }

    /// <summary>
    /// Encapsulates the different options available for drawing the two different paths
    /// (move path and push path) while the user selects a destination cell for a piece.
    /// Used by the properties MainArea.MoveDrawMode and MainArea.PushDrawMode.
    /// </summary>
    public enum PathDrawMode
    {
        /// <summary>
        /// The path is not displayed.
        /// </summary>
        None,

        /// <summary>
        /// The path is displayed as a line with rounded corners.
        /// </summary>
        Line,

        /// <summary>
        /// The path is displayed as a sequence of arrows connecting the cells.
        /// </summary>
        Arrows,

        /// <summary>
        /// The path is displayed using dots in the middle of the cells.
        /// </summary>
        Dots
    }

    /// <summary>
    /// Abstract base class for items in the Undo buffer.
    /// Subclasses are UndoMoveItem and UndoPushItem.
    /// </summary>
    public abstract class UndoItem { }

    /// <summary>
    /// An item in the Undo buffer representing a Sokoban move (but no push).
    /// </summary>
    public class UndoMoveItem : UndoItem
    {
        /// <summary>
        /// The position from where the Sokoban moved.
        /// </summary>
        public Point MovedFrom;

        /// <summary>
        /// The position the Sokoban moved to.
        /// </summary>
        public Point MovedTo;

        /// <summary>
        /// The number of cells moved by the Sokoban.
        /// </summary>
        public int Moves;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="from">The position from where the Sokoban moved.</param>
        /// <param name="to">The position the Sokoban moved to.</param>
        /// <param name="moves">The number of moves required to go from 'from' to 'to'.</param>
        public UndoMoveItem(Point from, Point to, int moves)
        {
            MovedFrom = from;
            MovedTo = to;
            Moves = moves;
        }
    }

    /// <summary>
    /// An item in the Undo buffer representing a push (including the corresponding move).
    /// </summary>
    public class UndoPushItem : UndoItem
    {
        /// <summary>
        /// The position from where the Sokoban moved.
        /// </summary>
        public Point MovedSokobanFrom;

        /// <summary>
        /// The position the Sokoban moved to.
        /// </summary>
        public Point MovedSokobanTo;

        /// <summary>
        /// The original position of the piece pushed.
        /// </summary>
        public Point MovedPieceFrom;

        /// <summary>
        /// The final position of the piece pushed.
        /// </summary>
        public Point MovedPieceTo;

        /// <summary>
        /// The number of cells moved by the Sokoban.
        /// </summary>
        public int Moves;

        /// <summary>
        /// The number of cells the piece was pushed.
        /// </summary>
        public int Pushes;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sFrom">The position from where the Sokoban moved.</param>
        /// <param name="sTo">The position the Sokoban moved to.</param>
        /// <param name="pFrom">The original position of the piece pushed.</param>
        /// <param name="pTo">The final position of the piece pushed.</param>
        /// <param name="moves">The number of moves required to go from 'sFrom' to 'sTo'.</param>
        /// <param name="pushes">The number of pushes required to push the piece for 'pFrom' to 'pTo'.</param>
        public UndoPushItem(Point sFrom, Point sTo, Point pFrom, Point pTo, int moves, int pushes)
        {
            MovedSokobanFrom = sFrom;
            MovedSokobanTo = sTo;
            MovedPieceFrom = pFrom;
            MovedPieceTo = pTo;
            Moves = moves;
            Pushes = pushes;
        }
    }

    /// <summary>
    /// Encapsulates the Main Area of Expert Sokoban's main form -- the area in which a
    /// level is played or edited.
    /// </summary>
    public class MainArea : DoubleBufferedPanel
    {
        /// <summary>
        /// Gets the current state of the main area.
        /// </summary>
        public MainAreaState State { get { return _state; } }

        /// <summary>
        /// If playing, true if the player made any moves. If editing, true
        /// if any changes have been made to the level.
        /// </summary>
        public bool Modified
        {
            get { return _modified; }
            set
            {
                if (value)
                    Ut.InternalError("Not allowed to set Modified to true");
                else
                    _modified = false;
            }
        }

        /// <summary>
        /// Gets or sets how the move path should be displayed while selecting a
        /// destination cell for a piece.
        /// </summary>
        public PathDrawMode MoveDrawMode
        {
            get { return _moveDrawMode; }
            set { _moveDrawMode = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets how the push path should be displayed while selecting a
        /// destination cell for a piece.
        /// </summary>
        public PathDrawMode PushDrawMode
        {
            get { return _pushDrawMode; }
            set { _pushDrawMode = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets whether the Sokoban and piece end-position should be displayed
        /// while selecting a destination cell for a piece.
        /// </summary>
        public bool ShowEndPos
        {
            get { return _showEndPos; }
            set { _showEndPos = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets whether the area the Sokoban can reach (the "move area")
        /// should be displayed or not.
        /// </summary>
        public bool ShowAreaSokoban
        {
            get { return _showAreaSokoban; }
            set { _showAreaSokoban = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets whether the area the selected piece can be pushed to
        /// (the "push area") should be displayed or not.
        /// </summary>
        public bool ShowAreaPiece
        {
            get { return _showAreaPiece; }
            set { _showAreaPiece = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets whether sound should be played or not.
        /// </summary>
        public bool SoundEnabled
        {
            get { return _soundEnabled; }
            set { _soundEnabled = value; }
        }

        /// <summary>
        /// The currently selected tool while editing a level.
        /// </summary>
        public MainAreaTool Tool
        {
            get { return _tool; }
            set { _tool = value; }
        }

        /// <summary>
        /// (read-only) The currently displayed level, or null if none.
        /// </summary>
        public SokobanLevel Level { get { return _state == MainAreaState.Null ? null : _level; } }

        /// <summary>
        /// (read-only) Returns the number of moves performed by the Sokoban so far.
        /// </summary>
        public int Moves { get { return _moves; } }

        /// <summary>
        /// (read-only) Returns the number of pushes performed by the Sokoban so far.
        /// </summary>
        public int Pushes { get { return _pushes; } }

        /// <summary>
        /// Triggers when the player solves (completes) a level.
        /// </summary>
        public event EventHandler LevelSolved;

        /// <summary>
        /// The currently displayed level. Value may be invalid if State == Null.
        /// </summary>
        private SokobanLevel _level;

        /// <summary>
        /// The renderer used to display the current level. Will be re-created whenever
        /// the Main Area is resized.
        /// </summary>
        private Renderer _renderer;

        /// <summary>
        /// In playing mode, determines what cells the player can move to from their
        /// current position. In editing mode, determines whether the level is properly
        /// enclosed by walls or not.
        /// </summary>
        private MoveFinder _moveFinder;

        /// <summary>
        /// In playing mode, once a piece has been selected, determines where said piece
        /// can be pushed to (and what would be the shortest push-path).
        /// </summary>
        private PushFinder _pushFinder;

        /// <summary>
        /// The state the Main Area is in.
        /// </summary>
        private MainAreaState _state;

        /// <summary>
        /// Indicates whether a move has been made or any changed to the level being edited.
        /// </summary>
        private bool _modified;

        /// <summary>
        /// The currently selected tool for editing.
        /// </summary>
        private MainAreaTool _tool;

        /// <summary>
        /// The currently selected option for displaying the move path.
        /// </summary>
        private PathDrawMode _moveDrawMode;

        /// <summary>
        /// The currently selected option for displaying the push path.
        /// </summary>
        private PathDrawMode _pushDrawMode;

        /// <summary>
        /// Whether the Sokoban and piece end-position should be displayed while
        /// selecting a destination cell for a piece.
        /// </summary>
        private bool _showEndPos;

        /// <summary>
        /// Whether the area reachable by the Sokoban (the "move area")
        /// should be displayed.
        /// </summary>
        private bool _showAreaSokoban;

        /// <summary>
        /// Whether the area reachable by the selected piece (the "push area")
        /// should be displayed.
        /// </summary>
        private bool _showAreaPiece;

        /// <summary>
        /// Whether sound is enabled or not.
        /// </summary>
        private bool _soundEnabled;

        /// <summary>
        /// The brush used to fill the area that indicates where the Sokoban can move.
        /// </summary>
        private Brush _moveBrush = new SolidBrush(Color.FromArgb(32, 0, 255, 0));

        /// <summary>
        /// The pen used to outline the area that indicates where the Sokoban can move.
        /// </summary>
        private Pen _movePen = new Pen(Color.FromArgb(128, 0, 255, 0), 1.5f);

        /// <summary>
        /// The brush used to fill the area that indicates where the selected piece
        /// can be pushed.
        /// </summary>
        private Brush _pushBrush = new SolidBrush(Color.FromArgb(32, 0, 0, 255));

        /// <summary>
        /// The pen used to outline the area that indicates where the selected piece
        /// can be pushed.
        /// </summary>
        private Pen _pushPen = new Pen(Color.FromArgb(128, 0, 0, 255), 1.5f);

        /// <summary>
        /// The brush used to fill the area around the level that alerts the user to the
        /// fact that the level is not properly enclosed by a wall.
        /// </summary>
        private Brush _editInvalidBrush = new SolidBrush(Color.FromArgb(64, 255, 0, 0));

        /// <summary>
        /// The pen used to outline the area around the level that alerts the user to
        /// the fact that the level is not properly enclosed by a wall.
        /// </summary>
        private Pen _editInvalidPen = new Pen(Color.FromArgb(255, 255, 0, 0), 1.5f);

        /// <summary>
        /// The brush used to display the move path in Dots mode.
        /// </summary>
        private Brush _movePathBrush = new SolidBrush(Color.FromArgb(0, 128, 0));

        /// <summary>
        /// The pen used to display the move path in Line mode.
        /// </summary>
        private Pen _movePathPen = new Pen(Color.FromArgb(255, 0, 192, 0), 2f);

        /// <summary>
        /// The brush used to display the push path in Dots mode.
        /// </summary>
        private Brush _pushPathBrush = new SolidBrush(Color.FromArgb(0, 0, 128));

        /// <summary>
        /// The pen used to display the push path in Line mode.
        /// </summary>
        private Pen _pushPathPen = new Pen(Color.FromArgb(0, 0, 0x80), 3.5f);

        /// <summary>
        /// The pen used to display the cursor (for keyboard selection).
        /// </summary>
        private Pen _cursorPen = new Pen(Color.FromArgb(0, 0, 0x80), 3.5f);

        /// <summary>
        /// Sound played when a level is solved.
        /// </summary>
        private SoundPlayerAsync _sndLevelSolved = new SoundPlayerAsync(Properties.Resources.SndLevelDone);

        /// <summary>
        /// Sound played when an invalid destination cell for a piece is clicked in
        /// playing mode, or when an invalid operation is attempted in editing mode.
        /// </summary>
        private SoundPlayerAsync _sndMeep = new SoundPlayerAsync(Properties.Resources.SndMeep);

        /// <summary>
        /// Sound played when a move is performed in playing mode, or when a piece,
        /// target, or the Sokoban starting position is placed in editing mode.
        /// </summary>
        private SoundPlayerAsync _sndPiecePlaced = new SoundPlayerAsync(Properties.Resources.SndPiecePlaced);

        /// <summary>
        /// Sound played when a wall is added or removed in editing mode.
        /// </summary>
        private SoundPlayerAsync _sndEditorClick = new SoundPlayerAsync(Properties.Resources.SndEditorClick);

        /// <summary>
        /// The currently selected piece for pushing (or null if none).
        /// </summary>
        private Point? _selectedPiece = null;

        /// <summary>
        /// The cell that is currently selected using the keyboard arrow keys.
        /// </summary>
        private Point? _cursorPos = null;

        /// <summary>
        /// Remembers the cell on which the spacebar was pressed. This enables the user
        /// to determine which of the four side of the piece the Sokoban should end up
        /// at.
        /// </summary>
        private Point? _origKeyDown = null;

        /// <summary>
        /// Remembers the cell on which the mouse button was pressed when the user
        /// starts dragging the mouse.
        /// </summary>
        private Point? _origMouseDown = null;

        /// <summary>
        /// Remembers what cell the mouse is over. This way, things like the move path
        /// and push path only need to be redrawn if it actually changes.
        /// </summary>
        private Point? _mouseOverCell = null;

        /// <summary>
        /// Remembers the entire history of actions by the player to allow for
        /// arbitrary undo.
        /// </summary>
        private Stack<UndoItem> _undo = new Stack<UndoItem>();

        /// <summary>
        /// Remembers the history of undone actions to allow for redo.
        /// </summary>
        private Stack<UndoItem> _redo = new Stack<UndoItem>();

        /// <summary>
        /// The currently-displayed push path, encoded as a sequence of cell co-ordinates.
        /// </summary>
        private Point[] _pushSequence;

        /// <summary>
        /// The currently-displayed move path, encoded as a sequence of cell co-ordinates.
        /// </summary>
        private Point[] _moveSequence;

        /// <summary>
        /// The end-position of the Sokoban under the currently-displayed push path.
        /// </summary>
        private Point _cellSeqSokoban;

        /// <summary>
        /// The number of moves performed by the Sokoban so far.
        /// </summary>
        private int _moves = 0;

        /// <summary>
        /// The number of pushes performed by the Sokoban so far.
        /// </summary>
        private int _pushes = 0;

        /// <summary>
        /// Main constructor.
        /// </summary>
        public MainArea()
        {
            SetStyle(ControlStyles.Selectable, true);
            _moveDrawMode = PathDrawMode.Line;
            _pushDrawMode = PathDrawMode.Arrows;
            _level = null;
            _renderer = null;
            _state = MainAreaState.Null;
            this.MouseDown += new MouseEventHandler(mouseDown);
            this.MouseMove += new MouseEventHandler(mouseMove);
            this.MouseUp += new MouseEventHandler(mouseUp);
            this.Paint += new PaintEventHandler(paint);
            this.PaintBuffer += new PaintEventHandler(paintBuffer);
            this.KeyDown += new KeyEventHandler(keyDown);
        }

        /// <summary>
        /// Invoked whenever the paint buffer needs to be redrawn. Renders the current
        /// level in its current state and (if applicable) the "level solved" message,
        /// but not the selected piece, move/push region, move/push path, or the
        /// Sokoban/piece end position (those are all handled in MainArea_Paint()).
        /// </summary>
        private void paintBuffer(object sender, PaintEventArgs e)
        {
            if (_state == MainAreaState.Null)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Control)),
                    new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
                return;
            }

            _renderer = new Renderer(_level, ClientSize);
            _renderer.Render(e.Graphics);

            if (_state == MainAreaState.Solved)
            {
                Image ImgLevelSolved = Properties.Resources.Skin_LevelSolved;
                if (ClientSize.Width < ImgLevelSolved.Width)
                    e.Graphics.DrawImage(ImgLevelSolved,
                        0, (ClientSize.Height - ClientSize.Width * ImgLevelSolved.Height / ImgLevelSolved.Width)/2,
                        ClientSize.Width, ClientSize.Width * ImgLevelSolved.Height / ImgLevelSolved.Width);
                else
                    e.Graphics.DrawImage(ImgLevelSolved,
                        ClientSize.Width/2 - ImgLevelSolved.Width/2,
                        ClientSize.Height/2 - ImgLevelSolved.Height/2);
            }
        }

        /// <summary>
        /// Initialises the move finder, which in playing mode determines what cells
        /// the player can move to from their current position, while in editing mode
        /// determines whether the level is properly enclosed by walls or not.
        /// </summary>
        private void reinitMoveFinder()
        {
            _moveFinder = State == MainAreaState.Editing
                ? new MoveFinderOutline(_level)
                : new MoveFinder(_level);
        }

        /// <summary>
        /// Invoked by a request to paint part or all of the Main Area. Since MainArea
        /// is a subclass of DoubleBufferedPanel, the level in its current state (as
        /// rendered in MainArea_PaintBuffer()) will already have been painted by its
        /// own Paint event handler. This function draws all the rest: the selected
        /// piece, move/push region, move/push path, and the Sokoban/piece end position.
        /// </summary>
        private void paint(object sender, PaintEventArgs e)
        {
            if (_state == MainAreaState.Null)
                return;

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;

            // Draw the region within which the Sokoban can move (if any)
            if (_moveFinder != null && (_state == MainAreaState.Move || _state == MainAreaState.Push) && _showAreaSokoban)
            {
                GraphicsPath Path = _renderer.ValidPath(_moveFinder);
                e.Graphics.FillPath(_moveBrush, Path);
                e.Graphics.DrawPath(_movePen, Path);
            }

            // Draw the region to which the selected piece can be pushed (if any)
            if (_pushFinder != null && _state == MainAreaState.Push && _showAreaPiece)
            {
                GraphicsPath Path = _renderer.ValidPath(_pushFinder);
                e.Graphics.FillPath(_pushBrush, Path);
                e.Graphics.DrawPath(_pushPen, Path);
            }

            // In editing mode, draw the outline that alerts the user to the fact that
            // the level is not properly enclosed by walls (if any)
            if (_moveFinder != null && _state == MainAreaState.Editing)
            {
                GraphicsPath Path = _renderer.ValidPath(_moveFinder);
                e.Graphics.FillPath(_editInvalidBrush, Path);
                e.Graphics.DrawPath(_editInvalidPen, Path);
            }

            // Keyboard selection
            if (_cursorPos != null)
            {
                GraphicsPath Path = _renderer.SelectorPath(_cursorPos.Value);
                e.Graphics.DrawPath(_cursorPen, Path);
            }

            // If we are not in push mode, we can stop here.
            if (_state != MainAreaState.Push)
                return;

            // Draw the selected piece
            _renderer.DrawCell(e.Graphics, _selectedPiece.Value, SokobanImage.PieceSelected);

            // If there is no push sequence to show, we can stop here.
            if (_pushSequence == null)
                return;

            if (ShowEndPos)
            {
                // Draw Sokoban end position
                GraphicsUtil.DrawImageAlpha(e.Graphics, Properties.Resources.Skin_Sokoban,
                    roundedRectangle(_renderer.CellRectForImage(_cellSeqSokoban)), 0.5f);

                // Draw piece end position
                if (_pushSequence.Length > 0)
                    GraphicsUtil.DrawImageAlpha(e.Graphics,
                        _level.Cell(_pushSequence[_pushSequence.Length-1]) == SokobanCell.Target
                        ? Properties.Resources.Skin_PieceTarget : Properties.Resources.Skin_Piece,
                        roundedRectangle(_renderer.CellRectForImage(_pushSequence[_pushSequence.Length-1])), 0.5f);
            }

            // Move path
            if (_moveDrawMode == PathDrawMode.Arrows)
                drawArrowSequence(e.Graphics, _level.SokobanPos, _moveSequence,
                    -_renderer.CellWidth/4, -_renderer.CellHeight/4);
            else if (_moveDrawMode == PathDrawMode.Line && _moveSequence.Length > 0)
                e.Graphics.DrawPath(_movePathPen, _renderer.LinePath(_level.SokobanPos, _moveSequence, 0.7f, 0.7f));
            else if (_moveDrawMode == PathDrawMode.Dots)
                for (int i = 0; i < _moveSequence.Length; i++)
                {
                    RectangleF CellRect = _renderer.CellRect(_moveSequence[i]);
                    e.Graphics.FillEllipse(_movePathBrush,
                        CellRect.Left + _renderer.CellWidth*2/5,
                        CellRect.Top + _renderer.CellHeight*2/5,
                        _renderer.CellWidth/5, _renderer.CellHeight/5);
                }

            // Push path
            if (_pushDrawMode == PathDrawMode.Arrows)
                drawArrowSequence(e.Graphics, _selectedPiece.Value, _pushSequence, 0, 0);
            else if (_pushDrawMode == PathDrawMode.Line && _pushSequence.Length > 0)
                e.Graphics.DrawPath(_pushPathPen, _renderer.LinePath(_selectedPiece.Value, _pushSequence, 0.7f, 0.7f));
            else if (_pushDrawMode == PathDrawMode.Dots)
                for (int i = 0; i < _pushSequence.Length; i++)
                {
                    RectangleF CellRect = _renderer.CellRect(_pushSequence[i]);
                    e.Graphics.FillEllipse(_pushPathBrush,
                        CellRect.Left + _renderer.CellWidth/3,
                        CellRect.Top + _renderer.CellHeight/3,
                        _renderer.CellWidth/3, _renderer.CellHeight/3);
                }
        }

        /// <summary>
        /// Used only by MainArea_Paint() to draw a move or push path as a sequence of
        /// arrows connecting cells.
        /// </summary>
        /// <param name="g">Graphics object to draw onto.</param>
        /// <param name="InitialPos">Initial position of the Sokoban (which is the first
        /// cell in the sequence of arrows, but not part of CellSequence).</param>
        /// <param name="CellSequence">The sequence of cells to render.</param>
        /// <param name="InflateX">An amount by which the destination rectangles for
        /// each arrow are inflated in X direction.</param>
        /// <param name="InflateY">An amount by which the destination rectangles for
        /// each arrow are inflated in Y direction.</param>
        private void drawArrowSequence(Graphics g, Point initialPos, Point[] sequence, float inflateX, float inflateY)
        {
            Point PrevCell = initialPos;
            for (int i = 0; i < sequence.Length; i++)
            {
                RectangleF CellRect = _renderer.CellRect(sequence[i]);
                Image Image;

                // Moving down
                if (PrevCell.X == sequence[i].X && PrevCell.Y == sequence[i].Y-1)
                {
                    CellRect.Offset(0, -_renderer.CellHeight/2);
                    Image = Properties.Resources.Skin_ArrowDown;
                }
                // Moving right
                else if (PrevCell.X == sequence[i].X-1 && PrevCell.Y == sequence[i].Y)
                {
                    CellRect.Offset(-_renderer.CellWidth/2, 0);
                    Image = Properties.Resources.Skin_ArrowRight;
                }
                // Moving left
                else if (PrevCell.X == sequence[i].X+1 && PrevCell.Y == sequence[i].Y)
                {
                    CellRect.Offset(_renderer.CellWidth/2, 0);
                    Image = Properties.Resources.Skin_ArrowLeft;
                }
                // Moving up
                else
                {
                    CellRect.Offset(0, _renderer.CellHeight/2);
                    Image = Properties.Resources.Skin_ArrowUp;
                }

                if (inflateX != 0 && inflateY != 0)
                    CellRect.Inflate(inflateX, inflateY);
                g.DrawImage(Image, CellRect);
                PrevCell = sequence[i];
            }
        }

        /// <summary>
        /// Takes a sequence of cells that describe Sokoban movements and (1) updates
        /// the current move path with it, and (2) calculates the push path from it and
        /// updates the current push path with that.
        /// </summary>
        /// <param name="NewMoveCellSequence">The sequence of cells making up the move
        /// path. If null, both the move path and push path are cleared.</param>
        private void updatePushPath(Point[] newMoveSequence)
        {
            if (newMoveSequence == null)
            {
                clearPushPath();
                return;
            }

            Point SokPos = _level.SokobanPos;
            Point PiecePos = _selectedPiece.Value;

            // Calculate the sequence of cells that makes up the push path
            List<Point> NewPushCellSequence = new List<Point>();
            foreach (Point Elem in newMoveSequence)
            {
                if (Elem.Equals(PiecePos))
                {
                    PiecePos.Offset(PiecePos.X-SokPos.X, PiecePos.Y-SokPos.Y);
                    NewPushCellSequence.Add(PiecePos);
                }
                SokPos = Elem;
            }

            _pushSequence = NewPushCellSequence.ToArray();
            _moveSequence = newMoveSequence;
            _cellSeqSokoban = SokPos;
            Invalidate();
        }

        /// <summary>
        /// Clears the push path (and hence, by implication, the move path as well).
        /// </summary>
        private void clearPushPath()
        {
            if (_pushSequence != null)
            {
                _pushSequence = null;
                _moveSequence = null;
                Invalidate();
            }
        }

        /// <summary>
        /// Takes a RectangleF, rounds all its values to integers, and returns the
        /// result.
        /// </summary>
        /// <param name="Src">Source RectangleF.</param>
        /// <returns>Rectangle with rounded values.</returns>
        private Rectangle roundedRectangle(RectangleF src)
        {
            return new Rectangle((int)src.X-2, (int)src.Y-2, (int)src.Width+4, (int)src.Height+4);
        }

        /// <summary>
        /// Determines whether the user has dragged the mouse from a cell to an adjacent
        /// cell, or pressed Space on a cell and then Enter on an adjacent cell. - If
        /// you just click a cell to push a piece to, PushFinder will find the shortest
        /// push-path regardless of where the Sokoban will end up. If you mouse-down
        /// *next* to the cell you want to push the piece to (or press Space on it), and
        /// then *drag* onto the cell before releasing (or press Enter on the
        /// destination cell), then we will try to move the piece in such a way that the
        /// Sokoban will end up on the original cell. GetOrigMouseDownDir() determines
        /// in which direction you dragged. (This value is passed on to
        /// PushFinder.Path().)
        /// </summary>
        /// <param name="Cell">The cell the mouse is pointing at now, or the cell the
        /// user pressed Enter on.</param>
        /// <param name="OrigDown">The cell the mouse was dragged from, or the cell the
        /// user pressed Space on.</param>
        /// <returns>0 if the answer is "no"; otherwise:
        /// 1 = user dragged downwards; 2 = user dragged to the right;
        /// 3 = user dragged to the left; 4 = user dragged upwards.</returns>
        private int origDownDir(Point cell, Point? origDown)
        {
            if (origDown == null)
                return 0;
            return (origDown.Value.X == cell.X && origDown.Value.Y == cell.Y-1) ? 1 :
                   (origDown.Value.X == cell.X-1 && origDown.Value.Y == cell.Y) ? 2 :
                   (origDown.Value.X == cell.X+1 && origDown.Value.Y == cell.Y) ? 3 :
                   (origDown.Value.X == cell.X && origDown.Value.Y == cell.Y+1) ? 4 : 0;
        }

        /// <summary>
        /// Invoked when the user presses the mouse button on the Main Area.
        /// Merely remembers the cell on which the mouse was pressed down.
        /// </summary>
        private void mouseDown(object sender, MouseEventArgs e)
        {
            _origKeyDown = null;
            _cursorPos = null;
            mouseMove(sender, e);
            if (_state == MainAreaState.Push)
                _origMouseDown = _renderer.CellFromPixel(e.Location);
            Invalidate();
            Focus();
        }

        /// <summary>
        /// Invoked when the user moves the mouse over the Main Area. If in Push mode,
        /// and the mouse is now in a different cell than before, the push path is
        /// recalculated and redrawn.
        /// </summary>
        private void mouseMove(object sender, MouseEventArgs e)
        {
            if (_state == MainAreaState.Push && _cursorPos == null)
            {
                Point Cell = _renderer.CellFromPixel(e.Location);
                if (_mouseOverCell == null || !Cell.Equals(_mouseOverCell.Value))
                {
                    _mouseOverCell = Cell;
                    updatePushPath(_pushFinder.Path(Cell, origDownDir(Cell, _origMouseDown)));
                }
            }
        }

        /// <summary>
        /// Invoked when the user releases the mouse button. All the click-related
        /// functionality of the Main Area happens in this method. In particular, in
        /// editing mode, the selected tool is applied; in Move or Push mode, a click on
        /// a piece selects/deselects it and switches into Push mode; in Push mode, a
        /// click on a valid destination cell executes the push.
        /// </summary>
        private void mouseUp(object sender, MouseEventArgs e)
        {
            if (_state == MainAreaState.Null)
                return;

            // Find out where we moused-up
            Point Cell = _renderer.CellFromPixel(e.Location);

            // Level editor functionality
            if (_state == MainAreaState.Editing)
            {
                processEditorClick(Cell);
            }

            // If the user clicked on a piece, initiate the PushFinder,
            // even if we are already in Push mode (because we want to
            // be able to select another piece).
            // Special case: if the user dragged from next to a piece onto
            // the piece, and this signifies a valid push according to the
            // current PushFinder, then we want to execute the push instead.
            else if (_level.IsPiece(Cell) &&
                     _state != MainAreaState.Solved &&
                     !(_pushFinder != null &&
                       origDownDir(Cell, _origMouseDown) != 0 &&
                       _pushFinder.GetDir(Cell, origDownDir(Cell, _origMouseDown))))
            {
                selectPiece(Cell);
            }

            // If the user clicked any other cell, try to push the piece there
            // (if this is not possible, ExecutePush() will meep)
            else if (_state == MainAreaState.Push)
            {
                executePush();
            }

            _origMouseDown = null;
        }

        /// <summary>
        /// Performs the action corresponding to the currently-selected tool in the
        /// level editor. This happens whenever the user clicks the mouse or presses
        /// Enter or Space.
        /// </summary>
        /// <param name="Cell">The cell that was "clicked".</param>
        private void processEditorClick(Point cell)
        {
            SokobanCell CellType = _level.Cell(cell);
            if (_tool == MainAreaTool.Wall)
            {
                if (_level.SokobanPos != cell)
                {
                    _level.SetCell(cell, CellType == SokobanCell.Wall ? SokobanCell.Blank : SokobanCell.Wall);
                    if (_soundEnabled)
                        _sndEditorClick.Play();
                    _modified = true;
                }
                else
                    if (_soundEnabled)
                        _sndMeep.Play();
            }
            else if (_tool == MainAreaTool.Piece)
            {
                if (_level.SokobanPos != cell && CellType != SokobanCell.Wall)
                {
                    _level.SetCell(cell,
                        CellType == SokobanCell.PieceOnTarget ? SokobanCell.Target :
                        CellType == SokobanCell.Blank         ? SokobanCell.Piece :
                        CellType == SokobanCell.Target        ? SokobanCell.PieceOnTarget :
                                                                SokobanCell.Blank);
                    if (_soundEnabled)
                        _sndPiecePlaced.Play();
                    _modified = true;
                }
                else
                    if (_soundEnabled)
                        _sndMeep.Play();
            }
            else if (_tool == MainAreaTool.Target)
            {
                if (CellType != SokobanCell.Wall)
                {
                    _level.SetCell(cell,
                        CellType == SokobanCell.PieceOnTarget ? SokobanCell.Piece :
                        CellType == SokobanCell.Blank         ? SokobanCell.Target :
                        CellType == SokobanCell.Target        ? SokobanCell.Blank :
                                                                SokobanCell.PieceOnTarget);
                    if (_soundEnabled)
                        _sndPiecePlaced.Play();
                    _modified = true;
                }
                else
                    if (_soundEnabled)
                        _sndMeep.Play();
            }
            else if (_tool == MainAreaTool.Sokoban)
            {
                if (CellType != SokobanCell.Wall &&
                    CellType != SokobanCell.Piece &&
                    CellType != SokobanCell.PieceOnTarget)
                {
                    Point PrevSokobanPos = _level.SokobanPos;
                    _level.SetSokobanPos(cell);
                    _renderer.RenderCell(Graphics.FromImage(Buffer), PrevSokobanPos);
                    if (_soundEnabled)
                        _sndPiecePlaced.Play();
                    _modified = true;
                }
                else
                    if (_soundEnabled)
                        _sndMeep.Play();
            }
            int PrevSizeX = _level.Width;
            int PrevSizeY = _level.Height;
            // Ensure a one-cell margin around the level. This way the level grows
            // automatically whenever the user draws walls or other things near the
            // edge.
            _level.EnsureSpace(1);
            reinitMoveFinder();
            if (_level.Width != PrevSizeX || _level.Height != PrevSizeY)
                Refresh();
            else
            {
                _renderer.RenderCell(Graphics.FromImage(Buffer), cell);
                Invalidate();
            }
        }

        /// <summary>
        /// Executes a push. This occurs if the user clicks the destination cell for the
        /// piece with the mouse, or presses Enter after selecting it with the keyboard.
        /// The current value of MoveCellSequence determines the actual action taken.
        /// </summary>
        private void executePush()
        {
            if (_moveSequence == null)
            {
                if (_soundEnabled)
                    _sndMeep.Play();
                return;
            }

            // Remove the move/push regions/paths by redrawing the plain level
            CreateGraphics().DrawImage(Buffer, 0, 0);

            // Prepare to move the Sokoban around visibly
            Graphics g = Graphics.FromImage(Buffer);
            Point OrigSokPos = _level.SokobanPos;
            Point? OrigPushPos = null, LastPushPos = null;
            bool EverPushed = false;

            // We need to remember how many moves and pushes were made in this move
            // because we need to save them in the Undo buffer
            int MovesMade = 0, PushesMade = 0;

            foreach (Point Move in _moveSequence)
            {
                System.Threading.Thread.Sleep(20);
                Point PrevSokPos = _level.SokobanPos;
                if (_level.IsPiece(Move))
                {
                    // need to push a piece
                    Point PushTo = new Point(2*Move.X-PrevSokPos.X, 2*Move.Y-PrevSokPos.Y);
                    _level.MovePiece(Move, PushTo);
                    _level.SetSokobanPos(Move);
                    _renderer.RenderCell(g, PushTo);
                    if (!EverPushed)
                    {
                        OrigPushPos = Move;
                        EverPushed = true;
                    }
                    LastPushPos = PushTo;
                    PushesMade++;
                }
                else
                    // just move Sokoban
                    _level.SetSokobanPos(Move);
                MovesMade++;
                _renderer.RenderCell(g, Move);
                _renderer.RenderCell(g, PrevSokPos);
                CreateGraphics().DrawImage(Buffer, 0, 0);
            }

            _moves += MovesMade;
            _pushes += PushesMade;

            // No piece is selected after the push
            _selectedPiece = null;

            // Level has now been "modified" (i.e. player made a move)
            _modified = true;

            // Did this push solve the level?
            if (_level.Solved)
            {
                _state = MainAreaState.Solved;
                _cursorPos = null;
                if (_soundEnabled)
                    _sndLevelSolved.Play();
                Refresh();
                if (LevelSolved != null)
                    LevelSolved(this, new EventArgs());
            }
            else
            {
                // Play a sound
                if (_soundEnabled)
                    _sndPiecePlaced.Play();

                // Add this action to the undo stack
                if (OrigPushPos == null)
                    _undo.Push(new UndoMoveItem(OrigSokPos, _level.SokobanPos, MovesMade));
                else
                    _undo.Push(new UndoPushItem(OrigSokPos, _level.SokobanPos, OrigPushPos.Value,
                        LastPushPos.Value, MovesMade, PushesMade));
                _redo = new Stack<UndoItem>();

                // Switch back into move mode
                _state = MainAreaState.Move;
                reinitMoveFinder();
                Invalidate();
            }
        }

        /// <summary>
        /// Discards the current level and replaces it with the specified level.
        /// </summary>
        /// <param name="Level">New level to display. The object will be cloned
        /// internally.</param>
        /// <param name="State">State to set the Main Area into. If this is set to
        /// MainAreaState.Push, nothing happens.</param>
        private void setLevel(SokobanLevel level, MainAreaState state)
        {
            if (state == MainAreaState.Push)
                return;

            _modified = false;
            _level = level.Clone();
            _level.EnsureSpace(1);
            _renderer = new Renderer(_level, ClientSize);
            _state = state;
            _selectedPiece = null;
            _cursorPos = null;
            _moves = 0;
            _pushes = 0;
            _undo = new Stack<UndoItem>();
            _redo = new Stack<UndoItem>();
            reinitMoveFinder();
            Refresh();
        }

        /// <summary>
        /// Discards the current level and allows the user to play the specified level.
        /// </summary>
        /// <param name="Level">The new level to play. The object will be cloned
        /// internally.</param>
        public void SetLevel(SokobanLevel level)
        {
            setLevel(level, MainAreaState.Move);
        }

        /// <summary>
        /// Discards the current level and allows the user to edit the specified level.
        /// </summary>
        /// <param name="Level">The new level to edit. The object will be cloned
        /// internally.</param>
        public void SetLevelEdit(SokobanLevel level)
        {
            setLevel(level, MainAreaState.Editing);
        }

        /// <summary>
        /// Performs the Undo operation, i.e. undoes the last move/push. If the level is
        /// at its initial state and there are no moves/pushes to undo, nothing happens.
        /// </summary>
        public void Undo()
        {
            if (_state == MainAreaState.Editing || _state == MainAreaState.Solved || 
                _state == MainAreaState.Null || _undo.Count == 0 || _level == null)
                return;

            UndoItem Item = _undo.Pop();
            _redo.Push(Item);

            if (Item is UndoMoveItem)
            {
                _level.SetSokobanPos((Item as UndoMoveItem).MovedFrom);
                _moves -= (Item as UndoMoveItem).Moves;
            }
            else if (Item is UndoPushItem)
            {
                UndoPushItem ItemPush = Item as UndoPushItem;
                _level.SetSokobanPos(ItemPush.MovedSokobanFrom);
                _level.MovePiece(ItemPush.MovedPieceTo, ItemPush.MovedPieceFrom);
                _moves -= ItemPush.Moves;
                _pushes -= ItemPush.Pushes;
            }
            else
                return;

            _selectedPiece = null;
            _state = MainAreaState.Move;
            if (_undo.Count == 0)
                _modified = false;
            reinitMoveFinder();
            Refresh();
        }

        /// <summary>
        /// Performs the Redo operation, i.e. redoes the last undone move/push. If there
        /// are no moves/pushes to redo, nothing happens.
        /// </summary>
        public void Redo()
        {
            if (_state == MainAreaState.Editing || _state == MainAreaState.Solved ||
                _state == MainAreaState.Null || _redo.Count == 0 || _level == null)
                return;

            UndoItem Item = _redo.Pop();
            _undo.Push(Item);

            if (Item is UndoMoveItem)
            {
                _level.SetSokobanPos((Item as UndoMoveItem).MovedTo);
                _moves += (Item as UndoMoveItem).Moves;
            }
            else if (Item is UndoPushItem)
            {
                UndoPushItem ItemPush = Item as UndoPushItem;
                _level.SetSokobanPos(ItemPush.MovedSokobanTo);
                _level.MovePiece(ItemPush.MovedPieceFrom, ItemPush.MovedPieceTo);
                _moves += ItemPush.Moves;
                _pushes += ItemPush.Pushes;
            }
            else
                return;

            _modified = true;
            _selectedPiece = null;
            _state = MainAreaState.Move;
            reinitMoveFinder();
            Refresh();
        }

        /// <summary>
        /// Clears the Main Area and switches into the Null state. The user is then
        /// unable to do anything in the Main Area.
        /// </summary>
        public void Clear()
        {
            _state = MainAreaState.Null;
            Refresh();
        }

        /// <summary>
        /// Deselects the currently selected piece.
        /// </summary>
        public void Deselect()
        {
            if (_state != MainAreaState.Push)
                return;
            _selectedPiece = null;
            _origKeyDown = null;
            _state = MainAreaState.Move;
            reinitMoveFinder();
            Invalidate();
        }

        /// <summary>
        /// Moves the selection rectangle that is controlled by the arrow keys.
        /// </summary>
        /// <param name="dx">Amount to move in X direction.</param>
        /// <param name="dy">Amount to move in Y direction.</param>
        /// <param name="FindPieceOrTarget">If true, will select a piece (if in
        /// move mode) or a target (if in push mode).</param>
        private void moveKeySelector(int direction, bool findPieceOrTarget)
        {
            if (_cursorPos == null)
                _cursorPos = _level.SokobanPos;

            if (findPieceOrTarget)
            {
                for (int i = 1; i < Math.Max(_level.Width, _level.Height); i++)
                {
                    for (int j = i/2; j >= 0; j--)
                    {
                        for (int k = -1; k <= 1; k += 2)
                        {
                            Point Examine =
                                direction == 1 ? new Point(_cursorPos.Value.X-k*j, _cursorPos.Value.Y-i+j) :
                                direction == 2 ? new Point(_cursorPos.Value.X-i+j, _cursorPos.Value.Y+k*j) :
                                direction == 3 ? new Point(_cursorPos.Value.X+i-j, _cursorPos.Value.Y-k*j) :
                                                 new Point(_cursorPos.Value.X+k*j, _cursorPos.Value.Y+i-j);
                            if ((_level.IsPiece(Examine) && (_state == MainAreaState.Move || _state == MainAreaState.Editing)) ||
                                (_level.Cell(Examine) == SokobanCell.Target && _state == MainAreaState.Push))
                            {
                                _cursorPos = Examine;
                                updatePushPathAfterKeyboardSelectionChange();
                                Invalidate();
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                int dx = direction == 2 ? -1 : direction == 3 ? 1 : 0;
                int dy = direction == 1 ? -1 : direction == 4 ? 1 : 0;
                _cursorPos = new Point((_cursorPos.Value.X + _level.Width + dx) % _level.Width,
                                      (_cursorPos.Value.Y + _level.Height + dy) % _level.Height);
            }

            updatePushPathAfterKeyboardSelectionChange();
            Invalidate();
        }

        /// <summary>
        /// Calls UpdatePushPath() or ClearPushPath() depending on whether the user has
        /// just moved the keyboard selection cursor onto a cell that represents a valid
        /// destination for a push.
        /// </summary>
        private void updatePushPathAfterKeyboardSelectionChange()
        {
            if (_state != MainAreaState.Push)
                return;

            if (_pushFinder != null && _selectedPiece != null && _cursorPos != null &&
                _selectedPiece.Value.Equals(_cursorPos.Value) &&
                origDownDir(_cursorPos.Value, _origKeyDown) != 0 &&
                _pushFinder.GetDir(_cursorPos.Value, origDownDir(_cursorPos.Value, _origKeyDown)))
            {
                updatePushPath(_pushFinder.Path(_cursorPos.Value, origDownDir(_cursorPos.Value, _origKeyDown)));
            }
            else if (_cursorPos != null && _level.IsPiece(_cursorPos.Value))
            {
                clearPushPath();
            }
            else if (_cursorPos != null && _pushFinder != null && _pushFinder.Get(_cursorPos.Value))
            {
                updatePushPath(_pushFinder.Path(_cursorPos.Value, origDownDir(_cursorPos.Value, _origKeyDown)));
            }
            else
            {
                clearPushPath();
            }
        }

        /// <summary>
        /// This function is overridden in order for persuade the arrow keys and Enter
        /// to fire the KeyDown event. (The SetStyle() call in the constructor is also
        /// necessary.)
        /// </summary>
        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Left || keyData == (Keys.Left | Keys.Shift))
                return true;
            if (keyData == Keys.Right || keyData == (Keys.Right | Keys.Shift))
                return true;
            if (keyData == Keys.Up || keyData == (Keys.Up | Keys.Shift))
                return true;
            if (keyData == Keys.Down || keyData == (Keys.Down | Keys.Shift))
                return true;
            if (keyData == Keys.Enter)
                return true;
            return base.IsInputKey(keyData);
        }

        /// <summary>
        /// Selects a piece. Occurs when the player clicks on a piece with the mouse, or
        /// selects one with the keyboard and presses Enter. If the given piece is already
        /// selected, deselects it.
        /// </summary>
        /// <param name="Cell">The cell containing the piece to be selected.</param>
        private void selectPiece(Point cell)
        {
            if (_state != MainAreaState.Move && _state != MainAreaState.Push) // should never happen
                return;
            if (_moveFinder == null) // should never happen
                return;

            // If we've simply clicked the same piece again, deselect it
            if (_selectedPiece != null && cell.Equals(_selectedPiece.Value))
                Deselect();

            // Otherwise, if the Sokoban can move anywhere adjacent to this piece,
            // then it is possible to select it and switch to Push mode
            else if (_level.IsPiece(cell) &&
                     (_moveFinder.Get(cell.X, cell.Y+1) ||
                      _moveFinder.Get(cell.X, cell.Y-1) ||
                      _moveFinder.Get(cell.X+1, cell.Y) ||
                      _moveFinder.Get(cell.X-1, cell.Y)))
            {
                _selectedPiece = cell;
                _mouseOverCell = null;
                _pushSequence = null;
                _moveSequence = null;
                _origKeyDown = null;
                _pushFinder = new PushFinder(_level, cell, _moveFinder);
                _state = MainAreaState.Push;
                Invalidate();
            }

            // If the user selected an invalid cell, meep
            else
                if (_soundEnabled)
                    _sndMeep.Play();
        }

        /// <summary>
        /// Invoked by a keypress. Interpreted keys are:
        /// - arrow keys: moves the keyboard selection.
        /// - Enter: selects a piece, or pushes it to the selected location.
        /// - Esc: deselects the currently-selected piece.
        /// - Space: initiates a special move, equivalent to the mouse dragging.
        /// </summary>
        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.Control || e.Alt)
                return;
            if (_state == MainAreaState.Solved || _state == MainAreaState.Null)
                return;

            // Escape: if a piece is selected, deselect it
            if (e.KeyCode == Keys.Escape && e.Modifiers == 0
                && _state == MainAreaState.Push && _selectedPiece != null)
                Deselect();

            // Escape: remove the keyboard cursor
            else if (e.KeyCode == Keys.Escape && e.Modifiers == 0)
            {
                _cursorPos = null;
                Invalidate();
            }

            // Arrow keys: move the cursor
            else if (e.KeyCode == Keys.Up)
                moveKeySelector(1, e.Shift);
            else if (e.KeyCode == Keys.Left)
                moveKeySelector(2, e.Shift);
            else if (e.KeyCode == Keys.Right)
                moveKeySelector(3, e.Shift);
            else if (e.KeyCode == Keys.Down)
                moveKeySelector(4, e.Shift);

            // Space or Enter while editing
            else if ((e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter) &&
                _state == MainAreaState.Editing && _cursorPos != null)
            {
                processEditorClick(_cursorPos.Value);
            }

            // Space: select the current cell as a potential end-position for the Sokoban
            else if (e.KeyCode == Keys.Space)
            {
                _origKeyDown = _cursorPos;
                updatePushPathAfterKeyboardSelectionChange();
            }

            // Enter: handle the special case in which the player "dragged" back onto
            // the same piece that is selected
            else if (e.KeyCode == Keys.Enter && _state == MainAreaState.Push &&
                     _cursorPos != null && _pushFinder != null && _selectedPiece != null &&
                     _selectedPiece.Value.Equals(_cursorPos.Value) &&
                     origDownDir(_cursorPos.Value, _origKeyDown) != 0 &&
                     _pushFinder.GetDir(_cursorPos.Value, origDownDir(_cursorPos.Value, _origKeyDown)))
                executePush();

            // Enter: if the cursor is on a valid piece, select it (if the piece is
            // already the currently-selected one, it is automatically deselected)
            else if (e.KeyCode == Keys.Enter && _cursorPos != null &&
                     _level.IsPiece(_cursorPos.Value))
                selectPiece(_cursorPos.Value);

            // Enter: if in push mode and valid destination is selected, execute the push
            // (if the selected destination is not valid, this will automatically meep)
            else if (e.KeyCode == Keys.Enter && _cursorPos != null && _state == MainAreaState.Push)
                executePush();
        }

        /// <summary>
        /// Determines (by asking the user if necessary) whether we are allowed to
        /// destroy the contents of the main area.
        /// </summary>
        /// <param name="Caption">Title bar caption to use in case any confirmation
        /// dialogs need to pop up.</param>
        public bool MayDestroy(string caption)
        {
            // If we're not playing or editing, we're definitely allowed.
            if (State == MainAreaState.Solved || State == MainAreaState.Null || !_modified)
                return true;

            // Ask the user the appropriate question.
            return State == MainAreaState.Editing
                ? DlgMessage.Show(Program.Translation.MainArea_Message_DiscardChanges,
                    caption, DlgType.Warning, Program.Translation.Dialogs_btnDiscard, Program.Translation.Dialogs_btnCancel) == 0
                : DlgMessage.Show(Program.Translation.MainArea_Message_GiveUp,
                    caption, DlgType.Warning, Program.Translation.Dialogs_btnGiveUp, Program.Translation.Dialogs_btnCancel) == 0;
        }
    }
}
