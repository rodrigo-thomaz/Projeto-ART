namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface ISensorDomain
    {
        #region Methods

        Task<List<Sensor>> GetAll(Guid applicationId);

        Task<Sensor> GetById(Guid sensorId);

        Task<SensorsInDevice> GetDeviceFromSensor(Guid sensorId);

        Task<Sensor> SetAlarmBuzzerOn(Guid sensorId, SensorChartLimiterPositionEnum position, bool alarmBuzzerOn);

        Task<Sensor> SetAlarmCelsius(Guid sensorId, SensorChartLimiterPositionEnum position, decimal alarmCelsius);

        Task<Sensor> SetAlarmOn(Guid sensorId, SensorChartLimiterPositionEnum position, bool alarmOn);

        #endregion Methods
    }
}