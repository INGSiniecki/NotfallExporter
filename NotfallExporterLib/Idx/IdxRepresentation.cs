using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Abstractions;

namespace Com.Ing.DiBa.NotfallExporterLib.Idx
{
    /*
     * represents an IdxFile
     */
    public class IdxRepresentation
    {
        public string File { get; set; }
        public IdxContent Content { get; set; }
    }
}
