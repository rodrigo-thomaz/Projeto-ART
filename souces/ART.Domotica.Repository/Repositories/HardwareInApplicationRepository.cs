namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class HardwareInApplicationRepository : RepositoryBase<ARTDbContext, HardwareInApplication>, IHardwareInApplicationRepository
    {
        #region Constructors

        public HardwareInApplicationRepository(ARTDbContext context)
            : base(context)
        {
        }

        public async Task<HardwareInApplication> GetByKey(Guid applicationId, Guid deviceId)
        {
            return await _context.HardwareInApplication.FindAsync(applicationId, deviceId);
        }

        #endregion Constructors
    }
}