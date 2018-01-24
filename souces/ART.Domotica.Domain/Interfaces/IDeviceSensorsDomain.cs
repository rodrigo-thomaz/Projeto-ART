namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IDeviceSensorsDomain
    {
        #region Methods

        Task<List<SensorInDevice>> GetAllByDeviceInApplicationId(Guid applicationId, Guid deviceId, DeviceDatasheetEnum deviceDatasheetId);

        Task<DeviceSensors> SetPublishIntervalInMilliSeconds(Guid deviceSensorsId, DeviceDatasheetEnum deviceDatasheetId, int publishIntervalInMilliSeconds);

        #endregion Methods
    }
}