
using System.Collections.ObjectModel;
using System.IO.Abstractions;
using System.IO.Compression;

namespace Com.Ing.DiBa.NotfallExporterLib.File
{
    public interface IFileHandler
    {
        IFileSystem FileSys { get; set; }

       
        void CreateReadyFile(string sourceFile);


       
        void ZipEmailFileTo(IFileInfo sourceFile, string destDirectory);

       
        ReadOnlyCollection<ZipArchiveEntry> getZipArchiveEntries(string zipFile);

       
        IFileInfo[] GetImportFiles(string directoryPath);

       
        void BackupFile(string sourceFile, string backupDirectory);

      
        void checkModel(ExportModel model);

        void exportFile(IFileInfo sourceFile, string destDirectory);
    }
}
