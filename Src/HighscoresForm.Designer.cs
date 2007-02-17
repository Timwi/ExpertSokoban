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
            this.HighscoresTable = new System.Windows.Forms.TableLayoutPanel();
            this.OKButton = new System.Windows.Forms.Button();
            this.LevelPicture = new System.Windows.Forms.PictureBox();
            this.HighscoresTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LevelPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // HighscoresTable
            // 
            this.HighscoresTable.AutoSize = true;
            this.HighscoresTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.HighscoresTable.ColumnCount = 1;
            this.HighscoresTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.HighscoresTable.Controls.Add(this.OKButton, 0, 0);
            this.HighscoresTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HighscoresTable.Location = new System.Drawing.Point(0, 0);
            this.HighscoresTable.Name = "HighscoresTable";
            this.HighscoresTable.Padding = new System.Windows.Forms.Padding(5);
            this.HighscoresTable.RowCount = 1;
            this.HighscoresTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.HighscoresTable.Size = new System.Drawing.Size(292, 273);
            this.HighscoresTable.TabIndex = 0;
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(182, 240);
            this.OKButton.Margin = new System.Windows.Forms.Padding(5);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(100, 23);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // LevelPicture
            // 
            this.LevelPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LevelPicture.Location = new System.Drawing.Point(0, 0);
            this.LevelPicture.Name = "LevelPicture";
            this.LevelPicture.Size = new System.Drawing.Size(200, 50);
            this.LevelPicture.TabIndex = 1;
            this.LevelPicture.TabStop = false;
            this.LevelPicture.Paint += new System.Windows.Forms.PaintEventHandler(this.LevelPicture_Paint);
            // 
            // HighscoresForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.LevelPicture);
            this.Controls.Add(this.HighscoresTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HighscoresForm";
            this.Text = "Highscores";
            this.HighscoresTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LevelPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel HighscoresTable;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.PictureBox LevelPicture;
    }
}