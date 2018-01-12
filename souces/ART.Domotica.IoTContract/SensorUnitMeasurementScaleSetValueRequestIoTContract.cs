namespace ART.Domotica.IoTContract
{
    using System;

    using ART.Domotica.Enums;

    public class SensorUnitMeasurementScaleSetValueRequestIoTContract
    {
        #region Properties

        public SensorUnitMeasurementScalePositionEnum Position
        {
            get; set;
        }

        public Guid SensorId
        {
            get; set;
        }

        public decimal Value
        {
            get; set;
        }

        #endregion Properties
    }
}