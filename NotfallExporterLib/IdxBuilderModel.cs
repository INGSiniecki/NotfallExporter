using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Abstractions;
using System.Xml;

namespace NotfallExporterLib
{
    public class IdxBuilderModel
    {
        protected static log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected string _destPath;

        public XmlDocument IdxIndexSpecification;
        public XmlDocument AccountConfig;

        protected IFileSystem _fileSystem;

    }
}
