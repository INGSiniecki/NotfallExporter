

using Com.Ing.DiBa.NotfallExporterLib.File.Export;

namespace Com.Ing.DiBa.NotfallExporterLib.Idx
{
    interface IIdxBuilder
    {
        IdxRepresentation BuildIdx(ExportFile exportFile);
    }
}
