namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IDeviceInApplicationDomain
    {
        #region Methods

        Task<ESPDevice> Insert(Guid applicationId, Guid createByApplicationUserId, string pin);

        Task<DeviceBase> Remove(Guid applicationId, Guid deviceId, Guid deviceDatasheetId);

        #endregion Methods
    }
}