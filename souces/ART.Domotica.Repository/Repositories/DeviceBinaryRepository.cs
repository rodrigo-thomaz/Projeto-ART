namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Linq;
    using System.Data.Entity;

    public class DeviceBinaryRepository : RepositoryBase<ARTDbContext, DeviceBinary>, IDeviceBinaryRepository
    {
        #region Constructors

        public DeviceBinaryRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<DeviceBinary> GetByKey(Guid deviceBinaryId, Guid deviceDatasheetId)
        {
            return await _context.DeviceBinary.FindAsync(deviceBinaryId, deviceDatasheetId);
        }

        public async Task<DeviceBinary> GetByDeviceMacAdresses(string stationMacAddress, string softAPMacAddress)
        {
            return await _context.ESPDevice
                .Where(x => x.DeviceWiFi.StationMacAddress == stationMacAddress)
                .Where(x => x.DeviceWiFi.SoftAPMacAddress == softAPMacAddress)
                .Select(x => x.DeviceBinary)
                .FirstOrDefaultAsync();
        }
    }
}