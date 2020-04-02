using System;
using System.Collections.Generic;
using System.Text;
using NotfallExporterLib;
using System.IO.Abstractions.TestingHelpers;
    
namespace TestNotFallExporterLib
{
    class NotFallImporterTestHelper : NotfallImporter
    {
        public NotFallImporterTestHelper(ImportModel model, MockFileSystem fileSystem) : base(model, fileSystem)
        {

        }



        public void InvokeBackup(string file)
        {
            backup(file);
        } 

        public List<string> InvokeExtractImports()
        {
            return extractImports();
        }

       
    }
}
