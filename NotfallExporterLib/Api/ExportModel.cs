namespace Com.Ing.DiBa.NotfallExporterLib.Api
{
    /*
     * contains all information for Notfallexporting
     */
    public class ExportModel
    {
       
        /// <summary>
        /// Directory to export Import-Files 
        /// </summary>
        public string ImportDirectory { get; set; }

         /// <summary>
        /// Directory to search for Import-Files
        /// </summary>
        public string ErrorDirectory { get; set; }

        /// <summary>
        /// Directory to backup all exported files
        /// </summary>
        public string BackupDirectory { get; set; }

        /// <summary>
        /// Accountconfig.xml
        /// </summary>
        public string AccountConfig { get; set; }

        /// <summary>
        /// IdxIndexSpecification.xml
        /// </summary>
        public string IdxIndexSpecification { get; set; }
    }
}
