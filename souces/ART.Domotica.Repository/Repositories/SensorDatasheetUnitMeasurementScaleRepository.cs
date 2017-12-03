using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories
{
    public class SensorDatasheetUnitMeasurementScaleRepository : ISensorDatasheetUnitMeasurementScaleRepository
    {
        private readonly ARTDbContext _context;

        public SensorDatasheetUnitMeasurementScaleRepository(ARTDbContext context)
        {
            _context = context;
        }

        public async Task<List<SensorDatasheetUnitMeasurementScale>> GetAll()
        {
            return await _context.SensorDatasheetUnitMeasurementScale
                .ToListAsync();
        }
    }
}
