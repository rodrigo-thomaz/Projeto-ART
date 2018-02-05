namespace ART.Domotica.Model
{
    using System;

    public class DeviceWiFiGetModel
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

        public int PublishIntervalInMilliSeconds
        {
            get; set;
        }

        #endregion Properties
    }
}