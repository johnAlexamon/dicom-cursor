using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Text;
using FellowOakDicom;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using Newtonsoft.Json;

namespace DicomSenderApp;

public partial class Form1 : Form
{
    private string? selectedDicomFilePath;
    private readonly string configFilePath;
    private readonly string logFilePath;
    private DataTable worklistTable = new DataTable();
    private bool debugLoggingEnabled = false;

    public Form1()
    {
        InitializeComponent();
        
        // Initialize config file path in AppData
        string appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            "DicomSenderApp");
            
        Directory.CreateDirectory(appDataPath);
        configFilePath = Path.Combine(appDataPath, "config.json");
        logFilePath = Path.Combine(appDataPath, "dicom_sender.log");
        
        LoadConfig();
        InitializeWorklistDataGrid();
        LogMessage("Application started", false);
    }

    private void InitializeWorklistDataGrid()
    {
        // Initialize DataTable for worklist results
        worklistTable = new DataTable();
        worklistTable.Columns.Add("Patient Name", typeof(string));
        worklistTable.Columns.Add("Patient ID", typeof(string));
        worklistTable.Columns.Add("Accession Number", typeof(string));
        worklistTable.Columns.Add("Modality", typeof(string));
        worklistTable.Columns.Add("Scheduled Date/Time", typeof(string));
        worklistTable.Columns.Add("Procedure", typeof(string));
        worklistTable.Columns.Add("Study Description", typeof(string));
        
        // Bind DataTable to DataGridView
        dataGridWorklist.DataSource = worklistTable;
    }

    private void LoadConfig()
    {
        try
        {
            if (File.Exists(configFilePath))
            {
                var config = JsonConvert.DeserializeObject<DicomConfig>(File.ReadAllText(configFilePath));
                if (config != null)
                {
                    txtSourceAE.Text = config.SourceAE;
                    txtTargetAE.Text = config.TargetAE;
                    txtTargetIP.Text = config.TargetIP;
                    numTargetPort.Value = config.TargetPort;
                    
                    LogMessage("Configuration loaded from " + configFilePath);
                }
            }
        }
        catch (Exception ex)
        {
            LogMessage($"Error loading configuration: {ex.Message}");
        }
    }

    private void SaveConfig()
    {
        try
        {
            var config = new DicomConfig
            {
                SourceAE = txtSourceAE.Text,
                TargetAE = txtTargetAE.Text,
                TargetIP = txtTargetIP.Text,
                TargetPort = (int)numTargetPort.Value
            };

            File.WriteAllText(configFilePath, JsonConvert.SerializeObject(config, Formatting.Indented));
            LogMessage("Configuration saved to " + configFilePath);
        }
        catch (Exception ex)
        {
            LogMessage($"Error saving configuration: {ex.Message}");
        }
    }

    private void btnSelectDicom_Click(object sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog
        {
            Filter = "DICOM Files (*.dcm)|*.dcm|All Files (*.*)|*.*",
            Title = "Select a DICOM File"
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            selectedDicomFilePath = openFileDialog.FileName;
            lblSelectedFile.Text = Path.GetFileName(selectedDicomFilePath);
            LogMessage($"Selected file: {selectedDicomFilePath}");
            
            // Load DICOM tags from the selected file
            LoadDicomTags();
        }
    }

    private void LoadDicomTags()
    {
        if (string.IsNullOrEmpty(selectedDicomFilePath) || !File.Exists(selectedDicomFilePath))
            return;
            
        try
        {
            var dicomFile = DicomFile.Open(selectedDicomFilePath);
            var dataset = dicomFile.Dataset;
            
            // Extract and display patient info
            if (dataset.Contains(DicomTag.PatientName))
                txtPatientName.Text = dataset.GetSingleValue<string>(DicomTag.PatientName);
                
            if (dataset.Contains(DicomTag.PatientID))
                txtPatientID.Text = dataset.GetSingleValue<string>(DicomTag.PatientID);
                
            // Extract and display UIDs
            if (dataset.Contains(DicomTag.StudyInstanceUID))
                txtStudyUID.Text = dataset.GetSingleValue<string>(DicomTag.StudyInstanceUID);
                
            if (dataset.Contains(DicomTag.SeriesInstanceUID))
                txtSeriesUID.Text = dataset.GetSingleValue<string>(DicomTag.SeriesInstanceUID);
                
            if (dataset.Contains(DicomTag.SOPInstanceUID))
                txtSOPInstanceUID.Text = dataset.GetSingleValue<string>(DicomTag.SOPInstanceUID);
                
            // Extract and display Confidentiality Code
            if (dataset.Contains(DicomTag.Parse("0040,1008")))
                txtConfidentialityCode.Text = dataset.GetSingleValue<string>(DicomTag.Parse("0040,1008"));
                
            LogMessage("DICOM tags loaded from selected file");
        }
        catch (Exception ex)
        {
            LogMessage($"Error loading DICOM tags: {ex.Message}");
        }
    }

    private void btnSaveConfig_Click(object sender, EventArgs e)
    {
        SaveConfig();
    }

    private async void btnSendEcho_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSourceAE.Text) || 
            string.IsNullOrWhiteSpace(txtTargetAE.Text) ||
            string.IsNullOrWhiteSpace(txtTargetIP.Text))
        {
            LogMessage("Please fill in all required fields");
            return;
        }

        try
        {
            LogMessage("Sending C-ECHO...");
            
            var client = DicomClientFactory.Create(
                txtTargetIP.Text, 
                (int)numTargetPort.Value,
                false, 
                txtSourceAE.Text, 
                txtTargetAE.Text);
            
            client.AssociationReleased += (sender, args) => 
                LogMessage("Association released");
                
            await client.AddRequestAsync(new DicomCEchoRequest());
            await client.SendAsync();
            
            LogMessage("C-ECHO completed successfully");
        }
        catch (Exception ex)
        {
            LogMessage($"C-ECHO failed: {ex.Message}");
        }
    }

    private async void btnSendDicom_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(selectedDicomFilePath))
        {
            LogMessage("Please select a DICOM file first");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtSourceAE.Text) || 
            string.IsNullOrWhiteSpace(txtTargetAE.Text) ||
            string.IsNullOrWhiteSpace(txtTargetIP.Text))
        {
            LogMessage("Please fill in all required fields");
            return;
        }

        try
        {
            string fileToSend = selectedDicomFilePath;
            
            // If tag modification is enabled, create a modified copy of the file
            if (chkModifyTags.Checked)
            {
                fileToSend = Path.Combine(Path.GetTempPath(), $"modified_{Path.GetFileName(selectedDicomFilePath)}");
                ModifyAndSaveDicomFile(selectedDicomFilePath, fileToSend);
            }

            LogMessage($"Sending file {Path.GetFileName(fileToSend)}...");
            
            var client = DicomClientFactory.Create(
                txtTargetIP.Text, 
                (int)numTargetPort.Value,
                false, 
                txtSourceAE.Text, 
                txtTargetAE.Text);
                
            client.AssociationReleased += (sender, args) => 
                LogMessage("Association released");
                
            await client.AddRequestAsync(new DicomCStoreRequest(fileToSend));
            await client.SendAsync();
            
            LogMessage("C-STORE completed successfully");
            
            // Delete temporary file if it was created
            if (fileToSend != selectedDicomFilePath && File.Exists(fileToSend))
            {
                try
                {
                    File.Delete(fileToSend);
                }
                catch
                {
                    // Ignore errors when deleting temp file
                }
            }
        }
        catch (Exception ex)
        {
            LogMessage($"C-STORE failed: {ex.Message}");
        }
    }

    private void ModifyAndSaveDicomFile(string sourcePath, string destPath)
    {
        try
        {
            var dicomFile = DicomFile.Open(sourcePath);
            var dataset = dicomFile.Dataset;
            
            // Modify patient information
            if (!string.IsNullOrWhiteSpace(txtPatientName.Text))
                dataset.AddOrUpdate(DicomTag.PatientName, txtPatientName.Text);
                
            if (!string.IsNullOrWhiteSpace(txtPatientID.Text))
                dataset.AddOrUpdate(DicomTag.PatientID, txtPatientID.Text);
                
            // Modify UIDs
            if (!string.IsNullOrWhiteSpace(txtStudyUID.Text))
                dataset.AddOrUpdate(DicomTag.StudyInstanceUID, txtStudyUID.Text);
                
            if (!string.IsNullOrWhiteSpace(txtSeriesUID.Text))
                dataset.AddOrUpdate(DicomTag.SeriesInstanceUID, txtSeriesUID.Text);
                
            if (!string.IsNullOrWhiteSpace(txtSOPInstanceUID.Text))
                dataset.AddOrUpdate(DicomTag.SOPInstanceUID, txtSOPInstanceUID.Text);
                
            // Also update MediaStorageSOPInstanceUID in file meta if SOP Instance UID was changed
            if (!string.IsNullOrWhiteSpace(txtSOPInstanceUID.Text))
                dicomFile.FileMetaInfo.AddOrUpdate(DicomTag.MediaStorageSOPInstanceUID, txtSOPInstanceUID.Text);
                
            // Modify Confidentiality Code
            if (!string.IsNullOrWhiteSpace(txtConfidentialityCode.Text))
                dataset.AddOrUpdate(DicomTag.Parse("0040,1008"), txtConfidentialityCode.Text);
                
            // Save modified file
            dicomFile.Save(destPath);
            
            LogMessage("Created modified DICOM file with updated tags");
        }
        catch (Exception ex)
        {
            LogMessage($"Error modifying DICOM file: {ex.Message}");
            throw; // Rethrow to handle in the calling method
        }
    }

    private void LogMessage(string message, bool isDebug = false)
    {
        if (isDebug && !debugLoggingEnabled)
            return;
            
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss");
        string formattedMessage = $"[{timestamp}] {(isDebug ? "[DEBUG] " : "")}{message}";
        
        // Log to UI
        if (txtLog.InvokeRequired)
        {
            txtLog.Invoke(new Action(() => AppendToLogTextBox(formattedMessage)));
            return;
        }
        else
        {
            AppendToLogTextBox(formattedMessage);
        }
        
        // Log to file
        try
        {
            File.AppendAllText(logFilePath, formattedMessage + Environment.NewLine);
        }
        catch (Exception ex)
        {
            // If logging to file fails, at least show it in the UI
            AppendToLogTextBox($"[{timestamp}] [ERROR] Failed to write to log file: {ex.Message}");
        }
    }
    
    private void AppendToLogTextBox(string message)
    {
        txtLog.AppendText(message + Environment.NewLine);
        txtLog.ScrollToCaret();
    }
    
    private void ToggleDebugLogging(bool enabled)
    {
        debugLoggingEnabled = enabled;
        LogMessage($"Debug logging {(enabled ? "enabled" : "disabled")}", false);
    }
    
    private void chkModifyTags_CheckedChanged(object sender, EventArgs e)
    {
        bool enableControls = chkModifyTags.Checked;
        
        txtPatientName.Enabled = enableControls;
        txtPatientID.Enabled = enableControls;
        txtStudyUID.Enabled = enableControls;
        txtSeriesUID.Enabled = enableControls;
        txtSOPInstanceUID.Enabled = enableControls;
        txtConfidentialityCode.Enabled = enableControls;
        btnGenerateUIDs.Enabled = enableControls;
    }
    
    private void btnGenerateUIDs_Click(object sender, EventArgs e)
    {
        // Generate new UIDs
        txtStudyUID.Text = DicomUIDGenerator.GenerateDerivedFromUUID().UID;
        txtSeriesUID.Text = DicomUIDGenerator.GenerateDerivedFromUUID().UID;
        txtSOPInstanceUID.Text = DicomUIDGenerator.GenerateDerivedFromUUID().UID;
        
        LogMessage("New UIDs generated");
    }
    
    private void btnAbout_Click(object sender, EventArgs e)
    {
        using var aboutForm = new AboutForm();
        aboutForm.ShowDialog(this);
    }
    
    private void chkDebugLogging_CheckedChanged(object sender, EventArgs e)
    {
        ToggleDebugLogging(chkDebugLogging.Checked);
    }
    
    private void chkUseDate_CheckedChanged(object sender, EventArgs e)
    {
        dateTimeScheduled.Enabled = chkUseDate.Checked;
    }
    
    private async void btnQueryWorklist_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSourceAE.Text) || 
            string.IsNullOrWhiteSpace(txtTargetAE.Text) ||
            string.IsNullOrWhiteSpace(txtTargetIP.Text))
        {
            LogMessage("Please fill in connection details (AE Titles, IP, Port)");
            return;
        }

        try
        {
            LogMessage("Querying modality worklist...");
            
            // Log connection details
            LogMessage($"Connection details - Source AE: {txtSourceAE.Text}, Target AE: {txtTargetAE.Text}, " +
                      $"IP: {txtTargetIP.Text}, Port: {numTargetPort.Value}", isDebug: true);
            
            // Clear existing results
            worklistTable.Clear();
            
            // Create C-FIND request for modality worklist
            var request = CreateWorklistRequest();
            
            // Add matching fields based on form inputs
            AddQueryParameters(request);
            
            // Log query parameters
            LogQueryParameters(request.Dataset);
            
            // Set callback for results
            request.OnResponseReceived += (req, response) => 
            {
                if (response.Status == DicomStatus.Success || response.Status == DicomStatus.Pending)
                {
                    // Log the response if debug logging is enabled
                    LogDicomDataset("Received response dataset:", response.Dataset);
                    
                    AddWorklistResult(response.Dataset);
                }
                else
                {
                    // Log error responses
                    LogMessage($"Response status: {response.Status} - {response.Status.Description}", isDebug: true);
                }
            };
            
            // Create the DICOM client
            var client = DicomClientFactory.Create(
                txtTargetIP.Text, 
                (int)numTargetPort.Value,
                false, 
                txtSourceAE.Text, 
                txtTargetAE.Text);
            
            // We need UI updates to happen on the right thread
            client.AssociationAccepted += (sender, args) => 
                BeginInvoke(new Action(() => LogMessage("Association accepted")));
                
            client.AssociationReleased += (sender, args) => 
                BeginInvoke(new Action(() => LogMessage("Association released")));
                
            client.AssociationRejected += (sender, args) => 
                BeginInvoke(new Action(() => LogMessage($"Association rejected: {args.Reason}")));
            
            // Add request and send it
            await client.AddRequestAsync(request);
            await client.SendAsync();
            
            if (worklistTable.Rows.Count == 0)
            {
                LogMessage("No worklist items found");
            }
            else
            {
                LogMessage($"Found {worklistTable.Rows.Count} worklist items");
            }
        }
        catch (Exception ex)
        {
            LogMessage($"Worklist query failed: {ex.Message}");
            LogMessage($"Exception details: {ex}", isDebug: true);
        }
    }
    
    private DicomCFindRequest CreateWorklistRequest()
    {
        // Create a C-FIND request for modality worklist SOP class
        var request = new DicomCFindRequest(DicomQueryRetrieveLevel.NotApplicable);
        
        // Add required fields for Modality Worklist Information Model
        request.Dataset.AddOrUpdate(DicomTag.SpecificCharacterSet, "ISO_IR 100");
        
        // Add all the return fields we want to see in our results
        request.Dataset.AddOrUpdate(DicomTag.PatientName, "");
        request.Dataset.AddOrUpdate(DicomTag.PatientID, "");
        request.Dataset.AddOrUpdate(DicomTag.AccessionNumber, "");
        request.Dataset.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDate, "");
        request.Dataset.AddOrUpdate(DicomTag.ScheduledProcedureStepStartTime, "");
        request.Dataset.AddOrUpdate(DicomTag.Modality, "");
        request.Dataset.AddOrUpdate(DicomTag.ScheduledProcedureStepDescription, "");
        request.Dataset.AddOrUpdate(DicomTag.ScheduledPerformingPhysicianName, "");
        request.Dataset.AddOrUpdate(DicomTag.StudyDescription, "");
        request.Dataset.AddOrUpdate(DicomTag.RequestedProcedureDescription, "");
        
        return request;
    }
    
    private void AddQueryParameters(DicomCFindRequest request)
    {
        // Add search parameters based on form input
        if (!string.IsNullOrWhiteSpace(txtWorklistPatientName.Text))
        {
            request.Dataset.AddOrUpdate(DicomTag.PatientName, txtWorklistPatientName.Text + "*");
        }
        
        if (!string.IsNullOrWhiteSpace(txtWorklistPatientID.Text))
        {
            request.Dataset.AddOrUpdate(DicomTag.PatientID, txtWorklistPatientID.Text);
        }
        
        if (!string.IsNullOrWhiteSpace(txtAccessionNumber.Text))
        {
            request.Dataset.AddOrUpdate(DicomTag.AccessionNumber, txtAccessionNumber.Text);
        }
        
        if (chkUseDate.Checked)
        {
            request.Dataset.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDate, 
                dateTimeScheduled.Value.ToString("yyyyMMdd"));
        }
    }
    
    private void AddWorklistResult(DicomDataset dataset)
    {
        // Need to invoke on UI thread
        if (dataGridWorklist.InvokeRequired)
        {
            dataGridWorklist.Invoke(new Action(() => AddWorklistResult(dataset)));
            return;
        }
        
        try
        {
            // Extract values from the dataset
            string patientName = GetDicomValue(dataset, DicomTag.PatientName);
            string patientId = GetDicomValue(dataset, DicomTag.PatientID);
            string accessionNumber = GetDicomValue(dataset, DicomTag.AccessionNumber);
            string modality = GetDicomValue(dataset, DicomTag.Modality);
            
            // Get date and time separately and combine them
            string date = GetDicomValue(dataset, DicomTag.ScheduledProcedureStepStartDate);
            string time = GetDicomValue(dataset, DicomTag.ScheduledProcedureStepStartTime);
            string dateTime = FormatScheduledDateTime(date, time);
            
            string procedureDesc = GetDicomValue(dataset, DicomTag.ScheduledProcedureStepDescription);
            string studyDesc = GetDicomValue(dataset, DicomTag.StudyDescription);
            
            // Add a new row to the DataTable
            worklistTable.Rows.Add(
                patientName,
                patientId,
                accessionNumber,
                modality,
                dateTime,
                procedureDesc,
                studyDesc
            );
        }
        catch (Exception ex)
        {
            LogMessage($"Error processing worklist item: {ex.Message}");
        }
    }
    
    private string GetDicomValue(DicomDataset dataset, DicomTag tag)
    {
        if (dataset.Contains(tag))
        {
            try
            {
                return dataset.GetSingleValue<string>(tag);
            }
            catch
            {
                return string.Empty;
            }
        }
        return string.Empty;
    }
    
    private string FormatScheduledDateTime(string date, string time)
    {
        if (string.IsNullOrEmpty(date))
            return "";
            
        // Attempt to parse and format the date/time in a readable format
        try
        {
            string formattedDate = date.Length >= 8 
                ? $"{date.Substring(0, 4)}-{date.Substring(4, 2)}-{date.Substring(6, 2)}"
                : date;
                
            string formattedTime = string.Empty;
            if (!string.IsNullOrEmpty(time) && time.Length >= 6)
            {
                formattedTime = $" {time.Substring(0, 2)}:{time.Substring(2, 2)}:{time.Substring(4, 2)}";
            }
            
            return formattedDate + formattedTime;
        }
        catch
        {
            return date + " " + time;
        }
    }
    
    private void LogQueryParameters(DicomDataset dataset)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("C-FIND query parameters:");
        
        // Log patient name
        if (dataset.Contains(DicomTag.PatientName))
        {
            sb.AppendLine($"  Patient Name: {dataset.GetSingleValue<string>(DicomTag.PatientName)}");
        }
        
        // Log patient ID
        if (dataset.Contains(DicomTag.PatientID) && !string.IsNullOrEmpty(dataset.GetSingleValue<string>(DicomTag.PatientID)))
        {
            sb.AppendLine($"  Patient ID: {dataset.GetSingleValue<string>(DicomTag.PatientID)}");
        }
        
        // Log accession number
        if (dataset.Contains(DicomTag.AccessionNumber) && !string.IsNullOrEmpty(dataset.GetSingleValue<string>(DicomTag.AccessionNumber)))
        {
            sb.AppendLine($"  Accession Number: {dataset.GetSingleValue<string>(DicomTag.AccessionNumber)}");
        }
        
        // Log scheduled date
        if (dataset.Contains(DicomTag.ScheduledProcedureStepStartDate) && 
            !string.IsNullOrEmpty(dataset.GetSingleValue<string>(DicomTag.ScheduledProcedureStepStartDate)))
        {
            sb.AppendLine($"  Scheduled Date: {dataset.GetSingleValue<string>(DicomTag.ScheduledProcedureStepStartDate)}");
        }
        
        LogMessage(sb.ToString(), isDebug: true);
    }
    
    private void LogDicomDataset(string prefix, DicomDataset dataset)
    {
        if (!debugLoggingEnabled)
            return;
            
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(prefix);
            
            // Log essential fields for worklist
            LogDicomValue(sb, dataset, DicomTag.PatientName, "Patient Name");
            LogDicomValue(sb, dataset, DicomTag.PatientID, "Patient ID");
            LogDicomValue(sb, dataset, DicomTag.AccessionNumber, "Accession Number");
            LogDicomValue(sb, dataset, DicomTag.Modality, "Modality");
            LogDicomValue(sb, dataset, DicomTag.ScheduledProcedureStepStartDate, "Scheduled Date");
            LogDicomValue(sb, dataset, DicomTag.ScheduledProcedureStepStartTime, "Scheduled Time");
            LogDicomValue(sb, dataset, DicomTag.ScheduledProcedureStepDescription, "Procedure");
            LogDicomValue(sb, dataset, DicomTag.StudyDescription, "Study Description");
            
            LogMessage(sb.ToString(), isDebug: true);
        }
        catch (Exception ex)
        {
            LogMessage($"Error logging DICOM dataset: {ex.Message}", isDebug: true);
        }
    }
    
    private void LogDicomValue(StringBuilder sb, DicomDataset dataset, DicomTag tag, string label)
    {
        if (dataset.Contains(tag))
        {
            try
            {
                string value = dataset.GetSingleValue<string>(tag);
                sb.AppendLine($"  {label}: {value}");
            }
            catch
            {
                sb.AppendLine($"  {label}: <error retrieving value>");
            }
        }
    }
}

public class DicomConfig
{
    public string SourceAE { get; set; } = "";
    public string TargetAE { get; set; } = "";
    public string TargetIP { get; set; } = "";
    public int TargetPort { get; set; } = 104;
}
