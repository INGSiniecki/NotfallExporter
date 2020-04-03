using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotfallExporterLib
{
    interface IImport
    {

        void start();


        void CreateRdy();


         void ZipEml();
        
    }
}
