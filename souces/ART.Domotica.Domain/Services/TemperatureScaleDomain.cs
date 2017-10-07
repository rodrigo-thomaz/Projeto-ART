using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Domotica.Model;
using AutoMapper;

namespace ART.Domotica.Domain.Services
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

        public async Task<List<TemperatureScaleGetAllModel>> GetAll()
        {
            var entities = await _temperatureScaleRepository.GetAll();
            var models = Mapper.Map<List<TemperatureScale>, List<TemperatureScaleGetAllModel>>(entities);
            return models;
        }

        #endregion
    }
}
