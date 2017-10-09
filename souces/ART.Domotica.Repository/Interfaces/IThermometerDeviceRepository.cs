namespace ART.Domotica.Repository.Interfaces
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IThermometerDeviceRepository : IRepository<ARTDbContext, ThermometerDevice, Guid>
    {
        #region Methods

        Task<List<ThermometerDevice>> GetList();

        #endregion Methods
    }
}