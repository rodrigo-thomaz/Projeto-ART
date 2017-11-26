namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Data.Entity;

    public class ApplicationMQRepository : RepositoryBase<ARTDbContext, ApplicationMQ, Guid>, IApplicationMQRepository
    {
        #region Constructors

        public ApplicationMQRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<ApplicationMQ> GetByDeviceId(Guid deviceId)
        {
            IQueryable<ApplicationMQ> query = from abs in _context.ApplicationMQ
                                              join dia in _context.DeviceInApplication on abs.Id equals dia.ApplicationId
                                              where dia.DeviceBaseId == deviceId
                                              select abs;

            return await query.SingleOrDefaultAsync();
        }
    }
}