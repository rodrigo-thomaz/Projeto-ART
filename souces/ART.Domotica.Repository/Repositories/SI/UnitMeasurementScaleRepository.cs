using ART.Domotica.Repository.Entities.SI;
using ART.Domotica.Repository.Interfaces.SI;
using ART.Infra.CrossCutting.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories.SI
{
    public class UnitMeasurementScaleRepository : RepositoryBase<ARTDbContext, UnitMeasurementScale>, IUnitMeasurementScaleRepository
    {
        public UnitMeasurementScaleRepository(ARTDbContext context)
            : base(context)
        {

        }

        public async Task<List<UnitMeasurementScale>> GetAll()
        {
            return await _context.UnitMeasurementScale
                .ToListAsync();
        }
    }
}
