namespace ART.Domotica.IoTContract
{
    using System;

    using ART.Domotica.Enums;

    public class SensorTriggerSetAlarmOnRequestIoTContract
    {
        #region Properties

        public bool AlarmOn
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