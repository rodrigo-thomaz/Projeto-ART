using ART.Domotica.Enums;

namespace ART.Domotica.Repository.Entities
{
    public class UnitMeasurementScale 
    {
        #region Properties
        
        public UnitMeasurementEnum UnitMeasurementId
        {
            get; set;
        }

        public UnitMeasurementTypeEnum UnitMeasurementTypeId
        {
            get; set;
        }

        public UnitMeasurement UnitMeasurement
        {
            get; set;
        }

        public UnitMeasurementType UnitMeasurementType
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

        public NumericalScalePrefix NumericalScalePrefix
        {
            get; set;
        }

        public NumericalScale NumericalScale
        {
            get; set;
        }

        public NumericalScaleType NumericalScaleType
        {
            get; set;
        }

        #endregion Properties
    }
}