namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IDeviceTypeDomain
    {
        #region Methods

        Task<List<DeviceType>> GetAll();

        #endregion Methods
    }
}