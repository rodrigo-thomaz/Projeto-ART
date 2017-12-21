namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IDeviceMQDomain
    {
        #region Methods

        Task<DeviceMQ> GetByKey(Guid deviceMQId, DeviceDatasheetEnum deviceDatasheetId);

        #endregion Methods
    }
}