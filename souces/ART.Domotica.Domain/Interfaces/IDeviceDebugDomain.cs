namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IDeviceDebugDomain
    {
        #region Methods

        Task<DeviceDebug> SetRemoteEnabled(Guid deviceDebugId, Guid deviceDatasheetId, bool value);

        Task<DeviceDebug> SetResetCmdEnabled(Guid deviceDebugId, Guid deviceDatasheetId, bool value);

        Task<DeviceDebug> SetSerialEnabled(Guid deviceDebugId, Guid deviceDatasheetId, bool value);

        Task<DeviceDebug> SetShowColors(Guid deviceDebugId, Guid deviceDatasheetId, bool value);

        Task<DeviceDebug> SetShowDebugLevel(Guid deviceDebugId, Guid deviceDatasheetId, bool value);

        Task<DeviceDebug> SetShowProfiler(Guid deviceDebugId, Guid deviceDatasheetId, bool value);

        Task<DeviceDebug> SetShowTime(Guid deviceDebugId, Guid deviceDatasheetId, bool value);

        #endregion Methods
    }
}