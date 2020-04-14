using System;

using System.Xml;

namespace Com.Ing.DiBa.NotfallExporterLib.Xml
{
    public interface IXmlIdxIndexSpecification
    {
        /// <summary>
        /// returns the Index-Node of a IdxIndexSpecification-File
        /// </summary>
        /// <returns></returns>
         XmlNode GetIndexListNode();
    }
}
