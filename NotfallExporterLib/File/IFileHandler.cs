using Com.Ing.DiBa.NotfallExporterLib.Export;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Abstractions;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ing.DiBa.NotfallExporterLib.File
{
    public interface IFileHandler
    {
        IFileSystem FileSys { get; set; }

        void CreateReadyFile(string sourceFile);
        void ZipEmailFileTo(string sourceFile, string destDirectory);

        ReadOnlyCollection<ZipArchiveEntry> getZipArchiveEntries(string zipFile);

        string[] GetImportFiles(string directory);

        void BackupFile(string sourceFile, string backupDirectory);

        void checkModel(ExportModel model);
    }
}
