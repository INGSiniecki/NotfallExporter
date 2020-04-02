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
        public string _file { get; }

        public Idx(string file)
        {
            _file = file;
        }
    }
}
