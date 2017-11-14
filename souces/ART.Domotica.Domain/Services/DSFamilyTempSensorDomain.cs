using System.Threading.Tasks;
using System;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
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
        private readonly IDeviceInApplicationRepository _deviceInApplicationRepository;

        #endregion

        #region constructors

        public DSFamilyTempSensorDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _dsFamilyTempSensorRepository = new DSFamilyTempSensorRepository(context);
            _dsFamilyTempSensorResolutionRepository = new DSFamilyTempSensorResolutionRepository(context);
            _espDeviceRepository = new ESPDeviceRepository(context);
            _deviceInApplicationRepository = new DeviceInApplicationRepository(context);
        }

        #endregion

        #region public voids

        public async Task<List<DSFamilyTempSensor>> GetAllByDeviceInApplicationId(Guid deviceInApplicationId)
        {
            var deviceInApplication = await _deviceInApplicationRepository.GetById(deviceInApplicationId);
            return await _dsFamilyTempSensorRepository.GetAllByDeviceId(deviceInApplication.DeviceBaseId);            
        }
        
        public async Task<List<DSFamilyTempSensorResolution>> GetAllResolutions()
        {
            return await _dsFamilyTempSensorResolutionRepository.GetAll();
        }

        public async Task SetResolution(AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionRequestContract> message)
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

        public async Task SetHighAlarm(AuthenticatedMessageContract<DSFamilyTempSensorSetHighAlarmRequestContract> message)
        {
            var entity = await _dsFamilyTempSensorRepository.GetById(message.Contract.DSFamilyTempSensorId);

            if (entity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            entity.HighAlarm = message.Contract.HighAlarm;

            await _dsFamilyTempSensorRepository.Update(entity);
        }

        public async Task SetLowAlarm(AuthenticatedMessageContract<DSFamilyTempSensorSetLowAlarmRequestContract> message)
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
 