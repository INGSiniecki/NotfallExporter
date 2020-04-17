
using System.Xml;

namespace Com.Ing.DiBa.NotfallExporterLib.Xml
{
    public interface IXmlAccountConfig
    {
      
         XmlNode GetAccountNode(string msn);
    }
}
