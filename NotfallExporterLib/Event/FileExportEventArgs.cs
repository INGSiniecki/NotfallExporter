using System;
using System.IO;
using System.IO.Abstractions;

namespace Com.Ing.DiBa.NotfallExporterLib.Event
{
    /// <summary>
    /// Class to contain Arguments for FileExportEvents
    /// </summary>
    public class FileExportEventArgs : EventArgs
    {
        /// <summary>
        /// IFileInfo object to represent the exported File
        /// </summary>
        public IFileInfo SourceFile { get; set; }

        /// <summary>
        /// instantiates an object of FileExportEventArgs
        /// </summary>
        /// <param name="directory"></param>
        public FileExportEventArgs(IFileInfo file)
        {
            SourceFile = file;
        }

        public long durationMillis { get; set; }
    }
}
