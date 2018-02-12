namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorInDeviceRepository : IRepository<ARTDbContext, SensorInDevice>
    {
        #region Methods

        Task<List<SensorInDevice>> GetByDeviceSensorsKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId);

        Task<SensorInDevice> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId);

        #endregion Methods
    }
}