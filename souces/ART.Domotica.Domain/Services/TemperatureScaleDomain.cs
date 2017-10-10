using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Domotica.Model;
using AutoMapper;
using log4net;
using ART.Infra.CrossCutting.Domain;
using ART.Infra.CrossCutting.Logging;

namespace ART.Domotica.Domain.Services
{
    public class TemperatureScaleDomain : DomainBase, ITemperatureScaleDomain
    {
        #region private readonly fields

        private readonly ILogger _logger;
        private readonly ITemperatureScaleRepository _temperatureScaleRepository;

        #endregion

        #region constructors

        public TemperatureScaleDomain(ILogger logger, ITemperatureScaleRepository temperatureScaleRepository)
        {
            _logger = logger;
            _temperatureScaleRepository = temperatureScaleRepository;
        }

        #endregion

        #region public voids

        public async Task<List<TemperatureScaleGetAllModel>> GetAll()
        {
            _logger.Debug();

            var data = await _temperatureScaleRepository.GetAll();
            var result = Mapper.Map<List<TemperatureScale>, List<TemperatureScaleGetAllModel>>(data);
            return result;
        }

        #endregion
    }
}
