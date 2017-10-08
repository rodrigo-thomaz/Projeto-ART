namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class HardwaresInApplicationRepository : RepositoryBase<ARTDbContext, HardwaresInApplication, Guid>, IHardwaresInApplicationRepository
    {
        #region Constructors

        public HardwaresInApplicationRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}