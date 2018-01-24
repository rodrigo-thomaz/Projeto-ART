namespace ART.Domotica.IoTContract
{
    using System.Collections.Generic;

    public class DeviceSensorsGetResponseIoTContract
    {
        #region Properties

        public int PublishIntervalInMilliSeconds
        {
            get; set;
        }

        public List<SensorInDeviceGetResponseIoTContract> SensorInDevice
        {
            get; set;
        }

        #endregion Properties
    }
}