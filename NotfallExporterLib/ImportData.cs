using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotfallExporterLib
{
    /*
     * contains all information for Notfallexporting
     */
    public class ImportData
    {
        public string _import_directory { get; set; }
        public string _error_directory { get; set; }
        public string _backup_directory { get; set; }
        public string _logging_directory { get; set; }

        public ImportData(string import_directory, string error_directory, string backup_directory, string logging_directory)
        {
            this._import_directory = import_directory;
            this._error_directory = error_directory;
            this._backup_directory = backup_directory;
            this._logging_directory = logging_directory;
        }

        public ImportData()
        {

        }
    }
}
