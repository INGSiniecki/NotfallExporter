using System;

namespace Com.Ing.DiBa.NotfallExporterLib.File.Export
{
    /// <summary>
    /// Class to represent the filename pattern for Eml files
    /// </summary>
    public class EmlExportDataPattern : ExportDataPattern
    {
        public const int Length = 4;
        public const string DoxisUser = "eml"; 
        
        /// <summary>
        /// checks if the elements of an eml filename match the pattern
        /// </summary>
        /// <param name="elements">elements of an eml file</param>
        /// <returns></returns>
         public override bool IsValid(string[] elements)
        {
            if (elements.Length != Length)
                return false;

            if (!elements[(int)ExportDataIndex.DoxisUser].Equals(DoxisUser))
                return false;

            if (!IsContentValid(elements))
                return false;

            return true;

        }


    }
}
