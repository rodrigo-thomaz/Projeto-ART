namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IDeviceSensorsDomain
    {
        #region Methods

        Task<DeviceSensors> SetPublishIntervalInSeconds(Guid deviceSensorsId, int publishIntervalInSeconds);

        #endregion Methods
    }
}