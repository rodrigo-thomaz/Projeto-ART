namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IDeviceSensorsDomain
    {
        #region Methods

        Task<DeviceSensors> GetFullByDeviceInApplicationId(Guid applicationId, Guid deviceId, Guid deviceDatasheetId);

        Task<DeviceSensors> SetPublishIntervalInMilliSeconds(Guid deviceSensorsId, Guid deviceDatasheetId, int publishIntervalInMilliSeconds);

        Task<DeviceSensors> SetReadIntervalInMilliSeconds(Guid deviceSensorsId, Guid deviceDatasheetId, int readIntervalInMilliSeconds);

        #endregion Methods
    }
}