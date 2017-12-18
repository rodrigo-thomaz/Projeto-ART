namespace ART.Domotica.Domain.Services
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Enums;
    using ART.Domotica.Repository;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Domotica.Repository.Repositories;
    using ART.Infra.CrossCutting.Domain;

    using Autofac;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class SensorTriggerDomain : DomainBase, ISensorTriggerDomain
    {
        #region Fields

        private readonly ISensorTriggerRepository _sensorTriggerRepository;
        private readonly ISensorRepository _sensorRepository;

        #endregion Fields

        #region Constructors

        public SensorTriggerDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _sensorTriggerRepository = new SensorTriggerRepository(context);
            _sensorRepository = new SensorRepository(context);
        }

        #endregion Constructors

        public async Task<Sensor> SetAlarmOn(Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, SensorUnitMeasurementScalePositionEnum position, bool alarmOn)
        {
            var entity = await _sensorRepository.GetByKey(sensorId, sensorDatasheetId, sensorTypeId);

            if (entity == null)
            {
                throw new Exception("Sensor not found");
            }

            var sensorTriggers = await _sensorTriggerRepository.GetSensorId(sensorId);

            if (position == SensorUnitMeasurementScalePositionEnum.Max)
            {
                var maxValue = sensorTriggers.Max(x => Convert.ToDecimal(x.Max));
                var sensorTrigger = sensorTriggers.First(x => Convert.ToDecimal(x.Max) == maxValue);
                sensorTrigger.TriggerOn = alarmOn;
            }
            else if (position == SensorUnitMeasurementScalePositionEnum.Min)
            {
                var minValue = sensorTriggers.Min(x => Convert.ToDecimal(x.Min));
                var sensorTrigger = sensorTriggers.First(x => Convert.ToDecimal(x.Min) == minValue);
                sensorTrigger.TriggerOn = alarmOn;
            }

            await _sensorTriggerRepository.Update(sensorTriggers);

            return entity;
        }

        public async Task<Sensor> SetAlarmCelsius(Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, SensorUnitMeasurementScalePositionEnum position, decimal alarmCelsius)
        {
            var entity = await _sensorRepository.GetByKey(sensorId, sensorDatasheetId, sensorTypeId);

            if (entity == null)
            {
                throw new Exception("Sensor not found");
            }

            var sensorTriggers = await _sensorTriggerRepository.GetSensorId(sensorId);

            if (position == SensorUnitMeasurementScalePositionEnum.Max)
            {
                var maxValue = sensorTriggers.Max(x => Convert.ToDecimal(x.Max));
                var sensorTrigger = sensorTriggers.First(x => Convert.ToDecimal(x.Max) == maxValue);
                sensorTrigger.Max = alarmCelsius;
            }
            else if (position == SensorUnitMeasurementScalePositionEnum.Min)
            {
                var minValue = sensorTriggers.Min(x => Convert.ToDecimal(x.Min));
                var sensorTrigger = sensorTriggers.First(x => Convert.ToDecimal(x.Min) == minValue);
                sensorTrigger.Min = alarmCelsius;
            }

            await _sensorTriggerRepository.Update(sensorTriggers);

            return entity;
        }

        public async Task<Sensor> SetAlarmBuzzerOn(Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, SensorUnitMeasurementScalePositionEnum position, bool alarmBuzzerOn)
        {
            var entity = await _sensorRepository.GetByKey(sensorId, sensorDatasheetId, sensorTypeId);

            if (entity == null)
            {
                throw new Exception("Sensor not found");
            }

            var sensorTriggers = await _sensorTriggerRepository.GetSensorId(sensorId);

            if (position == SensorUnitMeasurementScalePositionEnum.Max)
            {
                var maxValue = sensorTriggers.Max(x => Convert.ToDecimal(x.Max));
                var sensorTrigger = sensorTriggers.First(x => Convert.ToDecimal(x.Max) == maxValue);
                sensorTrigger.BuzzerOn = alarmBuzzerOn;
            }
            else if (position == SensorUnitMeasurementScalePositionEnum.Min)
            {
                var minValue = sensorTriggers.Min(x => Convert.ToDecimal(x.Min));
                var sensorTrigger = sensorTriggers.First(x => Convert.ToDecimal(x.Min) == minValue);
                sensorTrigger.BuzzerOn = alarmBuzzerOn;
            }

            await _sensorTriggerRepository.Update(sensorTriggers);

            return entity;
        }
    }
}