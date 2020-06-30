

using Com.Ing.DiBa.NotfallExporterLib.File;
using Com.Ing.DiBa.NotfallExporterLib.File.Export;

namespace Com.Ing.DiBa.NotfallExporterLib.Api
{
    public interface IFileExporter
    {
        IMessenger Messenger { get; set; }
        ExportModel ExportModel { get; set; }

        IFileHandler FileHandler { get; set; }
        void Start(ExportFile sourceFile);

        void InitializeIdxBuilder();


    }
}
