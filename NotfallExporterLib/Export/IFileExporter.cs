

using Com.Ing.DiBa.NotfallExporterLib.Event;
using System.IO.Abstractions;

namespace Com.Ing.DiBa.NotfallExporterLib.Export
{
    interface IFileExporter
    {
        event FileExportEventHandler FileExportEvent;
        void Start(IFileInfo sourceFile);


    }
}
