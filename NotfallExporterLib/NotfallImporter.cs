using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Abstractions;
using System.IO;
using System.Xml;

namespace NotfallExporterLib
{

    /*
     * manages the NotfallImport
     */
    public class NotfallImporter : NotfallImporterModel, INotfallImporter, FileSystemAbstraction
    {
        
        public NotfallImporter(ImportData model)
        {
            _fileSystem = new FileSystem();
            Data = model;


        }

        //tests if the directories exist
        private void CheckData()
        {
            if (!_fileSystem.Directory.Exists(Data.ErrorDirectory)) 
                throw new DirectoryNotFoundException($"{Data.ErrorDirectory} konnte nicht gefunden werden");

            if (!_fileSystem.Directory.Exists(Data.ImportDirectory))
                throw new DirectoryNotFoundException($"{Data.ImportDirectory} konnte nicht gefunden werden");

            if (!_fileSystem.Directory.Exists(Data.BackupDirectory))
                throw new DirectoryNotFoundException($"{Data.BackupDirectory} konnte nicht gefunden werden");
        }

        //starts the import 
        public void Start()
        {
            CheckData();

            log.Info($"Import has been started:\nError-Directory: {Data.ErrorDirectory}\nImport-Directory: {Data.ImportDirectory}\nBackup-Directory: {Data.BackupDirectory}");

            IdxBuilder idxBuilder = new IdxBuilder(Data.ImportDirectory);
            idxBuilder.SetFileSystem(_fileSystem);

            idxBuilder.AccountConfig = new XmlDocument();
            idxBuilder.AccountConfig.Load(_fileSystem.File.OpenRead(Data.AccountConfig));

            idxBuilder.IdxIndexSpecification = new XmlDocument();
            idxBuilder.IdxIndexSpecification.Load(_fileSystem.File.OpenRead(Data.IdxIndexSpecification));

            foreach (string importFile in ExtractImports())
            {
                Import import = new Import(Data.ImportDirectory, importFile);
                import.Start(idxBuilder);
                import.CreateRdy();
                Backup(importFile);
            }

            
        }

        //returns all eml and zip file in the error directory
        public List<string> ExtractImports()
        {
            string[] files = _fileSystem.Directory.GetFiles(Data.ErrorDirectory);

            List<string> importFiles = new List<string>();
            
            for(int i = 0; i < files.Length; i++)
            {
                if (files[i].GetFileExtension().Equals("eml") || files[i].GetFileExtension().Equals("zip"))
                    importFiles.Add(files[i]);
            }

            log.Info($"{importFiles.ToArray().Length} Import Files found");

            return importFiles;
        }

        //moves the Importfiles to the backup directory
        public void Backup(string file)
        {
            DateTime currentDate = DateTime.Today;
            string backupDirectoryPath = Path.Combine(Data.BackupDirectory,"Backup",currentDate.ToString("dd_MM_yy"));

            //creates a daily-directory in case it doesn't exist.
            if (!_fileSystem.Directory.Exists(backupDirectoryPath))
                _fileSystem.Directory.CreateDirectory(backupDirectoryPath);

            string backupFilePath = Path.Combine(backupDirectoryPath, file.GetFileName());

            if (_fileSystem.File.Exists(backupFilePath))
                log.Warn($"File: {backupFilePath.GetFileName()} already exists in Backup-Directory" );
            else
            {
                _fileSystem.File.Move(file, backupFilePath);
                log.Info($"File: {backupFilePath.GetFileName()} moved to Backup-Directory");
            }
        }

        public void SetFileSystem(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
    }
}
