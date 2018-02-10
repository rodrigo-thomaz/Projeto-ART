namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceSerialRepository : IRepository<ARTDbContext, DeviceSerial>
    {
        #region Methods

        Task<DeviceSerial> GetByKey(Guid deviceSerialId, Guid deviceId, Guid deviceDatasheetId);

        #endregion Methods
    }
}