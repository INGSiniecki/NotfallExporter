using Com.Ing.DiBa.NotfallExporterLib.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Com.Ing.DiBa.NotfallExporterLib.Xml
{
    public class XmlAccountConfig : XmlConfig, IXmlAccountConfig
    {
        public XmlAccountConfig(string xmlFile, IFileHandler fileHandler) : base(xmlFile, fileHandler)
        {
        }

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
