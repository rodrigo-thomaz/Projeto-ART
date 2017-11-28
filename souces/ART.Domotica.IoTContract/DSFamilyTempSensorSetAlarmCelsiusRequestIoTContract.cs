namespace ART.Domotica.IoTContract
{
    using System;

    using ART.Domotica.Enums;

    public class DSFamilyTempSensorSetAlarmCelsiusRequestIoTContract
    {
        #region Properties

        public decimal AlarmCelsius
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