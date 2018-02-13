namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceDisplayRepository : RepositoryBase<ARTDbContext, DeviceDisplay, Guid>, IDeviceDisplayRepository
    {
        #region Constructors

        public DeviceDisplayRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<DeviceDisplay> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId)
        {
            return await _context.DeviceDisplay.FindAsync(deviceTypeId, deviceDatasheetId, deviceId);
        }
    }
}