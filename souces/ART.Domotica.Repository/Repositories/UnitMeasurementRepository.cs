using ART.Domotica.Enums;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories
{
    public class UnitMeasurementRepository : IUnitMeasurementRepository
    {
        private readonly ARTDbContext _context;

        public UnitMeasurementRepository(ARTDbContext context)
        {
            _context = context;
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
