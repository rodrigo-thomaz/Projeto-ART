namespace ART.Domotica.Repository.Entities
{
    using ART.Domotica.Enums;

    public class UnitMeasurementScale
    {
        #region Properties

        public short Base
        {
            get; set;
        }

        public short Exponent
        {
            get; set;
        }

        public string Name
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

        public NumericalScalePrefix NumericalScalePrefix
        {
            get; set;
        }

        public NumericalScalePrefixEnum NumericalScalePrefixId
        {
            get; set;
        }

        #endregion Properties
    }
}