
using Com.Ing.DiBa.NotfallExporterLib.File;
using Com.Ing.DiBa.NotfallExporterLib.Idx;
using Com.Ing.DiBa.NotfallExporterLib.Util;
using System.IO.Abstractions;
using Com.Ing.DiBa.NotfallExporterLib.File.Xml;
using Com.Ing.DiBa.NotfallExporterLib.File.Export;
using System;

namespace Com.Ing.DiBa.NotfallExporterLib.Api
{

/// <summary>
/// Class to Export a File
/// </summary>
    public class FileExporter :  IFileExporter
    {

        public ExportModel ExportModel { get; set; }

        private IMessenger _messenger;
        public IMessenger Messenger 
        { 
            get => _messenger;
            set
            {
                FileHandler.Messenger = value;
                _messenger = value;
            } 
        }
        public IFileHandler FileHandler { get; set; }

        private IIdxBuilder _idxBuilder;

        /// <summary>
        /// instantiate a new object of the FileExporter-Class
        /// </summary>
        /// <param name="model">contains Path-Informations for exporting</param>
        /// <param name="fileHandler">object for FileSystem operations</param>
        public FileExporter(ExportModel model, IFileHandler fileHandler)
        {
            FileHandler = fileHandler;

            ExportModel = model;
        }

        /// <summary>
        ///  instantiate a new object of the FileExporter-Class
        /// </summary>
        /// <param name="model">contains Path-Infromations for exporting</param>
        public FileExporter(ExportModel model) : this(model, new FileHandler())
        {
        }

        /// <summary>
        /// starts the import of the file
        /// </summary>
        /// <param name="sourceFile">source-File</param>
        public void Start(ExportFile sourceFile)
        {

            ExportFile exportedFile = new ExportFile(exportFile(sourceFile));
            exportedFile.Data = sourceFile.Data;

            try
            {
                _idxBuilder.BuildIdx(exportedFile);

                FileHandler.CreateReadyFile(exportedFile.File);

                FileHandler.BackupFile(sourceFile.File, ExportModel.BackupDirectory);

                Messenger?.SendMessage($"{sourceFile.File.Name} exported!");
            }catch(Exception e)
            {
                Messenger?.SendMessage($"Export of File {sourceFile.File.Name} failed!");
            }
        }

        private IFileInfo exportFile(ExportFile sourceFile)
        {
            IFileInfo exportedFile;
            //creates the Import File
            if (sourceFile.Data.DoxisUser.Equals("eml"))
            {
               exportedFile = FileHandler.ExportEmlFile(sourceFile.File, ExportModel.ImportDirectory);
            }
            else
            {
               exportedFile =  FileHandler.ExportZipFile(sourceFile.File, ExportModel.ImportDirectory);
            }
            Log.Logger.Info($"File: {sourceFile.File.Name} imported to Import-Directory");

            return exportedFile;
        }

        /// <summary>
        /// initializes the IdxBuilder object of the FileExporter
        /// </summary>
        public void InitializeIdxBuilder()
        {

             XmlAccountConfig accountConfig = new XmlAccountConfig(ExportModel.AccountConfig, FileHandler);
             XmlIdxIndexSpecification indexSpecification = new XmlIdxIndexSpecification(ExportModel.IdxIndexSpecification, FileHandler);
            
            _idxBuilder = new IdxBuilder(accountConfig, indexSpecification, FileHandler);
        }

    }
}
