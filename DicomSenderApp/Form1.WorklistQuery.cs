using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using FellowOakDicom;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;

namespace DicomSenderApp;

// This partial class contains all functionality related to the Modality Worklist tab
public partial class Form1
{
    private DataTable worklistTable = new DataTable();
    
    // UI controls for worklist configuration
    private TextBox txtWorklistSourceAE;
    private TextBox txtWorklistTargetAE;
    private TextBox txtWorklistTargetIP;
    private NumericUpDown numWorklistTargetPort;
    private Button btnSaveWorklistConfig;

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
    
    private void InitializeWorklistConfigControls()
    {
        // Create a group box for the configuration
        GroupBox groupWorklistConfig = new GroupBox
        {
            Text = "Worklist Server Configuration",
            Location = new System.Drawing.Point(10, 10),
            Size = new System.Drawing.Size(460, 150),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        
        // Source AE
        Label lblWorklistSourceAE = new Label
        {
            Text = "Source AE Title:",
            Location = new System.Drawing.Point(10, 25),
            AutoSize = true
        };
        
        txtWorklistSourceAE = new TextBox
        {
            Location = new System.Drawing.Point(150, 22),
            Size = new System.Drawing.Size(150, 23),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        
        // Target AE
        Label lblWorklistTargetAE = new Label
        {
            Text = "Target AE Title:",
            Location = new System.Drawing.Point(10, 55),
            AutoSize = true
        };
        
        txtWorklistTargetAE = new TextBox
        {
            Location = new System.Drawing.Point(150, 52),
            Size = new System.Drawing.Size(150, 23),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        
        // Target IP
        Label lblWorklistTargetIP = new Label
        {
            Text = "Target IP Address:",
            Location = new System.Drawing.Point(10, 85),
            AutoSize = true
        };
        
        txtWorklistTargetIP = new TextBox
        {
            Location = new System.Drawing.Point(150, 82),
            Size = new System.Drawing.Size(150, 23),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        
        // Target Port
        Label lblWorklistTargetPort = new Label
        {
            Text = "Target Port:",
            Location = new System.Drawing.Point(10, 115),
            AutoSize = true
        };
        
        numWorklistTargetPort = new NumericUpDown
        {
            Location = new System.Drawing.Point(150, 112),
            Size = new System.Drawing.Size(80, 23),
            Minimum = 1,
            Maximum = 65535,
            Value = 104
        };
        
        // Save Button
        btnSaveWorklistConfig = new Button
        {
            Text = "Save Worklist Config",
            Location = new System.Drawing.Point(320, 112),
            Size = new System.Drawing.Size(130, 23),
            Anchor = AnchorStyles.Top | AnchorStyles.Right
        };
        
        btnSaveWorklistConfig.Click += btnSaveWorklistConfig_Click;
        
        // Add controls to the group box
        groupWorklistConfig.Controls.Add(lblWorklistSourceAE);
        groupWorklistConfig.Controls.Add(txtWorklistSourceAE);
        groupWorklistConfig.Controls.Add(lblWorklistTargetAE);
        groupWorklistConfig.Controls.Add(txtWorklistTargetAE);
        groupWorklistConfig.Controls.Add(lblWorklistTargetIP);
        groupWorklistConfig.Controls.Add(txtWorklistTargetIP);
        groupWorklistConfig.Controls.Add(lblWorklistTargetPort);
        groupWorklistConfig.Controls.Add(numWorklistTargetPort);
        groupWorklistConfig.Controls.Add(btnSaveWorklistConfig);
        
        // Add the group box to the Worklist tab
        tabPageWorklist.Controls.Add(groupWorklistConfig);
        
        // Adjust positions of existing controls to make room for the config section
        // (Move the search controls and results grid down)
        foreach (Control control in tabPageWorklist.Controls)
        {
            if (control != groupWorklistConfig && !(control is TabControl))
            {
                control.Location = new System.Drawing.Point(
                    control.Location.X, 
                    control.Location.Y + 160
                );
            }
        }
        
        // Adjust size of results grid if needed
        if (dataGridWorklist != null)
        {
            dataGridWorklist.Size = new System.Drawing.Size(
                dataGridWorklist.Size.Width,
                dataGridWorklist.Size.Height - 160
            );
        }
    }
    
    private void btnSaveWorklistConfig_Click(object sender, EventArgs e)
    {
        SaveConfig();
    }
    
    private void chkUseDate_CheckedChanged(object sender, EventArgs e)
    {
        dateTimeScheduled.Enabled = chkUseDate.Checked;
    }
    
    private async void btnQueryWorklist_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtWorklistSourceAE.Text) || 
            string.IsNullOrWhiteSpace(txtWorklistTargetAE.Text) ||
            string.IsNullOrWhiteSpace(txtWorklistTargetIP.Text))
        {
            LogMessage("Please fill in connection details (AE Titles, IP, Port) in the worklist configuration section");
            return;
        }

        try
        {
            LogMessage("Querying modality worklist...");
            
            // Log connection details
            LogMessage($"Connection details - Source AE: {txtWorklistSourceAE.Text}, Target AE: {txtWorklistTargetAE.Text}, " +
                      $"IP: {txtWorklistTargetIP.Text}, Port: {numWorklistTargetPort.Value}", isDebug: true);
            
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
                txtWorklistTargetIP.Text, 
                (int)numWorklistTargetPort.Value,
                false, 
                txtWorklistSourceAE.Text, 
                txtWorklistTargetAE.Text);
            
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
        // Create a C-FIND request for modality worklist SOP class with explicit UID
        var request = new DicomCFindRequest(DicomUID.ModalityWorklistInformationModelFind, DicomPriority.Medium);
        
        // Add required fields for Modality Worklist Information Model
        request.Dataset.AddOrUpdate(DicomTag.SpecificCharacterSet, "ISO_IR 100");
        // Add all the return fields we want to see in our results as empty strings (wildcards)
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
        
        LogMessage("Created worklist C-FIND request", isDebug: true);
        
        return request;
    }
    
    private void AddQueryParameters(DicomCFindRequest request)
    {
        // Track if any specific filters were applied
        bool hasFilters = false;
        
        // Add search parameters based on form input
        if (!string.IsNullOrWhiteSpace(txtWorklistPatientName.Text))
        {
            request.Dataset.AddOrUpdate(DicomTag.PatientName, txtWorklistPatientName.Text + "*");
            hasFilters = true;
        }
        
        if (!string.IsNullOrWhiteSpace(txtWorklistPatientID.Text))
        {
            request.Dataset.AddOrUpdate(DicomTag.PatientID, txtWorklistPatientID.Text);
            hasFilters = true;
        }
        
        if (!string.IsNullOrWhiteSpace(txtAccessionNumber.Text))
        {
            request.Dataset.AddOrUpdate(DicomTag.AccessionNumber, txtAccessionNumber.Text);
            hasFilters = true;
        }
        
        if (chkUseDate.Checked)
        {
            request.Dataset.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDate, 
                dateTimeScheduled.Value.ToString("yyyyMMdd"));
            hasFilters = true;
        }
        
        // Log whether this is a filtered query or returning all entries
        if (hasFilters)
        {
            LogMessage("Sending worklist query with filters", isDebug: true);
        }
        else
        {
            LogMessage("Sending worklist query to retrieve ALL entries (no filters)", isDebug: true);
            // When no filters are applied, the empty strings in the dataset will match all entries
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