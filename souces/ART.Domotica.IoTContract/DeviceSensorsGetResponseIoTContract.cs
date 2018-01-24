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

        public List<SensorInDeviceGetResponseIoTContract> SensorsInDevice
        {
            get; set;
        }

        public List<SensorDatasheetGetResponseIoTContract> SensorDatasheets
        {
            get; set;
        }

        #endregion Properties
    }
}