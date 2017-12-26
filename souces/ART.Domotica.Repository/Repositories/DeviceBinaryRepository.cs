namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceBinaryRepository : RepositoryBase<ARTDbContext, DeviceBinary>, IDeviceBinaryRepository
    {
        #region Constructors

        public DeviceBinaryRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<DeviceBinary> GetByKey(Guid deviceBinaryId, DeviceDatasheetEnum deviceDatasheetId)
        {
            return await _context.DeviceBinary.FindAsync(deviceBinaryId, deviceDatasheetId);
        }
    }
}