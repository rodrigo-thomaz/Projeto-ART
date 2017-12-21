namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IDeviceDatasheetDomain
    {
        #region Methods

        Task<List<DeviceDatasheet>> GetAll();

        #endregion Methods
    }
}