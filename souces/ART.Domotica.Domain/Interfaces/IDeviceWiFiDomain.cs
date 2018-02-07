namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IDeviceWiFiDomain
    {
        #region Methods

        Task<DeviceWiFi> SetHostName(Guid deviceWiFiId, Guid deviceDatasheetId, string hostName);

        Task<DeviceWiFi> SetPublishIntervalInMilliSeconds(Guid deviceWiFiId, Guid deviceDatasheetId, long publishIntervalInMilliSeconds);

        #endregion Methods
    }
}