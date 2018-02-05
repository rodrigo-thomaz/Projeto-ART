namespace ART.Domotica.Model
{
    using System;

    public class DeviceWiFiSetHostNameModel
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