namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceSensorsSetReadIntervalInMilliSecondsModel
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

        public int ReadIntervalInMilliSeconds
        {
            get; set;
        }

        #endregion Properties
    }
}