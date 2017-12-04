using ART.Domotica.Repository.Entities;
using ART.Infra.CrossCutting.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Interfaces
{
    public interface IDeviceSensorsRepository : IRepository<ARTDbContext, DeviceSensors, Guid>
    {
        #region Methods

        Task<List<DeviceSensors>> GetAllByApplicationId(Guid applicationId);

        #endregion Methods
    }
}