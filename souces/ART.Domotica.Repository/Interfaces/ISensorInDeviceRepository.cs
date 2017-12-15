namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;
    using System.Collections.Generic;

    public interface ISensorInDeviceRepository : IRepository<ARTDbContext, SensorInDevice>
    {
        Task<SensorInDevice> GetByKey(Guid deviceSensorsId, Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId);

        Task<List<SensorInDevice>> GetByDeviceSensorsKey(Guid deviceSensorsId);
    }
}