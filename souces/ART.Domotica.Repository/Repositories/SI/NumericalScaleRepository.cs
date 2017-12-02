using ART.Domotica.Repository.Entities.SI;
using ART.Domotica.Repository.Interfaces.SI;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories.SI
{
    public class NumericalScaleRepository : INumericalScaleRepository
    {
        private readonly ARTDbContext _context;

        public NumericalScaleRepository(ARTDbContext context)
        {
            _context = context;
        }

        public async Task<List<NumericalScale>> GetAll()
        {
            return await _context.NumericalScale
                .ToListAsync();
        }
    }
}
