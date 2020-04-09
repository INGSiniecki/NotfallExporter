
using System.IO;
using System.Security.Permissions;


namespace Com.Ing.DiBa.NotfallExporterLib.Export
{
    /*
     * class for continous NotfallImport
     */
    public class NotfallExportJob :  INotfallExportJob
    {
        protected DirectoryExporter _notfallImporter;
        protected FileSystemWatcher _watcher;

        public NotfallExportJob(DirectoryExporter notfallImporter)
        {
            _notfallImporter = notfallImporter;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void StartJob()
        {

            _notfallImporter.Start();


            _watcher = new FileSystemWatcher(); 
                _watcher.Path = _notfallImporter.ImportModel.ErrorDirectory;

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

        public void StopJob()
        {
            _watcher.Dispose();
            _watcher = null;
        }



        public void OnChanged(object source, FileSystemEventArgs e) =>
            _notfallImporter.Start();



    }
}
