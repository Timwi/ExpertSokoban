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
            this.LevelSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.LevelUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelRetry = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.LevelExit = new System.Windows.Forms.ToolStripMenuItem();
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
            this.ViewLevelsList = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStrip1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStrip2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewEditToolStrip = new System.Windows.Forms.ToolStripMenuItem();
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
            this.MainToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.MainArea = new ExpertSokoban.MainArea();
            this.LevelListSplitter = new System.Windows.Forms.Splitter();
            this.BugWorkaroundTimer = new System.Windows.Forms.Timer(this.components);
            this.LevelListPanel.SuspendLayout();
            this.EditToolStrip.SuspendLayout();
            this.LevelListToolStrip2.SuspendLayout();
            this.LevelListToolStrip1.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.MainToolStripContainer.ContentPanel.SuspendLayout();
            this.MainToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.MainToolStripContainer.SuspendLayout();
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
            this.LevelListPanel.Location = new System.Drawing.Point(644, 0);
            this.LevelListPanel.Name = "LevelListPanel";
            this.LevelListPanel.Size = new System.Drawing.Size(152, 482);
            this.LevelListPanel.TabIndex = 6;
            this.LevelListPanel.Visible = false;
            this.LevelListPanel.Resize += new System.EventHandler(this.LevelListPanel_Resize);
            // 
            // LevelList
            // 
            this.LevelList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LevelList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.LevelList.EditingIndex = null;
            this.LevelList.IntegralHeight = false;
            this.LevelList.Location = new System.Drawing.Point(0, 60);
            this.LevelList.Name = "LevelList";
            this.LevelList.PlayingIndex = null;
            this.LevelList.ScrollAlwaysVisible = true;
            this.LevelList.Size = new System.Drawing.Size(152, 422);
            this.LevelList.TabIndex = 2;
            this.LevelList.DoubleClick += new System.EventHandler(this.LevelList_DoubleClick);
            this.LevelList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LevelList_KeyPress);
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
            this.EditToolStrip.Location = new System.Drawing.Point(0, 50);
            this.EditToolStrip.Name = "EditToolStrip";
            this.EditToolStrip.Size = new System.Drawing.Size(152, 25);
            this.EditToolStrip.TabIndex = 3;
            this.EditToolStrip.Text = "Edit toolbar";
            this.EditToolStrip.Visible = false;
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
            this.EditToolOK.Click += new System.EventHandler(this.EditOK_Click);
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
            this.LevelToolDelete});
            this.LevelListToolStrip2.Location = new System.Drawing.Point(0, 35);
            this.LevelListToolStrip2.Name = "LevelListToolStrip2";
            this.LevelListToolStrip2.Size = new System.Drawing.Size(152, 25);
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
            this.LevelListToolStrip1.Size = new System.Drawing.Size(152, 25);
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
            this.LevelListClosePanel.Size = new System.Drawing.Size(152, 10);
            this.LevelListClosePanel.TabIndex = 5;
            this.LevelListClosePanel.CloseClicked += new System.EventHandler(this.ViewLevelsList_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LevelMenu,
            this.EditMenu,
            this.ViewMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(644, 24);
            this.MainMenu.TabIndex = 8;
            this.MainMenu.Text = "menuStrip1";
            // 
            // LevelMenu
            // 
            this.LevelMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LevelNew,
            this.LevelOpen,
            this.LevelSave,
            this.LevelSep1,
            this.LevelUndo,
            this.LevelRetry,
            this.LevelSep2,
            this.LevelExit});
            this.LevelMenu.Name = "LevelMenu";
            this.LevelMenu.Size = new System.Drawing.Size(44, 20);
            this.LevelMenu.Text = "&Level";
            // 
            // LevelNew
            // 
            this.LevelNew.Name = "LevelNew";
            this.LevelNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.LevelNew.Size = new System.Drawing.Size(194, 22);
            this.LevelNew.Text = "&New level file";
            this.LevelNew.Click += new System.EventHandler(this.LevelNew_Click);
            // 
            // LevelOpen
            // 
            this.LevelOpen.Name = "LevelOpen";
            this.LevelOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.LevelOpen.Size = new System.Drawing.Size(194, 22);
            this.LevelOpen.Text = "&Open level file...";
            this.LevelOpen.Click += new System.EventHandler(this.LevelOpen_Click);
            // 
            // LevelSave
            // 
            this.LevelSave.Enabled = false;
            this.LevelSave.Name = "LevelSave";
            this.LevelSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.LevelSave.Size = new System.Drawing.Size(194, 22);
            this.LevelSave.Text = "&Save level file";
            this.LevelSave.Click += new System.EventHandler(this.LevelSave_Click);
            // 
            // LevelSep1
            // 
            this.LevelSep1.Name = "LevelSep1";
            this.LevelSep1.Size = new System.Drawing.Size(191, 6);
            // 
            // LevelUndo
            // 
            this.LevelUndo.Name = "LevelUndo";
            this.LevelUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.LevelUndo.Size = new System.Drawing.Size(194, 22);
            this.LevelUndo.Text = "&Undo move";
            this.LevelUndo.Click += new System.EventHandler(this.LevelUndo_Click);
            // 
            // LevelRetry
            // 
            this.LevelRetry.Name = "LevelRetry";
            this.LevelRetry.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.LevelRetry.Size = new System.Drawing.Size(194, 22);
            this.LevelRetry.Text = "&Retry level";
            this.LevelRetry.Click += new System.EventHandler(this.LevelRetry_Click);
            // 
            // LevelSep2
            // 
            this.LevelSep2.Name = "LevelSep2";
            this.LevelSep2.Size = new System.Drawing.Size(191, 6);
            // 
            // LevelExit
            // 
            this.LevelExit.Name = "LevelExit";
            this.LevelExit.Size = new System.Drawing.Size(194, 22);
            this.LevelExit.Text = "E&xit";
            this.LevelExit.Click += new System.EventHandler(this.LevelExit_Click);
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
            this.EditCreateLevel.Enabled = false;
            this.EditCreateLevel.Name = "EditCreateLevel";
            this.EditCreateLevel.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.EditCreateLevel.Size = new System.Drawing.Size(225, 22);
            this.EditCreateLevel.Text = "Create &new level";
            this.EditCreateLevel.Click += new System.EventHandler(this.EditCreateLevel_Click);
            // 
            // EditEdit
            // 
            this.EditEdit.Enabled = false;
            this.EditEdit.Name = "EditEdit";
            this.EditEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.EditEdit.Size = new System.Drawing.Size(225, 22);
            this.EditEdit.Text = "&Edit level";
            this.EditEdit.Click += new System.EventHandler(this.EditEdit_Click);
            // 
            // EditAddComment
            // 
            this.EditAddComment.Enabled = false;
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
            this.EditCut.Enabled = false;
            this.EditCut.Name = "EditCut";
            this.EditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.EditCut.Size = new System.Drawing.Size(225, 22);
            this.EditCut.Text = "C&ut";
            this.EditCut.Click += new System.EventHandler(this.EditCut_Click);
            // 
            // EditCopy
            // 
            this.EditCopy.Enabled = false;
            this.EditCopy.Name = "EditCopy";
            this.EditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.EditCopy.Size = new System.Drawing.Size(225, 22);
            this.EditCopy.Text = "&Copy";
            this.EditCopy.Click += new System.EventHandler(this.EditCopy_Click);
            // 
            // EditPaste
            // 
            this.EditPaste.Enabled = false;
            this.EditPaste.Name = "EditPaste";
            this.EditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.EditPaste.Size = new System.Drawing.Size(225, 22);
            this.EditPaste.Text = "&Paste";
            this.EditPaste.Click += new System.EventHandler(this.EditPaste_Click);
            // 
            // EditDelete
            // 
            this.EditDelete.Enabled = false;
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
            this.EditFinish.Enabled = false;
            this.EditFinish.Name = "EditFinish";
            this.EditFinish.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)));
            this.EditFinish.Size = new System.Drawing.Size(225, 22);
            this.EditFinish.Text = "&Finish editing";
            this.EditFinish.Click += new System.EventHandler(this.EditOK_Click);
            // 
            // EditCancel
            // 
            this.EditCancel.Enabled = false;
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
            this.EditWall.Enabled = false;
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
            this.EditPiece.Enabled = false;
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
            this.EditTarget.Enabled = false;
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
            this.EditSokoban.Enabled = false;
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
            this.EditUnusedHotkeys.Name = "EditUnusedHotkeys";
            this.EditUnusedHotkeys.Size = new System.Drawing.Size(225, 22);
            this.EditUnusedHotkeys.Text = "Unused hotkeys: bghjkloqrvxyz";
            this.EditUnusedHotkeys.Visible = false;
            // 
            // ViewMenu
            // 
            this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewLevelsList,
            this.ViewToolStrip1,
            this.ViewToolStrip2,
            this.ViewEditToolStrip,
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
            // ViewLevelsList
            // 
            this.ViewLevelsList.Name = "ViewLevelsList";
            this.ViewLevelsList.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.ViewLevelsList.Size = new System.Drawing.Size(275, 22);
            this.ViewLevelsList.Text = "Display &levels list";
            this.ViewLevelsList.Click += new System.EventHandler(this.ViewLevelsList_Click);
            // 
            // ViewToolStrip1
            // 
            this.ViewToolStrip1.Enabled = false;
            this.ViewToolStrip1.Name = "ViewToolStrip1";
            this.ViewToolStrip1.Size = new System.Drawing.Size(275, 22);
            this.ViewToolStrip1.Text = "Display &file toolbar";
            this.ViewToolStrip1.Click += new System.EventHandler(this.ViewToolStrip1_Click);
            // 
            // ViewToolStrip2
            // 
            this.ViewToolStrip2.Enabled = false;
            this.ViewToolStrip2.Name = "ViewToolStrip2";
            this.ViewToolStrip2.Size = new System.Drawing.Size(275, 22);
            this.ViewToolStrip2.Text = "Display &operations toolbar";
            this.ViewToolStrip2.Click += new System.EventHandler(this.ViewToolStrip2_Click);
            // 
            // ViewEditToolStrip
            // 
            this.ViewEditToolStrip.Enabled = false;
            this.ViewEditToolStrip.Name = "ViewEditToolStrip";
            this.ViewEditToolStrip.Size = new System.Drawing.Size(275, 22);
            this.ViewEditToolStrip.Text = "Display &edit toolbar";
            this.ViewEditToolStrip.Click += new System.EventHandler(this.ViewEditToolStrip_Click);
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
            this.ViewUnusedHotkeys.Name = "ViewUnusedHotkeys";
            this.ViewUnusedHotkeys.Size = new System.Drawing.Size(275, 22);
            this.ViewUnusedHotkeys.Text = "Unused hotkeys: bcghjkquvwxyz";
            this.ViewUnusedHotkeys.Visible = false;
            // 
            // MainToolStripContainer
            // 
            // 
            // MainToolStripContainer.ContentPanel
            // 
            this.MainToolStripContainer.ContentPanel.AutoScroll = true;
            this.MainToolStripContainer.ContentPanel.Controls.Add(this.MainArea);
            this.MainToolStripContainer.ContentPanel.Size = new System.Drawing.Size(644, 458);
            this.MainToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.MainToolStripContainer.Name = "MainToolStripContainer";
            this.MainToolStripContainer.Size = new System.Drawing.Size(644, 482);
            this.MainToolStripContainer.TabIndex = 9;
            this.MainToolStripContainer.Text = "toolStripContainer1";
            // 
            // MainToolStripContainer.TopToolStripPanel
            // 
            this.MainToolStripContainer.TopToolStripPanel.Controls.Add(this.MainMenu);
            // 
            // MainArea
            // 
            this.MainArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(206)))));
            this.MainArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainArea.Location = new System.Drawing.Point(0, 0);
            this.MainArea.MoveDrawMode = ExpertSokoban.PathDrawMode.Line;
            this.MainArea.Name = "MainArea";
            this.MainArea.PushDrawMode = ExpertSokoban.PathDrawMode.Arrows;
            this.MainArea.ShowEndPos = false;
            this.MainArea.Size = new System.Drawing.Size(644, 458);
            this.MainArea.TabIndex = 1;
            this.MainArea.Tool = ExpertSokoban.MainAreaTool.Wall;
            this.MainArea.LevelChanged += new System.EventHandler(this.MainArea_LevelChanged);
            this.MainArea.MoveMade += new System.EventHandler(this.MainArea_MoveMade);
            // 
            // LevelListSplitter
            // 
            this.LevelListSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.LevelListSplitter.Location = new System.Drawing.Point(641, 0);
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
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 482);
            this.Controls.Add(this.LevelListSplitter);
            this.Controls.Add(this.MainToolStripContainer);
            this.Controls.Add(this.LevelListPanel);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "Mainform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Expert Sokoban";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Mainform_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Mainform_FormClosing);
            this.LevelListPanel.ResumeLayout(false);
            this.LevelListPanel.PerformLayout();
            this.EditToolStrip.ResumeLayout(false);
            this.EditToolStrip.PerformLayout();
            this.LevelListToolStrip2.ResumeLayout(false);
            this.LevelListToolStrip2.PerformLayout();
            this.LevelListToolStrip1.ResumeLayout(false);
            this.LevelListToolStrip1.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.MainToolStripContainer.ContentPanel.ResumeLayout(false);
            this.MainToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.MainToolStripContainer.TopToolStripPanel.PerformLayout();
            this.MainToolStripContainer.ResumeLayout(false);
            this.MainToolStripContainer.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem ViewLevelsList;
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
        private System.Windows.Forms.ToolStripSeparator LevelSep2;
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
    }
}

