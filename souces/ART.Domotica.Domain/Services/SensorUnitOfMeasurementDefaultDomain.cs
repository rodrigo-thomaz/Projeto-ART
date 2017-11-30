using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;

namespace ART.Domotica.Domain.Services
{
    public class SensorUnitOfMeasurementDefaultDomain : DomainBase, ISensorUnitOfMeasurementDefaultDomain
    {
        #region private readonly fields

        private readonly ISensorUnitOfMeasurementDefaultRepository _sensorUnitOfMeasurementDefaultRepository;

        #endregion

        #region constructors

        public SensorUnitOfMeasurementDefaultDomain(ISensorUnitOfMeasurementDefaultRepository sensorUnitOfMeasurementDefaultRepository)
        {
            _sensorUnitOfMeasurementDefaultRepository = sensorUnitOfMeasurementDefaultRepository;
        }

        #endregion

        #region public voids

        public async Task<List<SensorUnitOfMeasurementDefault>> GetAll()
        {
            return await _sensorUnitOfMeasurementDefaultRepository.GetAll();
        }

        #endregion
    }
}
