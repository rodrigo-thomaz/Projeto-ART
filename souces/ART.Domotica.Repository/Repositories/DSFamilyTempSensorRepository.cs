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

        public async Task<List<DSFamilyTempSensor>> GetAll()
        {
            return await _context.DSFamilyTempSensor
                .ToListAsync();
        }

        public async Task<List<DSFamilyTempSensor>> GetListInApplication(Guid applicationUserId)
        {
            IQueryable<DSFamilyTempSensor> query = from hia in _context.HardwareInApplication
                                              join sensor in _context.DSFamilyTempSensor on hia.HardwareBaseId equals sensor.Id
                                              join au in _context.ApplicationUser on hia.ApplicationId equals au.ApplicationId
                                              where au.Id == applicationUserId
                                              select sensor;

            var data = await query.ToListAsync();

            var ids = data.Select(x => x.Id);

            await _context.HardwareInApplication
                .Where(x => ids.Contains(x.HardwareBaseId))
                .LoadAsync();

            return data;
        }

        public async Task<SensorsInDevice> GetDeviceFromSensor(Guid dsFamilyTempSensorId)
        {
            var entity = await _context.SensorsInDevice
                .SingleOrDefaultAsync(x => x.SensorBaseId == dsFamilyTempSensorId);

            return entity;
        }

        public async Task<List<DSFamilyTempSensor>> GetAllByHardwareId(Guid hardwareId)
        {            
            return await _context.DSFamilyTempSensor
                .Include(x => x.DSFamilyTempSensorResolution)
                .Where(x => x.SensorsInDevice.FirstOrDefault(y => y.DeviceBaseId == hardwareId) != null)
                .ToListAsync();
        }

        public async Task<List<DSFamilyTempSensor>> GetAllThatAreNotInApplicationByDevice(Guid deviceBaseId)
        {
            return await _context.DSFamilyTempSensor
                .Where(x => x.SensorsInDevice.Any(y => y.DeviceBaseId == deviceBaseId))
                .Where(x => !x.HardwaresInApplication.Any())
                .ToListAsync();
        }
    }
}
