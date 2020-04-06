using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Abstractions;
using System.IO.Compression;
using System.IO;

namespace NotfallExporterLib
{

    /*
     * represents the Import of one File
     */
    public class Import : ImportModel, IImport, FileSystemAbstraction
    {
        public Import(string dest_directory, string file)
        {
            _fileSystem = new FileSystem();

            _destDirectory = dest_directory;
            _filePath = file;
        }

        //starts the import of the given file
        public void Start(IdxBuilder idxBuilder)
        {
            string importedFilePath; 
            //creates the Import File
            if(_filePath.GetFileExtension().Equals("eml"))
            {
                importedFilePath = ZipEml();
            }
            else
            {
                _fileSystem.File.Copy(_filePath, Path.Combine(_destDirectory, _filePath.GetFileName()));
                importedFilePath = Path.Combine(_destDirectory, _filePath.GetFileName());
            }
            log.Info($"File: {_filePath.GetFileName()} imported to Import-Directory");

            //creating an IdxFile
            _idx = idxBuilder.CreateIdx(importedFilePath);

        }

        //creates a .rdy file in the import directory for the given file
        public void CreateRdy()
        {
            if (_fileSystem.File.Exists(Path.Combine(_destDirectory, _filePath.GetFileName().RemoveFileExtension() + ".zip")) && _fileSystem.File.Exists(_idx.File))
            {
                _fileSystem.File.Create(Path.Combine(_destDirectory, _filePath.GetFileName().RemoveFileExtension() + ".rdy"));
                log.Info($"Rdy File created: {Path.Combine(_destDirectory, _filePath.GetFileName().RemoveFileExtension())}.rdy");
            }
            else
            {
                log.Error($"Could not create Rdy File: {Path.Combine(_destDirectory, _filePath.GetFileName().RemoveFileExtension())}.rdy");
            }

        }

         public string ZipEml()
        {

            string zipFilePath = Path.Combine(_destDirectory, _filePath.GetFileName().RemoveFileExtension() + ".zip");

           

            if (_fileSystem.File.Exists(zipFilePath))
                log.Warn($"File: {zipFilePath.GetFileName()} already exists in Import-Directory");
            else
                using (ZipArchive archive = new ZipArchive(_fileSystem.File.Create(zipFilePath), ZipArchiveMode.Create))
                {
                    ZipArchiveEntry zipElement = archive.CreateEntry(_filePath.GetFileName());

                    using (MemoryStream originalFileMemoryStream = new MemoryStream(_fileSystem.File.ReadAllBytes(_filePath)))
                    {
                        using(Stream zipElementStream = zipElement.Open())
                        {
                            originalFileMemoryStream.CopyTo(zipElementStream);
                        }
                    }
                    log.Info($"File: {_filePath.GetFileName()} zipped");
                }
            return zipFilePath;
        }

        public void SetFileSystem(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
    }
}
