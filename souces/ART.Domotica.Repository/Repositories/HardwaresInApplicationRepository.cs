namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class HardwaresInApplicationRepository : RepositoryBase<ARTDbContext, HardwaresInApplication, Guid>, IHardwaresInApplicationRepository
    {
        #region Constructors

        public HardwaresInApplicationRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public async Task<List<HardwaresInApplication>> GetList()
        {
            var data = await _context.HardwaresInApplication
                .ToListAsync();
            return data;
        }

        #endregion Methods
    }
}