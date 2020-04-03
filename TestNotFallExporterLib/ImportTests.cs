using NotfallExporterLib;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace TestNotFallExporterLib
{
    public class ImportTests
    {
        MockFileSystem _fileSystem;

        [Fact]
        public void testStartForZip()
        {
            //Arrange
            Import import = new Import(@"c:\NotfallImporter\Import",  @"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip");
            import.setFileSystem(_fileSystem);
            //Act
            import.start();
            //Assert
            Assert.True(_fileSystem.File.Exists(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip"));
            
        }

        [Fact]
        public void testStartForEml()
        {
            //Arrange
            Import import = new Import(@"c:\NotfallImporter\Import", @"c:\NotfallImporter\Error\eml_20190220123417_99802_0000009200.eml");
            import.setFileSystem(_fileSystem);
            //Act
            import.start();
            //Assert
            Assert.True(_fileSystem.File.Exists(@"c:\NotfallImporter\Import\eml_20190220123417_99802_0000009200.zip"));

        }

        [Fact]
        public void testCreateRdyForFilesNotExisting()
        {
            //Arrange
            Import import = new Import(@"c:\NotfallImporter\Import", @"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip");
            import.setFileSystem(_fileSystem);
            //Act
            import.CreateRdy();

            //Assert
            Assert.False(_fileSystem.File.Exists(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.rdy"));

        }

        [Fact]
        public void testCreateRdyForFilesExisting()
        {
            //Arrange
            Import import = new Import(@"c:\NotfallImporter\Import", @"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip");
            import.setFileSystem(_fileSystem);
            import.start();
            //Act
            import.CreateRdy();

            //Assert
            Assert.True(_fileSystem.File.Exists(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.rdy"));

        }
        public ImportTests()
        {
            _fileSystem = _fileSystem = FakeFileSystem.createFileSystem();
        }
    }
}
