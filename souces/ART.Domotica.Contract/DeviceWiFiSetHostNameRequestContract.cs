namespace ART.Domotica.Contract
{
    using System;

    public class DeviceWiFiSetHostNameRequestContract
    {
        #region Properties

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceWiFiId
        {
            get; set;
        }

        public string HostName
        {
            get; set;
        }

        #endregion Properties
    }
}