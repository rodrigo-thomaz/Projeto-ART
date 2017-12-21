namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IDeviceNTPDomain
    {
        #region Methods

        Task<DeviceNTP> SetTimeZone(Guid deviceNTPId, DeviceDatasheetEnum deviceDatasheetId, byte timeZoneId);

        Task<DeviceNTP> SetUpdateIntervalInMilliSecond(Guid deviceNTPId, DeviceDatasheetEnum deviceDatasheetId, int updateIntervalInMilliSecond);

        #endregion Methods
    }
}