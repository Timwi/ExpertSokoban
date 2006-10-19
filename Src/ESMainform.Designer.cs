namespace ExpertSokoban
{
    partial class ESMainform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ESMainform));
            this.LevelListPanel = new System.Windows.Forms.Panel();
            this.LevelListToolStrip = new System.Windows.Forms.ToolStrip();
            this.LevelToolOpen = new System.Windows.Forms.ToolStripButton();
            this.LevelToolSep = new System.Windows.Forms.ToolStripSeparator();
            this.LevelToolComment = new System.Windows.Forms.ToolStripButton();
            this.LevelToolClear = new System.Windows.Forms.ToolStripButton();
            this.LevelToolClose = new System.Windows.Forms.ToolStripButton();
            this.LevelListSplitter = new System.Windows.Forms.Splitter();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.GameMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuShowList = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLevelsSub = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLevCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLevAddComment = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLevClear = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuRetry = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelToolNewLevel = new System.Windows.Forms.ToolStripButton();
            this.LevelToolDelete = new System.Windows.Forms.ToolStripButton();
            this.MainArea = new ExpertSokoban.ESMainArea();
            this.LevelList = new ExpertSokoban.ESLevelListBox();
            this.LevelListPanel.SuspendLayout();
            this.LevelListToolStrip.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // LevelListPanel
            // 
            this.LevelListPanel.BackColor = System.Drawing.SystemColors.Control;
            this.LevelListPanel.Controls.Add(this.LevelList);
            this.LevelListPanel.Controls.Add(this.LevelListToolStrip);
            this.LevelListPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.LevelListPanel.Location = new System.Drawing.Point(630, 24);
            this.LevelListPanel.Name = "LevelListPanel";
            this.LevelListPanel.Size = new System.Drawing.Size(200, 464);
            this.LevelListPanel.TabIndex = 6;
            this.LevelListPanel.Visible = false;
            // 
            // LevelListToolStrip
            // 
            this.LevelListToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LevelToolOpen,
            this.LevelToolSep,
            this.LevelToolNewLevel,
            this.LevelToolComment,
            this.LevelToolDelete,
            this.LevelToolClear,
            this.LevelToolClose});
            this.LevelListToolStrip.Location = new System.Drawing.Point(0, 0);
            this.LevelListToolStrip.Name = "LevelListToolStrip";
            this.LevelListToolStrip.Size = new System.Drawing.Size(200, 25);
            this.LevelListToolStrip.TabIndex = 0;
            this.LevelListToolStrip.Text = "toolStrip1";
            // 
            // LevelToolOpen
            // 
            this.LevelToolOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolOpen.Image = ((System.Drawing.Image) (resources.GetObject("LevelToolOpen.Image")));
            this.LevelToolOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolOpen.Name = "LevelToolOpen";
            this.LevelToolOpen.Size = new System.Drawing.Size(23, 22);
            this.LevelToolOpen.Text = "Open level file";
            this.LevelToolOpen.Click += new System.EventHandler(this.MenuOpen_Click);
            // 
            // LevelToolSep
            // 
            this.LevelToolSep.Name = "LevelToolSep";
            this.LevelToolSep.Size = new System.Drawing.Size(6, 25);
            // 
            // LevelToolComment
            // 
            this.LevelToolComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolComment.Image = ((System.Drawing.Image) (resources.GetObject("LevelToolComment.Image")));
            this.LevelToolComment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolComment.Name = "LevelToolComment";
            this.LevelToolComment.Size = new System.Drawing.Size(23, 22);
            this.LevelToolComment.Text = "Add a comment...";
            this.LevelToolComment.Click += new System.EventHandler(this.MenuLevAddComment_Click);
            // 
            // LevelToolClear
            // 
            this.LevelToolClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolClear.Image = ((System.Drawing.Image) (resources.GetObject("LevelToolClear.Image")));
            this.LevelToolClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolClear.Name = "LevelToolClear";
            this.LevelToolClear.Size = new System.Drawing.Size(23, 22);
            this.LevelToolClear.Text = "Clear level list";
            this.LevelToolClear.Click += new System.EventHandler(this.MenuLevClear_Click);
            // 
            // LevelToolClose
            // 
            this.LevelToolClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolClose.DoubleClickEnabled = true;
            this.LevelToolClose.Image = ((System.Drawing.Image) (resources.GetObject("LevelToolClose.Image")));
            this.LevelToolClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LevelToolClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolClose.Name = "LevelToolClose";
            this.LevelToolClose.Size = new System.Drawing.Size(23, 22);
            this.LevelToolClose.Text = "Hide level list";
            this.LevelToolClose.Click += new System.EventHandler(this.MenuShowList_Click);
            // 
            // LevelListSplitter
            // 
            this.LevelListSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.LevelListSplitter.Location = new System.Drawing.Point(627, 24);
            this.LevelListSplitter.Name = "LevelListSplitter";
            this.LevelListSplitter.Size = new System.Drawing.Size(3, 464);
            this.LevelListSplitter.TabIndex = 7;
            this.LevelListSplitter.TabStop = false;
            this.LevelListSplitter.Visible = false;
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GameMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(830, 24);
            this.MainMenu.TabIndex = 8;
            this.MainMenu.Text = "menuStrip1";
            // 
            // GameMenu
            // 
            this.GameMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuOpen,
            this.MenuShowList,
            this.MenuLevelsSub,
            this.MenuSep1,
            this.MenuUndo,
            this.MenuRetry,
            this.MenuSep2,
            this.MenuExit});
            this.GameMenu.Name = "GameMenu";
            this.GameMenu.Size = new System.Drawing.Size(46, 20);
            this.GameMenu.Text = "&Game";
            // 
            // MenuOpen
            // 
            this.MenuOpen.Name = "MenuOpen";
            this.MenuOpen.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MenuOpen.Size = new System.Drawing.Size(194, 22);
            this.MenuOpen.Text = "&Open level file...";
            this.MenuOpen.Click += new System.EventHandler(this.MenuOpen_Click);
            // 
            // MenuShowList
            // 
            this.MenuShowList.Name = "MenuShowList";
            this.MenuShowList.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.MenuShowList.Size = new System.Drawing.Size(194, 22);
            this.MenuShowList.Text = "Display &levels list";
            this.MenuShowList.Click += new System.EventHandler(this.MenuShowList_Click);
            // 
            // MenuLevelsSub
            // 
            this.MenuLevelsSub.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuLevCreate,
            this.MenuLevAddComment,
            this.MenuLevClear});
            this.MenuLevelsSub.Name = "MenuLevelsSub";
            this.MenuLevelsSub.Size = new System.Drawing.Size(194, 22);
            this.MenuLevelsSub.Text = "Le&vels list";
            // 
            // MenuLevCreate
            // 
            this.MenuLevCreate.Name = "MenuLevCreate";
            this.MenuLevCreate.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.MenuLevCreate.Size = new System.Drawing.Size(206, 22);
            this.MenuLevCreate.Text = "Create &new level...";
            this.MenuLevCreate.Click += new System.EventHandler(this.MenuLevCreate_Click);
            // 
            // MenuLevAddComment
            // 
            this.MenuLevAddComment.Name = "MenuLevAddComment";
            this.MenuLevAddComment.ShortcutKeyDisplayString = "";
            this.MenuLevAddComment.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.MenuLevAddComment.Size = new System.Drawing.Size(206, 22);
            this.MenuLevAddComment.Text = "Add a co&mment...";
            this.MenuLevAddComment.Click += new System.EventHandler(this.MenuLevAddComment_Click);
            // 
            // MenuLevClear
            // 
            this.MenuLevClear.Name = "MenuLevClear";
            this.MenuLevClear.Size = new System.Drawing.Size(206, 22);
            this.MenuLevClear.Text = "&Clear levels list";
            this.MenuLevClear.Click += new System.EventHandler(this.MenuLevClear_Click);
            // 
            // MenuSep1
            // 
            this.MenuSep1.Name = "MenuSep1";
            this.MenuSep1.Size = new System.Drawing.Size(191, 6);
            // 
            // MenuUndo
            // 
            this.MenuUndo.Name = "MenuUndo";
            this.MenuUndo.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.MenuUndo.Size = new System.Drawing.Size(194, 22);
            this.MenuUndo.Text = "&Undo";
            this.MenuUndo.Click += new System.EventHandler(this.MenuUndo_Click);
            // 
            // MenuRetry
            // 
            this.MenuRetry.Name = "MenuRetry";
            this.MenuRetry.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.MenuRetry.Size = new System.Drawing.Size(194, 22);
            this.MenuRetry.Text = "&Retry level";
            this.MenuRetry.Click += new System.EventHandler(this.MenuRetry_Click);
            // 
            // MenuSep2
            // 
            this.MenuSep2.Name = "MenuSep2";
            this.MenuSep2.Size = new System.Drawing.Size(191, 6);
            // 
            // MenuExit
            // 
            this.MenuExit.Name = "MenuExit";
            this.MenuExit.Size = new System.Drawing.Size(194, 22);
            this.MenuExit.Text = "E&xit";
            this.MenuExit.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // LevelToolNewLevel
            // 
            this.LevelToolNewLevel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolNewLevel.Image = ((System.Drawing.Image) (resources.GetObject("LevelToolNewLevel.Image")));
            this.LevelToolNewLevel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolNewLevel.Name = "LevelToolNewLevel";
            this.LevelToolNewLevel.Size = new System.Drawing.Size(23, 22);
            this.LevelToolNewLevel.Text = "Create new level";
            this.LevelToolNewLevel.Click += new System.EventHandler(this.MenuLevCreate_Click);
            // 
            // LevelToolDelete
            // 
            this.LevelToolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolDelete.Image = ((System.Drawing.Image) (resources.GetObject("LevelToolDelete.Image")));
            this.LevelToolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolDelete.Name = "LevelToolDelete";
            this.LevelToolDelete.Size = new System.Drawing.Size(23, 22);
            this.LevelToolDelete.Text = "Delete selected item";
            this.LevelToolDelete.Click += new System.EventHandler(this.LevelToolDelete_Click);
            // 
            // MainArea
            // 
            this.MainArea.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (255)))), ((int) (((byte) (255)))), ((int) (((byte) (206)))));
            this.MainArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainArea.Location = new System.Drawing.Point(0, 24);
            this.MainArea.Name = "MainArea";
            this.MainArea.Size = new System.Drawing.Size(627, 464);
            this.MainArea.TabIndex = 1;
            this.MainArea.MoveMade += new System.EventHandler(this.MainArea_MoveMade);
            // 
            // LevelList
            // 
            this.LevelList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LevelList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.LevelList.IntegralHeight = false;
            this.LevelList.Location = new System.Drawing.Point(0, 25);
            this.LevelList.Name = "LevelList";
            this.LevelList.ScrollAlwaysVisible = true;
            this.LevelList.Size = new System.Drawing.Size(200, 439);
            this.LevelList.TabIndex = 2;
            this.LevelList.DoubleClick += new System.EventHandler(this.LevelList_DoubleClick);
            this.LevelList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LevelList_KeyPress);
            this.LevelList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LevelList_KeyUp);
            // 
            // ESMainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 488);
            this.Controls.Add(this.MainArea);
            this.Controls.Add(this.LevelListSplitter);
            this.Controls.Add(this.LevelListPanel);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "ESMainform";
            this.Text = "Expert Sokoban";
            this.LevelListPanel.ResumeLayout(false);
            this.LevelListPanel.PerformLayout();
            this.LevelListToolStrip.ResumeLayout(false);
            this.LevelListToolStrip.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExpertSokoban.ESMainArea MainArea;
        private System.Windows.Forms.Panel LevelListPanel;
        private System.Windows.Forms.Splitter LevelListSplitter;
        private ExpertSokoban.ESLevelListBox LevelList;
        private System.Windows.Forms.ToolStrip LevelListToolStrip;
        private System.Windows.Forms.ToolStripButton LevelToolClose;
        private System.Windows.Forms.ToolStripButton LevelToolOpen;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem GameMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuOpen;
        private System.Windows.Forms.ToolStripMenuItem MenuShowList;
        private System.Windows.Forms.ToolStripSeparator MenuSep1;
        private System.Windows.Forms.ToolStripMenuItem MenuUndo;
        private System.Windows.Forms.ToolStripMenuItem MenuRetry;
        private System.Windows.Forms.ToolStripSeparator MenuSep2;
        private System.Windows.Forms.ToolStripMenuItem MenuExit;
        private System.Windows.Forms.ToolStripButton LevelToolComment;
        private System.Windows.Forms.ToolStripButton LevelToolClear;
        private System.Windows.Forms.ToolStripMenuItem MenuLevelsSub;
        private System.Windows.Forms.ToolStripMenuItem MenuLevCreate;
        private System.Windows.Forms.ToolStripMenuItem MenuLevAddComment;
        private System.Windows.Forms.ToolStripSeparator LevelToolSep;
        private System.Windows.Forms.ToolStripMenuItem MenuLevClear;
        private System.Windows.Forms.ToolStripButton LevelToolNewLevel;
        private System.Windows.Forms.ToolStripButton LevelToolDelete;
    }
}

