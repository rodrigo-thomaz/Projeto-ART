using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;

namespace ART.Domotica.Domain.Services
{
    public class SensorDatasheetUnitMeasurementScaleDomain : DomainBase, ISensorDatasheetUnitMeasurementScaleDomain
    {
        #region private readonly fields

        private readonly ISensorDatasheetUnitMeasurementScaleRepository _sensorDatasheetUnitMeasurementScaleRepository;

        #endregion

        #region constructors

        public SensorDatasheetUnitMeasurementScaleDomain(ISensorDatasheetUnitMeasurementScaleRepository sensorDatasheetUnitMeasurementScaleRepository)
        {
            _sensorDatasheetUnitMeasurementScaleRepository = sensorDatasheetUnitMeasurementScaleRepository;
        }

        #endregion

        #region public voids

        public async Task<List<SensorDatasheetUnitMeasurementScale>> GetAll()
        {
            return await _sensorDatasheetUnitMeasurementScaleRepository.GetAll();
        }

        #endregion
    }
}
