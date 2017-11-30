using ART.Domotica.Enums;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using ART.Infra.CrossCutting.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories
{
    public class SensorUnitOfMeasurementDefaultRepository : RepositoryBase<ARTDbContext, SensorUnitOfMeasurementDefault, SensorDatasheetEnum>, ISensorUnitOfMeasurementDefaultRepository
    {
        public SensorUnitOfMeasurementDefaultRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<List<SensorUnitOfMeasurementDefault>> GetAll()
        {
            return await _context.SensorUnitOfMeasurementDefault
                .ToListAsync();
        }
    }
}
