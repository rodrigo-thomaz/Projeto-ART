namespace ART.Domotica.Model
{
    using System;

    public class DeviceNTPSetTimeZoneCompletedModel
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