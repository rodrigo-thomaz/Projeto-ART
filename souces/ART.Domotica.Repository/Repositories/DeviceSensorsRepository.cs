namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Linq;

    public class DeviceSensorsRepository : RepositoryBase<ARTDbContext, DeviceSensors>, IDeviceSensorsRepository
    {
        #region Constructors

        public DeviceSensorsRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<DeviceSensors> GetByKey(Guid deviceId, DeviceDatasheetEnum deviceDatasheetId)
        {
            return await _context.DeviceSensors.FindAsync(deviceId, deviceDatasheetId);
        }

        public async Task<List<SensorInDevice>> GetAllByDeviceId(Guid deviceId)
        {
            return await _context.SensorInDevice
                .Include(x => x.Sensor.SensorTempDSFamily.SensorTempDSFamilyResolution)
                .Include(x => x.Sensor.SensorUnitMeasurementScale)
                .Where(x => x.Sensor.SensorInDevice.FirstOrDefault(y => y.DeviceSensors.Id == deviceId) != null)
                .ToListAsync();
        }
    }
}