namespace ART.Domotica.Repository.Entities
{
    public class DSFamilyTempSensor : SensorBase
    {
        #region Properties

        public string DeviceAddress
        {
            get; set;
        }

        public DSFamilyTempSensorResolution DSFamilyTempSensorResolution
        {
            get; set;
        }

        public byte DSFamilyTempSensorResolutionId
        {
            get; set;
        }

        public string Family
        {
            get; set;
        }

        public bool HasAlarm
        {
            get; set;
        }

        public decimal HighAlarm
        {
            get; set;
        }

        public decimal LowAlarm
        {
            get; set;
        }

        public TemperatureScale TemperatureScale
        {
            get; set;
        }

        public byte TemperatureScaleId
        {
            get; set;
        }

        #endregion Properties
    }
}