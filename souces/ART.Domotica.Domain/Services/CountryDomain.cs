using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;

namespace ART.Domotica.Domain.Services
{
    public class CountryDomain : DomainBase, ICountryDomain
    {
        #region private readonly fields

        private readonly ICountryRepository _countryRepository;

        #endregion

        #region constructors

        public CountryDomain(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        #endregion

        #region public voids

        public async Task<List<Country>> GetAll()
        {
            return await _countryRepository.GetAll();
        }

        #endregion
    }
}
