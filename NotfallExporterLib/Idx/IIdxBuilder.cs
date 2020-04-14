

namespace Com.Ing.DiBa.NotfallExporterLib.Idx
{
    interface IIdxBuilder
    {
        /// <summary>
        /// Creates a IdxFile from the given File
        /// </summary>
        /// <param name="sourceFile">file from which to create the Idx-File</param>
        /// <param name="destDirectory">Destination-Directory</param>
        /// <returns></returns>
        IdxRepresentation BuildIdx(string sourceFile, string destDirectory);
    }
}
