namespace ART.Domotica.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceDatasheetRepository : IRepository<ARTDbContext, DeviceDatasheet, DeviceDatasheetEnum>
    {
        #region Methods

        Task<List<DeviceDatasheet>> GetAll();

        #endregion Methods
    }
}