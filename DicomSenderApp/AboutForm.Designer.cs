namespace DicomSenderApp
{
    partial class AboutForm
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
            lblTitle = new Label();
            lblVersion = new Label();
            lblCopyright = new Label();
            linkGitHub = new LinkLabel();
            btnOK = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(29, 28);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(304, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Alexamon DICOM Sender";
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Location = new Point(31, 72);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(94, 20);
            lblVersion.TabIndex = 1;
            lblVersion.Text = "Version: 1.1.0";
            // 
            // lblCopyright
            // 
            lblCopyright.AutoSize = true;
            lblCopyright.Location = new Point(31, 101);
            lblCopyright.Name = "lblCopyright";
            lblCopyright.Size = new Size(258, 20);
            lblCopyright.TabIndex = 2;
            lblCopyright.Text = "© 2025 Alexamon. All rights reserved.";
            lblCopyright.Click += lblCopyright_Click;
            // 
            // linkGitHub
            // 
            linkGitHub.AutoSize = true;
            linkGitHub.Location = new Point(31, 131);
            linkGitHub.Name = "linkGitHub";
            linkGitHub.Size = new Size(328, 20);
            linkGitHub.TabIndex = 3;
            linkGitHub.TabStop = true;
            linkGitHub.Text = "https://github.com/johnAlexamon/dicom-cursor";
            linkGitHub.LinkClicked += linkGitHub_LinkClicked;
            // 
            // btnOK
            // 
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(141, 176);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(94, 29);
            btnOK.TabIndex = 4;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // AboutForm
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(377, 226);
            Controls.Add(btnOK);
            Controls.Add(linkGitHub);
            Controls.Add(lblCopyright);
            Controls.Add(lblVersion);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "About";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.LinkLabel linkGitHub;
        private System.Windows.Forms.Button btnOK;
    }
} 