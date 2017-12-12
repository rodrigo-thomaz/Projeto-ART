namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Domotica.Enums;
    using ART.Domotica.Enums.SI;
    using ART.Domotica.Repository.Entities.SI;
    using ART.Infra.CrossCutting.Repository;

    public class SensorUnitMeasurementScale : IEntity<Guid>
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

        public Guid Id
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

        public Sensor Sensor
        {
            get; set;
        }

        public SensorDatasheetEnum SensorDatasheetId
        {
            get; set;
        }

        public SensorDatasheetUnitMeasurementScale SensorDatasheetUnitMeasurementScale
        {
            get; set;
        }

        public SensorTypeEnum SensorTypeId
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