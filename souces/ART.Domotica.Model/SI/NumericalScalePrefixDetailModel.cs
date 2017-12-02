namespace ART.Domotica.Model.SI
{
    using ART.Domotica.Enums.SI;

    public class NumericalScalePrefixDetailModel
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

        public string Symbol
        {
            get; set;
        }

        #endregion Properties
    }
}