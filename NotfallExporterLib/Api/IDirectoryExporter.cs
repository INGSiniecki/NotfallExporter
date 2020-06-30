



namespace Com.Ing.DiBa.NotfallExporterLib.Api
{
    public interface IDirectoryExporter
    {

        IMessenger Messenger { get; set; }
        void Start();
    }
}
