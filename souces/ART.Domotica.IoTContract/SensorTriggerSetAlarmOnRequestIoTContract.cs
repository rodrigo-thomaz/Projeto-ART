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

        public SensorUnitMeasurementScalePositionEnum Position
        {
            get; set;
        }

        #endregion Properties
    }
}