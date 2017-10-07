using System.Threading.Tasks;
using System;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Domotica.Model;
using AutoMapper;

namespace ART.Domotica.Domain.Services
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

        public async Task<List<DSFamilyTempSensorGetAllModel>> GetAll(Guid applicationUserId)
        {
            var data = await _dsFamilyTempSensorRepository.GetAll(applicationUserId);
            var result = Mapper.Map<List<DSFamilyTempSensor>, List<DSFamilyTempSensorGetAllModel>>(data);
            return result;
        }

        public async Task<List<DSFamilyTempSensorResolutionGetAllModel>> GetAllResolutions()
        {
            var data = await _dsFamilyTempSensorResolutionRepository.GetAll();
            var result = Mapper.Map<List<DSFamilyTempSensorResolution>, List<DSFamilyTempSensorResolutionGetAllModel>>(data);
            return result;
        }

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

            await _dsFamilyTempSensorRepository.Update(dsFamilyTempSensorEntity);
        }

        public async Task SetHighAlarm(Guid dsFamilyTempSensorId, decimal highAlarm)
        {
            var entity = await _dsFamilyTempSensorRepository.GetById(dsFamilyTempSensorId);

            if (entity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            entity.HighAlarm = highAlarm;

            await _dsFamilyTempSensorRepository.Update(entity);
        }

        public async Task SetLowAlarm(Guid dsFamilyTempSensorId, decimal lowAlarm)
        {
            var entity = await _dsFamilyTempSensorRepository.GetById(dsFamilyTempSensorId);

            if (entity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            entity.LowAlarm = lowAlarm;

            await _dsFamilyTempSensorRepository.Update(entity);
        }

        public async Task<SensorsInDevice> GetDeviceFromSensor(Guid dsFamilyTempSensorId)
        {
            var entity = await _dsFamilyTempSensorRepository.GetDeviceFromSensor(dsFamilyTempSensorId);

            if (entity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }            

            return entity;
        }

        #endregion
    }
}
 