using System.Threading.Tasks;
using System;
using ART.Data.Domain.Interfaces;
using ART.Data.Repository.Interfaces;

namespace ART.Data.Domain.Services
{
    public class DSFamilyTempSensorDomain : IDSFamilyTempSensorDomain
    {
        #region private readonly fields

        private readonly IDSFamilyTempSensorRepository _dsFamilyTempSensorRepository;
        private readonly IDSFamilyTempSensorResolutionRepository _dsFamilyTempSensorResolutionRepository;

        #endregion

        #region constructors

        public DSFamilyTempSensorDomain(IDSFamilyTempSensorRepository dsFamilyTempSensorRepository, IDSFamilyTempSensorResolutionRepository dsFamilyTempSensorResolutionRepository)
        {
            _dsFamilyTempSensorRepository = dsFamilyTempSensorRepository;
            _dsFamilyTempSensorResolutionRepository = dsFamilyTempSensorResolutionRepository;
        }

        #endregion

        #region public voids

        public async Task SetResolution(Guid dsFamilyTempSensorId, byte dsFamilyTempSensorResolutionId)
        {
            var dsFamilyTempSensorEntity = await _dsFamilyTempSensorRepository.GetById(dsFamilyTempSensorId);

            if(dsFamilyTempSensorEntity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            var dsFamilyTempSensorResolutionEntity = await _dsFamilyTempSensorResolutionRepository.GetById(dsFamilyTempSensorResolutionId);

            if (dsFamilyTempSensorResolutionEntity == null)
            {
                throw new Exception("DSFamilyTempSensorResolution not found");
            }

            dsFamilyTempSensorEntity.DSFamilyTempSensorResolutionId = dsFamilyTempSensorResolutionEntity.Id;
            dsFamilyTempSensorEntity.DSFamilyTempSensorResolution = dsFamilyTempSensorResolutionEntity;

            await _dsFamilyTempSensorRepository.Update(dsFamilyTempSensorEntity);
        }

        public async Task SetHighAlarm(Guid dsFamilyTempSensorId, decimal highAlarm)
        {
            var dsFamilyTempSensorEntity = await _dsFamilyTempSensorRepository.GetById(dsFamilyTempSensorId);

            if (dsFamilyTempSensorEntity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            dsFamilyTempSensorEntity.HighAlarm = highAlarm;

            await _dsFamilyTempSensorRepository.Update(dsFamilyTempSensorEntity);
        }

        public async Task SetLowAlarm(Guid dsFamilyTempSensorId, decimal lowAlarm)
        {
            var dsFamilyTempSensorEntity = await _dsFamilyTempSensorRepository.GetById(dsFamilyTempSensorId);

            if (dsFamilyTempSensorEntity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            dsFamilyTempSensorEntity.LowAlarm = lowAlarm;

            await _dsFamilyTempSensorRepository.Update(dsFamilyTempSensorEntity);
        }

        #endregion
    }
}
 