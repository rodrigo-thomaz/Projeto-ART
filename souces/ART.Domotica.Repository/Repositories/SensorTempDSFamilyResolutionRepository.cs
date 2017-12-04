using System.Collections.Generic;
using System.Threading.Tasks;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using System.Data.Entity;
using ART.Infra.CrossCutting.Repository;

namespace ART.Domotica.Repository.Repositories
{
    public class SensorTempDSFamilyResolutionRepository : RepositoryBase<ARTDbContext, SensorTempDSFamilyResolution, byte>, ISensorTempDSFamilyResolutionRepository
    {
        public SensorTempDSFamilyResolutionRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<List<SensorTempDSFamilyResolution>> GetAll()
        {
            return await _context.SensorTempDSFamilyResolution
                .ToListAsync();
        }
    }
}
