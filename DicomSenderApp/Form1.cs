using System;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using FellowOakDicom;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using Newtonsoft.Json;

namespace DicomSenderApp;

public partial class Form1 : Form
{
    private string? selectedDicomFilePath;
    private readonly string configFilePath;

    public Form1()
    {
        InitializeComponent();
        
        // Initialize config file path in AppData
        string appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            "DicomSenderApp");
            
        Directory.CreateDirectory(appDataPath);
        configFilePath = Path.Combine(appDataPath, "config.json");
        
        LoadConfig();
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
            LogMessage($"Sending file {Path.GetFileName(selectedDicomFilePath)}...");
            
            var client = DicomClientFactory.Create(
                txtTargetIP.Text, 
                (int)numTargetPort.Value,
                false, 
                txtSourceAE.Text, 
                txtTargetAE.Text);
                
            client.AssociationReleased += (sender, args) => 
                LogMessage("Association released");
                
            await client.AddRequestAsync(new DicomCStoreRequest(selectedDicomFilePath));
            await client.SendAsync();
            
            LogMessage("C-STORE completed successfully");
        }
        catch (Exception ex)
        {
            LogMessage($"C-STORE failed: {ex.Message}");
        }
    }

    private void LogMessage(string message)
    {
        if (txtLog.InvokeRequired)
        {
            txtLog.Invoke(new Action(() => LogMessage(message)));
            return;
        }

        txtLog.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}");
        txtLog.ScrollToCaret();
    }
}

public class DicomConfig
{
    public string SourceAE { get; set; } = "";
    public string TargetAE { get; set; } = "";
    public string TargetIP { get; set; } = "";
    public int TargetPort { get; set; } = 104;
}
