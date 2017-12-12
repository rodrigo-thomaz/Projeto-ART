using ART.Domotica.Repository.Entities.Locale;
using ART.Domotica.Repository.Interfaces.Locale;
using ART.Infra.CrossCutting.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;

namespace ART.Domotica.Repository.Repositories.Locale
{
    public class CountryRepository : RepositoryBase<ARTDbContext, Country, short>, ICountryRepository
    {
        public CountryRepository(ARTDbContext context) : base(context)
        {

        }

        public async Task<List<Country>> GetAll()
        {
            return await _context.Country
                .OrderBy(x => x.Name)             
                .ToListAsync();
        }
    }
}
