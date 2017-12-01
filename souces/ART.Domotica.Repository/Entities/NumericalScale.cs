namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    using ART.Domotica.Enums;

    public class NumericalScale
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

        public ICollection<UnitMeasurementScale> UnitMeasurementScales
        {
            get; set;
        }

        #endregion Properties
    }
}