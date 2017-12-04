namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class SensorUnitMeasurementScaleSetValueModel
    {
        #region Properties

        public Guid DeviceId
        {
            get; set;
        }

        public SensorUnitMeasurementScalePositionEnum Position
        {
            get; set;
        }

        public Guid SensorUnitMeasurementScaleId
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