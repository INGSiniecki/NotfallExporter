using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Permissions;
using System.IO.Abstractions;

namespace NotfallExporterLib
{
    /*
     * class for continous NotfallImport
     */
    public class NotfallImportJob
    {
        NotfallImporter _notfallImporter;
        FileSystemWatcher _watcher;

        public NotfallImportJob(NotfallImporter notfallImporter)
        {
            _notfallImporter = notfallImporter;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void startJob()
        {

            _notfallImporter.start();


            _watcher = new FileSystemWatcher(); 
                _watcher.Path = _notfallImporter._model._error_directory;

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

        public void stopJob()
        {
            _watcher.Dispose();
            _watcher = null;
        }

         ~NotfallImportJob()
        {
            _watcher.Dispose();
        }

        public void OnChanged(object source, FileSystemEventArgs e) =>
            _notfallImporter.start();



    }
}
