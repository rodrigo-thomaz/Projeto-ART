namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class Country : IEntity<short>
    {
        #region Properties

        public Continent Continent
        {
            get; set;
        }

        public ContinentEnum ContinentId
        {
            get; set;
        }

        public short Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public ICollection<NumericalScaleTypeCountry> NumericalScaleTypesCountry
        {
            get; set;
        }

        #endregion Properties
    }
}