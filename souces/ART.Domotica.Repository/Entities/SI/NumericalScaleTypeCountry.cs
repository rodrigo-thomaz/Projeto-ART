namespace ART.Domotica.Repository.Entities.SI
{
    using ART.Domotica.Enums.SI;
    using ART.Domotica.Repository.Entities.Locale;

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

        public NumericalScaleType NumericalScaleType
        {
            get; set;
        }

        public NumericalScaleTypeEnum NumericalScaleTypeId
        {
            get; set;
        }

        #endregion Properties
    }
}