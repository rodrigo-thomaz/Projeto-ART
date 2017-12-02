using ART.Domotica.Enums.SI;
using ART.Domotica.Repository.Entities.SI;
using ART.Domotica.Repository.Interfaces.SI;
using ART.Infra.CrossCutting.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories.SI
{
    public class NumericalScaleTypeRepository : RepositoryBase<ARTDbContext, NumericalScaleType, NumericalScaleTypeEnum>, INumericalScaleTypeRepository
    {
        public NumericalScaleTypeRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<List<NumericalScaleType>> GetAll()
        {
            return await _context.NumericalScaleType
                .ToListAsync();
        }
    }
}
