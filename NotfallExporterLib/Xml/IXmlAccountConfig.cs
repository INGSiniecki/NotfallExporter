
using System.Xml;

namespace Com.Ing.DiBa.NotfallExporterLib.Xml
{
    public interface IXmlAccountConfig
    {
        /// <summary>
        /// returns the Account Node which matches the given msn.
        /// </summary>
        /// <param name="msn"></param>
        /// <returns></returns>
         XmlNode GetAccountNode(string msn);
    }
}
