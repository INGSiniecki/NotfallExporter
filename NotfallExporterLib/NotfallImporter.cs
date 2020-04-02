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
    public class NotfallImporter : FileSystemAbstraction
    {
        public ImportModel _model { get; set; }
        private static  log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public NotfallImporter(ImportModel model, IFileSystem fileSystem = null) : base(fileSystem)
        {

            _model = model;

            checkModel();
        }

        //tests if the directories exist
        private void checkModel()
        {
            if (!_fileSystem.Directory.Exists(_model._error_directory)) 
                throw new DirectoryNotFoundException(String.Format("{0} konnte nicht gefunden werden", _model._error_directory));

            if (!_fileSystem.Directory.Exists(_model._import_directory))
                throw new DirectoryNotFoundException(String.Format("{0} konnte nicht gefunden werden", _model._import_directory));

            if (!_fileSystem.Directory.Exists(_model._backup_directory))
                throw new DirectoryNotFoundException(String.Format("{0} konnte nicht gefunden werden", _model._backup_directory));
        }

        //starts the import 
        public void start()
        {

            log.Info(String.Format("Import has been started:\nError-Directory: {0}\nImport-Directory: {1}\nBackup-Directory: {2}", _model._error_directory, _model._import_directory, _model._backup_directory));

            foreach (string importFile in extractImports())
            {
                Import import = new Import(_model._import_directory, importFile, _fileSystem);
                import.start();
                import.CreateRdy();
                backup(importFile);
            }

            
        }

        //returns all eml and zip file in the error directory
        protected List<string> extractImports()
        {
            string[] files = _fileSystem.Directory.GetFiles(_model._error_directory);

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
        protected void backup(string file)
        {
            DateTime currentDate = DateTime.Today;
            string backupDirectoryPath = _model._backup_directory + "\\Backup" + currentDate.ToString("dd_MM_yy");

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
    }
}
