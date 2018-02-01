namespace ART.Domotica.Contract
{
    public class DeviceWiFiDetailResponseContract
    {
        #region Properties

        public string HostName
        {
            get; set;
        }

        public int PublishIntervalInMilliSeconds
        {
            get; set;
        }

        #endregion Properties
    }
}