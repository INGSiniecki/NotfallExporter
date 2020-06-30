
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
        public ExportModel ExportModel { get; }

        private readonly IFileExporter _fileExporter;

        private IMessenger _messenger;
        public IMessenger Messenger 
        {
            get => _messenger;
            set 
            {
                _fileExporter.Messenger = value;
                _messenger = value;
                
            }
            
        }

        private readonly IFileHandler _fileHandler;


        /// <summary>
        /// instantiate a new object of te DirectoryExporter-Class
        /// </summary>
        /// <param name="model">contains Path-Informations for exporting</param>
        /// <param name="fileHandler">object for FileSystem operations</param>
        public DirectoryExporter(IFileExporter fileExporter)
        {
            _fileExporter = fileExporter;
            ExportModel = fileExporter.ExportModel;
            _fileHandler = fileExporter.FileHandler;
        }




        /// <summary>
        /// starts the Import of the given Directory
        /// </summary>
        public void Start()
        {

            Log.Logger.Info($"Import has been started:\nError-Directory: {ExportModel.ErrorDirectory}\nImport-Directory: {ExportModel.ImportDirectory}\nBackup-Directory: {ExportModel.BackupDirectory}");

            

            try
            {
                _fileExporter.InitializeIdxBuilder();
                StartExport();
            } catch (Exception e)
            {
                Messenger.sendError(e);
            }
        }


        private void StartExport()
        {

            long startTime = DateTime.Now.Millisecond;
            int fileImportCount = 0;

            foreach (IFileInfo exportFileInfo in _fileHandler.GetImportFiles(ExportModel.ErrorDirectory))
            {
                ExportFile exportFile = new ExportFile(exportFileInfo);
                exportFile.BuilData();

                if (exportFile.Data != null)
                {
                    _fileExporter.Start(exportFile);
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
