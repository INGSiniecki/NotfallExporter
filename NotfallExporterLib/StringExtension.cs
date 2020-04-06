using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotfallExporterLib
{
    public static class StringExtension
    {
        public static string RemoveFileExtension(this string str)
        {
            return str.Split('.')[0];
        }

        public static string GetFileExtension(this string str)
        {
            return str.Split('.')[1];
        }

        public static string GetFileName(this string str)
        {
            string[] elements = str.Split('\\');
            return elements[elements.Length-1];
        }

        public static string ExtractMSN(this string str)
        {
            return str.Split('_')[2];
        }

        public static string GetPaginierNr(this string str)
        {
            return str.Split('_')[2] + str.Split('_')[3];
        }

        public static string GetVermittlerNr(this string str)
        {
            string[] elements = str.Split('_');
            if (elements.Length == 6)
                return str.Split('_')[5];
            else
                return "";

        }
    }
}
