

using System.IO.Abstractions;

namespace Com.Ing.DiBa.NotfallExporterLib.Idx
{
    /// <summary>
    /// Class to represent a Idx-File
    /// </summary>
    public class IdxRepresentation
    {

        /// <summary>
        /// FilePath to the IdxFile
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// content of the idx
        /// </summary>
        public IdxContent Content { get; set; }
    }
}
