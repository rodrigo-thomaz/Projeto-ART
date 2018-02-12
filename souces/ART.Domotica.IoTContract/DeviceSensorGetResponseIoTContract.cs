namespace ART.Domotica.IoTContract
{
    using System.Collections.Generic;

    public class DeviceSensorGetResponseIoTContract
    {
        #region Properties

        public long PublishIntervalInMilliSeconds
        {
            get; set;
        }

        public long ReadIntervalInMilliSeconds
        {
            get; set;
        }

        public List<SensorDatasheetGetResponseIoTContract> SensorDatasheets
        {
            get; set;
        }

        public List<SensorInDeviceGetResponseIoTContract> SensorsInDevice
        {
            get; set;
        }

        #endregion Properties
    }
}