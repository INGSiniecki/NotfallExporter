

using Com.Ing.DiBa.NotfallExporterLib.Event;

namespace Com.Ing.DiBa.NotfallExporterLib.Idx
{
    interface IIdxBuilder
    {
        event ErrorEventHandler ErrorEvent;
        IdxRepresentation BuildIdx(string sourceFile, string destDirectory);
    }
}
