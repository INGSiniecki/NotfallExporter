
using Com.Ing.DiBa.NotfallExporterLib.File;
using Com.Ing.DiBa.NotfallExporterLib.Util;
using System;
using System.IO.Abstractions;
using Com.Ing.DiBa.NotfallExporterLib.File.Export;

namespace Com.Ing.DiBa.NotfallExporterLib.Api
{


    /// <summary>
    /// Class to Export a Directory.
    /// </summary>
    public class DirectoryExporter : IDirectoryExporter
    {

        /// <summary>
        /// contains Path-Information for exporting
        /// </summary>
        public ExportModel ImportModel { get; }
        public IMessenger Messenger { get; set; }

        private readonly IFileHandler _fileHandler;

        /// <summary>
        /// instantiate a new object of te DirectoryExporter-Class
        /// </summary>
        /// <param name="model">contains Path-Informations for exporting</param>
        public DirectoryExporter(ExportModel model) : this(model, new FileHandler())
        {

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

            Log.Logger.Info($"Import has been started:\nError-Directory: {ImportModel.ErrorDirectory}\nImport-Directory: {ImportModel.ImportDirectory}\nBackup-Directory: {ImportModel.BackupDirectory}");
            FileExporter fileExporter = new FileExporter(ImportModel, _fileHandler);

            fileExporter.Messenger = Messenger;
            _fileHandler.Messenger = Messenger;

            startExport(fileExporter);

        }

        private void startExport(FileExporter fileExporter)
        {

            long startTime = DateTime.Now.Millisecond;
            int fileImportCount = 0;

            foreach (IFileInfo exportFileInfo in _fileHandler.GetImportFiles(ImportModel.ErrorDirectory))
            {
                ExportFile exportFile = new ExportFile(exportFileInfo);
                exportFile.BuilData();

                if (exportFile.Data != null)
                {
                    fileExporter.Start(exportFile);
                    fileImportCount++;
                }
                else
                {
                    Messenger?.SendMessage($"{exportFile.File.Name} couldn't be exported!");
                }

            }
            Inform(fileImportCount, DateTime.Now.Millisecond - startTime);

        }

        private void Inform(int fileImportCount, long deltaTime)
        {
            if(fileImportCount > 0)
            {
                Messenger?.SendMessage($"{fileImportCount} files exported!");
                Messenger?.SendMessage($"Duration: {deltaTime}ms");
            } else
            {
                Messenger?.SendMessage($"Error-Directory empty!");
            }
        }

    }
}
