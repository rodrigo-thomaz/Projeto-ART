namespace ART.Domotica.Contract
{
    using System;

    public class ESPDeviceSetTimeZoneRequestContract
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