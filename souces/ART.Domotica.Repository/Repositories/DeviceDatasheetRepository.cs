using ART.Domotica.Enums;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using ART.Infra.CrossCutting.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories
{
    public class DeviceDatasheetRepository : RepositoryBase<ARTDbContext, DeviceDatasheet, Guid>, IDeviceDatasheetRepository
    {
        public DeviceDatasheetRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<List<DeviceDatasheet>> GetAll()
        {
            return await _context.DeviceDatasheet
                .ToListAsync();
        }
    }
}
