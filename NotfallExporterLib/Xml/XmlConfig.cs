using Com.Ing.DiBa.NotfallExporterLib.File;
using System.Xml;

namespace Com.Ing.DiBa.NotfallExporterLib.Xml
{
    /// <summary>
    /// Class to interact with Xml-Configs
    /// </summary>
    public class XmlConfig
    {
        protected XmlDocument _xmlFile;
        private readonly IFileHandler _fileHandler;

        public XmlConfig(string xmlFile, IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;

            _xmlFile = new XmlDocument();
            _xmlFile.Load(_fileHandler.FileSys.File.OpenRead(xmlFile));

        }

        protected XmlNode GetRootNode(string name)
        {
            foreach (XmlNode node in _xmlFile.ChildNodes)
                if (node.Name.Equals(name))
                    return node;

            return null;
        }
    }
}
