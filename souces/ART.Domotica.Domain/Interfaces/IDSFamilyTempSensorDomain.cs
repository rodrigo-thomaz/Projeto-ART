namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Enums;

    public interface IDSFamilyTempSensorDomain
    {
        #region Methods

        Task<List<DSFamilyTempSensor>> GetAllByDeviceInApplicationId(Guid deviceInApplicationId);

        Task<List<DSFamilyTempSensorResolution>> GetAllResolutions();

        Task<SensorsInDevice> GetDeviceFromSensor(Guid dsFamilyTempSensorId);

        Task<DSFamilyTempSensor> SetAlarmBuzzerOn(Guid dsFamilyTempSensorId, TempSensorAlarmPositionContract position, bool alarmBuzzerOn);

        Task<DSFamilyTempSensor> SetAlarmCelsius(Guid dsFamilyTempSensorId, TempSensorAlarmPositionContract position, decimal alarmCelsius);

        Task<DSFamilyTempSensor> SetAlarmOn(Guid dsFamilyTempSensorId, TempSensorAlarmPositionContract position, bool alarmOn);

        Task<DSFamilyTempSensor> SetChartLimiterCelsius(Guid dsFamilyTempSensorId, TempSensorAlarmPositionContract position, decimal chartLimiterCelsius);

        Task<DSFamilyTempSensor> SetResolution(Guid dsFamilyTempSensorId, byte dsFamilyTempSensorResolutionId);

        Task<DSFamilyTempSensor> SetScale(Guid dsFamilyTempSensorId, UnitOfMeasurementEnum unitOfMeasurementId);

        #endregion Methods
    }
}