namespace ART.Domotica.IoTContract
{
    using System;

    using ART.Domotica.Enums;

    public class SensorTriggerSetTriggerValueRequestIoTContract
    {
        #region Properties

        public decimal AlarmCelsius
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