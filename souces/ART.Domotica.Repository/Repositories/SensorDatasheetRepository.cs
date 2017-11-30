using ART.Domotica.Enums;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using ART.Infra.CrossCutting.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories
{
    public class SensorDatasheetRepository : RepositoryBase<ARTDbContext, SensorDatasheet, SensorDatasheetEnum>, ISensorDatasheetRepository
    {
        public SensorDatasheetRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<List<SensorDatasheet>> GetAll()
        {
            return await _context.SensorDatasheet
                .ToListAsync();
        }
    }
}
