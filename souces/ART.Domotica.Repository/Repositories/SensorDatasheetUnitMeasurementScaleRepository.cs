using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using ART.Infra.CrossCutting.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories
{
    public class SensorDatasheetUnitMeasurementScaleRepository : RepositoryBase<ARTDbContext, SensorDatasheetUnitMeasurementScale>, ISensorDatasheetUnitMeasurementScaleRepository
    {
        public SensorDatasheetUnitMeasurementScaleRepository(ARTDbContext context)
            : base(context)
        {
            
        }

        public async Task<List<SensorDatasheetUnitMeasurementScale>> GetAll()
        {
            return await _context.SensorDatasheetUnitMeasurementScale
                .ToListAsync();
        }
    }
}
