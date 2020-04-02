using System;
using Xunit;
using NotfallExporterLib;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using System.IO;

namespace TestNotFallExporterLib
{
    public class TestNotfallImporter
    {


        private MockFileSystem _fileSystem;

        [Fact]
        public void TestDirectoryNotFound()
        {
            ImportModel model = new ImportModel();

            Action a = () => new NotfallImporter(model);
            Assert.Throws<DirectoryNotFoundException>(a);

        }

        [Fact]
        public void TestBackupForNull()
        {
            
            ImportModel model = CreateModel();
            NotFallImporterTestHelper importer = new NotFallImporterTestHelper(model, _fileSystem);

            Assert.Throws<NullReferenceException>(() => importer.InvokeBackup(null));

        }

        [Fact]
        public void TestBackupForFileNotFound()
        {

            ImportModel model = CreateModel();
            NotFallImporterTestHelper importer = new NotFallImporterTestHelper(model, _fileSystem);

            Assert.Throws<FileNotFoundException>(() => importer.InvokeBackup("not Existing"));

        }
        [Fact]
        public void TestBackup()
        {
            //Arrange
            ImportModel model = CreateModel();
            NotFallImporterTestHelper importer = new NotFallImporterTestHelper(model, _fileSystem);

            //Act
            importer.InvokeBackup(@"c:\NotfallImporter\Error\eml_20190220123417_99802_0000009200.eml");

            //Assert
            Assert.True(_fileSystem.File.Exists(String.Format(@"c:\NotfallImporter\Backup\Backup{0}\eml_20190220123417_99802_0000009200.eml", DateTime.Today.ToString("dd_MM_yy"))));

        }

        [Fact]
        public void TestExtractImports()
        {
            //Arrange
            ImportModel model = CreateModel();
            NotFallImporterTestHelper importer = new NotFallImporterTestHelper(model, _fileSystem);

            //Act
            List<string> imports = importer.InvokeExtractImports();


            //Assert
            string[] assertImports = { @"c:\NotfallImporter\Error\eml_20190220123417_99802_0000009200.eml",
                @"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip" };

            Assert.Equal(imports.ToArray(), assertImports);

        }

        public ImportModel CreateModel()
        {
            ImportModel model = new ImportModel();
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
