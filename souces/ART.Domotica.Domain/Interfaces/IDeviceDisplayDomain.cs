namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IDeviceDisplayDomain
    {
        #region Methods

        Task<DeviceDisplay> SetEnabled(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, bool value);

        #endregion Methods
    }
}