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
    public class NotfallImporter : NotfallImporterModel, INotfallImporter
    {
        
        public NotfallImporter(ImportData model, IFileSystem fileSystem = null)
        {
            if (fileSystem == null)
                _fileSystem = new FileSystem();
            else
                _fileSystem = fileSystem;

            Data = model;

            _idxBuilder = new IdxBuilder(Data.ImportDirectory, _fileSystem);

            _idxBuilder.AccountConfig = new XmlDocument();
            _idxBuilder.AccountConfig.Load(_fileSystem.File.OpenRead(Data.AccountConfig));

            _idxBuilder.IdxIndexSpecification = new XmlDocument();
            _idxBuilder.IdxIndexSpecification.Load(_fileSystem.File.OpenRead(Data.IdxIndexSpecification));

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

            Log.Info($"Import has been started:\nError-Directory: {Data.ErrorDirectory}\nImport-Directory: {Data.ImportDirectory}\nBackup-Directory: {Data.BackupDirectory}");

           

            foreach (string importFile in ExtractImports())
            {
                Import import = new Import(Data.ImportDirectory, importFile, _fileSystem);
                import.Start(_idxBuilder);
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

            Log.Info($"{importFiles.ToArray().Length} Import Files found");

            return importFiles;
        }

        //moves the Importfiles to the backup directory
        public void Backup(string file)
        {
            DateTime currentDate = DateTime.Today;
            string backupDirectoryPath = Path.Combine(Data.BackupDirectory,"Backup"+currentDate.ToString("dd_MM_yy"));

            //creates a daily-directory in case it doesn't exist.
            if (!_fileSystem.Directory.Exists(backupDirectoryPath))
                _fileSystem.Directory.CreateDirectory(backupDirectoryPath);

            string backupFilePath = Path.Combine(backupDirectoryPath, file.GetFileName());

            if (_fileSystem.File.Exists(backupFilePath))
                Log.Warn($"File: {backupFilePath.GetFileName()} already exists in Backup-Directory" );
            else
            {
                _fileSystem.File.Move(file, backupFilePath);
                Log.Info($"File: {backupFilePath.GetFileName()} moved to Backup-Directory");
            }
        }

    }
}
