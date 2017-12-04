namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using ART.Domotica.Repository.Entities;

    public interface IDeviceSensorsDomain
    {
        #region Methods

        Task<List<DeviceSensors>> GetAllByApplicationId(Guid applicationId);

        #endregion Methods
    }
}