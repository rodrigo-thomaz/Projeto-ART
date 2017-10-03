using System.Collections.Generic;
using System.Threading.Tasks;
using ART.Data.Repository.Entities;
using ART.Data.Repository.Interfaces;
using System.Data.Entity;

namespace ART.Data.Repository.Repositories
{
    public class DSFamilyTempSensorResolutionRepository : RepositoryBase<DSFamilyTempSensorResolution, byte>, IDSFamilyTempSensorResolutionRepository
    {
        public DSFamilyTempSensorResolutionRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<List<DSFamilyTempSensorResolution>> GetAll()
        {
            return await _context.DSFamilyTempSensorResolution.ToListAsync();
        }
    }
}
