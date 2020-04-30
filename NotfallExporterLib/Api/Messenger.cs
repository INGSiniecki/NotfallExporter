using System;

namespace Com.Ing.DiBa.NotfallExporterLib.Api
{
    public class Messenger : IMessenger
    {
        public DelegateMessage Message { get; set; }

        public void sendError(Exception e) => Message(e.StackTrace);

        public void SendMessage(string message) => Message(message);
    }
}
