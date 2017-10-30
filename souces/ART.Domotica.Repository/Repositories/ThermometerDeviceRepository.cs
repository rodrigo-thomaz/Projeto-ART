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

    public class ThermometerDeviceRepository : RepositoryBase<ARTDbContext, ThermometerDevice, Guid>, IThermometerDeviceRepository
    {
        #region Constructors

        public ThermometerDeviceRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public async Task<List<ThermometerDevice>> GetList()
        {
            var data = await _context.ThermometerDevice
                .ToListAsync();
            return data;
        }

        public async Task<List<string>> GetExistingPins()
        {
            var entity = await _context.HardwaresInApplication.FirstOrDefaultAsync();

            var data = await _context.ThermometerDevice
                .Where(x => x.HardwaresInApplication.Any())
                .Select(x => x.Pin)
                .ToListAsync();
            return data;
        }

        public async Task<List<ThermometerDevice>> GetThermometerDeviceNotInApplication()
        {
            var data = await _context.ThermometerDevice
                .Where(x => !x.HardwaresInApplication.Any())
                .ToListAsync();
            return data;
        }

        #endregion Methods
    }
}