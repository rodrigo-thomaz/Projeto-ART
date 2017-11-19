namespace ART.Domotica.Model
{
    using System;

    public class DSFamilyTempSensorSetChartLimiterCelsiusCompletedModel
    {
        #region Properties

        public decimal ChartLimiterCelsius
        {
            get; set;
        }

        public Guid DeviceId
        {
            get; set;
        }

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public TempSensorAlarmPositionModel Position
        {
            get; set;
        }

        #endregion Properties
    }
}