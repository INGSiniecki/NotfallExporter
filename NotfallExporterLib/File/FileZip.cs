

using Com.Ing.DiBa.NotfallExporterLib.Util;
using System.IO;
using System.IO.Abstractions;
using System.IO.Compression;

namespace Com.Ing.DiBa.NotfallExporterLib.File
{
    public class FileZip
    {
        private readonly IFileSystem _fileSystem;
        private readonly IFileInfo _sourceFile;
        public FileZip(IFileInfo sourceFile, IFileSystem fileSystem)
        {
            _sourceFile = sourceFile;
            _fileSystem = fileSystem;
        }

        public IFileInfo ZipFile(IDirectoryInfo destDirectory)
        {
            IFileInfo zipFile = _fileSystem.FileInfo.FromFileName(Path.Combine(destDirectory.FullName, Path.ChangeExtension(_sourceFile.Name, ".zip")));

            if (zipFile.Exists)
            {
                string warnMessage = $"File: {zipFile.Name} already exists in Import-Directory";
                Log.Logger.Warn(warnMessage);
            }
            else
            {
                 CreateZipArchive(zipFile);
            }
            return zipFile;
        }

        private void CreateZipArchive(IFileInfo zipFile)
        {
            using (ZipArchive archive = new ZipArchive(zipFile.Create(), ZipArchiveMode.Create))
            {
                addEntryFromSourceFile(archive);
            }
        }

        private void addEntryFromSourceFile(ZipArchive archive)
        {
            ZipArchiveEntry zipElement = archive.CreateEntry(_sourceFile.Name);

            using (MemoryStream originalFileMemoryStream = new MemoryStream(_fileSystem.File.ReadAllBytes(_sourceFile.FullName)))
            {
                using (Stream zipElementStream = zipElement.Open())
                {
                    originalFileMemoryStream.CopyTo(zipElementStream);
                }
            }
            Log.Logger.Info($"File: {_sourceFile.Name} zipped");
        }
    }
}
