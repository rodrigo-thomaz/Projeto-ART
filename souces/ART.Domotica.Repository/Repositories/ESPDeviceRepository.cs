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

    public class ESPDeviceRepository : RepositoryBase<ARTDbContext, ESPDeviceBase, Guid>, IESPDeviceRepository
    {
        #region Constructors

        public ESPDeviceRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public async Task<List<HardwaresInApplication>> GetListInApplication(Guid applicationUserId)
        {
            IQueryable<HardwaresInApplication> query = from hia in _context.HardwaresInApplication
                                                   join au in _context.ApplicationUser on hia.ApplicationId equals au.ApplicationId
                                                   where au.Id == applicationUserId
                                                   select hia;

            var data = await query.ToListAsync();

            var ids = data.Select(x => x.HardwareBaseId);

            await _context.Set<HardwareBase>()
                .Where(x => ids.Contains(x.Id))
                .LoadAsync();

            return data;
        }        

        public async Task<HardwareBase> GetByPin(string pin)
        {
            var data = await _context.ThermometerDevice
                .Where(x => x.Pin == pin)
                .SingleOrDefaultAsync();

            return data;
        }

        public async Task<HardwaresInApplication> GetInApplicationById(Guid hardwaresInApplicationId)
        {
            var entity = await _context.HardwaresInApplication.FindAsync(hardwaresInApplicationId);
            return entity;
        }

        public async Task InsertInApplication(HardwaresInApplication entity)
        {
            _context.HardwaresInApplication.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFromApplication(HardwaresInApplication entity)
        {
            _context.HardwaresInApplication.Remove(entity);
            await _context.SaveChangesAsync();
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

        public async Task<List<ESPDeviceBase>> GetESPDevicesNotInApplication()
        {
            var data = await _context.Set<ESPDeviceBase>()
                .Where(x => !x.HardwaresInApplication.Any())
                .ToListAsync();
            return data;
        }

        #endregion Methods
    }
}