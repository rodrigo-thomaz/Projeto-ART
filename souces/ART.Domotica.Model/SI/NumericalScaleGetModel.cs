namespace ART.Domotica.Model.SI
{
    using ART.Domotica.Enums.SI;

    public class NumericalScaleGetModel
    {
        #region Properties

        public string Name
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

        public decimal ScientificNotationBase
        {
            get; set;
        }

        public decimal ScientificNotationExponent
        {
            get; set;
        }

        #endregion Properties
    }
}