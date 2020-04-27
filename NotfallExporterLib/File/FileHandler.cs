using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Abstractions;
using System.Collections.ObjectModel;
using System.IO.Compression;
using Com.Ing.DiBa.NotfallExporterLib.Util;
using Com.Ing.DiBa.NotfallExporterLib.Event;
using System.IO;
using ErrorEventHandler = Com.Ing.DiBa.NotfallExporterLib.Event.ErrorEventHandler;
using ErrorEventArgs = Com.Ing.DiBa.NotfallExporterLib.Event.ErrorEventArgs;
using Com.Ing.DiBa.NotfallExporterLib.Api;

namespace Com.Ing.DiBa.NotfallExporterLib.File
{
    /// <summary>
    /// Class for FileSystem-Operations
    /// </summary>
    public class FileHandler : IFileHandler
    {
        public IFileSystem FileSys { get; set; }

        public event WarnEventHandler WarnEvent;
        public event ErrorEventHandler ErrorEvent;

        /// <summary>
        /// Creates a Day-Backup-Directory when it is not yet existing and moves a file into it.
        /// 
        /// </summary>
        /// <param name="sourceFile">file to backup</param>
        /// <param name="backupDirectory">Backup-Directory</param>
        public void BackupFile(IFileInfo sourceFile, string backupDirectoryPath)
        {

            IDirectoryInfo backupDirectory = FileSys.DirectoryInfo.FromDirectoryName(backupDirectoryPath);
            sourceFile.Refresh();
            if (backupDirectory.Exists && sourceFile.Exists)
            {
                IFileBackup backup = new FileBackup(backupDirectory, FileSys);
                backup.BackupFile(sourceFile);
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
        public bool CheckModel(ExportModel model)
        {
            return CheckDirectoryPath(model.ErrorDirectory) && CheckDirectoryPath(model.ImportDirectory) &&
                CheckDirectoryPath(model.BackupDirectory) && CheckFilePath(model.AccountConfig) && CheckFilePath(model.IdxIndexSpecification); 
        }

        private bool CheckDirectoryPath(string directory)
        {
            return FileSys.Directory.Exists(directory);
        }

        private bool CheckFilePath(string file)
        {
            return FileSys.File.Exists(file);
        }


        /// <summary>
        /// Creates a Ready file from a given file.
        /// </summary>
        /// <param name="sourceFile">file to ceate a Ready-File from</param>
        public void CreateReadyFile(IFileInfo sourceFile)
        {
            sourceFile.Refresh();
            if (sourceFile.Exists)
            {
                IFileReady readyFile = new FileReady(sourceFile, FileSys);
                readyFile.Create();
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
        public ReadOnlyCollection<ZipArchiveEntry> getZipArchiveEntries(IFileInfo zipFile)
        {
            zipFile.Refresh();
            if (zipFile.Exists)
            {
                using (ZipArchive archive = new ZipArchive(FileSys.File.OpenRead(zipFile.FullName), ZipArchiveMode.Read))
                    return archive.Entries;
            }

            return new ReadOnlyCollection<ZipArchiveEntry>(new List<ZipArchiveEntry>());
        }

        /// <summary>
        /// Converts a Email-File into a Zip-File and moves it to a Directory.
        /// </summary>
        /// <param name="sourceFile">Email-File to convert</param>
        /// <param name="destDirectory">Destination-Directory</param>
        public IFileInfo ExportEmlFile(IFileInfo sourceFile, string destDirectoryPath)
        {
            IDirectoryInfo destDirectory = FileSys.DirectoryInfo.FromDirectoryName(destDirectoryPath);

            IFileZip zip = new FileZip(sourceFile, FileSys);

            if (destDirectory.Exists && sourceFile.Exists)
            {
                return zip.ZipFile(destDirectory);
            }
            return null;
      
        }

        /// <summary>
        /// moves a file to the given Directory
        /// </summary>
        /// <param name="sourceFile">File to move</param>
        /// <param name="destDirectory">Directory to move the file in</param>
        public IFileInfo ExportZipFile(IFileInfo sourceFile, string destDirectory)
        {
            IFileInfo zipFile = FileSys.FileInfo.FromFileName(Path.Combine(destDirectory, sourceFile.Name));

            if (!zipFile.Exists)
            {
                FileSys.File.Copy(sourceFile.FullName, zipFile.FullName);
            }
            else
            {
                onWarnEvent($"{sourceFile.Name} already exists in Import-Directory");
            }
            return zipFile;
        }
        /// <summary>
        /// loads and Xml-File
        /// </summary>
        /// <param name="path">Path to the Xml-File</param>
        /// <returns>XmlDocument object to represent the XMl-File</returns>


        private void onWarnEvent(string message)
        {
            WarnEventHandler handler = WarnEvent;
            handler?.Invoke(this, new WarnEventArgs(message));
        }


        private void onErrorEvent(Exception e)
        {
            ErrorEventHandler handler = ErrorEvent;
            handler?.Invoke(this, new ErrorEventArgs(e));
        }
    }
}
