using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotfallExporterLib.Database.Model
{
    public class IdxDBModel
    {

        public int BCount { get; set; }
        public int VorgangsartID { get; set; }
        public string VorgangsartName { get; set; }
        public string ProduktName { get; set; }
        public string Postkorb { get; set; }
        public string PaginierNr { get; set; }
        public string DokumentartID { get; set; }
        public int MethodenID { get; set; }
        public string KundenNr { get; set; }
        public string KontoNr { get; set; }
        public string AbschlussDatum { get; set; }
        public int BaufiVorgangsNr { get; set; }
        public int VersandArt { get; set; }
        public string NameDerImageDatei { get; set; }
        public string Igz { get; set; }
        public string Gz { get; set; }
        public string Original { get; set; }
        public int KreditkartenNr { get; set; }
        public int PartnerNr { get; set; }
        public int VermittlerNr { get; set; }
        public string DMSDocClassId { get; set; }
        public string ExterneNummer { get; set; }
        public string ZuliefererID { get; set; }
        public string BearbeitungsInfo { get; set; }
        public string Prioritaet { get; set; }
        public string Posteingangsdatum { get; set; }
        public int Bearbeitungsprio { get; set; }
        public string AS_DMSUEBERGABE_ID { get; set; }
        public string MANDAT_ID { get; set; }

    }
}
