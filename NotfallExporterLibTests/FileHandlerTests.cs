
using Com.Ing.DiBa.NotfallExporterLib.Api;
using Com.Ing.DiBa.NotfallExporterLib.File;
using System;
using System.IO;
using System.IO.Abstractions;
using Xunit;

namespace Com.Ing.DiBa.NotfallExporterLibTests
{
    public class FileHandlerTests
    {
        readonly FileHandler _fileHandler;

        readonly string _testZipFile = @"c:\NotfallImporter\Error\vmi_20190304121156_99998_0000798569_0170631125_0123456789.zip";
        readonly string _testEmlFile = @"c:\NotfallImporter\Error\eml_20190220123417_99802_0000009200.eml";

        readonly ExportModel _exportModel = new ExportModel()
        {
            ErrorDirectory = @"c:\NotfallImporter\Error",
            BackupDirectory = @"c:\NotfallImporter\Backup",
            ImportDirectory = @"c:\NotfallImporter\Import",
            AccountConfig = @"c:\NotfallImporter\AccountConfig.xml",
            IdxIndexSpecification = @"c:\NotfallImporter\IdxIndexSpezifikation.xml"
        };

        [Fact]
        public void TestBackupFile()
        {
            //Arrange
            _fileHandler.FileSys = TestFileSystem.CreateFileSystem();

            //Act
            _fileHandler.BackupFile(_fileHandler.FileSys.FileInfo.FromFileName(_testZipFile), _exportModel.BackupDirectory);

            //Assert
            DateTime currentDate = DateTime.Today;
            string backupFile = Path.Combine(@"c:\NotfallImporter\Backup", "Backup" + currentDate.ToString("dd_MM_yy"), Path.GetFileName(_testZipFile));

            Assert.True(_fileHandler.FileSys.File.Exists(backupFile));
            Assert.False(_fileHandler.FileSys.File.Exists(_testZipFile));
        }

        [Fact]
        public void TestBackupFileForFileNotFound()
        {
            //Arrange
            _fileHandler.FileSys = TestFileSystem.CreateFileSystem();

            //Act
            Action a = () => _fileHandler.BackupFile(_fileHandler.FileSys.FileInfo.FromFileName(@"c:\NotExisting.txt"), _exportModel.BackupDirectory);

            Assert.Throws<FileNotFoundException>(a);
        }

        [Fact]
        public void TestCheckModel()
        {
            //Arrange
            _fileHandler.FileSys = TestFileSystem.CreateFileSystem();

            //Act
            _fileHandler.CheckModel(_exportModel);
        }

        [Fact]
        public void TestCheckModelNotExisting()
        {
            //Arrange
            _fileHandler.FileSys = TestFileSystem.CreateFileSystem();

            ExportModel model = new ExportModel()
            {
                ErrorDirectory = @"c:\NotfallImporter\NotExisting",
                BackupDirectory = @"c:\NotfallImporter\NotExisting",
                ImportDirectory = @"c:\NotfallImporter\NotExisting",
                AccountConfig = @"c:\NotfallImporter\NotExisting",
                IdxIndexSpecification = @"c:\NotfallImporter\NotExisting"
            };


            //Act
            Action a = () => _fileHandler.CheckModel(new ExportModel() { BackupDirectory = @"c:\NotExisting" });

            Assert.Throws<DirectoryNotFoundException>(a);
        }

        [Fact]
        public void TestCreateReadyFile()
        {
            //Arrange
            _fileHandler.FileSys = TestFileSystem.CreateFileSystem();

            _fileHandler.FileSys.File.Create(Path.Combine(_exportModel.ErrorDirectory, Path.ChangeExtension(Path.GetFileName(_testZipFile), "idx")));

            //Act
            _fileHandler.CreateReadyFile(_fileHandler.FileSys.FileInfo.FromFileName(_testZipFile));

            //Assert
            string readyFile = Path.Combine(_exportModel.ErrorDirectory, Path.ChangeExtension(Path.GetFileName(_testZipFile), "rdy"));
            Assert.True(_fileHandler.FileSys.File.Exists(readyFile));
        }

        [Fact]
        public void TestCreateReadyFileForIdxNotExisting()
        {
            //Arrange
            _fileHandler.FileSys = TestFileSystem.CreateFileSystem();

            //Act
            _fileHandler.CreateReadyFile(_fileHandler.FileSys.FileInfo.FromFileName(_testZipFile));

            //Assert
            string readyFile = Path.Combine(_exportModel.ErrorDirectory, Path.ChangeExtension(Path.GetFileName(_testZipFile), "rdy"));
            Assert.False(_fileHandler.FileSys.File.Exists(readyFile));
        }

        [Fact]
        public void TestGetImportFiles()
        {
            //Arrange
            _fileHandler.FileSys = TestFileSystem.CreateFileSystem();

            //Act
            IFileInfo[] files = _fileHandler.GetImportFiles(_exportModel.ErrorDirectory);


            //Assert
            Assert.Contains(_fileHandler.FileSys.FileInfo.FromFileName(_testZipFile), files);
            Assert.Contains(_fileHandler.FileSys.FileInfo.FromFileName(_testEmlFile), files);
           Assert.DoesNotContain(_fileHandler.FileSys.FileInfo.FromFileName(@"c:\NotfallImporter\Error\TextFile.txt"), files);

        }


        [Fact]
        public void TestZipEmailFileToForFileAlreadyExists()
        {
            //Arrange
            _fileHandler.FileSys = TestFileSystem.CreateFileSystem();

            //Act
            _fileHandler.ExportEmlFile(_fileHandler.FileSys.FileInfo.FromFileName(_testZipFile), _exportModel.ImportDirectory);

            //Assert
            string zipFile = Path.Combine(_exportModel.ImportDirectory, Path.GetFileNameWithoutExtension(_testEmlFile) + ".zip");
            Assert.True(_fileHandler.FileSys.File.Exists(zipFile));
        }



        public FileHandlerTests()
        {
            _fileHandler = new FileHandler();
            _fileHandler.FileSys = TestFileSystem.CreateFileSystem();
        }
    }
}
