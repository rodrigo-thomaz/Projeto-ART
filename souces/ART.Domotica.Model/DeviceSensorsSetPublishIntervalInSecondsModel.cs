namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceSensorsSetPublishIntervalInSecondsModel
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