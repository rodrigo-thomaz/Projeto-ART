namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IHardwareInApplicationRepository : IRepository<ARTDbContext, HardwareInApplication, Guid>
    {
        #region Methods
                

        #endregion Methods
    }
}