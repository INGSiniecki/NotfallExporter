using Com.Ing.DiBa.NotfallExporterLib.File;
using Com.Ing.DiBa.NotfallExporterLib.File.Xml;
using Xunit;

namespace TestNotFallExporterLib
{
    public class XmlConfigTests
    {
        private string _accountConfigPath = @"c:\NotfallImporter\AccountConfig.xml";
        private string _idxSpecification = @"c:\NotfallImporter\IdxIndexSpezifikation.xml";

        private FileHandler _fileHandler;

        [Fact]
        public void TestGetRootNode()
        {
            _fileHandler.FileSys = TestFileSystem.CreateFileSystem();

            IXmlIdxIndexSpecification indexSpecification = new XmlIdxIndexSpecification(_idxSpecification, _fileHandler);
            indexSpecification.GetIndexListNode();

            Assert.Equal("Index", indexSpecification.GetIndexListNode().Name);
        }

        [Fact]
        public void TestGetAccountNode()
        {
            _fileHandler.FileSys = TestFileSystem.CreateFileSystem();

            IXmlAccountConfig indexSpecification = new XmlAccountConfig(_accountConfigPath, _fileHandler);

            Assert.Equal("99802", indexSpecification.GetAccountNode("99802").FirstChild.InnerText);
        }

        public XmlConfigTests()
        {
            _fileHandler = new FileHandler();
            _fileHandler.FileSys = TestFileSystem.CreateFileSystem();
        }
    }
}
