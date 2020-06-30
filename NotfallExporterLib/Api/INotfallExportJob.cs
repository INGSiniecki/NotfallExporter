
using System.Dynamic;
using System.IO;


namespace Com.Ing.DiBa.NotfallExporterLib.Api
{
    interface INotfallExportJob
    {

        IMessenger Messenger { get; set; }
        void StartJob();

        void StopJob();
        
        void OnChanged(object source, FileSystemEventArgs e);
    }
}

