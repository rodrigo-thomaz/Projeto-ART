namespace ART.Domotica.Contract
{
    public class ESPDeviceCheckForUpdatesRPCResponseContract
    {
        #region Properties

        public byte[] Buffer
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        #endregion Properties
    }
}