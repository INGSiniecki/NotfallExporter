using Com.Ing.DiBa.NotfallExporterLib.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ing.DiBa.NotfallExporterLib.Export
{
    interface IDirectoryExporter
    {
        /// <summary>
        /// starts the Import of the given Directory
        /// </summary>
        void Start();
    }
}
