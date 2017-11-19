namespace ART.Domotica.IoTContract
{
    using System;

    public class DSFamilyTempSensorSetChartLimiterCelsiusRequestIoTContract
    {
        #region Properties

        public decimal ChartLimiterCelsius
        {
            get; set;
        }

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public TempSensorAlarmPositionIoTContract Position
        {
            get; set;
        }

        #endregion Properties
    }
}