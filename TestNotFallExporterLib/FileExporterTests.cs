﻿using Com.Ing.DiBa.NotfallExporterLib;
using Com.Ing.DiBa.NotfallExporterLib.Export;
using Com.Ing.DiBa.NotfallExporterLib.File;
using Com.Ing.DiBa.NotfallExporterLib.Idx;
using Com.Ing.DiBa.NotfallExporterLib.Xml;
using System;
using System.IO;
using Xunit;

namespace TestNotFallExporterLib
{
    public class FileExporterTests

    {
        private FileHandler _fileHandler;
        [Fact]
        public void TestStartForZip()
        {
            //Arrange
            FileExporter import = new FileExporter(CreateModel(), _fileHandler);

            //Act
            import.Start(@"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip");

            //Assert
            Assert.True(_fileHandler.FileSys.File.Exists(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip"));
            Assert.True(_fileHandler.FileSys.File.Exists(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.rdy"));
            Assert.True(_fileHandler.FileSys.File.Exists(@"c:\NotfallImporter\Import\vmi_20190304121156_99998_0000798569_0170631125_0123456789.idx"));
        }

        [Fact]
        public void TestStartForEml()
        {
            //Arrange
            FileExporter import = new FileExporter(CreateModel(), _fileHandler);

            //Act
            import.Start(@"c:\NotfallImporter\Error\eml_20190220123417_99802_0000009200.eml");

            //Assert
            Assert.True(_fileHandler.FileSys.File.Exists(@"c:\NotfallImporter\Import\eml_20190220123417_99802_0000009200.zip"));
            Assert.True(_fileHandler.FileSys.File.Exists(@"c:\NotfallImporter\Import\eml_20190220123417_99802_0000009200.idx"));
            Assert.True(_fileHandler.FileSys.File.Exists(@"c:\NotfallImporter\Import\eml_20190220123417_99802_0000009200.rdy"));
        }

        [Fact]
        public void TestStartForFileNotExisting()
        {
            //Arrange
            FileExporter import = new FileExporter(CreateModel(), _fileHandler);

            //Act
            Action a  = () => import.Start(@"C:\NotExisting.txt");

            //Assert
            Assert.Throws<FileNotFoundException>(a);
           
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

        public FileExporterTests()
        {
            _fileHandler = new FileHandler();
            _fileHandler.FileSys = TestFileSystem.CreateFileSystem();
        }
    }
}
