namespace ART.Domotica.Model
{
    using ART.Domotica.Enums;
    using System;

    public class DeviceSensorsSetPublishIntervalInSecondsModel
    {
        #region Properties

        public Guid DeviceSensorsId
        {
            get; set;
        }

        public DeviceDatasheetEnum DeviceDatasheetId
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