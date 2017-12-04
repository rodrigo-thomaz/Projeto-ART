namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IDeviceSernsorsDomain
    {
        #region Methods

        Task<List<DeviceSernsors>> GetAllByApplicationId(Guid applicationId);

        #endregion Methods
    }
}