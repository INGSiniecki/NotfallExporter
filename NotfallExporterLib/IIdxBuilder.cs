using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotfallExporterLib
{
    interface IIdxBuilder
    {
         Idx createIdx(string src_file);
    }
}
