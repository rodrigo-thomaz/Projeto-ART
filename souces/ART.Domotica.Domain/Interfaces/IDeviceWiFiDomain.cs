namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IDeviceWiFiDomain
    {
        #region Methods

        Task<DeviceWiFi> SetHostName(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, string hostName);

        Task<DeviceWiFi> SetPublishIntervalInMilliSeconds(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, long publishIntervalInMilliSeconds);

        #endregion Methods
    }
}