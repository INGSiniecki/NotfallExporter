
using System;

namespace Com.Ing.DiBa.NotfallExporterLib.Api
{
    public delegate void DelegateMessage(string message);
    public interface IMessenger
    {
         DelegateMessage Message { get; set; }
        void SendMessage(string message);
        void sendError(Exception e);
    }
}
