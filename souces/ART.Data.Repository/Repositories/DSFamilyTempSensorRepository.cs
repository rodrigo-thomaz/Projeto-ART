using ART.Data.Repository.Entities;
using ART.Data.Repository.Interfaces;
using ART.Infra.CrossCutting.Repository;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Data.Repository.Repositories
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
    }
}
