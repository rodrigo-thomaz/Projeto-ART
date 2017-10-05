namespace ART.Data.Repository.Interfaces
{
    using System;

    using ART.Data.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IApplicationUserRepository : IRepository<ARTDbContext, ApplicationUser, Guid>
    {
    }
}