namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IApplicationMQRepository : IRepository<ARTDbContext, ApplicationMQ, Guid>
    {
        #region Methods

        Task<ApplicationMQ> GetByHardwareId(Guid hardwareId);

        #endregion Methods
    }
}