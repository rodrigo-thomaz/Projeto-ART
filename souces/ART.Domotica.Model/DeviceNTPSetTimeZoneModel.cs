namespace ART.Domotica.Model
{
    using System;

    public class DeviceNTPSetTimeZoneModel
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

        #endregion Properties
    }
}