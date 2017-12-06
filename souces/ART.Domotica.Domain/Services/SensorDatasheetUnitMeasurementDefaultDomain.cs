using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;

namespace ART.Domotica.Domain.Services
{
    public class SensorDatasheetUnitMeasurementDefaultDomain : DomainBase, ISensorDatasheetUnitMeasurementDefaultDomain
    {
        #region private readonly fields

        private readonly ISensorDatasheetUnitMeasurementDefaultRepository _sensorDatasheetUnitMeasurementDefaultRepository;

        #endregion

        #region constructors

        public SensorDatasheetUnitMeasurementDefaultDomain(ISensorDatasheetUnitMeasurementDefaultRepository sensorDatasheetUnitMeasurementDefaultRepository)
        {
            _sensorDatasheetUnitMeasurementDefaultRepository = sensorDatasheetUnitMeasurementDefaultRepository;
        }

        #endregion

        #region public voids

        public async Task<List<SensorDatasheetUnitMeasurementDefault>> GetAll()
        {
            return await _sensorDatasheetUnitMeasurementDefaultRepository.GetAll();
        }

        #endregion
    }
}
