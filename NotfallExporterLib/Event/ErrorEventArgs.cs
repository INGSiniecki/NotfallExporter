

using System;

namespace Com.Ing.DiBa.NotfallExporterLib.Event
{
    /// <summary>
    /// Class to contain Arguments for ErrorEvents
    /// </summary>
    public class ErrorEventArgs
    {
        /// <summary>
        /// Exception object which was thrown
        /// </summary>
        public Exception Exception { set; get; }

        /// <summary>
        /// instantiates an object of ErrorEventArgs
        /// </summary>
        /// <param name="exception">Exception object which was thrown</param>
        public ErrorEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}
