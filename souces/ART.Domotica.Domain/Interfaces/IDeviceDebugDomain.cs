namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IDeviceDebugDomain
    {
        #region Methods

        Task<DeviceDebug> SetActive(Guid deviceDebugId, DeviceDatasheetEnum deviceDatasheetId, bool active);

        #endregion Methods
    }
}