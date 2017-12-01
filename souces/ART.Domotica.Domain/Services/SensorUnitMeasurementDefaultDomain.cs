using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;

namespace ART.Domotica.Domain.Services
{
    public class SensorUnitMeasurementDefaultDomain : DomainBase, ISensorUnitMeasurementDefaultDomain
    {
        #region private readonly fields

        private readonly ISensorUnitMeasurementDefaultRepository _sensorUnitMeasurementDefaultRepository;

        #endregion

        #region constructors

        public SensorUnitMeasurementDefaultDomain(ISensorUnitMeasurementDefaultRepository sensorUnitMeasurementDefaultRepository)
        {
            _sensorUnitMeasurementDefaultRepository = sensorUnitMeasurementDefaultRepository;
        }

        #endregion

        #region public voids

        public async Task<List<SensorUnitMeasurementDefault>> GetAll()
        {
            return await _sensorUnitMeasurementDefaultRepository.GetAll();
        }

        #endregion
    }
}
