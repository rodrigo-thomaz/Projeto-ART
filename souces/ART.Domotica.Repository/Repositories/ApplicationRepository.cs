namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Data.Entity;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Threading.Tasks;
    using System.Linq;

    public class ApplicationRepository : RepositoryBase<ARTDbContext, Application, Guid>, IApplicationRepository
    {
        #region Constructors

        public ApplicationRepository(ARTDbContext context)
            : base(context)
        {
        }

        public async Task<Application> GetFullByKey(Guid applicationId)
        {
            return await _context.Application
                .Include(x => x.ApplicationMQ)                
                .Where(x => x.Id == applicationId)
                .FirstOrDefaultAsync();
        }

        #endregion Constructors
    }
}