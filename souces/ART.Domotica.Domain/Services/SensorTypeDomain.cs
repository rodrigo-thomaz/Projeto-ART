using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;

namespace ART.Domotica.Domain.Services
{
    public class SensorTypeDomain : DomainBase, ISensorTypeDomain
    {
        #region private readonly fields

        private readonly ISensorTypeRepository _sensorTypeRepository;

        #endregion

        #region constructors

        public SensorTypeDomain(ISensorTypeRepository sensorTypeRepository)
        {
            _sensorTypeRepository = sensorTypeRepository;
        }

        #endregion

        #region public voids

        public async Task<List<SensorType>> GetAll()
        {
            return await _sensorTypeRepository.GetAll();
        }

        #endregion
    }
}
