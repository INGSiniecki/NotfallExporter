

using Com.Ing.DiBa.NotfallExporterLib.File.Export;

namespace Com.Ing.DiBa.NotfallExporterLib.Api
{
    interface IFileExporter
    {
        IMessenger Messenger { get; set; }
        void Start(ExportFile sourceFile);

        void InitializeIdxBuilder();


    }
}
