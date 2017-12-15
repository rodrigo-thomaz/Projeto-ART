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

        public async Task<SensorInDevice> GetByKey(Guid deviceSensorsId, Guid sensorId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId)
        {
            return await _context.SensorInDevice
                .FindAsync(deviceSensorsId, sensorId, sensorDatasheetId, sensorTypeId);
        }

        public async Task<List<SensorInDevice>> GetByDeviceSensorsKey(Guid deviceSensorsId)
        {
            return await _context.SensorInDevice
                .Where(x => x.DeviceSensorsId == deviceSensorsId)
                .ToListAsync();
        }
    }
}