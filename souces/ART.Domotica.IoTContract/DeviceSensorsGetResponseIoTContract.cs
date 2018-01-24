using System.Collections.Generic;

namespace ART.Domotica.IoTContract
{
    public class DeviceSensorsGetResponseIoTContract
    {
        #region Properties

        public List<SensorInDeviceGetResponseIoTContract> SensorInDevice
        {
            get; set;
        }

        public int PublishIntervalInMilliSeconds
        {
            get; set;
        }

        #endregion Properties
    }
}