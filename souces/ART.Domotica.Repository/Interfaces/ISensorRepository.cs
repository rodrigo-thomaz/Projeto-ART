namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorRepository : IRepository<ARTDbContext, Sensor, Guid>
    {
        #region Methods

        Task<List<Sensor>> GetAll(Guid applicationId);

        Task<List<Sensor>> GetAllByDeviceId(Guid deviceId);

        Task<SensorsInDevice> GetDeviceFromSensor(Guid sensorId);

        #endregion Methods
    }
}