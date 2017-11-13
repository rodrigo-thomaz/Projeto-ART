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
using ART.Infra.CrossCutting.Domain;
using Autofac;
using ART.Domotica.Repository;
using ART.Domotica.Repository.Repositories;

namespace ART.Domotica.Domain.Services
{
    public class DSFamilyTempSensorDomain : DomainBase, IDSFamilyTempSensorDomain
    {
        #region private readonly fields

        private readonly IDSFamilyTempSensorRepository _dsFamilyTempSensorRepository;
        private readonly IESPDeviceRepository _espDeviceRepository;
        private readonly IDSFamilyTempSensorResolutionRepository _dsFamilyTempSensorResolutionRepository;
        private readonly IHardwareInApplicationRepository _hardwareInApplicationRepository;

        #endregion

        #region constructors

        public DSFamilyTempSensorDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _dsFamilyTempSensorRepository = new DSFamilyTempSensorRepository(context);
            _dsFamilyTempSensorResolutionRepository = new DSFamilyTempSensorResolutionRepository(context);
            _espDeviceRepository = new ESPDeviceRepository(context);
            _hardwareInApplicationRepository = new HardwareInApplicationRepository(context);
        }

        #endregion

        #region public voids

        public async Task<List<DSFamilyTempSensorGetListModel>> GetList(AuthenticatedMessageContract message)
        {
            var data = await _dsFamilyTempSensorRepository.GetList();
            var result = Mapper.Map<List<DSFamilyTempSensor>, List<DSFamilyTempSensorGetListModel>>(data);
            return result;
        }

        public async Task<List<DSFamilyTempSensorGetAllModel>> GetAll(Guid applicationUserId)
        {
            var data = await _dsFamilyTempSensorRepository.GetAll(applicationUserId);
            var result = Mapper.Map<List<DSFamilyTempSensor>, List<DSFamilyTempSensorGetAllModel>>(data);
            return result;
        }

        public async Task<List<DSFamilyTempSensorGetAllByHardwareInApplicationIdResponseContract>> GetAllByHardwareInApplicationId(Guid hardwareApplicationId)
        {
            var hardwareInApplication = await _hardwareInApplicationRepository.GetById(hardwareApplicationId);
            var data = await _dsFamilyTempSensorRepository.GetAllByHardwareId(hardwareInApplication.HardwareBaseId);
            var result = Mapper.Map<List<DSFamilyTempSensor>, List<DSFamilyTempSensorGetAllByHardwareInApplicationIdResponseContract>>(data);
            return result;
        }
        
        public async Task<List<DSFamilyTempSensorResolutionGetAllModel>> GetAllResolutions()
        {
            var data = await _dsFamilyTempSensorResolutionRepository.GetAll();
            var result = Mapper.Map<List<DSFamilyTempSensorResolution>, List<DSFamilyTempSensorResolutionGetAllModel>>(data);
            return result;
        }

        public async Task SetResolution(AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionContract> message)
        {
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
 