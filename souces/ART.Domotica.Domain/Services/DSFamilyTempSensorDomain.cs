using System.Threading.Tasks;
using System;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Domotica.Model;
using AutoMapper;
using ART.Domotica.Contract;
using ART.Infra.CrossCutting.MQ.Contract;
using log4net;
using ART.Infra.CrossCutting.Domain;

namespace ART.Domotica.Domain.Services
{
    public class DSFamilyTempSensorDomain : DomainBase, IDSFamilyTempSensorDomain
    {
        #region private readonly fields

        private readonly ILog _log;
        private readonly IDSFamilyTempSensorRepository _dsFamilyTempSensorRepository;
        private readonly IDSFamilyTempSensorResolutionRepository _dsFamilyTempSensorResolutionRepository;

        #endregion

        #region constructors

        public DSFamilyTempSensorDomain(ILog log, IDSFamilyTempSensorRepository dsFamilyTempSensorRepository, IDSFamilyTempSensorResolutionRepository dsFamilyTempSensorResolutionRepository)
        {
            _log = log;
            _dsFamilyTempSensorRepository = dsFamilyTempSensorRepository;
            _dsFamilyTempSensorResolutionRepository = dsFamilyTempSensorResolutionRepository;
        }

        #endregion

        #region public voids

        public async Task<List<DSFamilyTempSensorGetListModel>> GetList(AuthenticatedMessageContract message)
        {
            _log.Debug(message);

            var data = await _dsFamilyTempSensorRepository.GetList();
            var result = Mapper.Map<List<DSFamilyTempSensor>, List<DSFamilyTempSensorGetListModel>>(data);
            return result;
        }

        public async Task<List<DSFamilyTempSensorGetAllModel>> GetAll(Guid applicationUserId)
        {
            _log.Debug(applicationUserId);

            var data = await _dsFamilyTempSensorRepository.GetAll(applicationUserId);
            var result = Mapper.Map<List<DSFamilyTempSensor>, List<DSFamilyTempSensorGetAllModel>>(data);
            return result;
        }

        public async Task<List<DSFamilyTempSensorResolutionGetAllModel>> GetAllResolutions()
        {
            _log.Debug(null);

            var data = await _dsFamilyTempSensorResolutionRepository.GetAll();
            var result = Mapper.Map<List<DSFamilyTempSensorResolution>, List<DSFamilyTempSensorResolutionGetAllModel>>(data);
            return result;
        }

        public async Task SetResolution(AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionContract> message)
        {
            _log.Debug(message);

            var dsFamilyTempSensorEntity = await _dsFamilyTempSensorRepository.GetById(message.Contract.DSFamilyTempSensorId);

            if(dsFamilyTempSensorEntity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            var dsFamilyTempSensorResolutionEntity = await _dsFamilyTempSensorResolutionRepository.GetById(message.Contract.DSFamilyTempSensorResolutionId);

            if (dsFamilyTempSensorResolutionEntity == null)
            {
                throw new Exception("DSFamilyTempSensorResolution not found");
            }

            dsFamilyTempSensorEntity.DSFamilyTempSensorResolutionId = dsFamilyTempSensorResolutionEntity.Id;

            await _dsFamilyTempSensorRepository.Update(dsFamilyTempSensorEntity);
        }

        public async Task SetHighAlarm(AuthenticatedMessageContract<DSFamilyTempSensorSetHighAlarmContract> message)
        {
            _log.Debug(message);

            var entity = await _dsFamilyTempSensorRepository.GetById(message.Contract.DSFamilyTempSensorId);

            if (entity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            entity.HighAlarm = message.Contract.HighAlarm;

            await _dsFamilyTempSensorRepository.Update(entity);
        }

        public async Task SetLowAlarm(AuthenticatedMessageContract<DSFamilyTempSensorSetLowAlarmContract> message)
        {
            _log.Debug(message);

            var entity = await _dsFamilyTempSensorRepository.GetById(message.Contract.DSFamilyTempSensorId);

            if (entity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            entity.LowAlarm = message.Contract.LowAlarm;

            await _dsFamilyTempSensorRepository.Update(entity);
        }

        public async Task<SensorsInDevice> GetDeviceFromSensor(Guid dsFamilyTempSensorId)
        {
            _log.Debug(dsFamilyTempSensorId);

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
 