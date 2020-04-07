using NotfallExporterLib;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace TestNotFallExporterLib
{
    public class ImportTests
    {
        MockFileSystem _fileSystem;

        [Fact]
        public void TestStartForZip()
        {
            //Arrange
            Import import = new Import(@"c:\NotfallImporter\Import",  @"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip", _fileSystem);
            IdxBuilder idxBuilder = new IdxBuilder(@"c:\NotfallImporter\Error", _fileSystem); 
            IdxBuilderTests.InitializeXml(idxBuilder);

            //Act
            import.Start(idxBuilder);
            //Assert
            Assert.True(_fileSystem.File.Exists(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip"));
            
        }

        [Fact]
        public void TestStartForEml()
        {
            //Arrange
            Import import = new Import(@"c:\NotfallImporter\Import", @"c:\NotfallImporter\Error\eml_20190220123417_99802_0000009200.eml", _fileSystem);

            IdxBuilder idxBuilder = new IdxBuilder(@"c:\NotfallImporter\Error", _fileSystem);
            IdxBuilderTests.InitializeXml(idxBuilder);

            //Act
            import.Start(idxBuilder);
            //Assert
            Assert.True(_fileSystem.File.Exists(@"c:\NotfallImporter\Import\eml_20190220123417_99802_0000009200.zip"));

        }

        [Fact]
        public void TestCreateRdyForFilesNotExisting()
        {
            //Arrange
            Import import = new Import(@"c:\NotfallImporter\Import", @"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip", _fileSystem);
            //Act
            import.CreateRdy();

            //Assert
            Assert.False(_fileSystem.File.Exists(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.rdy"));

        }

        [Fact]
        public void TestCreateRdyForFilesExisting()
        {
            //Arrange
            Import import = new Import(@"c:\NotfallImporter\Import", @"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip", _fileSystem);

            IdxBuilder idxBuilder = new IdxBuilder(@"c:\NotfallImporter\Error", _fileSystem);
            IdxBuilderTests.InitializeXml(idxBuilder);


            import.Start(idxBuilder);
            //Act
            import.CreateRdy();

            //Assert
            Assert.True(_fileSystem.File.Exists(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.rdy"));

        }
        public ImportTests()
        {
            _fileSystem = _fileSystem = FakeFileSystem.CreateFileSystem();
        }
    }
}
