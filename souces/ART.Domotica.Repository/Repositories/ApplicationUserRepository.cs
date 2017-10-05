namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class ApplicationUserRepository : RepositoryBase<ARTDbContext, ApplicationUser, Guid>, IApplicationUserRepository
    {
        #region Constructors

        public ApplicationUserRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}