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
    public class IdxBuilder : FileSystemAbstraction
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string _destPath;
        public IdxBuilder(String path, IFileSystem fileSystem = null) : base(fileSystem)
        {
            if (!_fileSystem.Directory.Exists(path))
                throw new DirectoryNotFoundException();

            _destPath = path;
        }


        public Idx createIdx(string src_file)
        {
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


    }
}
