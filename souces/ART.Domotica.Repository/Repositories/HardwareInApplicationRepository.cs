namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class HardwareInApplicationRepository : RepositoryBase<ARTDbContext, HardwareInApplication, Guid>, IHardwareInApplicationRepository
    {
        #region Constructors

        public HardwareInApplicationRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}