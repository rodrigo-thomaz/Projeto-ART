using ART.Data.Repository.Entities;
using ART.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Data.Repository.Repositories
{
    public class TemperatureScaleRepository : RepositoryBase<TemperatureScale, byte>, ITemperatureScaleRepository
    {
        public TemperatureScaleRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<List<TemperatureScale>> GetAll()
        {
            return await _context.TemperatureScale.ToListAsync();
        }
    }
}
