﻿
using Com.Ing.DiBa.NotfallExporterLib.File;
using Com.Ing.DiBa.NotfallExporterLib.Util;
using Com.Ing.DiBa.NotfallExporterLib.Event;
using System;
using System.IO.Abstractions;
using System.Xml;
using Com.Ing.DiBa.NotfallExporterLib.File.Export;

namespace Com.Ing.DiBa.NotfallExporterLib.Api
{


    /// <summary>
    /// Class to Export a Directory.
    /// </summary>
    public class DirectoryExporter : IDirectoryExporter
    {
        /// <summary>
        /// object to handle FileExportEvents
        /// </summary>
        public event FileExportEventHandler FileExportEvent;
        /// <summary>
        /// object to handle DirectoyExportEvents
        /// </summary>
        public event DirectoryExportEventHandler DirectoryExportEvent;
        /// <summary>
        /// object to handle ErrorEvents
        /// </summary>
        public event ErrorEventHandler ErrorEvent;
        /// <summary>
        /// object to handle WarnEvents
        /// </summary>
        public event WarnEventHandler WarnEvent;

        /// <summary>
        /// contains Path-Information for exporting
        /// </summary>
        public ExportModel ImportModel { get; }

        private readonly IFileHandler _fileHandler;

        private bool _errorThrown = false;

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
            _fileHandler.ErrorEvent += (eventSender, args) => handleErrorEvent(eventSender, args);
            _fileHandler.WarnEvent += (eventSender, args) => WarnEvent(eventSender, args);
        }

        private void handleErrorEvent(object eventSender, ErrorEventArgs args)
        {
            if (args.Exception is XmlException)
            {
                _errorThrown = true;
            }
            ErrorEvent?.Invoke(eventSender, args);
        }



        /// <summary>
        /// starts the Import of the given Directory
        /// </summary>
        public void Start()
        {

            if (_fileHandler.CheckModel(ImportModel))
            {
                Log.Logger.Info($"Import has been started:\nError-Directory: {ImportModel.ErrorDirectory}\nImport-Directory: {ImportModel.ImportDirectory}\nBackup-Directory: {ImportModel.BackupDirectory}");
                FileExporter fileExporter = new FileExporter(ImportModel, _fileHandler);

                fileExporter.FileExportEvent += (eventSender, args) => FileExportEvent(eventSender, args);

                if (!_errorThrown)
                {
                    startExport(fileExporter);
                }
            }

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
                    //Fehler
                }

            }


            if (fileImportCount == 0)
            {
                OnWarnEvent("Directory empty!");
            }
            else
            {
                OnExportDirectoryEvent(DateTime.Now.Millisecond - startTime, fileImportCount);
            }

        }

        private void OnExportDirectoryEvent(long deltaTime, int fileImportCount)
        {
            DirectoryExportEventHandler handler = DirectoryExportEvent;

            DirectoryExportEventArgs args = new DirectoryExportEventArgs(_fileHandler.FileSys.DirectoryInfo.FromDirectoryName(ImportModel.ErrorDirectory))
            {
                durationMillis = deltaTime,
                importedFileCount = fileImportCount,
            };

            handler?.Invoke(this, args);
        }

        private void OnWarnEvent(string message)
        {
            WarnEventHandler handler = WarnEvent;
            handler?.Invoke(this, new WarnEventArgs(message));
        }

    }
}
