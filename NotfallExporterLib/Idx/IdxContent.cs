
using System.Collections.Generic;
using System.Text;

namespace Com.Ing.DiBa.NotfallExporterLib.Idx
{
    /// <summary>
    /// Class to represent the content of a Idx-File
    /// </summary>
    public class IdxContent
    {
        public IList<string> Lines { get; set; }

        /// <summary>
        /// converts the IdxContent to String
        /// </summary>
        /// <returns>string</returns>
        override public string ToString()
        {
            StringBuilder content = new StringBuilder();
            foreach (string line in Lines)
                content.AppendLine(line);

            return content.ToString();
        }
    }
}
