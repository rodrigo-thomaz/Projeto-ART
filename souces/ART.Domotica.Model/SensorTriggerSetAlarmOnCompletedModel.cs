namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class SensorTriggerSetAlarmOnCompletedModel
    {
        #region Properties

        public bool AlarmOn
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