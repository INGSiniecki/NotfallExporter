
using Com.Ing.DiBa.NotfallExporterLib.Api;
using Com.Ing.DiBa.NotfallExporterLib.Event;
using System.Collections.ObjectModel;
using System.IO.Abstractions;
using System.IO.Compression;

namespace Com.Ing.DiBa.NotfallExporterLib.File
{
    public interface IFileHandler
    {
         event WarnEventHandler WarnEvent;
         event ErrorEventHandler ErrorEvent;

        IFileSystem FileSys { get; set; }

       
        void CreateReadyFile(IFileInfo sourceFile);

        ReadOnlyCollection<ZipArchiveEntry> getZipArchiveEntries(IFileInfo zipFile);

       
        IFileInfo[] GetImportFiles(string directoryPath);

       
        void BackupFile(IFileInfo sourceFile, string backupDirectory);

      
        bool CheckModel(ExportModel model);

        IFileInfo ExportEmlFile(IFileInfo sourceFile, string destDirectory);

        IFileInfo ExportZipFile(IFileInfo sourceFile, string destDirectory);

    }
}
