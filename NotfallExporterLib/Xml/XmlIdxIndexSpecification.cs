
using Com.Ing.DiBa.NotfallExporterLib.File;
using System.Xml;

namespace Com.Ing.DiBa.NotfallExporterLib.Xml
{
    public class XmlIdxIndexSpecification : XmlConfig, IXmlIdxIndexSpecification
    {
        public XmlIdxIndexSpecification(string xmlFile, IFileHandler fileHandler) : base(xmlFile, fileHandler)
        {
        }

        public XmlNode GetIndexListNode()
        {
            return GetRootNode("Index");
        }
    }
}
