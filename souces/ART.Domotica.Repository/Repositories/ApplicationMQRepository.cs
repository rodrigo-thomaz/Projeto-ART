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

        public async Task<ApplicationMQ> GetByHardwareId(Guid hardwareId)
        {
            IQueryable<ApplicationMQ> query = from abs in _context.ApplicationMQ
                                              join dia in _context.HardwareInApplication on abs.Id equals dia.ApplicationId
                                              where dia.HardwareId == hardwareId
                                              select abs;

            return await query.SingleOrDefaultAsync();
        }
    }
}