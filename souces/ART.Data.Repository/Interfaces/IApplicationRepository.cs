namespace ART.Data.Repository.Interfaces
{
    using System;

    using ART.Data.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IApplicationRepository : IRepository<ARTDbContext, Application, Guid>
    {
    }
}