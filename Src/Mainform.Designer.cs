namespace ExpertSokoban
{
    partial class Mainform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainform));
            this.LevelListPanel = new System.Windows.Forms.Panel();
            this.LevelList = new ExpertSokoban.LevelListBox();
            this.LevelContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextHighscores = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextNewLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextNewComment = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextCut = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextCutItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextHide = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStrip = new System.Windows.Forms.ToolStrip();
            this.EditToolWall = new System.Windows.Forms.ToolStripButton();
            this.EditToolPiece = new System.Windows.Forms.ToolStripButton();
            this.EditToolTarget = new System.Windows.Forms.ToolStripButton();
            this.EditToolSokoban = new System.Windows.Forms.ToolStripButton();
            this.EditToolSep = new System.Windows.Forms.ToolStripSeparator();
            this.EditToolOK = new System.Windows.Forms.ToolStripButton();
            this.EditToolCancel = new System.Windows.Forms.ToolStripButton();
            this.LevelListToolStrip2 = new System.Windows.Forms.ToolStrip();
            this.LevelToolCut = new System.Windows.Forms.ToolStripButton();
            this.LevelToolCopy = new System.Windows.Forms.ToolStripButton();
            this.LevelToolPaste = new System.Windows.Forms.ToolStripButton();
            this.LevelToolDelete = new System.Windows.Forms.ToolStripButton();
            this.LevelToolNext = new System.Windows.Forms.ToolStripButton();
            this.LevelToolNextUnsolved = new System.Windows.Forms.ToolStripButton();
            this.LevelListToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.LevelToolNew = new System.Windows.Forms.ToolStripButton();
            this.LevelToolOpen = new System.Windows.Forms.ToolStripButton();
            this.LevelToolSave = new System.Windows.Forms.ToolStripButton();
            this.LevelToolSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.LevelToolNewLevel = new System.Windows.Forms.ToolStripButton();
            this.LevelToolEdit = new System.Windows.Forms.ToolStripButton();
            this.LevelToolComment = new System.Windows.Forms.ToolStripButton();
            this.LevelListClosePanel = new RT.Util.Controls.NiceClosePanel();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.LevelMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelNew = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelSave = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.LevelUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelRetry = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelHighscores = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.LevelPrevious = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelNext = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelPreviousUnsolved = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelNextUnsolved = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.LevelChangePlayer = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.LevelExit = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelUnusedHotkeys = new System.Windows.Forms.ToolStripMenuItem();
            this.UnusedCTRLShortcuts = new System.Windows.Forms.ToolStripMenuItem();
            this.EditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EditCreateLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.EditEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.EditAddComment = new System.Windows.Forms.ToolStripMenuItem();
            this.EditSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.EditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.EditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.EditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.EditDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.EditSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.EditFinish = new System.Windows.Forms.ToolStripMenuItem();
            this.EditCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.EditSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.EditWall = new ExpertSokoban.MenuRadioItemMainAreaTool();
            this.EditToolOptions = new ExpertSokoban.MenuRadioGroupMainAreaTool();
            this.EditPiece = new ExpertSokoban.MenuRadioItemMainAreaTool();
            this.EditTarget = new ExpertSokoban.MenuRadioItemMainAreaTool();
            this.EditSokoban = new ExpertSokoban.MenuRadioItemMainAreaTool();
            this.EditUnusedHotkeys = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLevelList = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStrip1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStrip2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewEditToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewMoveNo = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.MovePathOptions = new ExpertSokoban.MenuRadioGroupPathDrawMode();
            this.ViewMoveLine = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.ViewMoveDots = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.ViewMoveArrows = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.ViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewPushNo = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.PushPathOptions = new ExpertSokoban.MenuRadioGroupPathDrawMode();
            this.ViewPushLine = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.ViewPushDots = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.ViewPushArrows = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.ViewSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewEndPos = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewUnusedHotkeys = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpKeyboard = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.MainToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.StatusMoves = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusPushes = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusPieces = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusEdit = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusSolved = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusNull = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainArea = new ExpertSokoban.MainArea();
            this.LevelListSplitter = new System.Windows.Forms.Splitter();
            this.BugWorkaroundTimer = new System.Windows.Forms.Timer(this.components);
            this.UpdateControlsTimer = new System.Windows.Forms.Timer(this.components);
            this.LevelListPanel.SuspendLayout();
            this.LevelContextMenu.SuspendLayout();
            this.EditToolStrip.SuspendLayout();
            this.LevelListToolStrip2.SuspendLayout();
            this.LevelListToolStrip1.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.MainToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.MainToolStripContainer.ContentPanel.SuspendLayout();
            this.MainToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.MainToolStripContainer.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // LevelListPanel
            // 
            this.LevelListPanel.BackColor = System.Drawing.SystemColors.Control;
            this.LevelListPanel.Controls.Add(this.LevelList);
            this.LevelListPanel.Controls.Add(this.EditToolStrip);
            this.LevelListPanel.Controls.Add(this.LevelListToolStrip2);
            this.LevelListPanel.Controls.Add(this.LevelListToolStrip1);
            this.LevelListPanel.Controls.Add(this.LevelListClosePanel);
            this.LevelListPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.LevelListPanel.Location = new System.Drawing.Point(639, 0);
            this.LevelListPanel.Name = "LevelListPanel";
            this.LevelListPanel.Size = new System.Drawing.Size(171, 482);
            this.LevelListPanel.TabIndex = 6;
            this.LevelListPanel.Visible = false;
            this.LevelListPanel.Resize += new System.EventHandler(this.LevelListPanel_Resize);
            // 
            // LevelList
            // 
            this.LevelList.ContextMenuStrip = this.LevelContextMenu;
            this.LevelList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LevelList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.LevelList.IntegralHeight = false;
            this.LevelList.Location = new System.Drawing.Point(0, 85);
            this.LevelList.Modified = false;
            this.LevelList.Name = "LevelList";
            this.LevelList.ScrollAlwaysVisible = true;
            this.LevelList.Size = new System.Drawing.Size(171, 397);
            this.LevelList.TabIndex = 2;
            this.LevelList.LevelActivating += new RT.Util.ConfirmEventHandler(this.LevelList_LevelActivating);
            this.LevelList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LevelList_KeyDown);
            this.LevelList.LevelActivated += new System.EventHandler(this.LevelList_LevelActivated);
            // 
            // LevelContextMenu
            // 
            this.LevelContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextPlay,
            this.ContextEdit,
            this.ContextHighscores,
            this.ContextSep1,
            this.ContextNewLevel,
            this.ContextNewComment,
            this.ContextSep2,
            this.ContextCut,
            this.ContextCutItem,
            this.ContextPaste,
            this.ContextDelete,
            this.ContextSep3,
            this.ContextHide});
            this.LevelContextMenu.Name = "LevelContextMenu";
            this.LevelContextMenu.Size = new System.Drawing.Size(228, 264);
            // 
            // ContextPlay
            // 
            this.ContextPlay.Name = "ContextPlay";
            this.ContextPlay.ShortcutKeyDisplayString = "Enter";
            this.ContextPlay.Size = new System.Drawing.Size(227, 22);
            this.ContextPlay.Text = "Pl&ay this level";
            this.ContextPlay.Click += new System.EventHandler(this.ContextPlay_Click);
            // 
            // ContextEdit
            // 
            this.ContextEdit.Name = "ContextEdit";
            this.ContextEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.ContextEdit.Size = new System.Drawing.Size(227, 22);
            this.ContextEdit.Text = "&Edit this level";
            this.ContextEdit.Click += new System.EventHandler(this.EditEdit_Click);
            // 
            // ContextHighscores
            // 
            this.ContextHighscores.Name = "ContextHighscores";
            this.ContextHighscores.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.ContextHighscores.Size = new System.Drawing.Size(227, 22);
            this.ContextHighscores.Text = "Show &highscores";
            this.ContextHighscores.Click += new System.EventHandler(this.LevelHighscores_Click);
            // 
            // ContextSep1
            // 
            this.ContextSep1.Name = "ContextSep1";
            this.ContextSep1.Size = new System.Drawing.Size(224, 6);
            // 
            // ContextNewLevel
            // 
            this.ContextNewLevel.Name = "ContextNewLevel";
            this.ContextNewLevel.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.ContextNewLevel.Size = new System.Drawing.Size(227, 22);
            this.ContextNewLevel.Text = "C&reate a new level here";
            this.ContextNewLevel.Click += new System.EventHandler(this.EditCreateLevel_Click);
            // 
            // ContextNewComment
            // 
            this.ContextNewComment.Name = "ContextNewComment";
            this.ContextNewComment.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.ContextNewComment.Size = new System.Drawing.Size(227, 22);
            this.ContextNewComment.Text = "&Insert a comment here";
            this.ContextNewComment.Click += new System.EventHandler(this.EditAddComment_Click);
            // 
            // ContextSep2
            // 
            this.ContextSep2.Name = "ContextSep2";
            this.ContextSep2.Size = new System.Drawing.Size(224, 6);
            // 
            // ContextCut
            // 
            this.ContextCut.Name = "ContextCut";
            this.ContextCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.ContextCut.Size = new System.Drawing.Size(227, 22);
            this.ContextCut.Text = "C&ut";
            this.ContextCut.Click += new System.EventHandler(this.EditCut_Click);
            // 
            // ContextCutItem
            // 
            this.ContextCutItem.Name = "ContextCutItem";
            this.ContextCutItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.ContextCutItem.Size = new System.Drawing.Size(227, 22);
            this.ContextCutItem.Text = "&Copy";
            this.ContextCutItem.Click += new System.EventHandler(this.EditCopy_Click);
            // 
            // ContextPaste
            // 
            this.ContextPaste.Name = "ContextPaste";
            this.ContextPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.ContextPaste.Size = new System.Drawing.Size(227, 22);
            this.ContextPaste.Text = "&Paste";
            this.ContextPaste.Click += new System.EventHandler(this.EditPaste_Click);
            // 
            // ContextDelete
            // 
            this.ContextDelete.Name = "ContextDelete";
            this.ContextDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.ContextDelete.Size = new System.Drawing.Size(227, 22);
            this.ContextDelete.Text = "&Delete";
            this.ContextDelete.Click += new System.EventHandler(this.EditDelete_Click);
            // 
            // ContextSep3
            // 
            this.ContextSep3.Name = "ContextSep3";
            this.ContextSep3.Size = new System.Drawing.Size(224, 6);
            // 
            // ContextHide
            // 
            this.ContextHide.Name = "ContextHide";
            this.ContextHide.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.ContextHide.Size = new System.Drawing.Size(227, 22);
            this.ContextHide.Text = "Hide &level list";
            this.ContextHide.Click += new System.EventHandler(this.ViewLevelList_Click);
            // 
            // EditToolStrip
            // 
            this.EditToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditToolWall,
            this.EditToolPiece,
            this.EditToolTarget,
            this.EditToolSokoban,
            this.EditToolSep,
            this.EditToolOK,
            this.EditToolCancel});
            this.EditToolStrip.Location = new System.Drawing.Point(0, 60);
            this.EditToolStrip.Name = "EditToolStrip";
            this.EditToolStrip.Size = new System.Drawing.Size(171, 25);
            this.EditToolStrip.TabIndex = 3;
            this.EditToolStrip.Text = "Edit toolbar";
            // 
            // EditToolWall
            // 
            this.EditToolWall.Checked = true;
            this.EditToolWall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EditToolWall.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditToolWall.Image = ((System.Drawing.Image)(resources.GetObject("EditToolWall.Image")));
            this.EditToolWall.Name = "EditToolWall";
            this.EditToolWall.Size = new System.Drawing.Size(23, 22);
            this.EditToolWall.Text = "Wall tool";
            this.EditToolWall.Click += new System.EventHandler(this.EditTool_Click);
            // 
            // EditToolPiece
            // 
            this.EditToolPiece.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditToolPiece.Image = ((System.Drawing.Image)(resources.GetObject("EditToolPiece.Image")));
            this.EditToolPiece.Name = "EditToolPiece";
            this.EditToolPiece.Size = new System.Drawing.Size(23, 22);
            this.EditToolPiece.Text = "Piece tool";
            this.EditToolPiece.Click += new System.EventHandler(this.EditTool_Click);
            // 
            // EditToolTarget
            // 
            this.EditToolTarget.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditToolTarget.Image = ((System.Drawing.Image)(resources.GetObject("EditToolTarget.Image")));
            this.EditToolTarget.Name = "EditToolTarget";
            this.EditToolTarget.Size = new System.Drawing.Size(23, 22);
            this.EditToolTarget.Text = "Target tool";
            this.EditToolTarget.Click += new System.EventHandler(this.EditTool_Click);
            // 
            // EditToolSokoban
            // 
            this.EditToolSokoban.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditToolSokoban.Image = ((System.Drawing.Image)(resources.GetObject("EditToolSokoban.Image")));
            this.EditToolSokoban.Name = "EditToolSokoban";
            this.EditToolSokoban.Size = new System.Drawing.Size(23, 22);
            this.EditToolSokoban.Text = "Sokoban tool";
            this.EditToolSokoban.Click += new System.EventHandler(this.EditTool_Click);
            // 
            // EditToolSep
            // 
            this.EditToolSep.Name = "EditToolSep";
            this.EditToolSep.Size = new System.Drawing.Size(6, 25);
            // 
            // EditToolOK
            // 
            this.EditToolOK.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditToolOK.Image = ((System.Drawing.Image)(resources.GetObject("EditToolOK.Image")));
            this.EditToolOK.Name = "EditToolOK";
            this.EditToolOK.Size = new System.Drawing.Size(23, 22);
            this.EditToolOK.Text = "Finish editing";
            this.EditToolOK.Click += new System.EventHandler(this.EditFinish_Click);
            // 
            // EditToolCancel
            // 
            this.EditToolCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditToolCancel.Image = ((System.Drawing.Image)(resources.GetObject("EditToolCancel.Image")));
            this.EditToolCancel.Name = "EditToolCancel";
            this.EditToolCancel.Size = new System.Drawing.Size(23, 22);
            this.EditToolCancel.Text = "Cancel editing";
            this.EditToolCancel.Click += new System.EventHandler(this.EditCancel_Click);
            // 
            // LevelListToolStrip2
            // 
            this.LevelListToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LevelToolCut,
            this.LevelToolCopy,
            this.LevelToolPaste,
            this.LevelToolDelete,
            this.LevelToolNext,
            this.LevelToolNextUnsolved});
            this.LevelListToolStrip2.Location = new System.Drawing.Point(0, 35);
            this.LevelListToolStrip2.Name = "LevelListToolStrip2";
            this.LevelListToolStrip2.Size = new System.Drawing.Size(171, 25);
            this.LevelListToolStrip2.TabIndex = 4;
            this.LevelListToolStrip2.Text = "toolStrip1";
            // 
            // LevelToolCut
            // 
            this.LevelToolCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolCut.Image = ((System.Drawing.Image)(resources.GetObject("LevelToolCut.Image")));
            this.LevelToolCut.Name = "LevelToolCut";
            this.LevelToolCut.Size = new System.Drawing.Size(23, 22);
            this.LevelToolCut.Text = "Cut";
            this.LevelToolCut.Click += new System.EventHandler(this.EditCut_Click);
            // 
            // LevelToolCopy
            // 
            this.LevelToolCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolCopy.Image = ((System.Drawing.Image)(resources.GetObject("LevelToolCopy.Image")));
            this.LevelToolCopy.Name = "LevelToolCopy";
            this.LevelToolCopy.Size = new System.Drawing.Size(23, 22);
            this.LevelToolCopy.Text = "Copy";
            this.LevelToolCopy.Click += new System.EventHandler(this.EditCopy_Click);
            // 
            // LevelToolPaste
            // 
            this.LevelToolPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolPaste.Image = ((System.Drawing.Image)(resources.GetObject("LevelToolPaste.Image")));
            this.LevelToolPaste.Name = "LevelToolPaste";
            this.LevelToolPaste.Size = new System.Drawing.Size(23, 22);
            this.LevelToolPaste.Text = "Paste";
            this.LevelToolPaste.Click += new System.EventHandler(this.EditPaste_Click);
            // 
            // LevelToolDelete
            // 
            this.LevelToolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolDelete.Image = ((System.Drawing.Image)(resources.GetObject("LevelToolDelete.Image")));
            this.LevelToolDelete.Name = "LevelToolDelete";
            this.LevelToolDelete.Size = new System.Drawing.Size(23, 22);
            this.LevelToolDelete.Text = "Delete selected item";
            this.LevelToolDelete.Click += new System.EventHandler(this.EditDelete_Click);
            // 
            // LevelToolNext
            // 
            this.LevelToolNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolNext.Image = ((System.Drawing.Image)(resources.GetObject("LevelToolNext.Image")));
            this.LevelToolNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolNext.Name = "LevelToolNext";
            this.LevelToolNext.Size = new System.Drawing.Size(23, 22);
            this.LevelToolNext.Text = "Next level";
            this.LevelToolNext.Click += new System.EventHandler(this.LevelNext_Click);
            // 
            // LevelToolNextUnsolved
            // 
            this.LevelToolNextUnsolved.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolNextUnsolved.Image = ((System.Drawing.Image)(resources.GetObject("LevelToolNextUnsolved.Image")));
            this.LevelToolNextUnsolved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolNextUnsolved.Name = "LevelToolNextUnsolved";
            this.LevelToolNextUnsolved.Size = new System.Drawing.Size(23, 22);
            this.LevelToolNextUnsolved.Text = "Next unsolved level";
            this.LevelToolNextUnsolved.Click += new System.EventHandler(this.LevelNextUnsolved_Click);
            // 
            // LevelListToolStrip1
            // 
            this.LevelListToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LevelToolNew,
            this.LevelToolOpen,
            this.LevelToolSave,
            this.LevelToolSep1,
            this.LevelToolNewLevel,
            this.LevelToolEdit,
            this.LevelToolComment});
            this.LevelListToolStrip1.Location = new System.Drawing.Point(0, 10);
            this.LevelListToolStrip1.Name = "LevelListToolStrip1";
            this.LevelListToolStrip1.Size = new System.Drawing.Size(171, 25);
            this.LevelListToolStrip1.TabIndex = 0;
            this.LevelListToolStrip1.Text = "Levels toolbar";
            // 
            // LevelToolNew
            // 
            this.LevelToolNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolNew.Image = ((System.Drawing.Image)(resources.GetObject("LevelToolNew.Image")));
            this.LevelToolNew.Name = "LevelToolNew";
            this.LevelToolNew.Size = new System.Drawing.Size(23, 22);
            this.LevelToolNew.Text = "New level file";
            this.LevelToolNew.Click += new System.EventHandler(this.LevelNew_Click);
            // 
            // LevelToolOpen
            // 
            this.LevelToolOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolOpen.Image = ((System.Drawing.Image)(resources.GetObject("LevelToolOpen.Image")));
            this.LevelToolOpen.Name = "LevelToolOpen";
            this.LevelToolOpen.Size = new System.Drawing.Size(23, 22);
            this.LevelToolOpen.Text = "Open level file";
            this.LevelToolOpen.Click += new System.EventHandler(this.LevelOpen_Click);
            // 
            // LevelToolSave
            // 
            this.LevelToolSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolSave.Image = ((System.Drawing.Image)(resources.GetObject("LevelToolSave.Image")));
            this.LevelToolSave.Name = "LevelToolSave";
            this.LevelToolSave.Size = new System.Drawing.Size(23, 22);
            this.LevelToolSave.Text = "Save level file";
            this.LevelToolSave.Click += new System.EventHandler(this.LevelSave_Click);
            // 
            // LevelToolSep1
            // 
            this.LevelToolSep1.Name = "LevelToolSep1";
            this.LevelToolSep1.Size = new System.Drawing.Size(6, 25);
            // 
            // LevelToolNewLevel
            // 
            this.LevelToolNewLevel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolNewLevel.Image = ((System.Drawing.Image)(resources.GetObject("LevelToolNewLevel.Image")));
            this.LevelToolNewLevel.Name = "LevelToolNewLevel";
            this.LevelToolNewLevel.Size = new System.Drawing.Size(23, 22);
            this.LevelToolNewLevel.Text = "Create new level";
            this.LevelToolNewLevel.Click += new System.EventHandler(this.EditCreateLevel_Click);
            // 
            // LevelToolEdit
            // 
            this.LevelToolEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolEdit.Image = ((System.Drawing.Image)(resources.GetObject("LevelToolEdit.Image")));
            this.LevelToolEdit.Name = "LevelToolEdit";
            this.LevelToolEdit.Size = new System.Drawing.Size(23, 22);
            this.LevelToolEdit.Text = "Edit level";
            this.LevelToolEdit.Click += new System.EventHandler(this.EditEdit_Click);
            // 
            // LevelToolComment
            // 
            this.LevelToolComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolComment.Image = ((System.Drawing.Image)(resources.GetObject("LevelToolComment.Image")));
            this.LevelToolComment.Name = "LevelToolComment";
            this.LevelToolComment.Size = new System.Drawing.Size(23, 22);
            this.LevelToolComment.Text = "Add a comment";
            this.LevelToolComment.Click += new System.EventHandler(this.EditAddComment_Click);
            // 
            // LevelListClosePanel
            // 
            this.LevelListClosePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.LevelListClosePanel.Location = new System.Drawing.Point(0, 0);
            this.LevelListClosePanel.Name = "LevelListClosePanel";
            this.LevelListClosePanel.Size = new System.Drawing.Size(171, 10);
            this.LevelListClosePanel.TabIndex = 5;
            this.LevelListClosePanel.CloseClicked += new System.EventHandler(this.ViewLevelList_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LevelMenu,
            this.EditMenu,
            this.ViewMenu,
            this.HelpMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(639, 24);
            this.MainMenu.TabIndex = 8;
            this.MainMenu.Text = "menuStrip1";
            // 
            // LevelMenu
            // 
            this.LevelMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LevelNew,
            this.LevelOpen,
            this.LevelSave,
            this.LevelSaveAs,
            this.LevelSep1,
            this.LevelUndo,
            this.LevelRedo,
            this.LevelRetry,
            this.LevelHighscores,
            this.LevelSep2,
            this.LevelPrevious,
            this.LevelNext,
            this.LevelPreviousUnsolved,
            this.LevelNextUnsolved,
            this.LevelSep3,
            this.LevelChangePlayer,
            this.LevelSep4,
            this.LevelExit,
            this.LevelUnusedHotkeys,
            this.UnusedCTRLShortcuts});
            this.LevelMenu.Name = "LevelMenu";
            this.LevelMenu.Size = new System.Drawing.Size(44, 20);
            this.LevelMenu.Text = "&Level";
            // 
            // LevelNew
            // 
            this.LevelNew.Name = "LevelNew";
            this.LevelNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.LevelNew.Size = new System.Drawing.Size(225, 22);
            this.LevelNew.Text = "&New level file";
            this.LevelNew.Click += new System.EventHandler(this.LevelNew_Click);
            // 
            // LevelOpen
            // 
            this.LevelOpen.Name = "LevelOpen";
            this.LevelOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.LevelOpen.Size = new System.Drawing.Size(225, 22);
            this.LevelOpen.Text = "&Open level file...";
            this.LevelOpen.Click += new System.EventHandler(this.LevelOpen_Click);
            // 
            // LevelSave
            // 
            this.LevelSave.Name = "LevelSave";
            this.LevelSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.LevelSave.Size = new System.Drawing.Size(225, 22);
            this.LevelSave.Text = "&Save level file";
            this.LevelSave.Click += new System.EventHandler(this.LevelSave_Click);
            // 
            // LevelSaveAs
            // 
            this.LevelSaveAs.Name = "LevelSaveAs";
            this.LevelSaveAs.Size = new System.Drawing.Size(225, 22);
            this.LevelSaveAs.Text = "Save level file &as...";
            this.LevelSaveAs.Click += new System.EventHandler(this.LevelSaveAs_Click);
            // 
            // LevelSep1
            // 
            this.LevelSep1.Name = "LevelSep1";
            this.LevelSep1.Size = new System.Drawing.Size(222, 6);
            // 
            // LevelUndo
            // 
            this.LevelUndo.Name = "LevelUndo";
            this.LevelUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.LevelUndo.Size = new System.Drawing.Size(225, 22);
            this.LevelUndo.Text = "&Undo move";
            this.LevelUndo.Click += new System.EventHandler(this.LevelUndo_Click);
            // 
            // LevelRedo
            // 
            this.LevelRedo.Name = "LevelRedo";
            this.LevelRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.LevelRedo.Size = new System.Drawing.Size(225, 22);
            this.LevelRedo.Text = "Redo &move";
            this.LevelRedo.Click += new System.EventHandler(this.LevelRedo_Click);
            // 
            // LevelRetry
            // 
            this.LevelRetry.Name = "LevelRetry";
            this.LevelRetry.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.LevelRetry.Size = new System.Drawing.Size(225, 22);
            this.LevelRetry.Text = "&Retry level";
            this.LevelRetry.Click += new System.EventHandler(this.LevelRetry_Click);
            // 
            // LevelHighscores
            // 
            this.LevelHighscores.Name = "LevelHighscores";
            this.LevelHighscores.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.LevelHighscores.Size = new System.Drawing.Size(225, 22);
            this.LevelHighscores.Text = "Show &highscores";
            this.LevelHighscores.Click += new System.EventHandler(this.LevelHighscores_Click);
            // 
            // LevelSep2
            // 
            this.LevelSep2.Name = "LevelSep2";
            this.LevelSep2.Size = new System.Drawing.Size(222, 6);
            // 
            // LevelPrevious
            // 
            this.LevelPrevious.Name = "LevelPrevious";
            this.LevelPrevious.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.LevelPrevious.Size = new System.Drawing.Size(225, 22);
            this.LevelPrevious.Text = "&Previous level";
            this.LevelPrevious.Click += new System.EventHandler(this.LevelPrevious_Click);
            // 
            // LevelNext
            // 
            this.LevelNext.Name = "LevelNext";
            this.LevelNext.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.LevelNext.Size = new System.Drawing.Size(225, 22);
            this.LevelNext.Text = "N&ext level";
            this.LevelNext.Click += new System.EventHandler(this.LevelNext_Click);
            // 
            // LevelPreviousUnsolved
            // 
            this.LevelPreviousUnsolved.Name = "LevelPreviousUnsolved";
            this.LevelPreviousUnsolved.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.LevelPreviousUnsolved.Size = new System.Drawing.Size(225, 22);
            this.LevelPreviousUnsolved.Text = "Pre&vious unsolved level";
            this.LevelPreviousUnsolved.Click += new System.EventHandler(this.LevelPreviousUnsolved_Click);
            // 
            // LevelNextUnsolved
            // 
            this.LevelNextUnsolved.Name = "LevelNextUnsolved";
            this.LevelNextUnsolved.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.LevelNextUnsolved.Size = new System.Drawing.Size(225, 22);
            this.LevelNextUnsolved.Text = "Next unsolve&d level";
            this.LevelNextUnsolved.Click += new System.EventHandler(this.LevelNextUnsolved_Click);
            // 
            // LevelSep3
            // 
            this.LevelSep3.Name = "LevelSep3";
            this.LevelSep3.Size = new System.Drawing.Size(222, 6);
            // 
            // LevelChangePlayer
            // 
            this.LevelChangePlayer.Name = "LevelChangePlayer";
            this.LevelChangePlayer.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.LevelChangePlayer.Size = new System.Drawing.Size(225, 22);
            this.LevelChangePlayer.Text = "&Change player name...";
            this.LevelChangePlayer.Click += new System.EventHandler(this.LevelChangePlayer_Click);
            // 
            // LevelSep4
            // 
            this.LevelSep4.Name = "LevelSep4";
            this.LevelSep4.Size = new System.Drawing.Size(222, 6);
            // 
            // LevelExit
            // 
            this.LevelExit.Name = "LevelExit";
            this.LevelExit.Size = new System.Drawing.Size(225, 22);
            this.LevelExit.Text = "E&xit";
            this.LevelExit.Click += new System.EventHandler(this.LevelExit_Click);
            // 
            // LevelUnusedHotkeys
            // 
            this.LevelUnusedHotkeys.Enabled = false;
            this.LevelUnusedHotkeys.Name = "LevelUnusedHotkeys";
            this.LevelUnusedHotkeys.Size = new System.Drawing.Size(225, 22);
            this.LevelUnusedHotkeys.Text = "Unused hotkeys: bfgijklqtwyz";
            this.LevelUnusedHotkeys.Visible = false;
            // 
            // UnusedCTRLShortcuts
            // 
            this.UnusedCTRLShortcuts.Enabled = false;
            this.UnusedCTRLShortcuts.Name = "UnusedCTRLShortcuts";
            this.UnusedCTRLShortcuts.Size = new System.Drawing.Size(225, 22);
            this.UnusedCTRLShortcuts.Text = "Unused CTRL shortcuts: agiq";
            this.UnusedCTRLShortcuts.Visible = false;
            // 
            // EditMenu
            // 
            this.EditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditCreateLevel,
            this.EditEdit,
            this.EditAddComment,
            this.EditSep1,
            this.EditCut,
            this.EditCopy,
            this.EditPaste,
            this.EditDelete,
            this.EditSep2,
            this.EditFinish,
            this.EditCancel,
            this.EditSep3,
            this.EditWall,
            this.EditPiece,
            this.EditTarget,
            this.EditSokoban,
            this.EditUnusedHotkeys});
            this.EditMenu.Name = "EditMenu";
            this.EditMenu.Size = new System.Drawing.Size(37, 20);
            this.EditMenu.Text = "&Edit";
            // 
            // EditCreateLevel
            // 
            this.EditCreateLevel.Name = "EditCreateLevel";
            this.EditCreateLevel.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.EditCreateLevel.Size = new System.Drawing.Size(225, 22);
            this.EditCreateLevel.Text = "Create &new level";
            this.EditCreateLevel.Click += new System.EventHandler(this.EditCreateLevel_Click);
            // 
            // EditEdit
            // 
            this.EditEdit.Name = "EditEdit";
            this.EditEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.EditEdit.Size = new System.Drawing.Size(225, 22);
            this.EditEdit.Text = "&Edit level";
            this.EditEdit.Click += new System.EventHandler(this.EditEdit_Click);
            // 
            // EditAddComment
            // 
            this.EditAddComment.Name = "EditAddComment";
            this.EditAddComment.ShortcutKeyDisplayString = "";
            this.EditAddComment.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.EditAddComment.Size = new System.Drawing.Size(225, 22);
            this.EditAddComment.Text = "Add a co&mment...";
            this.EditAddComment.Click += new System.EventHandler(this.EditAddComment_Click);
            // 
            // EditSep1
            // 
            this.EditSep1.Name = "EditSep1";
            this.EditSep1.Size = new System.Drawing.Size(222, 6);
            // 
            // EditCut
            // 
            this.EditCut.Name = "EditCut";
            this.EditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.EditCut.Size = new System.Drawing.Size(225, 22);
            this.EditCut.Text = "C&ut";
            this.EditCut.Click += new System.EventHandler(this.EditCut_Click);
            // 
            // EditCopy
            // 
            this.EditCopy.Name = "EditCopy";
            this.EditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.EditCopy.Size = new System.Drawing.Size(225, 22);
            this.EditCopy.Text = "&Copy";
            this.EditCopy.Click += new System.EventHandler(this.EditCopy_Click);
            // 
            // EditPaste
            // 
            this.EditPaste.Name = "EditPaste";
            this.EditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.EditPaste.Size = new System.Drawing.Size(225, 22);
            this.EditPaste.Text = "&Paste";
            this.EditPaste.Click += new System.EventHandler(this.EditPaste_Click);
            // 
            // EditDelete
            // 
            this.EditDelete.Name = "EditDelete";
            this.EditDelete.ShortcutKeyDisplayString = "";
            this.EditDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.EditDelete.Size = new System.Drawing.Size(225, 22);
            this.EditDelete.Text = "&Delete";
            this.EditDelete.Click += new System.EventHandler(this.EditDelete_Click);
            // 
            // EditSep2
            // 
            this.EditSep2.Name = "EditSep2";
            this.EditSep2.Size = new System.Drawing.Size(222, 6);
            // 
            // EditFinish
            // 
            this.EditFinish.Name = "EditFinish";
            this.EditFinish.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)));
            this.EditFinish.Size = new System.Drawing.Size(225, 22);
            this.EditFinish.Text = "&Finish editing";
            this.EditFinish.Click += new System.EventHandler(this.EditFinish_Click);
            // 
            // EditCancel
            // 
            this.EditCancel.Name = "EditCancel";
            this.EditCancel.ShortcutKeyDisplayString = "";
            this.EditCancel.Size = new System.Drawing.Size(225, 22);
            this.EditCancel.Text = "C&ancel editing";
            this.EditCancel.Click += new System.EventHandler(this.EditCancel_Click);
            // 
            // EditSep3
            // 
            this.EditSep3.Name = "EditSep3";
            this.EditSep3.Size = new System.Drawing.Size(222, 6);
            // 
            // EditWall
            // 
            this.EditWall.Name = "EditWall";
            this.EditWall.ParentGroup = this.EditToolOptions;
            this.EditWall.ShortcutKeyDisplayString = "";
            this.EditWall.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.EditWall.Size = new System.Drawing.Size(225, 22);
            this.EditWall.Text = "&Wall tool";
            this.EditWall.Value = ExpertSokoban.MainAreaTool.Wall;
            // 
            // EditToolOptions
            // 
            this.EditToolOptions.ValueChanged += new System.EventHandler(this.EditToolOptions_ValueChanged);
            // 
            // EditPiece
            // 
            this.EditPiece.Name = "EditPiece";
            this.EditPiece.ParentGroup = this.EditToolOptions;
            this.EditPiece.ShortcutKeyDisplayString = "";
            this.EditPiece.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.EditPiece.Size = new System.Drawing.Size(225, 22);
            this.EditPiece.Text = "P&iece tool";
            this.EditPiece.Value = ExpertSokoban.MainAreaTool.Piece;
            // 
            // EditTarget
            // 
            this.EditTarget.Name = "EditTarget";
            this.EditTarget.ParentGroup = this.EditToolOptions;
            this.EditTarget.ShortcutKeyDisplayString = "";
            this.EditTarget.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.EditTarget.Size = new System.Drawing.Size(225, 22);
            this.EditTarget.Text = "&Target tool";
            this.EditTarget.Value = ExpertSokoban.MainAreaTool.Target;
            // 
            // EditSokoban
            // 
            this.EditSokoban.Name = "EditSokoban";
            this.EditSokoban.ParentGroup = this.EditToolOptions;
            this.EditSokoban.ShortcutKeyDisplayString = "";
            this.EditSokoban.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.EditSokoban.Size = new System.Drawing.Size(225, 22);
            this.EditSokoban.Text = "&Sokoban tool";
            this.EditSokoban.Value = ExpertSokoban.MainAreaTool.Sokoban;
            // 
            // EditUnusedHotkeys
            // 
            this.EditUnusedHotkeys.Enabled = false;
            this.EditUnusedHotkeys.Name = "EditUnusedHotkeys";
            this.EditUnusedHotkeys.Size = new System.Drawing.Size(225, 22);
            this.EditUnusedHotkeys.Text = "Unused hotkeys: bghjkloqrvxyz";
            this.EditUnusedHotkeys.Visible = false;
            // 
            // ViewMenu
            // 
            this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewLevelList,
            this.ViewToolStrip1,
            this.ViewToolStrip2,
            this.ViewEditToolStrip,
            this.ViewStatusBar,
            this.ViewSep1,
            this.ViewMoveNo,
            this.ViewMoveLine,
            this.ViewMoveDots,
            this.ViewMoveArrows,
            this.ViewSep2,
            this.ViewPushNo,
            this.ViewPushLine,
            this.ViewPushDots,
            this.ViewPushArrows,
            this.ViewSep3,
            this.ViewEndPos,
            this.ViewUnusedHotkeys});
            this.ViewMenu.Name = "ViewMenu";
            this.ViewMenu.Size = new System.Drawing.Size(41, 20);
            this.ViewMenu.Text = "&View";
            // 
            // ViewLevelList
            // 
            this.ViewLevelList.Name = "ViewLevelList";
            this.ViewLevelList.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.ViewLevelList.Size = new System.Drawing.Size(275, 22);
            this.ViewLevelList.Text = "Display &level list";
            this.ViewLevelList.Click += new System.EventHandler(this.ViewLevelList_Click);
            // 
            // ViewToolStrip1
            // 
            this.ViewToolStrip1.Name = "ViewToolStrip1";
            this.ViewToolStrip1.Size = new System.Drawing.Size(275, 22);
            this.ViewToolStrip1.Text = "Display &file toolbar";
            this.ViewToolStrip1.Click += new System.EventHandler(this.ViewToolStrip1_Click);
            // 
            // ViewToolStrip2
            // 
            this.ViewToolStrip2.Name = "ViewToolStrip2";
            this.ViewToolStrip2.Size = new System.Drawing.Size(275, 22);
            this.ViewToolStrip2.Text = "Display &operations toolbar";
            this.ViewToolStrip2.Click += new System.EventHandler(this.ViewToolStrip2_Click);
            // 
            // ViewEditToolStrip
            // 
            this.ViewEditToolStrip.Name = "ViewEditToolStrip";
            this.ViewEditToolStrip.Size = new System.Drawing.Size(275, 22);
            this.ViewEditToolStrip.Text = "Display &edit toolbar";
            this.ViewEditToolStrip.Click += new System.EventHandler(this.ViewEditToolStrip_Click);
            // 
            // ViewStatusBar
            // 
            this.ViewStatusBar.Name = "ViewStatusBar";
            this.ViewStatusBar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.ViewStatusBar.Size = new System.Drawing.Size(275, 22);
            this.ViewStatusBar.Text = "Display stat&us bar";
            this.ViewStatusBar.Click += new System.EventHandler(this.ViewStatusBar_Click);
            // 
            // ViewSep1
            // 
            this.ViewSep1.Name = "ViewSep1";
            this.ViewSep1.Size = new System.Drawing.Size(272, 6);
            // 
            // ViewMoveNo
            // 
            this.ViewMoveNo.Name = "ViewMoveNo";
            this.ViewMoveNo.ParentGroup = this.MovePathOptions;
            this.ViewMoveNo.Size = new System.Drawing.Size(275, 22);
            this.ViewMoveNo.Text = "Don\'t display &move path";
            this.ViewMoveNo.Value = ExpertSokoban.PathDrawMode.None;
            // 
            // MovePathOptions
            // 
            this.MovePathOptions.ValueChanged += new System.EventHandler(this.MovePathOptions_ValueChanged);
            // 
            // ViewMoveLine
            // 
            this.ViewMoveLine.Name = "ViewMoveLine";
            this.ViewMoveLine.ParentGroup = this.MovePathOptions;
            this.ViewMoveLine.Size = new System.Drawing.Size(275, 22);
            this.ViewMoveLine.Text = "Display move path as li&ne";
            this.ViewMoveLine.Value = ExpertSokoban.PathDrawMode.Line;
            // 
            // ViewMoveDots
            // 
            this.ViewMoveDots.Name = "ViewMoveDots";
            this.ViewMoveDots.ParentGroup = this.MovePathOptions;
            this.ViewMoveDots.Size = new System.Drawing.Size(275, 22);
            this.ViewMoveDots.Text = "Display move path as &dots";
            this.ViewMoveDots.Value = ExpertSokoban.PathDrawMode.Dots;
            // 
            // ViewMoveArrows
            // 
            this.ViewMoveArrows.Name = "ViewMoveArrows";
            this.ViewMoveArrows.ParentGroup = this.MovePathOptions;
            this.ViewMoveArrows.Size = new System.Drawing.Size(275, 22);
            this.ViewMoveArrows.Text = "Display move path as &arrows";
            this.ViewMoveArrows.Value = ExpertSokoban.PathDrawMode.Arrows;
            // 
            // ViewSep2
            // 
            this.ViewSep2.Name = "ViewSep2";
            this.ViewSep2.Size = new System.Drawing.Size(272, 6);
            // 
            // ViewPushNo
            // 
            this.ViewPushNo.Name = "ViewPushNo";
            this.ViewPushNo.ParentGroup = this.PushPathOptions;
            this.ViewPushNo.Size = new System.Drawing.Size(275, 22);
            this.ViewPushNo.Text = "Don\'t display &push path";
            this.ViewPushNo.Value = ExpertSokoban.PathDrawMode.None;
            // 
            // PushPathOptions
            // 
            this.PushPathOptions.ValueChanged += new System.EventHandler(this.PushPathOptions_ValueChanged);
            // 
            // ViewPushLine
            // 
            this.ViewPushLine.Name = "ViewPushLine";
            this.ViewPushLine.ParentGroup = this.PushPathOptions;
            this.ViewPushLine.Size = new System.Drawing.Size(275, 22);
            this.ViewPushLine.Text = "Display push path as l&ine";
            this.ViewPushLine.Value = ExpertSokoban.PathDrawMode.Line;
            // 
            // ViewPushDots
            // 
            this.ViewPushDots.Name = "ViewPushDots";
            this.ViewPushDots.ParentGroup = this.PushPathOptions;
            this.ViewPushDots.Size = new System.Drawing.Size(275, 22);
            this.ViewPushDots.Text = "Display push path as do&ts";
            this.ViewPushDots.Value = ExpertSokoban.PathDrawMode.Dots;
            // 
            // ViewPushArrows
            // 
            this.ViewPushArrows.Name = "ViewPushArrows";
            this.ViewPushArrows.ParentGroup = this.PushPathOptions;
            this.ViewPushArrows.Size = new System.Drawing.Size(275, 22);
            this.ViewPushArrows.Text = "Display push path as a&rrows";
            this.ViewPushArrows.Value = ExpertSokoban.PathDrawMode.Arrows;
            // 
            // ViewSep3
            // 
            this.ViewSep3.Name = "ViewSep3";
            this.ViewSep3.Size = new System.Drawing.Size(272, 6);
            // 
            // ViewEndPos
            // 
            this.ViewEndPos.Name = "ViewEndPos";
            this.ViewEndPos.Size = new System.Drawing.Size(275, 22);
            this.ViewEndPos.Text = "Display end position of &Sokoban and piece";
            this.ViewEndPos.Click += new System.EventHandler(this.ViewEndPos_Click);
            // 
            // ViewUnusedHotkeys
            // 
            this.ViewUnusedHotkeys.Enabled = false;
            this.ViewUnusedHotkeys.Name = "ViewUnusedHotkeys";
            this.ViewUnusedHotkeys.Size = new System.Drawing.Size(275, 22);
            this.ViewUnusedHotkeys.Text = "Unused hotkeys: bcghjkqvwxyz";
            this.ViewUnusedHotkeys.Visible = false;
            // 
            // HelpMenu
            // 
            this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpHelp,
            this.HelpKeyboard,
            this.HelpAbout});
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Size = new System.Drawing.Size(40, 20);
            this.HelpMenu.Text = "&Help";
            // 
            // HelpHelp
            // 
            this.HelpHelp.Name = "HelpHelp";
            this.HelpHelp.Size = new System.Drawing.Size(199, 22);
            this.HelpHelp.Text = "&Online Help...";
            this.HelpHelp.Click += new System.EventHandler(this.HelpHelp_Click);
            // 
            // HelpKeyboard
            // 
            this.HelpKeyboard.Name = "HelpKeyboard";
            this.HelpKeyboard.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.HelpKeyboard.Size = new System.Drawing.Size(199, 22);
            this.HelpKeyboard.Text = "&Keyboard shortcuts...";
            this.HelpKeyboard.Click += new System.EventHandler(this.HelpKeyboard_Click);
            // 
            // HelpAbout
            // 
            this.HelpAbout.Name = "HelpAbout";
            this.HelpAbout.Size = new System.Drawing.Size(199, 22);
            this.HelpAbout.Text = "&About";
            this.HelpAbout.Click += new System.EventHandler(this.HelpAbout_Click);
            // 
            // MainToolStripContainer
            // 
            // 
            // MainToolStripContainer.BottomToolStripPanel
            // 
            this.MainToolStripContainer.BottomToolStripPanel.Controls.Add(this.StatusBar);
            // 
            // MainToolStripContainer.ContentPanel
            // 
            this.MainToolStripContainer.ContentPanel.AutoScroll = true;
            this.MainToolStripContainer.ContentPanel.Controls.Add(this.MainArea);
            this.MainToolStripContainer.ContentPanel.Size = new System.Drawing.Size(639, 436);
            this.MainToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.MainToolStripContainer.Name = "MainToolStripContainer";
            this.MainToolStripContainer.Size = new System.Drawing.Size(639, 482);
            this.MainToolStripContainer.TabIndex = 9;
            this.MainToolStripContainer.Text = "toolStripContainer1";
            // 
            // MainToolStripContainer.TopToolStripPanel
            // 
            this.MainToolStripContainer.TopToolStripPanel.Controls.Add(this.MainMenu);
            // 
            // StatusBar
            // 
            this.StatusBar.Dock = System.Windows.Forms.DockStyle.None;
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusMoves,
            this.StatusPushes,
            this.StatusPieces,
            this.StatusEdit,
            this.StatusSolved,
            this.StatusNull});
            this.StatusBar.Location = new System.Drawing.Point(0, 0);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(639, 22);
            this.StatusBar.SizingGrip = false;
            this.StatusBar.TabIndex = 0;
            // 
            // StatusMoves
            // 
            this.StatusMoves.Name = "StatusMoves";
            this.StatusMoves.Size = new System.Drawing.Size(51, 17);
            this.StatusMoves.Text = "Moves: 0";
            this.StatusMoves.Visible = false;
            // 
            // StatusPushes
            // 
            this.StatusPushes.Name = "StatusPushes";
            this.StatusPushes.Size = new System.Drawing.Size(54, 17);
            this.StatusPushes.Text = "Pushes: 0";
            this.StatusPushes.Visible = false;
            // 
            // StatusPieces
            // 
            this.StatusPieces.Name = "StatusPieces";
            this.StatusPieces.Size = new System.Drawing.Size(102, 17);
            this.StatusPieces.Text = "Remaining pieces: 0";
            this.StatusPieces.Visible = false;
            // 
            // StatusEdit
            // 
            this.StatusEdit.Name = "StatusEdit";
            this.StatusEdit.Size = new System.Drawing.Size(174, 17);
            this.StatusEdit.Text = "You are currently editing this level.";
            this.StatusEdit.Visible = false;
            // 
            // StatusSolved
            // 
            this.StatusSolved.Name = "StatusSolved";
            this.StatusSolved.Size = new System.Drawing.Size(217, 17);
            this.StatusSolved.Text = "You have solved the level. Congratulations!";
            this.StatusSolved.Visible = false;
            // 
            // StatusNull
            // 
            this.StatusNull.Name = "StatusNull";
            this.StatusNull.Size = new System.Drawing.Size(337, 17);
            this.StatusNull.Text = "No level s currently selected. Select a level from the level list to play.";
            this.StatusNull.Visible = false;
            // 
            // MainArea
            // 
            this.MainArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(206)))));
            this.MainArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainArea.Location = new System.Drawing.Point(0, 0);
            this.MainArea.Modified = false;
            this.MainArea.MoveDrawMode = ExpertSokoban.PathDrawMode.Line;
            this.MainArea.Name = "MainArea";
            this.MainArea.PushDrawMode = ExpertSokoban.PathDrawMode.Arrows;
            this.MainArea.ShowEndPos = false;
            this.MainArea.Size = new System.Drawing.Size(639, 436);
            this.MainArea.TabIndex = 1;
            this.MainArea.TabStop = true;
            this.MainArea.Tool = ExpertSokoban.MainAreaTool.Wall;
            this.MainArea.Click += new System.EventHandler(this.MainArea_Click);
            this.MainArea.LevelSolved += new System.EventHandler(this.MainArea_LevelSolved);
            this.MainArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainArea_KeyDown);
            // 
            // LevelListSplitter
            // 
            this.LevelListSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.LevelListSplitter.Location = new System.Drawing.Point(636, 0);
            this.LevelListSplitter.MinSize = 50;
            this.LevelListSplitter.Name = "LevelListSplitter";
            this.LevelListSplitter.Size = new System.Drawing.Size(3, 482);
            this.LevelListSplitter.TabIndex = 10;
            this.LevelListSplitter.TabStop = false;
            this.LevelListSplitter.Visible = false;
            // 
            // BugWorkaroundTimer
            // 
            this.BugWorkaroundTimer.Enabled = true;
            this.BugWorkaroundTimer.Tick += new System.EventHandler(this.BugWorkaroundTimer_Tick);
            // 
            // UpdateControlsTimer
            // 
            this.UpdateControlsTimer.Enabled = true;
            this.UpdateControlsTimer.Interval = 10;
            this.UpdateControlsTimer.Tick += new System.EventHandler(this.UpdateControlsTimer_Tick);
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 482);
            this.Controls.Add(this.LevelListSplitter);
            this.Controls.Add(this.MainToolStripContainer);
            this.Controls.Add(this.LevelListPanel);
            this.Icon = global::ExpertSokoban.Properties.Resources.ExpertSokoban;
            this.KeyPreview = true;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "Mainform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Expert Sokoban";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Mainform_FormClosing);
            this.LevelListPanel.ResumeLayout(false);
            this.LevelListPanel.PerformLayout();
            this.LevelContextMenu.ResumeLayout(false);
            this.EditToolStrip.ResumeLayout(false);
            this.EditToolStrip.PerformLayout();
            this.LevelListToolStrip2.ResumeLayout(false);
            this.LevelListToolStrip2.PerformLayout();
            this.LevelListToolStrip1.ResumeLayout(false);
            this.LevelListToolStrip1.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.MainToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.MainToolStripContainer.BottomToolStripPanel.PerformLayout();
            this.MainToolStripContainer.ContentPanel.ResumeLayout(false);
            this.MainToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.MainToolStripContainer.TopToolStripPanel.PerformLayout();
            this.MainToolStripContainer.ResumeLayout(false);
            this.MainToolStripContainer.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RT.Util.Controls.NiceClosePanel LevelListClosePanel;
        private ExpertSokoban.MainArea MainArea;
        private System.Windows.Forms.Panel LevelListPanel;
        private ExpertSokoban.LevelListBox LevelList;
        private System.Windows.Forms.ToolStrip LevelListToolStrip1;
        private System.Windows.Forms.ToolStripButton LevelToolOpen;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem LevelOpen;
        private System.Windows.Forms.ToolStripMenuItem ViewLevelList;
        private System.Windows.Forms.ToolStripButton LevelToolComment;
        private System.Windows.Forms.ToolStripButton LevelToolNew;
        private System.Windows.Forms.ToolStripSeparator LevelToolSep1;
        private System.Windows.Forms.ToolStripMenuItem LevelNew;
        private System.Windows.Forms.ToolStripButton LevelToolNewLevel;
        private System.Windows.Forms.ToolStripMenuItem ViewMenu;
        private MenuRadioItemPathDrawMode ViewMoveNo;
        private MenuRadioItemPathDrawMode ViewMoveDots;
        private MenuRadioItemPathDrawMode ViewMoveLine;
        private MenuRadioItemPathDrawMode ViewMoveArrows;
        private System.Windows.Forms.ToolStripSeparator ViewSep2;
        private MenuRadioItemPathDrawMode ViewPushNo;
        private MenuRadioItemPathDrawMode ViewPushDots;
        private MenuRadioItemPathDrawMode ViewPushLine;
        private MenuRadioItemPathDrawMode ViewPushArrows;
        private System.Windows.Forms.ToolStripSeparator ViewSep3;
        private System.Windows.Forms.ToolStripMenuItem ViewEndPos;
        private System.Windows.Forms.ToolStripMenuItem LevelMenu;
        private System.Windows.Forms.ToolStripSeparator ViewSep1;
        private System.Windows.Forms.ToolStripMenuItem EditMenu;
        private System.Windows.Forms.ToolStripSeparator LevelSep1;
        private System.Windows.Forms.ToolStripButton LevelToolSave;
        private System.Windows.Forms.ToolStripMenuItem LevelSave;
        private System.Windows.Forms.ToolStripContainer MainToolStripContainer;
        private System.Windows.Forms.Splitter LevelListSplitter;
        private System.Windows.Forms.ToolStrip EditToolStrip;
        private System.Windows.Forms.ToolStripButton EditToolWall;
        private System.Windows.Forms.ToolStripButton EditToolPiece;
        private System.Windows.Forms.ToolStripButton EditToolTarget;
        private System.Windows.Forms.ToolStripButton EditToolSokoban;
        private MenuRadioItemMainAreaTool EditWall;
        private MenuRadioItemMainAreaTool EditPiece;
        private MenuRadioItemMainAreaTool EditTarget;
        private MenuRadioItemMainAreaTool EditSokoban;
        private System.Windows.Forms.ToolStripButton EditToolOK;
        private System.Windows.Forms.ToolStripSeparator EditToolSep;
        private System.Windows.Forms.ToolStripMenuItem EditFinish;
        private System.Windows.Forms.ToolStripMenuItem EditCancel;
        private System.Windows.Forms.ToolStripSeparator EditSep3;
        private System.Windows.Forms.ToolStripButton EditToolCancel;
        private System.Windows.Forms.ToolStrip LevelListToolStrip2;
        private System.Windows.Forms.ToolStripButton LevelToolCut;
        private System.Windows.Forms.ToolStripButton LevelToolCopy;
        private System.Windows.Forms.ToolStripButton LevelToolPaste;
        private System.Windows.Forms.ToolStripMenuItem ViewToolStrip1;
        private System.Windows.Forms.ToolStripMenuItem ViewToolStrip2;
        private System.Windows.Forms.ToolStripMenuItem ViewEditToolStrip;
        private System.Windows.Forms.ToolStripMenuItem ViewUnusedHotkeys;
        private System.Windows.Forms.ToolStripButton LevelToolDelete;
        private System.Windows.Forms.ToolStripButton LevelToolEdit;
        private System.Windows.Forms.ToolStripMenuItem LevelExit;
        private System.Windows.Forms.ToolStripMenuItem LevelUndo;
        private System.Windows.Forms.ToolStripMenuItem LevelRetry;
        private System.Windows.Forms.ToolStripMenuItem EditCreateLevel;
        private System.Windows.Forms.ToolStripMenuItem EditEdit;
        private System.Windows.Forms.ToolStripMenuItem EditAddComment;
        private System.Windows.Forms.ToolStripSeparator EditSep1;
        private System.Windows.Forms.ToolStripMenuItem EditCut;
        private System.Windows.Forms.ToolStripMenuItem EditCopy;
        private System.Windows.Forms.ToolStripMenuItem EditPaste;
        private System.Windows.Forms.ToolStripMenuItem EditDelete;
        private System.Windows.Forms.ToolStripSeparator EditSep2;
        private System.Windows.Forms.ToolStripMenuItem EditUnusedHotkeys;
        private System.Windows.Forms.Timer BugWorkaroundTimer;
        private ExpertSokoban.MenuRadioGroupPathDrawMode MovePathOptions;
        private ExpertSokoban.MenuRadioGroupPathDrawMode PushPathOptions;
        private ExpertSokoban.MenuRadioGroupMainAreaTool EditToolOptions;
        private System.Windows.Forms.ToolStripSeparator LevelSep2;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu;
        private System.Windows.Forms.ToolStripMenuItem HelpAbout;
        private System.Windows.Forms.ToolStripMenuItem HelpHelp;
        private System.Windows.Forms.ToolStripMenuItem LevelNext;
        private System.Windows.Forms.ToolStripMenuItem LevelNextUnsolved;
        private System.Windows.Forms.ToolStripSeparator LevelSep3;
        private System.Windows.Forms.ToolStripButton LevelToolNext;
        private System.Windows.Forms.ToolStripButton LevelToolNextUnsolved;
        private System.Windows.Forms.ToolStripMenuItem LevelPrevious;
        private System.Windows.Forms.ToolStripMenuItem LevelPreviousUnsolved;
        private System.Windows.Forms.ToolStripMenuItem UnusedCTRLShortcuts;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel StatusMoves;
        private System.Windows.Forms.ToolStripStatusLabel StatusPushes;
        private System.Windows.Forms.ToolStripStatusLabel StatusPieces;
        private System.Windows.Forms.ToolStripStatusLabel StatusNull;
        private System.Windows.Forms.ToolStripStatusLabel StatusEdit;
        private System.Windows.Forms.ToolStripStatusLabel StatusSolved;
        private System.Windows.Forms.ToolStripMenuItem ViewStatusBar;
        private System.Windows.Forms.ToolStripMenuItem LevelChangePlayer;
        private System.Windows.Forms.ToolStripSeparator LevelSep4;
        private System.Windows.Forms.ToolStripMenuItem LevelUnusedHotkeys;
        private System.Windows.Forms.ContextMenuStrip LevelContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ContextPlay;
        private System.Windows.Forms.ToolStripMenuItem ContextEdit;
        private System.Windows.Forms.ToolStripSeparator ContextSep1;
        private System.Windows.Forms.ToolStripMenuItem ContextNewLevel;
        private System.Windows.Forms.ToolStripMenuItem ContextNewComment;
        private System.Windows.Forms.ToolStripSeparator ContextSep2;
        private System.Windows.Forms.ToolStripMenuItem ContextCut;
        private System.Windows.Forms.ToolStripMenuItem ContextCutItem;
        private System.Windows.Forms.ToolStripMenuItem ContextPaste;
        private System.Windows.Forms.ToolStripMenuItem ContextDelete;
        private System.Windows.Forms.ToolStripSeparator ContextSep3;
        private System.Windows.Forms.ToolStripMenuItem ContextHide;
        private System.Windows.Forms.ToolStripMenuItem LevelRedo;
        private System.Windows.Forms.ToolStripMenuItem HelpKeyboard;
        private System.Windows.Forms.ToolStripMenuItem LevelSaveAs;
        private System.Windows.Forms.Timer UpdateControlsTimer;
        private System.Windows.Forms.ToolStripMenuItem LevelHighscores;
        private System.Windows.Forms.ToolStripMenuItem ContextHighscores;
    }
}

