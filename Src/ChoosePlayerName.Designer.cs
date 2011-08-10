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
            this.lblLanguage = new System.Windows.Forms.Label();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.lblFlowButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblPrompt = new System.Windows.Forms.Label();
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.pnlLayout.SuspendLayout();
            this.lblFlowButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLayout
            // 
            this.pnlLayout.AutoSize = true;
            this.pnlLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlLayout.ColumnCount = 2;
            this.pnlLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlLayout.Controls.Add(this.lblLanguage, 0, 0);
            this.pnlLayout.Controls.Add(this.cmbLanguage, 1, 0);
            this.pnlLayout.Controls.Add(this.lblFlowButtons, 0, 3);
            this.pnlLayout.Controls.Add(this.lblPrompt, 0, 1);
            this.pnlLayout.Controls.Add(this.txtPlayerName, 0, 2);
            this.pnlLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLayout.Location = new System.Drawing.Point(0, 0);
            this.pnlLayout.Name = "pnlLayout";
            this.pnlLayout.RowCount = 4;
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlLayout.Size = new System.Drawing.Size(547, 211);
            this.pnlLayout.TabIndex = 0;
            // 
            // lblLanguage
            // 
            this.lblLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(8, 12);
            this.lblLanguage.Margin = new System.Windows.Forms.Padding(8, 8, 8, 32);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(173, 13);
            this.lblLanguage.TabIndex = 2;
            this.lblLanguage.Tag = "notranslate";
            this.lblLanguage.Text = "&Language/Sprache/Язык/Lingvo:";
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(197, 8);
            this.cmbLanguage.Margin = new System.Windows.Forms.Padding(8, 8, 8, 32);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(342, 21);
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
            this.lblFlowButtons.Location = new System.Drawing.Point(8, 147);
            this.lblFlowButtons.Margin = new System.Windows.Forms.Padding(8);
            this.lblFlowButtons.Name = "lblFlowButtons";
            this.lblFlowButtons.Size = new System.Drawing.Size(531, 56);
            this.lblFlowButtons.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(438, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Tag = "notranslate";
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(342, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Tag = "notranslate";
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lblPrompt
            // 
            this.lblPrompt.AutoSize = true;
            this.pnlLayout.SetColumnSpan(this.lblPrompt, 2);
            this.lblPrompt.Location = new System.Drawing.Point(8, 69);
            this.lblPrompt.Margin = new System.Windows.Forms.Padding(8);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(434, 26);
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
            this.txtPlayerName.Location = new System.Drawing.Point(8, 111);
            this.txtPlayerName.Margin = new System.Windows.Forms.Padding(8);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new System.Drawing.Size(531, 20);
            this.txtPlayerName.TabIndex = 1;
            // 
            // ChoosePlayerNameForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(547, 211);
            this.Controls.Add(this.pnlLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChoosePlayerNameForm";
            this.Text = "Choose player name";
            this.pnlLayout.ResumeLayout(false);
            this.pnlLayout.PerformLayout();
            this.lblFlowButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlLayout;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.FlowLayoutPanel lblFlowButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblPrompt;
        private System.Windows.Forms.TextBox txtPlayerName;
    }
}