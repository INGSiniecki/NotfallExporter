using Com.Ing.DiBa.NotfallExporterLib.Idx;
using Dapper;
using NotfallExporterLib.Database.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotfallExporterLib.Database
{
    public class SqliteDataAccess
    {

        private static string save_statement = "insert into Idx (BCount, VorgangsartID,	VorgangsartName, ProduktName, Postkorb, PaginierNr, DokumentartID, " +
                    "MethodenID, KundenNr, KontoNr ,AbschlussDatum, BaufiVorgangsNr, Versandart, NameDerImageDatei, Igz, Gz, Original," +
                    " KreditkartenNr, PartnerNr, VermittlerNr, DMSDocClassId, ExterneNummer, ZuliefererID, BearbeitungsInfo, Prioritaet, " +
                    "Posteingangsdatum, Bearbeitungsprio, AS_DMSUEBERGABE_ID, MANDAT_ID) values (@BCount, @VorgangsartID,	@VorgangsartName, @ProduktName, @Postkorb, @PaginierNr, @DokumentartID, " +
                    "@MethodenID, @KundenNr, @KontoNr ,@AbschlussDatum, @BaufiVorgangsNr, @Versandart, @NameDerImageDatei, @Igz, @Gz, @Original," +
                    " @KreditkartenNr, @PartnerNr, @VermittlerNr, @DMSDocClassId, @ExterneNummer, @ZuliefererID, @BearbeitungsInfo, @Prioritaet, " +
                    "@Posteingangsdatum, @Bearbeitungsprio, @AS_DMSUEBERGABE_ID, @MANDAT_ID)  ";
        public static void SaveIdx(IdxDBModel idx)
        {
            using(IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(save_statement, idx);

            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
