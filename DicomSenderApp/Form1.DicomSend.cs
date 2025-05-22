using System;
using System.IO;
using System.Windows.Forms;
using FellowOakDicom;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using Newtonsoft.Json;
using System.Collections.Generic;

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
                
            // Extract and display Rejection Note tags
            // Code Meaning from Concept Name Code Sequence (0040,A043)
            if (dataset.Contains(DicomTag.Parse("0040,A043")))
            {
                var conceptNameCodeSequence = dataset.GetSequence(DicomTag.Parse("0040,A043"));
                if (conceptNameCodeSequence.Items.Count > 0 && 
                    conceptNameCodeSequence.Items[0].Contains(DicomTag.CodeMeaning))
                {
                    txtCodeMeaning.Text = conceptNameCodeSequence.Items[0].GetSingleValue<string>(DicomTag.CodeMeaning);
                    LogMessage($"Found Code Meaning: {txtCodeMeaning.Text}");
                }
            }
            
            // Referenced study/series/SOP instance UIDs from Current Requested Procedure Evidence Sequence (0040,A375)
            if (dataset.Contains(DicomTag.Parse("0040,A375")))
            {
                LogMessage("Found Current Requested Procedure Evidence Sequence (0040,A375)", true);
                var evidenceSequence = dataset.GetSequence(DicomTag.Parse("0040,A375"));
                if (evidenceSequence.Items.Count > 0)
                {
                    var evidenceItem = evidenceSequence.Items[0];
                    
                    // Study Instance UID - directly in the evidence item
                    if (evidenceItem.Contains(DicomTag.StudyInstanceUID))
                    {
                        txtRefStudyUID.Text = evidenceItem.GetSingleValue<string>(DicomTag.StudyInstanceUID);
                        LogMessage($"Found Study Instance UID: {txtRefStudyUID.Text}", true);
                    }
                    
                    // Navigate to Referenced Series Sequence
                    if (evidenceItem.Contains(DicomTag.Parse("0008,1115")))
                    {
                        LogMessage("Found Referenced Series Sequence (0008,1115)", true);
                        var seriesSequence = evidenceItem.GetSequence(DicomTag.Parse("0008,1115"));
                        if (seriesSequence.Items.Count > 0)
                        {
                            var seriesItem = seriesSequence.Items[0];
                            
                            // Series Instance UID
                            if (seriesItem.Contains(DicomTag.SeriesInstanceUID))
                            {
                                txtRefSeriesUID.Text = seriesItem.GetSingleValue<string>(DicomTag.SeriesInstanceUID);
                                LogMessage($"Found Series Instance UID: {txtRefSeriesUID.Text}", true);
                            }
                            
                            // Navigate to Referenced SOP Sequence
                            if (seriesItem.Contains(DicomTag.Parse("0008,1199")))
                            {
                                LogMessage("Found Referenced SOP Sequence (0008,1199)", true);
                                var sopSequence = seriesItem.GetSequence(DicomTag.Parse("0008,1199"));
                                if (sopSequence.Items.Count > 0)
                                {
                                    var sopItem = sopSequence.Items[0];
                                    
                                    // SOP Instance UID
                                    if (sopItem.Contains(DicomTag.ReferencedSOPInstanceUID))
                                    {
                                        txtRefSOPInstanceUID.Text = sopItem.GetSingleValue<string>(DicomTag.ReferencedSOPInstanceUID);
                                        LogMessage($"Found Referenced SOP Instance UID: {txtRefSOPInstanceUID.Text}", true);
                                    }
                                }
                            }
                        }
                    }
                    
                    // Try alternate path via Content Sequence if UIDs weren't found
                    if (string.IsNullOrEmpty(txtRefSOPInstanceUID.Text) && dataset.Contains(DicomTag.Parse("0040,a730")))
                    {
                        LogMessage("Trying alternate path via Content Sequence (0040,a730)", true);
                        var contentSequence = dataset.GetSequence(DicomTag.Parse("0040,a730"));
                        if (contentSequence.Items.Count > 0)
                        {
                            var contentItem = contentSequence.Items[0];
                            if (contentItem.Contains(DicomTag.Parse("0008,1199")))
                            {
                                var sopSequence = contentItem.GetSequence(DicomTag.Parse("0008,1199"));
                                if (sopSequence.Items.Count > 0)
                                {
                                    var sopItem = sopSequence.Items[0];
                                    if (sopItem.Contains(DicomTag.ReferencedSOPInstanceUID))
                                    {
                                        txtRefSOPInstanceUID.Text = sopItem.GetSingleValue<string>(DicomTag.ReferencedSOPInstanceUID);
                                        LogMessage($"Found Referenced SOP Instance UID (alternate path): {txtRefSOPInstanceUID.Text}", true);
                                    }
                                }
                            }
                        }
                    }
                }
            }
                
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
                
            // Modify rejection note tags if needed
            if (!string.IsNullOrWhiteSpace(txtCodeMeaning.Text) ||
                !string.IsNullOrWhiteSpace(txtRefSOPInstanceUID.Text) ||
                !string.IsNullOrWhiteSpace(txtRefSeriesUID.Text) ||
                !string.IsNullOrWhiteSpace(txtRefStudyUID.Text))
            {
                // Handle Code Meaning in Concept Name Code Sequence
                if (!string.IsNullOrWhiteSpace(txtCodeMeaning.Text))
                {
                    var conceptNameCodeSequence = new DicomSequence(DicomTag.Parse("0040,A043"));
                    var conceptNameItem = new DicomDataset();
                    
                    // Add standard rejection note code from DICOM
                    conceptNameItem.Add(DicomTag.CodeValue, "113001");
                    conceptNameItem.Add(DicomTag.CodingSchemeDesignator, "DCM");
                    conceptNameItem.Add(DicomTag.CodeMeaning, txtCodeMeaning.Text);
                    
                    conceptNameCodeSequence.Items.Add(conceptNameItem);
                    dataset.AddOrUpdate(DicomTag.Parse("0040,A043"), conceptNameCodeSequence);
                    
                    LogMessage("Added Concept Name Code Sequence with Code Meaning", true);
                }
                
                // Handle Referenced UIDs in Current Requested Procedure Evidence Sequence
                if (!string.IsNullOrWhiteSpace(txtRefSOPInstanceUID.Text) ||
                    !string.IsNullOrWhiteSpace(txtRefSeriesUID.Text) ||
                    !string.IsNullOrWhiteSpace(txtRefStudyUID.Text))
                {
                    var evidenceSequence = new DicomSequence(DicomTag.Parse("0040,A375"));
                    var evidenceItem = new DicomDataset();
                    
                    // Add Study Instance UID directly to the evidence item
                    if (!string.IsNullOrWhiteSpace(txtRefStudyUID.Text))
                    {
                        evidenceItem.Add(DicomTag.StudyInstanceUID, txtRefStudyUID.Text);
                        LogMessage($"Added Study Instance UID to evidence: {txtRefStudyUID.Text}", true);
                    }
                    
                    // Create the Referenced Series Sequence if Series or SOP Instance UIDs are provided
                    if (!string.IsNullOrWhiteSpace(txtRefSeriesUID.Text) || 
                        !string.IsNullOrWhiteSpace(txtRefSOPInstanceUID.Text))
                    {
                        var seriesSequence = new DicomSequence(DicomTag.Parse("0008,1115"));
                        var seriesItem = new DicomDataset();
                        
                        // Add Series Instance UID to the series item
                        if (!string.IsNullOrWhiteSpace(txtRefSeriesUID.Text))
                        {
                            seriesItem.Add(DicomTag.SeriesInstanceUID, txtRefSeriesUID.Text);
                            LogMessage($"Added Series Instance UID to series: {txtRefSeriesUID.Text}", true);
                        }
                        
                        // Create the Referenced SOP Sequence if SOP Instance UID is provided
                        if (!string.IsNullOrWhiteSpace(txtRefSOPInstanceUID.Text))
                        {
                            var sopSequence = new DicomSequence(DicomTag.Parse("0008,1199"));
                            var sopItem = new DicomDataset();
                            
                            // Add SOP Class UID and SOP Instance UID to the SOP item
                            sopItem.Add(DicomTag.ReferencedSOPClassUID, "1.2.840.10008.5.1.4.1.1.7"); // Secondary Capture Image Storage
                            sopItem.Add(DicomTag.ReferencedSOPInstanceUID, txtRefSOPInstanceUID.Text);
                            
                            LogMessage($"Added Referenced SOP Instance UID to SOP sequence: {txtRefSOPInstanceUID.Text}", true);
                            
                            sopSequence.Items.Add(sopItem);
                            seriesItem.AddOrUpdate(DicomTag.Parse("0008,1199"), sopSequence);
                        }
                        
                        seriesSequence.Items.Add(seriesItem);
                        evidenceItem.AddOrUpdate(DicomTag.Parse("0008,1115"), seriesSequence);
                    }
                    
                    evidenceSequence.Items.Add(evidenceItem);
                    dataset.AddOrUpdate(DicomTag.Parse("0040,A375"), evidenceSequence);
                    
                    LogMessage("Added Current Requested Procedure Evidence Sequence", true);
                    
                    // Also add to Content Sequence for completeness
                    if (!string.IsNullOrWhiteSpace(txtRefSOPInstanceUID.Text))
                    {
                        var contentSequence = new DicomSequence(DicomTag.Parse("0040,a730"));
                        var contentItem = new DicomDataset();
                        
                        contentItem.Add(DicomTag.Parse("0040,a010"), "CONTAINS"); // Relationship Type
                        contentItem.Add(DicomTag.Parse("0040,a040"), "IMAGE"); // Value Type
                        
                        var sopSequence = new DicomSequence(DicomTag.Parse("0008,1199"));
                        var sopItem = new DicomDataset();
                        
                        sopItem.Add(DicomTag.ReferencedSOPClassUID, "1.2.840.10008.5.1.4.1.1.7"); // Secondary Capture Image Storage
                        sopItem.Add(DicomTag.ReferencedSOPInstanceUID, txtRefSOPInstanceUID.Text);
                        
                        sopSequence.Items.Add(sopItem);
                        contentItem.AddOrUpdate(DicomTag.Parse("0008,1199"), sopSequence);
                        
                        contentSequence.Items.Add(contentItem);
                        dataset.AddOrUpdate(DicomTag.Parse("0040,a730"), contentSequence);
                        
                        LogMessage("Added Content Sequence with Referenced SOP Instance", true);
                    }
                }
            }
                
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
        bool enabled = chkModifyTags.Checked;
        
        // Enable/disable tag fields
        txtPatientName.Enabled = enabled;
        txtPatientID.Enabled = enabled;
        txtStudyUID.Enabled = enabled;
        txtSeriesUID.Enabled = enabled;
        txtSOPInstanceUID.Enabled = enabled;
        txtConfidentialityCode.Enabled = enabled;
        
        // Enable/disable rejection note fields
        txtCodeMeaning.Enabled = enabled;
        txtRefSOPInstanceUID.Enabled = enabled;
        txtRefSeriesUID.Enabled = enabled;
        txtRefStudyUID.Enabled = enabled;
        
        // Enable/disable generate buttons
        btnGenerateUIDs.Enabled = enabled;
        btnGenerateStudyUID.Enabled = enabled;
        btnGenerateSeriesUID.Enabled = enabled;
        btnGenerateSOPUID.Enabled = enabled;
        
        // Enable/disable save tags button
        btnSaveDicomTags.Enabled = enabled;
    }
    
    private void btnGenerateUIDs_Click(object sender, EventArgs e)
    {
        // Generate all UIDs
        GenerateStudyUID();
        GenerateSeriesUID();
        GenerateSOPInstanceUID();
    }
    
    private void btnGenerateStudyUID_Click(object sender, EventArgs e)
    {
        // Generate only Study UID
        GenerateStudyUID();
    }
    
    private void btnGenerateSeriesUID_Click(object sender, EventArgs e)
    {
        // Generate only Series UID
        GenerateSeriesUID();
    }
    
    private void btnGenerateSOPUID_Click(object sender, EventArgs e)
    {
        // Generate only SOP Instance UID
        GenerateSOPInstanceUID();
    }
    
    private void GenerateStudyUID()
    {
        txtStudyUID.Text = DicomUID.Generate().UID;
        LogMessage("Generated new Study UID");
    }
    
    private void GenerateSeriesUID()
    {
        txtSeriesUID.Text = DicomUID.Generate().UID;
        LogMessage("Generated new Series UID");
    }
    
    private void GenerateSOPInstanceUID()
    {
        txtSOPInstanceUID.Text = DicomUID.Generate().UID;
        LogMessage("Generated new SOP Instance UID");
    }

    // Clear log
    private void btnToggleLog_Click(object sender, EventArgs e)
    {
        // Clear the log text box
        txtLog.Clear();
        LogMessage("Log cleared");
    }
    
    // Save DICOM tag values to configuration
    private void btnSaveDicomTags_Click(object sender, EventArgs e)
    {
        SaveDicomTagsToConfig();
        LogMessage("DICOM tag values saved to configuration");
    }
    
    // Perform a full DICOM dump of all tags in the selected file
    private void btnDicomDump_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(selectedDicomFilePath) || !File.Exists(selectedDicomFilePath))
        {
            LogMessage("Please select a DICOM file first");
            return;
        }
        
        try
        {
            var dicomFile = DicomFile.Open(selectedDicomFilePath);
            
            LogMessage($"DICOM Dump of file: {Path.GetFileName(selectedDicomFilePath)}");
            LogMessage("=".PadRight(80, '='));
            
            // Dump file meta info
            LogMessage("File Meta Information:");
            LogAllDicomTags(dicomFile.FileMetaInfo);
            
            // Dump dataset
            LogMessage("\nDataset:");
            LogAllDicomTags(dicomFile.Dataset);
            
            LogMessage("=".PadRight(80, '='));
            LogMessage("DICOM Dump completed");
        }
        catch (Exception ex)
        {
            LogMessage($"Error performing DICOM dump: {ex.Message}");
        }
    }
    
    // Log all tags in a DICOM dataset (recursive for sequences)
    private void LogAllDicomTags(DicomDataset dataset, string prefix = "")
    {
        if (dataset == null)
            return;
        
        foreach (var item in dataset)
        {
            try
            {
                string value = "";
                
                // Handle different DICOM VRs (Value Representations)
                if (item is DicomSequence seq)
                {
                    LogMessage($"{prefix}{item.Tag} [{item.ValueRepresentation}] {item.Tag.DictionaryEntry?.Name ?? "Unknown"} (Sequence with {seq.Items.Count} item(s))");
                    int itemIndex = 0;
                    foreach (var sequenceItem in seq.Items)
                    {
                        LogMessage($"{prefix}  > Item #{++itemIndex}:");
                        LogAllDicomTags(sequenceItem, prefix + "    ");
                    }
                    continue;
                }
                else if (item is DicomElement element)
                {
                    if (element.Count > 0)
                    {
                        try
                        {
                            // Try to get value as string if possible
                                                                                    if (element.Count == 1)                            {                                value = dataset.GetValueOrDefault(item.Tag, 0, string.Empty);                            }                            else
                            {
                                // For multi-valued elements, get all values
                                var values = new List<string>();
                                for (int i = 0; i < element.Count; i++)
                                {
                                    var itemValue = dataset.GetValueOrDefault(item.Tag, i, string.Empty);
                                    values.Add(itemValue);
                                }
                                value = string.Join(" | ", values);
                            }
                        }
                        catch
                        {
                            value = "<Unable to display value>";
                        }
                    }
                }
                
                // Truncate very long values
                if (value.Length > 100)
                {
                    value = value.Substring(0, 100) + "...";
                }
                
                // Log tag, VR, name and value
                LogMessage($"{prefix}{item.Tag} [{item.ValueRepresentation}] {item.Tag.DictionaryEntry?.Name ?? "Unknown"} = {value}");
            }
            catch (Exception ex)
            {
                LogMessage($"{prefix}{item.Tag} Error reading tag: {ex.Message}");
            }
        }
    }
    
    private void SaveDicomTagsToConfig()
    {
        try
        {
            // Read existing config
            DicomConfig config;
            if (File.Exists(configFilePath))
            {
                config = JsonConvert.DeserializeObject<DicomConfig>(File.ReadAllText(configFilePath));
                if (config == null)
                {
                    config = new DicomConfig();
                }
            }
            else
            {
                config = new DicomConfig();
            }
            
            // Update DICOM tag values
            config.PatientName = txtPatientName.Text;
            config.PatientID = txtPatientID.Text;
            config.StudyUID = txtStudyUID.Text;
            config.SeriesUID = txtSeriesUID.Text;
            config.SOPInstanceUID = txtSOPInstanceUID.Text;
            config.ConfidentialityCode = txtConfidentialityCode.Text;
            
            // Update rejection note tag values
            config.CodeMeaning = txtCodeMeaning.Text;
            config.RefSOPInstanceUID = txtRefSOPInstanceUID.Text;
            config.RefSeriesUID = txtRefSeriesUID.Text;
            config.RefStudyUID = txtRefStudyUID.Text;
            
            // Save the updated config
            File.WriteAllText(configFilePath, JsonConvert.SerializeObject(config, Formatting.Indented));
            LogMessage("Saved DICOM tag and rejection note values to configuration");
        }
        catch (Exception ex)
        {
            LogMessage($"Error saving DICOM tag values: {ex.Message}");
        }
    }
} 