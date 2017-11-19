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
        private readonly ITemperatureScaleRepository _temperatureScaleRepository;

        #endregion

        #region constructors

        public DSFamilyTempSensorDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _dsFamilyTempSensorRepository = new DSFamilyTempSensorRepository(context);
            _dsFamilyTempSensorResolutionRepository = new DSFamilyTempSensorResolutionRepository(context);
            _espDeviceRepository = new ESPDeviceRepository(context);
            _deviceInApplicationRepository = new DeviceInApplicationRepository(context);
            _temperatureScaleRepository = new TemperatureScaleRepository(context);
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

        public async Task<DSFamilyTempSensor> SetScale(AuthenticatedMessageContract<DSFamilyTempSensorSetScaleRequestContract> message)
        {
            var dsFamilyTempSensorEntity = await _dsFamilyTempSensorRepository.GetById(message.Contract.DSFamilyTempSensorId);

            if(dsFamilyTempSensorEntity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            var temperatureScaleEntity = await _temperatureScaleRepository.GetById(message.Contract.TemperatureScaleId);

            if (temperatureScaleEntity == null)
            {
                throw new Exception("TemperatureScale not found");
            }
            
            dsFamilyTempSensorEntity.TemperatureScaleId = temperatureScaleEntity.Id;

            await _dsFamilyTempSensorRepository.Update(dsFamilyTempSensorEntity);

            //LoadDevice
            await _dsFamilyTempSensorRepository.GetDeviceFromSensor(dsFamilyTempSensorEntity.Id);

            return dsFamilyTempSensorEntity;
        }

        public async Task<DSFamilyTempSensor> SetResolution(AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionRequestContract> message)
        {
            var dsFamilyTempSensorEntity = await _dsFamilyTempSensorRepository.GetById(message.Contract.DSFamilyTempSensorId);

            if (dsFamilyTempSensorEntity == null)
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

            //LoadDevice
            await _dsFamilyTempSensorRepository.GetDeviceFromSensor(dsFamilyTempSensorEntity.Id);

            return dsFamilyTempSensorEntity;
        }

        public async Task<DSFamilyTempSensor> SetAlarmOn(AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmOnRequestContract> message)
        {
            var entity = await _dsFamilyTempSensorRepository.GetById(message.Contract.DSFamilyTempSensorId);

            if (entity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            if(message.Contract.Position == TempSensorAlarmPositionContract.High)
                entity.HighAlarm.AlarmOn = message.Contract.AlarmOn;
            else if (message.Contract.Position == TempSensorAlarmPositionContract.Low)
                entity.LowAlarm.AlarmOn = message.Contract.AlarmOn;

            await _dsFamilyTempSensorRepository.Update(entity);

            //LoadDevice
            await _dsFamilyTempSensorRepository.GetDeviceFromSensor(entity.Id);

            return entity;
        }

        public async Task<DSFamilyTempSensor> SetAlarmCelsius(AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmCelsiusRequestContract> message)
        {
            var entity = await _dsFamilyTempSensorRepository.GetById(message.Contract.DSFamilyTempSensorId);

            if (entity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            if (message.Contract.Position == TempSensorAlarmPositionContract.High)
                entity.HighAlarm.AlarmCelsius = message.Contract.AlarmCelsius;
            else if (message.Contract.Position == TempSensorAlarmPositionContract.Low)
                entity.LowAlarm.AlarmCelsius = message.Contract.AlarmCelsius;
            
            await _dsFamilyTempSensorRepository.Update(entity);

            //LoadDevice
            await _dsFamilyTempSensorRepository.GetDeviceFromSensor(entity.Id);

            return entity;
        }

        public async Task<DSFamilyTempSensor> SetAlarmBuzzerOn(AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmBuzzerOnRequestContract> message)
        {
            var entity = await _dsFamilyTempSensorRepository.GetById(message.Contract.DSFamilyTempSensorId);

            if (entity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            if (message.Contract.Position == TempSensorAlarmPositionContract.High)
                entity.HighAlarm.AlarmBuzzerOn = message.Contract.AlarmBuzzerOn;
            else if (message.Contract.Position == TempSensorAlarmPositionContract.Low)
                entity.LowAlarm.AlarmBuzzerOn = message.Contract.AlarmBuzzerOn;

            await _dsFamilyTempSensorRepository.Update(entity);

            //LoadDevice
            await _dsFamilyTempSensorRepository.GetDeviceFromSensor(entity.Id);

            return entity;
        }

        public async Task<DSFamilyTempSensor> SetChartLimiterCelsius(AuthenticatedMessageContract<DSFamilyTempSensorSetChartLimiterCelsiusRequestContract> message)
        {
            var entity = await _dsFamilyTempSensorRepository.GetById(message.Contract.DSFamilyTempSensorId);

            if (entity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            if (message.Contract.Position == TempSensorAlarmPositionContract.High)
                entity.HighChartLimiterCelsius = message.Contract.ChartLimiterCelsius;
            else if (message.Contract.Position == TempSensorAlarmPositionContract.Low)
                entity.LowChartLimiterCelsius = message.Contract.ChartLimiterCelsius;

            await _dsFamilyTempSensorRepository.Update(entity);

            //LoadDevice
            await _dsFamilyTempSensorRepository.GetDeviceFromSensor(entity.Id);

            return entity;
        }

        #endregion
    }
}
 