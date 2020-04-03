using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Abstractions;

namespace NotfallExporterLib
{
    public class NotfallImporterModel
    {
        protected IFileSystem _fileSystem;
        public ImportData _data { get; set; }
        protected static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
