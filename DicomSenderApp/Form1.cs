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
    private readonly string configFilePath;
    private readonly string logFilePath;
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
        
        InitializeWorklistConfigControls();
        LoadConfig();
        InitializeWorklistDataGrid();
        LogMessage("Application started", false);
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
                    // Main tab configuration
                    txtSourceAE.Text = config.SourceAE;
                    txtTargetAE.Text = config.TargetAE;
                    txtTargetIP.Text = config.TargetIP;
                    numTargetPort.Value = config.TargetPort;
                    
                    // Worklist tab configuration
                    txtWorklistSourceAE.Text = config.WorklistSourceAE;
                    txtWorklistTargetAE.Text = config.WorklistTargetAE;
                    txtWorklistTargetIP.Text = config.WorklistTargetIP;
                    numWorklistTargetPort.Value = config.WorklistTargetPort;
                    
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
                // Main tab configuration
                SourceAE = txtSourceAE.Text,
                TargetAE = txtTargetAE.Text,
                TargetIP = txtTargetIP.Text,
                TargetPort = (int)numTargetPort.Value,
                
                // Worklist tab configuration
                WorklistSourceAE = txtWorklistSourceAE.Text,
                WorklistTargetAE = txtWorklistTargetAE.Text,
                WorklistTargetIP = txtWorklistTargetIP.Text,
                WorklistTargetPort = (int)numWorklistTargetPort.Value
            };

            File.WriteAllText(configFilePath, JsonConvert.SerializeObject(config, Formatting.Indented));
            LogMessage("Configuration saved to " + configFilePath);
        }
        catch (Exception ex)
        {
            LogMessage($"Error saving configuration: {ex.Message}");
        }
    }

    public void LogMessage(string message, bool isDebug = false)
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
    
    private void btnAbout_Click(object sender, EventArgs e)
    {
        using var aboutForm = new AboutForm();
        aboutForm.ShowDialog(this);
    }
    
    private void chkDebugLogging_CheckedChanged(object sender, EventArgs e)
    {
        ToggleDebugLogging(chkDebugLogging.Checked);
    }
    
    private void btnSaveConfig_Click(object sender, EventArgs e)
    {
        SaveConfig();
    }
}

public class DicomConfig
{
    public string SourceAE { get; set; } = "";
    public string TargetAE { get; set; } = "";
    public string TargetIP { get; set; } = "";
    public int TargetPort { get; set; } = 104;
    
    // Worklist specific configuration
    public string WorklistSourceAE { get; set; } = "";
    public string WorklistTargetAE { get; set; } = "";
    public string WorklistTargetIP { get; set; } = "";
    public int WorklistTargetPort { get; set; } = 104;
}
