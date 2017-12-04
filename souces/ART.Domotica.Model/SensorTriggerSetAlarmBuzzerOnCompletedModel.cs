namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class SensorTriggerSetAlarmBuzzerOnCompletedModel
    {
        #region Properties

        public bool AlarmBuzzerOn
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

        public SensorUnitMeasurementScalePositionEnum Position
        {
            get; set;
        }

        #endregion Properties
    }
}