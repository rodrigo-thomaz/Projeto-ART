using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;

namespace ART.Domotica.Domain.Services
{
    public class TemperatureScaleDomain : DomainBase, ITemperatureScaleDomain
    {
        #region private readonly fields

        private readonly ITemperatureScaleRepository _temperatureScaleRepository;

        #endregion

        #region constructors

        public TemperatureScaleDomain(ITemperatureScaleRepository temperatureScaleRepository)
        {
            _temperatureScaleRepository = temperatureScaleRepository;
        }

        #endregion

        #region public voids

        public async Task<List<TemperatureScale>> GetAll()
        {
            return await _temperatureScaleRepository.GetAll();
        }

        #endregion
    }
}
