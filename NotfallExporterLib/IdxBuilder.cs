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
    public class IdxBuilder : IdxBuilderModel, IIdxBuilder
    {

        public IdxBuilder(string destPath, IFileSystem fileSystem)
        {

            if (fileSystem == null)
                _fileSystem = new FileSystem();
            else
                _fileSystem = fileSystem;


            _destPath = destPath;
        }


        public Idx CreateIdx(string importedFilePath)
        {
            if (!_fileSystem.Directory.Exists(_destPath))
                throw new DirectoryNotFoundException();

            string idxInput = FillIdx(importedFilePath);

            //creates the file
            string destFile = Path.Combine(_destPath, importedFilePath.GetFileName().RemoveFileExtension() + ".idx");

            //write infos in the file
            _fileSystem.File.WriteAllText(destFile, idxInput);
            Log.Info(String.Format("Creating: {0}", destFile));

            return new Idx(destFile);
        }

        private string FillIdx(string importedFilePath)
        {

            StringBuilder idxLines = new StringBuilder();

            XmlNode indexRoot = IdxIndexSpecification.ChildNodes[2];

            XmlNode account = GetAccountNode(importedFilePath);

            using (ZipArchive archive = new ZipArchive(_fileSystem.File.OpenRead(importedFilePath), ZipArchiveMode.Read))
            {
                //iterating through all entries in the zip file
                foreach(ZipArchiveEntry entry in archive.Entries)
                {
                    idxLines.AppendLine(CreateIdxLine(entry.Name, indexRoot, account, importedFilePath));
                }
            }
            return idxLines.ToString();
        }

        private XmlNode GetAccountNode(string importedFilePath)
        {
            XmlNode routing = AccountConfig.ChildNodes[2];

            foreach(XmlNode account in routing)
            {
                if (account.FirstChild.InnerText.Equals(importedFilePath.ExtractMSN()))
                    return account;
            }
            return null;
        }

        private string CreateIdxLine(string entryname, XmlNode indexRoot, XmlNode account, string importedFilePath)
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
                if (index.InnerText.Equals("VermittlerNr") && importedFilePath.Split('_')[0].Equals("vmi"))
                    importedFilePath.GetVermittlerNr();


                if (index.InnerText.Equals("PaginierNr"))
                    line.Append(account.ChildNodes.Item(2).Name + entryname.GetPaginierNr());

                line.Append(";");


            }

            return line.ToString().Substring(0, line.Length - 1);
        }

    }
}
