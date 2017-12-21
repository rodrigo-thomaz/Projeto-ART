namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceNTPSetTimeZoneModel
    {
        #region Properties

        public DeviceDatasheetEnum DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceNTPId
        {
            get; set;
        }

        public byte TimeZoneId
        {
            get; set;
        }

        #endregion Properties
    }
}