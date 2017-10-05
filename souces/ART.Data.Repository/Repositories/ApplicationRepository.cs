namespace ART.Data.Repository.Repositories
{
    using System;

    using ART.Data.Repository.Entities;
    using ART.Data.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class ApplicationRepository : RepositoryBase<ARTDbContext, Application, Guid>, IApplicationRepository
    {
        #region Constructors

        public ApplicationRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}