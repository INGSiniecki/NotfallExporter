using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.IO.Compression;
using System.Text;

namespace TestNotFallExporterLib
{
    class FakeFileSystem
    {

        public static MockFileSystem CreateFileSystem()
        {
            MockFileSystem fileSystem =  new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {@"c:\NotfallImporter\Backup", new MockDirectoryData()},
                {@"c:\NotfallImporter\Import", new MockDirectoryData()},
                {@"c:\NotfallImporter\Error", new MockDirectoryData()},

                {@"c:\NotfallImporter\Error\eml_20190220123417_99802_0000009200.eml", new MockFileData("test")},
                {@"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip", new MockFileData(string.Empty)},
                {@"c:\NotfallImporter\Error\TextFile.txt", new MockFileData(string.Empty)},
                {@"c:\NotfallImporter\AccountConfig.xml", new MockFileData(new MockFileData("<Routing><Account><Testwert1>99802</Testwert1><Testwert2>100</Testwert2></Account><Testwert1>99998</Testwert1><Testwert2>200</Testwert2><Account>Testwert2</Account></Routing>"))},
                 {@"c:\NotfallImporter\IdxIndexSpezifikation.xml", new MockFileData("<Index><I1>Teswert1</I1><I2>Testwert2</I2></Index>") }
            });

            using(ZipArchive archive = new ZipArchive(fileSystem.File.Create(@"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip"), ZipArchiveMode.Create))
            {
                archive.CreateEntry("20190304121156_0803230050_0803230050_1551697916302_1_diba_infoblatt.pdf");
            }

            return fileSystem;
        }


    }
}
