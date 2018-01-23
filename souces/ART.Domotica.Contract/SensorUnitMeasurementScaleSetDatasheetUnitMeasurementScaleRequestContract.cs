namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;
    using ART.Domotica.Enums.SI;

    public class SensorUnitMeasurementScaleSetDatasheetUnitMeasurementScaleRequestContract
    {
        #region Properties

        public NumericalScalePrefixEnum NumericalScalePrefixId
        {
            get; set;
        }

        public NumericalScaleTypeEnum NumericalScaleTypeId
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

        public UnitMeasurementEnum UnitMeasurementId
        {
            get; set;
        }

        public UnitMeasurementTypeEnum UnitMeasurementTypeId
        {
            get; set;
        }

        #endregion Properties
    }
}