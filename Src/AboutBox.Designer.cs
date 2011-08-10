namespace ExpertSokoban
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.ctLogo = new System.Windows.Forms.PictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblURL = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlFlowRight = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCredits = new System.Windows.Forms.Label();
            this.lblRummage = new RT.Util.Controls.LabelEx();
            this.pnlFlowLeft = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize) (this.ctLogo)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlFlowRight.SuspendLayout();
            this.pnlFlowLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctLogo
            // 
            this.ctLogo.Image = global::ExpertSokoban.Properties.Resources.Skin_Sokoban;
            this.ctLogo.Location = new System.Drawing.Point(0, 0);
            this.ctLogo.Margin = new System.Windows.Forms.Padding(0);
            this.ctLogo.Name = "ctLogo";
            this.ctLogo.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.ctLogo.Size = new System.Drawing.Size(150, 160);
            this.ctLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ctLogo.TabIndex = 12;
            this.ctLogo.TabStop = false;
            this.ctLogo.Tag = "notranslate";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.lblVersion.Location = new System.Drawing.Point(6, 28);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(56, 17);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Tag = "notranslate";
            this.lblVersion.Text = "Version";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.lblCopyright.Location = new System.Drawing.Point(6, 55);
            this.lblCopyright.Margin = new System.Windows.Forms.Padding(6, 10, 6, 0);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(70, 17);
            this.lblCopyright.TabIndex = 21;
            this.lblCopyright.Tag = "notranslate";
            this.lblCopyright.Text = "Copyright";
            this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.btnOK.Location = new System.Drawing.Point(465, 371);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(129, 26);
            this.btnOK.TabIndex = 24;
            this.btnOK.Text = "OK";
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.lblProductName.Location = new System.Drawing.Point(6, 10);
            this.lblProductName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(108, 18);
            this.lblProductName.TabIndex = 20;
            this.lblProductName.Tag = "notranslate";
            this.lblProductName.Text = "Product Name";
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblURL.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.lblURL.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblURL.Location = new System.Drawing.Point(6, 72);
            this.lblURL.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(179, 15);
            this.lblURL.TabIndex = 26;
            this.lblURL.Tag = "notranslate";
            this.lblURL.Text = "http://www.cutebits.com/ExpSok";
            this.lblURL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblURL.Click += new System.EventHandler(this.clickUrl);
            // 
            // pnlMain
            // 
            this.pnlMain.AutoSize = true;
            this.pnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlMain.ColumnCount = 2;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlMain.Controls.Add(this.pnlFlowRight, 1, 0);
            this.pnlMain.Controls.Add(this.pnlFlowLeft, 0, 0);
            this.pnlMain.Controls.Add(this.btnOK, 1, 1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(10, 10);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 2;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlMain.Size = new System.Drawing.Size(594, 397);
            this.pnlMain.TabIndex = 27;
            this.pnlMain.Tag = "notranslate";
            // 
            // pnlFlowRight
            // 
            this.pnlFlowRight.AutoSize = true;
            this.pnlFlowRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlFlowRight.Controls.Add(this.lblProductName);
            this.pnlFlowRight.Controls.Add(this.lblVersion);
            this.pnlFlowRight.Controls.Add(this.lblCopyright);
            this.pnlFlowRight.Controls.Add(this.lblURL);
            this.pnlFlowRight.Controls.Add(this.lblCredits);
            this.pnlFlowRight.Controls.Add(this.lblRummage);
            this.pnlFlowRight.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlFlowRight.Font = new System.Drawing.Font("Arial", 9.75F);
            this.pnlFlowRight.Location = new System.Drawing.Point(159, 3);
            this.pnlFlowRight.Name = "pnlFlowRight";
            this.pnlFlowRight.Padding = new System.Windows.Forms.Padding(0, 10, 10, 10);
            this.pnlFlowRight.Size = new System.Drawing.Size(247, 197);
            this.pnlFlowRight.TabIndex = 29;
            // 
            // lblCredits
            // 
            this.lblCredits.AutoSize = true;
            this.lblCredits.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.lblCredits.Location = new System.Drawing.Point(6, 97);
            this.lblCredits.Margin = new System.Windows.Forms.Padding(6, 10, 6, 0);
            this.lblCredits.Name = "lblCredits";
            this.lblCredits.Size = new System.Drawing.Size(191, 64);
            this.lblCredits.TabIndex = 26;
            this.lblCredits.Text = "Credits:\r\n    Programming: Timwi, Roman\r\n    Graphics: Roman, Timwi\r\n    Testing:" +
                " Hawthorn";
            // 
            // lblRummage
            // 
            this.lblRummage.Font = new System.Drawing.Font("Arial", 9.75F);
            this.lblRummage.Location = new System.Drawing.Point(6, 171);
            this.lblRummage.Margin = new System.Windows.Forms.Padding(6, 10, 6, 0);
            this.lblRummage.Name = "lblRummage";
            this.lblRummage.Size = new System.Drawing.Size(225, 16);
            this.lblRummage.TabIndex = 27;
            this.lblRummage.TabStop = true;
            this.lblRummage.Text = "This game is protected by {Rummage}.";
            this.lblRummage.LinkActivated += new RT.Util.Controls.LinkEventHandler(this.clickRummageUrl);
            // 
            // pnlFlowLeft
            // 
            this.pnlFlowLeft.AutoSize = true;
            this.pnlFlowLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlFlowLeft.Controls.Add(this.ctLogo);
            this.pnlFlowLeft.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlFlowLeft.Font = new System.Drawing.Font("Arial", 9.75F);
            this.pnlFlowLeft.Location = new System.Drawing.Point(3, 3);
            this.pnlFlowLeft.Name = "pnlFlowLeft";
            this.pnlFlowLeft.Size = new System.Drawing.Size(150, 160);
            this.pnlFlowLeft.TabIndex = 28;
            // 
            // AboutBox
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(614, 417);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Expert Sokoban";
            ((System.ComponentModel.ISupportInitialize) (this.ctLogo)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlFlowRight.ResumeLayout(false);
            this.pnlFlowRight.PerformLayout();
            this.pnlFlowLeft.ResumeLayout(false);
            this.pnlFlowLeft.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ctLogo;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private System.Windows.Forms.Label lblCredits;
        private System.Windows.Forms.FlowLayoutPanel pnlFlowLeft;
        private System.Windows.Forms.FlowLayoutPanel pnlFlowRight;
        private RT.Util.Controls.LabelEx lblRummage;
    }
}
