namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Model;

    public interface IDSFamilyTempSensorDomain
    {
        #region Methods

        Task<List<DSFamilyTempSensor>> GetAll(Guid applicationUserId);

        Task<SensorsInDevice> GetDeviceFromSensor(Guid dsFamilyTempSensorId);

        Task<List<DSFamilyTempSensorResolutionGetAllModel>> GetAllResolutions();

        Task SetHighAlarm(Guid dsFamilyTempSensorId, decimal highAlarm);

        Task SetLowAlarm(Guid dsFamilyTempSensorId, decimal lowAlarm);

        Task SetResolution(Guid dsFamilyTempSensorId, byte dsFamilyTempSensorResolutionId);

        #endregion Methods
    }
}