using System.Collections.Generic;
using System.Threading.Tasks;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using System.Data.Entity;
using ART.Infra.CrossCutting.Repository;

namespace ART.Domotica.Repository.Repositories
{
    public class DSFamilyTempSensorResolutionRepository : RepositoryBase<ARTDbContext, DSFamilyTempSensorResolution, byte>, IDSFamilyTempSensorResolutionRepository
    {
        public DSFamilyTempSensorResolutionRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<List<DSFamilyTempSensorResolution>> GetAll()
        {
            return await _context.DSFamilyTempSensorResolution
                .ToListAsync();
        }
    }
}
