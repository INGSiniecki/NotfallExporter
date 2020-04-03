using System;
using Xunit;
using NotfallExporterLib;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using System.IO;
using NotfallExporterLib;

namespace TestNotFallExporterLib
{
    public class TestNotfallImporter
    {


        private MockFileSystem _fileSystem;

        [Fact]
        public void TestDirectoryNotFound()
        {
            ImportData model = new ImportData();
            NotfallImporter importer = new NotfallImporter(model);

            Action a = () => importer.Start();
            Assert.Throws<DirectoryNotFoundException>(a);

        }

        [Fact]
        public void TestBackupForNull()
        {
            
            ImportData model = CreateModel();
            NotfallImporter importer = new NotfallImporter(model);
            importer.setFileSystem(_fileSystem);

            Assert.Throws<NullReferenceException>(() => importer.Backup(null));

        }

        [Fact]
        public void TestBackupForFileNotFound()
        {
            //Arrange
            ImportData model = CreateModel();
            NotfallImporter importer = new NotfallImporter(model);
            importer.setFileSystem(_fileSystem);

            //Act
            //Assert
            Assert.Throws<FileNotFoundException>(() => importer.Backup("not Existing"));

        }
        [Fact]
        public void TestBackup()
        {
            //Arrange
            ImportData model = CreateModel();
            NotfallImporter importer = new NotfallImporter(model);
            importer.setFileSystem(_fileSystem);

            //Act
            importer.Backup(@"c:\NotfallImporter\Error\eml_20190220123417_99802_0000009200.eml");

            //Assert
            Assert.True(_fileSystem.File.Exists(String.Format(@"c:\NotfallImporter\Backup\Backup{0}\eml_20190220123417_99802_0000009200.eml", DateTime.Today.ToString("dd_MM_yy"))));

        }

        [Fact]
        public void TestExtractImports()
        {
            //Arrange
            ImportData model = CreateModel();
            NotfallImporter importer = new NotfallImporter(model);
            importer.setFileSystem(_fileSystem);

            //Act
            List<string> imports = importer.ExtractImports();


            //Assert
            string[] assertImports = { @"c:\NotfallImporter\Error\eml_20190220123417_99802_0000009200.eml",
                @"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip" };

            Assert.Equal(imports.ToArray(), assertImports);

        }

        public ImportData CreateModel()
        {
            ImportData model = new ImportData();
            model._error_directory = @"c:\NotfallImporter\Error";
            model._backup_directory = @"c:\NotfallImporter\Backup";
            model._import_directory = @"c:\NotfallImporter\Import";
            return model;
        }
                

        public TestNotfallImporter()
        {
            _fileSystem = FakeFileSystem.createFileSystem();
        }
    }
}
