using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NotfallExporterLib;
using System.IO.Abstractions.TestingHelpers;
using System.IO;
using System.Xml;

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
            idxBuilder.SetFileSystem(_fileSystem);

            idxBuilder.IdxIndexSpecification = new System.Xml.XmlDocument();
            idxBuilder.IdxIndexSpecification.LoadXml("<?xml version='1.0' ?><!--comment--><Index><I1>Teswert1</I1><I2>Testwert2</I2></Index>");

            idxBuilder.AccountConfig = new System.Xml.XmlDocument();
            idxBuilder.AccountConfig.LoadXml("<?xml version='1.0' ?><!--comment--><Routing><Account><Testwert1>99802</Testwert1><Testwert2>100</Testwert2></Account><Testwert1>99998</Testwert1><Testwert2>200</Testwert2><Account>Testwert2</Account></Routing>");

            //Act
            Idx idx = idxBuilder.CreateIdx(@"c:\NotfallImporter\Error\eml_20190220123417_99802_0000009200.eml");

            //Assert
            Assert.True(_fileSystem.File.Exists(@"c:\NotfallImporter\Import\eml_20190220123417_99802_0000009200.idx"));

            string text = _fileSystem.File.ReadAllText(@"c:\NotfallImporter\Import\eml_20190220123417_99802_0000009200.idx");

        }

       

        [Fact]
        public void TestCreateIdxForNull()
        {
            //Arrange
            IdxBuilder idxBuilder = new IdxBuilder(@"c:\NotfallImporter\Error\");
            idxBuilder.SetFileSystem(_fileSystem);
            Action a = () => idxBuilder.CreateIdx(null);

            //Act and Assert
            Assert.Throws<NullReferenceException>(a);
        }

        public IdxBuilderTests()
        {
            _fileSystem = _fileSystem = FakeFileSystem.CreateFileSystem();
        }
    }
}
