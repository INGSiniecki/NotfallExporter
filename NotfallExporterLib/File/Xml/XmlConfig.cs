using Com.Ing.DiBa.NotfallExporterLib.File;
using System.Xml;

namespace Com.Ing.DiBa.NotfallExporterLib.File.Xml
{
    /// <summary>
    /// Class to interact with Xml-Configs
    /// </summary>
    public class XmlConfig
    {
        protected XmlDocument _xmlFile;
        private readonly IFileHandler _fileHandler;

        /// <summary>
        /// instantiates a object of XmlConfig
        /// </summary>
        /// <param name="xmlFile">path to the xml-File</param>
        /// <param name="fileHandler">object for FileSystem operations</param>
        public XmlConfig(string xmlFile, IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;
            _xmlFile = LoadXml(xmlFile);

        }

        private XmlDocument LoadXml(string path)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(path);
            }
            catch (XmlException e)
            {

            }
            return xml;
        }

        /// <summary>
        /// returns the Account Node which matches the given msn.
        /// </summary>
        /// <param name="msn"></param>
        /// <returns></returns>
        protected XmlNode GetRootNode(string name)
        {
            foreach (XmlNode node in _xmlFile.ChildNodes)
                if (node.Name.Equals(name))
                    return node;

            return null;
        }
    }
}
