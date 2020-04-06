using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotfallExporterLib
{
    interface INotfallImportJob
    {
        void StartJob();

         void StopJob();
        
     void OnChanged(object source, FileSystemEventArgs e);
    }
}

