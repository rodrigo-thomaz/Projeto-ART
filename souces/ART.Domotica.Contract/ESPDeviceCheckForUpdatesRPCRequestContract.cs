namespace ART.Domotica.Contract
{
    public class ESPDeviceCheckForUpdatesRPCRequestContract
    {
        #region Properties

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

        public string SoftAPMacAddress
        {
            get; set;
        }

        public string StationMacAddress
        {
            get; set;
        }

        #endregion Properties
    }
}