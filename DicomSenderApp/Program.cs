using System;
using System.Windows.Forms;
using FellowOakDicom;
using Microsoft.Extensions.DependencyInjection;

namespace DicomSenderApp;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Initialize DicomSetupBuilder for fo-dicom
        new DicomSetupBuilder()
            .RegisterServices(services => services.AddFellowOakDicom())
            .Build();

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }    
}