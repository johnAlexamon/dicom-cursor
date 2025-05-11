namespace DicomSenderApp;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.label1 = new System.Windows.Forms.Label();
        this.txtSourceAE = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.txtTargetAE = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.txtTargetIP = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.numTargetPort = new System.Windows.Forms.NumericUpDown();
        this.lblSelectedFile = new System.Windows.Forms.Label();
        this.btnSelectDicom = new System.Windows.Forms.Button();
        this.btnSendEcho = new System.Windows.Forms.Button();
        this.btnSendDicom = new System.Windows.Forms.Button();
        this.btnSaveConfig = new System.Windows.Forms.Button();
        this.txtLog = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.groupBox3 = new System.Windows.Forms.GroupBox();
        ((System.ComponentModel.ISupportInitialize)(this.numTargetPort)).BeginInit();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.groupBox3.SuspendLayout();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(16, 30);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(90, 20);
        this.label1.TabIndex = 0;
        this.label1.Text = "Source AE:";
        // 
        // txtSourceAE
        // 
        this.txtSourceAE.Location = new System.Drawing.Point(142, 27);
        this.txtSourceAE.Name = "txtSourceAE";
        this.txtSourceAE.Size = new System.Drawing.Size(200, 27);
        this.txtSourceAE.TabIndex = 1;
        this.txtSourceAE.Text = "MYSCU";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(16, 63);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(82, 20);
        this.label2.TabIndex = 2;
        this.label2.Text = "Target AE:";
        // 
        // txtTargetAE
        // 
        this.txtTargetAE.Location = new System.Drawing.Point(142, 60);
        this.txtTargetAE.Name = "txtTargetAE";
        this.txtTargetAE.Size = new System.Drawing.Size(200, 27);
        this.txtTargetAE.TabIndex = 3;
        this.txtTargetAE.Text = "PACS";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(16, 96);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(120, 20);
        this.label3.TabIndex = 4;
        this.label3.Text = "Target IP Address:";
        // 
        // txtTargetIP
        // 
        this.txtTargetIP.Location = new System.Drawing.Point(142, 93);
        this.txtTargetIP.Name = "txtTargetIP";
        this.txtTargetIP.Size = new System.Drawing.Size(200, 27);
        this.txtTargetIP.TabIndex = 5;
        this.txtTargetIP.Text = "127.0.0.1";
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(16, 129);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(91, 20);
        this.label4.TabIndex = 6;
        this.label4.Text = "Target Port:";
        // 
        // numTargetPort
        // 
        this.numTargetPort.Location = new System.Drawing.Point(142, 126);
        this.numTargetPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
        this.numTargetPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        this.numTargetPort.Name = "numTargetPort";
        this.numTargetPort.Size = new System.Drawing.Size(150, 27);
        this.numTargetPort.TabIndex = 7;
        this.numTargetPort.Value = new decimal(new int[] { 104, 0, 0, 0 });
        // 
        // lblSelectedFile
        // 
        this.lblSelectedFile.AutoSize = true;
        this.lblSelectedFile.Location = new System.Drawing.Point(142, 27);
        this.lblSelectedFile.Name = "lblSelectedFile";
        this.lblSelectedFile.Size = new System.Drawing.Size(125, 20);
        this.lblSelectedFile.TabIndex = 8;
        this.lblSelectedFile.Text = "No file selected";
        // 
        // btnSelectDicom
        // 
        this.btnSelectDicom.Location = new System.Drawing.Point(16, 24);
        this.btnSelectDicom.Name = "btnSelectDicom";
        this.btnSelectDicom.Size = new System.Drawing.Size(120, 29);
        this.btnSelectDicom.TabIndex = 9;
        this.btnSelectDicom.Text = "Select File...";
        this.btnSelectDicom.UseVisualStyleBackColor = true;
        this.btnSelectDicom.Click += new System.EventHandler(this.btnSelectDicom_Click);
        // 
        // btnSendEcho
        // 
        this.btnSendEcho.Location = new System.Drawing.Point(16, 59);
        this.btnSendEcho.Name = "btnSendEcho";
        this.btnSendEcho.Size = new System.Drawing.Size(160, 29);
        this.btnSendEcho.TabIndex = 10;
        this.btnSendEcho.Text = "Send Echo";
        this.btnSendEcho.UseVisualStyleBackColor = true;
        this.btnSendEcho.Click += new System.EventHandler(this.btnSendEcho_Click);
        // 
        // btnSendDicom
        // 
        this.btnSendDicom.Location = new System.Drawing.Point(182, 59);
        this.btnSendDicom.Name = "btnSendDicom";
        this.btnSendDicom.Size = new System.Drawing.Size(160, 29);
        this.btnSendDicom.TabIndex = 11;
        this.btnSendDicom.Text = "Send DICOM";
        this.btnSendDicom.UseVisualStyleBackColor = true;
        this.btnSendDicom.Click += new System.EventHandler(this.btnSendDicom_Click);
        // 
        // btnSaveConfig
        // 
        this.btnSaveConfig.Location = new System.Drawing.Point(348, 59);
        this.btnSaveConfig.Name = "btnSaveConfig";
        this.btnSaveConfig.Size = new System.Drawing.Size(160, 29);
        this.btnSaveConfig.TabIndex = 12;
        this.btnSaveConfig.Text = "Save Config";
        this.btnSaveConfig.UseVisualStyleBackColor = true;
        this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
        // 
        // txtLog
        // 
        this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.txtLog.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.txtLog.Location = new System.Drawing.Point(6, 26);
        this.txtLog.Multiline = true;
        this.txtLog.Name = "txtLog";
        this.txtLog.ReadOnly = true;
        this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        this.txtLog.Size = new System.Drawing.Size(518, 242);
        this.txtLog.TabIndex = 13;
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(16, 27);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(105, 20);
        this.label5.TabIndex = 14;
        this.label5.Text = "Selected File:";
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Controls.Add(this.txtSourceAE);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.txtTargetAE);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.txtTargetIP);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.numTargetPort);
        this.groupBox1.Location = new System.Drawing.Point(12, 12);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(358, 165);
        this.groupBox1.TabIndex = 15;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Configuration";
        // 
        // groupBox2
        // 
        this.groupBox2.Controls.Add(this.lblSelectedFile);
        this.groupBox2.Controls.Add(this.btnSelectDicom);
        this.groupBox2.Controls.Add(this.btnSendEcho);
        this.groupBox2.Controls.Add(this.btnSendDicom);
        this.groupBox2.Controls.Add(this.btnSaveConfig);
        this.groupBox2.Location = new System.Drawing.Point(12, 183);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(530, 100);
        this.groupBox2.TabIndex = 16;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "Operations";
        // 
        // groupBox3
        // 
        this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.groupBox3.Controls.Add(this.txtLog);
        this.groupBox3.Location = new System.Drawing.Point(12, 289);
        this.groupBox3.Name = "groupBox3";
        this.groupBox3.Size = new System.Drawing.Size(530, 274);
        this.groupBox3.TabIndex = 17;
        this.groupBox3.TabStop = false;
        this.groupBox3.Text = "Log";
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(554, 575);
        this.Controls.Add(this.groupBox3);
        this.Controls.Add(this.groupBox2);
        this.Controls.Add(this.groupBox1);
        this.MinimumSize = new System.Drawing.Size(570, 600);
        this.Name = "Form1";
        this.Text = "DICOM Sender";
        ((System.ComponentModel.ISupportInitialize)(this.numTargetPort)).EndInit();
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.groupBox3.ResumeLayout(false);
        this.groupBox3.PerformLayout();
        this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtSourceAE;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtTargetAE;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtTargetIP;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.NumericUpDown numTargetPort;
    private System.Windows.Forms.Label lblSelectedFile;
    private System.Windows.Forms.Button btnSelectDicom;
    private System.Windows.Forms.Button btnSendEcho;
    private System.Windows.Forms.Button btnSendDicom;
    private System.Windows.Forms.Button btnSaveConfig;
    private System.Windows.Forms.TextBox txtLog;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox3;
}
