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

        public async Task<List<DSFamilyTempSensor>> GetAll(Guid applicationId)
        {
            IQueryable<DSFamilyTempSensor> query = from sensor in _context.DSFamilyTempSensor
                        join hardApp in _context.HardwaresInApplication on sensor.Id equals hardApp.HardwareBaseId
                        where hardApp.ApplicationId == applicationId
                        select sensor;

            return await query.ToListAsync();
        }

        public async Task<SensorsInDevice> GetDeviceFromSensor(Guid dsFamilyTempSensorId)
        {
            var entity = await _context.SensorsInDevice
                .SingleOrDefaultAsync(x => x.SensorBaseId == dsFamilyTempSensorId);

            return entity;
        }
    }
}
