namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class SensorInDeviceRepository : RepositoryBase<ARTDbContext, SensorInDevice>, ISensorInDeviceRepository
    {
        #region Constructors

        public SensorInDeviceRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<SensorInDevice> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId, Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId)
        {
            return await _context.SensorInDevice
                .FindAsync(deviceTypeId, deviceDatasheetId, deviceId, sensorId, sensorDatasheetId, sensorTypeId);
        }

        public async Task<List<SensorInDevice>> GetByDeviceSensorKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId)
        {
            return await _context.SensorInDevice
                .Where(x => x.DeviceTypeId == deviceTypeId)
                .Where(x => x.DeviceDatasheetId == deviceDatasheetId)
                .Where(x => x.DeviceId == deviceId)
                .ToListAsync();
        }        
    }
}