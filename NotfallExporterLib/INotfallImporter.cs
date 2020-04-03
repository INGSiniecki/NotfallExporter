using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotfallExporterLib
{
    interface INotfallImporter
    {

        void Start();



        List<string> ExtractImports();


        void Backup(string file);
    }
}
