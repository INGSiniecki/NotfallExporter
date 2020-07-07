using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotfallExporterLib.Database.Model
{
    public class IdxDBBuilder
    {

        private static int ParseInteger(string element) => element.Equals("") ? 0 : int.Parse(element);
        public static IdxDBModel BuildIdxDBModel(string line)
        {
            string[] lines = line.Split(';');
            IdxDBModel idxModel = new IdxDBModel();

            idxModel.BCount = ParseInteger(lines[0]);
                idxModel.VorgangsartID = ParseInteger(lines[1]);
            idxModel.VorgangsartName = lines[2];
            idxModel.ProduktName = lines[3];
            idxModel.Postkorb = lines[4];
            idxModel.PaginierNr = lines[5];
            idxModel.DokumentartID = lines[6];
            idxModel.MethodenID = ParseInteger(lines[7]);
            idxModel.KundenNr = lines[8];
            idxModel.KontoNr = lines[9];
            idxModel.AbschlussDatum = lines[10];
            idxModel.BaufiVorgangsNr = ParseInteger(lines[11]);
            idxModel.VersandArt = ParseInteger(lines[12]);
            idxModel.NameDerImageDatei = lines[13];
            idxModel.Igz = lines[14];
            idxModel.Gz = lines[15];
            idxModel.Original = lines[16];
            idxModel.KreditkartenNr = ParseInteger(lines[17]);
            idxModel.PartnerNr = ParseInteger(lines[18]);
            idxModel.VermittlerNr = ParseInteger(lines[19]);
            idxModel.DMSDocClassId = lines[20];
            idxModel.ExterneNummer = lines[21];
            idxModel.ZuliefererID = lines[22];
            idxModel.BearbeitungsInfo = lines[23];
            idxModel.Prioritaet = lines[24];
            idxModel.Posteingangsdatum = lines[25];
            idxModel.Bearbeitungsprio = ParseInteger(lines[26]);
            idxModel.AS_DMSUEBERGABE_ID = lines[27];
                idxModel.MANDAT_ID = lines[27];

            

            return idxModel;
        }
    }
}
