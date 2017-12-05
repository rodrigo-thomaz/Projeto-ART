using System.Threading.Tasks;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Interfaces;
using ART.Domotica.Repository.Entities;
using System.Collections.Generic;
using ART.Infra.CrossCutting.Domain;
using System;
using ART.Domotica.Enums;
using ART.Domotica.Repository.Repositories;
using System.Linq;
using Autofac;
using ART.Domotica.Repository;
using ART.Domotica.Enums.SI;
using ART.Domotica.Repository.Interfaces.SI;
using ART.Domotica.Repository.Repositories.SI;

namespace ART.Domotica.Domain.Services
{
    public class SensorDomain : DomainBase, ISensorDomain
    {
        #region private readonly fields

        private readonly ISensorRepository _sensorRepository;
        private readonly ISensorTriggerRepository _sensorTriggerRepository;
        private readonly IUnitMeasurementRepository _unitMeasurementRepository;
        private readonly IDeviceInApplicationRepository _deviceInApplicationRepository;

        #endregion

        #region constructors

        public SensorDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _sensorRepository = new SensorRepository(context);
            _sensorTriggerRepository = new SensorTriggerRepository(context);
            _unitMeasurementRepository = new UnitMeasurementRepository(context);
            _deviceInApplicationRepository = new DeviceInApplicationRepository(context);
        }

        #endregion

        #region public voids

        public async Task<List<Sensor>> GetAllByApplicationId(Guid applicationId)
        {
            return await _sensorRepository.GetAllByApplicationId(applicationId);
        }

        public async Task<Sensor> SetAlarmOn(Guid sensorId, SensorUnitMeasurementScalePositionEnum position, bool alarmOn)
        {
            var entity = await _sensorRepository.GetByKey(sensorId);

            if (entity == null)
            {
                throw new Exception("Sensor not found");
            }

            var sensorTriggers = await _sensorTriggerRepository.GetSensorId(sensorId);

            if (position == SensorUnitMeasurementScalePositionEnum.Max)
            {
                var maxValue = sensorTriggers.Max(x => Convert.ToDecimal(x.TriggerValue));
                var sensorTrigger = sensorTriggers.First(x => Convert.ToDecimal(x.TriggerValue) == maxValue);
                sensorTrigger.TriggerOn = alarmOn;
            }
            else if (position == SensorUnitMeasurementScalePositionEnum.Min)
            {
                var minValue = sensorTriggers.Min(x => Convert.ToDecimal(x.TriggerValue));
                var sensorTrigger = sensorTriggers.First(x => Convert.ToDecimal(x.TriggerValue) == minValue);
                sensorTrigger.TriggerOn = alarmOn;
            }

            await _sensorTriggerRepository.Update(sensorTriggers);

            return entity;
        }

        public async Task<Sensor> SetAlarmCelsius(Guid sensorId, SensorUnitMeasurementScalePositionEnum position, decimal alarmCelsius)
        {
            var entity = await _sensorRepository.GetByKey(sensorId);

            if (entity == null)
            {
                throw new Exception("Sensor not found");
            }

            var sensorTriggers = await _sensorTriggerRepository.GetSensorId(sensorId);

            if (position == SensorUnitMeasurementScalePositionEnum.Max)
            {
                var maxValue = sensorTriggers.Max(x => Convert.ToDecimal(x.TriggerValue));
                var sensorTrigger = sensorTriggers.First(x => Convert.ToDecimal(x.TriggerValue) == maxValue);
                sensorTrigger.TriggerValue = alarmCelsius.ToString();
            }
            else if (position == SensorUnitMeasurementScalePositionEnum.Min)
            {
                var minValue = sensorTriggers.Min(x => Convert.ToDecimal(x.TriggerValue));
                var sensorTrigger = sensorTriggers.First(x => Convert.ToDecimal(x.TriggerValue) == minValue);
                sensorTrigger.TriggerValue = alarmCelsius.ToString();
            }

            await _sensorTriggerRepository.Update(sensorTriggers);

            return entity;
        }

        public async Task<Sensor> SetAlarmBuzzerOn(Guid sensorId, SensorUnitMeasurementScalePositionEnum position, bool alarmBuzzerOn)
        {
            var entity = await _sensorRepository.GetByKey(sensorId);

            if (entity == null)
            {
                throw new Exception("Sensor not found");
            }

            var sensorTriggers = await _sensorTriggerRepository.GetSensorId(sensorId);

            if (position == SensorUnitMeasurementScalePositionEnum.Max)
            {
                var maxValue = sensorTriggers.Max(x => Convert.ToDecimal(x.TriggerValue));
                var sensorTrigger = sensorTriggers.First(x => Convert.ToDecimal(x.TriggerValue) == maxValue);
                sensorTrigger.BuzzerOn = alarmBuzzerOn;
            }
            else if (position == SensorUnitMeasurementScalePositionEnum.Min)
            {
                var minValue = sensorTriggers.Min(x => Convert.ToDecimal(x.TriggerValue));
                var sensorTrigger = sensorTriggers.First(x => Convert.ToDecimal(x.TriggerValue) == minValue);
                sensorTrigger.BuzzerOn = alarmBuzzerOn;
            }

            await _sensorTriggerRepository.Update(sensorTriggers);

            return entity;
        }

        public async Task<SensorsInDevice> GetDeviceFromSensor(Guid sensorId)
        {
            var data = await _sensorRepository.GetDeviceFromSensor(sensorId);

            if (data == null)
            {
                throw new Exception("Sensor not found");
            }

            return data;
        }

        public async Task<Sensor> GetByKey(Guid sensorId)
        {
            var data = await _sensorRepository.GetByKey(sensorId);

            if (data == null)
            {
                throw new Exception("Sensor not found");
            }

            return data;
        }

        public async Task<Sensor> SetUnitMeasurement(Guid sensorId, UnitMeasurementEnum unitMeasurementId)
        {
            var sensorTempDSFamilyEntity = await _sensorRepository.GetByKey(sensorId);

            if (sensorTempDSFamilyEntity == null)
            {
                throw new Exception("Sensor not found");
            }

            var unitMeasurementEntity = await _unitMeasurementRepository.GetByKey(unitMeasurementId, UnitMeasurementTypeEnum.Temperature);

            if (unitMeasurementEntity == null)
            {
                throw new Exception("UnitMeasurement not found");
            }

            //sensorTempDSFamilyEntity.UnitMeasurementId = unitMeasurementEntity.Id;

            await _sensorRepository.Update(sensorTempDSFamilyEntity);

            return sensorTempDSFamilyEntity;
        }

        public async Task<List<Sensor>> GetAllByDeviceInApplicationId(Guid applicationId, Guid deviceBaseId)
        {
            var deviceInApplication = await _deviceInApplicationRepository.GetByKey(applicationId, deviceBaseId);

            if (deviceInApplication == null)
            {
                throw new Exception("DeviceInApplication not found");
            }

            return await _sensorRepository.GetAllByDeviceId(deviceInApplication.DeviceBaseId);
        }

        #endregion
    }
}
