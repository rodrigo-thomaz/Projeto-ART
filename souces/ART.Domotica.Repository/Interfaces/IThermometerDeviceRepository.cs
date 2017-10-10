namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IThermometerDeviceRepository : IRepository<ARTDbContext, ThermometerDevice, Guid>
    {
        #region Methods

        Task<List<ThermometerDevice>> GetList();

        #endregion Methods
    }
}