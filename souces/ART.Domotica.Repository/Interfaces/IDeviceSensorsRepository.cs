namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceSensorsRepository : IRepository<ARTDbContext, DeviceSensors>
    {
        #region Methods

        Task<DeviceSensors> GetFullByDeviceId(Guid deviceId);

        Task<DeviceSensors> GetByKey(Guid deviceId, DeviceDatasheetEnum deviceDatasheetId);

        #endregion Methods
    }
}