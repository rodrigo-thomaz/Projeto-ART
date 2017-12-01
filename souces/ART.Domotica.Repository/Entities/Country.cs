namespace ART.Domotica.Repository.Entities
{
    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class Country : IEntity<short>
    {
        #region Properties

        public short Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public NumericalScale NumericalScale
        {
            get; set;
        }

        public NumericalScaleEnum NumericalScaleId
        {
            get; set;
        }

        public ContinentEnum ContinentId
        {
            get; set;
        }

        public Continent Continent
        {
            get; set;
        }

        #endregion Properties
    }
}