namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    public abstract class DeviceBase : HardwareBase
    {
        #region Properties

        public DeviceMQ DeviceMQ
        {
            get; set;
        }

        public DeviceNTP DeviceNTP
        {
            get; set;
        }

        public DeviceSensors DeviceSensors
        {
            get; set;
        }

        #endregion Properties
    }
}