using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories
{
    public class SensorsInDeviceRepository : ISensorsInDeviceRepository
    {
        private readonly ARTDbContext _context;

        #region Constructors

        public SensorsInDeviceRepository(ARTDbContext context)            
        {
            _context = context;
        }

        #endregion Constructors

        public async Task<List<SensorsInDevice>> GetAllByApplicationId(Guid applicationId)
        {
            IQueryable<SensorsInDevice> query = from sid in _context.SensorsInDevice
                                                join dia in _context.DeviceInApplication on sid.DeviceSensorsId equals dia.DeviceBaseId
                                                where dia.ApplicationId == applicationId
                                                select sid;

            return await query.ToListAsync();
        }
    }
}
