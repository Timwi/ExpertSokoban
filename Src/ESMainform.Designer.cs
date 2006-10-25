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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainform));
            this.LevelListPanel = new System.Windows.Forms.Panel();
            this.LevelList = new ExpertSokoban.LevelListBox();
            this.LevelListToolStrip = new System.Windows.Forms.ToolStrip();
            this.LevelToolNew = new System.Windows.Forms.ToolStripButton();
            this.LevelToolOpen = new System.Windows.Forms.ToolStripButton();
            this.LevelToolSave = new System.Windows.Forms.ToolStripButton();
            this.LevelToolSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.LevelToolNewLevel = new System.Windows.Forms.ToolStripButton();
            this.LevelToolComment = new System.Windows.Forms.ToolStripButton();
            this.LevelToolDelete = new System.Windows.Forms.ToolStripButton();
            this.LevelToolSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.LevelToolClose = new System.Windows.Forms.ToolStripButton();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.GameMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.GameUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.GameRetry = new System.Windows.Forms.ToolStripMenuItem();
            this.GameSep = new System.Windows.Forms.ToolStripSeparator();
            this.GameExit = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLevelsList = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewMoveNo = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMoveLine = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMoveDots = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMoveArrows = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewPushNo = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewPushLine = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewPushDots = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewPushArrows = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewEndPos = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelNew = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelSave = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.LevelCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelAddComment = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.LevelCut = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.EditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.MainToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.MainArea = new ExpertSokoban.MainArea();
            this.LevelListSplitter = new System.Windows.Forms.Splitter();
            this.LevelListPanel.SuspendLayout();
            this.LevelListToolStrip.SuspendLayout();
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
            this.LevelListPanel.Controls.Add(this.LevelListToolStrip);
            this.LevelListPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.LevelListPanel.Location = new System.Drawing.Point(630, 0);
            this.LevelListPanel.Name = "LevelListPanel";
            this.LevelListPanel.Size = new System.Drawing.Size(200, 488);
            this.LevelListPanel.TabIndex = 6;
            this.LevelListPanel.Visible = false;
            // 
            // LevelList
            // 
            this.LevelList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LevelList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.LevelList.IntegralHeight = false;
            this.LevelList.Location = new System.Drawing.Point(0, 25);
            this.LevelList.Name = "LevelList";
            this.LevelList.ScrollAlwaysVisible = true;
            this.LevelList.Size = new System.Drawing.Size(200, 463);
            this.LevelList.TabIndex = 2;
            this.LevelList.DoubleClick += new System.EventHandler(this.LevelList_DoubleClick);
            this.LevelList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LevelList_KeyPress);
            // 
            // LevelListToolStrip
            // 
            this.LevelListToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LevelToolNew,
            this.LevelToolOpen,
            this.LevelToolSave,
            this.LevelToolSep1,
            this.LevelToolNewLevel,
            this.LevelToolComment,
            this.LevelToolDelete,
            this.LevelToolSep2,
            this.LevelToolClose});
            this.LevelListToolStrip.Location = new System.Drawing.Point(0, 0);
            this.LevelListToolStrip.Name = "LevelListToolStrip";
            this.LevelListToolStrip.Size = new System.Drawing.Size(200, 25);
            this.LevelListToolStrip.TabIndex = 0;
            this.LevelListToolStrip.Text = "toolStrip1";
            // 
            // LevelToolNew
            // 
            this.LevelToolNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolNew.Image = ((System.Drawing.Image)(resources.GetObject("LevelToolNew.Image")));
            this.LevelToolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolNew.Name = "LevelToolNew";
            this.LevelToolNew.Size = new System.Drawing.Size(23, 22);
            this.LevelToolNew.Text = "New level file";
            this.LevelToolNew.Click += new System.EventHandler(this.LevelNew_Click);
            // 
            // LevelToolOpen
            // 
            this.LevelToolOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolOpen.Image = ((System.Drawing.Image)(resources.GetObject("LevelToolOpen.Image")));
            this.LevelToolOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolOpen.Name = "LevelToolOpen";
            this.LevelToolOpen.Size = new System.Drawing.Size(23, 22);
            this.LevelToolOpen.Text = "Open level file";
            this.LevelToolOpen.Click += new System.EventHandler(this.LevelOpen_Click);
            // 
            // LevelToolSave
            // 
            this.LevelToolSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolSave.Enabled = false;
            this.LevelToolSave.Image = ((System.Drawing.Image)(resources.GetObject("LevelToolSave.Image")));
            this.LevelToolSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolSave.Name = "LevelToolSave";
            this.LevelToolSave.Size = new System.Drawing.Size(23, 22);
            this.LevelToolSave.Text = "Save level file";
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
            this.LevelToolNewLevel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolNewLevel.Name = "LevelToolNewLevel";
            this.LevelToolNewLevel.Size = new System.Drawing.Size(23, 22);
            this.LevelToolNewLevel.Text = "Create new level";
            this.LevelToolNewLevel.Click += new System.EventHandler(this.LevelCreate_Click);
            // 
            // LevelToolComment
            // 
            this.LevelToolComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolComment.Image = ((System.Drawing.Image)(resources.GetObject("LevelToolComment.Image")));
            this.LevelToolComment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolComment.Name = "LevelToolComment";
            this.LevelToolComment.Size = new System.Drawing.Size(23, 22);
            this.LevelToolComment.Text = "Add a comment...";
            this.LevelToolComment.Click += new System.EventHandler(this.LevelAddComment_Click);
            // 
            // LevelToolDelete
            // 
            this.LevelToolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolDelete.Image = ((System.Drawing.Image)(resources.GetObject("LevelToolDelete.Image")));
            this.LevelToolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolDelete.Name = "LevelToolDelete";
            this.LevelToolDelete.Size = new System.Drawing.Size(23, 22);
            this.LevelToolDelete.Text = "Delete selected item";
            this.LevelToolDelete.Click += new System.EventHandler(this.LevelDelete_Click);
            // 
            // LevelToolSep2
            // 
            this.LevelToolSep2.Name = "LevelToolSep2";
            this.LevelToolSep2.Size = new System.Drawing.Size(6, 25);
            // 
            // LevelToolClose
            // 
            this.LevelToolClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolClose.DoubleClickEnabled = true;
            this.LevelToolClose.Image = ((System.Drawing.Image)(resources.GetObject("LevelToolClose.Image")));
            this.LevelToolClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LevelToolClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolClose.Name = "LevelToolClose";
            this.LevelToolClose.Size = new System.Drawing.Size(23, 22);
            this.LevelToolClose.Text = "Hide level list";
            this.LevelToolClose.Click += new System.EventHandler(this.ViewLevelsList_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GameMenu,
            this.ViewMenu,
            this.LevelMenu,
            this.EditMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(630, 24);
            this.MainMenu.TabIndex = 8;
            this.MainMenu.Text = "menuStrip1";
            // 
            // GameMenu
            // 
            this.GameMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GameUndo,
            this.GameRetry,
            this.GameSep,
            this.GameExit});
            this.GameMenu.Name = "GameMenu";
            this.GameMenu.Size = new System.Drawing.Size(46, 20);
            this.GameMenu.Text = "&Game";
            // 
            // GameUndo
            // 
            this.GameUndo.Name = "GameUndo";
            this.GameUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.GameUndo.Size = new System.Drawing.Size(165, 22);
            this.GameUndo.Text = "&Undo";
            this.GameUndo.Click += new System.EventHandler(this.GameUndo_Click);
            // 
            // GameRetry
            // 
            this.GameRetry.Name = "GameRetry";
            this.GameRetry.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.GameRetry.Size = new System.Drawing.Size(165, 22);
            this.GameRetry.Text = "&Retry level";
            this.GameRetry.Click += new System.EventHandler(this.GameRetry_Click);
            // 
            // GameSep
            // 
            this.GameSep.Name = "GameSep";
            this.GameSep.Size = new System.Drawing.Size(162, 6);
            // 
            // GameExit
            // 
            this.GameExit.Name = "GameExit";
            this.GameExit.Size = new System.Drawing.Size(165, 22);
            this.GameExit.Text = "E&xit";
            this.GameExit.Click += new System.EventHandler(this.GameExit_Click);
            // 
            // ViewMenu
            // 
            this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewLevelsList,
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
            this.ViewEndPos});
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
            // ViewSep1
            // 
            this.ViewSep1.Name = "ViewSep1";
            this.ViewSep1.Size = new System.Drawing.Size(272, 6);
            // 
            // ViewMoveNo
            // 
            this.ViewMoveNo.Name = "ViewMoveNo";
            this.ViewMoveNo.Size = new System.Drawing.Size(275, 22);
            this.ViewMoveNo.Text = "Don\'t display &move path";
            this.ViewMoveNo.Click += new System.EventHandler(this.ViewMove_Click);
            // 
            // ViewMoveLine
            // 
            this.ViewMoveLine.Name = "ViewMoveLine";
            this.ViewMoveLine.Size = new System.Drawing.Size(275, 22);
            this.ViewMoveLine.Text = "Display move path as &line";
            this.ViewMoveLine.Click += new System.EventHandler(this.ViewMove_Click);
            // 
            // ViewMoveDots
            // 
            this.ViewMoveDots.Name = "ViewMoveDots";
            this.ViewMoveDots.Size = new System.Drawing.Size(275, 22);
            this.ViewMoveDots.Text = "Display move path as &dots";
            this.ViewMoveDots.Click += new System.EventHandler(this.ViewMove_Click);
            // 
            // ViewMoveArrows
            // 
            this.ViewMoveArrows.Name = "ViewMoveArrows";
            this.ViewMoveArrows.Size = new System.Drawing.Size(275, 22);
            this.ViewMoveArrows.Text = "Display move path as &arrows";
            this.ViewMoveArrows.Click += new System.EventHandler(this.ViewMove_Click);
            // 
            // ViewSep2
            // 
            this.ViewSep2.Name = "ViewSep2";
            this.ViewSep2.Size = new System.Drawing.Size(272, 6);
            // 
            // ViewPushNo
            // 
            this.ViewPushNo.Name = "ViewPushNo";
            this.ViewPushNo.Size = new System.Drawing.Size(275, 22);
            this.ViewPushNo.Text = "Don\'t display &push path";
            this.ViewPushNo.Click += new System.EventHandler(this.ViewPush_Click);
            // 
            // ViewPushLine
            // 
            this.ViewPushLine.Name = "ViewPushLine";
            this.ViewPushLine.Size = new System.Drawing.Size(275, 22);
            this.ViewPushLine.Text = "Display push path as l&ine";
            this.ViewPushLine.Click += new System.EventHandler(this.ViewPush_Click);
            // 
            // ViewPushDots
            // 
            this.ViewPushDots.Name = "ViewPushDots";
            this.ViewPushDots.Size = new System.Drawing.Size(275, 22);
            this.ViewPushDots.Text = "Display push path as d&ots";
            this.ViewPushDots.Click += new System.EventHandler(this.ViewPush_Click);
            // 
            // ViewPushArrows
            // 
            this.ViewPushArrows.Name = "ViewPushArrows";
            this.ViewPushArrows.Size = new System.Drawing.Size(275, 22);
            this.ViewPushArrows.Text = "Display push path as a&rrows";
            this.ViewPushArrows.Click += new System.EventHandler(this.ViewPush_Click);
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
            this.ViewEndPos.Text = "Display &end position of Sokoban and piece";
            this.ViewEndPos.Click += new System.EventHandler(this.ViewEndPos_Click);
            // 
            // LevelMenu
            // 
            this.LevelMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LevelNew,
            this.LevelOpen,
            this.LevelSave,
            this.LevelSep1,
            this.LevelCreate,
            this.LevelEdit,
            this.LevelAddComment,
            this.LevelSep2,
            this.LevelCut,
            this.LevelCopy,
            this.LevelPaste,
            this.LevelDelete});
            this.LevelMenu.Name = "LevelMenu";
            this.LevelMenu.Size = new System.Drawing.Size(44, 20);
            this.LevelMenu.Text = "&Level";
            // 
            // LevelNew
            // 
            this.LevelNew.Name = "LevelNew";
            this.LevelNew.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.LevelNew.Size = new System.Drawing.Size(206, 22);
            this.LevelNew.Text = "Ne&w level file";
            this.LevelNew.Click += new System.EventHandler(this.LevelNew_Click);
            // 
            // LevelOpen
            // 
            this.LevelOpen.Name = "LevelOpen";
            this.LevelOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.LevelOpen.Size = new System.Drawing.Size(206, 22);
            this.LevelOpen.Text = "&Open level file...";
            this.LevelOpen.Click += new System.EventHandler(this.LevelOpen_Click);
            // 
            // LevelSave
            // 
            this.LevelSave.Enabled = false;
            this.LevelSave.Name = "LevelSave";
            this.LevelSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.LevelSave.Size = new System.Drawing.Size(206, 22);
            this.LevelSave.Text = "&Save level file";
            // 
            // LevelSep1
            // 
            this.LevelSep1.Name = "LevelSep1";
            this.LevelSep1.Size = new System.Drawing.Size(203, 6);
            // 
            // LevelCreate
            // 
            this.LevelCreate.Enabled = false;
            this.LevelCreate.Name = "LevelCreate";
            this.LevelCreate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.LevelCreate.Size = new System.Drawing.Size(206, 22);
            this.LevelCreate.Text = "Create &new level";
            this.LevelCreate.Click += new System.EventHandler(this.LevelCreate_Click);
            // 
            // LevelEdit
            // 
            this.LevelEdit.Enabled = false;
            this.LevelEdit.Name = "LevelEdit";
            this.LevelEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.LevelEdit.Size = new System.Drawing.Size(206, 22);
            this.LevelEdit.Text = "&Edit level";
            this.LevelEdit.Click += new System.EventHandler(this.LevelEdit_Click);
            // 
            // LevelAddComment
            // 
            this.LevelAddComment.Enabled = false;
            this.LevelAddComment.Name = "LevelAddComment";
            this.LevelAddComment.ShortcutKeyDisplayString = "";
            this.LevelAddComment.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.LevelAddComment.Size = new System.Drawing.Size(206, 22);
            this.LevelAddComment.Text = "Add a co&mment...";
            this.LevelAddComment.Click += new System.EventHandler(this.LevelAddComment_Click);
            // 
            // LevelSep2
            // 
            this.LevelSep2.Name = "LevelSep2";
            this.LevelSep2.Size = new System.Drawing.Size(203, 6);
            // 
            // LevelCut
            // 
            this.LevelCut.Enabled = false;
            this.LevelCut.Name = "LevelCut";
            this.LevelCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.LevelCut.Size = new System.Drawing.Size(206, 22);
            this.LevelCut.Text = "C&ut";
            this.LevelCut.Click += new System.EventHandler(this.LevelCut_Click);
            // 
            // LevelCopy
            // 
            this.LevelCopy.Enabled = false;
            this.LevelCopy.Name = "LevelCopy";
            this.LevelCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.LevelCopy.Size = new System.Drawing.Size(206, 22);
            this.LevelCopy.Text = "&Copy";
            this.LevelCopy.Click += new System.EventHandler(this.LevelCopy_Click);
            // 
            // LevelPaste
            // 
            this.LevelPaste.Enabled = false;
            this.LevelPaste.Name = "LevelPaste";
            this.LevelPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.LevelPaste.Size = new System.Drawing.Size(206, 22);
            this.LevelPaste.Text = "&Paste";
            this.LevelPaste.Click += new System.EventHandler(this.LevelPaste_Click);
            // 
            // LevelDelete
            // 
            this.LevelDelete.Enabled = false;
            this.LevelDelete.Name = "LevelDelete";
            this.LevelDelete.ShortcutKeyDisplayString = "";
            this.LevelDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.LevelDelete.Size = new System.Drawing.Size(206, 22);
            this.LevelDelete.Text = "&Delete";
            this.LevelDelete.Click += new System.EventHandler(this.LevelDelete_Click);
            // 
            // EditMenu
            // 
            this.EditMenu.Name = "EditMenu";
            this.EditMenu.Size = new System.Drawing.Size(37, 20);
            this.EditMenu.Text = "&Edit";
            this.EditMenu.Visible = false;
            // 
            // MainToolStripContainer
            // 
            // 
            // MainToolStripContainer.ContentPanel
            // 
            this.MainToolStripContainer.ContentPanel.AutoScroll = true;
            this.MainToolStripContainer.ContentPanel.Controls.Add(this.MainArea);
            this.MainToolStripContainer.ContentPanel.Size = new System.Drawing.Size(630, 464);
            this.MainToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.MainToolStripContainer.Name = "MainToolStripContainer";
            this.MainToolStripContainer.Size = new System.Drawing.Size(630, 488);
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
            this.MainArea.Size = new System.Drawing.Size(630, 464);
            this.MainArea.TabIndex = 1;
            this.MainArea.MoveMade += new System.EventHandler(this.MainArea_MoveMade);
            // 
            // LevelListSplitter
            // 
            this.LevelListSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.LevelListSplitter.Location = new System.Drawing.Point(627, 0);
            this.LevelListSplitter.Name = "LevelListSplitter";
            this.LevelListSplitter.Size = new System.Drawing.Size(3, 488);
            this.LevelListSplitter.TabIndex = 10;
            this.LevelListSplitter.TabStop = false;
            this.LevelListSplitter.Visible = false;
            // 
            // ESMainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 488);
            this.Controls.Add(this.LevelListSplitter);
            this.Controls.Add(this.MainToolStripContainer);
            this.Controls.Add(this.LevelListPanel);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "ESMainform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Expert Sokoban";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ESMainform_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ESMainform_FormClosing);
            this.LevelListPanel.ResumeLayout(false);
            this.LevelListPanel.PerformLayout();
            this.LevelListToolStrip.ResumeLayout(false);
            this.LevelListToolStrip.PerformLayout();
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

        private ExpertSokoban.MainArea MainArea;
        private System.Windows.Forms.Panel LevelListPanel;
        private ExpertSokoban.LevelListBox LevelList;
        private System.Windows.Forms.ToolStrip LevelListToolStrip;
        private System.Windows.Forms.ToolStripButton LevelToolClose;
        private System.Windows.Forms.ToolStripButton LevelToolOpen;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem GameMenu;
        private System.Windows.Forms.ToolStripMenuItem LevelOpen;
        private System.Windows.Forms.ToolStripMenuItem ViewLevelsList;
        private System.Windows.Forms.ToolStripMenuItem GameUndo;
        private System.Windows.Forms.ToolStripMenuItem GameRetry;
        private System.Windows.Forms.ToolStripSeparator GameSep;
        private System.Windows.Forms.ToolStripMenuItem GameExit;
        private System.Windows.Forms.ToolStripButton LevelToolComment;
        private System.Windows.Forms.ToolStripButton LevelToolNew;
        private System.Windows.Forms.ToolStripMenuItem LevelCreate;
        private System.Windows.Forms.ToolStripMenuItem LevelAddComment;
        private System.Windows.Forms.ToolStripSeparator LevelToolSep1;
        private System.Windows.Forms.ToolStripMenuItem LevelNew;
        private System.Windows.Forms.ToolStripButton LevelToolNewLevel;
        private System.Windows.Forms.ToolStripButton LevelToolDelete;
        private System.Windows.Forms.ToolStripMenuItem ViewMenu;
        private System.Windows.Forms.ToolStripMenuItem ViewMoveNo;
        private System.Windows.Forms.ToolStripMenuItem ViewMoveLine;
        private System.Windows.Forms.ToolStripMenuItem ViewMoveArrows;
        private System.Windows.Forms.ToolStripSeparator ViewSep2;
        private System.Windows.Forms.ToolStripMenuItem ViewPushNo;
        private System.Windows.Forms.ToolStripMenuItem ViewPushLine;
        private System.Windows.Forms.ToolStripMenuItem ViewPushArrows;
        private System.Windows.Forms.ToolStripMenuItem ViewMoveDots;
        private System.Windows.Forms.ToolStripMenuItem ViewPushDots;
        private System.Windows.Forms.ToolStripSeparator ViewSep3;
        private System.Windows.Forms.ToolStripMenuItem ViewEndPos;
        private System.Windows.Forms.ToolStripSeparator LevelSep2;
        private System.Windows.Forms.ToolStripMenuItem LevelCut;
        private System.Windows.Forms.ToolStripMenuItem LevelCopy;
        private System.Windows.Forms.ToolStripMenuItem LevelPaste;
        private System.Windows.Forms.ToolStripMenuItem LevelDelete;
        private System.Windows.Forms.ToolStripMenuItem LevelMenu;
        private System.Windows.Forms.ToolStripSeparator ViewSep1;
        private System.Windows.Forms.ToolStripMenuItem LevelEdit;
        private System.Windows.Forms.ToolStripMenuItem EditMenu;
        private System.Windows.Forms.ToolStripSeparator LevelSep1;
        private System.Windows.Forms.ToolStripSeparator LevelToolSep2;
        private System.Windows.Forms.ToolStripButton LevelToolSave;
        private System.Windows.Forms.ToolStripMenuItem LevelSave;
        private System.Windows.Forms.ToolStripContainer MainToolStripContainer;
        private System.Windows.Forms.Splitter LevelListSplitter;
    }
}

