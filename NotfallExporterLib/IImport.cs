﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotfallExporterLib
{
    interface IImport
    {

        void Start(IdxBuilder idxBuilder);


        void CreateRdy();


         string ZipEml();
        
    }
}
