namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceDatasheetRepository : IRepository<ARTDbContext, DeviceDatasheet, Guid>
    {
        #region Methods

        Task<List<DeviceDatasheet>> GetAll();

        #endregion Methods
    }
}