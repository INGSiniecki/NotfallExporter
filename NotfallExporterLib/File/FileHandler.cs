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
using System.Xml;

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
        public void BackupFile(string sourceFile, string backupDirectory)
        {
            DateTime currentDate = DateTime.Today;
            string backupDirectoryPath = Path.Combine(backupDirectory, "Backup" + currentDate.ToString("dd_MM_yy"));

            //creates a daily-directory in case it doesn't exist.
            if (!FileSys.Directory.Exists(backupDirectoryPath))
                FileSys.Directory.CreateDirectory(backupDirectoryPath);

            string backupFilePath = Path.Combine(backupDirectoryPath, sourceFile.GetFileName());

            if (FileSys.File.Exists(backupFilePath))
            {
                string warnMessage = $"File: {backupFilePath.GetFileName()} already exists in Backup-Directory";
                Log.Logger.Warn(warnMessage);
                onWarnEvent(warnMessage);
                FileSys.File.Move(sourceFile, Path.Combine(backupDirectoryPath, $"{sourceFile.GetFileName().RemoveFileExtension()} Copy.{sourceFile.GetFileExtension()}" ));
            }
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
        public bool CheckModel(ExportModel model)
        {
            try
            {
                if (!FileSys.Directory.Exists(model.ErrorDirectory))
                    throw new DirectoryNotFoundException($"{model.ErrorDirectory} was not found");

                if (!FileSys.Directory.Exists(model.ImportDirectory))
                    throw new DirectoryNotFoundException($"{model.ImportDirectory} was not found");

                if (!FileSys.Directory.Exists(model.BackupDirectory))
                    throw new DirectoryNotFoundException($"{model.BackupDirectory} was not found");

                if (!FileSys.File.Exists(model.AccountConfig))
                    throw new FileNotFoundException($"{model.AccountConfig} was not found");

                if (!FileSys.File.Exists(model.IdxIndexSpecification))
                    throw new FileNotFoundException($"{model.IdxIndexSpecification} was not found");
            }catch(Exception exception)
            {
                
                onErrorEvent(exception);
                return false;
            }
            return true;
        }


        /// <summary>
        /// Creates a Ready file from a given file.
        /// </summary>
        /// <param name="sourceFile">file to ceate a Ready-File from</param>
        public void CreateReadyFile(string sourceFile)
        {
            string readyFile = Path.ChangeExtension(sourceFile, "rdy");

            if (FileSys.File.Exists(readyFile))
            {
                onWarnEvent($"File: {readyFile.GetFileName()} already exists in Import-Directory");
            }
            else if (FileSys.File.Exists(sourceFile) && FileSys.File.Exists(Path.ChangeExtension(sourceFile, "idx")))
            {
                FileSys.File.Create(readyFile);
                Log.Logger.Info($"Rdy File created: {readyFile}");
            }
            else
            {
                string errorMessage = $"Could not create Rdy File for File: {sourceFile}";
                Log.Logger.Error(errorMessage);
                onErrorEvent(new Exception(errorMessage));
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
            {
                string warnMessage = $"File: {zipFilePath.GetFileName()} already exists in Import-Directory";
                Log.Logger.Warn(warnMessage);
                onWarnEvent(warnMessage);
            }
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

        /// <summary>
        /// moves a file to the given Directory
        /// </summary>
        /// <param name="sourceFile">File to move</param>
        /// <param name="destDirectory">Directory to move the file in</param>
        public void exportFile(IFileInfo sourceFile, string destDirectory)
        {
            string destFilePath = Path.Combine(destDirectory, sourceFile.Name);
            if (!FileSys.File.Exists(destFilePath))
            {
                FileSys.File.Copy(sourceFile.FullName, destFilePath);
            }
            else
            {
                onWarnEvent($"{sourceFile.Name} already exists in Import-Directory");
            }
        }
        /// <summary>
        /// loads and Xml-File
        /// </summary>
        /// <param name="path">Path to the Xml-File</param>
        /// <returns>XmlDocument object to represent the XMl-File</returns>
        public XmlDocument LoadXml(string path)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(path);
            }
            catch(XmlException e)
            {
                onErrorEvent(new XmlException($"{path.GetFileName()} is corrupt!"));
            }
            return xml;
        }


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
