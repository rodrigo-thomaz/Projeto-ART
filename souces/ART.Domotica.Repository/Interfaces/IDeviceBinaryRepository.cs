namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceBinaryRepository : IRepository<ARTDbContext, DeviceBinary>
    {
        #region Methods

        Task<DeviceBinary> GetByKey(Guid deviceBinaryId, DeviceDatasheetEnum deviceDatasheetId);

        #endregion Methods
    }
}