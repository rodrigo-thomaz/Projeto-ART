using ART.MQ.Consumer.Entities;
using ART.MQ.Consumer.IRepositories;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.MQ.Consumer.Repositories
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
