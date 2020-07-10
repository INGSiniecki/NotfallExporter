using Dapper;
using NotfallExporterLib.Database.Model;
using System.Configuration;
using System.Data;
using System.Data.SQLite;


namespace NotfallExporterLib.Database
{
    class IdxDatabase : IIdxDatabase
    {
        private IDbConnection _connection;

        public IdxDatabase()
        {
        }

        public void SaveIdx(string sql, IdxDBModel idxModel)
        {
            if (_connection == null)
            {
                _connection = new SQLiteConnection(LoadConnectionString());
                _connection.Open();
            }

            _connection.Execute(sql, idxModel);
        }

        ~IdxDatabase()
        {
            _connection.Close();
        }


        private string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
