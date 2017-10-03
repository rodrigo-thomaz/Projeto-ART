using System.Threading.Tasks;
using ART.Data.Domain.Interfaces;
using ART.Data.Repository.Interfaces;
using ART.Data.Repository.Entities;
using System.Collections.Generic;

namespace ART.Data.Domain.Services
{
    public class TemperatureScaleDomain : ITemperatureScaleDomain
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

        public async Task<List<TemperatureScale>> GetScales()
        {
            return await _temperatureScaleRepository.GetAll();
        }

        #endregion
    }
}
