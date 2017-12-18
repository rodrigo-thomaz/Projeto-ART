namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface ISensorTriggerDomain
    {
        #region Methods

        Task<SensorTrigger> Delete(Guid sensorTriggerId, Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId);

        Task<SensorTrigger> Insert(Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, bool triggerOn, bool buzzerOn, decimal max, decimal min);

        Task<SensorTrigger> SetBuzzerOn(Guid sensorTriggerId, Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, bool triggerOn);

        Task<SensorTrigger> SetTriggerOn(Guid sensorTriggerId, Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, bool triggerOn);

        Task<SensorTrigger> SetTriggerValue(Guid sensorTriggerId, Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, SensorUnitMeasurementScalePositionEnum position, decimal triggerValue);

        #endregion Methods
    }
}