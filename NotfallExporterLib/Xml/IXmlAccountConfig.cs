﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Com.Ing.DiBa.NotfallExporterLib.Xml
{
    public interface IXmlAccountConfig
    {
         XmlNode GetAccountNode(string msn);
    }
}
