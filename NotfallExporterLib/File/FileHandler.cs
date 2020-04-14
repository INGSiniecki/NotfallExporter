using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Abstractions;
using System.Collections.ObjectModel;
using System.IO.Compression;
using System.IO;
using Com.Ing.DiBa.NotfallExporterLib.Util;
using Com.Ing.DiBa.NotfallExporterLib.Export;

namespace Com.Ing.DiBa.NotfallExporterLib.File
{
    public class FileHandler : IFileHandler
    {
        public IFileSystem FileSys { get; set; }

       
        public void BackupFile(string sourceFile, string backupDirectory)
        {
            DateTime currentDate = DateTime.Today;
            string backupDirectoryPath = Path.Combine(backupDirectory, "Backup" + currentDate.ToString("dd_MM_yy"));

            //creates a daily-directory in case it doesn't exist.
            if (!FileSys.Directory.Exists(backupDirectoryPath))
                FileSys.Directory.CreateDirectory(backupDirectoryPath);

            string backupFilePath = Path.Combine(backupDirectoryPath, sourceFile.GetFileName());

            if (FileSys.File.Exists(backupFilePath))
                Log.Logger.Warn($"File: {backupFilePath.GetFileName()} already exists in Backup-Directory");
            else
            {
                FileSys.File.Move(sourceFile, backupFilePath);
                Log.Logger.Info($"File: {backupFilePath.GetFileName()} moved to Backup-Directory");
            }

        }

        public FileHandler()
        {
            FileSys = new FileSystem();
        }

        
        public void checkModel(ExportModel model)
        {

            if (!FileSys.Directory.Exists(model.ErrorDirectory))
                throw new DirectoryNotFoundException($"{model.ErrorDirectory} konnte nicht gefunden werden");

            if (!FileSys.Directory.Exists(model.ImportDirectory))
                throw new DirectoryNotFoundException($"{model.ImportDirectory} konnte nicht gefunden werden");

            if (!FileSys.Directory.Exists(model.BackupDirectory))
                throw new DirectoryNotFoundException($"{model.BackupDirectory} konnte nicht gefunden werden");

            if (!FileSys.File.Exists(model.AccountConfig))
                throw new FileNotFoundException($"{model.AccountConfig} konnte nicht gefunden werden");

            if (!FileSys.File.Exists(model.IdxIndexSpecification))
                throw new FileNotFoundException($"{model.IdxIndexSpecification} konnte nicht gefunden werden");
        }

       
        public void CreateReadyFile(string sourceFile)
        {

            if (FileSys.File.Exists(sourceFile) && FileSys.File.Exists(Path.ChangeExtension(sourceFile, "idx")))
            {
                string readyFile = Path.ChangeExtension(sourceFile, "rdy");
                FileSys.File.Create(readyFile);
                Log.Logger.Info($"Rdy File created: {readyFile}");
            }
            else
            {
                Log.Logger.Error($"Could not create Rdy File for File: {sourceFile}");
            }
        }
        
        public string[] GetImportFiles(string directoryPath)
        {
            List<string> files = FileSys.Directory.GetFiles(directoryPath, "*.eml").ToList();
            files.AddRange(FileSys.Directory.GetFiles(directoryPath, "*.zip").ToList());


            Log.Logger.Info($"{files.ToArray().Length} Import Files found");

            return files.ToArray();
        }


        public ReadOnlyCollection<ZipArchiveEntry> getZipArchiveEntries(string zipFile)
        {
            using (ZipArchive archive = new ZipArchive(FileSys.File.OpenRead(zipFile), ZipArchiveMode.Read)) 
            return archive.Entries;
        }

        public void ZipEmailFileTo(string sourceFile, string destDirectory)
        {
            string zipFilePath = Path.Combine(destDirectory, sourceFile.GetFileName().RemoveFileExtension() + ".zip");



            if (FileSys.File.Exists(zipFilePath))
                Log.Logger.Warn($"File: {zipFilePath.GetFileName()} already exists in Import-Directory");
            else
                using (ZipArchive archive = new ZipArchive(FileSys.File.Create(zipFilePath), ZipArchiveMode.Create))
                {
                    ZipArchiveEntry zipElement = archive.CreateEntry(sourceFile.GetFileName());

                    using (MemoryStream originalFileMemoryStream = new MemoryStream(FileSys.File.ReadAllBytes(sourceFile)))
                    {
                        using (Stream zipElementStream = zipElement.Open())
                        {
                            originalFileMemoryStream.CopyTo(zipElementStream);
                        }
                    }
                    Log.Logger.Info($"File: {sourceFile.GetFileName()} zipped");
                }
        }
    }
}
