namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceSerialRepository : IRepository<ARTDbContext, DeviceSerial>
    {
        #region Methods

        Task<List<DeviceSerial>> GetAllByDeviceKey(Guid deviceId, Guid deviceDatasheetId);

        Task<DeviceSerial> GetByKey(Guid deviceSerialId, Guid deviceId, Guid deviceDatasheetId);

        #endregion Methods
    }
}