
using System.IO;


namespace Com.Ing.DiBa.NotfallExporterLib.Export
{
    interface INotfallExportJob
    {
        /// <summary>
        /// starts the continous export
        /// </summary>
        void StartJob();

        /// <summary>
        /// stops the continous export
        /// </summary>
         void StopJob();
        
     void OnChanged(object source, FileSystemEventArgs e);
    }
}

