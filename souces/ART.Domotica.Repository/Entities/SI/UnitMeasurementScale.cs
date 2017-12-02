namespace ART.Domotica.Repository.Entities.SI
{
    using System.Collections.Generic;

    using ART.Domotica.Enums.SI;

    public class UnitMeasurementScale
    {
        #region Properties

        public NumericalScale NumericalScale
        {
            get; set;
        }

        public NumericalScalePrefix NumericalScalePrefix
        {
            get; set;
        }

        public NumericalScalePrefixEnum NumericalScalePrefixId
        {
            get; set;
        }

        public NumericalScaleType NumericalScaleType
        {
            get; set;
        }

        public NumericalScaleTypeEnum NumericalScaleTypeId
        {
            get; set;
        }

        public ICollection<SensorUnitMeasurementDefault> SensorUnitMeasurementDefaults
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

        public UnitMeasurementType UnitMeasurementType
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