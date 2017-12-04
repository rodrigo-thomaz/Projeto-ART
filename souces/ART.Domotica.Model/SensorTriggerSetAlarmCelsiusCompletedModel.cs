namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class SensorTriggerSetAlarmCelsiusCompletedModel
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

        public SensorUnitMeasurementScalePositionEnum Position
        {
            get; set;
        }

        public Guid SensorTempDSFamilyId
        {
            get; set;
        }

        #endregion Properties
    }
}