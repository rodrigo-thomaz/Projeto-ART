using ART.Domotica.Enums;
using ART.Infra.CrossCutting.Repository;
using System.Collections.Generic;

namespace ART.Domotica.Repository.Entities
{
    public class Continent : IEntity<ContinentEnum>
    {
        #region Properties

        public ContinentEnum Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public ICollection<Country> Countries
        {
            get; set;
        }

        #endregion Properties
    }
}