
using Com.Ing.DiBa.NotfallExporterLib.File;
using Com.Ing.DiBa.NotfallExporterLib.Util;
using System.IO.Abstractions;

namespace Com.Ing.DiBa.NotfallExporterLib.Export
{
    public delegate void ExportEventHandler(object eventSender, string exportedElement);
    /// <summary>
    /// Class to Export a Directory.
    /// </summary>
    public class DirectoryExporter :  IDirectoryExporter
    {
        public event ExportEventHandler FileExportEvent;
        public event ExportEventHandler DirectoryExportEvent;

        /// <summary>
        /// contains Path-Information for exporting
        /// </summary>
        public ExportModel ImportModel { get;}

        private readonly IFileHandler _fileHandler;

        /// <summary>
        /// instantiate a new object of te DirectoryExporter-Class
        /// </summary>
        /// <param name="model">contains Path-Informations for exporting</param>
        public DirectoryExporter(ExportModel model)
        {
            ImportModel = model;
            _fileHandler = new FileHandler();
        }

        /// <summary>
        /// instantiate a new object of te DirectoryExporter-Class
        /// </summary>
        /// <param name="model">contains Path-Informations for exporting</param>
        /// <param name="fileHandler">object for FileSystem operations</param>
        public DirectoryExporter(ExportModel model, IFileHandler fileHandler)
        {
            ImportModel = model;
            _fileHandler = fileHandler;
        }



        /// <summary>
        /// starts the Import of the given Directory
        /// </summary>
        public void Start()
        {
            _fileHandler.checkModel(ImportModel);

            Log.Logger.Info($"Import has been started:\nError-Directory: {ImportModel.ErrorDirectory}\nImport-Directory: {ImportModel.ImportDirectory}\nBackup-Directory: {ImportModel.BackupDirectory}");
            FileExporter fileExporter = new FileExporter(ImportModel, _fileHandler);

            fileExporter.FileExportEvent += (eventSender, importedElement) => FileExportEvent(eventSender, importedElement);

            foreach (IFileInfo importFile in _fileHandler.GetImportFiles(ImportModel.ErrorDirectory))
            {
                fileExporter.Start(importFile);
            }

            onExportDirectoryEvent(ImportModel.ErrorDirectory);
        }

        private void onExportDirectoryEvent(string exportedElement)
        {
            ExportEventHandler handler = DirectoryExportEvent;
            handler?.Invoke(this, exportedElement);
        }


   
    }
}
