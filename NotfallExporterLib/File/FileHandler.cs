using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Abstractions;
using System.Collections.ObjectModel;
using System.IO.Compression;
using System.IO;
using Com.Ing.DiBa.NotfallExporterLib.Util;

namespace Com.Ing.DiBa.NotfallExporterLib.File
{
    /// <summary>
    /// Class for FileSystem-Operations
    /// </summary>
    public class FileHandler : IFileHandler
    {
        public IFileSystem FileSys { get; set; }


        /// <summary>
        /// Creates a Day-Backup-Directory when it is not yet existing and moves a file into it.
        /// 
        /// </summary>
        /// <param name="sourceFile">file to backup</param>
        /// <param name="backupDirectory">Backup-Directory</param>
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

        /// <summary>
        /// instantiate a object of the FileHandler class
        /// </summary>
        public FileHandler()
        {
            FileSys = new FileSystem();
        }


        /// <summary>
        /// checks wether the paths of a ExportModel exist.
        /// </summary>
        /// <param name="model">contains Export-Path Infromation</param>
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


        /// <summary>
        /// Creates a Ready file from a given file.
        /// </summary>
        /// <param name="sourceFile">file to ceate a Ready-File from</param>
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

        /// <summary>
        /// returns .zip and .eml files in a given Directory.
        /// </summary>
        /// <param name="directoryPath">Directory to extract import files from</param>
        /// <returns>Array of FileInfo representing all Import-Files</returns>
        public IFileInfo[] GetImportFiles(string directoryPath)
        {
            IDirectoryInfo directory = FileSys.DirectoryInfo.FromDirectoryName(directoryPath);

            List<IFileInfo> files = directory.GetFiles("*.eml", SearchOption.TopDirectoryOnly).ToList();

            files.AddRange(directory.GetFiles("*.zip", SearchOption.TopDirectoryOnly).ToList());


            Log.Logger.Info($"{files.ToArray().Length} Import Files found");

            return files.ToArray();
        }

        /// <summary>
        /// returns all Entries of a zip File
        /// </summary>
        /// <param name="zipFile">Zip-File to get the entries from</param>
        /// <returns>Collection of ZipEntries</returns>
        public ReadOnlyCollection<ZipArchiveEntry> getZipArchiveEntries(string zipFile)
        {
            using (ZipArchive archive = new ZipArchive(FileSys.File.OpenRead(zipFile), ZipArchiveMode.Read)) 
            return archive.Entries;
        }

        /// <summary>
        /// Converts a Email-File into a Zip-File and moves it to a Directory.
        /// </summary>
        /// <param name="sourceFile">Email-File to convert</param>
        /// <param name="destDirectory">Destination-Directory</param>
        public void ZipEmailFileTo(IFileInfo sourceFile, string destDirectory)
        {
            string zipFilePath = Path.Combine(destDirectory, Path.ChangeExtension(sourceFile.Name,".zip"));



            if (FileSys.File.Exists(zipFilePath))
                Log.Logger.Warn($"File: {zipFilePath.GetFileName()} already exists in Import-Directory");
            else
                using (ZipArchive archive = new ZipArchive(FileSys.File.Create(zipFilePath), ZipArchiveMode.Create))
                {
                    ZipArchiveEntry zipElement = archive.CreateEntry(sourceFile.Name);

                    using (MemoryStream originalFileMemoryStream = new MemoryStream(FileSys.File.ReadAllBytes(sourceFile.FullName)))
                    {
                        using (Stream zipElementStream = zipElement.Open())
                        {
                            originalFileMemoryStream.CopyTo(zipElementStream);
                        }
                    }
                    Log.Logger.Info($"File: {sourceFile.Name} zipped");
                }
        }

        public void exportFile(IFileInfo sourceFile, string destDirectory)
        {
            string destFilePath = Path.Combine(destDirectory, sourceFile.Name);
            if (!FileSys.File.Exists(destFilePath))
            {
                FileSys.File.Copy(sourceFile.FullName, destFilePath);
            }
        }
    }
}
