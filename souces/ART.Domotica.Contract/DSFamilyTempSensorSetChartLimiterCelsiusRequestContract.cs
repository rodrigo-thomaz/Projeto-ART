namespace ART.Domotica.Contract
{
    using System;

    public class DSFamilyTempSensorSetChartLimiterCelsiusRequestContract
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

        public TempSensorAlarmPositionContract Position
        {
            get; set;
        }

        #endregion Properties
    }
}