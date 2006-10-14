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
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.LevelIndicator = new System.Windows.Forms.ToolStripLabel();
            this.ToolPrevLevel = new System.Windows.Forms.ToolStripButton();
            this.ToolNextLevel = new System.Windows.Forms.ToolStripButton();
            this.ToolUndo = new System.Windows.Forms.ToolStripButton();
            this.ToolRetry = new System.Windows.Forms.ToolStripButton();
            this.ToolOpen = new System.Windows.Forms.ToolStripButton();
            this.ToolEdit = new System.Windows.Forms.ToolStripButton();
            this.ToolExit = new System.Windows.Forms.ToolStripButton();
            this.LevelListPanel = new System.Windows.Forms.Panel();
            this.LevelList = new ExpertSokoban.ESLevelListBox();
            this.LevelListToolStrip = new System.Windows.Forms.ToolStrip();
            this.LevelListClose = new System.Windows.Forms.ToolStripButton();
            this.LevelListSplitter = new System.Windows.Forms.Splitter();
            this.MainArea = new ExpertSokoban.ESMainArea();
            this.MainToolStrip.SuspendLayout();
            this.LevelListPanel.SuspendLayout();
            this.LevelListToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LevelIndicator,
            this.ToolPrevLevel,
            this.ToolNextLevel,
            this.ToolUndo,
            this.ToolRetry,
            this.ToolOpen,
            this.ToolEdit,
            this.ToolExit});
            this.MainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.Size = new System.Drawing.Size(655, 25);
            this.MainToolStrip.TabIndex = 0;
            this.MainToolStrip.Text = "Main";
            // 
            // LevelIndicator
            // 
            this.LevelIndicator.Name = "LevelIndicator";
            this.LevelIndicator.Size = new System.Drawing.Size(51, 22);
            this.LevelIndicator.Text = "Level 1/1";
            // 
            // ToolPrevLevel
            // 
            this.ToolPrevLevel.Image = ((System.Drawing.Image) (resources.GetObject("ToolPrevLevel.Image")));
            this.ToolPrevLevel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolPrevLevel.Name = "ToolPrevLevel";
            this.ToolPrevLevel.Size = new System.Drawing.Size(96, 22);
            this.ToolPrevLevel.Text = "&Previous Level";
            // 
            // ToolNextLevel
            // 
            this.ToolNextLevel.Image = ((System.Drawing.Image) (resources.GetObject("ToolNextLevel.Image")));
            this.ToolNextLevel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolNextLevel.Name = "ToolNextLevel";
            this.ToolNextLevel.Size = new System.Drawing.Size(78, 22);
            this.ToolNextLevel.Text = "&Next Level";
            // 
            // ToolUndo
            // 
            this.ToolUndo.Image = ((System.Drawing.Image) (resources.GetObject("ToolUndo.Image")));
            this.ToolUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolUndo.Name = "ToolUndo";
            this.ToolUndo.Size = new System.Drawing.Size(52, 22);
            this.ToolUndo.Text = "&Undo";
            this.ToolUndo.Click += new System.EventHandler(this.ToolUndo_Click);
            // 
            // ToolRetry
            // 
            this.ToolRetry.Image = ((System.Drawing.Image) (resources.GetObject("ToolRetry.Image")));
            this.ToolRetry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolRetry.Name = "ToolRetry";
            this.ToolRetry.Size = new System.Drawing.Size(82, 22);
            this.ToolRetry.Text = "&Retry Level";
            // 
            // ToolOpen
            // 
            this.ToolOpen.Image = ((System.Drawing.Image) (resources.GetObject("ToolOpen.Image")));
            this.ToolOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolOpen.Name = "ToolOpen";
            this.ToolOpen.Size = new System.Drawing.Size(112, 22);
            this.ToolOpen.Text = "&Open Level File...";
            this.ToolOpen.Click += new System.EventHandler(this.ToolOpen_Click);
            // 
            // ToolEdit
            // 
            this.ToolEdit.Image = ((System.Drawing.Image) (resources.GetObject("ToolEdit.Image")));
            this.ToolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolEdit.Name = "ToolEdit";
            this.ToolEdit.Size = new System.Drawing.Size(85, 22);
            this.ToolEdit.Text = "&Edit Level...";
            this.ToolEdit.Click += new System.EventHandler(this.ToolEdit_Click);
            // 
            // ToolExit
            // 
            this.ToolExit.Image = ((System.Drawing.Image) (resources.GetObject("ToolExit.Image")));
            this.ToolExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolExit.Name = "ToolExit";
            this.ToolExit.Size = new System.Drawing.Size(45, 22);
            this.ToolExit.Text = "E&xit";
            this.ToolExit.Click += new System.EventHandler(this.ToolExit_Click);
            // 
            // LevelListPanel
            // 
            this.LevelListPanel.BackColor = System.Drawing.SystemColors.Control;
            this.LevelListPanel.Controls.Add(this.LevelList);
            this.LevelListPanel.Controls.Add(this.LevelListToolStrip);
            this.LevelListPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.LevelListPanel.Location = new System.Drawing.Point(455, 25);
            this.LevelListPanel.Name = "LevelListPanel";
            this.LevelListPanel.Size = new System.Drawing.Size(200, 421);
            this.LevelListPanel.TabIndex = 6;
            this.LevelListPanel.Visible = false;
            // 
            // LevelList
            // 
            this.LevelList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LevelList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.LevelList.FormattingEnabled = true;
            this.LevelList.IntegralHeight = false;
            this.LevelList.Location = new System.Drawing.Point(0, 25);
            this.LevelList.Name = "LevelList";
            this.LevelList.Size = new System.Drawing.Size(200, 396);
            this.LevelList.TabIndex = 2;
            // 
            // LevelListToolStrip
            // 
            this.LevelListToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LevelListClose});
            this.LevelListToolStrip.Location = new System.Drawing.Point(0, 0);
            this.LevelListToolStrip.Name = "LevelListToolStrip";
            this.LevelListToolStrip.Size = new System.Drawing.Size(200, 25);
            this.LevelListToolStrip.TabIndex = 0;
            this.LevelListToolStrip.Text = "toolStrip1";
            // 
            // LevelListClose
            // 
            this.LevelListClose.Image = ((System.Drawing.Image) (resources.GetObject("LevelListClose.Image")));
            this.LevelListClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LevelListClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelListClose.Name = "LevelListClose";
            this.LevelListClose.Size = new System.Drawing.Size(45, 22);
            this.LevelListClose.Text = "&Close";
            this.LevelListClose.Click += new System.EventHandler(this.LevelListClose_Click);
            // 
            // LevelListSplitter
            // 
            this.LevelListSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.LevelListSplitter.Location = new System.Drawing.Point(452, 25);
            this.LevelListSplitter.Name = "LevelListSplitter";
            this.LevelListSplitter.Size = new System.Drawing.Size(3, 421);
            this.LevelListSplitter.TabIndex = 7;
            this.LevelListSplitter.TabStop = false;
            this.LevelListSplitter.Visible = false;
            // 
            // MainArea
            // 
            this.MainArea.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (255)))), ((int) (((byte) (255)))), ((int) (((byte) (206)))));
            this.MainArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainArea.Location = new System.Drawing.Point(0, 25);
            this.MainArea.Name = "MainArea";
            this.MainArea.Size = new System.Drawing.Size(452, 421);
            this.MainArea.TabIndex = 1;
            // 
            // ESMainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 446);
            this.Controls.Add(this.MainArea);
            this.Controls.Add(this.LevelListSplitter);
            this.Controls.Add(this.LevelListPanel);
            this.Controls.Add(this.MainToolStrip);
            this.Name = "ESMainform";
            this.Text = "Expert Sokoban";
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.LevelListPanel.ResumeLayout(false);
            this.LevelListPanel.PerformLayout();
            this.LevelListToolStrip.ResumeLayout(false);
            this.LevelListToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.ToolStripButton ToolPrevLevel;
        private System.Windows.Forms.ToolStripButton ToolNextLevel;
        private System.Windows.Forms.ToolStripButton ToolUndo;
        private System.Windows.Forms.ToolStripButton ToolRetry;
        private System.Windows.Forms.ToolStripButton ToolOpen;
        private System.Windows.Forms.ToolStripLabel LevelIndicator;
        private System.Windows.Forms.ToolStripButton ToolEdit;
        private System.Windows.Forms.ToolStripButton ToolExit;
        private ExpertSokoban.ESMainArea MainArea;
        private System.Windows.Forms.Panel LevelListPanel;
        private System.Windows.Forms.Splitter LevelListSplitter;
        private ExpertSokoban.ESLevelListBox LevelList;
        private System.Windows.Forms.ToolStrip LevelListToolStrip;
        private System.Windows.Forms.ToolStripButton LevelListClose;
    }
}

