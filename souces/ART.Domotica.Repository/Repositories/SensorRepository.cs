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
    public class SensorRepository : RepositoryBase<ARTDbContext, Sensor, Guid>, ISensorRepository
    {
        public SensorRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<List<Sensor>> GetAllByApplicationId(Guid applicationId)
        {
            return await _context.Sensor
                .Include(x => x.SensorUnitMeasurementScale)
                .Include(x => x.SensorTriggers)
                .Include(x => x.SensorTempDSFamily)
                //.Where(x => x.DevicesInApplication.All(y => y.ApplicationId == applicationId))
                .ToListAsync();
        }

        public async Task<SensorInDevice> GetDeviceFromSensor(Guid sensorId)
        {
            var entity = await _context.SensorInDevice
                .SingleOrDefaultAsync(x => x.SensorId == sensorId);

            return entity;
        }

        public async Task<List<Sensor>> GetAllByDeviceId(Guid deviceId)
        {
            return await _context.Sensor
                .Include(x => x.SensorTempDSFamily.SensorTempDSFamilyResolution)
                .Include(x => x.SensorUnitMeasurementScale)
                .Where(x => x.SensorInDevice.FirstOrDefault(y => y.DeviceSensors.Id == deviceId) != null)
                .ToListAsync();
        }
    }
}
