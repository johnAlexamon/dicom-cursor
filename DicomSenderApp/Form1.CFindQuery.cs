using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using FellowOakDicom;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;

namespace DicomSenderApp;

// This partial class contains all functionality related to the DICOM C-FIND tab
public partial class Form1
{
    private DataTable cfindTable = new DataTable();
    
    // UI controls for C-FIND configuration
    private TextBox txtCFindSourceAE;
    private TextBox txtCFindTargetAE;
    private TextBox txtCFindTargetIP;
    private NumericUpDown numCFindTargetPort;
    private Button btnSaveCFindConfig;

    private void InitializeCFindDataGrid()
    {
        // Initialize DataTable for C-FIND results
        cfindTable = new DataTable();
        cfindTable.Columns.Add("Patient Name", typeof(string));
        cfindTable.Columns.Add("Patient ID", typeof(string));
        cfindTable.Columns.Add("Accession Number", typeof(string));
        cfindTable.Columns.Add("Study Date", typeof(string));
        cfindTable.Columns.Add("Study Time", typeof(string));
        cfindTable.Columns.Add("Modality", typeof(string));
        cfindTable.Columns.Add("Study Description", typeof(string));
        
        // Bind DataTable to DataGridView
        dataGridCFind.DataSource = cfindTable;
    }
    
