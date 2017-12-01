namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IDSFamilyTempSensorDomain
    {
        #region Methods

        Task<List<DSFamilyTempSensor>> GetAllByDeviceInApplicationId(Guid deviceInApplicationId);

        Task<List<DSFamilyTempSensorResolution>> GetAllResolutions();

        Task<DSFamilyTempSensor> GetById(Guid dsFamilyTempSensorId);

        Task<SensorsInDevice> GetDeviceFromSensor(Guid dsFamilyTempSensorId);

        Task<DSFamilyTempSensor> SetAlarmBuzzerOn(Guid dsFamilyTempSensorId, SensorChartLimiterPositionEnum position, bool alarmBuzzerOn);

        Task<DSFamilyTempSensor> SetAlarmCelsius(Guid dsFamilyTempSensorId, SensorChartLimiterPositionEnum position, decimal alarmCelsius);

        Task<DSFamilyTempSensor> SetAlarmOn(Guid dsFamilyTempSensorId, SensorChartLimiterPositionEnum position, bool alarmOn);

        Task<DSFamilyTempSensor> SetResolution(Guid dsFamilyTempSensorId, byte dsFamilyTempSensorResolutionId);

        Task<DSFamilyTempSensor> SetUnitMeasurement(Guid dsFamilyTempSensorId, UnitMeasurementEnum unitMeasurementId);

        #endregion Methods
    }
}