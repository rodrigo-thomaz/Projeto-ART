namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorRepository : IRepository<ARTDbContext, Sensor>
    {
        #region Methods

        Task<List<Sensor>> GetAllByApplicationId(Guid applicationId);

        Task<List<Sensor>> GetAllByDeviceId(Guid deviceId);

        Task<Sensor> GetByKey(Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId);

        Task<SensorInDevice> GetDeviceFromSensor(Guid sensorId);

        Task<List<SensorInApplication>> GetSensorsInApplicationByDeviceId(Guid applicationId, Guid deviceId);

        #endregion Methods
    }
}