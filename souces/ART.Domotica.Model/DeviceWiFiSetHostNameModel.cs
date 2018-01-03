namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceWiFiSetHostNameModel
    {
        #region Properties

        public DeviceDatasheetEnum DeviceDatasheetId
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