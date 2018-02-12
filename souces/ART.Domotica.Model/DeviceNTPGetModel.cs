namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceNTPGetModel
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

        public byte TimeZoneId
        {
            get; set;
        }

        public long UpdateIntervalInMilliSecond
        {
            get; set;
        }

        #endregion Properties
    }
}