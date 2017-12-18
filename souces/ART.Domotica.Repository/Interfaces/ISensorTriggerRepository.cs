namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorTriggerRepository : IRepository<ARTDbContext, SensorTrigger>
    {
        #region Methods

        Task<List<SensorTrigger>> GetSensorId(Guid sensorId);

        #endregion Methods
    }
}