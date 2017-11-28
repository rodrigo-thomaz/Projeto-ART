namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IDSFamilyTempSensorDomain
    {
        #region Methods

        Task<List<DSFamilyTempSensor>> GetAllByDeviceInApplicationId(Guid deviceInApplicationId);

        Task<List<DSFamilyTempSensorResolution>> GetAllResolutions();

        Task<SensorsInDevice> GetDeviceFromSensor(Guid dsFamilyTempSensorId);

        Task<DSFamilyTempSensor> GetById(Guid dsFamilyTempSensorId);

        Task<DSFamilyTempSensor> SetAlarmBuzzerOn(Guid dsFamilyTempSensorId, TempSensorAlarmPositionContract position, bool alarmBuzzerOn);

        Task<DSFamilyTempSensor> SetAlarmCelsius(Guid dsFamilyTempSensorId, TempSensorAlarmPositionContract position, decimal alarmCelsius);

        Task<DSFamilyTempSensor> SetAlarmOn(Guid dsFamilyTempSensorId, TempSensorAlarmPositionContract position, bool alarmOn);

        Task<SensorChartLimiter> SetChartLimiterCelsius(Guid sensorBaseId, TempSensorAlarmPositionContract position, decimal chartLimiterCelsius);

        Task<DSFamilyTempSensor> SetResolution(Guid dsFamilyTempSensorId, byte dsFamilyTempSensorResolutionId);

        Task<DSFamilyTempSensor> SetUnitOfMeasurement(Guid dsFamilyTempSensorId, UnitOfMeasurementEnum unitOfMeasurementId);

        #endregion Methods
    }
}