using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Abstractions;
using System.IO;
using System.IO.Compression;
using System.Xml;

namespace NotfallExporterLib
{
    /*
     * creates and initializes IdxFiles
     */
    public class IdxBuilder : IdxBuilderModel, IIdxBuilder, FileSystemAbstraction
    {

        public IdxBuilder(string destPath)
        {
            _fileSystem = new FileSystem();

            _destPath = destPath;
        }


        public Idx CreateIdx(string importFilePath)
        {
            if (!_fileSystem.Directory.Exists(_destPath))
                throw new DirectoryNotFoundException();

            string idxInput = FillIdx(importFilePath);

            //creates the file
            string dest_file = Path.Combine(_destPath, importFilePath.GetFileName().RemoveFileExtension() + ".idx");

            //write infos in the file
            _fileSystem.File.WriteAllText(dest_file, idxInput);
            log.Info(String.Format("Creating: {0}", dest_file));

            return new Idx(dest_file);
        }

        private string FillIdx(string importFilePath)
        {

            StringBuilder idxLines = new StringBuilder();

            XmlNode indexRoot = IdxIndexSpecification.ChildNodes[2];

            XmlNode account = GetAccountNode(importFilePath);

            using (ZipArchive archive = new ZipArchive(_fileSystem.File.OpenRead(importFilePath), ZipArchiveMode.Read))
            {
                //iterating through all entries in the zip file
                foreach(ZipArchiveEntry entry in archive.Entries)
                {
                    idxLines.AppendLine(CreateIdxLine(entry.Name, indexRoot, account, importFilePath));
                }
            }
            return idxLines.ToString();
        }

        private XmlNode GetAccountNode(string importFilePath)
        {
            XmlNode routing = AccountConfig.ChildNodes[2];

            foreach(XmlNode account in routing)
            {
                if (account.FirstChild.InnerText.Equals(importFilePath.ExtractMSN()))
                    return account;
            }
            return null;
        }

        private string CreateIdxLine(string entryname, XmlNode indexRoot, XmlNode account, string importFilePath)
        {
            StringBuilder line = new StringBuilder();
            foreach(XmlNode index in indexRoot.ChildNodes)
            {
                foreach(XmlNode data in account.ChildNodes)
                {
                    //read data from AccountConfig
                    if(index.InnerText.Equals(data.Name))
                        line.Append(data.InnerText);


                    
                }

                //define dynamic data

                if (index.InnerText.Equals("BCount"))
                    line.Append("1");

                //VermittlerNr only in uploads
                if (index.InnerText.Equals("VermittlerNr") && importFilePath.Split('_')[0].Equals("vmi"))
                    importFilePath.GetVermittlerNr();


                if (index.InnerText.Equals("PaginierNr"))
                    line.Append(account.ChildNodes.Item(2).Name + entryname.GetPaginierNr());

                line.Append(";");


            }

            return line.ToString().Substring(0, line.Length - 1);
        }

        public void SetFileSystem(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
    }
}
