namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorsInDeviceRepository : IRepository<ARTDbContext, SensorsInDevice>
    {
        #region Methods

        Task<List<SensorsInDevice>> GetAllByApplicationId(Guid applicationId);

        #endregion Methods
    }
}