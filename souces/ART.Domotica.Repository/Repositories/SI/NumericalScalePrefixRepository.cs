using ART.Domotica.Enums.SI;
using ART.Domotica.Repository.Entities.SI;
using ART.Domotica.Repository.Interfaces.SI;
using ART.Infra.CrossCutting.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories.SI
{
    public class NumericalScalePrefixRepository : RepositoryBase<ARTDbContext, NumericalScalePrefix, NumericalScalePrefixEnum>, INumericalScalePrefixRepository
    {
        public NumericalScalePrefixRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<List<NumericalScalePrefix>> GetAll()
        {
            return await _context.NumericalScalePrefix
                .ToListAsync();
        }
    }
}
