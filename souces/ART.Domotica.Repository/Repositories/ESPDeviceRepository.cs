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

        public async Task<List<HardwareInApplication>> GetListInApplication(Guid applicationUserId)
        {
            IQueryable<HardwareInApplication> query = from hia in _context.HardwareInApplication
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

        public async Task<HardwareInApplication> GetInApplicationById(Guid hardwareInApplicationId)
        {
            var entity = await _context.HardwareInApplication.FindAsync(hardwareInApplicationId);
            return entity;
        }

        public async Task InsertInApplication(HardwareInApplication entity)
        {
            _context.HardwareInApplication.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFromApplication(HardwareInApplication entity)
        {
            _context.HardwareInApplication.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<string>> GetExistingPins()
        {
            var entity = await _context.HardwareInApplication.FirstOrDefaultAsync();

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

        public async Task<ESPDeviceBase> GetDeviceInApplication(int chipId, int flashChipId, string macAddress)
        {
            var data = await _context.Set<ESPDeviceBase>()
               .Include(x => x.HardwaresInApplication)
               .Where(x => x.ChipId == chipId)
               .Where(x => x.FlashChipId == flashChipId)
               .Where(x => x.MacAddress == macAddress)               
               .SingleOrDefaultAsync();

            return data;
        }

        #endregion Methods
    }
}