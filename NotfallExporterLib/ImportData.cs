namespace NotfallExporterLib
{
    /*
     * contains all information for Notfallexporting
     */
    public class ImportData
    {
        public string ImportDirectory { get; set; }
        public string ErrorDirectory { get; set; }
        public string BackupDirectory { get; set; }

        public string AccountConfig;
        public string IdxIndexSpecification;

        public ImportData(string importDirectory, string errorDirectory, string backupDirectory)
        {
            this.ImportDirectory = importDirectory;
            this.ErrorDirectory = errorDirectory;
            this.BackupDirectory = backupDirectory;
        }

        public ImportData()
        {

        }
    }
}
