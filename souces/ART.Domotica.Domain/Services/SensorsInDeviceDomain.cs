using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using ART.Infra.CrossCutting.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ART.Domotica.Domain.Services
{
    public class SensorsInDeviceDomain : DomainBase, ISensorsInDeviceDomain
    {
        #region private readonly fields

        private readonly ISensorsInDeviceRepository _sensorsInDeviceRepository;

        #endregion

        #region constructors

        public SensorsInDeviceDomain(ISensorsInDeviceRepository sensorsInDeviceRepository)
        {
            _sensorsInDeviceRepository = sensorsInDeviceRepository;
        }

        #endregion

        #region public voids

        public async Task<List<SensorsInDevice>> GetAllByApplicationId(Guid applicationId)
        {
            return await _sensorsInDeviceRepository.GetAllByApplicationId(applicationId);
        }

        #endregion
    }
}
