namespace ART.Domotica.Repository.Interfaces
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;
    using System.Threading.Tasks;

    public interface IApplicationRepository : IRepository<ARTDbContext, Application, Guid>
    {
        Task<Application> GetFullByKey(Guid applicationId);
    }
}