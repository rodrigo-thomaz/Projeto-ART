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

        Task<DeviceSensors> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId);

        Task<DeviceSensors> GetFullByDeviceId(Guid deviceId);

        #endregion Methods
    }
}