namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Data.Entity;

    public class HardwareRepository : RepositoryBase<ARTDbContext, HardwareBase, Guid>, IHardwareRepository
    {
        #region Constructors

        public HardwareRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public async Task<List<HardwareBase>> GetList()
        {
            var data = await _context.Set<HardwareBase>()
                .Include(x => x.HardwaresInApplication)
                .ToListAsync();            
            return data;
        }

        #endregion Methods
    }
}