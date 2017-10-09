namespace ART.Domotica.Repository.Interfaces
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IHardwaresInApplicationRepository : IRepository<ARTDbContext, HardwaresInApplication, Guid>
    {
        #region Methods

        Task<List<HardwaresInApplication>> GetList();

        #endregion Methods
    }
}