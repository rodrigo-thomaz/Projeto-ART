namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class SensorInApplicationRepository : RepositoryBase<ARTDbContext, SensorInApplication>, ISensorInApplicationRepository
    {
        #region Constructors

        public SensorInApplicationRepository(ARTDbContext context)
            : base(context)
        {
        }

        public async Task<SensorInApplication> GetByKey(Guid applicationId, Guid sensorId)
        {
            return await _context.SensorInApplication.FindAsync(applicationId, sensorId);
        }

        #endregion Constructors
    }
}