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

        public decimal HighChartLimiterCelsius
        {
            get; set;
        }

        public decimal LowChartLimiterCelsius
        {
            get; set;
        }        

        #endregion Properties
    }
}