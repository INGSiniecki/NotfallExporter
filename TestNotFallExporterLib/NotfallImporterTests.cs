using System;
using Xunit;
using NotfallExporterLib;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using System.IO;

namespace TestNotFallExporterLib
{
    public class NotfallImporterTests


    {


        private MockFileSystem _fileSystem;

        [Fact]
        public void TestDirectoryNotFound()
        {
            ImportData model = new ImportData();

            Action a = () => new NotfallImporter(model, _fileSystem);
            Assert.Throws<ArgumentNullException> (a);

        }

        [Fact]
        public void TestBackupForNull()
        {
            
            ImportData model = CreateModel();
            NotfallImporter importer = new NotfallImporter(model, _fileSystem);

            Assert.Throws<NullReferenceException>(() => importer.Backup(null));

        }

        [Fact]
        public void TestBackupForFileNotFound()
        {
            //Arrange
            ImportData model = CreateModel();
            NotfallImporter importer = new NotfallImporter(model, _fileSystem);

            //Act
            //Assert
            Assert.Throws<FileNotFoundException>(() => importer.Backup("not Existing"));

        }
        [Fact]
        public void TestBackup()
        {
            //Arrange
            ImportData model = CreateModel();
            NotfallImporter importer = new NotfallImporter(model, _fileSystem);

            //Act
            importer.Backup(@"c:\NotfallImporter\Error\eml_20190220123417_99802_0000009200.eml");

            //Assert
            Assert.True(_fileSystem.File.Exists($@"c:\NotfallImporter\Backup\Backup{DateTime.Today.ToString("dd_MM_yy")}\eml_20190220123417_99802_0000009200.eml"));

        }

        [Fact]
        public void TestStart()
        {
            //Arrange
            ImportData model = CreateModel();
            NotfallImporter importer = new NotfallImporter(model, _fileSystem);

            //Act
            importer.Start();

            //Assert
            Assert.True(_fileSystem.File.Exists(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.idx"));
            Assert.True(_fileSystem.File.Exists(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip"));
            Assert.True(_fileSystem.File.Exists(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.rdy"));

            Assert.True(_fileSystem.File.Exists(@"c:\NotfallImporter\Import\eml_20190220123417_99802_0000009200.zip"));
            Assert.True(_fileSystem.File.Exists(@"c:\NotfallImporter\Import\eml_20190220123417_99802_0000009200.idx"));
            Assert.True(_fileSystem.File.Exists(@"c:\NotfallImporter\Import\eml_20190220123417_99802_0000009200.rdy"));
        }

        [Fact]
        public void TestExtractImports()
        {
            //Arrange
            ImportData model = CreateModel();
            NotfallImporter importer = new NotfallImporter(model, _fileSystem);

            //Act
            List<string> imports = importer.ExtractImports();


            //Assert
            string[] assertImports = { @"c:\NotfallImporter\Error\eml_20190220123417_99802_0000009200.eml",
                @"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip" };

            Assert.Equal(imports.ToArray(), assertImports);

        }

        public ImportData CreateModel()
        {
            ImportData model = new ImportData()
            {
                ErrorDirectory = @"c:\NotfallImporter\Error",
                BackupDirectory = @"c:\NotfallImporter\Backup",
                ImportDirectory = @"c:\NotfallImporter\Import",
                AccountConfig = @"c:\NotfallImporter\AccountConfig.xml",
                IdxIndexSpecification = @"c:\NotfallImporter\IdxIndexSpezifikation.xml"
            };
            return model;
        }
                

        public NotfallImporterTests()
        {
            _fileSystem = FakeFileSystem.CreateFileSystem();
        }
    }
}
