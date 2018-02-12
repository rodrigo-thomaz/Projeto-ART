namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IDeviceDebugDomain
    {
        #region Methods

        Task<DeviceDebug> SetRemoteEnabled(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, bool value);

        Task<DeviceDebug> SetResetCmdEnabled(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, bool value);

        Task<DeviceDebug> SetSerialEnabled(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, bool value);

        Task<DeviceDebug> SetShowColors(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, bool value);

        Task<DeviceDebug> SetShowDebugLevel(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, bool value);

        Task<DeviceDebug> SetShowProfiler(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, bool value);

        Task<DeviceDebug> SetShowTime(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, bool value);

        #endregion Methods
    }
}