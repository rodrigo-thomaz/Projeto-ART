using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using ART.Infra.CrossCutting.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories
{
    public class TempSensorRangeRepository : RepositoryBase<ARTDbContext, TempSensorRange, byte>, ITempSensorRangeRepository
    {
        public TempSensorRangeRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<List<TempSensorRange>> GetAll()
        {
            return await _context.TempSensorRange
                .ToListAsync();
        }
    }
}
