namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Linq;
    using System.Data.Entity;
    using ART.Domotica.Enums;

    public class DeviceDatasheetBinaryRepository : RepositoryBase<ARTDbContext, DeviceDatasheetBinary>, IDeviceDatasheetBinaryRepository
    {
        #region Constructors

        public DeviceDatasheetBinaryRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<DeviceDatasheetBinary> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceDatasheetBinaryId)
        {
            return await _context.DeviceDatasheetBinary.FindAsync(deviceTypeId, deviceDatasheetId, deviceDatasheetBinaryId);
        }

        public async Task<DeviceDatasheetBinary> GetLastVersioByDatasheetKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId)
        {
            return await _context.DeviceDatasheetBinary
                .Where(x => x.DeviceTypeId == deviceTypeId)
                .Where(x => x.DeviceDatasheetId == deviceDatasheetId)
                .OrderByDescending(x => x.CreateDate)                
                .FirstOrDefaultAsync();
        }
    }
}