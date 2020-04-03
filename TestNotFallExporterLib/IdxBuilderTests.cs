using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NotfallExporterLib;
using System.IO.Abstractions.TestingHelpers;
using System.IO;

namespace TestNotFallExporterLib
{
    public class IdxBuilderTests
    {
        private MockFileSystem _fileSystem;
        [Fact]
        public void TestCreateIdx()
        {
            //Arrange
            IdxBuilder idxBuilder = new IdxBuilder(@"c:\NotfallImporter\Import");
            idxBuilder.setFileSystem(_fileSystem);

            //Act
            Idx idx = idxBuilder.CreateIdx(@"c:\NotfallImporter\Error\eml_20190220123417_99802_0000009200.eml");

            //Assert
            Assert.True(_fileSystem.File.Exists(@"c:\NotfallImporter\Import\eml_20190220123417_99802_0000009200.idx"));

            string text = _fileSystem.File.ReadAllText(@"c:\NotfallImporter\Import\eml_20190220123417_99802_0000009200.idx");

            Assert.Equal("eml;20190220123417;99802;0000009200;",text);
        }

       

        [Fact]
        public void TestCreateIdxForNull()
        {
            //Arrange
            IdxBuilder idxBuilder = new IdxBuilder(@"c:\NotfallImporter\Error\");
            idxBuilder.setFileSystem(_fileSystem);
            Action a = () => idxBuilder.CreateIdx(null);

            //Act and Assert
            Assert.Throws<NullReferenceException>(a);
        }

        public IdxBuilderTests()
        {
            _fileSystem = _fileSystem = FakeFileSystem.createFileSystem();
        }
    }
}
