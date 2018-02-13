namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceDebugRepository : IRepository<ARTDbContext, DeviceDebug>
    {
        #region Methods

        Task<List<DeviceDebug>> GetAllByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId);

        Task<DeviceDebug> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId);

        #endregion Methods
    }
}