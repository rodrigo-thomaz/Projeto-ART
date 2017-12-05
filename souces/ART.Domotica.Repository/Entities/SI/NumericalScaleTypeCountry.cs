namespace ART.Domotica.Repository.Entities.SI
{
    using ART.Domotica.Enums.SI;
    using ART.Domotica.Repository.Entities.Locale;
    using ART.Infra.CrossCutting.Repository;

    public class NumericalScaleTypeCountry : IEntity
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