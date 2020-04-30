



namespace Com.Ing.DiBa.NotfallExporterLib.Api
{
    interface IDirectoryExporter
    {

        IMessenger Messenger { get; set; }
        void Start();
    }
}
