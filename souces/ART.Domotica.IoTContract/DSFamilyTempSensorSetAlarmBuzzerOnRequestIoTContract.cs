namespace ART.Domotica.IoTContract
{
    using System;

    using ART.Domotica.Enums;

    public class DSFamilyTempSensorSetAlarmBuzzerOnRequestIoTContract
    {
        #region Properties

        public bool AlarmBuzzerOn
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