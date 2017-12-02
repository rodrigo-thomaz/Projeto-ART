using ART.Domotica.Enums;
using ART.Domotica.Repository.Entities.SI;
using ART.Domotica.Repository.Interfaces;
using ART.Infra.CrossCutting.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories
{
    public class UnitMeasurementRepository : RepositoryBase<ARTDbContext, UnitMeasurement, UnitMeasurementEnum>, IUnitMeasurementRepository
    {
        public UnitMeasurementRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<List<UnitMeasurement>> GetAll()
        {
            return await _context.UnitMeasurement
                .ToListAsync();
        }

        public async Task<UnitMeasurement> GetByKey(UnitMeasurementEnum unitMeasurementId, UnitMeasurementTypeEnum unitMeasurementTypeId)
        {
            var entity = await _context.Set<UnitMeasurement>().FindAsync(new object[] 
            {
                unitMeasurementId,
                unitMeasurementTypeId
            });

            return entity;
        }
    }
}
