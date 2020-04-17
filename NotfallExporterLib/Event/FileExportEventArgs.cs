using System;
using System.IO;

namespace NotfallExporterLib.Event
{
    class FileExportEventArgs : EventArgs
    {
        private FileInfo _sourceFile;
        public FileExportEventArgs(FileInfo directory)
        {
            _sourceFile = directory;
        }

        public long durationMillis { get; set; }
    }
}
