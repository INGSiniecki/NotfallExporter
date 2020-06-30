
using System.IO;
using System.Security.Permissions;


namespace Com.Ing.DiBa.NotfallExporterLib.Api
{

    /// <summary>
    /// Class to continously Export Files from a Directory
    /// </summary>
    public class NotfallExportJob :  INotfallExportJob
    {
        /// <summary>
        /// object to export a directory
        /// </summary>
        private readonly IFileExporter _fileExporter;
        private readonly IDirectoryExporter _directoryExporter;
        private FileSystemWatcher _watcher;

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

        /// <summary>
        /// instantiates a object of NotfallExportJob
        /// </summary>
        /// <param name="fileExporter">object to export a directory</param>
        public NotfallExportJob(IFileExporter fileExporter)
        {
            _fileExporter = fileExporter;
            _directoryExporter = new DirectoryExporter(fileExporter);
        }


        /// <summary>
        /// starts the continous export
        /// </summary>
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void StartJob()
        {

            _directoryExporter.Start();


            _watcher = new FileSystemWatcher(); 
            _watcher.Path = _fileExporter.ExportModel.ErrorDirectory;

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                _watcher.NotifyFilter = NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName
                                     | NotifyFilters.DirectoryName;

                _watcher.Filter = "";

                // Add event handlers.
                _watcher.Created += OnChanged;

                // Begin watching.
                _watcher.EnableRaisingEvents = true;

        }
        /// <summary>
        /// stops the continous export
        /// </summary>
        public void StopJob()
        {
            _watcher.Dispose();
            _watcher = null;
        }



        public void OnChanged(object source, FileSystemEventArgs e) =>
            _directoryExporter.Start();



    }
}
