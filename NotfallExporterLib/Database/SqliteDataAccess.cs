﻿
using Dapper;
using NotfallExporterLib.Database.Model;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;


namespace NotfallExporterLib.Database
{
    public class SqliteDataAccess : ISqliteDataAccess
    {

        /// <summary>
        /// database connection
        /// </summary>
        public IDbConnection DbConnection { get; set; }

        public SqliteDataAccess()
        {
            DbConnection = new SQLiteConnection(LoadConnectionString());
            DbConnection.Open();
        }

        public SqliteDataAccess(DbConnection connection)
        {
            DbConnection = connection;
        }

        ~SqliteDataAccess()
        {
            DbConnection.Close();
        }

        private const string save_statement = "insert into Idx (BCount, VorgangsartID,	VorgangsartName, ProduktName, Postkorb, PaginierNr, DokumentartID, " +
                    "MethodenID, KundenNr, KontoNr ,AbschlussDatum, BaufiVorgangsNr, Versandart, NameDerImageDatei, Igz, Gz, Original," +
                    " KreditkartenNr, PartnerNr, VermittlerNr, DMSDocClassId, ExterneNummer, ZuliefererID, BearbeitungsInfo, Prioritaet, " +
                    "Posteingangsdatum, Bearbeitungsprio, AS_DMSUEBERGABE_ID, MANDAT_ID) values (@BCount, @VorgangsartID,	@VorgangsartName, @ProduktName, @Postkorb, @PaginierNr, @DokumentartID, " +
                    "@MethodenID, @KundenNr, @KontoNr ,@AbschlussDatum, @BaufiVorgangsNr, @Versandart, @NameDerImageDatei, @Igz, @Gz, @Original," +
                    " @KreditkartenNr, @PartnerNr, @VermittlerNr, @DMSDocClassId, @ExterneNummer, @ZuliefererID, @BearbeitungsInfo, @Prioritaet, " +
                    "@Posteingangsdatum, @Bearbeitungsprio, @AS_DMSUEBERGABE_ID, @MANDAT_ID)  ";

        /// <summary>
        /// saves the content of a idx file in a sqlite database
        /// </summary>
        /// <param name="idx">object to contain idx content</param>DbConnection
        public void SaveIdx(IdxDBModel idx)
        {
                DbConnection.Execute(save_statement, idx);
        }



        private string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
