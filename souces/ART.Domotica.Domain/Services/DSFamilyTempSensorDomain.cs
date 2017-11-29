using System.Threading.Tasks;
using System;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;
using Autofac;
using ART.Domotica.Repository;
using ART.Domotica.Repository.Repositories;
using ART.Domotica.Enums;
using System.Linq;

namespace ART.Domotica.Domain.Services
{
    public class DSFamilyTempSensorDomain : DomainBase, IDSFamilyTempSensorDomain
    {
        #region private readonly fields

        private readonly IDSFamilyTempSensorRepository _dsFamilyTempSensorRepository;
        private readonly IDSFamilyTempSensorResolutionRepository _dsFamilyTempSensorResolutionRepository;
        private readonly IDeviceInApplicationRepository _deviceInApplicationRepository;
        private readonly IUnitOfMeasurementRepository _unitOfMeasurementRepository;
        private readonly ISensorTriggerRepository _sensorTriggerRepository;

        #endregion

        #region constructors

        public DSFamilyTempSensorDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _dsFamilyTempSensorRepository = new DSFamilyTempSensorRepository(context);
            _dsFamilyTempSensorResolutionRepository = new DSFamilyTempSensorResolutionRepository(context);
            _deviceInApplicationRepository = new DeviceInApplicationRepository(context);
            _unitOfMeasurementRepository = new UnitOfMeasurementRepository(context);
            _sensorTriggerRepository = new SensorTriggerRepository(context);
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

        public async Task<DSFamilyTempSensor> SetUnitOfMeasurement(Guid dsFamilyTempSensorId, UnitOfMeasurementEnum unitOfMeasurementId)
        {
            var dsFamilyTempSensorEntity = await _dsFamilyTempSensorRepository.GetById(dsFamilyTempSensorId);

            if(dsFamilyTempSensorEntity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            var unitOfMeasurementEntity = await _unitOfMeasurementRepository.GetByKey(unitOfMeasurementId, UnitOfMeasurementTypeEnum.Temperature);

            if (unitOfMeasurementEntity == null)
            {
                throw new Exception("UnitOfMeasurement not found");
            }
            
            dsFamilyTempSensorEntity.UnitOfMeasurementId = unitOfMeasurementEntity.Id;

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

        public async Task<DSFamilyTempSensor> SetAlarmOn(Guid dsFamilyTempSensorId, SensorChartLimiterPositionEnum position, bool alarmOn)
        {
            var entity = await _dsFamilyTempSensorRepository.GetById(dsFamilyTempSensorId);

            if (entity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }           

            var sensorTriggers = await _sensorTriggerRepository.GetSensorBaseId(dsFamilyTempSensorId);

            if (position == SensorChartLimiterPositionEnum.Max)
            {
                var maxValue = sensorTriggers.Max(x => Convert.ToDecimal(x.TriggerValue));
                var sensorTrigger = sensorTriggers.First(x => Convert.ToDecimal(x.TriggerValue) == maxValue);
                sensorTrigger.TriggerOn = alarmOn;
            }
            else if (position == SensorChartLimiterPositionEnum.Min)
            {
                var minValue = sensorTriggers.Min(x => Convert.ToDecimal(x.TriggerValue));
                var sensorTrigger = sensorTriggers.First(x => Convert.ToDecimal(x.TriggerValue) == minValue);
                sensorTrigger.TriggerOn = alarmOn;
            }

            await _sensorTriggerRepository.Update(sensorTriggers);

            return entity;
        }

        public async Task<DSFamilyTempSensor> SetAlarmCelsius(Guid dsFamilyTempSensorId, SensorChartLimiterPositionEnum position, decimal alarmCelsius)
        {
            var entity = await _dsFamilyTempSensorRepository.GetById(dsFamilyTempSensorId);

            if (entity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            var sensorTriggers = await _sensorTriggerRepository.GetSensorBaseId(dsFamilyTempSensorId);

            if (position == SensorChartLimiterPositionEnum.Max)
            {
                var maxValue = sensorTriggers.Max(x => Convert.ToDecimal(x.TriggerValue));
                var sensorTrigger = sensorTriggers.First(x => Convert.ToDecimal(x.TriggerValue) == maxValue);
                sensorTrigger.TriggerValue = alarmCelsius.ToString();
            }
            else if (position == SensorChartLimiterPositionEnum.Min)
            {
                var minValue = sensorTriggers.Min(x => Convert.ToDecimal(x.TriggerValue));
                var sensorTrigger = sensorTriggers.First(x => Convert.ToDecimal(x.TriggerValue) == minValue);
                sensorTrigger.TriggerValue = alarmCelsius.ToString();
            }

            await _sensorTriggerRepository.Update(sensorTriggers);

            return entity;
        }

        public async Task<DSFamilyTempSensor> SetAlarmBuzzerOn(Guid dsFamilyTempSensorId, SensorChartLimiterPositionEnum position, bool alarmBuzzerOn)
        {
            var entity = await _dsFamilyTempSensorRepository.GetById(dsFamilyTempSensorId);

            if (entity == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            var sensorTriggers = await _sensorTriggerRepository.GetSensorBaseId(dsFamilyTempSensorId);

            if (position == SensorChartLimiterPositionEnum.Max)
            {
                var maxValue = sensorTriggers.Max(x => Convert.ToDecimal(x.TriggerValue));
                var sensorTrigger = sensorTriggers.First(x => Convert.ToDecimal(x.TriggerValue) == maxValue);
                sensorTrigger.BuzzerOn = alarmBuzzerOn;
            }
            else if (position == SensorChartLimiterPositionEnum.Min)
            {
                var minValue = sensorTriggers.Min(x => Convert.ToDecimal(x.TriggerValue));
                var sensorTrigger = sensorTriggers.First(x => Convert.ToDecimal(x.TriggerValue) == minValue);
                sensorTrigger.BuzzerOn = alarmBuzzerOn;
            }

            await _sensorTriggerRepository.Update(sensorTriggers);

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

        public async Task<DSFamilyTempSensor> GetById(Guid dsFamilyTempSensorId)
        {
            var data = await _dsFamilyTempSensorRepository.GetById(dsFamilyTempSensorId);

            if (data == null)
            {
                throw new Exception("DSFamilyTempSensor not found");
            }

            return data;
        }

        #endregion
    }
}
 