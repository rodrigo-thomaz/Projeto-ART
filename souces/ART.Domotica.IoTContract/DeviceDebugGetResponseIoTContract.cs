namespace ART.Domotica.IoTContract
{
    public class DeviceDebugGetResponseIoTContract
    {
        #region Properties

        public bool RemoteEnabled
        {
            get; set;
        }

        public bool ResetCmdEnabled
        {
            get; set;
        }

        public bool SerialEnabled
        {
            get; set;
        }

        public bool ShowColors
        {
            get; set;
        }

        public bool ShowDebugLevel
        {
            get; set;
        }

        public bool ShowProfiler
        {
            get; set;
        }

        public bool ShowTime
        {
            get; set;
        }

        #endregion Properties
    }
}