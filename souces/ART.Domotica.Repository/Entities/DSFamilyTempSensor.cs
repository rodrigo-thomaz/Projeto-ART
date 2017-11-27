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

        public TempSensorAlarm HighAlarm
        {
            get; set;
        }

        public decimal HighChartLimiterCelsius
        {
            get; set;
        }

        public TempSensorAlarm LowAlarm
        {
            get; set;
        }

        public decimal LowChartLimiterCelsius
        {
            get; set;
        }

        public TempSensorRange TempSensorRange
        {
            get; set;
        }

        public byte TempSensorRangeId
        {
            get; set;
        }        

        #endregion Properties
    }
}