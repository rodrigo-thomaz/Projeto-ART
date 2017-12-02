namespace ART.Domotica.Repository.Entities.Locale
{
    using System.Collections.Generic;

    using ART.Domotica.Enums.Locale;
    using ART.Infra.CrossCutting.Repository;

    public class Continent : IEntity<ContinentEnum>
    {
        #region Properties

        public ICollection<Country> Countries
        {
            get; set;
        }

        public ContinentEnum Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        #endregion Properties
    }
}