
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

       
        void CreateReadyFile(string sourceFile);


       
        void ZipEmailFileTo(IFileInfo sourceFile, string destDirectory);

       
        ReadOnlyCollection<ZipArchiveEntry> getZipArchiveEntries(string zipFile);

       
        IFileInfo[] GetImportFiles(string directoryPath);

       
        void BackupFile(string sourceFile, string backupDirectory);

      
        bool CheckModel(ExportModel model);

        void exportFile(IFileInfo sourceFile, string destDirectory);
    }
}
