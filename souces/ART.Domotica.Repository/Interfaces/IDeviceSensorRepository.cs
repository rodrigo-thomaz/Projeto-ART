namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceSensorRepository : IRepository<ARTDbContext, DeviceSensor>
    {
        #region Methods

        Task<DeviceSensor> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId);

        Task<DeviceSensor> GetFullByDeviceId(Guid deviceId);

        #endregion Methods
    }
}