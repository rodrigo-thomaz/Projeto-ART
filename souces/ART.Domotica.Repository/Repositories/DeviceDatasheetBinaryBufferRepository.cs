namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceDatasheetBinaryBufferRepository : RepositoryBase<ARTDbContext, DeviceDatasheetBinaryBuffer>, IDeviceDatasheetBinaryBufferRepository
    {
        #region Constructors

        public DeviceDatasheetBinaryBufferRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<DeviceDatasheetBinaryBuffer> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceDatasheetBinaryBufferId)
        {
            return await _context.DeviceDatasheetBinaryBuffer.FindAsync(deviceTypeId, deviceDatasheetId, deviceDatasheetBinaryBufferId);
        }
    }
}