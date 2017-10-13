namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IHardwareRepository : IRepository<ARTDbContext, HardwareBase, Guid>
    {
        #region Methods

        Task<List<string>> GetExistingPins();

        Task<List<HardwareBase>> GetHardwaresNotInApplication();

        #endregion Methods
    }
}