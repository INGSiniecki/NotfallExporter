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
    interface FileSystemAbstraction
    {
         void SetFileSystem(IFileSystem fileSystem);
            
    }
}
