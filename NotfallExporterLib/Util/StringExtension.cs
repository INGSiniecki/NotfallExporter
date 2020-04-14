

namespace Com.Ing.DiBa.NotfallExporterLib.Util
{
    public static class StringExtension
    {
        /// <summary>
        /// removes the File Extension
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveFileExtension(this string str)
        {
            return str.Split('.')[0];
        }

        /// <summary>
        /// returns the File Extension
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetFileExtension(this string str)
        {
            return str.Split('.')[1];
        }

        /// <summary>
        /// returns the Filename
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetFileName(this string str)
        {
            string[] elements = str.Split('\\');
            return elements[elements.Length-1];
        }

        /// <summary>
        /// extracts the MSN from the filename
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ExtractMSN(this string str)
        {
            return str.Split('_')[2];
        }

        /// <summary>
        /// extracts the PaginierNr from the filename
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetPaginierNr(this string str)
        {
            return str.Split('_')[2] + str.Split('_')[3];
        }

        /// <summary>
        /// extracts the VermittlerNr from the filename
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
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
