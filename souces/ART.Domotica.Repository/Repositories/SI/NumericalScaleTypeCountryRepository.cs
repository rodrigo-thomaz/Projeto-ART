using ART.Domotica.Repository.Entities.SI;
using ART.Domotica.Repository.Interfaces.SI;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories.SI
{
    public class NumericalScaleTypeCountryRepository : INumericalScaleTypeCountryRepository
    {
        private readonly ARTDbContext _context;

        public NumericalScaleTypeCountryRepository(ARTDbContext context)
        {
            _context = context;
        }

        public async Task<List<NumericalScaleTypeCountry>> GetAll()
        {
            return await _context.NumericalScaleTypeCountry
                .ToListAsync();
        }
    }
}
