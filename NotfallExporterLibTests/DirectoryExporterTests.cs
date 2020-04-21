
using Xunit;
using Com.Ing.DiBa.NotfallExporterLib;
using Com.Ing.DiBa.NotfallExporterLib.Export;
using Com.Ing.DiBa.NotfallExporterLib.File;

namespace Com.Ing.DiBa.NotfallExporterLibTests
{
    public class DirectoryExporterTests
    {
        private FileHandler _fileHandler;
       
        [Fact]
        public void TestStart()
        {
            //Arrange
            ExportModel model = CreateModel();
            DirectoryExporter importer = new DirectoryExporter(model, _fileHandler);

            //Act
            importer.Start();

            //Assert
            Assert.True(_fileHandler.FileSys.File.Exists(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.idx"));
            Assert.True(_fileHandler.FileSys.File.Exists(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip"));
            Assert.True(_fileHandler.FileSys.File.Exists(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.rdy"));

            Assert.True(_fileHandler.FileSys.File.Exists(@"c:\NotfallImporter\Import\eml_20190220123417_99802_0000009200.zip"));
            Assert.True(_fileHandler.FileSys.File.Exists(@"c:\NotfallImporter\Import\eml_20190220123417_99802_0000009200.idx"));
            Assert.True(_fileHandler.FileSys.File.Exists(@"c:\NotfallImporter\Import\eml_20190220123417_99802_0000009200.rdy"));
        }


        public ExportModel CreateModel()
        {
            ExportModel model = new ExportModel()
            {
                ErrorDirectory = @"c:\NotfallImporter\Error",
                BackupDirectory = @"c:\NotfallImporter\Backup",
                ImportDirectory = @"c:\NotfallImporter\Import",
                AccountConfig = @"c:\NotfallImporter\AccountConfig.xml",
                IdxIndexSpecification = @"c:\NotfallImporter\IdxIndexSpezifikation.xml"
            };
            return model;
        }
                

        public DirectoryExporterTests()
        {
            _fileHandler = new FileHandler();
            _fileHandler.FileSys = TestFileSystem.CreateFileSystem();
        }
    }
}
