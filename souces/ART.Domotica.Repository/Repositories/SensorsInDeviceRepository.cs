namespace ART.Domotica.Repository.Repositories
{
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class SensorsInDeviceRepository : RepositoryBase<ARTDbContext, SensorsInDevice>, ISensorsInDeviceRepository
    {
        #region Constructors

        public SensorsInDeviceRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}