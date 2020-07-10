using NotfallExporterLib.Database.Model;

namespace NotfallExporterLib.Database
{
    public interface IIdxDatabase
    {
        void SaveIdx(string sql, IdxDBModel idxModel);
    }
}
