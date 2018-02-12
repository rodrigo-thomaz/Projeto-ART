namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Linq;
    using ART.Domotica.Enums;

    public class DeviceSensorsRepository : RepositoryBase<ARTDbContext, DeviceSensors>, IDeviceSensorsRepository
    {
        #region Constructors

        public DeviceSensorsRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<DeviceSensors> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId)
        {
            return await _context.DeviceSensors.FindAsync(deviceTypeId, deviceDatasheetId, deviceId);
        }

        public async Task<DeviceSensors> GetFullByDeviceId(Guid deviceId)
        {
            return await _context.DeviceSensors
                .Include(x => x.SensorInDevice.Select(y => y.Sensor.SensorDatasheet.SensorDatasheetUnitMeasurementDefault))
                .Include(x => x.SensorInDevice.Select(y => y.Sensor.SensorUnitMeasurementScale))
                .Include(x => x.SensorInDevice.Select(y => y.Sensor.SensorTriggers))
                .Include(x => x.SensorInDevice.Select(y => y.Sensor.SensorTempDSFamily.SensorTempDSFamilyResolution))                
                .Where(x => x.Id == deviceId)
                .FirstOrDefaultAsync();
        }
    }
}