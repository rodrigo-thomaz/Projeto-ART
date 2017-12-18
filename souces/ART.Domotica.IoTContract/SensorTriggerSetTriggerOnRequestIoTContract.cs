namespace ART.Domotica.IoTContract
{
    using System;

    using ART.Domotica.Enums;

    public class SensorTriggerSetTriggerOnRequestIoTContract
    {
        #region Properties

        public bool AlarmOn
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