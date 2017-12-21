namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceNTPGetModel
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

        public int UpdateIntervalInMilliSecond
        {
            get; set;
        }

        #endregion Properties
    }
}