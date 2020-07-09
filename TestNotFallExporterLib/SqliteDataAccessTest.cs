using Autofac.Extras.Moq;
using NotfallExporterLib.Database;
using NotfallExporterLib.Database.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Text;
using Xunit;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Moq;

namespace TestNotFallExporterLib
{
    public class SqliteDataAccessTest
    {

        private const string CreateStatement = "CREATE TABLE Idx (BCount INTEGER, VorgangsartID	INTEGER, VorgangsartName TEXT, ProduktName TEXT,Postkorb TEXT,PaginierNr TEXT, DokumentartID	TEXT,MethodenID	INTEGER,KundenNr	TEXT,KontoNr	TEXT,AbschlussDatum	TEXT,BaufiVorgangsNr	INTEGER,Versandart	INTEGER,NameDerImageDatei	TEXT,Igz	TEXT,Gz	TEXT,Original	TEXT,KreditkartenNr	NUMERIC,PartnerNr	NUMERIC,VermittlerNr	NUMERIC,DMSDocClassId	TEXT,ExterneNummer	TEXT,ZuliefererID	TEXT,BearbeitungsInfo	TEXT,Prioritaet	TEXT,Posteingangsdatum	TEXT,Bearbeitungsprio	NUMERIC,AS_DMSUEBERGABE_ID	TEXT, MANDAT_ID	TEXT)";

        private const string save_statement = "insert into Idx (BCount, VorgangsartID,	VorgangsartName, ProduktName, Postkorb, PaginierNr, DokumentartID, " +
            "MethodenID, KundenNr, KontoNr ,AbschlussDatum, BaufiVorgangsNr, Versandart, NameDerImageDatei, Igz, Gz, Original," +
            " KreditkartenNr, PartnerNr, VermittlerNr, DMSDocClassId, ExterneNummer, ZuliefererID, BearbeitungsInfo, Prioritaet, " +
            "Posteingangsdatum, Bearbeitungsprio, AS_DMSUEBERGABE_ID, MANDAT_ID) values (@BCount, @VorgangsartID,	@VorgangsartName, @ProduktName, @Postkorb, @PaginierNr, @DokumentartID, " +
            "@MethodenID, @KundenNr, @KontoNr ,@AbschlussDatum, @BaufiVorgangsNr, @Versandart, @NameDerImageDatei, @Igz, @Gz, @Original," +
            " @KreditkartenNr, @PartnerNr, @VermittlerNr, @DMSDocClassId, @ExterneNummer, @ZuliefererID, @BearbeitungsInfo, @Prioritaet, " +
            "@Posteingangsdatum, @Bearbeitungsprio, @AS_DMSUEBERGABE_ID, @MANDAT_ID)  ";
        [Fact]
        public void TestSaveIdx()
        {
            DbConnection connection = CreateInMemoryDatabase();
            ISqliteDataAccess dbService = new SqliteDataAccess(connection);
            dbService.DbConnection = connection;

            string line = "1;51;Immobilienfinanzierung;Direkt-Baufinanzierung;;81.998020000009200.eml;22;;;;;;-5;;;;;;;;1808;;;;0;;500;;";
            IdxDBModel idxModel = IdxDBBuilder.BuildIdxDBModel(line);

            dbService.SaveIdx(idxModel);
            Assert.Equal(1, idxModel.BCount);

            var command = connection.CreateCommand();
            command.CommandText = "select * from Idx";

            int bCount = 0;
            string vorgangsartName = "";
            int bearbeitungsPrio = 0;

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    bCount = reader.GetInt32(0);
                    vorgangsartName = reader.GetString(2);
                    bearbeitungsPrio = reader.GetInt32(26);

                }
            }

            Assert.Equal(1, bCount);
            Assert.Equal("Immobilienfinanzierung", vorgangsartName);
            Assert.Equal(500, bearbeitungsPrio);

        }

        [Fact]
        public void testSaveIdxSuccesfull()
        {
            using(var mock = AutoMock.GetLoose())
            {
                string sql = "insert into Idx(BCount, VorgangsartID, VorgangsartName, ProduktName, Postkorb, PaginierNr, DokumentartID, " +
                    "MethodenID, KundenNr, KontoNr ,AbschlussDatum, BaufiVorgangsNr, Versandart, NameDerImageDatei, Igz, Gz, Original," +
                    " KreditkartenNr, PartnerNr, VermittlerNr, DMSDocClassId, ExterneNummer, ZuliefererID, BearbeitungsInfo, Prioritaet, " +
                    "Posteingangsdatum, Bearbeitungsprio, AS_DMSUEBERGABE_ID, MANDAT_ID) values (@BCount, @VorgangsartID,	@VorgangsartName, @ProduktName, @Postkorb, @PaginierNr, @DokumentartID, " +
                    "@MethodenID, @KundenNr, @KontoNr ,@AbschlussDatum, @BaufiVorgangsNr, @Versandart, @NameDerImageDatei, @Igz, @Gz, @Original," +
                    " @KreditkartenNr, @PartnerNr, @VermittlerNr, @DMSDocClassId, @ExterneNummer, @ZuliefererID, @BearbeitungsInfo, @Prioritaet, " +
                    "@Posteingangsdatum, @Bearbeitungsprio, @AS_DMSUEBERGABE_ID, @MANDAT_ID)";

                string line = "1;51;Immobilienfinanzierung;Direkt-Baufinanzierung;;81.998020000009200.eml;22;;;;;;-5;;;;;;;;1808;;;;0;;500;;";
                IdxDBModel idxModel = IdxDBBuilder.BuildIdxDBModel(line);

                mock.Mock<IDbConnection>().Setup(x => x.Execute(sql, idxModel, null, null, null)).Returns(1);

                ISqliteDataAccess dbService = mock.Create<SqliteDataAccess>();
                dbService.SaveIdx(idxModel);

                mock.Mock<IDbConnection>().Verify(x => x.Execute(sql, idxModel, null, null, null), Times.Exactly(1));

            }
        }

        private DbConnection CreateInMemoryDatabase()
        {
            DbConnection connection = new SQLiteConnection("Data Source=:memory:");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = CreateStatement;
            command.ExecuteNonQuery();

            return connection;
        }
    }
}
