using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Media;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using RT.Util;
using RT.Util.Controls;
using RT.Util.Dialogs;

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
        /// <param name="From">The position from where the Sokoban moved.</param>
        /// <param name="To">The position the Sokoban moved to.</param>
        public UndoMoveItem(Point From, Point To, int MovesMade)
        {
            MovedFrom = From;
            MovedTo = To;
            Moves = MovesMade;
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
        /// <param name="SFrom">The position from where the Sokoban moved.</param>
        /// <param name="STo">The position the Sokoban moved to.</param>
        /// <param name="PFrom">The original position of the piece pushed.</param>
        /// <param name="PTo">The final position of the piece pushed.</param>
        public UndoPushItem(Point SFrom, Point STo, Point PFrom, Point PTo, int MovesMade, int PushesMade)
        {
            MovedSokobanFrom = SFrom;
            MovedSokobanTo = STo;
            MovedPieceFrom = PFrom;
            MovedPieceTo = PTo;
            Moves = MovesMade;
            Pushes = PushesMade;
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
        public MainAreaState State { get { return FState; } }

        /// <summary>
        /// If playing, true if the player made any moves. If editing, true
        /// if any changes have been made to the level.
        /// </summary>
        public bool Modified
        {
            get { return FModified; }
            set
            {
                if (value)
                    Ut.InternalError("Not allowed to set Modified to true");
                else
                    FModified = false;
            }
        }

        /// <summary>
        /// Gets or sets how the move path should be displayed while selecting a
        /// destination cell for a piece.
        /// </summary>
        public PathDrawMode MoveDrawMode
        {
            get { return FMoveDrawMode; }
            set { FMoveDrawMode = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets how the push path should be displayed while selecting a
        /// destination cell for a piece.
        /// </summary>
        public PathDrawMode PushDrawMode
        {
            get { return FPushDrawMode; }
            set { FPushDrawMode = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets whether the Sokoban and piece end-position should be displayed
        /// while selecting a destination cell for a piece.
        /// </summary>
        public bool ShowEndPos
        {
            get { return FShowEndPos; }
            set { FShowEndPos = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets whether the area the Sokoban can reach (the "move area")
        /// should be displayed or not.
        /// </summary>
        public bool ShowAreaSokoban
        {
            get { return FShowAreaSokoban; }
            set { FShowAreaSokoban = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets whether the area the selected piece can be pushed to
        /// (the "push area") should be displayed or not.
        /// </summary>
        public bool ShowAreaPiece
        {
            get { return FShowAreaPiece; }
            set { FShowAreaPiece = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets whether sound should be played or not.
        /// </summary>
        public bool SoundEnabled
        {
            get { return FSoundEnabled; }
            set { FSoundEnabled = value; }
        }

        /// <summary>
        /// The currently selected tool while editing a level.
        /// </summary>
        public MainAreaTool Tool
        {
            get { return FTool; }
            set { FTool = value; }
        }

        /// <summary>
        /// (read-only) The currently displayed level, or null if none.
        /// </summary>
        public SokobanLevel Level { get { return FState == MainAreaState.Null ? null : FLevel; } }

        /// <summary>
        /// (read-only) Returns the number of moves performed by the Sokoban so far.
        /// </summary>
        public int Moves { get { return FMoves; } }

        /// <summary>
        /// (read-only) Returns the number of pushes performed by the Sokoban so far.
        /// </summary>
        public int Pushes { get { return FPushes; } }

        /// <summary>
        /// Triggers when the player solves (completes) a level.
        /// </summary>
        public event EventHandler LevelSolved;

        /// <summary>
        /// The currently displayed level. Value may be invalid if State == Null.
        /// </summary>
        private SokobanLevel FLevel;

        /// <summary>
        /// The renderer used to display the current level. Will be re-created whenever
        /// the Main Area is resized.
        /// </summary>
        private Renderer Renderer;

        /// <summary>
        /// In playing mode, determines what cells the player can move to from their
        /// current position. In editing mode, determines whether the level is properly
        /// enclosed by walls or not.
        /// </summary>
        private MoveFinder MoveFinder;

        /// <summary>
        /// In playing mode, once a piece has been selected, determines where said piece
        /// can be pushed to (and what would be the shortest push-path).
        /// </summary>
        private PushFinder PushFinder;

        /// <summary>
        /// The state the Main Area is in.
        /// </summary>
        private MainAreaState FState;

        /// <summary>
        /// Indicates whether a move has been made or any changed to the level being edited.
        /// </summary>
        private bool FModified;

        /// <summary>
        /// The currently selected tool for editing.
        /// </summary>
        private MainAreaTool FTool;

        /// <summary>
        /// The currently selected option for displaying the move path.
        /// </summary>
        private PathDrawMode FMoveDrawMode;

        /// <summary>
        /// The currently selected option for displaying the push path.
        /// </summary>
        private PathDrawMode FPushDrawMode;

        /// <summary>
        /// Whether the Sokoban and piece end-position should be displayed while
        /// selecting a destination cell for a piece.
        /// </summary>
        private bool FShowEndPos;

        /// <summary>
        /// Whether the area reachable by the Sokoban (the "move area")
        /// should be displayed.
        /// </summary>
        private bool FShowAreaSokoban;

        /// <summary>
        /// Whether the area reachable by the selected piece (the "push area")
        /// should be displayed.
        /// </summary>
        private bool FShowAreaPiece;

        /// <summary>
        /// Whether sound is enabled or not.
        /// </summary>
        private bool FSoundEnabled;

        /// <summary>
        /// The brush used to fill the area that indicates where the Sokoban can move.
        /// </summary>
        private Brush MoveBrush = new SolidBrush(Color.FromArgb(32, 0, 255, 0));

        /// <summary>
        /// The pen used to outline the area that indicates where the Sokoban can move.
        /// </summary>
        private Pen MovePen = new Pen(Color.FromArgb(128, 0, 255, 0), 1.5f);

        /// <summary>
        /// The brush used to fill the area that indicates where the selected piece
        /// can be pushed.
        /// </summary>
        private Brush PushBrush = new SolidBrush(Color.FromArgb(32, 0, 0, 255));

        /// <summary>
        /// The pen used to outline the area that indicates where the selected piece
        /// can be pushed.
        /// </summary>
        private Pen PushPen = new Pen(Color.FromArgb(128, 0, 0, 255), 1.5f);

        /// <summary>
        /// The brush used to fill the area around the level that alerts the user to the
        /// fact that the level is not properly enclosed by a wall.
        /// </summary>
        private Brush EditInvalidBrush = new SolidBrush(Color.FromArgb(64, 255, 0, 0));

        /// <summary>
        /// The pen used to outline the area around the level that alerts the user to
        /// the fact that the level is not properly enclosed by a wall.
        /// </summary>
        private Pen EditInvalidPen = new Pen(Color.FromArgb(255, 255, 0, 0), 1.5f);

        /// <summary>
        /// The brush used to display the move path in Dots mode.
        /// </summary>
        private Brush MovePathBrush = new SolidBrush(Color.FromArgb(0, 128, 0));

        /// <summary>
        /// The pen used to display the move path in Line mode.
        /// </summary>
        private Pen MovePathPen = new Pen(Color.FromArgb(255, 0, 192, 0), 2f);

        /// <summary>
        /// The brush used to display the push path in Dots mode.
        /// </summary>
        private Brush PushPathBrush = new SolidBrush(Color.FromArgb(0, 0, 128));

        /// <summary>
        /// The pen used to display the push path in Line mode.
        /// </summary>
        private Pen PushPathPen = new Pen(Color.FromArgb(0, 0, 0x80), 3.5f);

        /// <summary>
        /// The pen used to display the cursor (for keyboard selection).
        /// </summary>
        private Pen CursorPen = new Pen(Color.FromArgb(0, 0, 0x80), 3.5f);

        /// <summary>
        /// Sound played when a level is solved.
        /// </summary>
        private SoundPlayerAsync SndLevelSolved = new SoundPlayerAsync(Properties.Resources.SndLevelDone);

        /// <summary>
        /// Sound played when an invalid destination cell for a piece is clicked in
        /// playing mode, or when an invalid operation is attempted in editing mode.
        /// </summary>
        private SoundPlayerAsync SndMeep = new SoundPlayerAsync(Properties.Resources.SndMeep);

        /// <summary>
        /// Sound played when a move is performed in playing mode, or when a piece,
        /// target, or the Sokoban starting position is placed in editing mode.
        /// </summary>
        private SoundPlayerAsync SndPiecePlaced = new SoundPlayerAsync(Properties.Resources.SndPiecePlaced);

        /// <summary>
        /// Sound played when a wall is added or removed in editing mode.
        /// </summary>
        private SoundPlayerAsync SndEditorClick = new SoundPlayerAsync(Properties.Resources.SndEditorClick);

        /// <summary>
        /// The currently selected piece for pushing (or null if none).
        /// </summary>
        private Point? SelectedPiece = null;

        /// <summary>
        /// The cell that is currently selected using the keyboard arrow keys.
        /// </summary>
        private Point? CursorPos = null;

        /// <summary>
        /// Remembers the cell on which the spacebar was pressed. This enables the user
        /// to determine which of the four side of the piece the Sokoban should end up
        /// at.
        /// </summary>
        private Point? OrigKeyDown = null;

        /// <summary>
        /// Remembers the cell on which the mouse button was pressed when the user
        /// starts dragging the mouse.
        /// </summary>
        private Point? OrigMouseDown = null;

        /// <summary>
        /// Remembers what cell the mouse is over. This way, things like the move path
        /// and push path only need to be redrawn if it actually changes.
        /// </summary>
        private Point? MouseOverCell = null;

        /// <summary>
        /// Remembers the entire history of actions by the player to allow for
        /// arbitrary undo.
        /// </summary>
        private Stack<UndoItem> FUndo = new Stack<UndoItem>();

        /// <summary>
        /// Remembers the history of undone actions to allow for redo.
        /// </summary>
        private Stack<UndoItem> FRedo = new Stack<UndoItem>();

        /// <summary>
        /// The currently-displayed push path, encoded as a sequence of cell co-ordinates.
        /// </summary>
        private Point[] PushCellSequence;

        /// <summary>
        /// The currently-displayed move path, encoded as a sequence of cell co-ordinates.
        /// </summary>
        private Point[] MoveCellSequence;

        /// <summary>
        /// The end-position of the Sokoban under the currently-displayed push path.
        /// </summary>
        private Point CellSeqSokoban;

        /// <summary>
        /// The number of moves performed by the Sokoban so far.
        /// </summary>
        private int FMoves = 0;

        /// <summary>
        /// The number of pushes performed by the Sokoban so far.
        /// </summary>
        private int FPushes = 0;

        /// <summary>
        /// Main constructor.
        /// </summary>
        public MainArea()
        {
            SetStyle(ControlStyles.Selectable, true);
            FMoveDrawMode = PathDrawMode.Line;
            FPushDrawMode = PathDrawMode.Arrows;
            FLevel = null;
            Renderer = null;
            FState = MainAreaState.Null;
            this.MouseDown += new MouseEventHandler(MainArea_MouseDown);
            this.MouseMove += new MouseEventHandler(MainArea_MouseMove);
            this.MouseUp += new MouseEventHandler(MainArea_MouseUp);
            this.Paint += new PaintEventHandler(MainArea_Paint);
            this.PaintBuffer += new PaintEventHandler(MainArea_PaintBuffer);
            this.KeyDown += new KeyEventHandler(MainArea_KeyDown);
        }

        /// <summary>
        /// Invoked whenever the paint buffer needs to be redrawn. Renders the current
        /// level in its current state and (if applicable) the "level solved" message,
        /// but not the selected piece, move/push region, move/push path, or the
        /// Sokoban/piece end position (those are all handled in MainArea_Paint()).
        /// </summary>
        private void MainArea_PaintBuffer(object sender, PaintEventArgs e)
        {
            if (FState == MainAreaState.Null)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Control)),
                    new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
                return;
            }

            Renderer = new Renderer(FLevel, ClientSize);
            Renderer.Render(e.Graphics);

            if (FState == MainAreaState.Solved)
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
        private void ReinitMoveFinder()
        {
            MoveFinder = State == MainAreaState.Editing
                ? new MoveFinderOutline(FLevel)
                : new MoveFinder(FLevel);
        }

        /// <summary>
        /// Invoked by a request to paint part or all of the Main Area. Since MainArea
        /// is a subclass of DoubleBufferedPanel, the level in its current state (as
        /// rendered in MainArea_PaintBuffer()) will already have been painted by its
        /// own Paint event handler. This function draws all the rest: the selected
        /// piece, move/push region, move/push path, and the Sokoban/piece end position.
        /// </summary>
        private void MainArea_Paint(object sender, PaintEventArgs e)
        {
            if (FState == MainAreaState.Null)
                return;

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;

            // Draw the region within which the Sokoban can move (if any)
            if (MoveFinder != null && (FState == MainAreaState.Move || FState == MainAreaState.Push) && FShowAreaSokoban)
            {
                GraphicsPath Path = Renderer.ValidPath(MoveFinder);
                e.Graphics.FillPath(MoveBrush, Path);
                e.Graphics.DrawPath(MovePen, Path);
            }

            // Draw the region to which the selected piece can be pushed (if any)
            if (PushFinder != null && FState == MainAreaState.Push && FShowAreaPiece)
            {
                GraphicsPath Path = Renderer.ValidPath(PushFinder);
                e.Graphics.FillPath(PushBrush, Path);
                e.Graphics.DrawPath(PushPen, Path);
            }

            // In editing mode, draw the outline that alerts the user to the fact that
            // the level is not properly enclosed by walls (if any)
            if (MoveFinder != null && FState == MainAreaState.Editing)
            {
                GraphicsPath Path = Renderer.ValidPath(MoveFinder);
                e.Graphics.FillPath(EditInvalidBrush, Path);
                e.Graphics.DrawPath(EditInvalidPen, Path);
            }

            // Keyboard selection
            if (CursorPos != null)
            {
                GraphicsPath Path = Renderer.SelectorPath(CursorPos.Value);
                e.Graphics.DrawPath(CursorPen, Path);
            }

            // If we are not in push mode, we can stop here.
            if (FState != MainAreaState.Push)
                return;

            // Draw the selected piece
            Renderer.DrawCell(e.Graphics, SelectedPiece.Value, SokobanImage.PieceSelected);

            // If there is no push sequence to show, we can stop here.
            if (PushCellSequence == null)
                return;

            if (ShowEndPos)
            {
                // Draw Sokoban end position
                GraphicsUtil.DrawImageAlpha(e.Graphics, Properties.Resources.Skin_Sokoban,
                    RoundedRectangle(Renderer.CellRectForImage(CellSeqSokoban)), 0.5f);

                // Draw piece end position
                if (PushCellSequence.Length > 0)
                    GraphicsUtil.DrawImageAlpha(e.Graphics,
                        FLevel.Cell(PushCellSequence[PushCellSequence.Length-1]) == SokobanCell.Target
                        ? Properties.Resources.Skin_PieceTarget : Properties.Resources.Skin_Piece,
                        RoundedRectangle(Renderer.CellRectForImage(PushCellSequence[PushCellSequence.Length-1])), 0.5f);
            }

            // Move path
            if (FMoveDrawMode == PathDrawMode.Arrows)
                DrawArrowSequence(e.Graphics, FLevel.SokobanPos, MoveCellSequence,
                    -Renderer.CellWidth/4, -Renderer.CellHeight/4);
            else if (FMoveDrawMode == PathDrawMode.Line && MoveCellSequence.Length > 0)
                e.Graphics.DrawPath(MovePathPen, Renderer.LinePath(FLevel.SokobanPos, MoveCellSequence, 0.7f, 0.7f));
            else if (FMoveDrawMode == PathDrawMode.Dots)
                for (int i = 0; i < MoveCellSequence.Length; i++)
                {
                    RectangleF CellRect = Renderer.CellRect(MoveCellSequence[i]);
                    e.Graphics.FillEllipse(MovePathBrush,
                        CellRect.Left + Renderer.CellWidth*2/5,
                        CellRect.Top + Renderer.CellHeight*2/5,
                        Renderer.CellWidth/5, Renderer.CellHeight/5);
                }

            // Push path
            if (FPushDrawMode == PathDrawMode.Arrows)
                DrawArrowSequence(e.Graphics, SelectedPiece.Value, PushCellSequence, 0, 0);
            else if (FPushDrawMode == PathDrawMode.Line && PushCellSequence.Length > 0)
                e.Graphics.DrawPath(PushPathPen, Renderer.LinePath(SelectedPiece.Value, PushCellSequence, 0.7f, 0.7f));
            else if (FPushDrawMode == PathDrawMode.Dots)
                for (int i = 0; i < PushCellSequence.Length; i++)
                {
                    RectangleF CellRect = Renderer.CellRect(PushCellSequence[i]);
                    e.Graphics.FillEllipse(PushPathBrush,
                        CellRect.Left + Renderer.CellWidth/3,
                        CellRect.Top + Renderer.CellHeight/3,
                        Renderer.CellWidth/3, Renderer.CellHeight/3);
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
        private void DrawArrowSequence(Graphics g, Point InitialPos, Point[] CellSequence,
            float InflateX, float InflateY)
        {
            Point PrevCell = InitialPos;
            for (int i = 0; i < CellSequence.Length; i++)
            {
                RectangleF CellRect = Renderer.CellRect(CellSequence[i]);
                Image Image;

                // Moving down
                if (PrevCell.X == CellSequence[i].X && PrevCell.Y == CellSequence[i].Y-1)
                {
                    CellRect.Offset(0, -Renderer.CellHeight/2);
                    Image = Properties.Resources.Skin_ArrowDown;
                }
                // Moving right
                else if (PrevCell.X == CellSequence[i].X-1 && PrevCell.Y == CellSequence[i].Y)
                {
                    CellRect.Offset(-Renderer.CellWidth/2, 0);
                    Image = Properties.Resources.Skin_ArrowRight;
                }
                // Moving left
                else if (PrevCell.X == CellSequence[i].X+1 && PrevCell.Y == CellSequence[i].Y)
                {
                    CellRect.Offset(Renderer.CellWidth/2, 0);
                    Image = Properties.Resources.Skin_ArrowLeft;
                }
                // Moving up
                else
                {
                    CellRect.Offset(0, Renderer.CellHeight/2);
                    Image = Properties.Resources.Skin_ArrowUp;
                }

                if (InflateX != 0 && InflateY != 0)
                    CellRect.Inflate(InflateX, InflateY);
                g.DrawImage(Image, CellRect);
                PrevCell = CellSequence[i];
            }
        }

        /// <summary>
        /// Takes a sequence of cells that describe Sokoban movements and (1) updates
        /// the current move path with it, and (2) calculates the push path from it and
        /// updates the current push path with that.
        /// </summary>
        /// <param name="NewMoveCellSequence">The sequence of cells making up the move
        /// path. If null, both the move path and push path are cleared.</param>
        private void UpdatePushPath(Point[] NewMoveCellSequence)
        {
            if (NewMoveCellSequence == null)
            {
                ClearPushPath();
                return;
            }

            Point SokPos = FLevel.SokobanPos;
            Point PiecePos = SelectedPiece.Value;

            // Calculate the sequence of cells that makes up the push path
            List<Point> NewPushCellSequence = new List<Point>();
            foreach (Point Elem in NewMoveCellSequence)
            {
                if (Elem.Equals(PiecePos))
                {
                    PiecePos.Offset(PiecePos.X-SokPos.X, PiecePos.Y-SokPos.Y);
                    NewPushCellSequence.Add(PiecePos);
                }
                SokPos = Elem;
            }

            PushCellSequence = NewPushCellSequence.ToArray();
            MoveCellSequence = NewMoveCellSequence;
            CellSeqSokoban = SokPos;
            Invalidate();
        }

        /// <summary>
        /// Clears the push path (and hence, by implication, the move path as well).
        /// </summary>
        private void ClearPushPath()
        {
            if (PushCellSequence != null)
            {
                PushCellSequence = null;
                MoveCellSequence = null;
                Invalidate();
            }
        }

        /// <summary>
        /// Takes a RectangleF, rounds all its values to integers, and returns the
        /// result.
        /// </summary>
        /// <param name="Src">Source RectangleF.</param>
        /// <returns>Rectangle with rounded values.</returns>
        private Rectangle RoundedRectangle(RectangleF Src)
        {
            return new Rectangle((int)Src.X-2, (int)Src.Y-2, (int)Src.Width+4, (int)Src.Height+4);
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
        private int OrigDownDir(Point Cell, Point? OrigDown)
        {
            if (OrigDown == null)
                return 0;
            return (OrigDown.Value.X == Cell.X && OrigDown.Value.Y == Cell.Y-1) ? 1 :
                   (OrigDown.Value.X == Cell.X-1 && OrigDown.Value.Y == Cell.Y) ? 2 :
                   (OrigDown.Value.X == Cell.X+1 && OrigDown.Value.Y == Cell.Y) ? 3 :
                   (OrigDown.Value.X == Cell.X && OrigDown.Value.Y == Cell.Y+1) ? 4 : 0;
        }

        /// <summary>
        /// Invoked when the user presses the mouse button on the Main Area.
        /// Merely remembers the cell on which the mouse was pressed down.
        /// </summary>
        private void MainArea_MouseDown(object sender, MouseEventArgs e)
        {
            OrigKeyDown = null;
            CursorPos = null;
            MainArea_MouseMove(sender, e);
            if (FState == MainAreaState.Push)
                OrigMouseDown = Renderer.CellFromPixel(e.Location);
            Invalidate();
            Focus();
        }

        /// <summary>
        /// Invoked when the user moves the mouse over the Main Area. If in Push mode,
        /// and the mouse is now in a different cell than before, the push path is
        /// recalculated and redrawn.
        /// </summary>
        private void MainArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (FState == MainAreaState.Push && CursorPos == null)
            {
                Point Cell = Renderer.CellFromPixel(e.Location);
                if (MouseOverCell == null || !Cell.Equals(MouseOverCell.Value))
                {
                    MouseOverCell = Cell;
                    UpdatePushPath(PushFinder.Path(Cell, OrigDownDir(Cell, OrigMouseDown)));
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
        private void MainArea_MouseUp(object sender, MouseEventArgs e)
        {
            if (FState == MainAreaState.Null)
                return;

            // Find out where we moused-up
            Point Cell = Renderer.CellFromPixel(e.Location);

            // Level editor functionality
            if (FState == MainAreaState.Editing)
            {
                ProcessEditorClick(Cell);
            }

            // If the user clicked on a piece, initiate the PushFinder,
            // even if we are already in Push mode (because we want to
            // be able to select another piece).
            // Special case: if the user dragged from next to a piece onto
            // the piece, and this signifies a valid push according to the
            // current PushFinder, then we want to execute the push instead.
            else if (FLevel.IsPiece(Cell) &&
                     FState != MainAreaState.Solved &&
                     !(PushFinder != null &&
                       OrigDownDir(Cell, OrigMouseDown) != 0 &&
                       PushFinder.GetDir(Cell, OrigDownDir(Cell, OrigMouseDown))))
            {
                SelectPiece(Cell);
            }

            // If the user clicked any other cell, try to push the piece there
            // (if this is not possible, ExecutePush() will meep)
            else if (FState == MainAreaState.Push)
            {
                ExecutePush();
            }

            OrigMouseDown = null;
        }

        /// <summary>
        /// Performs the action corresponding to the currently-selected tool in the
        /// level editor. This happens whenever the user clicks the mouse or presses
        /// Enter or Space.
        /// </summary>
        /// <param name="Cell">The cell that was "clicked".</param>
        private void ProcessEditorClick(Point Cell)
        {
            SokobanCell CellType = FLevel.Cell(Cell);
            if (FTool == MainAreaTool.Wall)
            {
                if (FLevel.SokobanPos != Cell)
                {
                    FLevel.SetCell(Cell, CellType == SokobanCell.Wall ? SokobanCell.Blank : SokobanCell.Wall);
                    if (FSoundEnabled)
                        SndEditorClick.Play();
                    FModified = true;
                }
                else
                    if (FSoundEnabled)
                        SndMeep.Play();
            }
            else if (FTool == MainAreaTool.Piece)
            {
                if (FLevel.SokobanPos != Cell && CellType != SokobanCell.Wall)
                {
                    FLevel.SetCell(Cell,
                        CellType == SokobanCell.PieceOnTarget ? SokobanCell.Target :
                        CellType == SokobanCell.Blank         ? SokobanCell.Piece :
                        CellType == SokobanCell.Target        ? SokobanCell.PieceOnTarget :
                                                                SokobanCell.Blank);
                    if (FSoundEnabled)
                        SndPiecePlaced.Play();
                    FModified = true;
                }
                else
                    if (FSoundEnabled)
                        SndMeep.Play();
            }
            else if (FTool == MainAreaTool.Target)
            {
                if (CellType != SokobanCell.Wall)
                {
                    FLevel.SetCell(Cell,
                        CellType == SokobanCell.PieceOnTarget ? SokobanCell.Piece :
                        CellType == SokobanCell.Blank         ? SokobanCell.Target :
                        CellType == SokobanCell.Target        ? SokobanCell.Blank :
                                                                SokobanCell.PieceOnTarget);
                    if (FSoundEnabled)
                        SndPiecePlaced.Play();
                    FModified = true;
                }
                else
                    if (FSoundEnabled)
                        SndMeep.Play();
            }
            else if (FTool == MainAreaTool.Sokoban)
            {
                if (CellType != SokobanCell.Wall &&
                    CellType != SokobanCell.Piece &&
                    CellType != SokobanCell.PieceOnTarget)
                {
                    Point PrevSokobanPos = FLevel.SokobanPos;
                    FLevel.SetSokobanPos(Cell);
                    Renderer.RenderCell(Graphics.FromImage(Buffer), PrevSokobanPos);
                    if (FSoundEnabled)
                        SndPiecePlaced.Play();
                    FModified = true;
                }
                else
                    if (FSoundEnabled)
                        SndMeep.Play();
            }
            int PrevSizeX = FLevel.Width;
            int PrevSizeY = FLevel.Height;
            // Ensure a one-cell margin around the level. This way the level grows
            // automatically whenever the user draws walls or other things near the
            // edge.
            FLevel.EnsureSpace(1);
            ReinitMoveFinder();
            if (FLevel.Width != PrevSizeX || FLevel.Height != PrevSizeY)
                Refresh();
            else
            {
                Renderer.RenderCell(Graphics.FromImage(Buffer), Cell);
                Invalidate();
            }
        }

        /// <summary>
        /// Executes a push. This occurs if the user clicks the destination cell for the
        /// piece with the mouse, or presses Enter after selecting it with the keyboard.
        /// The current value of MoveCellSequence determines the actual action taken.
        /// </summary>
        private void ExecutePush()
        {
            if (MoveCellSequence == null)
            {
                if (FSoundEnabled)
                    SndMeep.Play();
                return;
            }

            // Remove the move/push regions/paths by redrawing the plain level
            CreateGraphics().DrawImage(Buffer, 0, 0);

            // Prepare to move the Sokoban around visibly
            Graphics g = Graphics.FromImage(Buffer);
            Point OrigSokPos = FLevel.SokobanPos;
            Point? OrigPushPos = null, LastPushPos = null;
            bool EverPushed = false;

            // We need to remember how many moves and pushes were made in this move
            // because we need to save them in the Undo buffer
            int MovesMade = 0, PushesMade = 0;

            foreach (Point Move in MoveCellSequence)
            {
                System.Threading.Thread.Sleep(20);
                Point PrevSokPos = FLevel.SokobanPos;
                if (FLevel.IsPiece(Move))
                {
                    // need to push a piece
                    Point PushTo = new Point(2*Move.X-PrevSokPos.X, 2*Move.Y-PrevSokPos.Y);
                    FLevel.MovePiece(Move, PushTo);
                    FLevel.SetSokobanPos(Move);
                    Renderer.RenderCell(g, PushTo);
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
                    FLevel.SetSokobanPos(Move);
                MovesMade++;
                Renderer.RenderCell(g, Move);
                Renderer.RenderCell(g, PrevSokPos);
                CreateGraphics().DrawImage(Buffer, 0, 0);
            }

            FMoves += MovesMade;
            FPushes += PushesMade;

            // No piece is selected after the push
            SelectedPiece = null;

            // Level has now been "modified" (i.e. player made a move)
            FModified = true;

            // Did this push solve the level?
            if (FLevel.Solved)
            {
                FState = MainAreaState.Solved;
                CursorPos = null;
                if (FSoundEnabled)
                    SndLevelSolved.Play();
                Refresh();
                if (LevelSolved != null)
                    LevelSolved(this, new EventArgs());
            }
            else
            {
                // Play a sound
                if (FSoundEnabled)
                    SndPiecePlaced.Play();

                // Add this action to the undo stack
                if (OrigPushPos == null)
                    FUndo.Push(new UndoMoveItem(OrigSokPos, FLevel.SokobanPos, MovesMade));
                else
                    FUndo.Push(new UndoPushItem(OrigSokPos, FLevel.SokobanPos, OrigPushPos.Value,
                        LastPushPos.Value, MovesMade, PushesMade));
                FRedo = new Stack<UndoItem>();

                // Switch back into move mode
                FState = MainAreaState.Move;
                ReinitMoveFinder();
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
        private void SetLevelDo(SokobanLevel Level, MainAreaState State)
        {
            if (State == MainAreaState.Push)
                return;

            FModified = false;
            FLevel = Level.Clone();
            FLevel.EnsureSpace(1);
            Renderer = new Renderer(FLevel, ClientSize);
            FState = State;
            SelectedPiece = null;
            CursorPos = null;
            FMoves = 0;
            FPushes = 0;
            FUndo = new Stack<UndoItem>();
            FRedo = new Stack<UndoItem>();
            ReinitMoveFinder();
            Refresh();
        }

        /// <summary>
        /// Discards the current level and allows the user to play the specified level.
        /// </summary>
        /// <param name="Level">The new level to play. The object will be cloned
        /// internally.</param>
        public void SetLevel(SokobanLevel Level)
        {
            SetLevelDo(Level, MainAreaState.Move);
        }

        /// <summary>
        /// Discards the current level and allows the user to edit the specified level.
        /// </summary>
        /// <param name="Level">The new level to edit. The object will be cloned
        /// internally.</param>
        public void SetLevelEdit(SokobanLevel Level)
        {
            SetLevelDo(Level, MainAreaState.Editing);
        }

        /// <summary>
        /// Performs the Undo operation, i.e. undoes the last move/push. If the level is
        /// at its initial state and there are no moves/pushes to undo, nothing happens.
        /// </summary>
        public void Undo()
        {
            if (FState == MainAreaState.Editing || FState == MainAreaState.Solved ||
                FUndo.Count == 0 || FLevel == null)
                return;

            UndoItem Item = FUndo.Pop();
            FRedo.Push(Item);

            if (Item is UndoMoveItem)
            {
                FLevel.SetSokobanPos((Item as UndoMoveItem).MovedFrom);
                FMoves -= (Item as UndoMoveItem).Moves;
            }
            else if (Item is UndoPushItem)
            {
                UndoPushItem ItemPush = Item as UndoPushItem;
                FLevel.SetSokobanPos(ItemPush.MovedSokobanFrom);
                FLevel.MovePiece(ItemPush.MovedPieceTo, ItemPush.MovedPieceFrom);
                FMoves -= ItemPush.Moves;
                FPushes -= ItemPush.Pushes;
            }
            else
                return;

            SelectedPiece = null;
            FState = MainAreaState.Move;
            ReinitMoveFinder();
            Refresh();
        }

        /// <summary>
        /// Performs the Redo operation, i.e. redoes the last undone move/push. If there
        /// are no moves/pushes to redo, nothing happens.
        /// </summary>
        public void Redo()
        {
            if (FState == MainAreaState.Editing || FState == MainAreaState.Solved ||
                FRedo.Count == 0 || FLevel == null)
                return;

            UndoItem Item = FRedo.Pop();
            FUndo.Push(Item);

            if (Item is UndoMoveItem)
            {
                FLevel.SetSokobanPos((Item as UndoMoveItem).MovedTo);
                FMoves += (Item as UndoMoveItem).Moves;
            }
            else if (Item is UndoPushItem)
            {
                UndoPushItem ItemPush = Item as UndoPushItem;
                FLevel.SetSokobanPos(ItemPush.MovedSokobanTo);
                FLevel.MovePiece(ItemPush.MovedPieceFrom, ItemPush.MovedPieceTo);
                FMoves += ItemPush.Moves;
                FPushes += ItemPush.Pushes;
            }
            else
                return;

            SelectedPiece = null;
            FState = MainAreaState.Move;
            ReinitMoveFinder();
            Refresh();
        }

        /// <summary>
        /// Clears the Main Area and switches into the Null state. The user is then
        /// unable to do anything in the Main Area.
        /// </summary>
        public void Clear()
        {
            FState = MainAreaState.Null;
            Refresh();
        }

        /// <summary>
        /// Deselects the currently selected piece.
        /// </summary>
        public void Deselect()
        {
            if (FState != MainAreaState.Push)
                return;
            SelectedPiece = null;
            OrigKeyDown = null;
            FState = MainAreaState.Move;
            ReinitMoveFinder();
            Invalidate();
        }

        /// <summary>
        /// Moves the selection rectangle that is controlled by the arrow keys.
        /// </summary>
        /// <param name="dx">Amount to move in X direction.</param>
        /// <param name="dy">Amount to move in Y direction.</param>
        /// <param name="FindPieceOrTarget">If true, will select a piece (if in
        /// move mode) or a target (if in push mode).</param>
        private void MoveKeySelector(int Direction, bool FindPieceOrTarget)
        {
            if (CursorPos == null)
                CursorPos = FLevel.SokobanPos;

            if (FindPieceOrTarget)
            {
                for (int i = 1; i < Math.Max(FLevel.Width, FLevel.Height); i++)
                {
                    for (int j = i/2; j >= 0; j--)
                    {
                        for (int k = -1; k <= 1; k += 2)
                        {
                            Point Examine =
                                Direction == 1 ? new Point(CursorPos.Value.X-k*j, CursorPos.Value.Y-i+j) :
                                Direction == 2 ? new Point(CursorPos.Value.X-i+j, CursorPos.Value.Y+k*j) :
                                Direction == 3 ? new Point(CursorPos.Value.X+i-j, CursorPos.Value.Y-k*j) :
                                                 new Point(CursorPos.Value.X+k*j, CursorPos.Value.Y+i-j);
                            if ((FLevel.IsPiece(Examine) && (FState == MainAreaState.Move || FState == MainAreaState.Editing)) ||
                                (FLevel.Cell(Examine) == SokobanCell.Target && FState == MainAreaState.Push))
                            {
                                CursorPos = Examine;
                                UpdatePushPathAfterKeyboardSelectionChange();
                                Invalidate();
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                int dx = Direction == 2 ? -1 : Direction == 3 ? 1 : 0;
                int dy = Direction == 1 ? -1 : Direction == 4 ? 1 : 0;
                CursorPos = new Point((CursorPos.Value.X + FLevel.Width + dx) % FLevel.Width,
                                      (CursorPos.Value.Y + FLevel.Height + dy) % FLevel.Height);
            }

            UpdatePushPathAfterKeyboardSelectionChange();
            Invalidate();
        }

        /// <summary>
        /// Calls UpdatePushPath() or ClearPushPath() depending on whether the user has
        /// just moved the keyboard selection cursor onto a cell that represents a valid
        /// destination for a push.
        /// </summary>
        private void UpdatePushPathAfterKeyboardSelectionChange()
        {
            if (FState != MainAreaState.Push)
                return;

            if (PushFinder != null && SelectedPiece != null && CursorPos != null &&
                SelectedPiece.Value.Equals(CursorPos.Value) &&
                OrigDownDir(CursorPos.Value, OrigKeyDown) != 0 &&
                PushFinder.GetDir(CursorPos.Value, OrigDownDir(CursorPos.Value, OrigKeyDown)))
            {
                UpdatePushPath(PushFinder.Path(CursorPos.Value, OrigDownDir(CursorPos.Value, OrigKeyDown)));
            }
            else if (CursorPos != null && FLevel.IsPiece(CursorPos.Value))
            {
                ClearPushPath();
            }
            else if (CursorPos != null && PushFinder != null && PushFinder.Get(CursorPos.Value))
            {
                UpdatePushPath(PushFinder.Path(CursorPos.Value, OrigDownDir(CursorPos.Value, OrigKeyDown)));
            }
            else
            {
                ClearPushPath();
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
        private void SelectPiece(Point Cell)
        {
            if (FState != MainAreaState.Move && FState != MainAreaState.Push) // should never happen
                return;
            if (MoveFinder == null) // should never happen
                return;

            // If we've simply clicked the same piece again, deselect it
            if (SelectedPiece != null && Cell.Equals(SelectedPiece.Value))
                Deselect();

            // Otherwise, if the Sokoban can move anywhere adjacent to this piece,
            // then it is possible to select it and switch to Push mode
            else if (FLevel.IsPiece(Cell) &&
                     (MoveFinder.Get(Cell.X, Cell.Y+1) ||
                      MoveFinder.Get(Cell.X, Cell.Y-1) ||
                      MoveFinder.Get(Cell.X+1, Cell.Y) ||
                      MoveFinder.Get(Cell.X-1, Cell.Y)))
            {
                SelectedPiece = Cell;
                MouseOverCell = null;
                PushCellSequence = null;
                MoveCellSequence = null;
                OrigKeyDown = null;
                PushFinder = new PushFinder(FLevel, Cell, MoveFinder);
                FState = MainAreaState.Push;
                Invalidate();
            }

            // If the user selected an invalid cell, meep
            else
                if (FSoundEnabled)
                    SndMeep.Play();
        }

        /// <summary>
        /// Invoked by a keypress. Interpreted keys are:
        /// - arrow keys: moves the keyboard selection.
        /// - Enter: selects a piece, or pushes it to the selected location.
        /// - Esc: deselects the currently-selected piece.
        /// - Space: initiates a special move, equivalent to the mouse dragging.
        /// </summary>
        private void MainArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control || e.Alt)
                return;
            if (FState == MainAreaState.Solved || FState == MainAreaState.Null)
                return;

            // Escape: if a piece is selected, deselect it
            if (e.KeyCode == Keys.Escape && e.Modifiers == 0
                && FState == MainAreaState.Push && SelectedPiece != null)
                Deselect();

            // Escape: remove the keyboard cursor
            else if (e.KeyCode == Keys.Escape && e.Modifiers == 0)
            {
                CursorPos = null;
                Invalidate();
            }

            // Arrow keys: move the cursor
            else if (e.KeyCode == Keys.Up)
                MoveKeySelector(1, e.Shift);
            else if (e.KeyCode == Keys.Left)
                MoveKeySelector(2, e.Shift);
            else if (e.KeyCode == Keys.Right)
                MoveKeySelector(3, e.Shift);
            else if (e.KeyCode == Keys.Down)
                MoveKeySelector(4, e.Shift);

            // Space or Enter while editing
            else if ((e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter) &&
                FState == MainAreaState.Editing && CursorPos != null)
            {
                ProcessEditorClick(CursorPos.Value);
            }

            // Space: select the current cell as a potential end-position for the Sokoban
            else if (e.KeyCode == Keys.Space)
            {
                OrigKeyDown = CursorPos;
                UpdatePushPathAfterKeyboardSelectionChange();
            }

            // Enter: handle the special case in which the player "dragged" back onto
            // the same piece that is selected
            else if (e.KeyCode == Keys.Enter && FState == MainAreaState.Push &&
                     CursorPos != null && PushFinder != null && SelectedPiece != null &&
                     SelectedPiece.Value.Equals(CursorPos.Value) &&
                     OrigDownDir(CursorPos.Value, OrigKeyDown) != 0 &&
                     PushFinder.GetDir(CursorPos.Value, OrigDownDir(CursorPos.Value, OrigKeyDown)))
                ExecutePush();

            // Enter: if the cursor is on a valid piece, select it (if the piece is
            // already the currently-selected one, it is automatically deselected)
            else if (e.KeyCode == Keys.Enter && CursorPos != null &&
                     FLevel.IsPiece(CursorPos.Value))
                SelectPiece(CursorPos.Value);

            // Enter: if in push mode and valid destination is selected, execute the push
            // (if the selected destination is not valid, this will automatically meep)
            else if (e.KeyCode == Keys.Enter && CursorPos != null)
                ExecutePush();
        }

        /// <summary>
        /// Determines (by asking the user if necessary) whether we are allowed to
        /// destroy the contents of the main area.
        /// </summary>
        /// <param name="Caption">Title bar caption to use in case any confirmation
        /// dialogs need to pop up.</param>
        public bool MayDestroy(string Caption)
        {
            // If we're not playing or editing, we're definitely allowed.
            if (State == MainAreaState.Solved || State == MainAreaState.Null || !FModified)
                return true;

            // Ask the user the appropriate question.
            return State == MainAreaState.Editing
                ? DlgMessage.Show("Are you sure you wish to discard your changes to the level you're editing?",
                    Caption, DlgType.Warning, "Discard changes", "Cancel") == 0
                : DlgMessage.Show("Are you sure you wish to give up the current level?",
                    Caption, DlgType.Warning, "Give up", "Cancel") == 0;
        }
    }
}
