namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using ART.Domotica.Enums;
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

        public async Task<DeviceSerial> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, Guid deviceSerialId)
        {
            return await _context.DeviceSerial.FindAsync(deviceTypeId, deviceDatasheetId, deviceId, deviceSerialId);
        }

        public async Task<List<DeviceSerial>> GetAllByDeviceKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId)
        {
            return await _context.DeviceSerial
                .Where(x => x.DeviceTypeId == deviceTypeId)
                .Where(x => x.DeviceDatasheetId == deviceDatasheetId)
                .Where(x => x.DeviceId == deviceId)                
                .ToListAsync();
        }
    }
}