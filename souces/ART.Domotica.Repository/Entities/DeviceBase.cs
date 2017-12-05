namespace ART.Domotica.Repository.Entities
{
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