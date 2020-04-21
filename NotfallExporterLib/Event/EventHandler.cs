namespace Com.Ing.DiBa.NotfallExporterLib.Event
{
    /// <summary>
    /// Handler for DirectoryExportEvents
    /// </summary>
    /// <param name="eventSender">object which has sent the event</param>
    /// <param name="args">EventArguments</param>
    public delegate void DirectoryExportEventHandler(object eventSender, DirectoryExportEventArgs args);
    /// <summary>
    /// Handler for FileExportEvents
    /// </summary>
    /// <param name="eventSender">object which has sent the event</param>
    /// <param name="args">EventArguments</param>
    public delegate void FileExportEventHandler(object eventSender, FileExportEventArgs args);
    /// <summary>
    /// Handler for WarnEvents
    /// </summary>
    /// <param name="eventSender">object which has sent the event</param>
    /// <param name="args">EventArguments</param>
    public delegate void WarnEventHandler(object eventSender, WarnEventArgs args);
    /// <summary>
    /// Handler for ErrorEvents
    /// </summary>
    /// <param name="eventSender">object which has sent the event</param>
    /// <param name="args">EventArguments</param>
    public delegate void ErrorEventHandler(object eventSender, ErrorEventArgs args);
} 