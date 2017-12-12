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
                .Where(x => x.SensorInApplication.Any(y => y.ApplicationId == applicationId))
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

        public async Task<List<HardwareInApplication>> GetHardwareInApplicationByDeviceId(Guid applicationId, Guid deviceId)
        {
            var query = from s in _context.Sensor
                        join hia in _context.HardwareInApplication on s.Id equals hia.HardwareId
                        join sid in _context.SensorInDevice on s.Id equals sid.SensorId
                        where hia.ApplicationId == applicationId
                        where sid.DeviceSensorsId == deviceId
                        select hia;

            return await query.ToListAsync();
        }
    }
}
