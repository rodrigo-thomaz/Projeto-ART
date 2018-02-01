namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceBaseRepository : IRepository<ARTDbContext, DeviceBase>
    {
        #region Methods

        Task<DeviceBase> GetByKey(Guid deviceId, Guid deviceDatasheetId);

        #endregion Methods
    }
}