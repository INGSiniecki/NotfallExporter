using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Abstractions;

namespace NotfallExporterLib
{
    /*
     * Helper class for Testing.
     * Makes it possible to inject a mocking FileSystem into a sub class
     */
    public class FileSystemAbstraction
    {
        protected IFileSystem _fileSystem;
        public FileSystemAbstraction (IFileSystem fileSystem)
        {
            if (fileSystem == null)
                _fileSystem = new FileSystem();
            else
            _fileSystem = fileSystem;
        }
            
    }
}
