using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;

namespace ART.Domotica.Domain.Services
{
    public class SensorDatasheetDomain : DomainBase, ISensorDatasheetDomain
    {
        #region private readonly fields

        private readonly ISensorDatasheetRepository _sensorDatasheetRepository;

        #endregion

        #region constructors

        public SensorDatasheetDomain(ISensorDatasheetRepository sensorDatasheetRepository)
        {
            _sensorDatasheetRepository = sensorDatasheetRepository;
        }

        #endregion

        #region public voids

        public async Task<List<SensorDatasheet>> GetAll()
        {
            return await _sensorDatasheetRepository.GetAll();
        }

        #endregion
    }
}
