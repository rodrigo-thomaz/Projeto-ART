namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface ISensorTriggerDomain
    {
        #region Methods

        Task<Sensor> SetAlarmBuzzerOn(Guid sensorId, SensorUnitMeasurementScalePositionEnum position, bool alarmBuzzerOn);

        Task<Sensor> SetAlarmCelsius(Guid sensorId, SensorUnitMeasurementScalePositionEnum position, decimal alarmCelsius);

        Task<Sensor> SetAlarmOn(Guid sensorId, SensorUnitMeasurementScalePositionEnum position, bool alarmOn);

        #endregion Methods
    }
}