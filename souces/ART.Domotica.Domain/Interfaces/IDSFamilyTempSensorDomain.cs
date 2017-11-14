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

        Task SetHighAlarm(AuthenticatedMessageContract<DSFamilyTempSensorSetHighAlarmRequestContract> message);

        Task SetLowAlarm(AuthenticatedMessageContract<DSFamilyTempSensorSetLowAlarmRequestContract> message);

        Task SetResolution(AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionRequestContract> message);

        #endregion Methods
    }
}