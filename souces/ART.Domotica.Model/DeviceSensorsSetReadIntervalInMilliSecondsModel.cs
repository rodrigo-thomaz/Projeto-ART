namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceSensorsSetReadIntervalInMilliSecondsModel
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

        public long ReadIntervalInMilliSeconds
        {
            get; set;
        }

        #endregion Properties
    }
}