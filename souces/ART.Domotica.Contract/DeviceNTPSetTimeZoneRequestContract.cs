namespace ART.Domotica.Contract
{
    using System;

    public class DeviceNTPSetTimeZoneRequestContract
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