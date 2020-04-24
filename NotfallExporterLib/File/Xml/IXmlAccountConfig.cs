
using System.Xml;

namespace Com.Ing.DiBa.NotfallExporterLib.File.Xml
{
    public interface IXmlAccountConfig
    {
      
         XmlNode GetAccountNode(string msn);
    }
}
