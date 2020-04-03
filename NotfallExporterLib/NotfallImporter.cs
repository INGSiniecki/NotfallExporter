using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Abstractions;
using System.IO;
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
            _data = model;


        }

        //tests if the directories exist
        private void CheckData()
        {
            if (!_fileSystem.Directory.Exists(_data._error_directory)) 
                throw new DirectoryNotFoundException(String.Format("{0} konnte nicht gefunden werden", _data._error_directory));

            if (!_fileSystem.Directory.Exists(_data._import_directory))
                throw new DirectoryNotFoundException(String.Format("{0} konnte nicht gefunden werden", _data._import_directory));

            if (!_fileSystem.Directory.Exists(_data._backup_directory))
                throw new DirectoryNotFoundException(String.Format("{0} konnte nicht gefunden werden", _data._backup_directory));
        }

        //starts the import 
        public void Start()
        {
            CheckData();

            log.Info(String.Format("Import has been started:\nError-Directory: {0}\nImport-Directory: {1}\nBackup-Directory: {2}", _data._error_directory, _data._import_directory, _data._backup_directory));

            foreach (string importFile in ExtractImports())
            {
                Import import = new Import(_data._import_directory, importFile);
                import.start();
                import.CreateRdy();
                Backup(importFile);
            }

            
        }

        //returns all eml and zip file in the error directory
        public List<string> ExtractImports()
        {
            string[] files = _fileSystem.Directory.GetFiles(_data._error_directory);

            List<string> importFiles = new List<string>();
            
            for(int i = 0; i < files.Length; i++)
            {
                if (files[i].getFileExtension().Equals("eml") || files[i].getFileExtension().Equals("zip"))
                    importFiles.Add(files[i]);
            }

            log.Info(String.Format("{0} Import Files found", importFiles.ToArray().Length));

            return importFiles;
        }

        //moves the Importfiles to the backup directory
        public void Backup(string file)
        {
            DateTime currentDate = DateTime.Today;
            string backupDirectoryPath = _data._backup_directory + "\\Backup" + currentDate.ToString("dd_MM_yy");

            //creates a daily-directory in case it doesn't exist.
            if (!_fileSystem.Directory.Exists(backupDirectoryPath))
                _fileSystem.Directory.CreateDirectory(backupDirectoryPath);

            string backupFilePath = backupDirectoryPath + "\\" + file.GetFileName();

            if (_fileSystem.File.Exists(backupFilePath))
                log.Warn(String.Format("File: {0} already exists in Backup-Directory", backupFilePath.GetFileName()));
            else
            {
                _fileSystem.File.Move(file, backupFilePath);
                log.Info(String.Format("File: {0} moved to Backup-Directory", backupFilePath.GetFileName()));
            }
        }

        public void setFileSystem(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
    }
}
