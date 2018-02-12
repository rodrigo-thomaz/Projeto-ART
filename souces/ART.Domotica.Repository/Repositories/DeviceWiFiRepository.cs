namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceWiFiRepository : RepositoryBase<ARTDbContext, DeviceWiFi, Guid>, IDeviceWiFiRepository
    {
        #region Constructors

        public DeviceWiFiRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<DeviceWiFi> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId)
        {
            return await _context.DeviceWiFi.FindAsync(deviceTypeId, deviceDatasheetId, deviceId);
        }
    }
}