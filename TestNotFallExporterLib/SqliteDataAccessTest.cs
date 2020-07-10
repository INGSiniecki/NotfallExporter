
using NotfallExporterLib.Database;
using NotfallExporterLib.Database.Model;
using Xunit;
using Moq;

namespace TestNotFallExporterLib
{
    public class SqliteDataAccessTest
    {
        [Fact]
        public void testSaveIdxSuccesfull()
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

            var mock = new Mock<IIdxDatabase>();

            mock.Setup(x => x.SaveIdx(sql, idxModel));


            var dbService = new SqliteDataAccess(mock.Object);
            dbService.SaveIdx(idxModel);

            mock.Verify(x => x.SaveIdx(sql, idxModel), Times.Exactly(1));



        }


    }
}
