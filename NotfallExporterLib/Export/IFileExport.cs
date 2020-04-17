

using System.IO.Abstractions;

namespace Com.Ing.DiBa.NotfallExporterLib.Export
{
    interface IFileExporter
    {
        event ExportEventHandler FileExportEvent;
        void Start(IFileInfo sourceFile);


    }
}
