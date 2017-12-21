namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceNTPSetTimeZoneRequestContract
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