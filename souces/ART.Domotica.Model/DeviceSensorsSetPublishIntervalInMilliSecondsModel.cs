namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceSensorsSetPublishIntervalInMilliSecondsModel
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

        public int PublishIntervalInMilliSeconds
        {
            get; set;
        }

        #endregion Properties
    }
}