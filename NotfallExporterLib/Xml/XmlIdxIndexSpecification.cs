
using Com.Ing.DiBa.NotfallExporterLib.File;
using System.Xml;

namespace Com.Ing.DiBa.NotfallExporterLib.Xml
{
    /// <summary>
    /// Class to represent the IdxindexSpecification.xml
    /// </summary>
    public class XmlIdxIndexSpecification : XmlConfig, IXmlIdxIndexSpecification
    {
        /// <summary>
        /// instantaites and object of XmlidxindexSpecification
        /// </summary>
        /// <param name="xmlFile">XmlFile-Path</param>
        /// <param name="fileHandler">object for FileSystem operations</param>
        public XmlIdxIndexSpecification(string xmlFile, IFileHandler fileHandler) : base(xmlFile, fileHandler)
        {
        }

        /// <summary>
        /// returns the Index-Node of a IdxIndexSpecification-File
        /// </summary>
        /// <returns></returns>
        public XmlNode GetIndexListNode()
        {
            return GetRootNode("Index");
        }
    }
}
