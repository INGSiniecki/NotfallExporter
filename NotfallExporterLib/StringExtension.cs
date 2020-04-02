using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotfallExporterLib
{
    public static class StringExtension
    {
        public static string removeFileExtension(this string str)
        {
            return str.Split('.')[0];
        }

        public static string getFileExtension(this string str)
        {
            return str.Split('.')[1];
        }

        public static string GetFileName(this string str)
        {
            string[] elements = str.Split('\\');
            return elements[elements.Length-1];
        }
    }
}
