

using Com.Ing.DiBa.NotfallExporterLib.Event;
using Com.Ing.DiBa.NotfallExporterLib.File.Export;

namespace Com.Ing.DiBa.NotfallExporterLib.Api
{
    interface IFileExporter
    {
        event FileExportEventHandler FileExportEvent;
        void Start(ExportFile sourceFile);


    }
}
