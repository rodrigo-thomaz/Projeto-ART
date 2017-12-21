namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IDeviceSensorsDomain
    {
        #region Methods

        Task<DeviceSensors> SetPublishIntervalInSeconds(Guid deviceSensorsId, DeviceDatasheetEnum deviceDatasheetId, int publishIntervalInSeconds);

        #endregion Methods
    }
}