namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;
    using ART.Domotica.Enums.SI;

    public class SensorUnitMeasurementScaleGetModel
    {
        #region Properties

        public decimal ChartLimiterMax
        {
            get; set;
        }

        public decimal ChartLimiterMin
        {
            get; set;
        }

        public short CountryId
        {
            get; set;
        }

        public NumericalScalePrefixEnum NumericalScalePrefixId
        {
            get; set;
        }

        public NumericalScaleTypeEnum NumericalScaleTypeId
        {
            get; set;
        }

        public decimal RangeMax
        {
            get; set;
        }

        public decimal RangeMin
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