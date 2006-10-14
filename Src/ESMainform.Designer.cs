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
            this.MainArea = new ExpertSokoban.ESMainArea();
            this.MainToolStrip.SuspendLayout();
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
            // 
            // ToolEdit
            // 
            this.ToolEdit.Image = ((System.Drawing.Image) (resources.GetObject("ToolEdit.Image")));
            this.ToolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolEdit.Name = "ToolEdit";
            this.ToolEdit.Size = new System.Drawing.Size(85, 22);
            this.ToolEdit.Text = "&Edit Level...";
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
            // MainArea
            // 
            this.MainArea.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (255)))), ((int) (((byte) (255)))), ((int) (((byte) (206)))));
            this.MainArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainArea.Location = new System.Drawing.Point(0, 25);
            this.MainArea.Name = "MainArea";
            this.MainArea.Size = new System.Drawing.Size(655, 421);
            this.MainArea.TabIndex = 1;
            // 
            // ESMainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 446);
            this.Controls.Add(this.MainArea);
            this.Controls.Add(this.MainToolStrip);
            this.Name = "ESMainform";
            this.Text = "Expert Sokoban";
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
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
    }
}

