namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceNTPRepository : IRepository<ARTDbContext, DeviceNTP>
    {
        #region Methods

        Task<DeviceNTP> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId);

        #endregion Methods
    }
}