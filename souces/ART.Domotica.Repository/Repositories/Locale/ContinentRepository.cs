using ART.Domotica.Enums.Locale;
using ART.Domotica.Repository.Entities.Locale;
using ART.Domotica.Repository.Interfaces.Locale;
using ART.Infra.CrossCutting.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories.Locale
{
    public class ContinentRepository : RepositoryBase<ARTDbContext, Continent, ContinentEnum>, IContinentRepository
    {
        public ContinentRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<List<Continent>> GetAll()
        {
            return await _context.Continent
                .ToListAsync();
        }
    }
}
