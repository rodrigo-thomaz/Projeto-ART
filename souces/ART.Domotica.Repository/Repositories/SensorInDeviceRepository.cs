namespace ART.Domotica.Repository.Repositories
{
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class SensorInDeviceRepository : RepositoryBase<ARTDbContext, SensorInDevice>, ISensorInDeviceRepository
    {
        #region Constructors

        public SensorInDeviceRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}