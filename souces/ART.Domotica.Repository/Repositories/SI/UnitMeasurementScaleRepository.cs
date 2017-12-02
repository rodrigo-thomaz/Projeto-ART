using ART.Domotica.Repository.Entities.SI;
using ART.Domotica.Repository.Interfaces.SI;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories.SI
{
    public class UnitMeasurementScaleRepository : IUnitMeasurementScaleRepository
    {
        private readonly ARTDbContext _context;

        public UnitMeasurementScaleRepository(ARTDbContext context)
        {
            _context = context;
        }

        public async Task<List<UnitMeasurementScale>> GetAll()
        {
            return await _context.UnitMeasurementScale
                .ToListAsync();
        }
    }
}
