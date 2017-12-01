using ART.Domotica.Enums;

namespace ART.Domotica.Repository.Entities
{
    public class NumericalScaleCountry
    {
        #region Properties

        public Country Country
        {
            get; set;
        }

        public short CountryId
        {
            get; set;
        }

        public NumericalScaleEnum NumericalScaleId
        {
            get; set;
        }

        public NumericalScale NumericalScale
        {
            get; set;
        }

        #endregion Properties
    }
}