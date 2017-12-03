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

namespace ART.Domotica.Domain.Services
{
    public class SensorDomain : DomainBase, ISensorDomain
    {
        #region private readonly fields

        private readonly ISensorRepository _sensorRepository;
        private readonly ISensorTriggerRepository _sensorTriggerRepository;

        #endregion

        #region constructors

        public SensorDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _sensorRepository = new SensorRepository(context);
            _sensorTriggerRepository = new SensorTriggerRepository(context);
        }

        #endregion

        #region public voids

        public async Task<List<Sensor>> GetAll(Guid applicationId)
        {
            return await _sensorRepository.GetAll(applicationId);
        }

        public async Task<Sensor> SetAlarmOn(Guid sensorId, SensorChartLimiterPositionEnum position, bool alarmOn)
        {
            var entity = await _sensorRepository.GetById(sensorId);

            if (entity == null)
            {
                throw new Exception("Sensor not found");
            }

            var sensorTriggers = await _sensorTriggerRepository.GetSensorId(sensorId);

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

        public async Task<Sensor> SetAlarmCelsius(Guid sensorId, SensorChartLimiterPositionEnum position, decimal alarmCelsius)
        {
            var entity = await _sensorRepository.GetById(sensorId);

            if (entity == null)
            {
                throw new Exception("Sensor not found");
            }

            var sensorTriggers = await _sensorTriggerRepository.GetSensorId(sensorId);

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

        public async Task<Sensor> SetAlarmBuzzerOn(Guid sensorId, SensorChartLimiterPositionEnum position, bool alarmBuzzerOn)
        {
            var entity = await _sensorRepository.GetById(sensorId);

            if (entity == null)
            {
                throw new Exception("Sensor not found");
            }

            var sensorTriggers = await _sensorTriggerRepository.GetSensorId(sensorId);

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

        public async Task<SensorsInDevice> GetDeviceFromSensor(Guid sensorId)
        {
            var data = await _sensorRepository.GetDeviceFromSensor(sensorId);

            if (data == null)
            {
                throw new Exception("Sensor not found");
            }

            return data;
        }

        public async Task<Sensor> GetById(Guid sensorId)
        {
            var data = await _sensorRepository.GetById(sensorId);

            if (data == null)
            {
                throw new Exception("Sensor not found");
            }

            return data;
        }
        
        #endregion
    }
}
