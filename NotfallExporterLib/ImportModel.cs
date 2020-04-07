using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Abstractions; 

namespace NotfallExporterLib
{
    public class ImportModel
    {
        protected static log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected string _filePath;
        protected Idx _idx;
        protected string _destDirectory;
        protected IFileSystem _fileSystem;
    }
}
