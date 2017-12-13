namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;

    public class SensorUnitMeasurementScaleSetValueRequestContract
    {
        #region Properties

        public SensorUnitMeasurementScalePositionEnum Position
        {
            get; set;
        }

        public SensorDatasheetEnum SensorDatasheetId
        {
            get; set;
        }

        public SensorTypeEnum SensorTypeId
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