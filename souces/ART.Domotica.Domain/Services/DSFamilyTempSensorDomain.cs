using System.Threading.Tasks;
using System;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Domotica.Contract;
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

            if (deviceInApplication == null)
            {
                throw new Exception("DeviceInApplication not found");
            }

            return await _dsFamilyTempSensorRepository.GetAllByDeviceId(deviceInApplication.DeviceBaseId);            
        }
        
        public async Task<List<DSFamilyTempSensorResolution>> GetAllResolutions()
        {
            return await _dsFamilyTempSensorResolutionRepository.GetAll();
        }

        public async Task<DSFamilyTempSensor> SetScale(Guid dsFamilyTempSensorId, byte temperatureScaleId)
        {
            var dsFamilyTempSensorEntity = await _dsFamilyTempSensorRepository.GetById(dsFamilyTempSensorId);

            if(dsFamilyTempSensorEntity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            var temperatureScaleEntity = await _temperatureScaleRepository.GetById(temperatureScaleId);

            if (temperatureScaleEntity == null)
            {
                throw new Exception("TemperatureScale not found");
            }
            
            dsFamilyTempSensorEntity.TemperatureScaleId = temperatureScaleEntity.Id;

            await _dsFamilyTempSensorRepository.Update(dsFamilyTempSensorEntity);

            return dsFamilyTempSensorEntity;
        }

        public async Task<DSFamilyTempSensor> SetResolution(Guid dsFamilyTempSensorId, byte dsFamilyTempSensorResolutionId)
        {
            var dsFamilyTempSensorEntity = await _dsFamilyTempSensorRepository.GetById(dsFamilyTempSensorId);

            if (dsFamilyTempSensorEntity == null)
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

            return dsFamilyTempSensorEntity;
        }

        public async Task<DSFamilyTempSensor> SetAlarmOn(Guid dsFamilyTempSensorId, TempSensorAlarmPositionContract position, bool alarmOn)
        {
            var entity = await _dsFamilyTempSensorRepository.GetById(dsFamilyTempSensorId);

            if (entity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            if(position == TempSensorAlarmPositionContract.High)
                entity.HighAlarm.AlarmOn = alarmOn;
            else if (position == TempSensorAlarmPositionContract.Low)
                entity.LowAlarm.AlarmOn = alarmOn;

            await _dsFamilyTempSensorRepository.Update(entity);

            return entity;
        }

        public async Task<DSFamilyTempSensor> SetAlarmCelsius(Guid dsFamilyTempSensorId, TempSensorAlarmPositionContract position, decimal alarmCelsius)
        {
            var entity = await _dsFamilyTempSensorRepository.GetById(dsFamilyTempSensorId);

            if (entity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            if (position == TempSensorAlarmPositionContract.High)
                entity.HighAlarm.AlarmCelsius = alarmCelsius;
            else if (position == TempSensorAlarmPositionContract.Low)
                entity.LowAlarm.AlarmCelsius = alarmCelsius;
            
            await _dsFamilyTempSensorRepository.Update(entity);

            return entity;
        }

        public async Task<DSFamilyTempSensor> SetAlarmBuzzerOn(Guid dsFamilyTempSensorId, TempSensorAlarmPositionContract position, bool alarmBuzzerOn)
        {
            var entity = await _dsFamilyTempSensorRepository.GetById(dsFamilyTempSensorId);

            if (entity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            if (position == TempSensorAlarmPositionContract.High)
                entity.HighAlarm.AlarmBuzzerOn = alarmBuzzerOn;
            else if (position == TempSensorAlarmPositionContract.Low)
                entity.LowAlarm.AlarmBuzzerOn = alarmBuzzerOn;

            await _dsFamilyTempSensorRepository.Update(entity);

            return entity;
        }

        public async Task<DSFamilyTempSensor> SetChartLimiterCelsius(Guid dsFamilyTempSensorId, TempSensorAlarmPositionContract position, decimal chartLimiterCelsius)
        {
            var entity = await _dsFamilyTempSensorRepository.GetById(dsFamilyTempSensorId);

            if (entity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            if (position == TempSensorAlarmPositionContract.High)
                entity.HighChartLimiterCelsius = chartLimiterCelsius;
            else if (position == TempSensorAlarmPositionContract.Low)
                entity.LowChartLimiterCelsius = chartLimiterCelsius;

            await _dsFamilyTempSensorRepository.Update(entity);

            return entity;
        }

        public async Task<SensorsInDevice> GetDeviceFromSensor(Guid dsFamilyTempSensorId)
        {
            var data = await _dsFamilyTempSensorRepository.GetDeviceFromSensor(dsFamilyTempSensorId);

            if (data == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            return data;
        }

        #endregion
    }
}
 