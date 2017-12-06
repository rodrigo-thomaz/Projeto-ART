using System.Threading.Tasks;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;
using ART.Domotica.Repository.Interfaces.Globalization;
using ART.Domotica.Repository.Entities.Globalization;
using ART.Domotica.Domain.Interfaces.Globalization;

namespace ART.Domotica.Domain.Services.Globalization
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
