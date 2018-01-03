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

        public async Task<DeviceWiFi> GetByKey(Guid deviceId, DeviceDatasheetEnum deviceDatasheetId)
        {
            return await _context.DeviceWiFi.FindAsync(deviceId, deviceDatasheetId);
        }
    }
}