namespace ART.Domotica.Repository.Entities.SI
{
    using System.Collections.Generic;

    using ART.Domotica.Enums.SI;
    using ART.Infra.CrossCutting.Repository;

    public class UnitMeasurementScale : IEntity
    {
        #region Properties

        public string Name
        {
            get; set;
        }

        public NumericalScale NumericalScale
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

        public ICollection<SensorDatasheetUnitMeasurementDefault> SensorDatasheetUnitMeasurementDefaults
        {
            get; set;
        }

        public ICollection<SensorDatasheetUnitMeasurementScale> SensorDatasheetUnitMeasurementScales
        {
            get; set;
        }

        public UnitMeasurement UnitMeasurement
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