using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace DicomSenderApp
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            LoadVersionInfo();
        }

        private void LoadVersionInfo()
        {
            try
            {
                // Get version info from the executing assembly
                Assembly assembly = Assembly.GetExecutingAssembly();
                Version? version = assembly.GetName().Version;

                if (version != null)
                {
                    // Format as major.minor.build
                    lblVersion.Text = $"Version: {version.Major}.{version.Minor}.{version.Build}";
                }
            }
            catch (Exception ex)
            {
                // Fallback to hardcoded version if there's an issue
                lblVersion.Text = "Version: 1.1.0";
                Debug.WriteLine($"Error loading version info: {ex.Message}");
            }
        }

        private void linkGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                linkGitHub.LinkVisited = true;
                // Open the GitHub repository in the default browser
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://github.com/johnAlexamon/dicom-cursor",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to open link: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblCopyright_Click(object sender, EventArgs e)
        {

        }
    }
} 