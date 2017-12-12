namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface ISensorDomain
    {
        #region Methods

        Task<List<Sensor>> GetAllByApplicationId(Guid applicationId);

        Task<List<Sensor>> GetAllByHardwareInApplicationId(Guid applicationId, Guid deviceId);

        Task<Sensor> GetByKey(Guid sensorId);

        Task<SensorInDevice> GetDeviceFromSensor(Guid sensorId);

        Task<Sensor> SetLabel(Guid sensorId, string label);

        #endregion Methods
    }
}