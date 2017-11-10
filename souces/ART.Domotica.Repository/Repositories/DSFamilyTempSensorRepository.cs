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

        public async Task<List<DSFamilyTempSensor>> GetList()
        {
            var data = await _context.DSFamilyTempSensor
                .ToListAsync();
            return data;
        }

        public async Task<List<DSFamilyTempSensor>> GetAll(Guid applicationUserId)
        {
            /////Errroooooo!!!!!
            IQueryable<DSFamilyTempSensor> query = from sensor in _context.DSFamilyTempSensor
                        join hardApp in _context.HardwareInApplication on sensor.Id equals hardApp.HardwareBaseId
                        where hardApp.ApplicationId == applicationUserId
                        select sensor;

            return await query.ToListAsync();
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
