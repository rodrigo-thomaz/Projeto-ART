namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IDeviceSerialDomain
    {
        #region Methods

        Task<DeviceSerial> GetByKey(Guid deviceSerialId, Guid deviceId, Guid deviceDatasheetId);

        Task<DeviceSerial> SetEnabled(Guid deviceSerialId, Guid deviceId, Guid deviceDatasheetId, bool enabled);

        Task<DeviceSerial> SetPin(Guid deviceSerialId, Guid deviceId, Guid deviceDatasheetId, short value, CommunicationDirection direction);

        #endregion Methods
    }
}