namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IHardwaresInApplicationRepository : IRepository<ARTDbContext, HardwaresInApplication, Guid>
    {
        #region Methods

        Task<List<HardwaresInApplication>> GetList(Guid applicationUserId);

        Task<HardwareBase> SearchPin(string pin);

        #endregion Methods
    }
}