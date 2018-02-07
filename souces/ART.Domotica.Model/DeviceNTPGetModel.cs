namespace ART.Domotica.Model
{
    using System;

    public class DeviceNTPGetModel
    {
        #region Properties

        public Guid DeviceDatasheetId
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

        public long UpdateIntervalInMilliSecond
        {
            get; set;
        }

        #endregion Properties
    }
}