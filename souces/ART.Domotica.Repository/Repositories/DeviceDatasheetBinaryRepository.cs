namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceDatasheetBinaryRepository : RepositoryBase<ARTDbContext, DeviceDatasheetBinary>, IDeviceDatasheetBinaryRepository
    {
        #region Constructors

        public DeviceDatasheetBinaryRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<DeviceDatasheetBinary> GetByKey(Guid deviceDatasheetBinaryId, DeviceDatasheetEnum deviceDatasheetId)
        {
            return await _context.DeviceDatasheetBinary.FindAsync(deviceDatasheetBinaryId, deviceDatasheetId);
        }
    }
}