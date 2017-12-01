namespace ART.Domotica.Repository.Entities
{
    using ART.Infra.CrossCutting.Repository;

    public class UnitMeasurementPrefix : IEntity<int>
    {
        #region Properties

        public int Id
        {
            get; set;
        }

        #endregion Properties
    }
}