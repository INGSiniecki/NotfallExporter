
using System.IO.Abstractions;

namespace Com.Ing.DiBa.NotfallExporterLib.File
{
    public interface IFileZip
    {
        IFileInfo ZipFile(IDirectoryInfo destDirectory);
    }
}
