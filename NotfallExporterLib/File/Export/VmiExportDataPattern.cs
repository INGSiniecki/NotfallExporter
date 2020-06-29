using System;


namespace Com.Ing.DiBa.NotfallExporterLib.File.Export
{
    /// <summary>
    /// Class to represent the filename pattern for Upload files
    /// </summary>
    public class VmiExportDataPattern : ExportDataPattern
    {
        public const int MINLENGTH = 5;
        public const int MAXLENGTH = 6;
        public const string DOXISUSER = "vmi";

        public const int VorgangsIDIndex = 4;
        public const int VermittlerIDIndex = 5;

        /// <summary>
        /// checks if the elements of an upload file match the pattern
        /// </summary>
        /// <param name="elements">elements of an upload(zip) file</param>
        /// <returns></returns>
        public override bool IsValid(string[] elements)
        {
            if (elements.Length < MINLENGTH || elements.Length > MAXLENGTH)
                return false;

            if (!elements[(int)ExportDataIndex.DoxisUser].Equals(DOXISUSER))
                return false;

            if (!IsContentValid(elements))
                return false;

            return true;
        }
    }
}
