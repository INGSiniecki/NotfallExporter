
using Com.Ing.DiBa.NotfallExporterLib.File;
using Com.Ing.DiBa.NotfallExporterLib.Idx;
using Com.Ing.DiBa.NotfallExporterLib.Util;
using Com.Ing.DiBa.NotfallExporterLib.Xml;
using System.IO;
using System.IO.Abstractions;

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

        public event ExportEventHandler FileExportEvent;

        /// <summary>
        /// instantiate a new object of the FileExporter-Class
        /// </summary>
        /// <param name="model">contains Path-Informations for exporting</param>
        /// <param name="fileHandler">object for FileSystem operations</param>
        public FileExporter(ExportModel model, IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;

            ExportModel = model;
            InitializeIdxBuilder();
        }

        /// <summary>
        ///  instantiate a new object of the FileExporter-Class
        /// </summary>
        /// <param name="model">contains Path-Infromations for exporting</param>
        public FileExporter(ExportModel model)
        {
            ExportModel = model;
            InitializeIdxBuilder();
        }

       


        /// <summary>
        /// starts the import of the file
        /// </summary>
        /// <param name="idxBuilder">instance of IdxBuilder for building Idx-Files</param>
        public void Start(IFileInfo sourceFile)
        {

            string importedFilePath = Path.Combine(ExportModel.ImportDirectory, Path.ChangeExtension(sourceFile.Name,"zip")); 

            //creates the Import File
            if(sourceFile.Name.GetFileExtension().Equals("eml"))
            {
                _fileHandler.ZipEmailFileTo(sourceFile, ExportModel.ImportDirectory);
            }
            else
            {
                _fileHandler.exportFile(sourceFile, Path.Combine(ExportModel.ImportDirectory));
            }
            Log.Logger.Info($"File: {sourceFile.Name} imported to Import-Directory");

            //creating an IdxFile
            _idxBuilder.BuildIdx(importedFilePath, ExportModel.ImportDirectory);

            _fileHandler.CreateReadyFile(importedFilePath);

            _fileHandler.BackupFile(sourceFile.FullName, ExportModel.BackupDirectory);

            FileExportEvent(this, sourceFile.Name);

        }

        private void InitializeIdxBuilder()
        {

            XmlAccountConfig accountConfig = new XmlAccountConfig(ExportModel.AccountConfig, _fileHandler);

            XmlIdxIndexSpecification indexSpecification = new XmlIdxIndexSpecification(ExportModel.IdxIndexSpecification, _fileHandler);

            _idxBuilder = new IdxBuilder(accountConfig, indexSpecification, _fileHandler);
        }

    }
}
