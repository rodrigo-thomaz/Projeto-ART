using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using ART.Infra.CrossCutting.Repository;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace ART.Domotica.Repository.Repositories
{
    public class DSFamilyTempSensorRepository : RepositoryBase<ARTDbContext, DSFamilyTempSensor, Guid>, IDSFamilyTempSensorRepository
    {
        public DSFamilyTempSensorRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<SensorsInDevice> GetDeviceFromSensor(Guid dsFamilyTempSensorId)
        {
            var entity = await _context.SensorsInDevice
                .SingleOrDefaultAsync(x => x.SensorBaseId == dsFamilyTempSensorId);

            return entity;
        }

        public async Task<List<DSFamilyTempSensor>> GetAllByDeviceId(Guid deviceId)
        {
            return await _context.DSFamilyTempSensor
                .Include(x => x.DSFamilyTempSensorResolution)
                .Include(x => x.SensorChartLimiter)
                .Where(x => x.SensorsInDevice.FirstOrDefault(y => y.DeviceBaseId == deviceId) != null)
                .ToListAsync();
        }
    }
}
