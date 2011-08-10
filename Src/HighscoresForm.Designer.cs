namespace ExpertSokoban
{
    partial class HighscoresForm
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
            this.pnlHighscores = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.ctLevelPicture = new System.Windows.Forms.PictureBox();
            this.pnlHighscores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.ctLevelPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHighscores
            // 
            this.pnlHighscores.AutoSize = true;
            this.pnlHighscores.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlHighscores.ColumnCount = 1;
            this.pnlHighscores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlHighscores.Controls.Add(this.btnOK, 0, 0);
            this.pnlHighscores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHighscores.Location = new System.Drawing.Point(0, 0);
            this.pnlHighscores.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlHighscores.Name = "pnlHighscores";
            this.pnlHighscores.Padding = new System.Windows.Forms.Padding(6);
            this.pnlHighscores.RowCount = 1;
            this.pnlHighscores.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlHighscores.Size = new System.Drawing.Size(341, 336);
            this.pnlHighscores.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(234, 299);
            this.btnOK.Margin = new System.Windows.Forms.Padding(6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(95, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Tag = "notranslate";
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.okClick);
            // 
            // ctLevelPicture
            // 
            this.ctLevelPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctLevelPicture.Location = new System.Drawing.Point(0, 0);
            this.ctLevelPicture.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ctLevelPicture.Name = "ctLevelPicture";
            this.ctLevelPicture.Size = new System.Drawing.Size(233, 61);
            this.ctLevelPicture.TabIndex = 1;
            this.ctLevelPicture.TabStop = false;
            this.ctLevelPicture.Paint += new System.Windows.Forms.PaintEventHandler(this.paintLevel);
            // 
            // HighscoresForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(341, 336);
            this.Controls.Add(this.ctLevelPicture);
            this.Controls.Add(this.pnlHighscores);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HighscoresForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Highscores";
            this.pnlHighscores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.ctLevelPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlHighscores;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox ctLevelPicture;
    }
}