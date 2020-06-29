namespace Com.Ing.DiBa.NotfallExporterLib.File.Export
{
    public interface IExportDataPattern
    {
        bool IsValid(string[] elements);
    }
}