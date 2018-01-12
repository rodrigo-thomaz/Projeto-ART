namespace ART.Domotica.IoTContract
{
    using ART.Domotica.Enums.SI;

    public class SensorUnitMeasurementScaleGetResponseIoTContract
    {
        #region Properties

        public decimal HighChartLimiterCelsius
        {
            get; set;
        }

        public decimal LowChartLimiterCelsius
        {
            get; set;
        }

        public UnitMeasurementEnum UnitMeasurementId
        {
            get; set;
        }

        #endregion Properties
    }
}