using NotfallExporterLib.Database.Model;
using System.Data;

namespace NotfallExporterLib.Database
{
    public interface ISqliteDataAccess
    {
        IDbConnection DbConnection { get; set; }
        void SaveIdx(IdxDBModel idx);
    }
}
