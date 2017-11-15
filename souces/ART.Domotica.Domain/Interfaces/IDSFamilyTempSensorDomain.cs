namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IDSFamilyTempSensorDomain
    {
        #region Methods

        Task<List<DSFamilyTempSensor>> GetAllByDeviceInApplicationId(Guid deviceInApplicationId);

        Task<List<DSFamilyTempSensorResolution>> GetAllResolutions();

        Task<SensorsInDevice> GetDeviceFromSensor(Guid dsFamilyTempSensorId);

        Task<DSFamilyTempSensor> SetHighAlarm(AuthenticatedMessageContract<DSFamilyTempSensorSetHighAlarmRequestContract> message);

        Task<DSFamilyTempSensor> SetLowAlarm(AuthenticatedMessageContract<DSFamilyTempSensorSetLowAlarmRequestContract> message);

        Task<DSFamilyTempSensor> SetResolution(AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionRequestContract> message);

        Task<DSFamilyTempSensor> SetScale(AuthenticatedMessageContract<DSFamilyTempSensorSetScaleRequestContract> message);

        Task<DSFamilyTempSensor> SetAlarmOn(AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmOnRequestContract> message);

        Task<DSFamilyTempSensor> SetAlarmOff(AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmOffRequestContract> message);

        #endregion Methods
    }
}