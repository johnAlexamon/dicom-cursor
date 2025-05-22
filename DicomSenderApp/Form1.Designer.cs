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
        this.groupBoxTags = new System.Windows.Forms.GroupBox();
        this.chkModifyTags = new System.Windows.Forms.CheckBox();
        this.label6 = new System.Windows.Forms.Label();
        this.txtPatientName = new System.Windows.Forms.TextBox();
        this.label7 = new System.Windows.Forms.Label();
        this.txtPatientID = new System.Windows.Forms.TextBox();
        this.label8 = new System.Windows.Forms.Label();
        this.txtStudyUID = new System.Windows.Forms.TextBox();
        this.label9 = new System.Windows.Forms.Label();
        this.txtSeriesUID = new System.Windows.Forms.TextBox();
        this.label10 = new System.Windows.Forms.Label();
        this.txtSOPInstanceUID = new System.Windows.Forms.TextBox();
        this.btnGenerateUIDs = new System.Windows.Forms.Button();
        this.label11 = new System.Windows.Forms.Label();
        this.txtConfidentialityCode = new System.Windows.Forms.TextBox();
        this.btnAbout = new System.Windows.Forms.Button();
        this.tabControl = new System.Windows.Forms.TabControl();
        this.tabPageSend = new System.Windows.Forms.TabPage();
        this.tabPageWorklist = new System.Windows.Forms.TabPage();
        this.groupBoxWorklistQuery = new System.Windows.Forms.GroupBox();
        this.label12 = new System.Windows.Forms.Label();
        this.txtWorklistPatientName = new System.Windows.Forms.TextBox();
        this.label13 = new System.Windows.Forms.Label();
        this.txtWorklistPatientID = new System.Windows.Forms.TextBox();
        this.label14 = new System.Windows.Forms.Label();
        this.txtAccessionNumber = new System.Windows.Forms.TextBox();
        this.label15 = new System.Windows.Forms.Label();
        this.dateTimeScheduled = new System.Windows.Forms.DateTimePicker();
        this.chkUseDate = new System.Windows.Forms.CheckBox();
        this.btnQueryWorklist = new System.Windows.Forms.Button();
        this.dataGridWorklist = new System.Windows.Forms.DataGridView();
        this.chkDebugLogging = new System.Windows.Forms.CheckBox();
        this.tabPageCFind = new System.Windows.Forms.TabPage();
        this.txtCFindPatientName = new System.Windows.Forms.TextBox();
        this.txtCFindPatientID = new System.Windows.Forms.TextBox();
        this.txtCFindAccessionNumber = new System.Windows.Forms.TextBox();
        this.btnQueryCFind = new System.Windows.Forms.Button();
        this.dataGridCFind = new System.Windows.Forms.DataGridView();
        this.btnGenerateStudyUID = new System.Windows.Forms.Button();
        this.btnGenerateSeriesUID = new System.Windows.Forms.Button();
        this.btnGenerateSOPUID = new System.Windows.Forms.Button();
        this.btnToggleLog = new System.Windows.Forms.Button();
        this.splitterLog = new System.Windows.Forms.Splitter();
        this.btnSaveDicomTags = new System.Windows.Forms.Button();
        this.tabPageLog = new System.Windows.Forms.TabPage();
        this.btnDicomDump = new System.Windows.Forms.Button();
        this.lblRejectionNotesHeader = new System.Windows.Forms.Label();
        this.lblCodeMeaning = new System.Windows.Forms.Label();
        this.txtCodeMeaning = new System.Windows.Forms.TextBox();
        this.lblRefSOPInstanceUID = new System.Windows.Forms.Label();
        this.txtRefSOPInstanceUID = new System.Windows.Forms.TextBox();
        this.lblRefSeriesUID = new System.Windows.Forms.Label();
        this.txtRefSeriesUID = new System.Windows.Forms.TextBox();
        this.lblRefStudyUID = new System.Windows.Forms.Label();
        this.txtRefStudyUID = new System.Windows.Forms.TextBox();
        ((System.ComponentModel.ISupportInitialize)(this.numTargetPort)).BeginInit();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.groupBoxTags.SuspendLayout();
        this.tabControl.SuspendLayout();
        this.tabPageSend.SuspendLayout();
        this.tabPageWorklist.SuspendLayout();
        this.groupBoxWorklistQuery.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridWorklist)).BeginInit();
        this.tabPageCFind.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridCFind)).BeginInit();
        this.tabPageLog.SuspendLayout();
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
        // btnDicomDump
        // 
        this.btnDicomDump = new System.Windows.Forms.Button();
        this.btnDicomDump.Location = new System.Drawing.Point(514, 59);
        this.btnDicomDump.Name = "btnDicomDump";
        this.btnDicomDump.Size = new System.Drawing.Size(160, 29);
        this.btnDicomDump.TabIndex = 13;
        this.btnDicomDump.Text = "DICOM Dump";
        this.btnDicomDump.UseVisualStyleBackColor = true;
        this.btnDicomDump.Click += new System.EventHandler(this.btnDicomDump_Click);

        // 
        // txtLog
        // 
        this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
        this.txtLog.Location = new System.Drawing.Point(3, 3);
        this.txtLog.Size = new System.Drawing.Size(786, 561);
        this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.txtLog.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.txtLog.Multiline = true;
        this.txtLog.Name = "txtLog";
        this.txtLog.ReadOnly = true;
        this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
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
        this.groupBox2.Controls.Add(this.btnDicomDump);
        this.groupBox2.Location = new System.Drawing.Point(12, 183);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(730, 100);
        this.groupBox2.TabIndex = 16;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "Operations";
        // 
        // groupBoxTags
        // 
        this.groupBoxTags.Controls.Clear();
        this.groupBoxTags.Controls.Add(this.chkModifyTags);
        this.groupBoxTags.Controls.Add(this.btnGenerateUIDs);
        this.groupBoxTags.Controls.Add(this.btnSaveDicomTags);
        this.groupBoxTags.Controls.Add(this.label6);
        this.groupBoxTags.Controls.Add(this.txtPatientName);
        this.groupBoxTags.Controls.Add(this.label7);
        this.groupBoxTags.Controls.Add(this.txtPatientID);
        this.groupBoxTags.Controls.Add(this.label8);
        this.groupBoxTags.Controls.Add(this.txtStudyUID);
        this.groupBoxTags.Controls.Add(this.btnGenerateStudyUID);
        this.groupBoxTags.Controls.Add(this.label9);
        this.groupBoxTags.Controls.Add(this.txtSeriesUID);
        this.groupBoxTags.Controls.Add(this.btnGenerateSeriesUID);
        this.groupBoxTags.Controls.Add(this.label10);
        this.groupBoxTags.Controls.Add(this.txtSOPInstanceUID);
        this.groupBoxTags.Controls.Add(this.btnGenerateSOPUID);
        this.groupBoxTags.Controls.Add(this.label11);
        this.groupBoxTags.Controls.Add(this.txtConfidentialityCode);
        this.groupBoxTags.Controls.Add(this.lblRejectionNotesHeader);
        this.groupBoxTags.Controls.Add(this.lblCodeMeaning);
        this.groupBoxTags.Controls.Add(this.txtCodeMeaning);
        this.groupBoxTags.Controls.Add(this.lblRefSOPInstanceUID);
        this.groupBoxTags.Controls.Add(this.txtRefSOPInstanceUID);
        this.groupBoxTags.Controls.Add(this.lblRefSeriesUID);
        this.groupBoxTags.Controls.Add(this.txtRefSeriesUID);
        this.groupBoxTags.Controls.Add(this.lblRefStudyUID);
        this.groupBoxTags.Controls.Add(this.txtRefStudyUID);
        this.groupBoxTags.Location = new System.Drawing.Point(12, 289);
        this.groupBoxTags.Name = "groupBoxTags";
        this.groupBoxTags.Size = new System.Drawing.Size(760, 334);
        this.groupBoxTags.TabIndex = 18;
        this.groupBoxTags.TabStop = false;
        this.groupBoxTags.Text = "Modify DICOM Tags";
        this.groupBoxTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        // 
        // chkModifyTags
        // 
        this.chkModifyTags.AutoSize = true;
        this.chkModifyTags.Location = new System.Drawing.Point(16, 26);
        this.chkModifyTags.Name = "chkModifyTags";
        this.chkModifyTags.Size = new System.Drawing.Size(186, 24);
        this.chkModifyTags.TabIndex = 0;
        this.chkModifyTags.Text = "Modify tags when sending";
        this.chkModifyTags.UseVisualStyleBackColor = true;
        this.chkModifyTags.CheckedChanged += new System.EventHandler(this.chkModifyTags_CheckedChanged);
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(16, 60);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(102, 20);
        this.label6.TabIndex = 1;
        this.label6.Text = "Patient Name:";
        // 
        // txtPatientName
        // 
        this.txtPatientName.Enabled = false;
        this.txtPatientName.Location = new System.Drawing.Point(142, 57);
        this.txtPatientName.Name = "txtPatientName";
        this.txtPatientName.Size = new System.Drawing.Size(200, 27);
        this.txtPatientName.TabIndex = 2;
        // 
        // label7
        // 
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(16, 93);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(77, 20);
        this.label7.TabIndex = 3;
        this.label7.Text = "Patient ID:";
        // 
        // txtPatientID
        // 
        this.txtPatientID.Enabled = false;
        this.txtPatientID.Location = new System.Drawing.Point(142, 90);
        this.txtPatientID.Name = "txtPatientID";
        this.txtPatientID.Size = new System.Drawing.Size(200, 27);
        this.txtPatientID.TabIndex = 4;
        // 
        // label8
        // 
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(16, 126);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(79, 20);
        this.label8.TabIndex = 5;
        this.label8.Text = "Study UID:";
        // 
        // txtStudyUID
        // 
        this.txtStudyUID.Enabled = false;
        this.txtStudyUID.Location = new System.Drawing.Point(142, 123);
        this.txtStudyUID.Name = "txtStudyUID";
        this.txtStudyUID.Size = new System.Drawing.Size(428, 27);
        this.txtStudyUID.TabIndex = 6;
        // 
        // label9
        // 
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(16, 159);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(81, 20);
        this.label9.TabIndex = 7;
        this.label9.Text = "Series UID:";
        // 
        // txtSeriesUID
        // 
        this.txtSeriesUID.Enabled = false;
        this.txtSeriesUID.Location = new System.Drawing.Point(142, 156);
        this.txtSeriesUID.Name = "txtSeriesUID";
        this.txtSeriesUID.Size = new System.Drawing.Size(428, 27);
        this.txtSeriesUID.TabIndex = 8;
        // 
        // label10
        // 
        this.label10.AutoSize = true;
        this.label10.Location = new System.Drawing.Point(16, 192);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(128, 20);
        this.label10.TabIndex = 9;
        this.label10.Text = "SOP Instance UID:";
        // 
        // txtSOPInstanceUID
        // 
        this.txtSOPInstanceUID.Enabled = false;
        this.txtSOPInstanceUID.Location = new System.Drawing.Point(142, 189);
        this.txtSOPInstanceUID.Name = "txtSOPInstanceUID";
        this.txtSOPInstanceUID.Size = new System.Drawing.Size(428, 27);
        this.txtSOPInstanceUID.TabIndex = 10;
        // 
        // btnGenerateUIDs
        // 
        this.btnGenerateUIDs.Enabled = false;
        this.btnGenerateUIDs.Location = new System.Drawing.Point(210, 26);
        this.btnGenerateUIDs.Name = "btnGenerateUIDs";
        this.btnGenerateUIDs.Size = new System.Drawing.Size(132, 29);
        this.btnGenerateUIDs.TabIndex = 11;
        this.btnGenerateUIDs.Text = "Generate UIDs";
        this.btnGenerateUIDs.UseVisualStyleBackColor = true;
        this.btnGenerateUIDs.Click += new System.EventHandler(this.btnGenerateUIDs_Click);
        // 
        // label11
        // 
        this.label11.AutoSize = true;
        this.label11.Location = new System.Drawing.Point(350, 60);
        this.label11.Name = "label11";
        this.label11.Size = new System.Drawing.Size(141, 20);
        this.label11.TabIndex = 12;
        this.label11.Text = "Confidentiality Code:";
        // 
        // txtConfidentialityCode
        // 
        this.txtConfidentialityCode.Enabled = false;
        this.txtConfidentialityCode.Location = new System.Drawing.Point(497, 57);
        this.txtConfidentialityCode.Name = "txtConfidentialityCode";
        this.txtConfidentialityCode.Size = new System.Drawing.Size(273, 27);
        this.txtConfidentialityCode.TabIndex = 13;
        // 
        // lblRejectionNotesHeader
        // 
        this.lblRejectionNotesHeader.AutoSize = true;
        this.lblRejectionNotesHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblRejectionNotesHeader.Location = new System.Drawing.Point(16, 225);
        this.lblRejectionNotesHeader.Name = "lblRejectionNotesHeader";
        this.lblRejectionNotesHeader.Size = new System.Drawing.Size(153, 20);
        this.lblRejectionNotesHeader.TabIndex = 14;
        this.lblRejectionNotesHeader.Text = "Rejection Note Tags:";
        // 
        // lblCodeMeaning
        // 
        this.lblCodeMeaning.AutoSize = true;
        this.lblCodeMeaning.Location = new System.Drawing.Point(16, 258);
        this.lblCodeMeaning.Name = "lblCodeMeaning";
        this.lblCodeMeaning.Size = new System.Drawing.Size(107, 20);
        this.lblCodeMeaning.TabIndex = 15;
        this.lblCodeMeaning.Text = "Code Meaning:";
        // 
        // txtCodeMeaning
        // 
        this.txtCodeMeaning.Enabled = false;
        this.txtCodeMeaning.Location = new System.Drawing.Point(142, 255);
        this.txtCodeMeaning.Name = "txtCodeMeaning";
        this.txtCodeMeaning.Size = new System.Drawing.Size(200, 27);
        this.txtCodeMeaning.TabIndex = 16;
        // 
        // lblRefSOPInstanceUID
        // 
        this.lblRefSOPInstanceUID.AutoSize = true;
        this.lblRefSOPInstanceUID.Location = new System.Drawing.Point(350, 258);
        this.lblRefSOPInstanceUID.Name = "lblRefSOPInstanceUID";
        this.lblRefSOPInstanceUID.Size = new System.Drawing.Size(136, 20);
        this.lblRefSOPInstanceUID.TabIndex = 17;
        this.lblRefSOPInstanceUID.Text = "Ref. SOP Inst. UID:";
        // 
        // txtRefSOPInstanceUID
        // 
        this.txtRefSOPInstanceUID.Enabled = false;
        this.txtRefSOPInstanceUID.Location = new System.Drawing.Point(497, 255);
        this.txtRefSOPInstanceUID.Name = "txtRefSOPInstanceUID";
        this.txtRefSOPInstanceUID.Size = new System.Drawing.Size(273, 27);
        this.txtRefSOPInstanceUID.TabIndex = 18;
        // 
        // lblRefSeriesUID
        // 
        this.lblRefSeriesUID.AutoSize = true;
        this.lblRefSeriesUID.Location = new System.Drawing.Point(16, 291);
        this.lblRefSeriesUID.Name = "lblRefSeriesUID";
        this.lblRefSeriesUID.Size = new System.Drawing.Size(89, 20);
        this.lblRefSeriesUID.TabIndex = 19;
        this.lblRefSeriesUID.Text = "Ref. Series:";
        // 
        // txtRefSeriesUID
        // 
        this.txtRefSeriesUID.Enabled = false;
        this.txtRefSeriesUID.Location = new System.Drawing.Point(142, 288);
        this.txtRefSeriesUID.Name = "txtRefSeriesUID";
        this.txtRefSeriesUID.Size = new System.Drawing.Size(428, 27);
        this.txtRefSeriesUID.TabIndex = 20;
        // 
        // lblRefStudyUID
        // 
        this.lblRefStudyUID.AutoSize = true;
        this.lblRefStudyUID.Location = new System.Drawing.Point(16, 324);
        this.lblRefStudyUID.Name = "lblRefStudyUID";
        this.lblRefStudyUID.Size = new System.Drawing.Size(87, 20);
        this.lblRefStudyUID.TabIndex = 21;
        this.lblRefStudyUID.Text = "Ref. Study:";
        // 
        // txtRefStudyUID
        // 
        this.txtRefStudyUID.Enabled = false;
        this.txtRefStudyUID.Location = new System.Drawing.Point(142, 321);
        this.txtRefStudyUID.Name = "txtRefStudyUID";
        this.txtRefStudyUID.Size = new System.Drawing.Size(428, 27);
        this.txtRefStudyUID.TabIndex = 22;
        // 
        // btnAbout
        // 
        this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btnAbout.Location = new System.Drawing.Point(696, 12);
        this.btnAbout.Name = "btnAbout";
        this.btnAbout.Size = new System.Drawing.Size(96, 29);
        this.btnAbout.TabIndex = 19;
        this.btnAbout.Text = "About";
        this.btnAbout.UseVisualStyleBackColor = true;
        this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
        // 
        // tabControl
        // 
        this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.tabControl.Controls.Add(this.tabPageSend);
        this.tabControl.Controls.Add(this.tabPageWorklist);
        this.tabControl.Controls.Add(this.tabPageCFind);
        this.tabControl.Controls.Add(this.tabPageLog);
        this.tabControl.Location = new System.Drawing.Point(12, 47);
        this.tabControl.Name = "tabControl";
        this.tabControl.SelectedIndex = 0;
        this.tabControl.Size = new System.Drawing.Size(780, 666);
        this.tabControl.TabIndex = 20;
        // 
        // tabPageSend
        // 
        this.tabPageSend.Controls.Add(this.groupBoxTags);
        this.tabPageSend.Controls.Add(this.groupBox2);
        this.tabPageSend.Controls.Add(this.groupBox1);
        this.tabPageSend.Location = new System.Drawing.Point(4, 29);
        this.tabPageSend.Name = "tabPageSend";
        this.tabPageSend.Padding = new System.Windows.Forms.Padding(3);
        this.tabPageSend.Size = new System.Drawing.Size(772, 633);
        this.tabPageSend.TabIndex = 0;
        this.tabPageSend.Text = "DICOM Send";
        this.tabPageSend.UseVisualStyleBackColor = true;
        // 
        // tabPageWorklist
        // 
        this.tabPageWorklist.Controls.Add(this.groupBoxWorklistQuery);
        this.tabPageWorklist.Controls.Add(this.dataGridWorklist);
        this.tabPageWorklist.Location = new System.Drawing.Point(4, 29);
        this.tabPageWorklist.Name = "tabPageWorklist";
        this.tabPageWorklist.Padding = new System.Windows.Forms.Padding(3);
        this.tabPageWorklist.Size = new System.Drawing.Size(792, 567);
        this.tabPageWorklist.TabIndex = 1;
        this.tabPageWorklist.Text = "Modality Worklist";
        this.tabPageWorklist.UseVisualStyleBackColor = true;
        // 
        // groupBoxWorklistQuery
        // 
        this.groupBoxWorklistQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.groupBoxWorklistQuery.Controls.Add(this.chkDebugLogging);
        this.groupBoxWorklistQuery.Controls.Add(this.btnQueryWorklist);
        this.groupBoxWorklistQuery.Controls.Add(this.chkUseDate);
        this.groupBoxWorklistQuery.Controls.Add(this.dateTimeScheduled);
        this.groupBoxWorklistQuery.Controls.Add(this.label15);
        this.groupBoxWorklistQuery.Controls.Add(this.txtAccessionNumber);
        this.groupBoxWorklistQuery.Controls.Add(this.label14);
        this.groupBoxWorklistQuery.Controls.Add(this.txtWorklistPatientID);
        this.groupBoxWorklistQuery.Controls.Add(this.label13);
        this.groupBoxWorklistQuery.Controls.Add(this.txtWorklistPatientName);
        this.groupBoxWorklistQuery.Controls.Add(this.label12);
        this.groupBoxWorklistQuery.Location = new System.Drawing.Point(6, 6);
        this.groupBoxWorklistQuery.Name = "groupBoxWorklistQuery";
        this.groupBoxWorklistQuery.Size = new System.Drawing.Size(760, 201);
        this.groupBoxWorklistQuery.TabIndex = 0;
        this.groupBoxWorklistQuery.TabStop = false;
        this.groupBoxWorklistQuery.Text = "Worklist Query Parameters";
        // 
        // label12
        // 
        this.label12.AutoSize = true;
        this.label12.Location = new System.Drawing.Point(14, 36);
        this.label12.Name = "label12";
        this.label12.Size = new System.Drawing.Size(102, 20);
        this.label12.TabIndex = 0;
        this.label12.Text = "Patient Name:";
        // 
        // txtWorklistPatientName
        // 
        this.txtWorklistPatientName.Location = new System.Drawing.Point(198, 33);
        this.txtWorklistPatientName.Name = "txtWorklistPatientName";
        this.txtWorklistPatientName.Size = new System.Drawing.Size(311, 27);
        this.txtWorklistPatientName.TabIndex = 1;
        // 
        // label13
        // 
        this.label13.AutoSize = true;
        this.label13.Location = new System.Drawing.Point(14, 69);
        this.label13.Name = "label13";
        this.label13.Size = new System.Drawing.Size(77, 20);
        this.label13.TabIndex = 2;
        this.label13.Text = "Patient ID:";
        // 
        // txtWorklistPatientID
        // 
        this.txtWorklistPatientID.Location = new System.Drawing.Point(198, 66);
        this.txtWorklistPatientID.Name = "txtWorklistPatientID";
        this.txtWorklistPatientID.Size = new System.Drawing.Size(311, 27);
        this.txtWorklistPatientID.TabIndex = 3;
        // 
        // label14
        // 
        this.label14.AutoSize = true;
        this.label14.Location = new System.Drawing.Point(14, 102);
        this.label14.Name = "label14";
        this.label14.Size = new System.Drawing.Size(140, 20);
        this.label14.TabIndex = 4;
        this.label14.Text = "Accession Number:";
        // 
        // txtAccessionNumber
        // 
        this.txtAccessionNumber.Location = new System.Drawing.Point(198, 99);
        this.txtAccessionNumber.Name = "txtAccessionNumber";
        this.txtAccessionNumber.Size = new System.Drawing.Size(311, 27);
        this.txtAccessionNumber.TabIndex = 5;
        // 
        // label15
        // 
        this.label15.AutoSize = true;
        this.label15.Location = new System.Drawing.Point(14, 135);
        this.label15.Name = "label15";
        this.label15.Size = new System.Drawing.Size(178, 20);
        this.label15.TabIndex = 6;
        this.label15.Text = "Scheduled Procedure Date:";
        // 
        // dateTimeScheduled
        // 
        this.dateTimeScheduled.Enabled = false;
        this.dateTimeScheduled.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        this.dateTimeScheduled.Location = new System.Drawing.Point(198, 132);
        this.dateTimeScheduled.Name = "dateTimeScheduled";
        this.dateTimeScheduled.Size = new System.Drawing.Size(163, 27);
        this.dateTimeScheduled.TabIndex = 7;
        // 
        // chkUseDate
        // 
        this.chkUseDate.AutoSize = true;
        this.chkUseDate.Location = new System.Drawing.Point(367, 134);
        this.chkUseDate.Name = "chkUseDate";
        this.chkUseDate.Size = new System.Drawing.Size(142, 24);
        this.chkUseDate.TabIndex = 8;
        this.chkUseDate.Text = "Include date filter";
        this.chkUseDate.UseVisualStyleBackColor = true;
        this.chkUseDate.CheckedChanged += new System.EventHandler(this.chkUseDate_CheckedChanged);
        // 
        // btnQueryWorklist
        // 
        this.btnQueryWorklist.Location = new System.Drawing.Point(198, 165);
        this.btnQueryWorklist.Name = "btnQueryWorklist";
        this.btnQueryWorklist.Size = new System.Drawing.Size(163, 29);
        this.btnQueryWorklist.TabIndex = 9;
        this.btnQueryWorklist.Text = "Query Worklist";
        this.btnQueryWorklist.UseVisualStyleBackColor = true;
        this.btnQueryWorklist.Click += new System.EventHandler(this.btnQueryWorklist_Click);
        // 
        // dataGridWorklist
        // 
        this.dataGridWorklist.AllowUserToAddRows = false;
        this.dataGridWorklist.AllowUserToDeleteRows = false;
        this.dataGridWorklist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.dataGridWorklist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.dataGridWorklist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridWorklist.Location = new System.Drawing.Point(6, 213);
        this.dataGridWorklist.Name = "dataGridWorklist";
        this.dataGridWorklist.ReadOnly = true;
        this.dataGridWorklist.RowHeadersWidth = 51;
        this.dataGridWorklist.RowTemplate.Height = 29;
        this.dataGridWorklist.Size = new System.Drawing.Size(760, 414);
        this.dataGridWorklist.TabIndex = 1;
        // 
        // chkDebugLogging
        // 
        this.chkDebugLogging.AutoSize = true;
        this.chkDebugLogging.Location = new System.Drawing.Point(367, 165);
        this.chkDebugLogging.Name = "chkDebugLogging";
        this.chkDebugLogging.Size = new System.Drawing.Size(188, 24);
        this.chkDebugLogging.TabIndex = 10;
        this.chkDebugLogging.Text = "Enable detailed logging";
        this.chkDebugLogging.UseVisualStyleBackColor = true;
        this.chkDebugLogging.CheckedChanged += new System.EventHandler(this.chkDebugLogging_CheckedChanged);
        // 
        // tabPageCFind
        // 
        this.tabPageCFind.Location = new System.Drawing.Point(4, 29);
        this.tabPageCFind.Name = "tabPageCFind";
        this.tabPageCFind.Padding = new System.Windows.Forms.Padding(3);
        this.tabPageCFind.Size = new System.Drawing.Size(792, 567);
        this.tabPageCFind.TabIndex = 2;
        this.tabPageCFind.Text = "DICOM C-FIND";
        this.tabPageCFind.UseVisualStyleBackColor = true;
        // 
        // txtCFindPatientName
        // 
        this.txtCFindPatientName.Location = new System.Drawing.Point(142, 27);
        this.txtCFindPatientName.Name = "txtCFindPatientName";
        this.txtCFindPatientName.Size = new System.Drawing.Size(200, 27);
        this.txtCFindPatientName.TabIndex = 1;
        // 
        // txtCFindPatientID
        // 
        this.txtCFindPatientID.Location = new System.Drawing.Point(142, 60);
        this.txtCFindPatientID.Name = "txtCFindPatientID";
        this.txtCFindPatientID.Size = new System.Drawing.Size(200, 27);
        this.txtCFindPatientID.TabIndex = 2;
        // 
        // txtCFindAccessionNumber
        // 
        this.txtCFindAccessionNumber.Location = new System.Drawing.Point(142, 93);
        this.txtCFindAccessionNumber.Name = "txtCFindAccessionNumber";
        this.txtCFindAccessionNumber.Size = new System.Drawing.Size(200, 27);
        this.txtCFindAccessionNumber.TabIndex = 3;
        // 
        // btnQueryCFind
        // 
        this.btnQueryCFind.Location = new System.Drawing.Point(142, 126);
        this.btnQueryCFind.Name = "btnQueryCFind";
        this.btnQueryCFind.Size = new System.Drawing.Size(200, 29);
        this.btnQueryCFind.TabIndex = 4;
        this.btnQueryCFind.Text = "Query C-FIND";
        this.btnQueryCFind.UseVisualStyleBackColor = true;
        this.btnQueryCFind.Click += new System.EventHandler(this.btnQueryCFind_Click);
        // 
        // dataGridCFind
        // 
        this.dataGridCFind.AllowUserToAddRows = false;
        this.dataGridCFind.AllowUserToDeleteRows = false;
        this.dataGridCFind.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.dataGridCFind.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.dataGridCFind.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridCFind.Location = new System.Drawing.Point(6, 161);
        this.dataGridCFind.Name = "dataGridCFind";
        this.dataGridCFind.ReadOnly = true;
        this.dataGridCFind.RowHeadersWidth = 51;
        this.dataGridCFind.RowTemplate.Height = 29;
        this.dataGridCFind.Size = new System.Drawing.Size(760, 400);
        this.dataGridCFind.TabIndex = 5;
        // 
        // btnGenerateStudyUID
        // 
        this.btnGenerateStudyUID.Enabled = false;
        this.btnGenerateStudyUID.Location = new System.Drawing.Point(576, 123);
        this.btnGenerateStudyUID.Name = "btnGenerateStudyUID";
        this.btnGenerateStudyUID.Size = new System.Drawing.Size(93, 27);
        this.btnGenerateStudyUID.TabIndex = 14;
        this.btnGenerateStudyUID.Text = "Generate";
        this.btnGenerateStudyUID.UseVisualStyleBackColor = true;
        this.btnGenerateStudyUID.Click += new System.EventHandler(this.btnGenerateStudyUID_Click);
        // 
        // btnGenerateSeriesUID
        // 
        this.btnGenerateSeriesUID.Enabled = false;
        this.btnGenerateSeriesUID.Location = new System.Drawing.Point(576, 156);
        this.btnGenerateSeriesUID.Name = "btnGenerateSeriesUID";
        this.btnGenerateSeriesUID.Size = new System.Drawing.Size(93, 27);
        this.btnGenerateSeriesUID.TabIndex = 15;
        this.btnGenerateSeriesUID.Text = "Generate";
        this.btnGenerateSeriesUID.UseVisualStyleBackColor = true;
        this.btnGenerateSeriesUID.Click += new System.EventHandler(this.btnGenerateSeriesUID_Click);
        // 
        // btnGenerateSOPUID
        // 
        this.btnGenerateSOPUID.Enabled = false;
        this.btnGenerateSOPUID.Location = new System.Drawing.Point(576, 189);
        this.btnGenerateSOPUID.Name = "btnGenerateSOPUID";
        this.btnGenerateSOPUID.Size = new System.Drawing.Size(93, 27);
        this.btnGenerateSOPUID.TabIndex = 16;
        this.btnGenerateSOPUID.Text = "Generate";
        this.btnGenerateSOPUID.UseVisualStyleBackColor = true;
        this.btnGenerateSOPUID.Click += new System.EventHandler(this.btnGenerateSOPUID_Click);
        // 
        // btnToggleLog
        // 
        this.btnToggleLog.Location = new System.Drawing.Point(734, 6);
        this.btnToggleLog.Size = new System.Drawing.Size(55, 26);
        this.btnToggleLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btnToggleLog.Text = "Clear";
        this.btnToggleLog.UseVisualStyleBackColor = true;
        this.btnToggleLog.Click += new System.EventHandler(this.btnToggleLog_Click);
        // 
        // splitterLog
        // 
        this.splitterLog = new System.Windows.Forms.Splitter();
        this.splitterLog.Dock = System.Windows.Forms.DockStyle.Top;
        this.splitterLog.Location = new System.Drawing.Point(3, 23);
        this.splitterLog.Name = "splitterLog";
        this.splitterLog.Size = new System.Drawing.Size(774, 5);
        this.splitterLog.TabIndex = 15;
        this.splitterLog.TabStop = false;
        // 
        // btnSaveDicomTags
        // 
        this.btnSaveDicomTags.Location = new System.Drawing.Point(348, 26);
        this.btnSaveDicomTags.Name = "btnSaveDicomTags";
        this.btnSaveDicomTags.Size = new System.Drawing.Size(132, 29);
        this.btnSaveDicomTags.Text = "Save Tags";
        this.btnSaveDicomTags.UseVisualStyleBackColor = true;
        this.btnSaveDicomTags.Enabled = false;
        this.btnSaveDicomTags.Visible = true;
        this.btnSaveDicomTags.Click += new System.EventHandler(this.btnSaveDicomTags_Click);

        // 
        // tabPageLog
        // 
        this.tabPageLog.Location = new System.Drawing.Point(4, 29);
        this.tabPageLog.Name = "tabPageLog";
        this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
        this.tabPageLog.Size = new System.Drawing.Size(792, 567);
        this.tabPageLog.TabIndex = 4;
        this.tabPageLog.Text = "Log";
        this.tabPageLog.UseVisualStyleBackColor = true;
        this.tabPageLog.Controls.Add(this.txtLog);
        this.tabPageLog.Controls.Add(this.btnToggleLog);

        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(804, 800);
        this.Controls.Add(this.tabControl);
        this.Controls.Add(this.btnAbout);
        this.MinimumSize = new System.Drawing.Size(820, 750);
        this.Name = "Form1";
        this.Text = "Alexamon DICOM Sender";
        ((System.ComponentModel.ISupportInitialize)(this.numTargetPort)).EndInit();
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.groupBoxTags.ResumeLayout(false);
        this.groupBoxTags.PerformLayout();
        this.tabControl.ResumeLayout(false);
        this.tabPageSend.ResumeLayout(false);
        this.tabPageWorklist.ResumeLayout(false);
        this.groupBoxWorklistQuery.ResumeLayout(false);
        this.groupBoxWorklistQuery.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridWorklist)).EndInit();
        this.tabPageCFind.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.dataGridCFind)).EndInit();
        this.tabPageLog.ResumeLayout(false);
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
    private System.Windows.Forms.GroupBox groupBoxTags;
    private System.Windows.Forms.CheckBox chkModifyTags;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtPatientName;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txtPatientID;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtStudyUID;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtSeriesUID;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox txtSOPInstanceUID;
    private System.Windows.Forms.Button btnGenerateUIDs;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.TextBox txtConfidentialityCode;
    private System.Windows.Forms.Button btnAbout;
    private System.Windows.Forms.TabControl tabControl;
    private System.Windows.Forms.TabPage tabPageSend;
    private System.Windows.Forms.TabPage tabPageWorklist;
    private System.Windows.Forms.GroupBox groupBoxWorklistQuery;
    private System.Windows.Forms.Button btnQueryWorklist;
    private System.Windows.Forms.CheckBox chkUseDate;
    private System.Windows.Forms.DateTimePicker dateTimeScheduled;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.TextBox txtAccessionNumber;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.TextBox txtWorklistPatientID;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.TextBox txtWorklistPatientName;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.DataGridView dataGridWorklist;
    private System.Windows.Forms.CheckBox chkDebugLogging;
    private System.Windows.Forms.TabPage tabPageCFind;
    private System.Windows.Forms.TextBox txtCFindPatientName;
    private System.Windows.Forms.TextBox txtCFindPatientID;
    private System.Windows.Forms.TextBox txtCFindAccessionNumber;
    private System.Windows.Forms.Button btnQueryCFind;
    private System.Windows.Forms.DataGridView dataGridCFind;
    private System.Windows.Forms.Button btnGenerateStudyUID;
    private System.Windows.Forms.Button btnGenerateSeriesUID;
    private System.Windows.Forms.Button btnGenerateSOPUID;
    private System.Windows.Forms.Button btnToggleLog;
    private System.Windows.Forms.Splitter splitterLog;
    private System.Windows.Forms.Button btnSaveDicomTags;
    private System.Windows.Forms.TabPage tabPageLog;
    private System.Windows.Forms.Button btnDicomDump;
    private System.Windows.Forms.Label lblRejectionNotesHeader;
    private System.Windows.Forms.Label lblCodeMeaning;
    private System.Windows.Forms.TextBox txtCodeMeaning;
    private System.Windows.Forms.Label lblRefSOPInstanceUID;
    private System.Windows.Forms.TextBox txtRefSOPInstanceUID;
    private System.Windows.Forms.Label lblRefSeriesUID;
    private System.Windows.Forms.TextBox txtRefSeriesUID;
    private System.Windows.Forms.Label lblRefStudyUID;
    private System.Windows.Forms.TextBox txtRefStudyUID;
}
