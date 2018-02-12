namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IDeviceNTPDomain
    {
        #region Methods

        Task<DeviceNTP> SetTimeZone(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, byte timeZoneId);

        Task<DeviceNTP> SetUpdateIntervalInMilliSecond(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, long updateIntervalInMilliSecond);

        #endregion Methods
    }
}