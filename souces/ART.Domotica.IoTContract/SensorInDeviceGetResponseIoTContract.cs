namespace ART.Domotica.IoTContract
{
    public class SensorInDeviceGetResponseIoTContract
    {
        #region Properties

        public short Ordination
        {
            get; set;
        }

        public SensorGetResponseIoTContract Sensor
        {
            get; set;
        }

        #endregion Properties
    }
}