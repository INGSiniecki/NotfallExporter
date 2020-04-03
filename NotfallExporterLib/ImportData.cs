namespace NotfallExporterLib
{
    /*
     * contains all information for Notfallexporting
     */
    public class ImportData
    {
        public string Import_Directory { get; set; }
        public string Error_Directory { get; set; }
        public string Backup_Directory { get; set; }

        public ImportData(string import_directory, string error_directory, string backup_directory)
        {
            this.Import_Directory = import_directory;
            this.Error_Directory = error_directory;
            this.Backup_Directory = backup_directory;
        }

        public ImportData()
        {

        }
    }
}
