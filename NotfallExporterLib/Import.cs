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
        public void start()
        {
            //creating an IdxFile
            IdxBuilder idxBuilder = new IdxBuilder(_destDirectory);
            idxBuilder.setFileSystem(_fileSystem);

            _idx = idxBuilder.CreateIdx(_filePath);

            //creates the Import File
            if(_filePath.getFileExtension().Equals("eml"))
            {
                ZipEml();
            }
            else
            {
                _fileSystem.File.Copy(_filePath, _destDirectory + "\\" + _filePath.GetFileName());
            }
            log.Info(String.Format("File: {0} imported to Import-Directory", _filePath.GetFileName()));

        }

        //creates a .rdy file in the import directory for the given file
        public void CreateRdy()
        {
            if (_fileSystem.File.Exists(_destDirectory + "\\" + _filePath.GetFileName().removeFileExtension() + ".zip") && _fileSystem.File.Exists(_idx._file))
            {
                _fileSystem.File.Create(_destDirectory + "\\" + _filePath.GetFileName().removeFileExtension() + ".rdy");
                log.Info(String.Format("Rdy File created: {0}\\{1}.rdy", _destDirectory, _filePath.GetFileName().removeFileExtension()));
            }
            else
            {
                log.Error(String.Format("Could not create Rdy File: {0}", _destDirectory + "\\" + _filePath.GetFileName().removeFileExtension() + ".rdy"));
            }

        }

         public void ZipEml()
        {

            string zipFilePath = _destDirectory + "\\" + _filePath.GetFileName().removeFileExtension() + ".zip";

           

            if (_fileSystem.File.Exists(zipFilePath))
                log.Warn(String.Format("File: {0} already exists in Import-Directory", zipFilePath.GetFileName()));
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
                    log.Info(String.Format("File: {0} zipped", _filePath.GetFileName()));
                }
        }

        public void setFileSystem(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
    }
}
