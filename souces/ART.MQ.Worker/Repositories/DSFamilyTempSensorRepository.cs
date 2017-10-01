using ART.MQ.Worker.Entities;
using ART.MQ.Worker.IRepositories;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.MQ.Worker.Repositories
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
