namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class HardwareInApplicationRepository : RepositoryBase<ARTDbContext, HardwareInApplication, Guid>, IHardwareInApplicationRepository
    {
        #region Constructors

        public HardwareInApplicationRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods        

        #endregion Methods
    }
}