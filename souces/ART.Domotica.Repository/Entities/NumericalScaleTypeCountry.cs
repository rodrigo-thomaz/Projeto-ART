using ART.Domotica.Enums;

namespace ART.Domotica.Repository.Entities
{
    public class NumericalScaleTypeCountry
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

        public NumericalScaleTypeEnum NumericalScaleTypeId
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