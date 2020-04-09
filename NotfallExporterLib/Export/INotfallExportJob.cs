
using System.IO;


namespace Com.Ing.DiBa.NotfallExporterLib.Export
{
    interface INotfallExportJob
    {
        void StartJob();

         void StopJob();
        
     void OnChanged(object source, FileSystemEventArgs e);
    }
}

