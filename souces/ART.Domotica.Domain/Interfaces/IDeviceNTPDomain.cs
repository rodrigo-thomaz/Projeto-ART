namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IDeviceNTPDomain
    {
        #region Methods

        Task<DeviceNTP> SetTimeZone(Guid deviceNTPId, Guid deviceDatasheetId, byte timeZoneId);

        Task<DeviceNTP> SetUpdateIntervalInMilliSecond(Guid deviceNTPId, Guid deviceDatasheetId, long updateIntervalInMilliSecond);

        #endregion Methods
    }
}