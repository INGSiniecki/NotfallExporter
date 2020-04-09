using Com.Ing.DiBa.NotfallExporterLib.File;
using Com.Ing.DiBa.NotfallExporterLib.Idx;
using System;

namespace Com.Ing.DiBa.NotfallExporterLib.Export
{
    interface IFileExporter
    {
        /// <summary>
        /// starts the import of the file
        /// </summary>
        /// <param name="idxBuilder">Builder class for Idx-Files</param>
        void Start(string sourceFile);

    }
}
