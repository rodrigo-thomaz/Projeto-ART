namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IApplicationRepository : IRepository<ARTDbContext, Application, Guid>
    {
        #region Methods

        Task<List<Application>> GetAll(Guid applicationUserId);

        #endregion Methods
    }
}