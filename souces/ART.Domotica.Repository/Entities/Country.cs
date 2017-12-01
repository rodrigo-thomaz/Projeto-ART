namespace ART.Domotica.Repository.Entities
{
    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;
    using System.Collections.Generic;

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

        public ICollection<NumericalScaleCountry> NumericalScalesCountry
        {
            get; set;
        }

        #endregion Properties
    }
}