using NotfallExporterLib.Database.Model;
using System.Data;

namespace NotfallExporterLib.Database
{
    public interface ISqliteDataAccess
    {
        IIdxDatabase Database { get; set; }
        void SaveIdx(IdxDBModel idx);
    }
}
