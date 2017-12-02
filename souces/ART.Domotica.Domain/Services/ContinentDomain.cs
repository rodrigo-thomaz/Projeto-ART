using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;

namespace ART.Domotica.Domain.Services
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
