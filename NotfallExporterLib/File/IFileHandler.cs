﻿
using Com.Ing.DiBa.NotfallExporterLib.Api;
using NotfallExporterLib.Database;
using System.Collections.ObjectModel;
using System.IO.Abstractions;
using System.IO.Compression;
using System.Xml;

namespace Com.Ing.DiBa.NotfallExporterLib.File
{
    public interface IFileHandler
    {
        ISqliteDataAccess DbService { get; set; }
        IMessenger Messenger { get; set; }

        IFileSystem FileSys { get; set; }
       
        void CreateReadyFile(IFileInfo sourceFile);

        ReadOnlyCollection<ZipArchiveEntry> getZipArchiveEntries(IFileInfo zipFile);

       
        IFileInfo[] GetImportFiles(string directoryPath);

       
        void BackupFile(IFileInfo sourceFile, string backupDirectoryPath);

      
        bool CheckModel(ExportModel model);

        IFileInfo ExportEmlFile(IFileInfo sourceFile, string destDirectoryPath);

        IFileInfo ExportZipFile(IFileInfo sourceFile, string destDirectoryPath);

        XmlDocument LoadXmlFile(IFileInfo sourceFile);


    }
}
