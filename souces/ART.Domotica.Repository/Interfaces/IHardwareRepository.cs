namespace ART.Domotica.Repository.Interfaces
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IHardwareRepository : IRepository<ARTDbContext, HardwareBase, Guid>
    {
        Task<List<string>> GetExistingPins();
        Task<List<HardwareBase>> GetHardwaresNotInApplication();
    }
}