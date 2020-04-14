
using Com.Ing.DiBa.NotfallExporterLib.File;
using Com.Ing.DiBa.NotfallExporterLib.Util;

namespace Com.Ing.DiBa.NotfallExporterLib.Export
{

    /// <summary>
    /// Class to Export a Directory.
    /// </summary>
    public class DirectoryExporter :  IDirectoryExporter
    {
        public ExportModel ImportModel { get;}

        private readonly IFileHandler _fileHandler;

        public DirectoryExporter(ExportModel model)
        {
            ImportModel = model;
            _fileHandler = new FileHandler();
        }

        public DirectoryExporter(ExportModel model, IFileHandler fileHandler)
        {
            ImportModel = model;
            _fileHandler = fileHandler;
        }



        public void Start()
        {
            _fileHandler.checkModel(ImportModel);

            Log.Logger.Info($"Import has been started:\nError-Directory: {ImportModel.ErrorDirectory}\nImport-Directory: {ImportModel.ImportDirectory}\nBackup-Directory: {ImportModel.BackupDirectory}");
            FileExporter import = new FileExporter(ImportModel, _fileHandler);


            foreach (string importFile in _fileHandler.GetImportFiles(ImportModel.ErrorDirectory))
            {
                import.Start(importFile);
            }
            
        }



    }
}
