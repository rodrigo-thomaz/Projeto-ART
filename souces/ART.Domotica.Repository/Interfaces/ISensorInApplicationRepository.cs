namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorInApplicationRepository : IRepository<ARTDbContext, SensorInApplication>
    {
        #region Methods

        Task<SensorInApplication> GetByKey(Guid applicationId, Guid sensorId);

        #endregion Methods
    }
}