using System;
using Xunit;
using Com.Ing.DiBa.NotfallExporterLib.Idx;
using Com.Ing.DiBa.NotfallExporterLib.File;
using Com.Ing.DiBa.NotfallExporterLib.Xml;
using System.IO;

namespace Com.Ing.DiBa.NotfallExporterLibTests
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

            IdxBuilder idxBuilder = new IdxBuilder(accountConfig, indexSpecification, _fileHandler);
            //Act
            IdxRepresentation idx = idxBuilder.BuildIdx(@"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip", @"c:\NotfallImporter\Import");

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

            IdxBuilder idxBuilder = new IdxBuilder(accountConfig, indexSpecification);
            Action a = () => idxBuilder.BuildIdx(@"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip", @"Not Existing");

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
