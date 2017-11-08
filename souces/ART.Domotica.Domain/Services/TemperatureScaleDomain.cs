using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Domotica.Model;
using AutoMapper;
using ART.Infra.CrossCutting.Domain;
using ART.Domotica.Contract;

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

        public async Task<List<TemperatureScaleGetAllModel>> GetAll()
        {
            var data = await _temperatureScaleRepository.GetAll();
            var result = Mapper.Map<List<TemperatureScale>, List<TemperatureScaleGetAllModel>>(data);
            return result;
        }

        public async Task<List<TemperatureScaleGetAllForDeviceResponseContract>> GetAllForDevice()
        {
            var data = await _temperatureScaleRepository.GetAll();
            var result = Mapper.Map<List<TemperatureScale>, List<TemperatureScaleGetAllForDeviceResponseContract>>(data);
            return result;
        }

        #endregion
    }
}
