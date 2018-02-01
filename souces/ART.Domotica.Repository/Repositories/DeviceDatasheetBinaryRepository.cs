namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Linq;
    using System.Data.Entity;

    public class DeviceDatasheetBinaryRepository : RepositoryBase<ARTDbContext, DeviceDatasheetBinary>, IDeviceDatasheetBinaryRepository
    {
        #region Constructors

        public DeviceDatasheetBinaryRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<DeviceDatasheetBinary> GetByKey(Guid deviceDatasheetBinaryId, Guid deviceDatasheetId)
        {
            return await _context.DeviceDatasheetBinary.FindAsync(deviceDatasheetBinaryId, deviceDatasheetId);
        }

        public async Task<DeviceDatasheetBinary> GetLastVersioByDatasheetKey(Guid deviceDatasheetId)
        {
            return await _context.DeviceDatasheetBinary
                .Where(x => x.DeviceDatasheetId == deviceDatasheetId)
                .OrderByDescending(x => x.CreateDate)                
                .FirstOrDefaultAsync();
        }
    }
}