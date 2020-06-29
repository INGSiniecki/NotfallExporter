using System;
using System.Text.RegularExpressions;

namespace Com.Ing.DiBa.NotfallExporterLib.File.Export
{
    public abstract class ExportDataPattern : IExportDataPattern
    {
        
        private const string ContentPattern = "^[0-9]*$";
        /// <summary>
        /// Index of ExportData in a export files filename
        /// </summary>
        public enum ExportDataIndex 
        {
            DoxisUser = 0,
            Datumsstempel = 1,
            RoutingID = 2,
            EindeutigeID = 3,
            VorgansID = 4,
            VermittlerID = 5
        }
        /// <summary>
        /// checks if the elements of a export file match the ContentPattern
        /// </summary>
        /// <param name="elements">elements of an export file</param>
        /// <returns></returns>
        protected bool IsContentValid(string[] elements)
        {
            for(int i = 0; i < elements.Length; i++)
            {
                if (i != (int)ExportDataIndex.DoxisUser && !Regex.IsMatch(elements[i], ContentPattern))
                    return false;
            }
            return true;
        }

        public abstract bool IsValid(string[] elements);
    }
}
