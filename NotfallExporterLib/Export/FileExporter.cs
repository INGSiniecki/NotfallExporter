
using Com.Ing.DiBa.NotfallExporterLib.File;
using Com.Ing.DiBa.NotfallExporterLib.Idx;
using Com.Ing.DiBa.NotfallExporterLib.Util;
using Com.Ing.DiBa.NotfallExporterLib.Xml;
using System.IO;


namespace Com.Ing.DiBa.NotfallExporterLib.Export
{

    /*
     * represents the Import of one File
     */
    public class FileExporter :  IFileExporter
    {
        public ExportModel ImportModel { get; set; }

        private IFileHandler _fileHandler;
        
        private IdxBuilder _idxBuilder;

        public FileExporter(ExportModel model, IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;

            ImportModel = model;
            InitializeIdxBuilder();
        }

        public FileExporter(ExportModel model)
        {
            ImportModel = model;
            InitializeIdxBuilder();
        }

        private void InitializeIdxBuilder()
        {

            XmlAccountConfig accountConfig = new XmlAccountConfig(ImportModel.AccountConfig, _fileHandler);

            XmlIdxIndexSpecification indexSpecification = new XmlIdxIndexSpecification(ImportModel.IdxIndexSpecification, _fileHandler);

            _idxBuilder = new IdxBuilder(accountConfig, indexSpecification, _fileHandler);
        }


        public void Start(string sourceFile)
        {
            string importedFilePath = Path.Combine(ImportModel.ImportDirectory, Path.ChangeExtension(sourceFile.GetFileName(),"zip")); 

            //creates the Import File
            if(sourceFile.GetFileExtension().Equals("eml"))
            {
                _fileHandler.ZipEmailFileTo(sourceFile, ImportModel.ImportDirectory);
            }
            else
            {
                _fileHandler.FileSys.File.Copy(sourceFile, Path.Combine(ImportModel.ImportDirectory, sourceFile.GetFileName()));
            }
            Log.Logger.Info($"File: {sourceFile.GetFileName()} imported to Import-Directory");

            //creating an IdxFile
            _idxBuilder.BuildIdx(importedFilePath, ImportModel.ImportDirectory);

            _fileHandler.CreateReadyFile(importedFilePath);

            _fileHandler.BackupFile(sourceFile, ImportModel.BackupDirectory);

        }



    }
}
