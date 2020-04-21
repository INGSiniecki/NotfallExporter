

namespace Com.Ing.DiBa.NotfallExporterLib.Event
{
    /// <summary>
    /// Class to contain Arguments for WarnEvents
    /// </summary>
    public class WarnEventArgs
    {
        /// <summary>
        /// WarnMessage
        /// </summary>
        public string WarnMessage  { set; get;}

        /// <summary>
        /// instantiates an object of WarnEventArgs
        /// </summary>
        /// <param name="warnMessage">WarnMessage</param>
        public WarnEventArgs(string warnMessage)
        {
            WarnMessage = warnMessage;
        }


    }
}
