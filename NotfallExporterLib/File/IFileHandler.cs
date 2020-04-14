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

        /// <summary>
        /// Creates a Ready file from a given file.
        /// </summary>
        /// <param name="sourceFile">file to craete a Ready-File from</param>
        void CreateReadyFile(string sourceFile);


        /// <summary>
        /// Converts a Email-File into a Zip-File and moves it to a Directory.
        /// </summary>
        /// <param name="sourceFile">Email-File to convert</param>
        /// <param name="destDirectory">Destination-Directory</param>
        void ZipEmailFileTo(string sourceFile, string destDirectory);

        /// <summary>
        /// returns all Entries of a zip File
        /// </summary>
        /// <param name="zipFile">Zip-File to get the entries from</param>
        /// <returns>Collection of ZipEntries</returns>
        ReadOnlyCollection<ZipArchiveEntry> getZipArchiveEntries(string zipFile);

        /// <summary>
        /// returns .zip and .eml files in a given Directory.
        /// </summary>
        /// <param name="directoryPath">Directory to extract import files from</param>
        /// <returns>string[] of files to import</returns>
        string[] GetImportFiles(string directory);

        /// <summary>
        /// Creates a Day-Backup-Directory when it is not yet existing and moves a file into it.
        /// 
        /// </summary>
        /// <param name="sourceFile">file to backup</param>
        /// <param name="backupDirectory">Backup-Directory</param>
        void BackupFile(string sourceFile, string backupDirectory);

        /// <summary>
        /// checks wether the paths of a ExportModel exist.
        /// </summary>
        /// <param name="model"></param>
        void checkModel(ExportModel model);
    }
}
