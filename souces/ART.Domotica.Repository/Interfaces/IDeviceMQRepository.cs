namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceMQRepository : IRepository<ARTDbContext, DeviceMQ>
    {
        #region Methods

        Task<DeviceMQ> GetByKey(Guid deviceId, Guid deviceDatasheetId);

        #endregion Methods
    }
}