namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;

    public class HardwareRepository : RepositoryBase<ARTDbContext, HardwareBase, Guid>, IHardwareRepository
    {
        #region Constructors

        public HardwareRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<List<string>> GetExistingPins()
        {
            var entity = await _context.HardwaresInApplication.FirstOrDefaultAsync();

            var data = await _context.Set<HardwareBase>()
                .Where(x => x.HardwaresInApplication.Any())
                .Select(x => x.Pin)
                .ToListAsync();
            return data;
        }

        public async Task<List<HardwareBase>> GetHardwaresNotInApplication()
        {
            var data = await _context.Set<HardwareBase>()
                .Where(x => !x.HardwaresInApplication.Any())
                .ToListAsync();
            return data;
        }
    }
}