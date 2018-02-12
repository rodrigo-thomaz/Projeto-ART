namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IDeviceSensorsDomain
    {
        #region Methods

        Task<DeviceSensors> GetFullByDeviceInApplicationId(Guid applicationId, DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId);

        Task<DeviceSensors> SetPublishIntervalInMilliSeconds(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, long publishIntervalInMilliSeconds);

        Task<DeviceSensors> SetReadIntervalInMilliSeconds(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, long readIntervalInMilliSeconds);

        #endregion Methods
    }
}