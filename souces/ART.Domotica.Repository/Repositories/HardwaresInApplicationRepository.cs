namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class HardwaresInApplicationRepository : RepositoryBase<ARTDbContext, HardwaresInApplication, Guid>, IHardwaresInApplicationRepository
    {
        #region Constructors

        public HardwaresInApplicationRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public async Task<List<HardwaresInApplication>> GetList(Guid applicationUserId)
        {
            IQueryable<HardwaresInApplication> query = from hia in _context.HardwaresInApplication
                                                   join au in _context.ApplicationUser on hia.ApplicationId equals au.ApplicationId
                                                   where au.Id == applicationUserId
                                                   select hia;

            var data = await query.ToListAsync();
            return data;
        }

        #endregion Methods
    }
}