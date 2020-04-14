
using Com.Ing.DiBa.NotfallExporterLib.File;
using Com.Ing.DiBa.NotfallExporterLib.Idx;
using Com.Ing.DiBa.NotfallExporterLib.Util;
using Com.Ing.DiBa.NotfallExporterLib.Xml;
using System.IO;


namespace Com.Ing.DiBa.NotfallExporterLib.Export
{

/// <summary>
/// Class to Export a File
/// </summary>
    public class FileExporter :  IFileExporter
    {
        public ExportModel ExportModel { get; set; }

        private readonly IFileHandler _fileHandler;  
        private IdxBuilder _idxBuilder;

        public FileExporter(ExportModel model, IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;

            ExportModel = model;
            InitializeIdxBuilder();
        }

        public FileExporter(ExportModel model)
        {
            ExportModel = model;
            InitializeIdxBuilder();
        }

        private void InitializeIdxBuilder()
        {

            XmlAccountConfig accountConfig = new XmlAccountConfig(ExportModel.AccountConfig, _fileHandler);

            XmlIdxIndexSpecification indexSpecification = new XmlIdxIndexSpecification(ExportModel.IdxIndexSpecification, _fileHandler);

            _idxBuilder = new IdxBuilder(accountConfig, indexSpecification, _fileHandler);
        }



        public void Start(string sourceFile)
        {
            string importedFilePath = Path.Combine(ExportModel.ImportDirectory, Path.ChangeExtension(sourceFile.GetFileName(),"zip")); 

            //creates the Import File
            if(sourceFile.GetFileExtension().Equals("eml"))
            {
                _fileHandler.ZipEmailFileTo(sourceFile, ExportModel.ImportDirectory);
            }
            else
            {
                _fileHandler.FileSys.File.Copy(sourceFile, Path.Combine(ExportModel.ImportDirectory, sourceFile.GetFileName()));
            }
            Log.Logger.Info($"File: {sourceFile.GetFileName()} imported to Import-Directory");

            //creating an IdxFile
            _idxBuilder.BuildIdx(importedFilePath, ExportModel.ImportDirectory);

            _fileHandler.CreateReadyFile(importedFilePath);

            _fileHandler.BackupFile(sourceFile, ExportModel.BackupDirectory);

        }



    }
}
