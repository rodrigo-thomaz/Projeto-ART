namespace ART.Domotica.Model
{
    using System;

    public class DeviceNTPSetTimeZoneModel
    {
        #region Properties

        public Guid DeviceId
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