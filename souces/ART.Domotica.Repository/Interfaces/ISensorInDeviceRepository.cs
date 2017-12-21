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

        Task<List<SensorInDevice>> GetByDeviceSensorsKey(Guid deviceSensorsId, DeviceDatasheetEnum deviceDatasheetId);

        Task<SensorInDevice> GetByKey(Guid deviceSensorsId, DeviceDatasheetEnum deviceDatasheetId, Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId);

        #endregion Methods
    }
}