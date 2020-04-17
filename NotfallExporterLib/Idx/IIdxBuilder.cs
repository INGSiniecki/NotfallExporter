

namespace Com.Ing.DiBa.NotfallExporterLib.Idx
{
    interface IIdxBuilder
    {
        
        IdxRepresentation BuildIdx(string sourceFile, string destDirectory);
    }
}
