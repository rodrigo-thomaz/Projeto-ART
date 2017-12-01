namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    using ART.Domotica.Enums;
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