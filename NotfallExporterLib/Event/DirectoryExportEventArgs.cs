

using System;
using System.IO;

namespace NotfallExporterLib.Event
{
    class DirectoryExportEventArgs : EventArgs
    {
        private DirectoryInfo _directory;
        public DirectoryExportEventArgs(DirectoryInfo directory)
        {
            _directory = directory;
        }

        public long durationMillis { get; set; }

    }
}
