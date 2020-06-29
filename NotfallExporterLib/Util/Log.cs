

using log4net;

namespace Com.Ing.DiBa.NotfallExporterLib.Util
{
    /// <summary>
    /// Class to Log
    /// </summary>
    public static class Log
    {
         public static ILog Logger { get; set; } = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
