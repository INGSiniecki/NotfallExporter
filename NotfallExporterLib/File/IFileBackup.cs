using System.IO.Abstractions;


namespace Com.Ing.DiBa.NotfallExporterLib.File
{
    public interface IFileBackup
    {
        void BackupFile(IFileInfo file);
    }
}
