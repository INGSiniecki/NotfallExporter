using Com.Ing.DiBa.NotfallExporterLib.Util;
using System;
using System.IO;
using System.IO.Abstractions;


namespace Com.Ing.DiBa.NotfallExporterLib.File
{
    public class FileBackup
    {
        private readonly IDirectoryInfo _backupDirectory;
        private readonly IFileSystem _fileSystem;
        public FileBackup(IDirectoryInfo backupdirectory, IFileSystem fileSystem)
        {
            _backupDirectory = backupdirectory;
            _fileSystem = fileSystem;
        }

        private IDirectoryInfo CreateDailyBackupDirectory()
        {
            DateTime currentDate = DateTime.Today;
            string dailyBackupDirectoryPath = Path.Combine(_backupDirectory.FullName, "Backup" + currentDate.ToString("dd_MM_yy"));

            if (!_fileSystem.Directory.Exists(dailyBackupDirectoryPath))
            {
                return _fileSystem.Directory.CreateDirectory(dailyBackupDirectoryPath);
            }

            return _fileSystem.DirectoryInfo.FromDirectoryName(dailyBackupDirectoryPath);
        }

        public void BackupFile(IFileInfo file)
        {
            IDirectoryInfo dailyBackupDirectoryPath = CreateDailyBackupDirectory();

            string backupFilePath = Path.Combine(dailyBackupDirectoryPath.FullName, file.Name);

            if (_fileSystem.File.Exists(backupFilePath))
            {
                file.Delete();
                string warnMessage = $"File: {backupFilePath.GetFileName()} already exists in Backup-Directory";
                Log.Logger.Warn(warnMessage);
            }
            else
            {
                _fileSystem.File.Move(file.FullName, backupFilePath);
                Log.Logger.Info($"File: {backupFilePath.GetFileName()} moved to Backup-Directory");
            }

        }
    }
}
