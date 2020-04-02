using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Text;

namespace TestNotFallExporterLib
{
    class FakeFileSystem
    {

        public static MockFileSystem createFileSystem()
        {
            return new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {@"c:\NotfallImporter\Backup", new MockDirectoryData()},
                {@"c:\NotfallImporter\Import", new MockDirectoryData()},
                {@"c:\NotfallImporter\Error", new MockDirectoryData()},

                {@"c:\NotfallImporter\Error\eml_20190220123417_99802_0000009200.eml", new MockFileData(string.Empty)},
                {@"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip", new MockFileData(string.Empty)},
                {@"c:\NotfallImporter\Error\TextFile.txt", new MockFileData(string.Empty)}
            });
        }
    }
}
