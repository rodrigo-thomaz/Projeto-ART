namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorTriggerRepository : IRepository<ARTDbContext, SensorTrigger>
    {
        #region Methods

        Task<SensorTrigger> GetByKey(Guid sensorTriggerId, Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId);

        Task<List<SensorTrigger>> GetSensorId(Guid sensorId);

        #endregion Methods
    }
}