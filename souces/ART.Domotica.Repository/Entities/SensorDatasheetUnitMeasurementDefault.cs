namespace ART.Domotica.Repository.Entities
{
    using ART.Domotica.Enums;
    using ART.Domotica.Enums.SI;
    using ART.Domotica.Repository.Entities.SI;
    using ART.Infra.CrossCutting.Repository;

    public class SensorDatasheetUnitMeasurementDefault : IEntity<SensorDatasheetEnum>
    {
        #region Properties

        public SensorDatasheetEnum Id
        {
            get; set;
        }

        public decimal Max
        {
            get; set;
        }

        public decimal Min
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

        public SensorDatasheet SensorDatasheet
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

        public UnitMeasurementScale UnitMeasurementScale
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