namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IDeviceSerialDomain
    {
        #region Methods

        Task<List<DeviceSerial>> GetAllByDeviceKey(Guid applicationId, DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId);

        Task<DeviceSerial> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, Guid deviceSerialId);

        Task<DeviceSerial> SetEnabled(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, Guid deviceSerialId, bool enabled);

        Task<DeviceSerial> SetPin(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, Guid deviceSerialId, short value, CommunicationDirection direction);

        #endregion Methods
    }
}