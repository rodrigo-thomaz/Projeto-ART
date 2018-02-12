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

    public class DeviceSensorRepository : RepositoryBase<ARTDbContext, DeviceSensor>, IDeviceSensorRepository
    {
        #region Constructors

        public DeviceSensorRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<DeviceSensor> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId)
        {
            return await _context.DeviceSensor.FindAsync(deviceTypeId, deviceDatasheetId, deviceId);
        }

        public async Task<DeviceSensor> GetFullByDeviceId(Guid deviceId)
        {
            return await _context.DeviceSensor
                .Include(x => x.SensorInDevice.Select(y => y.Sensor.SensorDatasheet.SensorDatasheetUnitMeasurementDefault))
                .Include(x => x.SensorInDevice.Select(y => y.Sensor.SensorUnitMeasurementScale))
                .Include(x => x.SensorInDevice.Select(y => y.Sensor.SensorTriggers))
                .Include(x => x.SensorInDevice.Select(y => y.Sensor.SensorTempDSFamily.SensorTempDSFamilyResolution))                
                .Where(x => x.Id == deviceId)
                .FirstOrDefaultAsync();
        }
    }
}