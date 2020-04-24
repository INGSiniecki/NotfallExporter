

using Com.Ing.DiBa.NotfallExporterLib.Util;

namespace Com.Ing.DiBa.NotfallExporterLib.File.Export
{
    /// <summary>
    /// Class to build ExportFileData objects
    /// </summary>
    public class ExportFileDataBuilder
    {
        /// <summary>
        /// builds an ExportFileData object from the name of a file
        /// </summary>
        /// <param name="exportFileName">name of a file</param>
        /// <returns></returns>
        public ExportFileData BuildDataFromFileName(string exportFileName)
        {
            ExportFileData exportFileData = null;

            string[] elements = exportFileName.RemoveFileExtension().Split('_');

            IExportDataPattern pattern = new EmlExportDataPattern();

            if(pattern.IsValid(elements))
            {
                exportFileData = CreateMainData(elements);
            }

            pattern = new VmiExportDataPattern();
            if(pattern.IsValid(elements))
            {
                exportFileData = CreateVmiData(elements);
            }

            return exportFileData;
            
        }


        private ExportFileData CreateMainData(string[] elements)
        {
            return new ExportFileData()
            {
                DoxisUser = elements[(int)ExportDataPattern.ExportDataIndex.DoxisUser],
                Datumsstempel = elements[(int)ExportDataPattern.ExportDataIndex.Datumsstempel],
                RoutingID = elements[(int)ExportDataPattern.ExportDataIndex.RoutingID],
                EindeutigeID = elements[(int)ExportDataPattern.ExportDataIndex.EindeutigeID]
            };
        }

        private ExportFileData CreateVmiData(string[] elements)
        {
            ExportFileData data = CreateMainData(elements);
            data.VorgangsID = elements[(int)ExportDataPattern.ExportDataIndex.VorgansID];
            if (elements.Length == 6)
                data.VermittlerID = elements[(int)ExportDataPattern.ExportDataIndex.VermittlerID];

            return data;
        }

    }
}
