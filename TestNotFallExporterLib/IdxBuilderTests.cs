using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NotfallExporterLib;
using System.IO.Abstractions.TestingHelpers;
using System.IO;
using System.Xml;
using System.IO.Compression;

namespace TestNotFallExporterLib
{
    public class IdxBuilderTests
    {
        private MockFileSystem _fileSystem;
        [Fact]
        public void TestCreateIdx()
        {
            //Arrange
            IdxBuilder idxBuilder = new IdxBuilder(@"c:\NotfallImporter\Import", _fileSystem);

            InitializeXml(idxBuilder);

            using (ZipArchive archive = new ZipArchive(_fileSystem.File.Create(@"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip"), ZipArchiveMode.Create))
            {
                archive.CreateEntry("20190304121156_0803230050_0803230050_1551697916302_1_diba_infoblatt.pdf");
            }

            //Act
            Idx idx = idxBuilder.CreateIdx(@"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip");

            //Assert
            Assert.True(_fileSystem.File.Exists(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.idx"));
            string idxText = _fileSystem.File.ReadAllText(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.idx");
            Assert.Equal("99998;200\r\n", idxText);

        }

        public static void InitializeXml(IdxBuilder idxBuilder)
        {
            idxBuilder.IdxIndexSpecification = new XmlDocument();
            idxBuilder.IdxIndexSpecification.LoadXml("<?xml version='1.0' ?><!--comment--><Index><I1>Testwert1</I1><I2>Testwert2</I2></Index>");

            idxBuilder.AccountConfig = new XmlDocument();
            idxBuilder.AccountConfig.LoadXml("<?xml version='1.0' ?><!--comment--><Routing><Account><Testwert1>99802</Testwert1><Testwert2>100</Testwert2></Account><Account><Testwert1>99998</Testwert1><Testwert2>200</Testwert2>T</Account></Routing>");
        }

       

        [Fact]
        public void TestCreateIdxForNull()
        {
            //Arrange
            IdxBuilder idxBuilder = new IdxBuilder(@"c:\NotfallImporter\Error\", _fileSystem);
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
