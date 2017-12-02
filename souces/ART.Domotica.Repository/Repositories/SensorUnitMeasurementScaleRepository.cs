using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories
{
    public class SensorUnitMeasurementScaleRepository : ISensorUnitMeasurementScaleRepository
    {
        private readonly ARTDbContext _context;

        public SensorUnitMeasurementScaleRepository(ARTDbContext context)
        {
            _context = context;
        }

        public async Task<List<SensorUnitMeasurementScale>> GetAll()
        {
            return await _context.SensorUnitMeasurementScale
                .ToListAsync();
        }
    }
}
