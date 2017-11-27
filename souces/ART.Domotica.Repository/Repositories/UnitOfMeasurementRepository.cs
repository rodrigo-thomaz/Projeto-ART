using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using ART.Infra.CrossCutting.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories
{
    public class UnitOfMeasurementRepository : RepositoryBase<ARTDbContext, UnitOfMeasurement, byte>, IUnitOfMeasurementRepository
    {
        public UnitOfMeasurementRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<List<UnitOfMeasurement>> GetAll()
        {
            return await _context.UnitOfMeasurement
                .ToListAsync();
        }
    }
}
