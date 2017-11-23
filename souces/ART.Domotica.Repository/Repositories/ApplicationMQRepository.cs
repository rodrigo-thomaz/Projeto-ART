namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class ApplicationMQRepository : RepositoryBase<ARTDbContext, ApplicationMQ, Guid>, IApplicationMQRepository
    {
        #region Constructors

        public ApplicationMQRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}