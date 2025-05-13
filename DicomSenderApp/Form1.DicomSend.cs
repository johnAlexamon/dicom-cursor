using System;
using System.IO;
using System.Windows.Forms;
using FellowOakDicom;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;

namespace DicomSenderApp;

// This partial class contains all functionality related to the DICOM Send tab
public partial class Form1
{
    private string? selectedDicomFilePath;

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
} 