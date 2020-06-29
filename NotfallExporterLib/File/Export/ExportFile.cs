

using System.IO.Abstractions;

namespace Com.Ing.DiBa.NotfallExporterLib.File.Export
{
    /// <summary>
    /// Class to represent a File to export
    /// </summary>
    public class ExportFile
    {
        /// <summary>
        /// object to represent the file in the FileSystem
        /// </summary>
        public  IFileInfo File { get; }

        /// <summary>
        /// object to represent the data extracted from the filename.
        /// Has to be defined with BuilData()
        /// </summary>
        public ExportFileData Data { get; set; }

        /// <summary>
        /// instantiates an object of ExportFile
        /// </summary>
        /// <param name="exportFile">object to represent the file in the FileSystem</param>
        public ExportFile(IFileInfo exportFile)
        {
            File = exportFile;
        }

        /// <summary>
        /// extracts data from the filename and 
        /// </summary>
        public void BuilData()
        {
            ExportFileDataBuilder dataBuilder = new ExportFileDataBuilder();
            Data = dataBuilder.BuildDataFromFileName(File.Name);
        }

    }
}
