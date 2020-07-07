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

        /// <summary>
        /// creates an instance of a IdxDBModel from given idx-lines
        /// </summary>
        /// <param name="line">content of a idx line</param>
        /// <returns>instance of a IdxDBModel</returns>
        public static IdxDBModel BuildIdxDBModel(string line)
        {
            string[] lines = line.Split(';');
            IdxDBModel idxModel = new IdxDBModel()
            {
                BCount = ParseInteger(lines[0]),
                VorgangsartID = ParseInteger(lines[1]),
                VorgangsartName = lines[2],
                ProduktName = lines[3],
                Postkorb = lines[4],
                PaginierNr = lines[5],
                DokumentartID = lines[6],
                MethodenID = ParseInteger(lines[7]),
                KundenNr = lines[8],
                KontoNr = lines[9],
                AbschlussDatum = lines[10],
                BaufiVorgangsNr = ParseInteger(lines[11]),
                VersandArt = ParseInteger(lines[12]),
                NameDerImageDatei = lines[13],
                Igz = lines[14],
                Gz = lines[15],
                Original = lines[16],
                KreditkartenNr = ParseInteger(lines[17]),
                PartnerNr = ParseInteger(lines[18]),
                VermittlerNr = ParseInteger(lines[19]),
                DMSDocClassId = lines[20],
                ExterneNummer = lines[21],
                ZuliefererID = lines[22],
                BearbeitungsInfo = lines[23],
                Prioritaet = lines[24],
                Posteingangsdatum = lines[25],
                Bearbeitungsprio = ParseInteger(lines[26]),
                AS_DMSUEBERGABE_ID = lines[27],
                MANDAT_ID = lines[27],
            };

           
            return idxModel;
        }
    }
}
