namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceSensorsRepository : IRepository<ARTDbContext, DeviceSensors>
    {
        #region Methods

        Task<List<SensorInDevice>> GetAllByDeviceId(Guid deviceId);

        Task<DeviceSensors> GetByKey(Guid deviceId, DeviceDatasheetEnum deviceDatasheetId);

        #endregion Methods
    }
}