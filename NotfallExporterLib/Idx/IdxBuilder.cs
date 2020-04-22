
using Com.Ing.DiBa.NotfallExporterLib.Event;
using Com.Ing.DiBa.NotfallExporterLib.File;
using Com.Ing.DiBa.NotfallExporterLib.Util;
using Com.Ing.DiBa.NotfallExporterLib.Xml;
using System;
using System.Collections.Generic;
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
        /// Event which fires when an handable Exception occurs
        /// </summary>
        public event ErrorEventHandler ErrorEvent;


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




        /// <summary>
        /// Creates a IdxFile from the given File
        /// </summary>
        /// <param name="sourceFile">file from which to create the Idx-File</param>
        /// <param name="destDirectory">Destination-Directory</param>
        /// <returns></returns>
        public IdxRepresentation BuildIdx(string sourceFile, string destDirectory)
        {
            IdxRepresentation idx = new IdxRepresentation {
                File = System.IO.Path.Combine(destDirectory, sourceFile.GetFileName().RemoveFileExtension() + ".idx"),
                Content = FillIdx(sourceFile)
            };

            //write infos in the file
            _fileHandler.FileSys.File.WriteAllText(idx.File, idx.Content.ToString());

            return idx;
        }

        private IdxContent FillIdx(string sourceFile)
        {
            IdxContent content = new IdxContent();
            content.Lines = new List<string>();

            XmlNode indexRoot = _idxIndexSpecification.GetIndexListNode();

            XmlNode account = _accountConfig.GetAccountNode(sourceFile.ExtractMSN());

            if (indexRoot == null)
            {
                onErrorEvent(new Exception("IdxIndexSpecification.xml corrupt!"));
            }
            else if (account == null)
            {
                onErrorEvent(new Exception("AccountConfig.xml corrupt!"));
            }
            else
            {

                //iterating through all entries in the zip file
                foreach (ZipArchiveEntry entry in _fileHandler.getZipArchiveEntries(sourceFile))
                {
                    content.Lines.Add(CreateIdxLine(entry.Name, indexRoot, account, sourceFile));
                }
            }
            return content;
        }

        private void onErrorEvent(Exception e)
        {
            ErrorEventHandler handler = ErrorEvent;
            handler?.Invoke(this, new ErrorEventArgs(e));
        }



        private string CreateIdxLine(string entryName, XmlNode indexRoot, XmlNode account, string sourceFile)
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
                if (index.InnerText.Equals("VermittlerNr") && sourceFile.Split('_')[0].Equals("vmi"))
                    sourceFile.GetVermittlerNr();


                if (index.InnerText.Equals("PaginierNr"))
                    line.Append(account.ChildNodes.Item(2).InnerText + entryName.GetPaginierNr());

                line.Append(";");


            }

            return line.ToString().Substring(0, line.Length - 1);
        }

    }
}
