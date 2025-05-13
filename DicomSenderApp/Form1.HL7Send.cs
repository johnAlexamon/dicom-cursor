using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using HL7.Dotnetcore;

namespace DicomSenderApp;

// This partial class contains all functionality related to the HL7 Message Sending tab
public partial class Form1
{
    private string? selectedHL7FilePath;
    
    // UI controls for HL7 configuration
    private TextBox txtHL7TargetIP;
    private NumericUpDown numHL7TargetPort;
    private TextBox txtHL7FilePreview;
    private Button btnSelectHL7File;
    private Button btnSendHL7;
    private Button btnSaveHL7Config;
    
    private void InitializeHL7Tab()
    {
        // Create the tab page if not already done in designer
        TabPage tabPageHL7 = new TabPage
        {
            Text = "HL7 Send",
            Name = "tabPageHL7",
            UseVisualStyleBackColor = true
        };
        
        // Add the tab to the tab control if not already there
        if (!tabControl.TabPages.ContainsKey("tabPageHL7"))
        {
            tabControl.TabPages.Add(tabPageHL7);
        }
        else
        {
            tabPageHL7 = tabControl.TabPages["tabPageHL7"];
            tabPageHL7.Controls.Clear(); // Clear existing controls if any
        }
        
        // Create the HL7 configuration group box
        GroupBox groupHL7Config = new GroupBox
        {
            Text = "HL7 Server Configuration",
            Location = new System.Drawing.Point(12, 12),
            Size = new System.Drawing.Size(358, 100),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        
        // Target IP
        Label lblHL7TargetIP = new Label
        {
            Text = "Target IP Address:",
            Location = new System.Drawing.Point(16, 30),
            AutoSize = true
        };
        
        txtHL7TargetIP = new TextBox
        {
            Location = new System.Drawing.Point(142, 27),
            Size = new System.Drawing.Size(200, 27),
            Text = "127.0.0.1",
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        
        // Target Port
        Label lblHL7TargetPort = new Label
        {
            Text = "Target Port:",
            Location = new System.Drawing.Point(16, 63),
            AutoSize = true
        };
        
        numHL7TargetPort = new NumericUpDown
        {
            Location = new System.Drawing.Point(142, 60),
            Size = new System.Drawing.Size(150, 27),
            Minimum = 1,
            Maximum = 65535,
            Value = 2100, // Default HL7 port
            Anchor = AnchorStyles.Top | AnchorStyles.Left
        };
        
        // Add controls to the config group
        groupHL7Config.Controls.Add(lblHL7TargetIP);
        groupHL7Config.Controls.Add(txtHL7TargetIP);
        groupHL7Config.Controls.Add(lblHL7TargetPort);
        groupHL7Config.Controls.Add(numHL7TargetPort);
        
        // Create operations group
        GroupBox groupHL7Operations = new GroupBox
        {
            Text = "Operations",
            Location = new System.Drawing.Point(12, 118),
            Size = new System.Drawing.Size(748, 100),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        
        // File selection
        btnSelectHL7File = new Button
        {
            Text = "Select HL7 File...",
            Location = new System.Drawing.Point(16, 24),
            Size = new System.Drawing.Size(120, 29)
        };
        
        Label lblSelectedHL7File = new Label
        {
            Text = "No file selected",
            Location = new System.Drawing.Point(142, 27),
            AutoSize = true,
            Name = "lblSelectedHL7File"
        };
        
        // Save config button
        btnSaveHL7Config = new Button
        {
            Text = "Save Config",
            Location = new System.Drawing.Point(16, 59),
            Size = new System.Drawing.Size(160, 29)
        };
        
        // Send HL7 button
        btnSendHL7 = new Button
        {
            Text = "Send HL7 Message",
            Location = new System.Drawing.Point(182, 59),
            Size = new System.Drawing.Size(160, 29)
        };
        
        // Add controls to operations group
        groupHL7Operations.Controls.Add(btnSelectHL7File);
        groupHL7Operations.Controls.Add(lblSelectedHL7File);
        groupHL7Operations.Controls.Add(btnSaveHL7Config);
        groupHL7Operations.Controls.Add(btnSendHL7);
        
        // Create preview group
        GroupBox groupHL7Preview = new GroupBox
        {
            Text = "Message Preview",
            Location = new System.Drawing.Point(12, 224),
            Size = new System.Drawing.Size(748, 240),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
        };
        
        // Preview text box
        txtHL7FilePreview = new TextBox
        {
            Multiline = true,
            ScrollBars = ScrollBars.Both,
            ReadOnly = true,
            Location = new System.Drawing.Point(16, 26),
            Size = new System.Drawing.Size(716, 198),
            Font = new System.Drawing.Font("Consolas", 9F),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
        };
        
        // Add controls to preview group
        groupHL7Preview.Controls.Add(txtHL7FilePreview);
        
        // Create log group (reuse from Form1.cs)
        GroupBox groupHL7Log = new GroupBox
        {
            Text = "Log",
            Location = new System.Drawing.Point(12, 470),
            Size = new System.Drawing.Size(748, 155),
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
        };
        
        // Create a new log text box for this tab
        TextBox txtHL7Log = new TextBox
        {
            Multiline = true,
            ScrollBars = ScrollBars.Both,
            ReadOnly = true,
            Location = new System.Drawing.Point(16, 26),
            Size = new System.Drawing.Size(716, 123),
            Font = new System.Drawing.Font("Consolas", 9F),
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
        };
        
        // Add text box to log group
        groupHL7Log.Controls.Add(txtHL7Log);
        
        // Add all groups to the tab page
        tabPageHL7.Controls.Add(groupHL7Config);
        tabPageHL7.Controls.Add(groupHL7Operations);
        tabPageHL7.Controls.Add(groupHL7Preview);
        tabPageHL7.Controls.Add(groupHL7Log);
        
        // Wire up event handlers
        btnSelectHL7File.Click += btnSelectHL7File_Click;
        btnSaveHL7Config.Click += btnSaveHL7Config_Click;
        btnSendHL7.Click += btnSendHL7_Click;
    }
    
    private void btnSelectHL7File_Click(object sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog
        {
            Filter = "HL7 Files (*.hl7)|*.hl7|Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
            Title = "Select an HL7 File"
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            selectedHL7FilePath = openFileDialog.FileName;
            
            // Find and update the label
            if (tabControl.TabPages["tabPageHL7"] is TabPage tabPage)
            {
                foreach (Control control in tabPage.Controls)
                {
                    if (control is GroupBox groupBox)
                    {
                        foreach (Control c in groupBox.Controls)
                        {
                            if (c is Label label && label.Name == "lblSelectedHL7File")
                            {
                                label.Text = Path.GetFileName(selectedHL7FilePath);
                                break;
                            }
                        }
                    }
                }
            }
            
            LogMessage($"Selected HL7 file: {selectedHL7FilePath}");
            
            // Load HL7 file content into preview
            LoadHL7FileContent();
        }
    }
    
    private void LoadHL7FileContent()
    {
        if (string.IsNullOrEmpty(selectedHL7FilePath) || !File.Exists(selectedHL7FilePath))
        {
            txtHL7FilePreview.Text = "No file selected or file does not exist.";
            return;
        }
        
        try
        {
            string content = File.ReadAllText(selectedHL7FilePath);
            txtHL7FilePreview.Text = content;
            
            // Try to parse and validate as HL7
            try
            {
                var message = new HL7.Dotnetcore.Message(content);
                LogMessage("HL7 message loaded and validated successfully");
            }
            catch (Exception ex)
            {
                LogMessage($"Warning: File may not be a valid HL7 message: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            txtHL7FilePreview.Text = $"Error loading file: {ex.Message}";
            LogMessage($"Error loading HL7 file: {ex.Message}");
        }
    }
    
    private void btnSaveHL7Config_Click(object sender, EventArgs e)
    {
        SaveConfig();
    }
    
    private async void btnSendHL7_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(selectedHL7FilePath) || !File.Exists(selectedHL7FilePath))
        {
            LogMessage("Please select an HL7 file first");
            return;
        }
        
        if (string.IsNullOrWhiteSpace(txtHL7TargetIP.Text))
        {
            LogMessage("Please fill in the target IP address");
            return;
        }
        
        try
        {
            string content = File.ReadAllText(selectedHL7FilePath);
            
            // Create HL7 message
            var message = new HL7.Dotnetcore.Message(content);
            
            // HL7 requires specific characters for message framing:
            // VT (Vertical Tab) at the start: ASCII 11 (0x0B)
            // FS (File Separator) at the end: ASCII 28 (0x1C)
            // CR (Carriage Return) at the end: ASCII 13 (0x0D)
            byte[] startBytes = new byte[] { 0x0B }; // VT
            byte[] endBytes = new byte[] { 0x1C, 0x0D }; // FS, CR
            
            // Build the complete message with framing
            byte[] messageBytes = Encoding.UTF8.GetBytes(message.SerializeMessage(false));
            
            // Create the framed message
            byte[] framedMessage = new byte[startBytes.Length + messageBytes.Length + endBytes.Length];
            startBytes.CopyTo(framedMessage, 0);
            messageBytes.CopyTo(framedMessage, startBytes.Length);
            endBytes.CopyTo(framedMessage, startBytes.Length + messageBytes.Length);
            
            LogMessage($"Sending HL7 message to {txtHL7TargetIP.Text}:{numHL7TargetPort.Value}...");
            
            // Connect and send
            using (var client = new TcpClient())
            {
                // Connect with timeout
                var connectTask = client.ConnectAsync(txtHL7TargetIP.Text, (int)numHL7TargetPort.Value);
                
                // Wait for connection with timeout
                var timeoutTask = Task.Delay(5000); // 5 second timeout
                var completedTask = await Task.WhenAny(connectTask, timeoutTask);
                
                if (completedTask == timeoutTask)
                {
                    throw new TimeoutException("Connection timed out after 5 seconds");
                }
                
                // Get stream and send message
                using (var stream = client.GetStream())
                {
                    await stream.WriteAsync(framedMessage, 0, framedMessage.Length);
                    
                    // Wait for ACK response
                    byte[] responseBuffer = new byte[4096];
                    
                    // Set read timeout
                    stream.ReadTimeout = 5000; // 5 seconds
                    
                    // Read response asynchronously with timeout
                    var readTask = stream.ReadAsync(responseBuffer, 0, responseBuffer.Length);
                    timeoutTask = Task.Delay(5000);
                    
                    completedTask = await Task.WhenAny(readTask, timeoutTask);
                    if (completedTask == timeoutTask)
                    {
                        throw new TimeoutException("Timeout waiting for ACK response");
                    }
                    
                    int bytesRead = await readTask;
                    
                    if (bytesRead > 0)
                    {
                        // Extract the response, ignoring the framing characters
                        string response = Encoding.UTF8.GetString(responseBuffer, 1, bytesRead - 3);
                        
                        try
                        {
                            // Parse the ACK message
                            var ackMessage = new HL7.Dotnetcore.Message(response);
                            string ackCode = ackMessage.GetValue("MSA.1");
                            
                            if (ackCode == "AA")
                            {
                                LogMessage("HL7 message sent successfully and acknowledged (AA)");
                            }
                            else if (ackCode == "AE")
                            {
                                LogMessage($"HL7 message had errors (AE): {ackMessage.GetValue("MSA.3")}");
                            }
                            else if (ackCode == "AR")
                            {
                                LogMessage($"HL7 message rejected (AR): {ackMessage.GetValue("MSA.3")}");
                            }
                            else
                            {
                                LogMessage($"Received ACK with unknown code: {ackCode}");
                            }
                        }
                        catch (Exception ex)
                        {
                            LogMessage($"Received response but failed to parse ACK: {ex.Message}");
                            LogMessage($"Raw response: {response}");
                        }
                    }
                    else
                    {
                        LogMessage("No acknowledgment received from server");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogMessage($"Error sending HL7 message: {ex.Message}");
            if (ex.InnerException != null)
            {
                LogMessage($"Inner exception: {ex.InnerException.Message}");
            }
        }
    }
} 