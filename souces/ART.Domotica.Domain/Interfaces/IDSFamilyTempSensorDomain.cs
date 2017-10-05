namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IDSFamilyTempSensorDomain
    {
        #region Methods

        Task<List<DSFamilyTempSensor>> GetAll(Guid applicationId);

        Task<SensorsInDevice> GetDeviceFromSensor(Guid dsFamilyTempSensorId);

        Task<List<DSFamilyTempSensorResolution>> GetResolutions();

        Task SetHighAlarm(Guid dsFamilyTempSensorId, decimal highAlarm);

        Task SetLowAlarm(Guid dsFamilyTempSensorId, decimal lowAlarm);

        Task SetResolution(Guid dsFamilyTempSensorId, byte dsFamilyTempSensorResolutionId);

        #endregion Methods
    }
}