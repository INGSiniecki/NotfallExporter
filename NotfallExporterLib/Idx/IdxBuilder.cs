

using Com.Ing.DiBa.NotfallExporterLib.File;
using Com.Ing.DiBa.NotfallExporterLib.File.Export;
using Com.Ing.DiBa.NotfallExporterLib.File.Xml;
using Com.Ing.DiBa.NotfallExporterLib.Util;
using NotfallExporterLib.Database;
using NotfallExporterLib.Database.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;

namespace Com.Ing.DiBa.NotfallExporterLib.Idx
{
    /// <summary>
    /// Class to create Idx-Files
    /// </summary>
    public class IdxBuilder : IIdxBuilder
    {

        private readonly IXmlIdxIndexSpecification _idxIndexSpecification;
        private readonly IXmlAccountConfig _accountConfig;
        private readonly IFileHandler _fileHandler;



        /// <summary>
        /// instantiate a object of IdxBuilder
        /// </summary>
        /// <param name="accountConfig">object to represent a AccountConfig.xml</param>
        /// <param name="xmlIdxIndexSpecification">object to represent a IdxIndexSpecification.xml</param>
        public IdxBuilder(IXmlAccountConfig accountConfig, IXmlIdxIndexSpecification xmlIdxIndexSpecification)
        {
            _accountConfig = accountConfig;
            _idxIndexSpecification = xmlIdxIndexSpecification;
            _fileHandler = new FileHandler();
        }
        /// <summary>
        /// instantiates a object of idxBuilder
        /// </summary>
        /// <param name="accountConfig">object to represent a AccountConfig.xml</param>
        /// <param name="xmlIdxIndexSpecification">object to represent a IdxIndexSpecification.xml</param>
        /// <param name="fileHandler">object for FilesSystem operations</param>
        public IdxBuilder(IXmlAccountConfig accountConfig, IXmlIdxIndexSpecification xmlIdxIndexSpecification, IFileHandler fileHandler)
        {
            _accountConfig = accountConfig;
            _idxIndexSpecification = xmlIdxIndexSpecification;
            _fileHandler = fileHandler;
        }

        private void BuildDBIdx(IdxContent content)
        {
            foreach(string line in content.Lines)
                SqliteDataAccess.SaveIdx(IdxDBBuilder.BuildIdxDBModel(line));
        }




        /// <summary>
        /// Creates a IdxFile from the given File
        /// </summary>
        /// <param name="exportFile">file from which to create the Idx-File</param>
        /// <param name="destDirectory">Destination-Directory</param>
        /// <returns></returns>
        public IdxRepresentation BuildIdx(ExportFile exportFile)
        {
            IdxRepresentation idx = new IdxRepresentation
            {
                File = Path.ChangeExtension(exportFile.File.FullName.RemoveFileExtension(), "idx"),
                Content = FillIdx(exportFile)
            };

            BuildDBIdx(idx.Content);

            //write infos in the file
            _fileHandler.FileSys.File.WriteAllText(idx.File, idx.Content.ToString());

            return idx;
        }

        private IdxContent FillIdx(ExportFile exportFile) 
        {
            IdxContent content = new IdxContent();
            content.Lines = new List<string>();

            XmlNode indexRoot = _idxIndexSpecification.GetIndexListNode();

            XmlNode account = _accountConfig.GetAccountNode(exportFile.Data.RoutingID);

            if (indexRoot == null)
                throw new XmlException($"IdxIndexSpecification invalid!");

            if(account == null)
                throw new XmlException($"IdxIndexSpecification invalid!");


            //iterating through all entries in the zip file
            foreach (ZipArchiveEntry entry in _fileHandler.getZipArchiveEntries(exportFile.File))
            {
                content.Lines.Add(CreateIdxLine(entry.Name, indexRoot, account, exportFile));
            }
            return content;
        }

        private string CreateIdxLine(string entryName, XmlNode indexRoot, XmlNode account, ExportFile exportFile)
        {
            StringBuilder line = new StringBuilder();
            foreach (XmlNode index in indexRoot.ChildNodes)
            {
                foreach (XmlNode data in account.ChildNodes)
                {
                    //read data from AccountConfig
                    if (index.InnerText.Equals(data.Name))
                        line.Append(data.InnerText);



                }

                //define dynamic data

                if (index.InnerText.Equals("BCount"))
                    line.Append("1");

                //VermittlerNr only in uploads
                if (index.InnerText.Equals("VermittlerNr") && exportFile.Data.VermittlerID != null)
                    line.Append(exportFile.Data.VermittlerID);


                if (index.InnerText.Equals("PaginierNr"))
                    line.Append(account.ChildNodes.Item(2).InnerText + entryName.GetPaginierNr());

                line.Append(";");


            }

            return line.ToString().Substring(0, line.Length - 1);
        }

    }
}
