using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Abstractions;
using System.IO;

namespace NotfallExporterLib
{
    /*
     * creates and initializes IdxFiles
     */
    public class IdxBuilder : IdxModel, IIdxBuilder, FileSystemAbstraction
    {

        public IdxBuilder(String path)
        {
            _fileSystem = new FileSystem();

            _destPath = path;
        }


        public Idx CreateIdx(string src_file)
        {
            if (!_fileSystem.Directory.Exists(_destPath))
                throw new DirectoryNotFoundException();

            StringBuilder text = new StringBuilder();

            string[] elements = src_file.GetFileName().removeFileExtension().Split('_');

            //extracts the infos from the filename
            foreach(string element in elements) {
                text.Append(element);
                text.Append(";");
            }

            //creates the file
            string dest_file = _destPath + "\\" + src_file.GetFileName().removeFileExtension() + ".idx";

            //write infos in the file
            _fileSystem.File.WriteAllText(dest_file, text.ToString());
            log.Info(String.Format("Creating: {0}", dest_file));

            return new Idx(dest_file);
        }

        public void setFileSystem(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
    }
}
