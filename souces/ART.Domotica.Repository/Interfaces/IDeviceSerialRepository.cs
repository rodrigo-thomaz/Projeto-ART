namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceSerialRepository : IRepository<ARTDbContext, DeviceSerial>
    {
        #region Methods

        Task<List<DeviceSerial>> GetAllByDeviceKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId);

        Task<DeviceSerial> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, Guid deviceSerialId);

        #endregion Methods
    }
}