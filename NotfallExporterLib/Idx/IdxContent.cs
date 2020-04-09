using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ing.DiBa.NotfallExporterLib.Idx
{
    public class IdxContent
    {
        public IList<string> Lines { get; set; }

        override public string ToString()
        {
            StringBuilder content = new StringBuilder();
            foreach (string line in Lines)
                content.AppendLine(line);

            return content.ToString();
        }
    }
}
