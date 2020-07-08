using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Abstractions;
using System.Collections.ObjectModel;
using System.IO.Compression;
using Com.Ing.DiBa.NotfallExporterLib.Util;
using System.IO;
using Com.Ing.DiBa.NotfallExporterLib.Api;
using System.Xml;
using NotfallExporterLib.Database;

namespace Com.Ing.DiBa.NotfallExporterLib.File
{
    /// <summary>
    /// Class for FileSystem-Operations
    /// </summary>
    public class FileHandler : IFileHandler
    {
        public IFileSystem FileSys { get; set; }
        public IMessenger Messenger { get; set; }
        public ISqliteDataAccess DbService { get; set; }


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
            IFileBackup backup = new FileBackup(backupDirectory, FileSys);
            backup.BackupFile(sourceFile);
        }

        /// <summary>
        /// instantiate a object of the FileHandler class
        /// </summary>
        public FileHandler()
        {
            FileSys = new FileSystem();
            DbService = new SqliteDataAccess();
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
            IFileReady readyFile = new FileReady(sourceFile, FileSys);
            readyFile.Create();
        }

        /// <summary>
        /// returns .zip and .eml files in a given Directory.
        /// </summary>
        /// <param name="directoryPath">Directory to extract import files from</param>
        /// <returns>Array of FileInfo representing all Import-Files</returns>
        public IFileInfo[] GetImportFiles(string directoryPath)
        {
            IDirectoryInfo directory = FileSys.DirectoryInfo.FromDirectoryName(directoryPath);

            if (!directory.Exists)
            {
                throw new DirectoryNotFoundException($"Error-Directory: {directory.FullName} doesn't exist!");
            }

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
            using (ZipArchive archive = new ZipArchive(FileSys.File.OpenRead(zipFile.FullName), ZipArchiveMode.Read))
                return archive.Entries;
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

            return zip.ZipFile(destDirectory);

        }

        /// <summary>
        /// moves a file to the given Directory
        /// </summary>
        /// <param name="sourceFile">File to move</param>
        /// <param name="destDirectory">Directory to move the file in</param>
        public IFileInfo ExportZipFile(IFileInfo sourceFile, string destDirectoryPath)
        {
            IFileInfo zipFile = FileSys.FileInfo.FromFileName(Path.Combine(destDirectoryPath, sourceFile.Name));

            if (!zipFile.Exists)
            {
                FileSys.File.Copy(sourceFile.FullName, zipFile.FullName);
            }
            else
            {
                Messenger?.SendMessage($"{zipFile.Name} already exists!");
            }
            return zipFile;
        }

        /// <summary>
        /// loads and Xml-File
        /// </summary>
        /// <param name="sourceFile">IFileInfo object to represent the Source-File</param>
        /// <returns>XmlDocument object to represent the Xml-File</returns>
        public XmlDocument LoadXmlFile(IFileInfo sourceFile)
        {
            XmlDocument doc = new XmlDocument();

            if (!sourceFile.Exists)
            {
                throw new FileNotFoundException($"XML-File: {sourceFile.FullName} doesn't exist!");
            }

            try
            {
                doc.Load(FileSys.File.OpenRead(sourceFile.FullName));
            }catch(XmlException e)
            {
                throw new XmlException($"XML-File: {sourceFile.FullName} is corrupt!");
            }

            return doc;
        }



    }
}
