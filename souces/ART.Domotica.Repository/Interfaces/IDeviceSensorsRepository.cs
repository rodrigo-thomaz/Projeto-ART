namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceSensorsRepository : IRepository<ARTDbContext, DeviceSensors, Guid>
    {
        #region Methods

        Task<List<DeviceSensors>> GetAllByApplicationId(Guid applicationId);

        #endregion Methods
    }
}