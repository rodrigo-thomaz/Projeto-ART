using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using ART.Infra.CrossCutting.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories
{
    public class SensorsInDeviceRepository : RepositoryBase<ARTDbContext, SensorsInDevice>, ISensorsInDeviceRepository
    {
        #region Constructors

        public SensorsInDeviceRepository(ARTDbContext context)
              : base(context)
        {

        }

        #endregion Constructors

        public async Task<List<SensorsInDevice>> GetAllByApplicationId(Guid applicationId)
        {
            IQueryable<SensorsInDevice> query = from sid in _context.SensorsInDevice
                                                join dia in _context.DeviceInApplication on sid.DeviceSensorsId equals dia.HardwareId
                                                where dia.ApplicationId == applicationId
                                                select sid;

            return await query.ToListAsync();
        }
    }
}
