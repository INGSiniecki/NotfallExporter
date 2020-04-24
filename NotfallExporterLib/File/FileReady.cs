using Com.Ing.DiBa.NotfallExporterLib.Util;
using System.IO;
using System.IO.Abstractions;

namespace Com.Ing.DiBa.NotfallExporterLib.File
{
    public class FileReady
    {
        private readonly IFileSystem _fileSystem;
        private readonly IFileInfo _sourceFile;
        public FileReady(IFileInfo sourceFile, IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _sourceFile = sourceFile;
        }

        public void Create()
        {
            IFileInfo readyFile = _fileSystem.FileInfo.FromFileName(Path.ChangeExtension(_sourceFile.FullName, "rdy"));

            if (readyFile.Exists)
            {
                Log.Logger.Warn($"Rdy-File already exitsts: {readyFile.Name}");
            }
            else if (_fileSystem.File.Exists(_sourceFile.FullName) && _fileSystem.File.Exists(Path.ChangeExtension(_sourceFile.FullName, "idx")))
            {
                _fileSystem.File.Create(Path.ChangeExtension(_sourceFile.FullName, "rdy"));
                Log.Logger.Info($"Rdy-File created: {readyFile}");
            }
            else
            {
                string errorMessage = $"Could not create Rdy File for File: {_sourceFile}";
                Log.Logger.Error(errorMessage);
            }
        }
    }
}
