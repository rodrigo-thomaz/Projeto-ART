namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceDebugRepository : IRepository<ARTDbContext, DeviceDebug>
    {
        #region Methods

        Task<DeviceDebug> GetByKey(Guid deviceId, Guid deviceDatasheetId);

        #endregion Methods
    }
}