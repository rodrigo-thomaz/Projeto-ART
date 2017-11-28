namespace ART.Domotica.Repository.Entities
{
    using ART.Domotica.Enums;

    public class DSFamilyTempSensor : TemperatureSensorBase
    {
        #region Constructors

        public DSFamilyTempSensor()
        {
            UnitOfMeasurementTypeId = UnitOfMeasurementTypeEnum.Temperature;
        }

        #endregion Constructors

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