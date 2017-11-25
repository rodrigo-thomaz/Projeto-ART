using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;

namespace ART.Domotica.Domain.Services
{
    public class TimeZoneDomain : DomainBase, ITimeZoneDomain
    {
        #region private readonly fields

        private readonly ITimeZoneRepository _timeZoneRepository;

        #endregion

        #region constructors

        public TimeZoneDomain(ITimeZoneRepository timeZoneRepository)
        {
            _timeZoneRepository = timeZoneRepository;
        }

        #endregion

        #region public voids

        public async Task<List<TimeZone>> GetAll()
        {
            return await _timeZoneRepository.GetAll();
        }

        #endregion
    }
}
