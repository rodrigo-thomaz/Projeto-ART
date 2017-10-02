using ART.Data.Repository.Entities;
using ART.Data.Repository.Interfaces;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Data.Repository.Repositories
{
    public class DSFamilyTempSensorRepository : RepositoryBase<DSFamilyTempSensor, Guid>, IDSFamilyTempSensorRepository
    {
        public DSFamilyTempSensorRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<DSFamilyTempSensor> GetByDeviceAddress(string deviceAddress)
        {
            return await _context.DSFamilyTempSensor.SingleOrDefaultAsync(x => x.DeviceAddress == deviceAddress);
        }
    }
}
