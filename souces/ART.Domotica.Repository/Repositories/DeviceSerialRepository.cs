namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceSerialRepository : RepositoryBase<ARTDbContext, DeviceSerial, Guid>, IDeviceSerialRepository
    {
        #region Constructors

        public DeviceSerialRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<DeviceSerial> GetByKey(Guid deviceSerialId, Guid deviceId, Guid deviceDatasheetId)
        {
            return await _context.DeviceSerial.FindAsync(deviceSerialId, deviceId, deviceDatasheetId);
        }

        public async Task<List<DeviceSerial>> GetAllByDeviceKey(Guid deviceId, Guid deviceDatasheetId)
        {
            return await _context.DeviceSerial
                .Where(x => x.DeviceId == deviceId)
                .Where(x => x.DeviceDatasheetId == deviceDatasheetId)
                .ToListAsync();
        }
    }
}