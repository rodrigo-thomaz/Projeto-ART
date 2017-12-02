using System.Threading.Tasks;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;
using ART.Domotica.Domain.Interfaces.Locale;
using ART.Domotica.Repository.Entities.Locale;
using ART.Domotica.Repository.Interfaces.Locale;

namespace ART.Domotica.Domain.Services.Locale
{
    public class ContinentDomain : DomainBase, IContinentDomain
    {
        #region private readonly fields

        private readonly IContinentRepository _continentRepository;

        #endregion

        #region constructors

        public ContinentDomain(IContinentRepository continentRepository)
        {
            _continentRepository = continentRepository;
        }

        #endregion

        #region public voids

        public async Task<List<Continent>> GetAll()
        {
            return await _continentRepository.GetAll();
        }

        #endregion
    }
}
