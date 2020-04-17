using Com.Ing.DiBa.NotfallExporterLib.File;
using System.Xml;

namespace Com.Ing.DiBa.NotfallExporterLib.Xml
{
    /// <summary>
    /// class to represent the AccountConfig.xml
    /// </summary>
    public class XmlAccountConfig : XmlConfig, IXmlAccountConfig
    {

        /// <summary>
        /// instantiates a object of XmlAccountConfig
        /// </summary>
        /// <param name="xmlFile">XmlFile-Path</param>
        /// <param name="fileHandler">object for FileSystem operations</param>
        public XmlAccountConfig(string xmlFile, IFileHandler fileHandler) : base(xmlFile, fileHandler)
        {
        }


        /// <summary>
        /// returns the Xml-Node with the given msn
        /// </summary>
        /// <param name="msn">MSN-Number</param>
        /// <returns></returns>
        public XmlNode GetAccountNode(string msn)
        {
            XmlNode routing = GetRootNode("Routing");

            //finding the right Account Node
            foreach (XmlNode account in routing)
            {
                if (account.FirstChild.InnerText.Equals(msn))
                    return account;
            }
            return null;
        }
    }
}
