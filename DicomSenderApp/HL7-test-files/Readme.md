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

## 🧰 Example on how to send hl7 file

.\hl7snd.bat -c localhost:2575 C:\GIT\dicom-cursor\DicomSenderApp\HL7-test-files\DCM4CHEE-populate-MWL-message-M4000.hl7
