using System;
using Xunit;
using Com.Ing.DiBa.NotfallExporterLib.Idx;
using Com.Ing.DiBa.NotfallExporterLib.File;
using System.IO;
using Com.Ing.DiBa.NotfallExporterLib.File.Xml;
using Com.Ing.DiBa.NotfallExporterLib.File.Export;

namespace TestNotFallExporterLib
{
    public class IdxBuilderTests
    {
        private FileHandler _fileHandler;
        [Fact]
        public void TestCreateIdx()
        {
            //Arrange

            XmlAccountConfig accountConfig = new XmlAccountConfig(@"c:\NotfallImporter\AccountConfig.xml", _fileHandler);
            XmlIdxIndexSpecification indexSpecification = new XmlIdxIndexSpecification(@"c:\NotfallImporter\IdxIndexSpezifikation.xml", _fileHandler);
            ExportFile exportFile = new ExportFile(_fileHandler.FileSys.FileInfo.FromFileName(@"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip"));

            IdxBuilder idxBuilder = new IdxBuilder(accountConfig, indexSpecification, _fileHandler);
            //Act
            IdxRepresentation idx = idxBuilder.BuildIdx(exportFile);

            //Assert
            Assert.True(_fileHandler.FileSys.File.Exists(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.idx"));
            string idxText = _fileHandler.FileSys.File.ReadAllText(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.idx");
            Assert.Equal("99998;200\r\n", idxText);

        }

        [Fact]
        public void TestCreateIdxForDestinationNotExisting()
        {
            //Arrange
            XmlAccountConfig accountConfig = new XmlAccountConfig(@"c:\NotfallImporter\AccountConfig.xml", _fileHandler);
            XmlIdxIndexSpecification indexSpecification = new XmlIdxIndexSpecification(@"c:\NotfallImporter\IdxIndexSpezifikation.xml", _fileHandler);
            ExportFile exportFile = new ExportFile(_fileHandler.FileSys.FileInfo.FromFileName(@"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip"));

            IdxBuilder idxBuilder = new IdxBuilder(accountConfig, indexSpecification);
            Action a = () => idxBuilder.BuildIdx(exportFile);

            //Act and Assert
            Assert.Throws<DirectoryNotFoundException>(a);
        }

        public IdxBuilderTests()
        {
            _fileHandler = new FileHandler();
            _fileHandler.FileSys = TestFileSystem.CreateFileSystem();
        }
    }
}
