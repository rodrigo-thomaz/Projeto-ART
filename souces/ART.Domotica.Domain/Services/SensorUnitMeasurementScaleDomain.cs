using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;

namespace ART.Domotica.Domain.Services
{
    public class SensorUnitMeasurementScaleDomain : DomainBase, ISensorUnitMeasurementScaleDomain
    {
        #region private readonly fields

        private readonly ISensorUnitMeasurementScaleRepository _sensorUnitMeasurementScaleRepository;

        #endregion

        #region constructors

        public SensorUnitMeasurementScaleDomain(ISensorUnitMeasurementScaleRepository sensorUnitMeasurementScaleRepository)
        {
            _sensorUnitMeasurementScaleRepository = sensorUnitMeasurementScaleRepository;
        }

        #endregion

        #region public voids

        public async Task<List<SensorUnitMeasurementScale>> GetAll()
        {
            return await _sensorUnitMeasurementScaleRepository.GetAll();
        }

        #endregion
    }
}
