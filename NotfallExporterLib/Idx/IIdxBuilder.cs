using Com.Ing.DiBa.NotfallExporterLib.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ing.DiBa.NotfallExporterLib.Idx
{
    interface IIdxBuilder
    {
        IdxRepresentation BuildIdx(string file, string destFile);
    }
}
