

using System;
using System.IO.Abstractions;

namespace Com.Ing.DiBa.NotfallExporterLib.Event
{
    /// <summary>
    /// Class to contain Arguments for DirectoryExportEvents
    /// </summary>
    public class DirectoryExportEventArgs : EventArgs
    {
        /// <summary>
        /// directory which was exported
        /// </summary>
        public IDirectoryInfo Directory { get; set; }
        /// <summary>
        /// instantiates an object of DirectoryExportEventArgs
        /// </summary>
        /// <param name="directory">directory which as exported</param>
        public DirectoryExportEventArgs(IDirectoryInfo directory)
        {
            Directory = directory;
        }

        /// <summary>
        /// duration of the Directory-Export in milliseconds
        /// </summary>
        public long durationMillis { get; set; }

        /// <summary>
        /// duration of 
        /// </summary>
        public long importedFileCount { get; set; } = 0;

    }
}
