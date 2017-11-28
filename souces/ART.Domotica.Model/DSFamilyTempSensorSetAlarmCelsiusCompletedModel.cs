namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DSFamilyTempSensorSetAlarmCelsiusCompletedModel
    {
        #region Properties

        public decimal AlarmCelsius
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

        public SensorChartLimiterPositionEnum Position
        {
            get; set;
        }

        #endregion Properties
    }
}