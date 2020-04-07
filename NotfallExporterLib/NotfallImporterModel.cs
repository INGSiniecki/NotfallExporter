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
        protected IdxBuilder _idxBuilder;
        public ImportData Data { get; set; }
        protected static log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
