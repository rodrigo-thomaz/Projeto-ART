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

        public async Task<SensorTrigger> SetTriggerOn(Guid sensorTriggerId, Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, bool triggerOn)
        {
            var entity = await _sensorTriggerRepository.GetByKey(sensorTriggerId, sensorId, sensorDatasheetId, sensorTypeId);

            if (entity == null)
            {
                throw new Exception("SensorTrigger not found");
            }

            entity.TriggerOn = triggerOn;

            await _sensorTriggerRepository.Update(entity);

            return entity;
        }

        public async Task<SensorTrigger> SetBuzzerOn(Guid sensorTriggerId, Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, bool buzzerOn)
        {
            var entity = await _sensorTriggerRepository.GetByKey(sensorTriggerId, sensorId, sensorDatasheetId, sensorTypeId);

            if (entity == null)
            {
                throw new Exception("SensorTrigger not found");
            }

            entity.BuzzerOn = buzzerOn;

            await _sensorTriggerRepository.Update(entity);

            return entity;
        }


        public async Task<SensorTrigger> SetTriggerValue(Guid sensorTriggerId, Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, PositionEnum position, decimal triggerValue)
        {
            var entity = await _sensorTriggerRepository.GetByKey(sensorTriggerId, sensorId, sensorDatasheetId, sensorTypeId);

            if (entity == null)
            {
                throw new Exception("SensorTrigger not found");
            }

            if (position == PositionEnum.Max)
            {                
                entity.Max = triggerValue;
            }
            else if (position == PositionEnum.Min)
            {
                entity.Min = triggerValue;
            }

            await _sensorTriggerRepository.Update(entity);

            return entity;
        }

        public async Task<SensorTrigger> Insert(Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, bool triggerOn, bool buzzerOn, decimal max, decimal min)
        {
            var entity = new SensorTrigger
            {
                SensorId = sensorId,
                SensorDatasheetId = sensorDatasheetId,
                SensorTypeId = sensorTypeId,
                TriggerOn = triggerOn,
                BuzzerOn = buzzerOn,
                Max = max,
                Min = min,
            };

            await _sensorTriggerRepository.Insert(entity);

            return entity;
        }

        public async Task<SensorTrigger> Delete(Guid sensorTriggerId, Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId)
        {
            var entity = await _sensorTriggerRepository.GetByKey(sensorTriggerId, sensorId, sensorDatasheetId, sensorTypeId);

            if (entity == null)
            {
                throw new Exception("SensorTrigger not found");
            }

            await _sensorTriggerRepository.Delete(entity);

            return entity;
        }
    }
}