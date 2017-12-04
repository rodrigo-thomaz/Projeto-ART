using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using ART.Infra.CrossCutting.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ART.Domotica.Domain.Services
{
    public class DeviceSensorsDomain : DomainBase, IDeviceSensorsDomain
    {
        #region private readonly fields

        private readonly IDeviceSensorsRepository _deviceSensorsRepository;

        #endregion

        #region constructors

        public DeviceSensorsDomain(IDeviceSensorsRepository deviceSensorsRepository)
        {
            _deviceSensorsRepository = deviceSensorsRepository;
        }

        #endregion

        #region public voids

        public async Task<List<DeviceSensors>> GetAllByApplicationId(Guid applicationId)
        {
            return await _deviceSensorsRepository.GetAllByApplicationId(applicationId);
        }

        #endregion
    }
}
