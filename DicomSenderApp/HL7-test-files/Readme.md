# HL7 Message Sender Using dcm4che's `hl7snd`

This guide explains how to send HL7 messages using the `hl7snd` tool from the [dcm4che](https://github.com/dcm4che/dcm4che) toolkit on Windows.

---

## 📦 Requirements

- Java (JRE 8+)
- [dcm4che toolkit](https://github.com/dcm4che/dcm4che/releases) (latest release)
- A target HL7 listener/server (e.g., DCM4CHEE or any HL7 consumer)

---

## 🧰 Setup

1. **Download dcm4che**

   Go to [dcm4che releases](https://github.com/dcm4che/dcm4che/releases) and download the latest `*-bin.zip`.

2. **Extract the archive**, e.g., to:

---

## 🧰 Example on how to send hl7 file to populate DMWL

.\hl7snd.bat -c localhost:2575 C:\GIT\dicom-cursor\DicomSenderApp\HL7-test-files\DCM4CHEE-populate-MWL-message-M4000.hl7

If you send the above message to your DCM4CHEE arc light 5 server you will see an entry on the DMWL via the GUI at http://localhost:8080/dcm4chee-arc/ui2/en/study/mwl


## 🧰 Example on how to C-FIND from the DMWL

- The below command will retreive all entries from the the AE title WORKLIST (This is the default for the DCM4CHEE arc light 5 server)
- The -r option specifies which fields we want to retreive from the MWL. If we want to filter and retreive only some entries we could use -m to specify matching keys. For more information on this see https://github.com/dcm4che/dcm4che/blob/master/dcm4che-tool/dcm4che-tool-findscu/README.md
.\findscu.bat -c WORKLIST@localhost:11112 -M MWL -r PatientName -r PatientID -r ScheduledProcedureStepSequence.Modality