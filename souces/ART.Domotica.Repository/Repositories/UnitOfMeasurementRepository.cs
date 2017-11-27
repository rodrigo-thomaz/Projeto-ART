using ART.Domotica.Enums;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories
{
    public class UnitOfMeasurementRepository : IUnitOfMeasurementRepository
    {
        private readonly ARTDbContext _context;

        public UnitOfMeasurementRepository(ARTDbContext context)
        {
            _context = context;
        }

        public async Task<List<UnitOfMeasurement>> GetAll()
        {
            return await _context.UnitOfMeasurement
                .ToListAsync();
        }

        public async Task<UnitOfMeasurement> GetByKey(UnitOfMeasurementEnum unitOfMeasurementId, UnitOfMeasurementTypeEnum unitOfMeasurementTypeId)
        {
            var entity = await _context.Set<UnitOfMeasurement>().FindAsync(new object[] 
            {
                unitOfMeasurementId,
                unitOfMeasurementTypeId
            });

            return entity;
        }
    }
}
