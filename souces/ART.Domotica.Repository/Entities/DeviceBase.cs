namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    public abstract class DeviceBase : HardwareBase
    {
        #region Properties

        public DeviceBrokerSetting DeviceBrokerSetting
        {
            get; set;
        }

        public ICollection<DeviceInApplication> DevicesInApplication
        {
            get; set;
        }

        public ICollection<SensorsInDevice> SensorsInDevice
        {
            get; set;
        }

        #endregion Properties
    }
}