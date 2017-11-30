using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using ART.Infra.CrossCutting.Repository;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories
{
    public class SensorTriggerRepository : RepositoryBase<ARTDbContext, SensorTrigger, Guid>, ISensorTriggerRepository
    {
        public SensorTriggerRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<List<SensorTrigger>> GetSensorId(Guid sensorId)
        {
            return await _context.SensorTrigger
                .Where(x => x.SensorId == sensorId)
                .ToListAsync();
        }
    }
}