    private void InitializeCFindConfigControls()
    {
        // Create a group box for the configuration
        GroupBox groupCFindConfig = new GroupBox
        {
            Text = "C-FIND Server Configuration",
            Location = new System.Drawing.Point(10, 10),
            Size = new System.Drawing.Size(460, 150),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        
        // Source AE
        Label lblCFindSourceAE = new Label
        {
            Text = "Source AE Title:",
            Location = new System.Drawing.Point(10, 25),
            AutoSize = true
        };
        
        txtCFindSourceAE = new TextBox
        {
            Location = new System.Drawing.Point(150, 22),
            Size = new System.Drawing.Size(150, 23),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        
        // Target AE
        Label lblCFindTargetAE = new Label
        {
            Text = "Target AE Title:",
            Location = new System.Drawing.Point(10, 55),
            AutoSize = true
        };
        
        txtCFindTargetAE = new TextBox
        {
            Location = new System.Drawing.Point(150, 52),
            Size = new System.Drawing.Size(150, 23),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        
        // Target IP
        Label lblCFindTargetIP = new Label
        {
            Text = "Target IP Address:",
            Location = new System.Drawing.Point(10, 85),
            AutoSize = true
        };
        
        txtCFindTargetIP = new TextBox
        {
            Location = new System.Drawing.Point(150, 82),
            Size = new System.Drawing.Size(150, 23),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        
        // Target Port
        Label lblCFindTargetPort = new Label
        {
            Text = "Target Port:",
            Location = new System.Drawing.Point(10, 115),
            AutoSize = true
        };
        
        numCFindTargetPort = new NumericUpDown
        {
            Location = new System.Drawing.Point(150, 112),
            Size = new System.Drawing.Size(80, 23),
            Minimum = 1,
            Maximum = 65535,
            Value = 104
        };
        
        // Save Button
        btnSaveCFindConfig = new Button
        {
            Text = "Save C-FIND Config",
            Location = new System.Drawing.Point(320, 112),
            Size = new System.Drawing.Size(130, 23),
            Anchor = AnchorStyles.Top | AnchorStyles.Right
        };
        
        btnSaveCFindConfig.Click += btnSaveCFindConfig_Click;
        
        // Add controls to the group box
        groupCFindConfig.Controls.Add(lblCFindSourceAE);
        groupCFindConfig.Controls.Add(txtCFindSourceAE);
        groupCFindConfig.Controls.Add(lblCFindTargetAE);
        groupCFindConfig.Controls.Add(txtCFindTargetAE);
        groupCFindConfig.Controls.Add(lblCFindTargetIP);
        groupCFindConfig.Controls.Add(txtCFindTargetIP);
        groupCFindConfig.Controls.Add(lblCFindTargetPort);
        groupCFindConfig.Controls.Add(numCFindTargetPort);
        groupCFindConfig.Controls.Add(btnSaveCFindConfig);
        
        // Add the group box to the C-FIND tab
        tabPageCFind.Controls.Add(groupCFindConfig);
        
        // Create query controls
        GroupBox groupCFindQuery = new GroupBox
        {
            Text = "C-FIND Query Parameters",
            Location = new System.Drawing.Point(10, 170),
            Size = new System.Drawing.Size(460, 150),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        
        // Patient Name
        Label lblCFindPatientName = new Label
        {
            Text = "Patient Name:",
            Location = new System.Drawing.Point(10, 25),
            AutoSize = true
        };
        
        txtCFindPatientName = new TextBox
        {
            Location = new System.Drawing.Point(150, 22),
            Size = new System.Drawing.Size(150, 23),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        
        // Patient ID
        Label lblCFindPatientID = new Label
        {
            Text = "Patient ID:",
            Location = new System.Drawing.Point(10, 55),
            AutoSize = true
        };
        
        txtCFindPatientID = new TextBox
        {
            Location = new System.Drawing.Point(150, 52),
            Size = new System.Drawing.Size(150, 23),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        
        // Accession Number
        Label lblCFindAccessionNumber = new Label
        {
            Text = "Accession Number:",
            Location = new System.Drawing.Point(10, 85),
            AutoSize = true
        };
        
        txtCFindAccessionNumber = new TextBox
        {
            Location = new System.Drawing.Point(150, 82),
            Size = new System.Drawing.Size(150, 23),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        
        // Query Button
        btnQueryCFind = new Button
        {
            Text = "Run C-FIND Query",
            Location = new System.Drawing.Point(320, 112),
            Size = new System.Drawing.Size(130, 23),
            Anchor = AnchorStyles.Top | AnchorStyles.Right
        };
        
        btnQueryCFind.Click += btnQueryCFind_Click;
        
        // Add controls to the group box
        groupCFindQuery.Controls.Add(lblCFindPatientName);
        groupCFindQuery.Controls.Add(txtCFindPatientName);
        groupCFindQuery.Controls.Add(lblCFindPatientID);
        groupCFindQuery.Controls.Add(txtCFindPatientID);
        groupCFindQuery.Controls.Add(lblCFindAccessionNumber);
        groupCFindQuery.Controls.Add(txtCFindAccessionNumber);
        groupCFindQuery.Controls.Add(btnQueryCFind);
        
        // Add the group box to the C-FIND tab
        tabPageCFind.Controls.Add(groupCFindQuery);
        
        // Create DataGridView for results
        dataGridCFind = new DataGridView
        {
            Location = new System.Drawing.Point(10, 330),
            Size = new System.Drawing.Size(760, 200),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        };
        
        // Add DataGridView to the C-FIND tab
        tabPageCFind.Controls.Add(dataGridCFind);
    }
    
    private void btnSaveCFindConfig_Click(object sender, EventArgs e)
    {
        SaveConfig();
    }
    
    private async void btnQueryCFind_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtCFindSourceAE.Text) || 
            string.IsNullOrWhiteSpace(txtCFindTargetAE.Text) ||
            string.IsNullOrWhiteSpace(txtCFindTargetIP.Text))
        {
            LogMessage("Please fill in connection details (AE Titles, IP, Port) in the C-FIND configuration section");
            return;
        }

        try
        {
            LogMessage("Performing DICOM C-FIND query...");
            
            // Log connection details
            LogMessage($"Connection details - Source AE: {txtCFindSourceAE.Text}, Target AE: {txtCFindTargetAE.Text}, " +
                      $"IP: {txtCFindTargetIP.Text}, Port: {numCFindTargetPort.Value}", isDebug: true);
            
            // Clear existing results
            cfindTable.Clear();
            
            // Create C-FIND request for patient root query
            var request = CreateCFindRequest();
            
            // Add matching fields based on form inputs
            AddCFindQueryParameters(request);
            
            // Log query parameters
            LogCFindQueryParameters(request.Dataset);
            
            // Set callback for results
            request.OnResponseReceived += (req, response) => 
            {
                if (response.Status == DicomStatus.Success || response.Status == DicomStatus.Pending)
                {
                    // Log the response if debug logging is enabled
                    LogDicomDataset("Received C-FIND response dataset:", response.Dataset);
                    
                    AddCFindResult(response.Dataset);
                }
                else
                {
                    // Log error responses
                    LogMessage($"Response status: {response.Status} - {response.Status.Description}", isDebug: true);
                }
            };
            
            // Create the DICOM client
            var client = DicomClientFactory.Create(
                txtCFindTargetIP.Text, 
                (int)numCFindTargetPort.Value,
                false, 
                txtCFindSourceAE.Text, 
                txtCFindTargetAE.Text);
            
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
            
            if (cfindTable.Rows.Count == 0)
            {
                LogMessage("No C-FIND results found");
            }
            else
            {
                LogMessage($"Found {cfindTable.Rows.Count} C-FIND results");
            }
        }
        catch (Exception ex)
        {
            LogMessage($"C-FIND query failed: {ex.Message}");
            LogMessage($"Exception details: {ex}", isDebug: true);
        }
    }
    
    private DicomCFindRequest CreateCFindRequest()
    {
        // Create a C-FIND request for Patient/Study Root Query
        var request = new DicomCFindRequest(DicomUID.StudyRootQueryRetrieveInformationModelFind, DicomPriority.Medium);
        
        // Add required fields
        request.Dataset.AddOrUpdate(DicomTag.SpecificCharacterSet, "ISO_IR 100");
        
        // Critical: set the Query/Retrieve Level to STUDY
        request.Dataset.AddOrUpdate(DicomTag.QueryRetrieveLevel, "STUDY");
        
        // Add all the return fields we want to see in our results as empty strings (wildcards)
        request.Dataset.AddOrUpdate(DicomTag.PatientName, "");
        request.Dataset.AddOrUpdate(DicomTag.PatientID, "");
        request.Dataset.AddOrUpdate(DicomTag.AccessionNumber, "");
        request.Dataset.AddOrUpdate(DicomTag.StudyDate, "");
        request.Dataset.AddOrUpdate(DicomTag.StudyTime, "");
        request.Dataset.AddOrUpdate(DicomTag.Modality, "");
        request.Dataset.AddOrUpdate(DicomTag.StudyDescription, "");
        request.Dataset.AddOrUpdate(DicomTag.StudyInstanceUID, "");
        
        LogMessage("Created C-FIND request", isDebug: true);
        
        return request;
    }
    
    private void AddCFindQueryParameters(DicomCFindRequest request)
    {
        // Track if any specific filters were applied
        bool hasFilters = false;
        
        // Add search parameters based on form input
        if (!string.IsNullOrWhiteSpace(txtCFindPatientName.Text))
        {
            request.Dataset.AddOrUpdate(DicomTag.PatientName, txtCFindPatientName.Text + "*");
            hasFilters = true;
        }
        
        if (!string.IsNullOrWhiteSpace(txtCFindPatientID.Text))
        {
            request.Dataset.AddOrUpdate(DicomTag.PatientID, txtCFindPatientID.Text);
            hasFilters = true;
        }
        
        if (!string.IsNullOrWhiteSpace(txtCFindAccessionNumber.Text))
        {
            request.Dataset.AddOrUpdate(DicomTag.AccessionNumber, txtCFindAccessionNumber.Text);
            hasFilters = true;
        }
        
        // Log whether this is a filtered query or returning all entries
        if (hasFilters)
        {
            LogMessage("Sending C-FIND query with filters", isDebug: true);
        }
        else
        {
            LogMessage("Sending C-FIND query to retrieve ALL entries (no filters)", isDebug: true);
            // When no filters are applied, the empty strings in the dataset will match all entries
        }
    }
    
    private void AddCFindResult(DicomDataset dataset)
    {
        // Need to invoke on UI thread
        if (dataGridCFind.InvokeRequired)
        {
            dataGridCFind.Invoke(new Action(() => AddCFindResult(dataset)));
            return;
        }
        
        try
        {
            // Extract values from the dataset
            string patientName = GetDicomValue(dataset, DicomTag.PatientName);
            string patientId = GetDicomValue(dataset, DicomTag.PatientID);
            string accessionNumber = GetDicomValue(dataset, DicomTag.AccessionNumber);
            string studyDate = GetDicomValue(dataset, DicomTag.StudyDate);
            string studyTime = GetDicomValue(dataset, DicomTag.StudyTime);
            string modality = GetDicomValue(dataset, DicomTag.Modality);
            string studyDesc = GetDicomValue(dataset, DicomTag.StudyDescription);
            
            // Add a new row to the DataTable
            cfindTable.Rows.Add(
                patientName,
                patientId,
                accessionNumber,
                studyDate,
                studyTime,
                modality,
                studyDesc
            );
        }
        catch (Exception ex)
        {
            LogMessage($"Error processing C-FIND result: {ex.Message}");
        }
    }
    
    private void LogCFindQueryParameters(DicomDataset dataset)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("C-FIND query parameters:");
        
        // Log Query/Retrieve Level - this is critical for C-FIND to work properly
        if (dataset.Contains(DicomTag.QueryRetrieveLevel))
        {
            string value = SafeGetDicomValue(dataset, DicomTag.QueryRetrieveLevel);
            sb.AppendLine($"  Query/Retrieve Level: {value}");
        }
        
        // Log patient name
        if (dataset.Contains(DicomTag.PatientName))
        {
            string value = SafeGetDicomValue(dataset, DicomTag.PatientName);
            sb.AppendLine($"  Patient Name: {value}");
        }
        
        // Log patient ID
        if (dataset.Contains(DicomTag.PatientID))
        {
            string value = SafeGetDicomValue(dataset, DicomTag.PatientID);
            if (!string.IsNullOrEmpty(value))
            {
                sb.AppendLine($"  Patient ID: {value}");
            }
        }
        
        // Log accession number
        if (dataset.Contains(DicomTag.AccessionNumber))
        {
            string value = SafeGetDicomValue(dataset, DicomTag.AccessionNumber);
            if (!string.IsNullOrEmpty(value))
            {
                sb.AppendLine($"  Accession Number: {value}");
            }
        }
        
        LogMessage(sb.ToString(), isDebug: true);
    }
} 