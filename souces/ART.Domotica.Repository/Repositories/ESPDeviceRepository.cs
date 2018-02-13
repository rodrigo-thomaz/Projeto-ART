namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using ART.Domotica.Enums;

    public class ESPDeviceRepository : RepositoryBase<ARTDbContext, ESPDevice>, IESPDeviceRepository
    {
        #region Constructors

        public ESPDeviceRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public async Task<List<ESPDevice>> GetAll()
        {
            return await _context.ESPDevice
                .Include(x => x.DevicesInApplication)
                .ToListAsync();
        }

        public async Task<ESPDevice> GetByPin(string pin)
        {
            var data = await _context.ESPDevice
                .Include(x => x.DevicesInApplication)
                .Include(x => x.DeviceDebug)
                .Include(x => x.DeviceWiFi)
                .Include(x => x.DeviceMQ)
                .Include(x => x.DeviceNTP.TimeZone)
                .Include(x => x.DeviceSensor.SensorInDevice.Select(y => y.Sensor.SensorTriggers))
                .Include(x => x.DeviceSensor.SensorInDevice.Select(y => y.Sensor.SensorUnitMeasurementScale))
                .Include(x => x.DeviceSensor.SensorInDevice.Select(y => y.Sensor.SensorTempDSFamily))                
                .Where(x => x.Pin == pin)
                .SingleOrDefaultAsync();

            return data;
        }

        public async Task<ESPDevice> GetFullByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId)
        {
            var data = await _context.ESPDevice
               .Include(x => x.DeviceNTP)
               .Include(x => x.DeviceMQ)
               .Include(x => x.DeviceSensor.SensorInDevice.Select(y => y.Sensor.SensorTriggers))
               .Include(x => x.DeviceSensor.SensorInDevice.Select(y => y.Sensor.SensorUnitMeasurementScale))
               .Include(x => x.DeviceSensor.SensorInDevice.Select(y => y.Sensor.SensorTempDSFamily))
               .Where(x => x.DeviceTypeId == deviceTypeId)
               .Where(x => x.DeviceDatasheetId == deviceDatasheetId)
               .Where(x => x.Id == deviceId)               
               .FirstOrDefaultAsync();

            return data;
        }

        public async Task<List<string>> GetExistingPins()
        {
            var data = await _context.ESPDevice
                .Where(x => x.DevicesInApplication.Any())
                .Select(x => x.Pin)
                .ToListAsync();
            return data;
        }

        public async Task<List<ESPDevice>> GetListNotInApplication()
        {
            var data = await _context.ESPDevice
                .Include(x => x.DeviceMQ)
                .Where(x => !x.DevicesInApplication.Any())
                .ToListAsync();

            return data;
        }

        public async Task<ESPDevice> GetDeviceInApplication(int chipId, int flashChipId, string stationMacAddress, string softAPMacAddress)
        {
            var data = await _context.ESPDevice
               .Include(x => x.DevicesInApplication)
               .Include(x => x.DeviceDatasheet)
               .Include(x => x.DeviceDebug)
               .Include(x => x.DeviceWiFi)
               .Include(x => x.DeviceMQ)
               .Include(x => x.DeviceNTP.TimeZone)
               .Where(x => x.ChipId == chipId)
               .Where(x => x.FlashChipId == flashChipId)
               .Where(x => x.DeviceWiFi.StationMacAddress == stationMacAddress)
               .Where(x => x.DeviceWiFi.SoftAPMacAddress == softAPMacAddress)
               .SingleOrDefaultAsync();

            return data;
        }

        public async Task<List<ESPDevice>> GetAllByApplicationId(Guid applicationId)
        {
            var data = await _context.ESPDevice
                .Include(x => x.DevicesInApplication)
                .Include(x => x.DeviceNTP)
                .Include(x => x.DeviceSerial)
                .Include(x => x.DeviceWiFi)
                .Include(x => x.DeviceDebug)
                .Include(x => x.DeviceDisplay)
                .Include(x => x.DeviceMQ)
                .Include(x => x.DeviceBinary.DeviceDatasheetBinary)
                .Include(x => x.DeviceSensor.SensorInDevice)
                .Where(x => x.DevicesInApplication.Any(y => y.ApplicationId == applicationId))
                .ToListAsync();           

            return data;
        }

        public async Task<ESPDevice> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId)
        {
            return await _context.ESPDevice.FindAsync(deviceTypeId, deviceDatasheetId, deviceId);
        }

        #endregion Methods
    }
}