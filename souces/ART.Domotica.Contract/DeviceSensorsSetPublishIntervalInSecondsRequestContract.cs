namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceSensorsSetPublishIntervalInSecondsRequestContract
    {
        #region Properties

        public DeviceDatasheetEnum DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceSensorsId
        {
            get; set;
        }

        public int PublishIntervalInSeconds
        {
            get; set;
        }

        #endregion Properties
    }
}