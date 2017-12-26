namespace ART.Domotica.Contract
{
    public class ESPDeviceCheckForUpdatesRPCRequestContract
    {
        #region Properties

        public string APMacAddress
        {
            get; set;
        }

        public long ChipSize
        {
            get; set;
        }

        public long FreeSpace
        {
            get; set;
        }

        public string Mode
        {
            get; set;
        }

        public string SDKVersion
        {
            get; set;
        }

        public long SketchSize
        {
            get; set;
        }

        public string STAMacAddress
        {
            get; set;
        }

        public string Version
        {
            get; set;
        }

        #endregion Properties
    }
}