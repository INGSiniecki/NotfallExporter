

namespace Com.Ing.DiBa.NotfallExporterLib.Util
{
    /// <summary>
    /// Class to Log
    /// </summary>
    public static class Log
    {
         static log4net.ILog Logger { get; } = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
