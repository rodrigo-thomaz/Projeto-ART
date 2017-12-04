namespace ART.Domotica.IoTContract
{
    using System;

    using ART.Domotica.Enums;

    public class SensorTriggerSetAlarmBuzzerOnRequestIoTContract
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