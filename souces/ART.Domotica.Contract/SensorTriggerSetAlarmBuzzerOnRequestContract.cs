namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;

    public class SensorTriggerSetAlarmBuzzerOnRequestContract
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

        public SensorUnitMeasurementScalePositionEnum Position
        {
            get; set;
        }

        #endregion Properties
    }
}