namespace ART.Domotica.Model
{
    using System;

    public class DeviceNTPGetModel
    {
        #region Properties

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