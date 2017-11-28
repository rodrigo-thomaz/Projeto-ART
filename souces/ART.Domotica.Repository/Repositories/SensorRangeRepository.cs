using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using ART.Infra.CrossCutting.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories
{
    public class SensorRangeRepository : RepositoryBase<ARTDbContext, SensorRange, byte>, ISensorRangeRepository
    {
        public SensorRangeRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<List<SensorRange>> GetAll()
        {
            return await _context.SensorRange
                .ToListAsync();
        }
    }
}
