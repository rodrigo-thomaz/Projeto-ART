namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceDatasheetBinaryRepository : IRepository<ARTDbContext, DeviceDatasheetBinary>
    {
        #region Methods

        Task<DeviceDatasheetBinary> GetByKey(Guid deviceDatasheetBinaryId, Guid deviceDatasheetId);

        Task<DeviceDatasheetBinary> GetLastVersioByDatasheetKey(Guid deviceDatasheetId);

        #endregion Methods
    }
}