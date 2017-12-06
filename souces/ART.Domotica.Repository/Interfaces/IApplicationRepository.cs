namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IApplicationRepository : IRepository<ARTDbContext, Application, Guid>
    {
        #region Methods

        Task<Application> GetFullByKey(Guid applicationId);

        #endregion Methods
    }
}