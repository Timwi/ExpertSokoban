namespace ExpertSokoban
{
    partial class ChoosePlayerNameForm
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
            this.pnlLayout = new System.Windows.Forms.TableLayoutPanel();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.lblFlowButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblPrompt = new System.Windows.Forms.Label();
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.imgLanguage = new System.Windows.Forms.PictureBox();
            this.pnlLayout.SuspendLayout();
            this.lblFlowButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.imgLanguage)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLayout
            // 
            this.pnlLayout.AutoSize = true;
            this.pnlLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlLayout.ColumnCount = 2;
            this.pnlLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlLayout.Controls.Add(this.cmbLanguage, 1, 0);
            this.pnlLayout.Controls.Add(this.lblFlowButtons, 0, 3);
            this.pnlLayout.Controls.Add(this.lblPrompt, 0, 1);
            this.pnlLayout.Controls.Add(this.txtPlayerName, 0, 2);
            this.pnlLayout.Controls.Add(this.imgLanguage, 0, 0);
            this.pnlLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLayout.Location = new System.Drawing.Point(0, 0);
            this.pnlLayout.Name = "pnlLayout";
            this.pnlLayout.RowCount = 4;
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlLayout.Size = new System.Drawing.Size(638, 243);
            this.pnlLayout.TabIndex = 0;
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(52, 15);
            this.cmbLanguage.Margin = new System.Windows.Forms.Padding(0, 9, 9, 9);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(577, 23);
            this.cmbLanguage.TabIndex = 0;
            // 
            // lblFlowButtons
            // 
            this.lblFlowButtons.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFlowButtons.AutoSize = true;
            this.lblFlowButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlLayout.SetColumnSpan(this.lblFlowButtons, 2);
            this.lblFlowButtons.Controls.Add(this.btnCancel);
            this.lblFlowButtons.Controls.Add(this.btnOK);
            this.lblFlowButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.lblFlowButtons.Location = new System.Drawing.Point(0, 132);
            this.lblFlowButtons.Margin = new System.Windows.Forms.Padding(0);
            this.lblFlowButtons.Name = "lblFlowButtons";
            this.lblFlowButtons.Size = new System.Drawing.Size(638, 111);
            this.lblFlowButtons.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(524, 9);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Tag = "notranslate";
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(410, 9);
            this.btnOK.Margin = new System.Windows.Forms.Padding(9, 9, 0, 9);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(105, 27);
            this.btnOK.TabIndex = 0;
            this.btnOK.Tag = "notranslate";
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lblPrompt
            // 
            this.lblPrompt.AutoSize = true;
            this.pnlLayout.SetColumnSpan(this.lblPrompt, 2);
            this.lblPrompt.Location = new System.Drawing.Point(9, 61);
            this.lblPrompt.Margin = new System.Windows.Forms.Padding(9, 9, 9, 0);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(487, 30);
            this.lblPrompt.TabIndex = 4;
            this.lblPrompt.Tag = "notranslate";
            this.lblPrompt.Text = "Please choose a name which will be used to identify you in highscore tables.\r\nYou" +
                " can change this name later by selecting “Change player name” from the “Level” m" +
                "enu.";
            // 
            // txtPlayerName
            // 
            this.txtPlayerName.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLayout.SetColumnSpan(this.txtPlayerName, 2);
            this.txtPlayerName.Location = new System.Drawing.Point(9, 100);
            this.txtPlayerName.Margin = new System.Windows.Forms.Padding(9);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new System.Drawing.Size(620, 23);
            this.txtPlayerName.TabIndex = 1;
            // 
            // imgLanguage
            // 
            this.imgLanguage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.imgLanguage.Image = global::ExpertSokoban.Properties.Resources.LanguageIcon_Classic_32px;
            this.imgLanguage.Location = new System.Drawing.Point(10, 10);
            this.imgLanguage.Margin = new System.Windows.Forms.Padding(10);
            this.imgLanguage.Name = "imgLanguage";
            this.imgLanguage.Size = new System.Drawing.Size(32, 32);
            this.imgLanguage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgLanguage.TabIndex = 5;
            this.imgLanguage.TabStop = false;
            // 
            // ChoosePlayerNameForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(638, 243);
            this.Controls.Add(this.pnlLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::ExpertSokoban.Properties.Resources.ExpertSokoban;
            this.MaximizeBox = false;
            this.Name = "ChoosePlayerNameForm";
            this.Text = "Choose player name";
            this.pnlLayout.ResumeLayout(false);
            this.pnlLayout.PerformLayout();
            this.lblFlowButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.imgLanguage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlLayout;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.FlowLayoutPanel lblFlowButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblPrompt;
        private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.PictureBox imgLanguage;
    }
}