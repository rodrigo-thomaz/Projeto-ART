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

        public async Task<List<ESPDeviceBase>> GetAll()
        {
            return await _context.ESPDeviceBase
                .Include(x => x.HardwaresInApplication)
                .ToListAsync();
        }

        public async Task<ESPDeviceBase> GetByPin(string pin)
        {
            var data = await _context.ESPDeviceBase
                .Where(x => x.Pin == pin)
                .SingleOrDefaultAsync();
            return data;
        }

        public async Task<List<string>> GetExistingPins()
        {
            var data = await _context.ESPDeviceBase
                .Where(x => x.HardwaresInApplication.Any())
                .Select(x => x.Pin)
                .ToListAsync();
            return data;
        }

        public async Task<List<ESPDeviceBase>> GetListNotInApplication()
        {
            var data = await _context.ESPDeviceBase
                .Where(x => !x.HardwaresInApplication.Any())
                .ToListAsync();

            return data;
        }

        public async Task<ESPDeviceBase> GetDeviceInApplication(int chipId, int flashChipId, string macAddress)
        {
            var data = await _context.ESPDeviceBase
               .Include(x => x.HardwaresInApplication)
               .Where(x => x.ChipId == chipId)
               .Where(x => x.FlashChipId == flashChipId)
               .Where(x => x.MacAddress == macAddress)               
               .SingleOrDefaultAsync();

            return data;
        }

        public async Task<List<ESPDeviceBase>> GetListInApplication(Guid applicationUserId)
        {
            IQueryable<ESPDeviceBase> query = from hia in _context.HardwareInApplication
                                                      join esp in _context.ESPDeviceBase on hia.HardwareBaseId equals esp.Id
                                                      join au in _context.ApplicationUser on hia.ApplicationId equals au.ApplicationId
                                                      where au.Id == applicationUserId
                                                      select esp;

            var data = await query.ToListAsync();

            var ids = data.Select(x => x.Id);

            await _context.HardwareInApplication
                .Where(x => ids.Contains(x.HardwareBaseId))
                .LoadAsync();

            return data;
        }

        #endregion Methods
    }
}