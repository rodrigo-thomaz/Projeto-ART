namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorTriggerRepository : IRepository<ARTDbContext, SensorTrigger, Guid>
    {
        #region Methods

        Task<List<SensorTrigger>> GetSensorBaseId(Guid sensorBaseId);

        #endregion Methods
    }
}