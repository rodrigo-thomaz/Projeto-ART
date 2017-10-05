namespace ART.Data.Repository.Repositories
{
    using System;

    using ART.Data.Repository.Entities;
    using ART.Data.Repository.Interfaces;
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