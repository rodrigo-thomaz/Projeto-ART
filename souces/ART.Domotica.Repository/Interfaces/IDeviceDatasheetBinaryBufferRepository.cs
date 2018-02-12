namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceDatasheetBinaryBufferRepository : IRepository<ARTDbContext, DeviceDatasheetBinaryBuffer>
    {
        #region Methods

        Task<DeviceDatasheetBinaryBuffer> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceDatasheetBinaryBufferId);

        #endregion Methods
    }
}