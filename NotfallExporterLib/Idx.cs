using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Abstractions;

namespace NotfallExporterLib
{
    /*
     * represents an IdxFile
     */
    public class Idx
    {
        public string File { get; }

        public Idx(string file)
        {
            File = file;
        }
    }
}
