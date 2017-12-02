namespace ART.Domotica.Repository.Entities.SI
{
    using System.Collections.Generic;

    using ART.Domotica.Enums.SI;

    public class NumericalScale
    {
        #region Properties

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

        public decimal ScientificNotationBase
        {
            get; set;
        }

        public decimal ScientificNotationExponent
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