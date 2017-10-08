namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class HardwareRepository : RepositoryBase<ARTDbContext, HardwareBase, Guid>, IHardwareRepository
    {
        #region Constructors

        public HardwareRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}