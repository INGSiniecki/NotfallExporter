namespace Com.Ing.DiBa.NotfallExporterLib
{
    /*
     * contains all information for Notfallexporting
     */
    public class ExportModel
    {
        public string ImportDirectory { get; set; }
        public string ErrorDirectory { get; set; }
        public string BackupDirectory { get; set; }

        public string AccountConfig { get; set; }
        public string IdxIndexSpecification;
    }
}
