using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;
using System;

namespace ART.Domotica.Domain.Services
{
    public class SensorDomain : DomainBase, ISensorDomain
    {
        #region private readonly fields

        private readonly ISensorRepository _sensorRepository;

        #endregion

        #region constructors

        public SensorDomain(ISensorRepository sensorRepository)
        {
            _sensorRepository = sensorRepository;
        }

        #endregion

        #region public voids

        public async Task<List<Sensor>> GetAll(Guid applicationId)
        {
            return await _sensorRepository.GetAll(applicationId);
        }

        #endregion
    }
}
