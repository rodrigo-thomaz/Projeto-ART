namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;

    public class SensorTriggerSetAlarmOnRequestContract
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

        public SensorUnitMeasurementScalePositionEnum Position
        {
            get; set;
        }

        #endregion Properties
    }
}