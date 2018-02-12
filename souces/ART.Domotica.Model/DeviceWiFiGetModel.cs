namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceWiFiGetModel
    {
        #region Properties

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceId
        {
            get; set;
        }

        public DeviceTypeEnum DeviceTypeId
        {
            get; set;
        }

        public string HostName
        {
            get; set;
        }

        public long PublishIntervalInMilliSeconds
        {
            get; set;
        }

        #endregion Properties
    }
}